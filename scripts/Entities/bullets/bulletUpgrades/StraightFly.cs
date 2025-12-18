using Godot;
using System;
using hardenedStone.scripts.entities.bullets;
namespace hardenedStone.scripts.entities.bullets.bulletUpgrades;
public partial class StraightFly : FlyType, IBulletMove
{
	public override void _Ready()
	{
		Velocity = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)) * Speed;
		Parent = GetParent<Bullet>();
	}
	
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;
		switch (Accel)
		{
			case AccelType.Basic:
				velocity = Velocity*(float)delta;
				break;
			case AccelType.EaseIn:
				AccelSpeed += (float)delta+AccelSpeed/10f;
				velocity = Velocity*(float)delta*AccelSpeed;
				break;
			case AccelType.EaseOut:
				AccelSpeed -= (float)delta+AccelSpeed/10f;
				velocity = Velocity*(float)delta* ((AccelSpeed > 1e-2f) ? AccelSpeed : 1e-2f);
				break;
		}
		Parent.Position += velocity;
	}
}
