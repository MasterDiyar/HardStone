using System.Collections.Generic;
using Godot;
using hardenedStone.scripts.entities;
using hardenedStone.scripts.Miscellaneous;

namespace hardenedStone.scripts.Items.Suit;

public partial class BaseSuit : Item
{
    public virtual float Armor { get; set; } = 0;

    public virtual string[] Immunity { get; set; } = ["None"];
    
    public virtual string[] HalfImmunity { get; set; } = ["None"];

    public static BaseSuit GetSuit(BaseSuit suit, SuitContainer sContainer)
    {
        suit.Armor = sContainer.Armor;
        suit.Immunity = sContainer.Immunity;
        suit.HalfImmunity = sContainer.HalfImmunity;
        return suit;
    }
   
}