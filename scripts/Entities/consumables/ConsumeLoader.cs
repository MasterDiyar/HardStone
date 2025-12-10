using Godot;

namespace hardenedStone.scripts.entities.consumables;

public class ConsumeLoader
{
    public Consumable GetConsume(ConsumeContainer cC)
    {
        Consumable consumable;
        switch (cC.Type)
        {
            case "tree":
                consumable = GD.Load<PackedScene>("res://scenes/entity/tree/tree.tscn").Instantiate<Consumable>();
                break;
            case "ore":
                
            default:
                consumable = GD.Load<PackedScene>("res://scenes/entity/tree/tree.tscn").Instantiate<Consumable>();
                break;
        }
        consumable.PostLoad(cC);
        return consumable;
    }
}