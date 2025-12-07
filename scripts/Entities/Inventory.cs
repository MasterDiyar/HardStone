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

    private ItemDatabase ItemDB;

    public override void _Ready()
    {
        ItemDB = GetNode<ItemDatabase>("/root/ItemDB");
        
        Backpack[5] = ItemDB.GetItemById("leather_tunic");
        Backpack[1] = ItemDB.GetItemById("ushanka");
        Backpack[0] = ItemDB.GetItemById("ushanka");
        Backpack[2] = ItemDB.GetItemById("breeches");
        
        OnSuitChange += MergeSuit;
        OnSuitChange?.Invoke();
    }

    
    public void MergeSuit()
    {
        mergedSuit = new BaseSuit
        {
            Armor =
                (chest?.Armor  ?? 0) +
                (helmet?.Armor ?? 0) +
                (pants?.Armor  ?? 0),

            Immunity =
                (chest?.Immunity  ?? [])
                .Union(helmet?.Immunity ?? [])
                .Union(pants?.Immunity  ?? [])
                .ToArray(),

            HalfImmunity =
                (chest?.HalfImmunity  ?? [])
                .Union(helmet?.HalfImmunity ?? [])
                .Union(pants?.HalfImmunity  ?? [])
                .ToArray(),
        };

    }
    
    public float ParseDamage(float damage, Modifier modifier)
    {
        if (mergedSuit.Immunity.Contains(modifier.Name))
            return 0;
        return  (mergedSuit.HalfImmunity.Contains(modifier.Name)) ? damage/2 : damage;
    }
    
    
}