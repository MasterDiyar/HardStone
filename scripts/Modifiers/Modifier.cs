using Godot;
using hardenedStone.scripts.entities;

namespace hardenedStone.scripts.Modifiers;

public abstract class Modifier
{
    public string Name;

    public float DamageModifier;
    public float RawDamage;

    public virtual void Modify(Entity area){}
}