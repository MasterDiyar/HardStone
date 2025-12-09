using Godot;
using System;
using hardenedStone.scripts.Items;

public partial class Testmap : Node2D
{
	private ItemDatabase ItemDB;
	public override void _Ready()
	{
		ItemDB = GetNode<ItemDatabase>("/root/ItemDB");
		Test();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void Test()
	{
		var pots = GD.Load<PackedScene>("res://scenes/items/throwed_item.tscn").Instantiate<ThrowedItem>();
		pots.PreLoad(ItemDB.GetItemById("poplar_log"));
		pots.Position = Vector2.One * 128;
		AddChild(pots);
		pots = GD.Load<PackedScene>("res://scenes/items/throwed_item.tscn").Instantiate<ThrowedItem>();
		pots.PreLoad(ItemDB.GetItemById("poplar_log"));
		pots.Position = Vector2.One * 256;
		AddChild(pots);
	}
}
