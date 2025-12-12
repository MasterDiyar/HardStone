using Godot;
using hardenedStone.scripts.entities.bullets;

namespace hardenedStone.scripts.Items.Weapon.WeaponDetails;

public static class Spawn
{
    public static Bullet CreateBullet(PackedScene bulletScene, BulletContainer container, float angle)
    {
        var bullet = bulletScene.Instantiate<Bullet>();
        bullet.Rotation = angle;
        bullet.ParseContainer(container);
        return bullet;
    }

    public static void SingleShot(PackedScene bulletScene,BulletContainer container, float angle, Node2D area)
    {
        var bullet = CreateBullet(bulletScene, container, angle);
        area.AddChild(bullet);
    }

    public static void MultiShot(PackedScene bulletScene,BulletContainer container, float angle, Node2D area, int count, float distance)
    {
        for (int i = 0; i < count; i++)
        {
            SingleShot(bulletScene, container, angle+i*distance, area);
        }
    }
}