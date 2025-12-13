using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using hardenedStone.scripts.entities;
using hardenedStone.scripts.entities.bullets;
using hardenedStone.scripts.Items;
using hardenedStone.scripts.Items.Weapon.WeaponDetails;
using hardenedStone.scripts.Modifiers;

namespace hardenedStone.scripts.Items.WeaponDict;
public partial class Weapon : Item
{
	protected Item[] Consumers;
	protected PackedScene[] bullets;
	protected Modifier[] Modifiers;
	protected Entity Master;
	
	protected float Damage;
	protected float AttackSpeed;
	protected Color[] Colors;
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

	private static Weapon ItemConverter(Item item) => new() {
			ID = item.ID,
			Texture = item.Texture,
			Name = item.Name,
			MaxCount = item.MaxCount,
			Count = item.Count
		};
	

	public static Weapon CreateWeapon(Item item, WeaponContainer container)
	{
		Weapon weapon = ItemConverter(item);
		weapon.AttackSpeed = container.AttackSpeed;
		weapon.Colors = [new Color(container.Colors[0], container.Colors[1], container.Colors[2]),
		                 new Color(container.Colors[3], container.Colors[4], container.Colors[5]),];
		weapon.Damage = container.Damage;
		weapon.Modifiers = container.Modifiers
			.Select(scripts.Modifiers.Modifiers.GetFromName)
			.ToArray();

		return weapon;
	}
}
