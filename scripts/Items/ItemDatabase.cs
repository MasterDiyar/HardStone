using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;
using hardenedStone.scripts.entities.consumables;

namespace hardenedStone.scripts.Items;

public partial class ItemDatabase : Node
{
    public Dictionary<string, ItemContainer> ItemContainers = new Dictionary<string, ItemContainer>();
    public Dictionary<string, ConsumeContainer> ConsumeContainers = new Dictionary<string, ConsumeContainer>();
    
    
    ItemUnpackager Unzip = new ItemUnpackager();
    ConsumeLoader Loader = new ConsumeLoader();

    public override void _Ready()
    {
        LoadItems("res://scripts/Items/items.json");
        LoadConsumables("res://scripts/Entities/consumables/consume.json");
    }

    public void LoadItems(string path)
    {
        if (!FileAccess.FileExists(path)) {
           GD.PrintErr("ItemDatabase: File not found: " + path);
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
            GD.Print("ItemDatabase: Loading from items.json");
            var itemList = JsonSerializer.Deserialize<List<ItemContainer>>(jText, option);
            
            foreach (var item in itemList.Where(item => !ItemContainers.TryAdd(item.Id, item)))
                GD.Print($"duplicate item {item.Id}");
            GD.Print(ItemContainers.Count, " items loaded");
        }catch (Exception e) {
            GD.PrintErr(e.Message);
        }
    }

    public void LoadConsumables(string path)
    {
        if (!FileAccess.FileExists(path)) {
            GD.PrintErr("ItemDatabase: File not found: " + path);
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
            GD.Print("ItemDatabase: Loading from consumables.json");
            var consumableList = JsonSerializer.Deserialize<List<ConsumeContainer>>(jText, option);
            foreach (var item in consumableList.Where(item => !ConsumeContainers.TryAdd(item.Id, item)))
                GD.Print($"duplicate consumable {item.Id}");
            GD.Print(ConsumeContainers.Count, " consumables loaded");
        } catch (Exception e) { GD.PrintErr(e.Message); }
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