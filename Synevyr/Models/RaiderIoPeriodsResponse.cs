namespace Synevyr.Models;

public class RaiderIoPeriodsResponse
{
    public Periods[] periods { get; set; }
}

public class Periods
{
    public string region { get; set; }
    public Previous previous { get; set; }
    public Current current { get; set; }
    public Next next { get; set; }
}

public class Previous
{
    public int period { get; set; }
    public string start { get; set; }
    public string end { get; set; }
}

public class Current
{
    public int period { get; set; }
    public string start { get; set; }
    public string end { get; set; }
}

public class Next
{
    public int period { get; set; }
    public string start { get; set; }
    public string end { get; set; }
}

