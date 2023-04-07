using Synevyr.Infrastructure;
using Synevyr.Models;
using Synevyr.Models.Dtos;

namespace Synevyr.Services;

public class DungeonService
{
    private readonly IRepository<DungeonRunModel> _runRepo;
    private readonly IRepository<DungeonModel> _dungeonRepo;
    private readonly RaiderIoApi _api;

    public DungeonService(IRepository<DungeonRunModel> runRepo, IRepository<DungeonModel> dungeonRepo, RaiderIoApi api)
    {
        _runRepo = runRepo;
        _dungeonRepo = dungeonRepo;
        _api = api;
    }

    public IEnumerable<DungeonStatsDto> GetRuns(string[] names, DateTime? start, DateTime? end)
    {
        var runs = _runRepo.AsQuaryable();

        if (names.Any(x => !string.IsNullOrEmpty(x)))
        {
            runs = names
                .Where(x => !string.IsNullOrEmpty(x))
                .Aggregate(runs, (current, name) => current.Where(x => x.Members.Any(x => x.Name == name)));
        }

        if (start.HasValue)
            runs = runs.Where(x => x.PeriodStart >= start);

        if (end.HasValue)
            runs = runs.Where(x => x.PeriodStart <= end);

        var dungeons = _dungeonRepo.AsQuaryable().ToList();

        var result = runs.ToList()
            .Select(x =>
                new DungeonStatsDto(dungeons.FirstOrDefault(y => x.DungeonId == y.DungeonId)?.Name ?? "Unkown",
                    x.Members,
                    x.PeriodStart,
                    x.PeriodEnd, 
                    TimeSpan.FromMilliseconds(x.TimeSpent), 
                    TimeSpan.FromMilliseconds(x.TimeGate),
                    x.TimeSpent < x.TimeGate,
                    x.KeyLevel,
                    x.Score,
                    x.completedAt));
        return result;
    }

    public async Task MAKE_DUNGEONS_GREAT_AGAIN()
    {
        var runs = _runRepo.AsQuaryable().ToList();

        foreach (var run in runs)
        {
            var result = await _api.GetRunDetails(run.RunId, "season-df-1");

            run.Season = result.season;
            run.DungeonId = result.dungeon.id;
            run.completedAt = result.completed_at;
            
            _runRepo.Save(run);
            await Task.Delay(500);
        }
            
    }
}

