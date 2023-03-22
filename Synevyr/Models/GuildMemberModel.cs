using Synevyr.Infrastructure;

namespace Synevyr.Models;

public class GuildMemberModel : Entity
{
    public string Name { get; set; }
    public int CharacterId { get; set; }
    public string Picture { get; set; }
    public int Rank { get; set; }
}