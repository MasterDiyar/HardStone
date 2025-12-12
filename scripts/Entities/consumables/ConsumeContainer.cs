using System.Text.Json.Serialization;
using hardenedStone.scripts.Items;

namespace hardenedStone.scripts.entities.consumables;

public class ConsumeContainer : IContainer
{
    [JsonPropertyName("id")]      public string   Id        { get; set; }
    [JsonPropertyName("texture")] public string   Texture   { get; set; }
    [JsonPropertyName("name")]    public string   Name      { get; set; }
    [JsonPropertyName("type")]    public string   Type      { get; set; }
    [JsonPropertyName("drop")]    public string[] Throwable { get; set; }
    [JsonPropertyName("count")]   public int[]    Counts    { get; set; }
    [JsonPropertyName("chance")]  public float[]  Chance    { get; set; }
    [JsonPropertyName("hp")]      public float    Hp        { get; set; }
}