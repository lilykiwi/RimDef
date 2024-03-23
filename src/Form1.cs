using SimpleSearch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RimDef
{
    public partial class Form1 : Form
    {
        private readonly XMLReader xmlReader = new XMLReader();
        private readonly ListView lwDetails = new ListView();

        List<Def> defs = new List<Def>();
        List<Def> defsView = new List<Def>();

        // FIXME: nullable properties
        public Form1()
        {
            InitializeComponent();

            txtModDir.Text = @"C:\Games\RimWorld";

            lwRecipe.Columns.Add("amount", 50);
            lwRecipe.Columns.Add("ingredient", 200);
            lwRecipe.Columns.Add("products", 100);

            //
            // lwDetail
            //
            lwDetails.GridLines = true;
            lwDetails.HeaderStyle = ColumnHeaderStyle.None;
            lwDetails.HideSelection = false;
            //lwDetails.Location = new Point(440, 440);
            lwDetails.Name = "lwDetail";
            lwDetails.TabIndex = 4;
            lwDetails.UseCompatibleStateImageBehavior = false;
            lwDetails.View = View.Details;
            //lwDetails.Visible = false;

            lwDetails.Columns.Add("key", 150);
            lwDetails.Columns.Add("value", 150);

            //Controls.Add(lwDetails);
        }

        private void LoadModList(string rimDir)
        {
            defs.Clear();
            lbMods.Items.Clear();
            lbDefTypes.DataSource = null;
            lwDefs.Items.Clear();
            xmlView.Clear();
            //gbDesc.Visible = false;
            //gbRecipe.Visible = false;
            //pictureBox1.Visible = false;

            List<Mod> modVersions;
            // TODO: add this to comboBox
            string[] versionNames = { "1.0", "1.1", "1.2", "1.3", "1.4", "1.5" };

            // FIXME: what the fuck.
            try
            {
                List<string> activeMods = xmlReader.ReadModConfig();

                // workshop* folder
                // TODO: merge this into main search, use ../../workshop/content/294100
                if (rimDir.Contains("294100")) // steam version
                {
                    xmlReader.modDir = rimDir;

                    foreach (string dir in Directory.GetDirectories(rimDir))
                    {
                        //Dictionary<string, string> defdirsTmp = new Dictionary<string, string>();
                        //Tuple<string, string> latest = null;

                        if (cbOnlyActiveMods.Checked)
                        {
                            string packageId = xmlReader.ReadPackageId(dir + @"/About/About.xml");
                            if (!activeMods.Contains(packageId))
                                continue;
                        }
                        string modName = xmlReader.ReadModName(dir + @"/About/About.xml");

                        Mod mod = new Mod(modName, "_", "1.4", dir, dir + @"/Defs/");
                        lbMods.Items.Add(mod);
                    }
                }
                else // non-steam version
                {
                    Mod core = new Mod(
                        "Core",
                        "_",
                        "_",
                        rimDir + @"/Data/Core/",
                        rimDir + @"/Data/Core/Defs/"
                    );
                    lbMods.Items.Add(core);

                    string modDir = rimDir + @"/Mods/";
                    xmlReader.modDir = modDir;

                    foreach (string dir in Directory.GetDirectories(modDir))
                    {
                        modVersions = new List<Mod>();
                        Mod latest = null;

                        string packageId = xmlReader.ReadPackageId(dir + @"/About/About.xml");
                        if (cbOnlyActiveMods.Checked)
                        {
                            if (!activeMods.Contains(packageId))
                                continue;
                        }

                        string modName = xmlReader.ReadModName(dir + @"/About/About.xml");

                        string path = dir + @"/Defs/";
                        if (Directory.Exists(path))
                        {
                            Mod mod = new Mod(modName, "_", packageId, dir, path);
                            modVersions.Add(mod);
                            latest = mod;
                        }

                        foreach (string ver in versionNames)
                        {
                            path = dir + "/" + ver + @"/Defs/";
                            if (Directory.Exists(path))
                            {
                                Mod mod = new Mod(modName, ver, packageId, dir, path);
                                modVersions.Add(mod);
                                latest = mod;
                            }
                        }

                        // TODO: implement comboBox for versions
                        //if (cbLatestVersion.Checked && latest != null)
                        //{
                        //    lbMods.Items.Add(latest);
                        //}
                        //else
                        //{
                        foreach (Mod m in modVersions)
                        {
                            lbMods.Items.Add(m);
                        }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading modlist: " + ex);
            }
        }

        // TODO: optimise
        private void LbMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            // reading all defs from selected mod
            Mod mod = (Mod)lbMods.SelectedItem;
            defs = xmlReader.LoadAllDefs(mod);
            defsView = defs;

            lwDefs.Items.Clear();
            lwDefs.Columns.Clear();
            lbDefTypes.DataSource = null;
            xmlView.Clear();
            //gbDesc.Visible = false;
            //gbRecipe.Visible = false;
            //pictureBox1.Visible = false;
            lwDetails.Items.Clear();
            lwRecipe.Items.Clear();

            lwDefs.Columns.Add("Type", 100);
            lwDefs.Columns.Add("Name", 120);
            lwDefs.Columns.Add("Label", 150);

            xmlReader.defTypes.Sort();
            lbDefTypes.DataSource = xmlReader.defTypes;
        }

        // Filter defs from loaded mod ListBox
        private void LbDefTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDefTypes.SelectedIndices.Count > 0)
            {
                string selectedType = xmlReader.defTypes[lbDefTypes.SelectedIndices[0]];
                lwDefs.Items.Clear();

                defsView = new List<Def>();
                foreach (Def def in defs)
                {
                    Console.WriteLine(def.defType + "def" + " " + selectedType.ToLower());
                    Console.WriteLine(def.defType + "def" == selectedType.ToLower());
                    if (def.defType + "def" == selectedType.ToLower())
                    {
                        defsView.Add(def);
                        string[] items = { def.defType, def.defName, def.label };
                        var listViewItem = new ListViewItem(items);
                        lwDefs.Items.Add(listViewItem);
                    }
                }
            }
        }

        // FIXME: what the fuck
        private void LwDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lwDefs.SelectedIndices.Count > 0)
            {
                Def def = defsView[lwDefs.SelectedIndices[0]];

                //gbRecipe.Visible = false;
                //gbDesc.Visible = false;
                //pictureBox1.Visible = false;
                //lwDetails.Visible = false;
                //cbDisable.Visible = false;

                lblPath.Text = def.file; //.Substring(def.file.IndexOf("/1."));
                xmlView.Text = def.xml;

                if (def.defType.ToLower() == "recipedef")
                {
                    RecipeDef recipe = (RecipeDef)def;

                    lwRecipe.Items.Clear();

                    foreach (string[] li in recipe.ingredients)
                    {
                        lwRecipe.Items.Add(new ListViewItem(li));
                    }

                    lwDetails.Items.Clear();
                    lwDetails.Items.Add(
                        new ListViewItem(new string[] { "Work amount", recipe.work })
                    );
                    lwDetails.Items.Add(
                        new ListViewItem(new string[] { "Skill requirements", recipe.skill })
                    );
                    lwDetails.Items.Add(
                        new ListViewItem(new string[] { "Research prerequisite", recipe.research })
                    );

                    //lwDetails.Size = new System.Drawing.Size(360, 60);
                    //lwDetails.Visible = true;
                    //gbRecipe.Visible = true;
                }

                if (def.defType.ToLower() == "thingdef")
                {
                    // Details
                    lwDetails.Items.Clear();
                    foreach (string[] row in def.details)
                    {
                        lwDetails.Items.Add(new ListViewItem(row));
                    }
                    if (lwDetails.Items.Count > 0)
                    {
                        //lwDetails.Size = new System.Drawing.Size(360, 110);
                        //lwDetails.Visible = true;
                    }

                    // FIXME: SRP me please
                    // Texture
                    Console.WriteLine("texture path = " + def.texture);
                    Bitmap image = new Bitmap(Properties.Resources.nopic);
                    if (File.Exists(def.texture))
                    {
                        try
                        {
                            image = new Bitmap(def.texture);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    pictureBox1.Image = (Image)image;
                    //pictureBox1.Visible = true;
                    pictureBox1.Refresh();

                    //cbDisable.Visible = true;
                    //cbDisable.Checked = def.disabled;
                }

                // Description
                if (def.description != "")
                {
                    thingDesc.Text = def.description;
                    //gbDesc.Visible = true;
                }
            }
        }

        // TODO: swap to OpenFileDialog or something
        private void BtnFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtModDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadModList(txtModDir.Text);
        }

        private SearchCore SearchCore { get; set; }

        // TODO: see about making this auto search
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        // FIXME: ignores Def type filter set in lbDefTypes
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchers = new List<Searcher> { new DefSearcher(defs) };
            SearchCore = new SearchCore(searchers);

            lwDefs.Items.Clear();
            lwDefs.Columns.Clear();
            lwDefs.Columns.Add("Mod", 150);
            lwDefs.Columns.Add("Type", 150);
            lwDefs.Columns.Add("Name", 150);
            lwDefs.Columns.Add("Label", 150);

            string searchText = txtSearch.Text;
            Console.WriteLine(searchText);

            var model = new SearchResponse();
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            model.Results = SearchCore.Search(searchText);
            s.Stop();
            model.TimeTaken = s.Elapsed;

            defsView.Clear();

            foreach (SearchResult result in model.Results)
            {
                Def def = result.Definition;
                string[] items = { def.mod.name, def.defType, def.defName, def.label };
                var listViewItem = new ListViewItem(items);
                lwDefs.Items.Add(listViewItem);
                defsView.Add(def);
            }
        }
    }
}
