using System.Collections.Generic;

namespace RimDef
{
  public class Patch(
      Mod mod,
      string xpath
    )
  {
    /// <summary>mod</summary>
    /// <remarks>parent mod</remarks>
    public Mod Mod = mod;

    public string Xpath = xpath;
  }
}
