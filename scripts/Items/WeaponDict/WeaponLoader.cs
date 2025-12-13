using Godot;
using Godot.Collections;

namespace hardenedStone.scripts.Items.WeaponDict;

public class WeaponLoader
{
    public Dictionary<int, PackedScene> GetWeaponById = new Dictionary<int, PackedScene>()
    {
        {0, GD.Load<PackedScene>("")}
    };

    public void LoadWeapons()
    {
        using var file = FileAccess.Open("res://scripts/Items", FileAccess.ModeFlags.Read);
        file.GetAsText();
        
    }
}