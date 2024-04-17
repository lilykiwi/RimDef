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

    public Button? loadButton;
    public Array<DirectoryEntry> DirectoryEntries;

    public RichTextLabel? Icons8Notice;

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

    public Godot.Collections.Array _get_tool_buttons()
    {
      return new Godot.Collections.Array { "RegeneratePanel" };
    }

    public void RegeneratePanel()
    {
      _Ready();
    }

    public override void _Ready()
    {
      foreach (Node child in GetChildren())
      {
        child.QueueFree();
      }
      DirectoryEntries.Clear();

      globalContainer = AddNewChild(this, new HBoxContainer());
      controlContainer = AddNewChild(globalContainer, new VBoxContainer());
      controlContainer.SizeFlagsHorizontal = SizeFlags.ShrinkBegin;

      codeEdit = AddNewChild(globalContainer, new CodeEdit());
      codeEdit.SizeFlagsHorizontal = SizeFlags.ExpandFill;

      loadButton = AddNewChild(controlContainer, new Button());
      loadButton.Text = "Load XML";
      loadButton.Disabled = true;
      //loadButton.Pressed += () => ();

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(this, false, "Rimworld Directory")
        )
      );

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(this, true, "Core Packages Directory")
        )
      );

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(this, true, "Local Mods Directory")
        )
      );

      DirectoryEntries.Add(
        AddNewChild(
          controlContainer,
          new DirectoryEntry(this, true, "Steam Mods Directory")
        )
      );

      foreach (DirectoryEntry entry in DirectoryEntries)
      {
        if (entry.LineEdit is not null)
          entry.LineEdit.TooltipText = entry.text;
      }

      Container c = AddNewChild(controlContainer, new Container());
      c.SizeFlagsVertical = SizeFlags.ExpandFill;

      // icons8 copyright attribution
      Icons8Notice = AddNewChild(controlContainer, new RichTextLabel());
      Icons8Notice.AppendText("Icons by [url=https://icons8.com/]Icons8[/url]");
      Icons8Notice.SizeFlagsHorizontal = SizeFlags.ExpandFill;
      Icons8Notice.FitContent = true;
      Icons8Notice.MetaClicked += (Variant meta) => _OnIcons8ButtonPressed();
    }

    /// <summary>EnableButtonIfValid</summary>
    /// <remarks>Quick method for setting the "Load XML" button to be
    /// enabled if any of the directories are valid (i.e. can infer
    /// paths or have mods dependent on their type). This should
    /// prevent the button from being pressed when there are no
    /// valid directories.</remarks>
    public void EnableButtonIfValid()
    {
      if (loadButton is null)
        return;

      bool isAnyValid = false;
      foreach (DirectoryEntry entry in DirectoryEntries)
        isAnyValid |= entry.IsDirValid;

      if (isAnyValid)
        loadButton.Disabled = false;
      else
        loadButton.Disabled = true;
    }

    public void _OnTextChanged(DirectoryEntry src)
    {
      if (src.InfoBox is null)
        return;
      if (src.LineEdit is null)
        return;
      if (src.hasAutoToggle is null)
        return;

      if (IsModDirValid(src.LineEdit.Text, (bool)!src.hasAutoToggle))
      {
        src.InfoBox.RightIcon = VerifiedIcon;
        src.IsDirValid = true;
        if ((bool)!src.hasAutoToggle)
        {
          _InferFromValidDir(src.LineEdit.Text);
          src.InfoBox.TooltipText = "Valid source dir for inference";
        }
        else
        {
          // we want to update the cute info box with the tooltip
          // and with the quantity of mods here
          src.InfoBox.Text = GetModCountAtDir(src.LineEdit.Text).ToString();
          src.InfoBox.TooltipText =
            "Found " + src.InfoBox.Text + " mods at this directory";
        }
      }
      else
      {
        src.InfoBox.RightIcon = NoticeIcon;
        src.InfoBox.Text = "";
        src.InfoBox.TooltipText = "Invalid directory";
        src.IsDirValid = false;
      }

      // call this to check if the main load button should be enabled
      EnableButtonIfValid();
    }

    // csharpier-ignore
    public void _InferFromValidDir(string? dir)
    {
      if (dir is null) return;

      Array<string> relatives = GetRelativeModPaths(dir);

      foreach (DirectoryEntry item in DirectoryEntries)
      {
        int i = DirectoryEntries.IndexOf(item);

        if ( i    == 0                    ) continue;
        if ( item.LineEdit is null        ) continue;
        if ( item.AutoToggle is null      ) continue;
        if (!item.AutoToggle.ButtonPressed) continue;

        item.LineEdit.Text = relatives[i];
        _OnTextChanged(item);

      }

    }

    public void _OnCheckBoxChanged(bool val, DirectoryEntry src)
    {
      if (src.LineEdit is null)
        return;
      if (src.DirButton is null)
        return;

      src.LineEdit.Editable = !val;
      src.DirButton.Disabled = val;
      _InferFromValidDir(DirectoryEntries[0].LineEdit?.Text);
    }

    public void _OnDirButtonPressed(DirectoryEntry src)
    {
      throw new NotImplementedException();
    }

    public void _OnIcons8ButtonPressed()
    {
      OS.ShellOpen("https://icons8.com/");
    }
  }
}
