using Synevyr.Infrastructure;
using Synevyr.Models;

namespace Synevyr.Services;

public class DataUpdateService : IHostedService
{
    private readonly RaiderIoApi _api;
    private readonly IRepository<GuildMemberModel> _memberRepo;
    private readonly IRepository<DungeonRunModel> _dungeonRepo;
    private bool IsRunning = true;

    public DataUpdateService(RaiderIoApi api, IRepository<GuildMemberModel> memberRepo, IRepository<DungeonRunModel> dungeonRepo)
    {
        _api = api;
        _memberRepo = memberRepo;
        _dungeonRepo = dungeonRepo;
    }

    public async Task UpdateGuildMembers()
    {
        var response = await _api.GetGuildInfo("silvermoon","synevyr");

        var members = response.members;

        foreach (var member in members)
        {
            var characterInfo = await _api.GetCharacterInfo("silvermoon", member.character.name, "season-df-1&tier=29");
        
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
            .Where(x=>x.Rank != 9 && x.Rank != 99)
            .ToList();

        var period = await _api.GetPeriods();
        var currentPeriod = period.periods.First(x => x.region == "eu").current;
        foreach (var member in members)
        {
            var runs = await _api.GetRuns(member.CharacterId, currentPeriod.start);

            foreach (var run in runs.runs)
            {
                var isExist = _dungeonRepo.AsQuaryable().Any(x => x.RunId == run.summary.keystone_run_id);
                if (isExist) continue;
         
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
                    PeriodStart =  DateTime.Parse(currentPeriod.start),
                    PeriodEnd = DateTime.Parse(currentPeriod.end)
                });
            }
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (IsRunning)
            {
                await UpdateGuildMembers();
                await Task.Delay(60 * 1000, cancellationToken);
                await UpdateRuns();
                await Task.Delay(TimeSpan.FromHours(3), cancellationToken);
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        IsRunning = false;
        return Task.CompletedTask;
    }
}