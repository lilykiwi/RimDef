using Godot;
using System;
using System.Diagnostics;
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

      if (FileAccess.FileExists(Dir + "/About/About.xml"))
      {
        string raw = FileAccess.GetFileAsString(Dir + "/About/About.xml");
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
  }
}
