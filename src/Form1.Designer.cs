namespace RimDef
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lbMods = new System.Windows.Forms.ListBox();
      this.lbDefTypes = new System.Windows.Forms.ListBox();
      this.lwDefs = new System.Windows.Forms.ListView();
      this.lwRecipe = new System.Windows.Forms.ListView();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.btnFolder = new System.Windows.Forms.Button();
      this.txtModDir = new System.Windows.Forms.TextBox();
      this.btnLoad = new System.Windows.Forms.Button();
      this.txtSearch = new System.Windows.Forms.TextBox();
      this.btnSearch = new System.Windows.Forms.Button();
      this.thingDesc = new System.Windows.Forms.TextBox();
      this.xmlView = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.gbDesc = new System.Windows.Forms.GroupBox();
      this.gbRecipe = new System.Windows.Forms.GroupBox();
      this.cbOnlyActiveMods = new System.Windows.Forms.CheckBox();
      this.lblPath = new System.Windows.Forms.Label();
      this.splitContainerMain = new System.Windows.Forms.SplitContainer();
      this.versionComboBox = new System.Windows.Forms.ComboBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.gbDesc.SuspendLayout();
      this.gbRecipe.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
      this.splitContainerMain.Panel1.SuspendLayout();
      this.splitContainerMain.Panel2.SuspendLayout();
      this.splitContainerMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbMods
      // 
      this.lbMods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
      | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.lbMods.FormattingEnabled = true;
      this.lbMods.Location = new System.Drawing.Point(3, 18);
      this.lbMods.Name = "lbMods";
      this.lbMods.Size = new System.Drawing.Size(149, 264);
      this.lbMods.TabIndex = 0;
      this.lbMods.SelectedIndexChanged += new System.EventHandler(this.LbMods_SelectedIndexChanged);
      // 
      // lbDefTypes
      // 
      this.lbDefTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
      | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.lbDefTypes.FormattingEnabled = true;
      this.lbDefTypes.Location = new System.Drawing.Point(3, 18);
      this.lbDefTypes.Name = "lbDefTypes";
      this.lbDefTypes.Size = new System.Drawing.Size(153, 264);
      this.lbDefTypes.TabIndex = 1;
      this.lbDefTypes.SelectedIndexChanged += new System.EventHandler(this.LbDefTypes_SelectedIndexChanged);
      // 
      // lwDefs
      // 
      this.lwDefs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
      | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.lwDefs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lwDefs.FullRowSelect = true;
      this.lwDefs.GridLines = true;
      this.lwDefs.HideSelection = false;
      this.lwDefs.Location = new System.Drawing.Point(0, 18);
      this.lwDefs.Name = "lwDefs";
      this.lwDefs.Size = new System.Drawing.Size(318, 471);
      this.lwDefs.TabIndex = 2;
      this.lwDefs.UseCompatibleStateImageBehavior = false;
      this.lwDefs.View = System.Windows.Forms.View.Details;
      this.lwDefs.SelectedIndexChanged += new System.EventHandler(this.LwDefs_SelectedIndexChanged);
      // 
      // lwRecipe
      // 
      this.lwRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.lwRecipe.GridLines = true;
      this.lwRecipe.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lwRecipe.HideSelection = false;
      this.lwRecipe.Location = new System.Drawing.Point(0, 18);
      this.lwRecipe.Name = "lwRecipe";
      this.lwRecipe.Size = new System.Drawing.Size(279, 101);
      this.lwRecipe.TabIndex = 3;
      this.lwRecipe.UseCompatibleStateImageBehavior = false;
      this.lwRecipe.View = System.Windows.Forms.View.Details;
      // 
      // btnFolder
      // 
      this.btnFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFolder.Location = new System.Drawing.Point(235, 19);
      this.btnFolder.Name = "btnFolder";
      this.btnFolder.Size = new System.Drawing.Size(25, 23);
      this.btnFolder.TabIndex = 5;
      this.btnFolder.Text = "...";
      this.btnFolder.UseVisualStyleBackColor = true;
      this.btnFolder.Click += new System.EventHandler(this.BtnFolder_Click);
      // 
      // txtModDir
      // 
      this.txtModDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModDir.Location = new System.Drawing.Point(3, 20);
      this.txtModDir.Name = "txtModDir";
      this.txtModDir.Size = new System.Drawing.Size(137, 20);
      this.txtModDir.TabIndex = 6;
      this.txtModDir.Text = "path";
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(266, 19);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(50, 23);
      this.btnLoad.TabIndex = 7;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
      // 
      // txtSearch
      // 
      this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSearch.Location = new System.Drawing.Point(1, 494);
      this.txtSearch.Name = "txtSearch";
      this.txtSearch.Size = new System.Drawing.Size(262, 20);
      this.txtSearch.TabIndex = 8;
      this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
      // 
      // btnSearch
      // 
      this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSearch.Location = new System.Drawing.Point(266, 492);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new System.Drawing.Size(50, 23);
      this.btnSearch.TabIndex = 9;
      this.btnSearch.Text = "Search";
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
      // 
      // thingDesc
      // 
      this.thingDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.thingDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.thingDesc.Location = new System.Drawing.Point(1, 19);
      this.thingDesc.Multiline = true;
      this.thingDesc.Name = "thingDesc";
      this.thingDesc.ReadOnly = true;
      this.thingDesc.Size = new System.Drawing.Size(170, 88);
      this.thingDesc.TabIndex = 10;
      // 
      // xmlView
      // 
      this.xmlView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
      | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.xmlView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.xmlView.Location = new System.Drawing.Point(1, 19);
      this.xmlView.Multiline = true;
      this.xmlView.Name = "xmlView";
      this.xmlView.ReadOnly = true;
      this.xmlView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.xmlView.Size = new System.Drawing.Size(277, 596);
      this.xmlView.TabIndex = 11;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 2);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(33, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Mods";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 2);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(51, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "Def Type";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(74, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "Rimworld path";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 3);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 13);
      this.label4.TabIndex = 15;
      this.label4.Text = "XML";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(3, 2);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(29, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "Defs";
      // 
      // gbDesc
      // 
      this.gbDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.gbDesc.Controls.Add(this.thingDesc);
      this.gbDesc.Controls.Add(this.pictureBox1);
      this.gbDesc.Location = new System.Drawing.Point(1, 621);
      this.gbDesc.Name = "gbDesc";
      this.gbDesc.Size = new System.Drawing.Size(279, 109);
      this.gbDesc.TabIndex = 19;
      this.gbDesc.TabStop = false;
      this.gbDesc.Text = "Description";
      // 
      // gbRecipe
      // 
      this.gbRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.gbRecipe.Controls.Add(this.lwRecipe);
      this.gbRecipe.Location = new System.Drawing.Point(1, 736);
      this.gbRecipe.Name = "gbRecipe";
      this.gbRecipe.Size = new System.Drawing.Size(279, 119);
      this.gbRecipe.TabIndex = 20;
      this.gbRecipe.TabStop = false;
      this.gbRecipe.Text = "Ingredients";
      this.gbRecipe.Enter += new System.EventHandler(this.gbRecipe_Enter);
      // 
      // cbOnlyActiveMods
      // 
      this.cbOnlyActiveMods.AutoSize = true;
      this.cbOnlyActiveMods.Location = new System.Drawing.Point(82, 2);
      this.cbOnlyActiveMods.Margin = new System.Windows.Forms.Padding(2);
      this.cbOnlyActiveMods.Name = "cbOnlyActiveMods";
      this.cbOnlyActiveMods.Size = new System.Drawing.Size(105, 17);
      this.cbOnlyActiveMods.TabIndex = 21;
      this.cbOnlyActiveMods.Text = "only active mods";
      this.cbOnlyActiveMods.UseVisualStyleBackColor = true;
      // 
      // lblPath
      // 
      this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.lblPath.AutoSize = true;
      this.lblPath.Location = new System.Drawing.Point(474, 15);
      this.lblPath.Name = "lblPath";
      this.lblPath.Size = new System.Drawing.Size(0, 13);
      this.lblPath.TabIndex = 24;
      // 
      // splitContainerMain
      // 
      this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
      | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainerMain.Location = new System.Drawing.Point(12, 12);
      this.splitContainerMain.Name = "splitContainerMain";
      // 
      // splitContainerMain.Panel1
      // 
      this.splitContainerMain.Panel1.Controls.Add(this.splitContainer3);
      this.splitContainerMain.Panel1.Controls.Add(this.versionComboBox);
      this.splitContainerMain.Panel1.Controls.Add(this.txtModDir);
      this.splitContainerMain.Panel1.Controls.Add(this.btnFolder);
      this.splitContainerMain.Panel1.Controls.Add(this.cbOnlyActiveMods);
      this.splitContainerMain.Panel1.Controls.Add(this.btnLoad);
      this.splitContainerMain.Panel1.Controls.Add(this.label3);
      // 
      // splitContainerMain.Panel2
      // 
      this.splitContainerMain.Panel2.Controls.Add(this.label4);
      this.splitContainerMain.Panel2.Controls.Add(this.xmlView);
      this.splitContainerMain.Panel2.Controls.Add(this.gbDesc);
      this.splitContainerMain.Panel2.Controls.Add(this.gbRecipe);
      this.splitContainerMain.Size = new System.Drawing.Size(603, 856);
      this.splitContainerMain.SplitterDistance = 318;
      this.splitContainerMain.TabIndex = 25;
      // 
      // versionComboBox
      // 
      this.versionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.versionComboBox.FormattingEnabled = true;
      this.versionComboBox.Location = new System.Drawing.Point(146, 19);
      this.versionComboBox.Name = "versionComboBox";
      this.versionComboBox.Size = new System.Drawing.Size(83, 21);
      this.versionComboBox.TabIndex = 26;
      this.versionComboBox.Text = "Version";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox1.Image = global::RimDef.Properties.Resources.nopic;
      this.pictureBox1.Location = new System.Drawing.Point(178, 7);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(100, 100);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
      | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.lbMods);
      this.splitContainer2.Panel1.Controls.Add(this.label1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.lbDefTypes);
      this.splitContainer2.Panel2.Controls.Add(this.label2);
      this.splitContainer2.Size = new System.Drawing.Size(318, 289);
      this.splitContainer2.SplitterDistance = 155;
      this.splitContainer2.TabIndex = 26;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
      | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer3.Location = new System.Drawing.Point(0, 46);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.label5);
      this.splitContainer3.Panel2.Controls.Add(this.lwDefs);
      this.splitContainer3.Panel2.Controls.Add(this.txtSearch);
      this.splitContainer3.Panel2.Controls.Add(this.btnSearch);
      this.splitContainer3.Size = new System.Drawing.Size(318, 810);
      this.splitContainer3.SplitterDistance = 289;
      this.splitContainer3.TabIndex = 21;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(627, 880);
      this.Controls.Add(this.splitContainerMain);
      this.Controls.Add(this.lblPath);
      this.Name = "Form1";
      this.Text = "RimDef";
      this.gbDesc.ResumeLayout(false);
      this.gbDesc.PerformLayout();
      this.gbRecipe.ResumeLayout(false);
      this.splitContainerMain.Panel1.ResumeLayout(false);
      this.splitContainerMain.Panel1.PerformLayout();
      this.splitContainerMain.Panel2.ResumeLayout(false);
      this.splitContainerMain.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
      this.splitContainerMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox lbMods;
    private System.Windows.Forms.ListBox lbDefTypes;
    private System.Windows.Forms.ListView lwDefs;
    private System.Windows.Forms.ListView lwRecipe;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button btnFolder;
    private System.Windows.Forms.TextBox txtModDir;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.TextBox thingDesc;
    private System.Windows.Forms.TextBox xmlView;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.GroupBox gbDesc;
    private System.Windows.Forms.GroupBox gbRecipe;
    private System.Windows.Forms.CheckBox cbOnlyActiveMods;
    private System.Windows.Forms.Label lblPath;
    private System.Windows.Forms.SplitContainer splitContainerMain;
    private System.Windows.Forms.ComboBox versionComboBox;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.SplitContainer splitContainer3;
  }
}

