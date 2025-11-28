namespace hardenedStone.scripts.Items.Suit.Pants;

using Godot;
public partial class Breeches : Pants
{
    public override float Armor { get; set; } = 2;

    public override string[] HalfImmunity { get; set; } = ["Ice", "Frost"];
}