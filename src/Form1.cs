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

    public readonly static string[] versionNames = { "Version", "All", "1.0", "1.1", "1.2", "1.3", "1.4", "1.5" };

    private SearchCore? SearchCore;

    public Form1()
    {
      InitializeComponent();

      txtModDir.Text = @"C:\Games\RimWorld";

      string appdataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      if (File.Exists(Path.Combine(appdataDir, "RimDef", "config.txt")))
      {
        txtModDir.Text = File.ReadAllText(Path.Combine(appdataDir, "RimDef", "config.txt"));
        LoadDir();
      }

      lwRecipe.Columns.Add("amount", 50);
      lwRecipe.Columns.Add("ingredient", 200);
      lwRecipe.Columns.Add("products", 100);

      //
      // lwDetail
      //
      lwDetails.GridLines = true;
      lwDetails.HeaderStyle = ColumnHeaderStyle.None;
      lwDetails.HideSelection = false;
      lwDetails.Name = "lwDetail";
      lwDetails.TabIndex = 4;
      lwDetails.UseCompatibleStateImageBehavior = false;
      lwDetails.View = View.Details;

      lwDetails.Columns.Add("key", 150);
      lwDetails.Columns.Add("value", 150);

      //Controls.Add(lwDetails);
      foreach (string version in versionNames)
      {
        versionComboBox.Items.Add(version);
      }
    }

    private void LoadModList(string rimDir)
    {
      defs.Clear();
      lbMods.Items.Clear();
      lbDefTypes.DataSource = null;
      lwDefs.Items.Clear();
      xmlView.Clear();
      //gbDesc.Text = "";
      thingDesc.Text = "";
      //gbRecipe.Visible = false;
      //pictureBox1.Visible = false;

      List<string> activeMods = xmlReader.ReadModConfig();

      foreach (Mod mod in xmlReader.GetMods(
          Path.Combine(rimDir, "Data"),
          versionComboBox.Text,
          core: true
      ))
        lbMods.Items.Add(mod);

      // we want to get mods from workshop (if it exists) and from local mod dir
      string workshopPath = Path.GetFullPath(Path.Combine(rimDir, "../../workshop/content/294100"));
      string localModPaths = Path.Combine(rimDir, "Mods");

      if (Directory.Exists(localModPaths))
        foreach (Mod mod in xmlReader.GetMods(localModPaths, versionComboBox.Text))
        {
          if (cbOnlyActiveMods.Checked && !activeMods.Contains(mod.packageId))
            continue;
          lbMods.Items.Add(mod);
        }

      // could put this in the function but its working i wanna get the commit out
      if (Directory.Exists(workshopPath))
        foreach (Mod mod in xmlReader.GetMods(workshopPath, versionComboBox.Text))
        {
          //Console.WriteLine(mod.packageId);
          if (cbOnlyActiveMods.Checked && !activeMods.Contains(mod.packageId))
            continue;
          lbMods.Items.Add(mod);
        }
    }

    // TODO: optimise
    private void LbMods_SelectedIndexChanged(object sender, EventArgs e)
    {
      // reading all defs from selected mod
      Mod mod = (Mod)lbMods.SelectedItem;
      xmlReader.defTypes.Clear();
      defs = xmlReader.LoadAllDefs(mod);
      defsView = defs;

      lwDefs.Items.Clear();
      lwDefs.Columns.Clear();
      lbDefTypes.DataSource = null;
      xmlView.Clear();
      thingDesc.Text = "";
      //gbDesc.Visible = false;
      //gbRecipe.Visible = false;
      //pictureBox1.Visible = false;
      lwDetails.Items.Clear();
      lwRecipe.Items.Clear();
      textBoxPath.Text = "";

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
          //Console.WriteLine(def.defType + "def" + " " + selectedType.ToLower());
          //Console.WriteLine(def.defType + "def" == selectedType.ToLower());
          if (def.defType + "def" == selectedType.ToLower())
          {
            defsView.Add(def);
            string[] items = { def.defType, def.defName, def.label };
            var listViewItem = new ListViewItem(items);
            lwDefs.Items.Add(listViewItem);
          }
        }
      }
      thingDesc.Text = "";
      xmlView.Text = "";
      lwDetails.Items.Clear();
      lwRecipe.Items.Clear();
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

        //lblPath.Text = def.file;
        textBoxPath.Text = def.file;
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

        // TODO: SRP me please
        // Texture
        Console.WriteLine("texture path = " + def.texture);
        Bitmap image = new Bitmap(100, 100);
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
        pictureBox1.Image = image;
        pictureBox1.Refresh();

        // Description
        if (def.description != "")
        {
          thingDesc.Text = def.description;
          //gbDesc.Visible = true;
        }
      }
      else
      {
        // nothing is selected, that's okay, we can clear all the boxes that have bad info
        thingDesc.Text = "";
        xmlView.Text = "";
        lwDetails.Items.Clear();
        lwRecipe.Items.Clear();
      }
    }

    // TODO: swap to OpenFileDialog or something
    private void BtnFolder_Click(object sender, EventArgs e)
    {
      DialogResult result = folderBrowserDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        txtModDir.Text = folderBrowserDialog1.SelectedPath;
        LoadDir();
      }
    }

    private void BtnLoad_Click(object sender, EventArgs e)
    {
      LoadDir();
    }

    private void LoadDir()
    {
      if (versionComboBox.Text != "All")
      {
        versionComboBox.Text = xmlReader.GetCoreVersion(txtModDir.Text);
      }
      LoadModList(txtModDir.Text);

      string appdataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      if (!Directory.Exists(Path.Combine(appdataDir, "RimDef")))
        Directory.CreateDirectory(Path.Combine(appdataDir, "RimDef"));

      File.WriteAllText(Path.Combine(appdataDir, "RimDef", "config.txt"), txtModDir.Text);

    }


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
      //Console.WriteLine(searchText);

      var model = new SearchResponse();
      var s = new System.Diagnostics.Stopwatch();
      s.Start();
      model.Results = SearchCore.Search(searchText);
      s.Stop();
      model.TimeTaken = s.Elapsed;

      defsView.Clear();

      foreach (SearchResult result in model.Results)
      {
        if (result.Definition == null)
          continue;
        Def def = result.Definition;
        string[] items = { def.mod.name, def.defType, def.defName, def.label };
        var listViewItem = new ListViewItem(items);
        lwDefs.Items.Add(listViewItem);
        defsView.Add(def);
      }
    }

    private void gbRecipe_Enter(object sender, EventArgs e) { }
  }
}
