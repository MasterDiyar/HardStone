using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;

namespace hardenedStone.scripts.Items;

public partial class ItemDatabase : Node
{
    Dictionary<string, ItemContainer> ItemContainers = new Dictionary<string, ItemContainer>();

    public override void _Ready()
    {
        LoadItems("res://scripts/Items/items.json");
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
            var itemList = JsonSerializer.Deserialize<List<ItemContainer>>(jText, option);

            foreach (var item in itemList.Where(item => !ItemContainers.TryAdd(item.Id, item)))
                GD.Print($"duplicate item {item.Id}");
        }catch (Exception e) {
            GD.PrintErr(e.Message);
        }
    }
}