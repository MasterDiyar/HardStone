using Godot;
using System;
using System.Collections.Generic;
using hardenedStone.scripts.entities;

public partial class PlayerAnimation : AnimatedSprite2D
{
	private Player _player;

	public override void _Ready()
	{
		_player = GetParent<Player>();
		_player.OnMove += HandleMovementAnimation;
	}

	private void HandleMovementAnimation(bool isMoving, List<Entity.State> dirs)
	{
		if (dirs == null || dirs.Count == 0)
			return;

		var dir = dirs[0];
		if (dir == Entity.State.Right)
			FlipH = false;
		else if (dir == Entity.State.Left)
			FlipH = true;

		if (isMoving)
			Play(GetRunAnimationName(dir));
		else
			Play(GetIdleAnimationName(dir));
	}

	private string GetIdleAnimationName(Entity.State dir)
	{
		return dir switch
		{
			Entity.State.Up => "idle_w",
			Entity.State.Down => "idle_s",
			Entity.State.Left => "idle_l",
			Entity.State.Right => "idle_l", 
			_ => "idle_s"
		};
	}

	private string GetRunAnimationName(Entity.State dir)
	{
		return dir switch
		{
			Entity.State.Up => "run_w",
			Entity.State.Down => "run_s",
			Entity.State.Left => "run_l",
			Entity.State.Right => "run_l", 
			_ => "run_s"
		};
	}
}
