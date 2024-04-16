using System;
using System.Linq;
using Godot;
using Godot.Collections;
using static RimDefGodot.NodeHelpers;
using static RimDefGodot.XMLReader;

namespace RimDefGodot
{
  [Tool]
  [GlobalClass]
  public partial class ConfigPage : MarginContainer
  {
    public HBoxContainer? globalContainer;
    public VBoxContainer? controlContainer;
    public CodeEdit? codeEdit;

    public Array<DirectoryEntry> DirectoryEntries;

    [ExportCategory("Icons")]
    [Export]
    public Texture2D? NoticeIcon;
    [Export]
    public Texture2D? VerifiedIcon;
    [Export]
    public Texture2D? FolderIcon;

    public ConfigPage()
    {
      DirectoryEntries = new Array<DirectoryEntry>();
    }

    public override void _Ready()
    {
      foreach (Node child in GetChildren())
      {
        child.QueueFree();
      }
      globalContainer = null;
      controlContainer = null;
      codeEdit = null;
      DirectoryEntries.Clear();

      globalContainer = AddNewChild(this, new HBoxContainer());
      controlContainer = AddNewChild(
        globalContainer, new VBoxContainer()
      );
      controlContainer.SizeFlagsHorizontal = SizeFlags.ShrinkBegin;

      codeEdit = AddNewChild(globalContainer, new CodeEdit());
      codeEdit.SizeFlagsHorizontal = SizeFlags.ExpandFill;

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(false, "Rimworld Directory", this)
        )
      );

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(true, "Core Packages Directory", this)
        )
      );

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(true, "Local Mods Directory", this)
        )
      );

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(true, "Steam Mods Directory", this)
        )
      );

      foreach (DirectoryEntry entry in DirectoryEntries)
      {
        if (entry.LineEdit is not null)
          entry.LineEdit.TooltipText = entry.text;
      }
    }

    public void _OnTextChanged(DirectoryEntry src)
    {
      if (src.InfoBox is not null && src.LineEdit is not null)
      {
        if (IsDirValid(src.LineEdit.Text, !src.hasAutoToggle))
        {
          src.InfoBox.RightIcon = VerifiedIcon;
          if (!src.hasAutoToggle)
          {
            _InferFromValidDir(src.LineEdit.Text);
            src.InfoBox.TooltipText = "Valid source dir for inference";
          }
          else
          {
            // we want to update the cute info box with the tooltip
            // and with the quantity of mods here
            src.InfoBox.Text = GetModCountAtDir(src.LineEdit.Text).ToString();
            src.InfoBox.TooltipText = "Found " + src.InfoBox.Text + " mods at this directory";
          }
        }
        else
        {
          src.InfoBox.RightIcon = NoticeIcon;
          src.InfoBox.Text = "";
          src.InfoBox.TooltipText = "Invalid directory";
        }
      }
    }

    public void _InferFromValidDir(string dir)
    {
      Godot.Collections.Array<string> relatives = GetRelativePaths(dir);

      foreach (DirectoryEntry item in DirectoryEntries)
      {
        int i = DirectoryEntries.IndexOf(item);
        if (i == 0) continue;
        if (item.LineEdit is not null && item.AutoToggle is not null && item.AutoToggle.ButtonPressed)
        {
          item.LineEdit.Text = relatives[i];
          _OnTextChanged(item);
        }
      }

    }

    public void _OnCheckBoxChanged(bool val, DirectoryEntry src)
    {
      if (src.LineEdit is not null && src.DirButton is not null)
      {
        src.LineEdit.Editable = !val;
        src.DirButton.Disabled = val;
        if (DirectoryEntries[0].LineEdit is not null)
          _InferFromValidDir(DirectoryEntries[0].LineEdit.Text);
      }
    }

    public void _OnDirButtonPressed(DirectoryEntry src)
    {
      throw new NotImplementedException();
    }
  }
}
