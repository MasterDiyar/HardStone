using Godot;
using System;
using hardenedStone.scripts.Items;

public partial class ThrowedItem : Area2D
{
	[Export] public Item ThrowedItemSprite;
	bool picked = false;
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}
	public void PreLoad(Item th)
	{
		ThrowedItemSprite = th;
		var Sprite = GetNode<Sprite2D>("Sprite2D");
		Sprite.Texture = ThrowedItemSprite.Texture;
	}
	private void OnAreaEntered(Area2D area)
	{
		GD.Print("OnAreaEntered");
		if (picked) return;
		if (area.GetParent() is Player player) {
			if (player.AddItem(ThrowedItemSprite.Clone())) {
				picked = true;
				QueueFree();
			}
		}
	}
}
