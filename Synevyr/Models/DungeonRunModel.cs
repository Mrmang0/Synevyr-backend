using Synevyr.Infrastructure;

namespace Synevyr.Models;

public class DungeonRunModel : Entity
{
    public int RunId { get; set; }
    public int TimeSpent { get; set; }
    public int TimeGate { get; set; }
    public IEnumerable<RunMember> Members { get; set; }
    public int KeyLevel { get; set; }
    public double Score { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public int DungeonId { get; set; }
    public string Season { get; set; }
    public DateTime completedAt { get; set; }
}

public class RunMember
{
    public Guid Id { get; set; }
    public int CharacterId { get; set; }
    public double Rio { get; set; }
    public string Name { get; set; }
}