namespace Synevyr.Models;

public class RaiderIoGuildResponse
{
    public string name { get; set; }
    public string faction { get; set; }
    public string region { get; set; }
    public string realm { get; set; }
    public string last_crawled_at { get; set; }
    public string profile_url { get; set; }
    public Members[] members { get; set; }
}

public class Members
{
    public int rank { get; set; }
    public Character character { get; set; }
}

public partial class Character
{
    public string name { get; set; }
    public string race { get; set; }
    public string @class { get; set; }
    public string active_spec_name { get; set; }
    public string active_spec_role { get; set; }
    public string gender { get; set; }
    public string faction { get; set; }
    public int achievement_points { get; set; }
    public int honorable_kills { get; set; }
    public string region { get; set; }
    public string realm { get; set; }
    public string last_crawled_at { get; set; }
    public string profile_url { get; set; }
    public string profile_banner { get; set; }
    public string id { get; set; }
    public string thumbnailUrl { get; set; }
}

