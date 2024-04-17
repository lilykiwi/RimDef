using System.Collections.Generic;

namespace RimDefGodot
{
  public class Def
  {
    public ContentPackage defSource;

    public string defType;
    public string defName;
    public string label;
    public string description;
    public string texture;
    public string xml;
    public string file;

    public List<string[]> details = new List<string[]>();

    public Def(
        ContentPackage defSource,
        string defType,
        string defName,
        string label,
        string description,
        string texture,
        string xml,
        string file
    )
    {
      this.defSource = defSource;
      this.defType = defType;
      this.defName = defName;
      this.label = label;
      this.description = description;
      this.texture = texture;
      this.xml = xml;
      this.file = file;
    }
  }
}
