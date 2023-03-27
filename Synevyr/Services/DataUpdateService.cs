using Synevyr.Infrastructure;
using Synevyr.Models;

namespace Synevyr.Services;

public class DataUpdateService : IHostedService
{
    private readonly RaiderIoApi _api;
    private readonly IRepository<GuildMemberModel> _memberRepo;
    private readonly IRepository<DungeonRunModel> _dungeonRepo;
    private readonly IRepository<UpdateDetails> _updateRepo;
    private readonly ILogger<DataUpdateService> _logger;
    private bool IsRunning = true;
    private Timer _timer;

    public DataUpdateService(RaiderIoApi api, IRepository<GuildMemberModel> memberRepo, IRepository<DungeonRunModel> dungeonRepo, IRepository<UpdateDetails> updateRepo, ILogger<DataUpdateService> logger)
    {
        _api = api;
        _memberRepo = memberRepo;
        _dungeonRepo = dungeonRepo;
        _updateRepo = updateRepo;
        _logger = logger;
    }

    public async Task UpdateGuildMembers()
    {
        var response = await _api.GetGuildInfo("silvermoon","synevyr");

        var members = response.members;

        foreach (var member in members)
        {
            try
            {
                await UpdateGuildMemberInfo(member);
            }
            catch (Exception e)
            {
                _logger.LogError("{e}",e);
            }
            
        }
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
                Picture = characterInfo.characterDetails.character.thumbnailUrl
            });
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
                    _logger.LogError("{e}",e);
                }
            }
        }
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

    public async Task SetTime(CancellationToken cancellationToken)
    {
        _timer = new Timer(async (x) => await UpdateRutine(cancellationToken), null, TimeSpan.Zero, TimeSpan.FromHours(2));
    }
    
    private async Task UpdateRutine(CancellationToken cancellationToken)
    {
        await UpdateGuildMembers();
        await Task.Delay(60 * 1000, cancellationToken);
        await UpdateRuns();
        await Task.Delay(TimeSpan.FromHours(3), cancellationToken);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await SetTime(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _timer.DisposeAsync();
        IsRunning = false;
    }
}