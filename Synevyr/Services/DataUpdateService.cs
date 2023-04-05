using Synevyr.Infrastructure;
using Synevyr.Models;

namespace Synevyr.Services;

public class DataUpdateService : IHostedService
{
    private readonly TheGreatBoostService _tgbService;
    private bool IsRunning = true;
    private Timer _timer;

    public DataUpdateService(TheGreatBoostService tgbService)
    {
        _tgbService = tgbService;
     
    }

    public async Task SetTime(CancellationToken cancellationToken)
    {
        _timer = new Timer(async (x) => await UpdateRutine(cancellationToken), null, TimeSpan.Zero, TimeSpan.FromHours(2));
    }
    
    private async Task UpdateRutine(CancellationToken cancellationToken)
    {
        await _tgbService.UpdateGuildMembers();
        await Task.Delay(60 * 1000, cancellationToken);
        await _tgbService.UpdateRuns();
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