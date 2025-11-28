namespace hardenedStone.scripts.Items.Suit.Helmet;

using Godot;
public partial class Ushanka : Helmet
{
    public override float Armor { get; set; } = 1; 

    public override string[] HalfImmunity { get; set; } = ["Ice", "Frost"];
}