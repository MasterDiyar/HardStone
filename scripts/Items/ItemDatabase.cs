using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;
using hardenedStone.scripts.entities.bullets;
using hardenedStone.scripts.entities.consumables;

namespace hardenedStone.scripts.Items;

public partial class ItemDatabase : Node
{
    public Dictionary<string, ItemContainer> ItemContainers = new Dictionary<string, ItemContainer>();
    public Dictionary<string, ConsumeContainer> ConsumeContainers = new Dictionary<string, ConsumeContainer>();
    public Dictionary<string, BulletContainer> BulletContainers = new Dictionary<string, BulletContainer>();
    public 
    
    ItemUnpackager Unzip = new ItemUnpackager();
    ConsumeLoader Loader = new ConsumeLoader();

    public override void _Ready()
    {
        LoadJsonList("res://scripts/Items/items.json", ItemContainers, "Items");
        LoadJsonList("res://scripts/Entities/consumables/consume.json", ConsumeContainers, "Consume");
        LoadJsonList("res://scripts/Entities/bullets/bullet.json", BulletContainers, "Bullet");
        
    }

    private void LoadJsonList<TContainer>(string path,
        Dictionary<string, TContainer> targetDictionary,
        string debugKey = "{not given}")
        where TContainer : IContainer
    {
        if (!FileAccess.FileExists(path)) {
           GD.PrintErr(debugKey+": File not found: " + path);
           return;
        }

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        string jText = file.GetAsText();

        var option = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        try {
            GD.Print(debugKey+": Loading from items.json");
            var list = JsonSerializer.Deserialize<List<TContainer>>(jText, option);
            
            foreach (var item in list.Where(i => !targetDictionary.TryAdd(i.Id, i)))
                GD.Print($"{debugKey}: duplicate item {item.Id}");        
            GD.Print($"{targetDictionary.Count} {debugKey.ToLower()} loaded");
        }catch (Exception e) {
            GD.PrintErr($"{debugKey}: {e.Message}");
        }
    }

    public Item GetItemById(string id)
    {
        if (ItemContainers.TryGetValue(id, out ItemContainer container))
            return Unzip.GetItem(container);
        
        throw new KeyNotFoundException($"Item with id {id} not found");
    }

    public Consumable GetConsumableById(string id)
    {
        if (ConsumeContainers.TryGetValue(id, out var value))
            return Loader.GetConsume(value);
                
        throw new KeyNotFoundException($"Consumable with id {id} not found");
    }
}