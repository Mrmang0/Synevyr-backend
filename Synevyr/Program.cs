using Synevyr.Infrastructure;
using Synevyr.Models;
using Synevyr.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .Build();
builder.Services.Configure<MongodbSettings>(builder.Configuration.GetSection(nameof(MongodbSettings)));
builder.Services.AddTransient(typeof(IRepository<>), typeof(MongoDbRepository<>));
builder.Services.AddHttpClient<RaiderIoApi>((client =>
{
    client.BaseAddress = new Uri("https://raider.io/api/");
}));
builder.Services.AddHostedService<DataUpdateService>();
var app = builder.Build();

app.MapGet("api/tgb/members", async (RaiderIoApi api, IRepository<GuildMemberModel> repo) =>
{
    var response = await api.GetGuildInfo("silvermoon","synevyr");

    var members = response
        .members;

    foreach (var member in members)
    {
        var characterInfo = await api.GetCharacterInfo("silvermoon", member.character.name, "season-df-1&tier=29");
        
        repo.Save(new GuildMemberModel()
        {
            Name = member.character.name,
            Rank = member.rank,
            CharacterId = characterInfo.characterDetails.character.id,
            Picture = characterInfo.characterDetails.character.thumbnailUrl
        });
    }
});

app.MapGet("api/tgb/dungeons/{slug}", async (RaiderIoApi api, IRepository<DungeonModel> repo, string slug) =>
{
    if (string.IsNullOrEmpty(slug))
    {
        slug = "season-df-1";
    }
    var result = await api.GetSeasonInfo(9);

    foreach (var dungeon in result.seasons.FirstOrDefault(x=>x.slug == slug).dungeons)
    {
        repo.Save(new DungeonModel()
        {
            Name = dungeon.name,
            DungeonId = dungeon.id,
            ShortName = dungeon.short_name
        });
    }
});

app.MapGet("api/tgb/runs", async (RaiderIoApi api, IRepository<DungeonRunModel> repo, IRepository<GuildMemberModel> memberRepo) =>
{
    var members = memberRepo
        .AsQuaryable()
        .Where(x=>x.Rank != 9 && x.Rank != 99)
        .ToList();

    var period = await api.GetPeriods();
    var currentPeriod = period.periods.First(x => x.region == "eu").current;
    foreach (var member in members)
    {
        var runs = await api.GetRuns(member.CharacterId, currentPeriod.start);

        foreach (var run in runs.runs)
        {
            var isExist = repo.AsQuaryable().Any(x => x.RunId == run.summary.keystone_run_id);
            if (isExist) continue;
         
            var details = await api.GetRunDetails(run.summary.keystone_run_id, run.summary.season);
            var runMembers = details.roster.ToList().Select(x => new RunMember()
            {
                Id = memberRepo.AsQuaryable().FirstOrDefault(y => y.CharacterId == x.character.id)?.Id ??
                     Guid.Empty,
                CharacterId = x.character.id,
                Rio = x.ranks.score,
                Name = x.character.name
            });

            repo.Save(new DungeonRunModel()
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
});

app.MapGet("api/tgb", (IRepository<GuildMemberModel> membersRepo, IRepository<DungeonRunModel> runsRepo, DateTime start) =>
{
   var members =  membersRepo.AsQuaryable().Where(x => x.Rank != 9 && x.Rank != 99).ToList();
   var runs = runsRepo.AsQuaryable()
       .Where(x=>x.PeriodStart == start)
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
           var carriages = run.Members.Where(x => x.Id != Guid.Empty && x.Id != member.Id && rio - x.Rio > 250).ToList();
           if(!carriages.Any())
               break;
           var rioSum = carriages.Select((x, i) => 10 - i * 15 * 10 / 100).Sum();
           var score = rioSum + run.KeyLevel * 5d;
           if (run.TimeGate > run.TimeSpent) score *= 1.5;
           else score += 10;
           tgbResult.Score += score;
           tgbResult.TimeInKeys += TimeSpan.FromMilliseconds(run.TimeSpent);
           tgbResult.KeysClosed++;
           tgbResult.Carriages.AddRange(carriages);
       }

       tgbResult.Carriages = tgbResult.Carriages.DistinctBy(x=>x.Id).ToList();
       result.Add(tgbResult);
   }
   return result.Where(x=>x.Score != 0).OrderByDescending(x=>x.Score);
});

app.MapGet("api/tgb/periods", (IRepository<DungeonRunModel> runsRepo) =>
{
    return runsRepo.AsQuaryable().Select(x => new
    {
        Start = x.PeriodStart,
        End = x.PeriodEnd
    }).ToList().DistinctBy(x=>x.Start);
});


app.Run();

