using System.Xml.Serialization;
using Synevyr.Models;

namespace Synevyr.Services;

public class IcyVeinsRssService
{
    private readonly HttpClient _client;

    public IcyVeinsRssService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Rss> GetNews()
    {
        var response = await _client.GetStringAsync("");

        var serializer = new XmlSerializer(typeof(Rss));
        using var reader = new StringReader(response);
        return (Rss) serializer.Deserialize(reader)!;
    }
}