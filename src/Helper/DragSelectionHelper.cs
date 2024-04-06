using Avalonia.Controls;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace RimDef.Helper
{

  // via https://stackoverflow.com/questions/2869566/wpf-listview-drag-select-multiple-items

  // good lord

  public class DragSelectionHelper : AvaloniaObject
  {

    public DragSelectionHelper()
    {
      IsDragSelectionEnabledProperty.Changed.AddClassHandler<Interactive>(IsDragSelectingEnabledPropertyChanged);
      IsDragSelectingProperty.Changed.AddClassHandler<Interactive>(IsDragSelectingPropertyChanged);
      IsDragClickStartedProperty.Changed.AddClassHandler<Interactive>(IsDragClickStartedPropertyChanged);
    }

    #region IsDragSelectionEnabledProperty

    public static bool GetIsDragSelectionEnabled(AvaloniaObject obj)
    {
      return obj.GetValue(IsDragSelectionEnabledProperty);
    }

    public static void SetIsDragSelectionEnabled(AvaloniaObject obj, bool value)
    {
      obj.SetValue(IsDragSelectionEnabledProperty, value);
    }


    public static readonly AttachedProperty<bool> IsDragSelectionEnabledProperty =
        AvaloniaProperty.RegisterAttached<DragSelectionHelper, bool>(nameof(IsDragSelectingEnabled), typeof(AvaloniaPropertyChangedEventArgs));


    public bool IsDragSelectingEnabled
    {
      get => GetValue(IsDragSelectionEnabledProperty);
      set => SetValue(IsDragSelectionEnabledProperty, value);
    }

    private static void IsDragSelectingEnabledPropertyChanged(AvaloniaObject o,
        AvaloniaPropertyChangedEventArgs e)
    {
      var listBox = o as ListBox;

      if (listBox == null)
        return;

      // if DragSelection is enabled
      if (GetIsDragSelectionEnabled(listBox))
      {
        // set the listbox's selection mode to multiple ( didn't work with extended )
        listBox.SelectionMode = SelectionMode.Multiple;
      }
      else // is selection is disabled
      {
        // set selection mode to the default
        listBox.SelectionMode = SelectionMode.Multiple;
      }
    }

    private static void listBox_PreviewKeyDown(object? sender, KeyEventArgs e)
    {
      var listBox = sender as ListBox;
      if (listBox == null)
        return;

      if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
      {
        SetIsDragSelectionEnabled(listBox, false);
      }
    }

    private static void ListBox_PreviewKeyUp(object? sender, KeyEventArgs e)
    {
      var listBox = sender as ListBox;
      if (listBox == null)
        return;

      if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
      {
        SetIsDragSelectionEnabled(listBox, true);
      }
    }

    private static void ListBox_PreviewMouseRightButtonDown(object? sender, PointerPressedEventArgs e)
    {
      // to prevent the listbox from selecting / deselecting wells on right click
      e.Handled = true;
    }

    public static void ListBox_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
      var point = e.GetCurrentPoint(sender as Control);
      if (point.Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
        SetIsDragClickStarted(sender as AvaloniaObject, true);
      e.Handled = true;
    }

    public static void ListBox_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
      var point = e.GetCurrentPoint(sender as Control);
      if (point.Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonReleased)
        SetIsDragClickStarted(sender as AvaloniaObject, false);
      e.Handled = true;
    }

    //public static AvaloniaObject GetParent(AvaloniaObject obj)
    //{
    //  if (obj == null)
    //    return null;
    //
    //  var ce = obj as StyledElement;
    //  if (ce == null) return VisualTreeHelper.GetParent(obj);
    //
    //  var parent = ContentOperations.GetParent(ce);
    //  if (parent != null)
    //    return parent;
    //
    //  var fce = ce as FrameworkContentElement;
    //  return fce != null ? fce.Parent : null;
    //}

    #endregion IsDragSelectionEnabledProperty

    #region IsDragSelectingProperty

    public static bool GetIsDragSelecting(AvaloniaObject obj)
    {
      return obj.GetValue(IsDragSelectingProperty);
    }

    public static void SetIsDragSelecting(AvaloniaObject obj, bool value)
    {
      obj.SetValue(IsDragSelectingProperty, value);
    }

    public static readonly StyledProperty<bool> IsDragSelectingProperty =
        AvaloniaProperty.Register<DragSelectionHelper, bool>(nameof(IsDragSelecting), false);

    public bool IsDragSelecting
    {
      get => GetValue(IsDragSelectingProperty);
      set => SetValue(IsDragSelectingProperty, value);
    }

    private static void IsDragSelectingPropertyChanged(AvaloniaObject o, AvaloniaPropertyChangedEventArgs e)
    {
      var listBoxItem = o as ListBoxItem;

      if (listBoxItem == null)
        return;

      if (!GetIsDragClickStarted(listBoxItem)) return;

      if (GetIsDragSelecting(listBoxItem))
      {
        listBoxItem.IsSelected = true;
      }
    }

    #endregion IsDragSelectingProperty

    #region IsDragClickStartedProperty



    public static bool GetIsDragClickStarted(AvaloniaObject obj)
    {
      return (bool)obj.GetValue(IsDragClickStartedProperty);
    }

    public static void SetIsDragClickStarted(AvaloniaObject obj, bool value)
    {
      obj.SetValue(IsDragClickStartedProperty, value);
    }

    public static readonly StyledProperty<bool> IsDragClickStartedProperty =
        AvaloniaProperty.Register<DragSelectionHelper, bool>(nameof(IsDragClickStarted), false);

    public bool IsDragClickStarted
    {
      get => GetValue(IsDragClickStartedProperty);
      set => SetValue(IsDragClickStartedProperty, value);
    }

    private static void IsDragClickStartedPropertyChanged(AvaloniaObject obj, AvaloniaPropertyChangedEventArgs e)
    {
      var listBox = obj as ListBox;

      if (listBox == null)
        return;

      //if (KeyboardDevice.IsKeyDown(Key.LeftCtrl) || Key.IsKeyDown(Key.RightCtrl))
      //  return;

      //var hitTestResult = VisualTreeHelper.HitTest(listBox, Mouse.GetPosition(listBox));
      //if (hitTestResult == null)
      //  return;

      //var element = hitTestResult.VisualHit;
      //while (element != null)
      //{
      //  var scrollBar = element as ScrollBar;
      //  if (scrollBar != null)
      //  {
      //    return;
      //  }
      //  element = VisualTreeHelper.GetParent(element);
      //}

      if (GetIsDragClickStarted(listBox))
        listBox.SelectedItems.Clear();
    }

    #endregion IsDragClickInitiatedProperty
  }
}
