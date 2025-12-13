using System.Text.Json.Serialization;

namespace hardenedStone.scripts.Items.Suit;

public class SuitContainer : IContainer
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("armor")] public float Armor { get; set; }
    [JsonPropertyName("immunity")] public string[] Immunity { get; set; }
    [JsonPropertyName("half_immunity")] public string[] HalfImmunity { get; set; }
}