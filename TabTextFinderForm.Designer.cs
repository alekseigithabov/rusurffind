namespace TabTextFinder
{
    partial class TabTextFinderForm
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
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.split = new System.Windows.Forms.SplitContainer();
            this.imgFile = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelExcludeFile = new System.Windows.Forms.Label();
            this.stripStatus = new System.Windows.Forms.StatusStrip();
            this.labelCase = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelWord = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelRegx = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelLines = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelBytes = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelChars = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelElapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelEsc = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgFind = new System.Windows.Forms.ToolStripProgressBar();
            this.labelCPU = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelMemory = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage = new System.Windows.Forms.TabPage();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.chkWord = new System.Windows.Forms.CheckBox();
            this.chkRecursive = new System.Windows.Forms.CheckBox();
            this.chkRegx = new System.Windows.Forms.CheckBox();
            this.chkCase = new System.Windows.Forms.CheckBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.labelRoot = new System.Windows.Forms.Label();
            this.imgRoot = new System.Windows.Forms.Label();
            this.imgText = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnFindIncl = new System.Windows.Forms.Button();
            this.btnFindExcl = new System.Windows.Forms.Button();
            this.btnFindFound = new System.Windows.Forms.Button();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.btnRecent = new System.Windows.Forms.Button();
            this.cmbFile = new TabTextFinder.UIControl.TextComboBox();
            this.cmbExclude = new TabTextFinder.UIControl.TextComboBox();
            this.tabFinds = new TabTextFinder.UIControl.FindTabControl();
            this.tabHelp = new System.Windows.Forms.TabPage();
            this.richHelp = new System.Windows.Forms.RichTextBox();
            this.cmbText = new TabTextFinder.UIControl.TextComboBox();
            this.cmbRoot = new TabTextFinder.UIControl.TextComboBox();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.stripStatus.SuspendLayout();
            this.tabFinds.SuspendLayout();
            this.tabHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split.IsSplitterFixed = true;
            this.split.Location = new System.Drawing.Point(0, 34);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.imgFile);
            this.split.Panel1.Controls.Add(this.labelFile);
            this.split.Panel1.Controls.Add(this.cmbFile);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.cmbExclude);
            this.split.Panel2.Controls.Add(this.labelExcludeFile);
            this.split.Size = new System.Drawing.Size(404, 30);
            this.split.SplitterDistance = 236;
            this.split.TabIndex = 6;
            // 
            // imgFile
            // 
            this.imgFile.Image = global::TabTextFinder.Properties.Resources.Img_documents_stack;
            this.imgFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgFile.Location = new System.Drawing.Point(6, 0);
            this.imgFile.Name = "imgFile";
            this.imgFile.Size = new System.Drawing.Size(21, 26);
            this.imgFile.TabIndex = 0;
            // 
            // labelFile
            // 
            this.labelFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFile.Location = new System.Drawing.Point(28, 0);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(60, 26);
            this.labelFile.TabIndex = 1;
            this.labelFile.Text = "Файлы";
            this.labelFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelExcludeFile
            // 
            this.labelExcludeFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelExcludeFile.Location = new System.Drawing.Point(0, 0);
            this.labelExcludeFile.Name = "labelExcludeFile";
            this.labelExcludeFile.Size = new System.Drawing.Size(64, 26);
            this.labelExcludeFile.TabIndex = 0;
            this.labelExcludeFile.Text = "&EXCLUDE";
            this.labelExcludeFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stripStatus
            // 
            this.stripStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.stripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelCase,
            this.labelWord,
            this.labelRegx,
            this.labelFiles,
            this.labelLines,
            this.labelBytes,
            this.labelChars,
            this.labelElapsed,
            this.labelSpring,
            this.labelEsc,
            this.prgFind,
            this.labelCPU,
            this.labelMemory});
            this.stripStatus.Location = new System.Drawing.Point(0, 432);
            this.stripStatus.Name = "stripStatus";
            this.stripStatus.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.stripStatus.ShowItemToolTips = true;
            this.stripStatus.Size = new System.Drawing.Size(704, 25);
            this.stripStatus.SizingGrip = false;
            this.stripStatus.TabIndex = 20;
            // 
            // labelCase
            // 
            this.labelCase.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelCase.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelCase.Image = global::TabTextFinder.Properties.Resources.Img_edit_lowercase;
            this.labelCase.Name = "labelCase";
            this.labelCase.Size = new System.Drawing.Size(20, 20);
            // 
            // labelWord
            // 
            this.labelWord.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelWord.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelWord.Image = global::TabTextFinder.Properties.Resources.Img_spell_check;
            this.labelWord.Name = "labelWord";
            this.labelWord.Size = new System.Drawing.Size(20, 20);
            // 
            // labelRegx
            // 
            this.labelRegx.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelRegx.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelRegx.Image = global::TabTextFinder.Properties.Resources.Img_asterisk;
            this.labelRegx.Name = "labelRegx";
            this.labelRegx.Size = new System.Drawing.Size(20, 20);
            // 
            // labelFiles
            // 
            this.labelFiles.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelFiles.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelFiles.Image = global::TabTextFinder.Properties.Resources.Img_documents_stack;
            this.labelFiles.Name = "labelFiles";
            this.labelFiles.Size = new System.Drawing.Size(20, 20);
            // 
            // labelLines
            // 
            this.labelLines.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelLines.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelLines.Image = global::TabTextFinder.Properties.Resources.Img_edit_rule;
            this.labelLines.Name = "labelLines";
            this.labelLines.Size = new System.Drawing.Size(20, 20);
            // 
            // labelBytes
            // 
            this.labelBytes.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelBytes.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelBytes.Image = global::TabTextFinder.Properties.Resources.Img_gear;
            this.labelBytes.Name = "labelBytes";
            this.labelBytes.Size = new System.Drawing.Size(20, 20);
            // 
            // labelChars
            // 
            this.labelChars.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelChars.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelChars.Image = global::TabTextFinder.Properties.Resources.Img_edit_shadow;
            this.labelChars.Name = "labelChars";
            this.labelChars.Size = new System.Drawing.Size(20, 20);
            // 
            // labelElapsed
            // 
            this.labelElapsed.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelElapsed.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelElapsed.Image = global::TabTextFinder.Properties.Resources.Img_clock;
            this.labelElapsed.Name = "labelElapsed";
            this.labelElapsed.Size = new System.Drawing.Size(20, 20);
            // 
            // labelSpring
            // 
            this.labelSpring.Name = "labelSpring";
            this.labelSpring.Size = new System.Drawing.Size(493, 20);
            this.labelSpring.Spring = true;
            // 
            // labelEsc
            // 
            this.labelEsc.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelEsc.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelEsc.Image = global::TabTextFinder.Properties.Resources.Img_keyboard;
            this.labelEsc.Name = "labelEsc";
            this.labelEsc.Size = new System.Drawing.Size(20, 20);
            this.labelEsc.Visible = false;
            // 
            // prgFind
            // 
            this.prgFind.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.prgFind.AutoSize = false;
            this.prgFind.Name = "prgFind";
            this.prgFind.Size = new System.Drawing.Size(70, 19);
            this.prgFind.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgFind.Visible = false;
            // 
            // labelCPU
            // 
            this.labelCPU.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelCPU.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelCPU.Image = global::TabTextFinder.Properties.Resources.Img_processor;
            this.labelCPU.Name = "labelCPU";
            this.labelCPU.Size = new System.Drawing.Size(20, 20);
            // 
            // labelMemory
            // 
            this.labelMemory.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelMemory.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelMemory.Image = global::TabTextFinder.Properties.Resources.Img_system_monitor;
            this.labelMemory.Name = "labelMemory";
            this.labelMemory.Size = new System.Drawing.Size(20, 20);
            this.labelMemory.Click += new System.EventHandler(this.labelMemory_Click);
            // 
            // tabPage
            // 
            this.tabPage.Location = new System.Drawing.Point(4, 21);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(626, 336);
            this.tabPage.TabIndex = 0;
            this.tabPage.Text = "Welcome !";
            this.tabPage.UseVisualStyleBackColor = true;
            // 
            // chkWord
            // 
            this.chkWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWord.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkWord.Image = global::TabTextFinder.Properties.Resources.Img_spell_check;
            this.chkWord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkWord.Location = new System.Drawing.Point(461, 66);
            this.chkWord.Name = "chkWord";
            this.chkWord.Size = new System.Drawing.Size(72, 28);
            this.chkWord.TabIndex = 13;
            this.chkWord.Text = "Слово";
            this.chkWord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkWord.UseVisualStyleBackColor = true;
            // 
            // chkRecursive
            // 
            this.chkRecursive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRecursive.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRecursive.Checked = true;
            this.chkRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecursive.Image = global::TabTextFinder.Properties.Resources.Img_folders_stack;
            this.chkRecursive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRecursive.Location = new System.Drawing.Point(408, 34);
            this.chkRecursive.Name = "chkRecursive";
            this.chkRecursive.Size = new System.Drawing.Size(96, 28);
            this.chkRecursive.TabIndex = 7;
            this.chkRecursive.Text = "Рекурсия";
            this.chkRecursive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkRecursive.UseVisualStyleBackColor = true;
            // 
            // chkRegx
            // 
            this.chkRegx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRegx.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRegx.Image = global::TabTextFinder.Properties.Resources.Img_asterisk;
            this.chkRegx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRegx.Location = new System.Drawing.Point(539, 65);
            this.chkRegx.Name = "chkRegx";
            this.chkRegx.Size = new System.Drawing.Size(83, 28);
            this.chkRegx.TabIndex = 14;
            this.chkRegx.Text = "Рег выр";
            this.chkRegx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkRegx.UseVisualStyleBackColor = true;
            // 
            // chkCase
            // 
            this.chkCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCase.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCase.Checked = true;
            this.chkCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCase.Image = global::TabTextFinder.Properties.Resources.Img_edit_lowercase;
            this.chkCase.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkCase.Location = new System.Drawing.Point(380, 66);
            this.chkCase.Name = "chkCase";
            this.chkCase.Size = new System.Drawing.Size(75, 28);
            this.chkCase.TabIndex = 12;
            this.chkCase.Text = "&Регистр";
            this.chkCase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkCase.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Image = global::TabTextFinder.Properties.Resources.Img_Find;
            this.btnFind.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFind.Location = new System.Drawing.Point(628, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 63);
            this.btnFind.TabIndex = 15;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Image = global::TabTextFinder.Properties.Resources.Img_folder_horizontal_open;
            this.btnBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowse.Location = new System.Drawing.Point(393, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(72, 28);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Обзор";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // labelText
            // 
            this.labelText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelText.Location = new System.Drawing.Point(28, 64);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(60, 26);
            this.labelText.TabIndex = 10;
            this.labelText.Text = "Текст";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRoot
            // 
            this.labelRoot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelRoot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRoot.Location = new System.Drawing.Point(28, 4);
            this.labelRoot.Name = "labelRoot";
            this.labelRoot.Size = new System.Drawing.Size(60, 26);
            this.labelRoot.TabIndex = 1;
            this.labelRoot.Text = "Папки";
            this.labelRoot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgRoot
            // 
            this.imgRoot.Image = global::TabTextFinder.Properties.Resources.Img_folder__arrow;
            this.imgRoot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgRoot.Location = new System.Drawing.Point(6, 4);
            this.imgRoot.Name = "imgRoot";
            this.imgRoot.Size = new System.Drawing.Size(21, 26);
            this.imgRoot.TabIndex = 0;
            // 
            // imgText
            // 
            this.imgText.Image = global::TabTextFinder.Properties.Resources.Img_edit;
            this.imgText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgText.Location = new System.Drawing.Point(6, 64);
            this.imgText.Name = "imgText";
            this.imgText.Size = new System.Drawing.Size(21, 26);
            this.imgText.TabIndex = 9;
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.Image = global::TabTextFinder.Properties.Resources.Img_wrench_screwdriver;
            this.btnConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConfig.Location = new System.Drawing.Point(560, 4);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(64, 28);
            this.btnConfig.TabIndex = 5;
            this.btnConfig.Text = "&Опц";
            this.btnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnFindIncl
            // 
            this.btnFindIncl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindIncl.Image = global::TabTextFinder.Properties.Resources.Img_plus_white;
            this.btnFindIncl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFindIncl.Location = new System.Drawing.Point(628, 68);
            this.btnFindIncl.Name = "btnFindIncl";
            this.btnFindIncl.Size = new System.Drawing.Size(23, 24);
            this.btnFindIncl.TabIndex = 16;
            this.btnFindIncl.UseVisualStyleBackColor = true;
            this.btnFindIncl.Click += new System.EventHandler(this.btnFindIncl_Click);
            // 
            // btnFindExcl
            // 
            this.btnFindExcl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindExcl.Image = global::TabTextFinder.Properties.Resources.Img_minus_white;
            this.btnFindExcl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFindExcl.Location = new System.Drawing.Point(653, 68);
            this.btnFindExcl.Name = "btnFindExcl";
            this.btnFindExcl.Size = new System.Drawing.Size(23, 24);
            this.btnFindExcl.TabIndex = 17;
            this.btnFindExcl.UseVisualStyleBackColor = true;
            this.btnFindExcl.Click += new System.EventHandler(this.btnFindExcl_Click);
            // 
            // btnFindFound
            // 
            this.btnFindFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindFound.Image = global::TabTextFinder.Properties.Resources.Img_document_search_result;
            this.btnFindFound.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFindFound.Location = new System.Drawing.Point(678, 68);
            this.btnFindFound.Name = "btnFindFound";
            this.btnFindFound.Size = new System.Drawing.Size(23, 24);
            this.btnFindFound.TabIndex = 18;
            this.btnFindFound.UseVisualStyleBackColor = true;
            this.btnFindFound.Click += new System.EventHandler(this.btnFindFound_Click);
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.DropDownWidth = 100;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.ItemHeight = 15;
            this.cmbEncoding.Location = new System.Drawing.Point(508, 36);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(115, 23);
            this.cmbEncoding.TabIndex = 8;
            // 
            // btnRecent
            // 
            this.btnRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRecent.Image = global::TabTextFinder.Properties.Resources.Img_database;
            this.btnRecent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRecent.Location = new System.Drawing.Point(471, 4);
            this.btnRecent.Name = "btnRecent";
            this.btnRecent.Size = new System.Drawing.Size(90, 28);
            this.btnRecent.TabIndex = 4;
            this.btnRecent.Text = "Недавние";
            this.btnRecent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRecent.UseVisualStyleBackColor = true;
            this.btnRecent.Click += new System.EventHandler(this.btnRecent_Click);
            // 
            // cmbFile
            // 
            this.cmbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFile.FormattingEnabled = true;
            this.cmbFile.Location = new System.Drawing.Point(88, 2);
            this.cmbFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbFile.MaxDropDownItems = 24;
            this.cmbFile.Name = "cmbFile";
            this.cmbFile.Size = new System.Drawing.Size(144, 23);
            this.cmbFile.TabIndex = 2;
            // 
            // cmbExclude
            // 
            this.cmbExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExclude.FormattingEnabled = true;
            this.cmbExclude.Location = new System.Drawing.Point(64, 2);
            this.cmbExclude.Name = "cmbExclude";
            this.cmbExclude.Size = new System.Drawing.Size(97, 23);
            this.cmbExclude.TabIndex = 1;
            // 
            // tabFinds
            // 
            this.tabFinds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFinds.Controls.Add(this.tabHelp);
            this.tabFinds.Location = new System.Drawing.Point(0, 96);
            this.tabFinds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabFinds.Name = "tabFinds";
            this.tabFinds.SelectedIndex = 0;
            this.tabFinds.Size = new System.Drawing.Size(706, 335);
            this.tabFinds.TabIndex = 19;
            this.tabFinds.SelectedIndexChanged += new System.EventHandler(this.tabFinds_SelectedIndexChanged);
            // 
            // tabHelp
            // 
            this.tabHelp.Controls.Add(this.richHelp);
            this.tabHelp.ImageIndex = 4;
            this.tabHelp.Location = new System.Drawing.Point(4, 24);
            this.tabHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabHelp.Name = "tabHelp";
            this.tabHelp.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabHelp.Size = new System.Drawing.Size(698, 307);
            this.tabHelp.TabIndex = 0;
            this.tabHelp.Text = "HELP";
            this.tabHelp.UseVisualStyleBackColor = true;
            // 
            // richHelp
            // 
            this.richHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richHelp.BackColor = System.Drawing.Color.White;
            this.richHelp.DetectUrls = false;
            this.richHelp.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.richHelp.Location = new System.Drawing.Point(0, 0);
            this.richHelp.Name = "richHelp";
            this.richHelp.ReadOnly = true;
            this.richHelp.Size = new System.Drawing.Size(699, 305);
            this.richHelp.TabIndex = 0;
            this.richHelp.Text = "";
            // 
            // cmbText
            // 
            this.cmbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbText.Location = new System.Drawing.Point(88, 66);
            this.cmbText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbText.MaxDropDownItems = 24;
            this.cmbText.Name = "cmbText";
            this.cmbText.Size = new System.Drawing.Size(286, 23);
            this.cmbText.TabIndex = 11;
            // 
            // cmbRoot
            // 
            this.cmbRoot.AllowDrop = true;
            this.cmbRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRoot.FormattingEnabled = true;
            this.cmbRoot.Location = new System.Drawing.Point(88, 6);
            this.cmbRoot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbRoot.MaxDropDownItems = 24;
            this.cmbRoot.Name = "cmbRoot";
            this.cmbRoot.Size = new System.Drawing.Size(299, 23);
            this.cmbRoot.TabIndex = 2;
            this.cmbRoot.DragDrop += new System.Windows.Forms.DragEventHandler(this.cmbRoot_DragDrop);
            this.cmbRoot.DragEnter += new System.Windows.Forms.DragEventHandler(this.cmbRoot_DragEnter);
            // 
            // TabTextFinderForm
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 457);
            this.Controls.Add(this.btnRecent);
            this.Controls.Add(this.cmbEncoding);
            this.Controls.Add(this.btnFindFound);
            this.Controls.Add(this.btnFindExcl);
            this.Controls.Add(this.btnFindIncl);
            this.Controls.Add(this.split);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.chkWord);
            this.Controls.Add(this.imgText);
            this.Controls.Add(this.imgRoot);
            this.Controls.Add(this.chkRegx);
            this.Controls.Add(this.chkCase);
            this.Controls.Add(this.chkRecursive);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tabFinds);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.cmbText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.labelRoot);
            this.Controls.Add(this.cmbRoot);
            this.Controls.Add(this.stripStatus);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "TabTextFinderForm";
            this.Text = "ruSURF FIND  v1.77";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            this.stripStatus.ResumeLayout(false);
            this.stripStatus.PerformLayout();
            this.tabFinds.ResumeLayout(false);
            this.tabHelp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stripStatus;
        private System.Windows.Forms.Label labelRoot;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Label labelText;
        private TabTextFinder.UIControl.TextComboBox cmbRoot;
        private TabTextFinder.UIControl.TextComboBox cmbFile;
        private TabTextFinder.UIControl.TextComboBox cmbText;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnFind;
        private TabTextFinder.UIControl.FindTabControl tabFinds;
        private System.Windows.Forms.CheckBox chkRecursive;
        private System.Windows.Forms.ToolStripStatusLabel labelFiles;
        private System.Windows.Forms.ToolStripStatusLabel labelLines;
        private System.Windows.Forms.ToolStripProgressBar prgFind;
        private System.Windows.Forms.ToolStripStatusLabel labelSpring;
        private System.Windows.Forms.ToolStripStatusLabel labelEsc;
        private System.Windows.Forms.ToolStripStatusLabel labelElapsed;
        private System.Windows.Forms.ToolStripStatusLabel labelBytes;
        private System.Windows.Forms.ToolStripStatusLabel labelChars;
        private System.Windows.Forms.ToolStripStatusLabel labelCPU;
        private System.Windows.Forms.TabPage tabPage;
        private System.Windows.Forms.TabPage tabHelp;
        private System.Windows.Forms.CheckBox chkCase;
        private System.Windows.Forms.CheckBox chkRegx;
        private System.Windows.Forms.ToolStripStatusLabel labelMemory;
        private System.Windows.Forms.RichTextBox richHelp;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label imgRoot;
        private System.Windows.Forms.Label imgFile;
        private System.Windows.Forms.Label imgText;
        private System.Windows.Forms.CheckBox chkWord;
        private System.Windows.Forms.ToolStripStatusLabel labelCase;
        private System.Windows.Forms.ToolStripStatusLabel labelWord;
        private System.Windows.Forms.ToolStripStatusLabel labelRegx;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Label labelExcludeFile;
        private TabTextFinder.UIControl.TextComboBox cmbExclude;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.Button btnFindIncl;
        private System.Windows.Forms.Button btnFindExcl;
        private System.Windows.Forms.Button btnFindFound;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Button btnRecent;

    }
}
