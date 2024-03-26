using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Xml;

namespace RimDef
{
  class XMLReader
  {

    // unused string
    //public string? modDir;

    public List<string> defTypes;

    public XMLReader()
    {
      defTypes = new List<string>();
    }

    public List<Def> LoadAllDefs(Mod mod)
    {
      //Console.WriteLine("loading mod: " + mod.name);

      List<Def> defs = new List<Def>();

      // TODO: prevent duplicates from appearing.
      // for example with rimmsqol: 
      // - Defs\WorldObjectDefs\WorldObjects.xml
      // - 1.4\Defs\WorldObjectDefs\WorldObjects.xml
      // both exist and have duplicate entries
      // currently both are shown and top-right path dictates which is which
      //Console.WriteLine(mod.dir + @"/About/About.xml");
      if (File.Exists(mod.dir + @"/About/About.xml"))
      {
        if (Directory.Exists(mod.defPath))
        {
          string[] files = Directory.GetFiles(mod.defPath, "*.xml", SearchOption.AllDirectories);

          foreach (string file in files)
          {
            if (File.Exists(file))
            {
              //Console.WriteLine("reading " + file);
              defs.AddRange(ReadXML(mod, file));
            }
            //else
            //{
            //  Console.WriteLine("skipping " + file);
            //}
          }
        }
        //else
        //{
        //  Console.WriteLine("invalid path: " + mod.defPath);
        //}

        if (Directory.Exists(mod.defPathVersion))
        {
          string[] files = Directory.GetFiles(mod.defPathVersion, "*.xml", SearchOption.AllDirectories);

          foreach (string file in files)
          {
            if (File.Exists(file))
            {
              //Console.WriteLine("reading " + file);
              defs.AddRange(ReadXML(mod, file));
            }
            //else
            //{
            //  Console.WriteLine("skipping " + file);
            //}
          }
        }
      }
      //else
      //{
      //  Console.WriteLine("skipping " + mod.name + ": " + mod.dir + @"/About/About.xml");
      //}

      return defs;
    }

    public List<Mod> GetMods(string dir, string versionText, bool core = false)
    {
      List<Mod> Packs = new List<Mod>();

      List<Mod> modVersions = new List<Mod>();

      if (core)
      {
        //Console.WriteLine("finding core mods at " + dir);
        foreach (string mod in Directory.GetDirectories(dir))
        {
          string aboutFile = Path.Combine(mod, "About/About.xml");
          Console.WriteLine(aboutFile);
          if (File.Exists(aboutFile))
          {
            // core packages dont specify a name. instead we infer from the path
            Packs.Add(new Mod(name: new DirectoryInfo(mod).Name,
                              packageId: ReadPackageId(aboutFile),
                              version: GetCoreVersion(mod, versionText),
                              dir: mod,
                              defPath: Path.Combine(mod, "Defs")));
          }
        }
      }
      else
      {
        //Console.WriteLine("finding mods at " + dir);
        foreach (string mod in Directory.GetDirectories(dir))
        {
          string aboutFile = Path.Combine(mod, "About/About.xml");
          //Console.WriteLine(aboutFile);
          if (File.Exists(aboutFile))
            modVersions.Add(new Mod(name: ReadModName(aboutFile),
                                    packageId: ReadPackageId(aboutFile),
                                    version: "_",
                                    dir: mod,
                                    defPath: Path.Combine(mod, "Defs")));

        }

        if (versionText == "All")
        {
          foreach (Mod mod in modVersions)
          {
            foreach (string ver in Form1.versionNames)
            {
              if (ver == "All")
              {
                string path = Path.Combine(mod.dir, "Defs");
                if (Directory.Exists(path))
                {
                  Packs.Add(new Mod(name: mod.name + "(base)",
                                    packageId: mod.packageId,
                                    version: "_",
                                    dir: mod.dir,
                                    defPath: path));
                }
              }
              else
              {
                string path = Path.Combine(mod.dir, ver);
                // this is a rather naive assumption but should prevent any false positives (i.e. empty lists)
                path = Path.Combine(path, "Defs");
                if (Directory.Exists(path))
                {
                  Packs.Add(new Mod(name: mod.name + ver,
                                    packageId: mod.packageId,
                                    version: "_",
                                    dir: mod.dir,
                                    defPath: path));
                }
              }

            }
          }
        }
        else
        {
          foreach (Mod mod in modVersions)
          {

            string baseDefs = "";
            string versionDefs = "_";

            string baseDefsDir = Path.Combine(mod.dir, "Defs");
            string versionDefsDir = Path.Combine(Path.Combine(mod.dir,
                                                              versionText),
                                                 "Defs");

            if (Directory.Exists(baseDefsDir))
              baseDefs = baseDefsDir;

            if (Directory.Exists(versionDefsDir))
              versionDefs = versionDefsDir;

            Packs.Add(new Mod(name: mod.name,
                  packageId: mod.packageId,
                  version: versionText,
                  dir: mod.dir,
                  defPath: baseDefs,
                  defPathVersion: versionDefs));
          }
        }
      }

      return Packs;
    }

    public string GetCoreVersion(string rimDir, string versionText = "_")
    {
      string gameVer = "Version";
      string path = Path.Combine(rimDir, "Version.txt");
      if (File.Exists(path))
        using (StreamReader sr = new StreamReader(path))
          gameVer = sr.ReadLine();

      if (gameVer.Length > 3)
        gameVer = gameVer.Substring(0, 3);
      else
        gameVer = "Version";

      // if we can't get the version, should we assume from the comboBox?
      if (versionText != "Version" && versionText != "All" && versionText != "_")
        gameVer = versionText;

      return gameVer;
    }

    public List<string> ReadModConfig()
    {
      List<string> activeMods = new List<string>();
      string path = Environment.ExpandEnvironmentVariables(
          "%USERPROFILE%/Appdata/LocalLow/Ludeon Studios/RimWorld by Ludeon Studios/Config/ModsConfig.xml"
      );
      if (!File.Exists(path))
      {
        Debug.Fail("file %USERPROFILE%/Appdata/LocalLow/Ludeon Studios/ ... /ModsConfig.xml does not exist?");
        return activeMods;
      }

      var doc = new XmlDocument();

      try
      {
        doc.Load(path);
      }
      catch (Exception e)
      {
        Debug.Fail("invalid mod config for " + path + "  -  " + e);
        return activeMods;
      }

      foreach (
          XmlNode node in doc.DocumentElement.SelectNodes("/ModsConfigData/activeMods/li")
      )
        activeMods.Add(node.InnerText);
      return activeMods;
    }

    public string ReadPackageId(string file)
    {
      string packageId = "_";
      var doc = new XmlDocument();
      if (!File.Exists(file))
      {
        Debug.Fail("file " + file + " does not exist?");
        return packageId;
      }

      try
      {
        doc.Load(file);
      }
      catch (Exception e)
      {
        Debug.Fail("invalid package id for " + file + "  -  " + e);
        return "error";
      }

      XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/packageId");
      if (node != null)
        packageId = node.InnerText.ToLower();
      return packageId;
    }

    public string ReadModName(string file)
    {
      string modName = "_";
      var doc = new XmlDocument();
      if (!File.Exists(file))
      {
        Debug.Fail("file " + file + " does not exist?");
        return modName;
      }

      try
      {
        doc.Load(file);
      }
      catch (Exception e)
      {
        Debug.Fail("invalid mod name for " + file + "  -  " + e);
        return "error";
      }

      XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/name");
      if (node != null)
        modName = node.InnerText.ToLower();
      return modName;
    }

    private List<Def> ReadXML(Mod mod, string file)
    {
      List<Def> xmlDefs = new List<Def>();

      var doc = new XmlDocument();
      //Console.WriteLine(file);

      try
      {
        doc.Load(file);
      }
      catch (Exception e)
      {
        Debug.Fail("invalid xml for " + file + "  -  " + e);
        return xmlDefs;
      }

      foreach (XmlNode node in doc.DocumentElement.SelectNodes("/Defs"))
      {
        foreach (XmlNode child in node.ChildNodes)
        {
          string type = child.Name;
          int idx = type.IndexOf("Def");
          if (idx > 0)
          {
            if (!defTypes.Contains(type))
            {
              defTypes.Add(type);
            }

            string defType = type.Substring(0, idx).ToLower();

            string defName = "";
            string label = "";
            string description = "";
            string texture = "";

            for (int i = 0; i < child.ChildNodes.Count; i++)
            {
              string name = child.ChildNodes[i].Name;
              if (name == "defName")
              {
                defName = child.ChildNodes[i].InnerText;
              }
              if (name == "label")
              {
                label = child.ChildNodes[i].InnerText;
              }
              if (name == "description")
              {
                description = child.ChildNodes[i].InnerText;
              }
            }

            // Texture
            XmlNode texNode = child.SelectSingleNode("graphicData/texPath");
            texture = GetTexture(texNode, mod.dir);

            // XML view
            string xmlOut = System.Xml.Linq.XDocument.Parse(child.OuterXml).ToString();

            Def def = CreateDef(
                mod,
                child,
                defType,
                defName,
                label,
                description,
                texture,
                xmlOut,
                file
            );

            if (defName != "")
            {
              xmlDefs.Add(def);
            }
          }
        }
      }

      return xmlDefs;
    }

    public string GetTexture(XmlNode node, string dir)
    {
      string[] orientations = { "_north", "_south", "_west", "_east" };
      string texture = "_";
      if (node != null)
      {
        // core textures
        // https://ludeon.com/forums/index.php?topic=2325

        string texPath = dir + @"Textures/" + node.InnerText;
        if (Directory.Exists(texPath))
        {
          string[] files = Directory.GetFiles(
              texPath,
              "*.*",
              SearchOption.AllDirectories
          );
          return files[0];
        }
        else
        {
          texture = dir + @"/Textures/" + node.InnerText + ".png";
          if (!File.Exists(texture))
          {
            //Console.WriteLine(texture + " does not exist");
            foreach (string ori in orientations)
            {
              string textureOri = dir + @"/Textures/" + node.InnerText + ori + ".png";
              if (File.Exists(textureOri))
              {
                return textureOri;
              }
            }
          }
          else
          {
            return texture;
          }
        }
      }

      return "_";
    }

    public Def CreateDef(
        Mod mod,
        XmlNode child,
        string defType,
        string defName,
        string label,
        string description,
        string texture,
        string xmlOut,
        string file
    )
    {
      if (defType == "thing")
      {
        //Console.WriteLine("found thingDef: " + defName);
        ThingDef thing = new ThingDef(
            mod: mod,
            defType: defType,
            defName: defName,
            label: label,
            description: description,
            texture: texture,
            xml: xmlOut,
            file: file
        );

        // <statBases>
        XmlNode statsNode = child.SelectSingleNode("statBases");
        if (statsNode != null)
        {
          foreach (XmlNode stat in statsNode.ChildNodes)
          {
            thing.details.Add(new string[] { stat.Name, stat.InnerText });
          }
        }
        return thing;
      }

      if (defType == "recipe")
      {
        //Console.WriteLine("found recipeDef: " + defName);
        // <products>
        string products = "?";
        XmlNodeList productNodes = child.SelectNodes("products");
        foreach (XmlNode n in productNodes)
        {
          foreach (XmlNode p in n.ChildNodes)
          {
            products += p.InnerXml + "x " + p.Name;
          }
        }

        // <researchPrerequisite>
        string research = "?";
        XmlNode researchNode = child.SelectSingleNode("researchPrerequisite");
        if (researchNode != null)
        {
          research = researchNode.InnerText;
        }

        // <skillRequirements>
        string skill = "?";
        XmlNode skillNode = child.SelectSingleNode("skillRequirements");
        if (skillNode != null)
        {
          skill = skillNode.InnerText;
        }

        // <workAmount>
        string work = "?";
        XmlNode workNode = child.SelectSingleNode("workAmount");
        if (workNode != null)
        {
          work = workNode.InnerText;
        }

        RecipeDef recipe = new RecipeDef(
            research: research,
            skill: skill,
            work: work,
            mod: mod,
            defType: defType,
            defName: defName,
            label: label,
            description: description,
            texture: texture,
            xml: xmlOut,
            file: file
        );

        // <ingredients>
        foreach (XmlNode n in child.SelectNodes("ingredients/li"))
        {
          string ingredients = "";
          foreach (XmlNode xml in n.SelectNodes("filter/thingDefs/li"))
          {
            ingredients += xml.InnerText + ", ";
          }
          foreach (XmlNode xml in n.SelectNodes("filter/categories/li"))
          {
            ingredients += xml.InnerText + ", ";
          }
          ingredients = ingredients.Substring(0, ingredients.Length - 2);

          string amount = n.LastChild.InnerText;

          recipe.AddIngredients(new string[] { amount, ingredients, products });
        }
        return recipe;
      }

      //Console.WriteLine("returning Def: " + defName);
      return new Def(
          mod: mod,
          defType: defType,
          defName: defName,
          label: label,
          description: description,
          texture: texture,
          xml: xmlOut,
          file: file
      );
    }
  }
}
