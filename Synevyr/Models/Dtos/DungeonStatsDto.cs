namespace Synevyr.Models.Dtos;

public record DungeonStatsDto(string Name, IEnumerable<RunMember> Members, DateTime PeriodStart, DateTime PeriodEnd,
    TimeSpan TimeSpent, TimeSpan TimeGate, bool InTime, int KeyLevel, double Scroe, string CompletedAt)
{
    public override string ToString()
    {
        return $"{{ Name = {Name}, Members = {Members}, PeriodStart = {PeriodStart}, PeriodEnd = {PeriodEnd}, TimeSpent = {TimeSpent}, TimeGate = {TimeGate}, InTime = {InTime}, KeyLevel = {KeyLevel}, Scroe = {Scroe} }}";
    }
}