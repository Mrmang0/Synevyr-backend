namespace Synevyr.Models;

public class RaiderIoRunsResponse
{
    public Runs[] runs { get; set; }
    public Ui ui { get; set; }
}

public class Runs
{
    public Summary summary { get; set; }
    public double score { get; set; }
}

public class Summary
{
    public string season { get; set; }
    public string status { get; set; }
    public Dungeon dungeon { get; set; }
    public int keystone_run_id { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int keystone_time_ms { get; set; }
    public string completed_at { get; set; }
    public int num_chests { get; set; }
    public int time_remaining_ms { get; set; }
    public int? logged_run_id { get; set; }
    public Weekly_modifiers[] weekly_modifiers { get; set; }
    public int num_modifiers_active { get; set; }
    public string faction { get; set; }
    public object deleted_at { get; set; }
    public string role { get; set; }
}

public class Dungeon
{
    public int id { get; set; }
    public string name { get; set; }
    public string short_name { get; set; }
    public string slug { get; set; }
    public int expansion_id { get; set; }
    public string patch { get; set; }
    public int keystone_timer_ms { get; set; }
    public int num_bosses { get; set; }
    public int group_finder_activity_id { get; set; }
}

public class Weekly_modifiers
{
    public int id { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

public class Ui
{
    public string season { get; set; }
    public int characterId { get; set; }
    public string role { get; set; }
    public string mode { get; set; }
    public string affixes { get; set; }
    public string date { get; set; }
}

