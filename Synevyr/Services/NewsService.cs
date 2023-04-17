using Synevyr.Models.Dtos;

namespace Synevyr.Services;

public class NewsService
{
    private readonly WowheadRssService _wowheadRss;
    private readonly IcyVeinsRssService _icyVeinsRss;

    public NewsService(WowheadRssService wowheadRss, IcyVeinsRssService icyVeinsRss)
    {
        _wowheadRss = wowheadRss;
        _icyVeinsRss = icyVeinsRss;
    }

    public async Task<IEnumerable<NewsDto>> GetNews()
    {
        var wowheadNews = await _wowheadRss.GetNews();
        var icyveinsNews = await _icyVeinsRss.GetNews();

        var result = wowheadNews.Channel.Item.Select(news => new NewsDto()
            {
                Title = news.Title,
                PubDate = DateTime.Parse(news.PubDate),
                Description = news.Description,
                Link = news.Link,
                Category = news.Category,
                Image = news.Content?.Url ?? ""
            })
            .ToList();
        
        
        
        result.AddRange(icyveinsNews.Channel.Item.Select(news => new NewsDto()
        {
            Title = news.Title,
            PubDate = DateTime.Parse(news.PubDate),
            Description = news.Description,
            Link = news.Link,
            Category = "IcyVeins",
            Image = ""
        }));

        return result.OrderByDescending(x=>x.PubDate);
    }
}