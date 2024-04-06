namespace RimDef
{
  public class Mod(
    string name,
    string packageId,
    string version,
    string dir,
    string defPath,
    string defPathVersion = "_"
    )
  {
    public string Name = name;
    public string PackageId = packageId;
    public string Version = version;
    public string Dir = dir;
    public string DefPath = defPath;
    public string DefPathVersion = defPathVersion;

    public override string ToString()
    {
      return Name;
    }
  }
}
