using Godot;
using hardenedStone.scripts.entities;

namespace hardenedStone.scripts.Modifiers;

public class MultiHurtModifier : Modifier
{
    public int times = 2;
    public override void Apply(Entity entity)
    {
        entity.AddChild(new PoisonEffect(this));
    }
}