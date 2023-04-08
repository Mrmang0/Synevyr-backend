using Synevyr.Models;

namespace Synevyr.Services;

public class RaiderIoApi
{
    private readonly HttpClient _client;

    public RaiderIoApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<RaiderIoGuildResponse?> GetGuildInfo(string realm, string name)
    {
        var response = await _client.GetFromJsonAsync<RaiderIoGuildResponse>($"v1/guilds/profile?region=eu&realm={realm}&name={name}&fields=members");
        return response;
    }
    
    public async Task<RaiderIoUserResponse?> GetUserProfile(string name)
    {
        var response = await _client.GetFromJsonAsync<RaiderIoUserResponse>($"profile?region=eu&realm=silvermoon&name={name}&fields=mythic_plus_recent_runs");
        return response;
    }

    public async Task<RaiderIoCharacterResponse> GetCharacterInfo(string realm, string name, string season)
    {
        var response =
            await _client.GetFromJsonAsync<RaiderIoCharacterResponse>($"characters/eu/{realm}/{name}?season={season}");
        return response;
    }

    public async Task<RaiderIoRunsResponse> GetRuns(int characterId,string date)
    {
        var response = await _client.GetFromJsonAsync<RaiderIoRunsResponse>(
            $"characters/mythic-plus-runs?season=season-df-1&characterId={characterId}&affixes=all&date={date}");
        return response;
    }

    public async Task<RaiderIoRunDetailsResponse> GetRunDetails(int runId, string season)
    {
        var response = await _client.GetFromJsonAsync<RaiderIoRunDetailsResponse>(
            $"v1/mythic-plus/run-details?season={season}&id={runId}");
        return response;
    }

    public async Task<RaiderIoStaticDataResponse> GetSeasonInfo(int id)
    {
        var response = await _client.GetFromJsonAsync<RaiderIoStaticDataResponse>($"v1/mythic-plus/static-data?expansion_id={id}");
        return response;
    }

    public async Task<RaiderIoPeriodsResponse> GetPeriods()
    {
        var response = await _client.GetFromJsonAsync<RaiderIoPeriodsResponse>($"v1/periods");
        return response;
    }

}

