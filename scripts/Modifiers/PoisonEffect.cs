using Godot;
using hardenedStone.scripts.entities;

namespace hardenedStone.scripts.Modifiers;

public partial class PoisonEffect : Node
{
    private Modifier data;
    private int ticksLeft;
    private Entity target;
    private Timer timer;

    public PoisonEffect(Modifier modifier)
    {
        data = modifier;
        ticksLeft = modifier.Ticks;
    }

    public override void _Ready()
    {
        target = GetParent<Entity>();

        timer = new Timer
        {
            WaitTime = data.Interval,
            OneShot = false
        };

        AddChild(timer);
        timer.Timeout += OnTick;
        timer.Start();
    }

    private void OnTick()
    {
        target.TakeRawDamage(data.RawDamage*data.DamageModifier);
        ticksLeft--;

        if (ticksLeft <= 0)
            QueueFree();
    }
}