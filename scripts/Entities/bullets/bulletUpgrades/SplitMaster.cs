using hardenedStone.scripts.Items;

namespace hardenedStone.scripts.entities.bullets.bulletUpgrades;
using Godot;
public class SplitMaster
{
    private PackedScene SplitBulletScene;
    private int _type, _count;
    private Bullet parent;
    private string[] bulletIds = ["smallK"];
    private ItemDatabase Idb;
    private float sA;
    public SplitMaster(Bullet mother, string path, int count, int type, float splitAngle=Mathf.Pi/4f)
    {
        parent = mother;
        parent.GetNode<Timer>("Timer").Timeout += OnTimeout;
        SplitBulletScene = GD.Load<PackedScene>(path);
        _type = type;
        _count = count;
        Idb =mother.GetNode<ItemDatabase>("/root/ItemDB");
        sA = splitAngle;
    }

    void OnTimeout()
    {
        for (int i = 0; i < _count; i++)
        {
            var bullet = SplitBulletScene.Instantiate<Bullet>();
            bullet.ParseContainer(Idb.BulletContainers[bulletIds[_type]]);
            if (_type == 1)
            {
                bullet.Rotation = i * sA;
            }
            
            parent.GetParent().AddChild(bullet);
        }
    }
}