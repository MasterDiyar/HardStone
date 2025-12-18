using System;
using System.Linq;
using Godot;
using hardenedStone.scripts.entities.bullets.bulletUpgrades;
using hardenedStone.scripts.Modifiers;

namespace hardenedStone.scripts.entities.bullets;

public partial class Bullet : Area2D ,IBullet
{
    public Action OnSplit;
    public SplitMaster SplitMaster;
    public Modifier[] Modifiers { get; set; } = [];
    public Timer timer;

    public float Damage = 10;
    bool isSplit = false;
    int splitCount = 0;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimeOut;
        BodyEntered += body => OnHit(body, Damage, Modifiers);
    }

    protected virtual void OnTimeOut()
    {
        QueueFree();
        if (isSplit)OnSplit?.Invoke();
    }

    public virtual void OnHit(Node2D area,float damage, Modifier[] mods)
    {
        if (area is Entity entity) {
            entity.TakeDamage(damage, mods.ToList());
        }
        if (isSplit)OnSplit?.Invoke();
    }

    public void ParseContainer(BulletContainer container)
    {
        Damage = Damage + container.BaseDamage;
        var flType  = container.FlyType.Split(':');
        FlyType flyNode; 
        
        switch (flType[1])
        {
            case "1":
                flyNode = GD
                    .Load<PackedScene>("res://scenes/entity/bullet/bulletUpgrade/straight_fly.tscn")
                    .Instantiate<StraightFly>();
                AddChild(flyNode);
            break;
            default:
                flyNode = null;
                break;
        }

        if (flyNode != null) {
            switch (flType[0])
            {
                case "basic":
                    flyNode.Accel = AccelType.Basic;
                    break;
            }

            flyNode.Speed = container.Speed;
            
        }

        if (container.SplitCount > 0)
        {
            isSplit = true;
            splitCount = container.SplitCount;
            SplitMaster = new SplitMaster(this, container.SplitPath, container.SplitCount,
                (int)container.Other[2]);
        }
        
        
    }
}