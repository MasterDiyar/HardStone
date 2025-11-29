using System.Text.Json.Serialization;

namespace hardenedStone.scripts.Items;

public class ItemContainer
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("texture")] public string TexturePath { get; set; }
    [JsonPropertyName("max_count")] public int MaxCount { get; set; } = 1;
    [JsonPropertyName("description")] public string Description { get; set; }
}