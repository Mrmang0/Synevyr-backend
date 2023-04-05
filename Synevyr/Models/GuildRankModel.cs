using Synevyr.Infrastructure;

namespace Synevyr.Models;

public class GuildRankModel : Entity
{
    public int RankId { get; set; }
    public string Name { get; set; }
}