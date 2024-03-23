using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace RimDef
{
    class XMLReader
    {
        public string modDir;

        public List<string> defTypes;

        public List<Def> LoadAllDefs(Mod mod)
        {
            Console.WriteLine("loading mod: " + mod.name);

            defTypes = new List<string>();

            List<Def> defs = new List<Def>();

            // NOTE: The contents of a Def folder don't follow a clear naming convention,
            // but the folder names are generally the same in every mod.
            // see https://rimworldwiki.com/wiki/Modding_Tutorials/Mod_folder_structure
            // dir/Defs
            // dir/Patches (implement patch defs)
            // FIXME: This will fail if dir/1.4 doesn't exist. Need to implement a new check system for different distribution folders.
            Console.WriteLine(mod.dir + @"/About/About.xml");
            if (File.Exists(mod.dir + @"/About/About.xml"))
            {
                string path = mod.dir + "/" + mod.version;
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);

                    foreach (string file in files)
                    {
                        if (File.Exists(file))
                        {
                            Console.WriteLine("reading " + file);
                            defs.AddRange(ReadXML(mod, file));
                        }
                        else
                        {
                            Console.WriteLine("skipping " + file);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("invalid path: " + path);
                }
            }
            else
            {
                Console.WriteLine("skipping " + mod.name + ": " + mod.dir + @"/About/About.xml");
            }

            return defs;
        }

        public List<string> ReadModConfig()
        {
            List<string> activeMods = new List<string>();
            string path = Environment.ExpandEnvironmentVariables(
                "%USERPROFILE%/Appdata/LocalLow/Ludeon Studios/RimWorld by Ludeon Studios/Config/ModsConfig.xml"
            );
            var doc = new XmlDocument();
            doc.Load(path);
            foreach (
                XmlNode node in doc.DocumentElement.SelectNodes("/ModsConfigData/activeMods/li")
            )
                activeMods.Add(node.InnerText);
            return activeMods;
        }

        public string ReadPackageId(string file)
        {
            string packageId = "-undefined-";
            var doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/packageId");
            if (node != null)
                packageId = node.InnerText.ToLower();
            return packageId;
        }

        public string ReadModName(string file)
        {
            string modName = "-undefined-";
            var doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/name");
            if (node != null)
                modName = node.InnerText.ToLower();
            return modName;
        }

        private List<Def> ReadXML(Mod mod, string file)
        {
            List<Def> xmlDefs = new List<Def>();

            var doc = new XmlDocument();
            doc.Load(file);
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
                        GetTexture(texNode, mod.dir);

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
                Console.WriteLine("found thingDef: " + defName);
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
                Console.WriteLine("found recipeDef: " + defName);
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

            Console.WriteLine("returning Def: " + defName);
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

        //public void DisableNode(Def def)
        //{
        //    try
        //    {
        //        // backup file
        //        string backup = def.file + ".ori";
        //        if (!File.Exists(backup))
        //            File.Copy(def.file, backup);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //
        //    try
        //    {
        //        // comment out xml
        //        var doc = new XmlDocument();
        //        doc.Load(def.file);
        //        XmlNode node = doc.DocumentElement.SelectSingleNode(
        //            "ThingDef[defName='" + def.defName + "']"
        //        );
        //        if (node != null)
        //        {
        //            XmlComment comment = doc.CreateComment(node.OuterXml);
        //            XmlNode parent = node.ParentNode;
        //            parent.ReplaceChild(comment, node);
        //            doc.Save(def.file);
        //            def.disabled = true;
        //            Console.WriteLine("'" + def.defName + "' disabled.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("'" + def.defName + "' not found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}

        //public void EnableNode(Def def)
        //{
        //    var doc = new XmlDocument();
        //    doc.Load(def.file);
        //    foreach (XmlNode comment in doc.SelectNodes("//comment()"))
        //    {
        //        string xml = comment.InnerText;
        //        if (!xml.StartsWith("<"))
        //            xml = "<" + xml + ">";
        //        XmlReader nodeReader = XmlReader.Create(new StringReader(xml));
        //        XmlNode newNode = doc.ReadNode(nodeReader);
        //        Console.WriteLine("enable: " + newNode.OuterXml);

        //        XmlNode defName = newNode.SelectSingleNode("defName");
        //        if (defName != null && defName.InnerText == def.defName)
        //        {
        //            XmlNode parent = comment.ParentNode;
        //            parent.ReplaceChild(newNode, comment);
        //            doc.Save(def.file);
        //            def.disabled = false;
        //            Console.WriteLine("'" + def.defName + "' enabled.");
        //            break;
        //        }
        //    }
        //}
    }
}
