using Godot;
using static RimDefGodot.NodeHelpers;

namespace RimDefGodot
{
  [Tool]
  [GlobalClass]
  public partial class DirectoryEntry : HBoxContainer
  {

    public DirectoryEntry(bool hasAutoToggle, string text, ConfigPage configPage)
    {
      this.hasAutoToggle = hasAutoToggle;
      this.text = text;
      this.configPage = configPage;
    }

    public string text;
    private ConfigPage configPage;
    public bool hasAutoToggle;

    public CheckBox? AutoToggle;
    public LineEdit? LineEdit;
    public LineEdit? InfoBox;
    public Button? DirButton;

    public override void _Ready()
    {
      AutoToggle?.Free();
      AutoToggle = null;
      LineEdit?.Free();
      LineEdit = null;
      InfoBox?.Free();
      InfoBox = null;
      DirButton?.Free();
      DirButton = null;

      // fallback for if we have any unmanaged children
      foreach (Node child in GetChildren())
      {
        child.Free();
      }

      if (hasAutoToggle)
      {
        AutoToggle = AddNewChild(this, new CheckBox());
        AutoToggle.Flat = true;
        //AutoToggle.Text = "Auto";
        AutoToggle.ButtonPressed = true;
        AutoToggle.Toggled += (bool val) =>
          configPage._OnCheckBoxChanged(val, this);
        AutoToggle.TooltipText = "Automatically infer this path from the base Rimworld directory";
      }

      LineEdit = AddNewChild(this, new LineEdit());
      LineEdit.SizeFlagsHorizontal = SizeFlags.ExpandFill;
      LineEdit.PlaceholderText = text;
      LineEdit.CustomMinimumSize = new Vector2(300, 0);
      LineEdit.TextChanged += (string text) =>
        configPage._OnTextChanged(this);

      InfoBox = AddNewChild(this, new LineEdit());
      InfoBox.Editable = false;
      if (hasAutoToggle)
        InfoBox.CustomMinimumSize = new Vector2(60, 0);
      else
        InfoBox.CustomMinimumSize = new Vector2(25, 0);
      InfoBox.AddThemeConstantOverride("minimum_character_width", 0);
      InfoBox.RightIcon = configPage.NoticeIcon;
      InfoBox.Alignment = HorizontalAlignment.Center;

      DirButton = AddNewChild(this, new Button());
      DirButton.Icon = configPage.FolderIcon;
      DirButton.Pressed += () =>
        configPage._OnDirButtonPressed(this);

      // update checkbox state
      configPage._OnCheckBoxChanged(hasAutoToggle, this);

      // TODO: auto load paths from a config file saved at user directory
    }
  }
}
