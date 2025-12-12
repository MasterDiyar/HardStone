using Godot;

namespace hardenedStone.scripts.entities.bullets;

public static class SplitSpawn
{
    public static Bullet CreateBullet(PackedScene bulletScene, BulletContainer container, float angle)
    {
        var bullet = bulletScene.Instantiate<Bullet>();
        bullet.Rotation = angle;
        bullet.ParseContainer(container);
        return bullet;
    }

    public static void SingleSplit(PackedScene bulletScene,BulletContainer container, float angle, Node2D area)
    {
        var bullet = CreateBullet(bulletScene, container, angle);
        area.AddChild(bullet);
    }
    
    public static void MultiSplit(PackedScene bulletScene,BulletContainer container, float angle, Node2D area, int count, 
        float distance, float mirror=Mathf.Pi) {
        for (int i = 0; i < count; i++)
        {
            SingleSplit(bulletScene, container, angle + i * distance, area);
            SingleSplit(bulletScene, container, angle + i * distance + mirror, area);
        }
    }
}