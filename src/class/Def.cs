using System.Collections.Generic;

namespace RimDef
{
  public class Def(
      Mod mod,
      string defType,
      string defName,
      string label,
      string description,
      string texture,
      string xml,
      string file
    )
  {
    public Mod Mod = mod;

    public string DefType = defType;
    public string DefName = defName;
    public string Label = label;
    public string Description = description;
    public string Texture = texture;
    public string Xml = xml;
    public string File = file;

    public List<string[]> details = new List<string[]>();
  }
}
