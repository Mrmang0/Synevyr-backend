namespace Synevyr.Models;

public class RaiderIoStaticDataResponse
{
    public Seasons[] seasons { get; set; }
    public Dungeons[] dungeons { get; set; }
}

public class Seasons
{
    public string slug { get; set; }
    public string name { get; set; }
    public string short_name { get; set; }
    public Seasonal_affix seasonal_affix { get; set; }
    public Starts starts { get; set; }
    public Ends ends { get; set; }
    public SeasonDungeons[] dungeons { get; set; }
}

public class Seasonal_affix
{
    public int id { get; set; }
    public string name { get; set; }
    public string icon { get; set; }
}

public class Starts
{
    public string us { get; set; }
    public string eu { get; set; }
    public string tw { get; set; }
    public string kr { get; set; }
    public string cn { get; set; }
}

public class Ends
{
    public object us { get; set; }
    public object eu { get; set; }
    public object tw { get; set; }
    public object kr { get; set; }
    public object cn { get; set; }
}

public class SeasonDungeons
{
    public int id { get; set; }
    public int challenge_mode_id { get; set; }
    public string slug { get; set; }
    public string name { get; set; }
    public string short_name { get; set; }
}

public class Dungeons
{
    public int id { get; set; }
    public int challenge_mode_id { get; set; }
    public string slug { get; set; }
    public string name { get; set; }
    public string short_name { get; set; }
}

