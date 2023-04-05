using Synevyr.Infrastructure;

namespace Synevyr.Models;

public class GuildMemberModel : Entity
{
    public string Name { get; set; }
    public int CharacterId { get; set; }
    public string Picture { get; set; }
    public int Rank { get; set; }
    public double Rio { get; set; }
    public string CharacterClass { get; set; }
    public int? ItemLevel { get; set; }
    public string Spec { get; set; }
}