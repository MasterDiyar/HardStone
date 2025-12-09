using Godot;

namespace hardenedStone.scripts.Items;

public partial class Item : Sprite2D
{
    public int Count = 0, MaxCount = 1;
    public string ID = "";

    public Item Clone()
    {
        var clone = new Item();

        clone.Count = this.Count;
        clone.MaxCount = this.MaxCount;
        clone.ID = this.ID;

        clone.Texture = this.Texture;

        clone.Position = this.Position;
        clone.Scale = this.Scale;
        clone.Rotation = this.Rotation;

        return clone;
    }
}