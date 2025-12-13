using System.Text.Json.Serialization;

namespace hardenedStone.scripts.Items.WeaponDict;

public class WeaponContainer : IContainer
{
    [JsonPropertyName("id")]           public string   Id           { get; set; }
    [JsonPropertyName("attack_speed")] public float    AttackSpeed  { get; set; }
    [JsonPropertyName("damage")]       public float    Damage       { get; set; }
    [JsonPropertyName("colors")]       public float[]  Colors       { get; set; }
    [JsonPropertyName("consume")]      public string[] Consumes     { get; set; }
    [JsonPropertyName("bullets")]      public string[] BulletPath   { get; set; }
    [JsonPropertyName("modifiers")]    public string[] Modifiers    { get; set; }
    [JsonPropertyName("animations")]   public string[] Animations   { get; set; }
}