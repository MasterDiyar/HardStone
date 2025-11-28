using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using hardenedStone.scripts.entities;

public partial class Movement : Node2D
{
	[Export] public float Speed = 30;
	[Export] public float Acceleration = 50;
	public CpuParticles2D DashParticles;
	
	Player _parent;
	private Vector2 _velocity = Vector2.Zero,
					target    = Vector2.Zero;
	
	public override void _Ready()
	{
		DashParticles = GetNode<CpuParticles2D>("dashParticle");
		_parent = GetParent<Player>();
		_parent.OnMove += HandleMove;
	}
	
	private void HandleMove(bool isMoving, List<Entity.State> directions)
	{
		target = Vector2.Zero;
		if (isMoving)
		{
			target = directions.Aggregate(target, (current, dir) => current + dir switch
			{
				Entity.State.Up => Vector2.Up,
				Entity.State.Down => Vector2.Down,
				Entity.State.Left => Vector2.Left,
				Entity.State.Right => Vector2.Right,
				_ => Vector2.Zero
			});
		}
		target = target.Normalized();
		var trueSpeed = (Input.IsActionPressed("shift")) ? Acceleration : Speed;
		_velocity = _velocity.Lerp(target * trueSpeed, Acceleration * (float)GetProcessDeltaTime());
	}

	private double lastDash = 0, currentTime = 0;

	public override void _Process(double delta)
	{
		currentTime += delta;
		_parent.GlobalPosition += _velocity * (float)delta;
		
		if (Input.IsActionPressed("ctrl") && lastDash + 3 <= currentTime)
		{
			DashParticles.Emitting = true;
			lastDash = currentTime;
			_parent.GlobalPosition += target * Speed;
		}
	}
}
