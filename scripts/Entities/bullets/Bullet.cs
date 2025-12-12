using Godot;
using hardenedStone.scripts.Modifiers;

namespace hardenedStone.scripts.entities.bullets;

public partial class Bullet : Area2D ,IBullet
{
    public Modifier[] Modifiers { get; set; } = [];

    public float Damage = 10;

    public override void _Ready()
    {
        BodyEntered += body => OnHit(body, Damage, Modifiers);
    }

    public virtual void OnHit(Node2D area,float damage, Modifier[] mods)
    {
        
    }

    public void ParseContainer(BulletContainer container)
    {
        
    }
}