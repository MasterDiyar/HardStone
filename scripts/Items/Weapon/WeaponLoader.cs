using Godot;
using Godot.Collections;

namespace hardenedStone.scripts.Items.Weapon;

public static class WeaponLoader
{
    public static Dictionary<int, PackedScene> GetWeaponById = new Dictionary<int, PackedScene>()
    {
        {0, GD.Load<PackedScene>("")}
    };
}