using Godot;
using hardenedStone.scripts.Modifiers;

namespace hardenedStone.scripts.entities.bullets;

public interface IBullet
{
    Modifier[] Modifiers {get;set;}
    void OnHit(Node2D area2D,float damage, Modifier[] mods);
}