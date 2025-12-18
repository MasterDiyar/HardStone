using Godot;
using System;
using hardenedStone.scripts.entities.bullets;

public partial class Swing : Bullet
{
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		throw new NotImplementedException();
	}

	public override void _Process(double delta)
	{
	}
	
	
}
