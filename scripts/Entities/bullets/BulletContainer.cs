using System.Text.Json.Serialization;
using hardenedStone.scripts.Items;

namespace hardenedStone.scripts.entities.bullets;

public class BulletContainer : IContainer
{
    [JsonPropertyName("id")]          public string  Id        {get; set;}
    [JsonPropertyName("speed")]       public float   Speed      {get; set;}
    [JsonPropertyName("base_damage")] public float   BaseDamage {get; set;}
    [JsonPropertyName("splitting")]   public int     SplitCount {get; set;}
    [JsonPropertyName("splitPath")]   public string  SplitPath  {get; set;}
    [JsonPropertyName("fly_type")]    public string  FlyType    {get; set;}
    [JsonPropertyName("other")]       public float[] Other      {get; set;}
}