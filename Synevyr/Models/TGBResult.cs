namespace Synevyr.Models;

public class TGBResult
{
    public string Name { get; set; }
    public TimeSpan TimeInKeys { get; set; }
    public double Score { get; set; }
    public string PictureUrl { get; set; }
    public int KeysClosed { get; set; }
    public List<RunMember> Carriages { get; set; }
}