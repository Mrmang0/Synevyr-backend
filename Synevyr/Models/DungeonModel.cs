using Synevyr.Infrastructure;

namespace Synevyr.Models;

public class DungeonModel : Entity
{
    public string Name { get; set; }
    public int DungeonId { get; set; }
    public string ShortName { get; set; }
}