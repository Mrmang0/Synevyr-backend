using System.Xml.Serialization;
using Synevyr.Models;

namespace Synevyr.Services;

public class WowheadRssService
{
    private readonly HttpClient _httpClient;

    public WowheadRssService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<WowheadRss> GetNews()
    {
        var response = await _httpClient.GetStringAsync("");
        response = response.Replace("media:content", "content");
        var serializer = new XmlSerializer(typeof(WowheadRss));
        using var reader = new StringReader(response);
        return (WowheadRss) serializer.Deserialize(reader)!;
    }
}