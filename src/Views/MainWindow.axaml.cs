using Avalonia.Controls;
using Avalonia.Input;

using System.Linq;
using RimDef.Helper;
using Avalonia.Interactivity;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace RimDef.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    ModsList.ItemsSource = new string[]
                {"cat", "camel", "cow", "chameleon", "mouse", "lion", "zebra" }
            .OrderBy(x => x);
    ModsList.AddHandler(PointerPressedEvent, DragSelectionHelper.ListBox_PointerPressed, RoutingStrategies.Tunnel);
    ModsList.AddHandler(PointerReleasedEvent, DragSelectionHelper.ListBox_PointerReleased, RoutingStrategies.Tunnel);


    DefTypesList.ItemsSource = new string[]
                {"cat", "camel", "cow", "chameleon", "mouse", "lion", "zebra" }
            .OrderBy(x => x);
    DefTypesList.AddHandler(PointerPressedEvent, DragSelectionHelper.ListBox_PointerPressed, RoutingStrategies.Tunnel);
    DefTypesList.AddHandler(PointerReleasedEvent, DragSelectionHelper.ListBox_PointerReleased, RoutingStrategies.Tunnel);

  }
}
