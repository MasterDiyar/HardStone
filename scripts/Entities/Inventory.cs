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

    public Action OnSuitChange, OnItemAdded, OnItemRemoved;

    private ItemDatabase ItemDB;

    public override void _Ready()
    {
        ItemDB = GetNode<ItemDatabase>("/root/ItemDB");
        
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

    public bool AddItem(Item item)
    {
        item = item.Clone();
        // 1. stack in hotbar
        if (TryStack(item, HotBar)) {
            OnItemAdded?.Invoke();
            return true;
        }
        // 2. stack in backpack
        if (TryStack(item, Backpack)) {
            OnItemAdded?.Invoke();
            return true;
        }
        // 3. empty slot in hotbar
        if (TryPlace(item, HotBar)) {
            OnItemAdded?.Invoke();
            return true;
        }
        // 4. empty slot in backpack
        if (TryPlace(item, Backpack)) {
            OnItemAdded?.Invoke();
            return true;
        }

        return false;
    }

    private bool TryStack(Item item, Item[] array)
    {
        for (int i = 0; i < array.Length; i++) {
            if (array[i] == null) continue;
            if (array[i].ID != item.ID) continue;
            int space = array[i].MaxCount - array[i].Count;
            if (space <= 0) continue;
            
            int toAdd = Math.Min(space, item.Count);

            array[i].Count += toAdd;
            item.Count -= toAdd;

            if (item.Count <= 0)
                return true;
        }
        return false;
    }

    private bool TryPlace(Item item, Item[] array)
    {
        for (int i = 0; i < array.Length; i++) {
            if (array[i] == null) {
                array[i] = item;
                return true;
            }
        }

        return false;
    }

    
    public float ParseDamage(float damage, Modifier modifier)
    {
        if (mergedSuit.Immunity.Contains(modifier.Name))
            return 0;
        return  (mergedSuit.HalfImmunity.Contains(modifier.Name)) ? damage/2 : damage;
    }
    
    
}