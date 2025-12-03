using Godot;
using hardenedStone.scripts.Items.Suit.ChestPlate;
using hardenedStone.scripts.Items.Suit.Helmet;
using hardenedStone.scripts.Items.Suit.Pants;

namespace hardenedStone.scripts.Items;

public class ItemUnpackager
{
    public Item GetItem(ItemContainer container)
    {
        Item item = container.Type switch
        {
            "chestplate" =>
                GetChestPlate(container),
            "helmet" =>
                GetHelmet(container),
            "pants" =>
                GetPants(container),
            _ =>
                GetDefaultItem(container)
        };
        item.Name = container.Name;
        item.Texture = GD.Load<Texture2D>("res://"+container.TexturePath);
        item.MaxCount = container.MaxCount;
        
        return item;
    }

    private Item GetDefaultItem(ItemContainer container) =>
        GD.Load<PackedScene>("res://scenes/items/item.tscn").Instantiate<Item>();
    
    public static ChestPlate GetChestPlate(ItemContainer container) => container.Id switch
    {
        "leather_tunic" => GD.Load<PackedScene>("res://scenes/items/suit/chestplate/tunic.tscn")
            .Instantiate<Tunic>(),
        _ => GD.Load<PackedScene>("res://scenes/items/suit/chestplate/tunic.tscn").Instantiate<Tunic>()
    };

    public static Pants GetPants(ItemContainer container) => container.Id switch
    {
        "breeches" => GD.Load<PackedScene>("res://scenes/items/suit/pants/breeches.tscn").Instantiate<Breeches>(),
        _ => GD.Load<PackedScene>("res://scenes/items/suit/pants/breeches.tscn").Instantiate<Pants>(),
    };

    public static Helmet GetHelmet(ItemContainer container) => container.Id switch
    {
        "ushanka" => GD.Load<PackedScene>("res://scenes/items/suit/helmet/ushanka.tscn").Instantiate<Ushanka>(),
        _ => GD.Load<PackedScene>("res://scenes/items/suit/helmet/ushanka.tscn").Instantiate<Helmet>(),
    };

}