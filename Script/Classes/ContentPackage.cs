namespace RimDefGodot
{
  public abstract class ContentPackage
  {
    public string packageId;
    public string version;
    public string baseDir;
    public string[] defPaths;

    public ContentPackage(string packageId, string version, string baseDir, string[]? defPaths)
    {
      this.packageId = packageId;
      this.version = version;
      this.baseDir = baseDir;

      this.defPaths = defPaths is null ? System.Array.Empty<string>() : defPaths;
    }

    public override string ToString()
    {
      return packageId;
    }
  }

  public class Expansion : ContentPackage
  {
    public Expansion(
      string packageId,
      string version,
      string baseDir,
      string[]? defPaths
      ) : base(packageId, version, baseDir, defPaths)
    { }
  }

  public class Mod : ContentPackage
  {

    public string name;

    public string url;

    public Mod(
      string name,
      string packageId,
      string version,
      string url,
      string baseDir,
      string[]? defPaths
      ) : base(packageId, version, baseDir, defPaths)
    {
      this.name = name;
      this.url = url;
    }
  }
}
