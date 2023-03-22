namespace Synevyr.Models;

public class RaiderIoRunDetailsResponse
{
    public string season { get; set; }
    public string status { get; set; }
    public DungeonDetails dungeon { get; set; }
    public int keystone_run_id { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int keystone_time_ms { get; set; }
    public string completed_at { get; set; }
    public int num_chests { get; set; }
    public int time_remaining_ms { get; set; }
    public object logged_run_id { get; set; }
    public Weekly_modifiers[] weekly_modifiers { get; set; }
    public int num_modifiers_active { get; set; }
    public string faction { get; set; }
    public object deleted_at { get; set; }
    public double score { get; set; }
    public object logged_details { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public bool isTournamentProfile { get; set; }
    public Roster[] roster { get; set; }
    public object[] loggedSources { get; set; }
}

public class DungeonDetails
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


public class Roster
{
    public RosterCharacter character { get; set; }
    public object oldCharacter { get; set; }
    public bool isTransfer { get; set; }
    public Guild guild { get; set; }
    public string role { get; set; }
    public Items items { get; set; }
    public Ranks ranks { get; set; }
}

public class RosterCharacter
{
    public int id { get; set; }
    public int persona_id { get; set; }
    public string name { get; set; }
    public Race race { get; set; }
    public string faction { get; set; }
    public int level { get; set; }
    public Spec spec { get; set; }
    public string path { get; set; }
    public Realm realm { get; set; }
    public Region region { get; set; }
    public object stream { get; set; }
    public object[] recruitmentProfiles { get; set; }
    public TalentLoadout talentLoadout { get; set; }
}

public class Class
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
}

public class Race
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public string faction { get; set; }
}

public class Spec
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public int class_id { get; set; }
    public string role { get; set; }
    public bool is_melee { get; set; }
}

public class Realm
{
    public int id { get; set; }
    public int connectedRealmId { get; set; }
    public string name { get; set; }
    public object altName { get; set; }
    public string slug { get; set; }
    public string altSlug { get; set; }
    public string locale { get; set; }
    public bool isConnected { get; set; }
}

public class Region
{
    public string name { get; set; }
    public string slug { get; set; }
    public string short_name { get; set; }
}

public class TalentLoadout
{
    public int specId { get; set; }
    public Loadout[] loadout { get; set; }
    public string loadoutText { get; set; }
}

public class Loadout
{
    public Node node { get; set; }
    public int entryIndex { get; set; }
    public int rank { get; set; }
}

public class Node
{
    public int id { get; set; }
    public int treeId { get; set; }
    public int type { get; set; }
    public Entries[] entries { get; set; }
    public bool important { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }
    public int row { get; set; }
    public int col { get; set; }
}

public class Entries
{
    public int id { get; set; }
    public int traitDefinitionId { get; set; }
    public int type { get; set; }
    public int maxRanks { get; set; }
    public Spell spell { get; set; }
}

public class Spell
{
    public int id { get; set; }
    public string name { get; set; }
    public string icon { get; set; }
    public int school { get; set; }
    public string rank { get; set; }
    public int hasCooldown { get; set; }
}

public class Guild
{
    public int id { get; set; }
    public string name { get; set; }
    public string faction { get; set; }
    public Realm1 realm { get; set; }
    public Region1 region { get; set; }
    public string path { get; set; }
}

public class Realm1
{
    public int id { get; set; }
    public int connectedRealmId { get; set; }
    public string name { get; set; }
    public object altName { get; set; }
    public string slug { get; set; }
    public string altSlug { get; set; }
    public string locale { get; set; }
    public bool isConnected { get; set; }
}

public class Region1
{
    public string name { get; set; }
    public string slug { get; set; }
    public string short_name { get; set; }
}



public class Corruption
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
    public int cloakRank { get; set; }
    public object[] spells { get; set; }
}




public class Ranks
{
    public double score { get; set; }
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

