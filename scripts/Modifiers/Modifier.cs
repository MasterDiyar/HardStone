using Godot;
using hardenedStone.scripts.entities;

namespace hardenedStone.scripts.Modifiers;

public abstract class Modifier : IModifier
{
    public string Name { get; set; }
    public float DamageModifier { get; set; }
    public float RawDamage { get; set; }
    public float Interval = 1f;
    public int Ticks;

    public abstract void Apply(Entity entity);
    
}