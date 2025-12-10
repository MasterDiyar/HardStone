using Godot;
using hardenedStone.scripts.Items;
using hardenedStone.scripts.Modifiers;

namespace hardenedStone.scripts.entities.consumables;

public partial class Consumable : StaticBody2D
{
    [Export] private Sprite2D texture;
    [Export] private string id = "nil";
    public float Hp;
    public string[] throwingItems;
    public int[] maxCount;
    public float[] chance;
    private ItemDatabase IDB;

    public override void _Ready()
    {
        IDB = GetNode<ItemDatabase>("/root/ItemDB");
        if (id == "nil") return;
        if (IDB.ConsumeContainers.TryGetValue(id, out var cCr))
            PostLoad(cCr);
    }

    public virtual void PostLoad(ConsumeContainer consumable)
    {
        Name = consumable.Name;
        throwingItems = consumable.Throwable;
        maxCount = consumable.Counts;
        chance = consumable.Chance;
        Hp = consumable.Hp;
        texture.Texture = GD.Load<Texture2D>(consumable.Texture+".png");
    }

    public void GetDamage(float damage, Modifier[] mods = null)
    {
        Hp -= damage;
        if (Hp < 0) OnDie(1);
    }

    private void OnDie(float multip)
    {
        PackedScene item = GD.Load<PackedScene>("res://scenes/items/throwed_item.tscn");

        for (int i = 0; i < throwingItems.Length; i++) {
            var throwable = item.Instantiate<ThrowedItem>();
            throwable.GlobalPosition = GlobalPosition;
            throwable.PreLoad(IDB.GetItemById(throwingItems[i]));
            if (GD.Randf() < chance[i])
                throwable.ThrowedItemSprite.Count = GD.RandRange(0, maxCount[i]);
            GetTree().Root.AddChild(throwable);
        }
        QueueFree();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("q"))
            GetDamage(100);
    }
}