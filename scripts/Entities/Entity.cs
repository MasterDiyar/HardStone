using System;
using System.Collections.Generic;
using Godot;
using hardenedStone.scripts.Modifiers;

namespace hardenedStone.scripts.entities;

public partial class Entity : CharacterBody2D
{
    public float MaxHp, Hp;

    public Action OnHurt, OnAttack;
    
    public Inventory Inventory;
    
    public enum State
    {
        Left,
        Right,
        Up,
        Down
    }
	
    public List<State> LastState = [];

    public Action<bool, List<State>> OnMove;

    public override void _Ready()
    {
        Hp = MaxHp;
    }

    public virtual void Attack(float angle)
    {
        OnAttack?.Invoke();
    }

    public virtual void TakeDamage(float damage, List<Modifier> modifiers)
    {
        float totalDamage=0;
        foreach (var mod in modifiers)
            totalDamage+=Inventory.ParseDamage(damage, mod);
        totalDamage += Mathf.Clamp(damage - Inventory.mergedSuit.Armor, 0, damage);
        Hp -= totalDamage;
        OnHurt?.Invoke();
    }
}