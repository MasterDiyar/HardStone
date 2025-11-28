using System.Linq;
using Godot;

namespace hardenedStone.scripts.Miscellaneous;

public static class Sundries
{
    public static bool AllPressed(string[] inputs) =>
        inputs.All(a => Input.IsActionPressed(a));

    public static bool AnyPressed(string[] inputs) =>
        inputs.Any(a => Input.IsActionPressed(a));
    
    public static T FindParentOfType<T>(Node node) where T : Node
    {
        if (node == null)
            return null;

        if (node is T typed)
            return typed;

        return FindParentOfType<T>(node.GetParent());
    }
}