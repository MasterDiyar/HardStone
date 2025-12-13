namespace hardenedStone.scripts.Modifiers;

public interface IModifier
{
    string Name { get; set; }
    float DamageModifier { get; set; }
    float RawDamage { get; set; }
}