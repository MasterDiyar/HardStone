using Godot;
using System;
using hardenedStone.scripts.entities;
using hardenedStone.scripts.entities.bullets;
using hardenedStone.scripts.Items;
using hardenedStone.scripts.Items.Weapon.WeaponDetails;
using hardenedStone.scripts.Modifiers;

public partial class Weapon : Sprite2D
{
	protected Item[] Consumers;
	protected PackedScene[] bullets;
	protected Modifier Modifiers;
	protected Entity Master;
	public override void _Ready()
	{
		Master = GetParent<Entity>();
		Master.OnAttack += OnAttack;
	}

	protected virtual void OnAttack()
	{
		
	}

	public override void _Process(double delta)
	{
	}
	
	
}
