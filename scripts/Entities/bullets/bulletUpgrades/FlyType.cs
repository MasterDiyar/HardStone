using Godot;

namespace hardenedStone.scripts.entities.bullets.bulletUpgrades;

public partial class FlyType : Node2D
{
    public AccelType Accel { get; set; } = AccelType.Basic;
    public Bullet Parent;
    public Vector2 Velocity;
    public float Angle=0;
    public float Speed=0;
    public float AccelSpeed=0;
}