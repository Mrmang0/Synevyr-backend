using MongoDB.Driver;
using Synevyr.Infrastructure;
using Synevyr.Models;
using Synevyr.Models.Dtos;

namespace Synevyr.Services;

public class TheGreatBoostService
{
    private readonly RaiderIoApi _api;
    private readonly IRepository<GuildMemberModel> _memberRepo;
    private readonly IRepository<DungeonRunModel> _dungeonRepo;
    private readonly IRepository<UpdateDetails> _updateRepo;
    private readonly ILogger<TheGreatBoostService> _logger;

    public TheGreatBoostService(RaiderIoApi api, IRepository<GuildMemberModel> memberRepo,
        IRepository<DungeonRunModel> dungeonRepo, IRepository<UpdateDetails> updateRepo,
        ILogger<TheGreatBoostService> logger)
    {
        _api = api;
        _memberRepo = memberRepo;
        _dungeonRepo = dungeonRepo;
        _updateRepo = updateRepo;
        _logger = logger;
    }

    private async Task UpdateRunInfo(Models.Runs run, Current currentPeriod)
    {
        var isExist = _dungeonRepo.AsQuaryable().Any(x => x.RunId == run.summary.keystone_run_id);
        if (isExist) return;

        var details = await _api.GetRunDetails(run.summary.keystone_run_id, run.summary.season);
        var runMembers = details.roster.ToList().Select(x => new RunMember()
        {
            Id = _memberRepo.AsQuaryable().FirstOrDefault(y => y.CharacterId == x.character.id)?.Id ??
                 Guid.Empty,
            CharacterId = x.character.id,
            Rio = x.ranks.score,
            Name = x.character.name
        });

        _dungeonRepo.Save(new DungeonRunModel()
        {
            RunId = run.summary.keystone_run_id,
            Members = runMembers,
            TimeGate = run.summary.keystone_time_ms,
            TimeSpent = run.summary.clear_time_ms,
            KeyLevel = details.mythic_level,
            Score = details.score,
            PeriodStart = DateTime.Parse(currentPeriod.start),
            PeriodEnd = DateTime.Parse(currentPeriod.end)
        });

        var entity = _updateRepo.AsQuaryable().FirstOrDefault() ?? new UpdateDetails();
        entity.LastUpdate = DateTime.Now;
        _updateRepo.Save(entity);
    }

    private async Task UpdateGuildMemberInfo(Members member)
    {
        var characterInfo = await _api.GetCharacterInfo("silvermoon", member.character.name, "season-df-1&tier=29");

        var existiongMember = _memberRepo.AsQuaryable()
            .FirstOrDefault(x => x.CharacterId == characterInfo.characterDetails.character.id);

        if (existiongMember != null)
        {
            existiongMember.Rank = member.rank;
            existiongMember.Picture = characterInfo.characterDetails.character.thumbnailUrl;

            _memberRepo.Save(existiongMember);
        }
        else
        {
            _memberRepo.Save(new GuildMemberModel()
            {
                Name = member.character.name,
                Rank = member.rank,
                CharacterId = characterInfo.characterDetails.character.id,
                Picture = characterInfo.characterDetails.character.thumbnailUrl,
                Rio = characterInfo.characterDetails.bestMythicPlusScore.score
            });
        }
    }

    public async Task UpdateGuildMembers()
    {
        var response = await _api.GetGuildInfo("silvermoon", "synevyr");

        var currentMembers = _memberRepo.AsQuaryable().ToList();
        var members = response.members;
        var notExistingMembers = currentMembers.Where(y => members.All(x => x.character.name != y.Name)).ToList();
        
        foreach (var member in notExistingMembers)
        {
            _memberRepo.Remove(member);
        }
        
        foreach (var member in members)
        {
            try
            {
                await UpdateGuildMemberInfo(member);
            }
            catch (Exception e)
            {
                _logger.LogError("{e}", e);
            }

        }
    }

    public async Task UpdateRuns()
    {
        var members = _memberRepo
            .AsQuaryable()
            .ToList();

        var period = await _api.GetPeriods();
        var currentPeriod = period.periods.First(x => x.region == "eu").current;
        foreach (var member in members)
        {
            var runs = await _api.GetRuns(member.CharacterId, currentPeriod.start);

            foreach (var run in runs.runs)
            {
                try
                {
                    await UpdateRunInfo(run, currentPeriod);
                }
                catch (Exception e)
                {
                    _logger.LogError("{e}", e);
                }
            }
        }
    }

    public IEnumerable<WeekPeriod> GetPerriods()
    {
        return _dungeonRepo.AsQuaryable().Select(x => new WeekPeriod
        {
            Start = x.PeriodStart,
            End = x.PeriodEnd
        }).ToList().DistinctBy(x => x.Start);
    }

    public UpdateDetails? GetLastUpdate()
    {
        return _updateRepo.AsQuaryable().FirstOrDefault();
    }

    public IEnumerable<TGBResult> GetRuns(DateTime start)
    {
        
        var bannedRanks = new[] {0, 1, 2, 8, 9, 99};

        var members = _memberRepo.AsQuaryable().Where(x => bannedRanks.All(y => x.Rank != y)).ToList();
        var runs = _dungeonRepo.AsQuaryable()
            .Where(x => x.PeriodStart == start)
            .Where(x => x.Members.Count(x => x.Id != Guid.Empty) > 1)
            .ToList();

        var result = new List<TGBResult>();

        foreach (var member in members)
        {
            var tgbResult = new TGBResult()
            {
                Name = member.Name,
                Score = 0,
                PictureUrl = member.Picture,
                TimeInKeys = TimeSpan.Zero,
                KeysClosed = 0,
                Carriages = new List<RunMember>()
            };
            var participatedRuns = runs.Where(x => x.Members.Any(x => x.Id == member.Id));

            foreach (var run in participatedRuns)
            {
                var rio = run.Members.First(x => x.Id == member.Id).Rio;
                var carriages = run.Members.Where(x => x.Id != Guid.Empty && x.Id != member.Id && rio - x.Rio > 250)
                    .ToList();

                var guildMembers =
                    carriages.Select(x => _memberRepo.AsQuaryable()
                            .FirstOrDefault(y => x.Id == y.Id && bannedRanks.All(z => y.Rank != z)))
                        .Where(x => x != null);

                if (!guildMembers?.Any() ?? false) continue;

                carriages = carriages.Where(x => guildMembers.Any(y => y.Id == x.Id)).ToList();

                if (!carriages.Any())
                    continue;
                var rioSum = carriages.Select((x, i) => 10 - i * 15 * 10 / 100).Sum();
                var score = rioSum + run.KeyLevel * 5d;
                if (run.TimeGate > run.TimeSpent) score *= 1.5;
                else score += 10;
                tgbResult.Score += score;
                tgbResult.TimeInKeys += TimeSpan.FromMilliseconds(run.TimeSpent);
                tgbResult.KeysClosed++;
                tgbResult.Carriages.AddRange(carriages);
            }

            tgbResult.Carriages = tgbResult.Carriages.DistinctBy(x => x.Id).ToList();
            result.Add(tgbResult);
        }

        return result.Where(x => x.Score != 0).OrderByDescending(x => x.Score);
    }
}