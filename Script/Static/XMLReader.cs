using Godot;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace RimDefGodot
{

  public static class XMLReader
  {
    public static void ReadRecursive(string Dir)
    {
      // we can use DirAccess to get the directories at the dir

      DirAccess.GetDirectoriesAt(Dir);
    }

    public static void GetPackagesAt(string Dir)
    {
      if (DirAccess.DirExistsAbsolute(Dir) == false)
        return;

      // we can assume that the folder we're looking at is a set of content packages, i.e. expansions or mods.

      string[] Packs = DirAccess.GetDirectoriesAt(Dir);

      foreach (string searchDir in Packs)
      {

      }
    }

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
        XmlNode? supportedVersions = doc.SelectSingleNode("/ModMetaData/supportedVersions");
      }

      return null;
    }

    public static int GetModCountAtDir(string Dir)
    {
      if (DirAccess.DirExistsAbsolute(Dir) == false)
        return -1;

      int temp = 0;

      foreach (string searchDir in DirAccess.GetDirectoriesAt(Dir))
      {
        GD.Print(searchDir);
        if (Godot.FileAccess.FileExists(Path.Join(Dir, searchDir, "/About/About.xml")))
          temp++;
      }

      return temp;
    }

    public static int GetFastModCountAtDir(string Dir)
    {
      if (DirAccess.DirExistsAbsolute(Dir) == false)
        return -1;
      foreach (string searchDir in DirAccess.GetDirectoriesAt(Dir))
      {
        if (Godot.FileAccess.FileExists(Path.Join(Dir, searchDir, "/About/About.xml")))
          return 1;
      }
      return 0;
    }

    public static bool DirHasRelativePaths(string Dir)
    {
      bool temp = false;
      string cDir;
      // dotnet profilers cost money so i'm doing it myself :D
      //ulong time;

      //time = Time.GetTicksMsec();
      if (DirAccess.DirExistsAbsolute(
        cDir = Path.Combine(Dir, "Data")))
        temp |= GetFastModCountAtDir(cDir) > 0;
      //GD.Print("Took: " + (Time.GetTicksMsec() - time) + "ms");

      //time = Time.GetTicksMsec();
      if (DirAccess.DirExistsAbsolute(
        cDir = Path.Combine(Dir, "Mods")))
        temp |= GetFastModCountAtDir(cDir) > 0;
      //GD.Print("Took: " + (Time.GetTicksMsec() - time) + "ms");

      //time = Time.GetTicksMsec();
      if (DirAccess.DirExistsAbsolute(
        cDir = Path.Combine(Dir, "../../workshop/content/294100")))
        temp |= GetFastModCountAtDir(cDir) > 0;
      //GD.Print("Took: " + (Time.GetTicksMsec() - time) + "ms");

      return temp;
    }

    public static Godot.Collections.Array<string> GetRelativePaths(string Dir)
    {
      string Data = "";
      string Mods = "";
      string Workshop = "";

      if (DirAccess.DirExistsAbsolute(Path.Combine(Dir, "Data")))
        Data = Path.Combine(Dir, "Data");

      if (DirAccess.DirExistsAbsolute(Path.Combine(Dir, "Mods")))
        Mods = Path.Combine(Dir, "Mods");

      if (DirAccess.DirExistsAbsolute(Path.Combine(Dir, "../../workshop/content/294100")))
        Workshop = Path.GetFullPath(Path.Combine(Dir, "../../workshop/content/294100"));


      return new Godot.Collections.Array<string>
      {
        "_",
        Data,
        Mods,
        Workshop,
      };

    }

    public static bool IsDirValid(string dir, bool checkRelative = false)
    {
      if (dir == "")
        return false;
      if (GetFastModCountAtDir(dir) > 0 && !checkRelative)
        return true;
      if (DirHasRelativePaths(dir) && checkRelative)
        return true;
      return false;
    }
  }
}
