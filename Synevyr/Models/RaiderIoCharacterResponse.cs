using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;

public class RaiderIoCharacterResponse
{
    public CharacterDetails characterDetails { get; set; }
}

public class CharacterDetails
{
    public DetailedCharacter character { get; set; }
    public object team { get; set; }
    // public RaidProgress[] raidProgress { get; set; }
    // public ItemDetails itemDetails { get; set; }
    public string seasonSlug { get; set; }
    // public MythicPlusScores mythicPlusScores { get; set; }
    public BestMythicPlusScore bestMythicPlusScore { get; set; }
    public string season { get; set; }
    public string tier { get; set; }
    public bool isMissingPersonaFields { get; set; }
    public bool isTournamentProfile { get; set; }
}

public class DetailedCharacter
{
    // public TalentLoadout talentLoadout { get; set; }
    // public string talents { get; set; }
    // public object[] talentsDetails { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public string faction { get; set; }
    public int? level { get; set; }
    public string path { get; set; }
    public object[] recruitmentProfiles { get; set; }
    public int? achievementPoints { get; set; }
    public int? honorableKills { get; set; }
    public int? itemLevelEquipped { get; set; }
    [JsonPropertyName("class")]
    public CharacterClass @class { get; set; }
    public ClassSpec spec { get; set; }
    public string thumbnailUrl { get; set; }
    // public Guild guild { get; set; }
}
public class ClassSpec
{
    public string name { get; set; }
    public string slug { get; set; }
}


public class CharacterClass
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
}



public class Covenant
{
    public int id { get; set; }
    public string name { get; set; }
    public string icon { get; set; }
    public int renownLevel { get; set; }
    public Soulbind soulbind { get; set; }
}

public class Soulbind
{
    public int id { get; set; }
    public string name { get; set; }
}

public class TalentLoadout
{
    public Nodes[] nodes { get; set; }
    public int specId { get; set; }
    public string loadoutText { get; set; }
}

public class Nodes
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
    public object rank { get; set; }
    public int hasCooldown { get; set; }
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
    public string name { get; set; }
    public string slug { get; set; }
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

public class CharacterCustomizations
{
    public object profile_banner { get; set; }
    public object profile_frame { get; set; }
    public object biography { get; set; }
    public object main_character { get; set; }
    public object bnet_battletag { get; set; }
    public string twitch_profile { get; set; }
    public string youtube_profile { get; set; }
    public string twitter_profile { get; set; }
    public string discord_profile { get; set; }
    public string wowhead_profile { get; set; }
    public bool isClaimed { get; set; }
}

public class Meta
{
    public string firstCrawledAt { get; set; }
    public string lastCrawledAt { get; set; }
    public string loggedOutAt { get; set; }
    public object missingAt { get; set; }
}

public class RaidProgress
{
    public string raid { get; set; }
    public string aotc { get; set; }
    public object cuttingEdge { get; set; }
    public EncountersDefeated encountersDefeated { get; set; }
}

public class EncountersDefeated
{
    public Normal[] normal { get; set; }
    public Heroic[] heroic { get; set; }
    public Mythic[] mythic { get; set; }
}

public class Normal
{
    public string slug { get; set; }
    public string firstDefeated { get; set; }
    public int itemLevel { get; set; }
    public int artifactTraits { get; set; }
    public int numKills { get; set; }
    public string lastDefeated { get; set; }
}

public class Heroic
{
    public string slug { get; set; }
    public string firstDefeated { get; set; }
    public int itemLevel { get; set; }
    public int artifactTraits { get; set; }
    public int numKills { get; set; }
    public string lastDefeated { get; set; }
}

public class Mythic
{
    public string slug { get; set; }
    public string firstDefeated { get; set; }
    public int itemLevel { get; set; }
    public int artifactTraits { get; set; }
    public int numKills { get; set; }
    public string lastDefeated { get; set; }
}

public class ItemDetails
{
    public string updated_at { get; set; }
    public double item_level_equipped { get; set; }
    public int artifact_traits { get; set; }
    public Corruption corruption { get; set; }
    public Items items { get; set; }
}

public class Corruption
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
    public int cloakRank { get; set; }
    public object[] spells { get; set; }
}

public class Items
{
    public Head head { get; set; }
    public Neck neck { get; set; }
    public Shoulder shoulder { get; set; }
    public Back back { get; set; }
    public Chest chest { get; set; }
    public Waist waist { get; set; }
    public Wrist wrist { get; set; }
    public Hands hands { get; set; }
    public Legs legs { get; set; }
    public Feet feet { get; set; }
    public Finger1 finger1 { get; set; }
    public Finger2 finger2 { get; set; }
    public Trinket1 trinket1 { get; set; }
    public Trinket2 trinket2 { get; set; }
    public Mainhand mainhand { get; set; }
}

public class Head
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public Azerite_powers[] azerite_powers { get; set; }
    public Corruption1 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public string tier { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Azerite_powers
{
    public int id { get; set; }
    public Spell1 spell { get; set; }
    public int tier { get; set; }
}

public class Spell1
{
    public int id { get; set; }
    public int school { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public object rank { get; set; }
}

public class Corruption1
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Neck
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption2 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public int[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption2
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Shoulder
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption3 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public string tier { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption3
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Back
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption4 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption4
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Chest
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption5 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public string tier { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption5
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Waist
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption6 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption6
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Wrist
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption7 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public int[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption7
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Hands
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption8 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption8
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Legs
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption9 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public string tier { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption9
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Feet
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption10 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption10
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Finger1
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption11 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public int[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption11
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Finger2
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption12 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption12
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Trinket1
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption13 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption13
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Trinket2
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption14 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption14
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class Mainhand
{
    public int item_id { get; set; }
    public int item_level { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public int item_quality { get; set; }
    public bool is_legendary { get; set; }
    public bool is_azerite_armor { get; set; }
    public object[] azerite_powers { get; set; }
    public Corruption15 corruption { get; set; }
    public object[] domination_shards { get; set; }
    public object[] gems { get; set; }
    public int[] bonuses { get; set; }
}

public class Corruption15
{
    public int added { get; set; }
    public int resisted { get; set; }
    public int total { get; set; }
}

public class MythicPlusScores
{
    public All all { get; set; }
    public Dps dps { get; set; }
    public Healer healer { get; set; }
    public Tank tank { get; set; }
    public Spec_0 spec_0 { get; set; }
    public Spec_1 spec_1 { get; set; }
    public Spec_2 spec_2 { get; set; }
    public Spec_3 spec_3 { get; set; }
}

public class All
{
    public double score { get; set; }
    public string scoreColor { get; set; }
    public Runs[] runs { get; set; }
    public AlternateRuns[] alternateRuns { get; set; }
    public RawRuns[] rawRuns { get; set; }
    public RawAlternateRuns[] rawAlternateRuns { get; set; }
}

public class Runs
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class AlternateRuns
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class RawRuns
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class RawAlternateRuns
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class Dps
{
    public double score { get; set; }
    public string scoreColor { get; set; }
    public Runs1[] runs { get; set; }
    public AlternateRuns1[] alternateRuns { get; set; }
    public RawRuns1[] rawRuns { get; set; }
    public RawAlternateRuns1[] rawAlternateRuns { get; set; }
}

public class Runs1
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class AlternateRuns1
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class RawRuns1
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class RawAlternateRuns1
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class Healer
{
    public double score { get; set; }
    public string scoreColor { get; set; }
    public Runs2[] runs { get; set; }
    public AlternateRuns2[] alternateRuns { get; set; }
    public RawRuns2[] rawRuns { get; set; }
    public RawAlternateRuns2[] rawAlternateRuns { get; set; }
}

public class Runs2
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class AlternateRuns2
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class RawRuns2
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class RawAlternateRuns2
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class Tank
{
    public int score { get; set; }
    public string scoreColor { get; set; }
    public object[] runs { get; set; }
    public object[] alternateRuns { get; set; }
    public object[] rawRuns { get; set; }
    public object[] rawAlternateRuns { get; set; }
}

public class Spec_0
{
    public double score { get; set; }
    public string scoreColor { get; set; }
    public Runs3[] runs { get; set; }
    public AlternateRuns3[] alternateRuns { get; set; }
    public RawRuns3[] rawRuns { get; set; }
    public RawAlternateRuns3[] rawAlternateRuns { get; set; }
}

public class Runs3
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class AlternateRuns3
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class RawRuns3
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class RawAlternateRuns3
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class Spec_1
{
    public double score { get; set; }
    public string scoreColor { get; set; }
    public Runs4[] runs { get; set; }
    public AlternateRuns4[] alternateRuns { get; set; }
    public RawRuns4[] rawRuns { get; set; }
    public RawAlternateRuns4[] rawAlternateRuns { get; set; }
}

public class Runs4
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class AlternateRuns4
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class RawRuns4
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class RawAlternateRuns4
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class Spec_2
{
    public double score { get; set; }
    public string scoreColor { get; set; }
    public Runs5[] runs { get; set; }
    public AlternateRuns5[] alternateRuns { get; set; }
    public RawRuns5[] rawRuns { get; set; }
    public RawAlternateRuns5[] rawAlternateRuns { get; set; }
}

public class Runs5
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class AlternateRuns5
{
    public int zoneId { get; set; }
    public int keystoneRunId { get; set; }
    public int mythicLevel { get; set; }
    public int clearTimeMs { get; set; }
    public double score { get; set; }
    public int period { get; set; }
    public int[] affixes { get; set; }
}

public class RawRuns5
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class RawAlternateRuns5
{
    public int id { get; set; }
    public int members_checksum { get; set; }
    public int region_id { get; set; }
    public int realm_id { get; set; }
    public int keystone_team_id { get; set; }
    public object keystone_platoon_id { get; set; }
    public int zone_id { get; set; }
    public string faction { get; set; }
    public int mythic_level { get; set; }
    public int clear_time_ms { get; set; }
    public int par_time_ms { get; set; }
    public int period { get; set; }
    public string completed_at { get; set; }
    public int affix_0_id { get; set; }
    public int affix_1_id { get; set; }
    public int affix_2_id { get; set; }
    public int affix_3_id { get; set; }
    public int? logged_run_id { get; set; }
    public int num_members { get; set; }
    public int group_comp_bitmask { get; set; }
    public int item_level_min { get; set; }
    public int item_level_max { get; set; }
    public double item_level_mean { get; set; }
    public double bnet_mythic_rating { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public object deleted_at { get; set; }
    public int keystoneTeamId { get; set; }
    public object keystonePlatoonId { get; set; }
}

public class Spec_3
{
    public int score { get; set; }
    public string scoreColor { get; set; }
    public object[] runs { get; set; }
    public object[] alternateRuns { get; set; }
    public object[] rawRuns { get; set; }
    public object[] rawAlternateRuns { get; set; }
}

public class BestMythicPlusScore
{
    public double score { get; set; }
    public string scoreColor { get; set; }
    public Season season { get; set; }
}

public class Season
{
    public string slug { get; set; }
    public string name { get; set; }
}

public class KeystoneAggregateStats
{
    public int level { get; set; }
    public int count { get; set; }
}

public class MythicPlusRanks
{
    public Overall overall { get; set; }
    public Faction_overall faction_overall { get; set; }
    public Faction_class faction_class { get; set; }
    public Healer1 healer { get; set; }
    public Class_healer class_healer { get; set; }
    public Faction_healer faction_healer { get; set; }
    public Faction_class_healer faction_class_healer { get; set; }
    public Dps1 dps { get; set; }
    public Class_dps class_dps { get; set; }
    public Faction_dps faction_dps { get; set; }
    public Faction_class_dps faction_class_dps { get; set; }
    public Spec_256 spec_256 { get; set; }
    public Faction_spec_256 faction_spec_256 { get; set; }
    public Spec_257 spec_257 { get; set; }
    public Faction_spec_257 faction_spec_257 { get; set; }
    public Spec_258 spec_258 { get; set; }
    public Faction_spec_258 faction_spec_258 { get; set; }
}

public class Overall
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Class1
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_overall
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_class
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Healer1
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Class_healer
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_healer
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_class_healer
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Dps1
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Class_dps
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_dps
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_class_dps
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Spec_256
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_spec_256
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Spec_257
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_spec_257
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Spec_258
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Faction_spec_258
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class ExpansionData
{
    public int expansionId { get; set; }
}

