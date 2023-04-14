using Microsoft.AspNetCore.Mvc;
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

    public SearchResult<DungeonStatsDto> GetRuns(string names, DateTime? start, DateTime? end, int skip, int take, bool descending, int minKeyLevel, int maxKeyLevel, int dungeonId)
    {
        var runs = _runRepo.AsQuaryable();
        runs = descending ? runs.OrderByDescending(x => x.completedAt) : runs.OrderBy(x => x.completedAt);
        
        if (!string.IsNullOrEmpty(names.Replace("\"", "")))
        {
            var splitedNames = names.Split(',');
            runs = runs.Where(x => x.Members.Any(y => splitedNames.Any(d => d == y.Name)));
        }

        if (dungeonId > 0)
        {
            runs = runs.Where(x => x.DungeonId == dungeonId);
        }
        
        if (start.HasValue)
            runs = runs.Where(x => x.PeriodStart >= start);

        if (end.HasValue)
            runs = runs.Where(x => x.PeriodStart <= end);

        var dungeons = _dungeonRepo.AsQuaryable().ToList();

        var count = runs.Count();
        
        if (skip != 0)
        {
            runs = runs.Skip(skip);
        }

        if (take != 0)
        {
            runs = runs.Take(take);
        }

        runs = runs.Where(x => x.KeyLevel >= minKeyLevel);
        if(maxKeyLevel > 0)
         runs = runs.Where(x => x.KeyLevel <= maxKeyLevel);
        
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
        return new SearchResult<DungeonStatsDto>()
        {
            Count = count,
            Result = result
        };
    }

    public async Task<ChartsResponse> GetChartsData(string names, DateTime? start, DateTime? end, int minKeyLevel, int maxKeyLevel, int dungeonId)
    {
        var runs =  GetRuns(names, start, end, 0, 0, false, minKeyLevel, maxKeyLevel, dungeonId);

        var groupedByKey = runs.Result.GroupBy(x => x.KeyLevel)
            .Select(x => new ChartData<int>(x.Key, x.Count()))
            .OrderBy(x=>x.key);
        var groupedByMembers = runs.Result.GroupBy(x => x.Members.Count(x => x.Id != Guid.Empty))
            .Select(x => new ChartData<int>(x.Key, x.Count()))
            .OrderBy(x=>x.key);
        var groupedByHours = runs.Result.Where(x=>x.CompletedAt != DateTime.MinValue)
            .GroupBy(x => (x?.CompletedAt - x?.TimeSpent)?.Hour ?? -2).Where(x=>x.Key != -2)
            .Select(x => new ChartData<int>(x.Key, x.Count()))
            .OrderBy(x=>x.key);
        var groupedByDays = runs.Result.GroupBy(x => (x.CompletedAt.DayOfWeek))
            .Select(x => new ChartData<int>((int)x.Key, x.Count()))
            .OrderBy(x=>x.key);
        var groupedByMonth = runs.Result.GroupBy(x => (x.CompletedAt.Month))
            .Select(x => new ChartData<int>(x.Key, x.Count()))
            .OrderBy(x=>x.key);
        var timeline = runs.Result
            .GroupBy(x => (x.CompletedAt.Date))
            .Select(x => new ChartData<DateTime>(x.Key, x.Count()))
            .OrderBy(x=>x.key);
        var timeRatio = runs.Result.GroupBy(x => (x.InTime))
            .Select(x => new ChartData<bool>(x.Key, x.Count()))
            .OrderBy(x=>x.key);


        return new ChartsResponse()
        {
            Ratio = timeRatio,
            ByMember = groupedByMembers,
            Timeline = timeline,
            ByDays = groupedByDays,
            ByHours = groupedByHours,
            ByKey = groupedByKey,
            ByMonths = groupedByMonth
        };
    }

    public async Task MAKE_DUNGEONS_GREAT_AGAIN()
    {
        var runs = _runRepo.AsQuaryable().ToList();

        foreach (var run in runs)
        {
            // var result = await _api.GetRunDetails(run.RunId, "season-df-1");
            //
            // run.Season = result.season;
            // run.DungeonId = result.dungeon.id;
            // run.completedAt = DateTime.Parse(result.completed_at);
            
            _runRepo.Save(run);
        }
            
    }
}

public record ChartData<T>(T key, int count);

public class ChartsResponse
{
    public IEnumerable<ChartData<int>> ByKey { get; set; }
    public IEnumerable<ChartData<int>> ByMember { get; set; }
    public IEnumerable<ChartData<int>> ByHours { get; set; }
    public IEnumerable<ChartData<int>> ByDays { get; set; }
    public IEnumerable<ChartData<int>> ByMonths { get; set; }
    public IEnumerable<ChartData<DateTime>> Timeline { get; set; }
    public IEnumerable<ChartData<bool>> Ratio { get; set; }
}