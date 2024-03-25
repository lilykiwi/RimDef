namespace RimDef
{
  public class Mod
  {
    // TODO: overhaul path tracking here. move some stuff from XMLReader here too.
    public string name;
    public string packageId;
    public string version;
    public string dir;
    public string defPath;
    public string defPathVersion;

    public Mod(string name, string packageId, string version, string dir, string defPath, string defPathVersion = "_")
    {
      this.name = name;
      this.packageId = packageId;
      this.version = version;
      this.dir = dir;
      this.defPath = defPath;
      this.defPathVersion = defPathVersion;
    }

    public override string ToString()
    {
      return name;
    }
  }
}
