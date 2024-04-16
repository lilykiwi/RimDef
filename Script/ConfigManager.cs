using Godot;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RimDefGodot
{

  [GlobalClass]
  public partial class ConfigManager : MarginContainer
  {

    // NOTE: this might be better off with a wrapper class to handle some simple functions, i.e. tracking path validation and modification state

    public string RimworldDirectory = "";
    //public bool isMainDirValid = false;

    public string CorePackDirectory = "";
    public bool isCorePackDirValid = false;

    public string LocalModDirectory = "";
    public bool isLocalModDirValid = false;

    public string WorkshopDirectory = "";
    public bool isWorkshopDirValid = false;

    [ExportCategory("Icons")]
    [Export]
    public Texture2D? Notice;
    [Export]
    public Texture2D? Verified;

    [ExportCategory("Control Refs")]

    [Export]
    public LineEdit? RimDirLineEdit;
    [Export]
    public LineEdit? CorePackLineEdit;
    [Export]
    public LineEdit? LocalModLineEdit;
    [Export]
    public LineEdit? WorkshopLineEdit;

    // TODO: add to constructor, potentially implement global file picker
    [Export]
    private FileDialog? fileDialog;
    public string fileDialogTarget = "_";

    public void _on_load_button_pressed()
    {

    }

    public void _on_dir_line_edit_changed(string text, string sourcePath)
    {
      // called every time the user edits the line entry
    }

    public void _on_directory_picker(string path)
    {
      if (fileDialog is null)
        return;

      if (GetNodeOrNull(path) is null)
        return;

      fileDialogTarget = path;
      fileDialog.Popup();
      return;
    }

    public void _on_file_dialog_dir_selected(string dir)
    {
      if (fileDialogTarget == "_")
        return;

      switch (fileDialogTarget)
      {
        case "%LineEditRimDir":
          RimworldDirectory = dir;
          LineEdit line = GetNodeOrNull<LineEdit>(fileDialogTarget);
          if (line != null)
          {
            line.Clear();
            line.InsertTextAtCaret(dir);
            line.RightIcon = null;
          }
          return;
        default:
        case "_":
          return;
      }
    }

    public void _on_check_button_toggled(bool toggled_on, string path)
    {
      Node node = GetNodeOrNull(path);
      if (node != null)
      {
        // TODO: improve this, separation of concerns violation
        node.GetChildOrNull<LineEdit>(1).Editable = !toggled_on;
        node.GetChildOrNull<Button>(2).Disabled = toggled_on;
      }
    }
  }

}
