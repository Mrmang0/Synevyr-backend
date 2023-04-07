namespace Synevyr.Models.Dtos;

public record DungeonStatsDto(string Name, IEnumerable<RunMember> Members, DateTime PeriodStart, DateTime PeriodEnd,
    int TimeSpent, int TimeGate, bool InTime, int KeyLevel, double Scroe, string argCompletedAt)
{
    public override string ToString()
    {
        return $"{{ Name = {Name}, Members = {Members}, PeriodStart = {PeriodStart}, PeriodEnd = {PeriodEnd}, TimeSpent = {TimeSpent}, TimeGate = {TimeGate}, InTime = {InTime}, KeyLevel = {KeyLevel}, Scroe = {Scroe} }}";
    }
}