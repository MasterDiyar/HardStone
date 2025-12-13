using System.Collections.Generic;

namespace hardenedStone.scripts.Modifiers;

public static class Modifiers
{
    public static MultiHurtModifier GetPoison(float damage, float amplifier, float duration, int ticks)
    {
        var modifier = GetWithName<MultiHurtModifier>("Poison", damage, amplifier);
        modifier.Ticks = ticks;
        modifier.Interval = duration;
        return modifier;
    }

    public static T GetWithName<T>(string modifierName, float damage, float amplifier)
    where T : IModifier, new()
    {
        var modifier = new T
        {
            Name = modifierName,
            RawDamage = damage,
            DamageModifier = amplifier
        };
        return modifier;
    }

    private static Dictionary<string, Modifier> ModifierDict = new Dictionary<string, Modifier>()
    {
        { "poison1", GetPoison(6, 2, 3, 3) },
        { "wooden", GetWithName<MultiHurtModifier>("Wooden", 3, 2)},
        { "cold_weapon",GetWithName<MultiHurtModifier>("ColdWeapon", 5, 2)},
        { "sharp", GetWithName<MultiHurtModifier>("Sharp", 12, 4)} 
    };

    public static Modifier GetFromName(string modifierName)
    {
        return ModifierDict[modifierName.ToLower()];
    }
}