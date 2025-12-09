using Godot;
using hardenedStone.scripts.Items;

namespace hardenedStone.scripts.entities.consumables;

public partial class Consumable : StaticBody2D
{
    private Sprite2D texture;
    public float Hp;
    public string[] throwingItems;
    public int[] maxCount;
    public float[] chance;

    public void PostLoad(ConsumeContainer consumable)
    {
        
    }

    private void OnDie(float multip)
    {
        PackedScene item = GD.Load<PackedScene>("res://scenes/items/throwed_item.tscn");

        for (int i = 0; i < throwingItems.Length; i++) {
            var throwable = item.Instantiate<ThrowedItem>();
            throwable.GlobalPosition = GlobalPosition;
            throwable.PreLoad(GetNode<ItemDatabase>("/root/ItemDB").GetItemById(throwingItems[i]));
            if (GD.Randf() < chance[i])
                throwable.ThrowedItemSprite.Count = GD.RandRange(0, maxCount[i]);
            GetTree().Root.AddChild(throwable);
        }
    }
    
    
}