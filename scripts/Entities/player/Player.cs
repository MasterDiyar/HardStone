using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using hardenedStone.scripts.entities;
using hardenedStone.scripts.Items;
using hardenedStone.scripts.Miscellaneous;

public partial class Player : Entity
{
	public override void _Ready()
	{
		Inventory = GetNode<Inventory>("inventory");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("lm"))
			Attack((GetGlobalMousePosition() - GlobalPosition).Angle());
		
		bool moving = Sundries.AnyPressed(["w", "a", "s", "d"]);
		if (moving)
			Movement();
		else
			OnMove?.Invoke(false, LastState);
	}

	void Movement()
	{
		LastState = [];
		if (Input.IsActionPressed("w"))
			LastState.Add(State.Up);
		if (Input.IsActionPressed("d"))
			LastState.Add(State.Right);
		if (Input.IsActionPressed("s"))
			LastState.Add(State.Down);
		if (Input.IsActionPressed("a"))
			LastState.Add(State.Left);
		
		OnMove?.Invoke(true, LastState);
	}

	public bool AddItem(Item item) =>
		Inventory.AddItem(item);

}
