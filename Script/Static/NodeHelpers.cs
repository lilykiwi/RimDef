using Godot;

namespace RimDefGodot
{
  [Tool]
  public static class NodeHelpers
  {
    public static T AddNewChild<T>(Node parent, T child) where T : Node
    {
      parent.AddChild(child);
      child.Owner = parent;
      return child;
    }
  }
}
