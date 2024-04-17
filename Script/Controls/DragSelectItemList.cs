using Godot;

namespace RimDefGodot
{
  [GlobalClass]
  public partial class DragSelectItemList : ItemList
  {
    private bool IsMouseOver = false;
    private bool IsMouseDown = false;
    private int index = 0;
    private int lastIndex = 0;

    public override void _EnterTree()
    {
      // connect mouse signals
      MouseEntered += _OnItemListMouseEntered;
      MouseExited += _OnItemListMouseExited;
    }

    public override void _ExitTree()
    {
      MouseEntered -= _OnItemListMouseEntered;
      MouseExited -= _OnItemListMouseExited;
    }

    private void _OnItemListMouseEntered()
    {
      IsMouseOver = true;
      lastIndex = -1;
      SetProcess(IsMouseOver);
    }

    private void _OnItemListMouseExited()
    {
      IsMouseOver = false;
      lastIndex = -1;
      SetProcess(IsMouseOver);
    }

    // every frame
    public override void _Process(double delta)
    {
      if (!IsMouseDown)
        return;

      index = GetItemAtPosition(GetLocalMousePosition());

      if (index == lastIndex)
        return;

      // the mouse button is held down and the index is different, so we can swap the value
      if (IsSelected(index))
        Deselect(index);
      else
        Select(index, single: false);

      lastIndex = index;
    }

    public override void _GuiInput(InputEvent @event)
    {
      if (@event is InputEventMouseButton mb)
      {
        if (mb.ButtonIndex == MouseButton.Left)
        {
          IsMouseDown = mb.Pressed;
          if (index == lastIndex)
            lastIndex = -1;
          mb.Canceled = true;
        }
      }
    }
  }
}
