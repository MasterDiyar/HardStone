using System;
using System.Linq;
using Godot;
using hardenedStone.scripts.Items;
using hardenedStone.scripts.Items.Suit;
using hardenedStone.scripts.Items.Suit.ChestPlate;
using hardenedStone.scripts.Items.Suit.Helmet;
using hardenedStone.scripts.Items.Suit.Pants;
using hardenedStone.scripts.Modifiers;

namespace hardenedStone.scripts.entities;

public partial class Inventory : Node2D
{
    private Entity parent;

    public ChestPlate chest;
    public Helmet helmet;
    public Pants pants;
    public BaseSuit mergedSuit;

    public Item[] HotBar = new Item[5];
    public Item[] Backpack = new Item[10];

    public Action OnSuitChange;

    public override void _Ready()
    {
        chest = new Tunic();
        pants = new Breeches();
        helmet = new Ushanka();
        
        
        OnSuitChange += MergeSuit;
        OnSuitChange?.Invoke();
    }

    
    public void MergeSuit()
    {
        mergedSuit = new BaseSuit();
        
        
        mergedSuit.Armor = chest.Armor + helmet.Armor + pants.Armor;
        mergedSuit.Immunity = chest.Immunity
            .Union(helmet.Immunity)
            .Union(pants.Immunity)
            .ToArray();
        mergedSuit.HalfImmunity = chest.HalfImmunity.Union(helmet.HalfImmunity).Union(pants.HalfImmunity).ToArray();
    }
    
    public float ParseDamage(float damage, Modifier modifier)
    {
        if (mergedSuit.Immunity.Contains(modifier.Name))
            return 0;
        return  (mergedSuit.HalfImmunity.Contains(modifier.Name)) ? damage/2 : damage;
    }
    
    
}