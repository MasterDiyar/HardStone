using System.Collections.Generic;
using Godot;

namespace hardenedStone.scripts.Items.Suit.ChestPlate;

public partial class Tunic : ChestPlate
{
    public override float Armor { get; set; } = 2;

    public override string[] HalfImmunity { get; set; } = ["Ice", "Frost"];
}