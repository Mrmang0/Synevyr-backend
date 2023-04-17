using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Synevyr.Infrastructure;
using Synevyr.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(so =>
{
    so.Limits.MaxConcurrentConnections = 100;
    so.Limits.MaxConcurrentUpgradedConnections = 100;
    so.Limits.MaxRequestBodySize = 52428800;
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
});
var serilog = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();
builder.Logging.AddSerilog(serilog);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .Build();
builder.Services.Configure<MongodbSettings>(builder.Configuration.GetSection(nameof(MongodbSettings)));
builder.Services.AddTransient(typeof(IRepository<>), typeof(MongoDbRepository<>));
builder.Services.AddTransient<TheGreatBoostService>();
builder.Services.AddTransient<RosterService>();
builder.Services.AddTransient<DungeonService>();
builder.Services.AddHttpClient<RaiderIoApi>((client =>
{
    client.BaseAddress = new Uri("https://raider.io/api/");
}));
builder.Services.AddHttpClient<IcyVeinsRssService>((client =>
{
    client.BaseAddress = new Uri("https://www.icy-veins.com/forums/forum/9-news.xml");
}));
builder.Services.AddHttpClient<WowheadRssService>((client =>
{
    client.BaseAddress = new Uri("https://www.wowhead.com/news/rss/all");
}));
builder.Services.AddTransient<NewsService>();

builder.Services.AddHostedService<DataUpdateService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(x => x.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors();


app.MapGet("api/tgb", (TheGreatBoostService service, DateTime start) => service.GetRuns(start));
app.MapGet("api/tgb/periods", (TheGreatBoostService service) => service.GetPerriods());
app.MapGet("api/tgb/lastUpdate", (TheGreatBoostService service) => service.GetLastUpdate());
app.MapGet("api/roster", (RosterService service,bool descending,string search, string sortField) => service.GetRoster(descending,search, sortField));
app.MapGet("api/dungeons",
    (DungeonService service, string names, DateTime? start, DateTime? end, int take, int skip, bool descending,
            int minKey, int maxKey, int dungeonId) =>
        service.GetRuns(names, start, end, skip, take, descending, minKey, maxKey, dungeonId));
app.MapGet("api/dungeons/names", (DungeonService service) => service.GetDungeons());
app.MapGet("api/dungeons/charts",
    (DungeonService service, string names, DateTime? start, DateTime? end,
            int minKey, int maxKey, int dungeonId) =>
        service.GetChartsData(names, start, end, minKey, maxKey, dungeonId));
app.MapGet("api/members/search", (RosterService service, string name) => service.GetGuildMembersNamesSearch(name));
app.MapGet("api/news", (NewsService service) => service.GetNews());

app.Run();

