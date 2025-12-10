namespace hardenedStone.scripts.entities.consumables;

using Godot;
public partial class Tree : Consumable
{
    [Export] Sprite2D StemSprite;

    public override void PostLoad(ConsumeContainer consumable)
    {
        base.PostLoad(consumable);
        StemSprite.Texture = GD.Load<Texture2D>(consumable.Texture+"stem.png");
    }
}