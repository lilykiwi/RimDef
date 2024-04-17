using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Godot;

namespace RimDefGodot
{
  public static class XMLReader
  {
    // TODO doc n fix
    public static void ReadRecursive(string Dir)
    {
      // we can use DirAccess to get the directories at the dir

      DirAccess.GetDirectoriesAt(Dir);
    }

    // TODO doc n fix
    public static void GetPackagesAt(string Dir)
    {
      if (DirAccess.DirExistsAbsolute(Dir) == false)
        return;

      // we can assume that the folder we're looking at is a set of content packages, i.e. expansions or mods.

      string[] Packs = DirAccess.GetDirectoriesAt(Dir);

      foreach (string searchDir in Packs) { }
    }

    // TODO doc n fix
    public static ContentPackage? ReadAboutXML(string Dir)
    {
      if (Godot.FileAccess.FileExists(Dir + "/About/About.xml"))
      {
        string raw = Godot.FileAccess.GetFileAsString(Dir + "/About/About.xml");
        XmlDocument doc = new();
        try
        {
          doc.LoadXml(raw);
        }
        catch (Exception ex)
        {
          Debug.WriteLine(ex.Message);
          return null;
        }

        if (doc is null)
        {
          return null;
        }

        // ModMetaData is present even on core expansions :D
        XmlNode? name = doc.SelectSingleNode("/ModMetaData/name");
        XmlNode? packageId = doc.SelectSingleNode("/ModMetaData/packageId");
        XmlNode? author = doc.SelectSingleNode("/ModMetaData/author");
        XmlNode? supportedVersions = doc.SelectSingleNode(
          "/ModMetaData/supportedVersions"
        );
      }

      return null;
    }

    // TODO: move to DirectoryManager
    /// <summary>GetModCountAtDir</summary>
    /// <remarks>O(n) complexity based on the amount of directories
    /// at the target path. This method should only check 1 level
    /// deep, and looks for ./About/About.xml for identifying a
    /// content package.</remarks>
    /// <param name="Dir">Directory string or null, doesn't need
    /// to be ran through Path.GetFullPath() or similar.</param>
    /// <returns>-1 invalid dir, returns # of mods in Dir</returns>
    public static int GetModCountAtDir(string Dir)
    {
      if (DirAccess.DirExistsAbsolute(Dir) == false)
        return -1;
      int temp = 0;
      foreach (string searchDir in DirAccess.GetDirectoriesAt(Dir))
        if (
          Godot.FileAccess.FileExists(
            Path.Join(Dir, searchDir, "/About/About.xml")
          )
        )
          temp++;
      return temp;
    }

    // TODO: move to DirectoryManager
    /// <summary>GetFastModCountAtDir</summary>
    /// <remarks>Absolute best case complexity of this method vs
    /// GetModCountAtDir is O(1), worst case is still O(n).
    /// This can be used to quickly check to see if a dir
    /// is valid, without getting an accurate result as to the
    /// quantity of mods.</remarks>
    /// <param name="Dir">Directory string or null, doesn't need
    /// to be ran through Path.GetFullPath() or similar.</param>
    /// <returns>-1 invalid dir, 0 no mods, 1 for any mod</returns>
    public static int GetFastModCountAtDir(string? Dir)
    {
      if (DirAccess.DirExistsAbsolute(Dir) == false)
        return -1;
      foreach (string searchDir in DirAccess.GetDirectoriesAt(Dir))
        if (
          Godot.FileAccess.FileExists(
            Path.Join(Dir, searchDir, "/About/About.xml")
          )
        )
          return 1;
      return 0;
    }

    // TODO: move to DirectoryManager
    /// <summary>ModDirHasRelativePaths</summary>
    /// <remarks>This is a relatively fast method to find if a
    /// directory has valid content pack directories relative to it.
    /// The checked directories are:
    /// - ./Data :: for core content packages i.e. expansions core
    /// - ./Mods :: for local mods added by the user
    /// - ../../workshop/content/294100 :: for Steam workshop mods,
    ///     assuming that the Dir parameter is a Steam library dir.
    /// Uses GetFastModCountAtDir, so worst case complexity is O(n)</remarks>
    /// <param name="Dir">Directory to check for relative paths</param>
    /// <returns>true if the dir has other search dirs relative to it</returns>
    public static bool ModDirHasRelativePaths(string Dir)
    {
      bool temp = false;
      string cDir;
      // dotnet profilers cost money so i'm doing it myself :D
      //ulong time;

      //time = Time.GetTicksMsec();
      if (DirAccess.DirExistsAbsolute(cDir = Path.Combine(Dir, "Data")))
        temp |= GetFastModCountAtDir(cDir) > 0;
      //GD.Print("Took: " + (Time.GetTicksMsec() - time) + "ms");

      //time = Time.GetTicksMsec();
      if (DirAccess.DirExistsAbsolute(cDir = Path.Combine(Dir, "Mods")))
        temp |= GetFastModCountAtDir(cDir) > 0;
      //GD.Print("Took: " + (Time.GetTicksMsec() - time) + "ms");

      //time = Time.GetTicksMsec();
      if (
        DirAccess.DirExistsAbsolute(
          cDir = Path.Combine(Dir, "../../workshop/content/294100")
        )
      )
        temp |= GetFastModCountAtDir(cDir) > 0;
      //GD.Print("Took: " + (Time.GetTicksMsec() - time) + "ms");

      return temp;
    }

    // TODO: move to DirectoryManager
    /// <summary>GetFastRelativeModPaths</summary>
    /// <remarks>O(1) method to get (potentially invalid) relative
    /// directories from the Dir parameter.</remarks>
    /// <param name="Dir">Directory to check for relative paths</param>
    /// <returns>Godot.Collections.Array Array of potentially invalid directories</returns>
    public static Godot.Collections.Array<string> GetFastRelativeModPaths(
      string Dir
    )
    {
      string Data = "";
      string Mods = "";
      string Workshop = "";

      if (DirAccess.DirExistsAbsolute(Path.Combine(Dir, "Data")))
        Data = Path.GetFullPath(Path.Combine(Dir, "Data"));

      if (DirAccess.DirExistsAbsolute(Path.Combine(Dir, "Mods")))
        Mods = Path.GetFullPath(Path.Combine(Dir, "Mods"));

      if (
        DirAccess.DirExistsAbsolute(
          Path.Combine(Dir, "../../workshop/content/294100")
        )
      )
        Workshop = Path.GetFullPath(
          Path.Combine(Dir, "../../workshop/content/294100")
        );

      return new Godot.Collections.Array<string>
      {
        "_", // first entry is skipped in ConfigPage._InferFromValidDir
        Data,
        Mods,
        Workshop,
      };
    }

    // TODO: move to DirectoryManager
    /// <summary>GetRelativeModPaths</summary>
    /// <remarks>worst-case O(n) method to get ONLY VALID relative
    /// directories from the Dir parameter.</remarks>
    /// <param name="Dir">Directory to check for relative paths</param>
    /// <returns>Godot.Collections.Array of valid directories</returns>
    public static Godot.Collections.Array<string> GetRelativeModPaths(
      string Dir
    )
    {
      string Data = "";
      string Mods = "";
      string Workshop = "";

      if (IsModDirValid(Path.Combine(Dir, "Data")))
        Data = Path.GetFullPath(Path.Combine(Dir, "Data"));

      if (IsModDirValid(Path.Combine(Dir, "Mods")))
        Mods = Path.GetFullPath(Path.Combine(Dir, "Mods"));

      if (IsModDirValid(Path.Combine(Dir, "../../workshop/content/294100")))
        Workshop = Path.GetFullPath(
          Path.Combine(Dir, "../../workshop/content/294100")
        );

      return new Godot.Collections.Array<string>
      {
        "_", // first entry is skipped in ConfigPage._InferFromValidDir
        Data,
        Mods,
        Workshop,
      };
    }

    // TODO: move to DirectoryManager
    /// <summary>IsModDirValid</summary>
    /// <remarks>worst-case O(n) method for checking if a directory holds
    /// content packages, or checking to see if a directory has relatives
    /// if checkRelative is true. The bool parameter can be inferred from
    /// the "hasAutoToggle" parameter in DirectoryEntry</remarks>
    /// <param name="dir">directory string, or null.</param>
    /// <param name="checkRelative">defaults to false</param>
    /// <returns>true if valid dir, false otherwise</returns>
    public static bool IsModDirValid(string? dir, bool checkRelative = false)
    {
      if (dir == "" || dir is null)
        return false;
      if (DirAccess.DirExistsAbsolute(dir) == false)
        return false;
      if (GetFastModCountAtDir(dir) > 0 && !checkRelative)
        return true;
      if (ModDirHasRelativePaths(dir) && checkRelative)
        return true;
      return false;
    }
  }
}
