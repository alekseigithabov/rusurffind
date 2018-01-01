namespace TabTextFinder
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
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
            this.chkCacheFile = new System.Windows.Forms.CheckBox();
            this.labelCacheSize = new System.Windows.Forms.Label();
            this.labelCacheSizeUnit = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numCacheSize = new System.Windows.Forms.NumericUpDown();
            this.textEditorArgument = new System.Windows.Forms.TextBox();
            this.labelEditorDesc = new System.Windows.Forms.Label();
            this.textEditorCommand = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.labelCmd = new System.Windows.Forms.Label();
            this.labelArg = new System.Windows.Forms.Label();
            this.groupEditor = new System.Windows.Forms.GroupBox();
            this.groupCache = new System.Windows.Forms.GroupBox();
            this.labelCache = new System.Windows.Forms.Label();
            this.groupLayout = new System.Windows.Forms.GroupBox();
            this.btnLayoutHorz = new System.Windows.Forms.RadioButton();
            this.btnLayoutVert = new System.Windows.Forms.RadioButton();
            this.groupViewerWrap = new System.Windows.Forms.GroupBox();
            this.labelWrapAnd = new System.Windows.Forms.Label();
            this.chkWrapWords = new System.Windows.Forms.CheckBox();
            this.chkWrapLines = new System.Windows.Forms.CheckBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabList = new System.Windows.Forms.TabPage();
            this.groupListMaxChars = new System.Windows.Forms.GroupBox();
            this.numMaxCharsRight = new System.Windows.Forms.NumericUpDown();
            this.numMaxCharsLeft = new System.Windows.Forms.NumericUpDown();
            this.labelListMaxCharsRight = new System.Windows.Forms.Label();
            this.labelListMaxCharsLeft = new System.Windows.Forms.Label();
            this.groupListPath = new System.Windows.Forms.GroupBox();
            this.chkListFullPath = new System.Windows.Forms.CheckBox();
            this.tabViewer = new System.Windows.Forms.TabPage();
            this.labelWrapNote = new System.Windows.Forms.Label();
            this.labelAzuki = new System.Windows.Forms.Label();
            this.groupTabWidth = new System.Windows.Forms.GroupBox();
            this.numTabWidth = new System.Windows.Forms.NumericUpDown();
            this.groupViewerFont = new System.Windows.Forms.GroupBox();
            this.textViewerFontName = new System.Windows.Forms.TextBox();
            this.btnViewerFont = new System.Windows.Forms.Button();
            this.groupViewerShow = new System.Windows.Forms.GroupBox();
            this.chkShowTab = new System.Windows.Forms.CheckBox();
            this.chkShowSpc = new System.Windows.Forms.CheckBox();
            this.chkShowEol = new System.Windows.Forms.CheckBox();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).BeginInit();
            this.groupEditor.SuspendLayout();
            this.groupCache.SuspendLayout();
            this.groupLayout.SuspendLayout();
            this.groupViewerWrap.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabList.SuspendLayout();
            this.groupListMaxChars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCharsRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCharsLeft)).BeginInit();
            this.groupListPath.SuspendLayout();
            this.tabViewer.SuspendLayout();
            this.groupTabWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTabWidth)).BeginInit();
            this.groupViewerFont.SuspendLayout();
            this.groupViewerShow.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkCacheFile
            // 
            this.chkCacheFile.Location = new System.Drawing.Point(16, 20);
            this.chkCacheFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkCacheFile.Name = "chkCacheFile";
            this.chkCacheFile.Size = new System.Drawing.Size(182, 26);
            this.chkCacheFile.TabIndex = 0;
            this.chkCacheFile.Text = "Cache file contents";
            this.chkCacheFile.UseVisualStyleBackColor = true;
            this.chkCacheFile.CheckedChanged += new System.EventHandler(this.chkCacheFile_CheckedChanged);
            // 
            // labelCacheSize
            // 
            this.labelCacheSize.AutoSize = true;
            this.labelCacheSize.Location = new System.Drawing.Point(16, 56);
            this.labelCacheSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCacheSize.Name = "labelCacheSize";
            this.labelCacheSize.Size = new System.Drawing.Size(62, 15);
            this.labelCacheSize.TabIndex = 1;
            this.labelCacheSize.Text = "Cache size";
            // 
            // labelCacheSizeUnit
            // 
            this.labelCacheSizeUnit.AutoSize = true;
            this.labelCacheSizeUnit.Location = new System.Drawing.Point(160, 56);
            this.labelCacheSizeUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCacheSizeUnit.Name = "labelCacheSizeUnit";
            this.labelCacheSizeUnit.Size = new System.Drawing.Size(25, 15);
            this.labelCacheSizeUnit.TabIndex = 3;
            this.labelCacheSizeUnit.Text = "MB";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(280, 344);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 30);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(344, 344);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // numCacheSize
            // 
            this.numCacheSize.Location = new System.Drawing.Point(92, 52);
            this.numCacheSize.Maximum = new decimal(new int[] {
            1024000,
            0,
            0,
            0});
            this.numCacheSize.Name = "numCacheSize";
            this.numCacheSize.Size = new System.Drawing.Size(63, 23);
            this.numCacheSize.TabIndex = 2;
            this.numCacheSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textEditorArgument
            // 
            this.textEditorArgument.Location = new System.Drawing.Point(48, 56);
            this.textEditorArgument.Name = "textEditorArgument";
            this.textEditorArgument.Size = new System.Drawing.Size(324, 23);
            this.textEditorArgument.TabIndex = 5;
            // 
            // labelEditorDesc
            // 
            this.labelEditorDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelEditorDesc.Location = new System.Drawing.Point(48, 84);
            this.labelEditorDesc.Name = "labelEditorDesc";
            this.labelEditorDesc.Size = new System.Drawing.Size(324, 36);
            this.labelEditorDesc.TabIndex = 6;
            this.labelEditorDesc.Text = "(%file, %line, %colm will be replaced with the file path, line and column numbers" +
    ")";
            // 
            // textEditorCommand
            // 
            this.textEditorCommand.Location = new System.Drawing.Point(48, 26);
            this.textEditorCommand.Name = "textEditorCommand";
            this.textEditorCommand.Size = new System.Drawing.Size(246, 23);
            this.textEditorCommand.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Image = global::TabTextFinder.Properties.Resources.Img_folder_horizontal_open;
            this.btnBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowse.Location = new System.Drawing.Point(300, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(72, 28);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // labelCmd
            // 
            this.labelCmd.AutoSize = true;
            this.labelCmd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCmd.Location = new System.Drawing.Point(12, 28);
            this.labelCmd.Name = "labelCmd";
            this.labelCmd.Size = new System.Drawing.Size(33, 15);
            this.labelCmd.TabIndex = 1;
            this.labelCmd.Text = "&Cmd";
            // 
            // labelArg
            // 
            this.labelArg.AutoSize = true;
            this.labelArg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelArg.Location = new System.Drawing.Point(12, 60);
            this.labelArg.Name = "labelArg";
            this.labelArg.Size = new System.Drawing.Size(31, 15);
            this.labelArg.TabIndex = 4;
            this.labelArg.Text = "&Args";
            // 
            // groupEditor
            // 
            this.groupEditor.Controls.Add(this.labelCmd);
            this.groupEditor.Controls.Add(this.labelArg);
            this.groupEditor.Controls.Add(this.textEditorArgument);
            this.groupEditor.Controls.Add(this.labelEditorDesc);
            this.groupEditor.Controls.Add(this.btnBrowse);
            this.groupEditor.Controls.Add(this.textEditorCommand);
            this.groupEditor.Location = new System.Drawing.Point(8, 8);
            this.groupEditor.Name = "groupEditor";
            this.groupEditor.Size = new System.Drawing.Size(380, 128);
            this.groupEditor.TabIndex = 2;
            this.groupEditor.TabStop = false;
            this.groupEditor.Text = "External editor command";
            // 
            // groupCache
            // 
            this.groupCache.Controls.Add(this.labelCache);
            this.groupCache.Controls.Add(this.chkCacheFile);
            this.groupCache.Controls.Add(this.labelCacheSize);
            this.groupCache.Controls.Add(this.numCacheSize);
            this.groupCache.Controls.Add(this.labelCacheSizeUnit);
            this.groupCache.Location = new System.Drawing.Point(8, 8);
            this.groupCache.Name = "groupCache";
            this.groupCache.Size = new System.Drawing.Size(380, 104);
            this.groupCache.TabIndex = 3;
            this.groupCache.TabStop = false;
            this.groupCache.Text = "File caching";
            // 
            // labelCache
            // 
            this.labelCache.AutoSize = true;
            this.labelCache.Location = new System.Drawing.Point(12, 80);
            this.labelCache.Name = "labelCache";
            this.labelCache.Size = new System.Drawing.Size(307, 15);
            this.labelCache.TabIndex = 4;
            this.labelCache.Text = "(Effective when searching the same files for many times.)";
            // 
            // groupLayout
            // 
            this.groupLayout.Controls.Add(this.btnLayoutHorz);
            this.groupLayout.Controls.Add(this.btnLayoutVert);
            this.groupLayout.Location = new System.Drawing.Point(8, 144);
            this.groupLayout.Name = "groupLayout";
            this.groupLayout.Size = new System.Drawing.Size(209, 64);
            this.groupLayout.TabIndex = 0;
            this.groupLayout.TabStop = false;
            this.groupLayout.Text = "Layout";
            // 
            // btnLayoutHorz
            // 
            this.btnLayoutHorz.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnLayoutHorz.Image = global::TabTextFinder.Properties.Resources.Img_split_vert;
            this.btnLayoutHorz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLayoutHorz.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLayoutHorz.Location = new System.Drawing.Point(108, 24);
            this.btnLayoutHorz.Name = "btnLayoutHorz";
            this.btnLayoutHorz.Size = new System.Drawing.Size(88, 28);
            this.btnLayoutHorz.TabIndex = 1;
            this.btnLayoutHorz.TabStop = true;
            this.btnLayoutHorz.Text = "&Horizontal";
            this.btnLayoutHorz.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLayoutHorz.UseVisualStyleBackColor = true;
            // 
            // btnLayoutVert
            // 
            this.btnLayoutVert.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnLayoutVert.Image = global::TabTextFinder.Properties.Resources.Img_split_horz;
            this.btnLayoutVert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLayoutVert.Location = new System.Drawing.Point(16, 24);
            this.btnLayoutVert.Name = "btnLayoutVert";
            this.btnLayoutVert.Size = new System.Drawing.Size(88, 28);
            this.btnLayoutVert.TabIndex = 0;
            this.btnLayoutVert.TabStop = true;
            this.btnLayoutVert.Text = "&Vertical";
            this.btnLayoutVert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLayoutVert.UseVisualStyleBackColor = true;
            // 
            // groupViewerWrap
            // 
            this.groupViewerWrap.Controls.Add(this.labelWrapAnd);
            this.groupViewerWrap.Controls.Add(this.chkWrapWords);
            this.groupViewerWrap.Controls.Add(this.chkWrapLines);
            this.groupViewerWrap.Location = new System.Drawing.Point(8, 196);
            this.groupViewerWrap.Name = "groupViewerWrap";
            this.groupViewerWrap.Size = new System.Drawing.Size(164, 60);
            this.groupViewerWrap.TabIndex = 2;
            this.groupViewerWrap.TabStop = false;
            this.groupViewerWrap.Text = "Wrapping";
            // 
            // labelWrapAnd
            // 
            this.labelWrapAnd.Location = new System.Drawing.Point(76, 24);
            this.labelWrapAnd.Name = "labelWrapAnd";
            this.labelWrapAnd.Size = new System.Drawing.Size(17, 22);
            this.labelWrapAnd.TabIndex = 8;
            this.labelWrapAnd.Text = "&";
            this.labelWrapAnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelWrapAnd.UseMnemonic = false;
            // 
            // chkWrapWords
            // 
            this.chkWrapWords.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkWrapWords.Location = new System.Drawing.Point(96, 24);
            this.chkWrapWords.Name = "chkWrapWords";
            this.chkWrapWords.Size = new System.Drawing.Size(56, 24);
            this.chkWrapWords.TabIndex = 1;
            this.chkWrapWords.Text = "Words";
            this.chkWrapWords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkWrapWords.UseVisualStyleBackColor = true;
            this.chkWrapWords.CheckedChanged += new System.EventHandler(this.chkWrapWords_CheckedChanged);
            // 
            // chkWrapLines
            // 
            this.chkWrapLines.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkWrapLines.Location = new System.Drawing.Point(12, 20);
            this.chkWrapLines.Name = "chkWrapLines";
            this.chkWrapLines.Size = new System.Drawing.Size(60, 28);
            this.chkWrapLines.TabIndex = 0;
            this.chkWrapLines.Text = "Lines";
            this.chkWrapLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkWrapLines.UseVisualStyleBackColor = true;
            this.chkWrapLines.CheckedChanged += new System.EventHandler(this.chkWrapLines_CheckedChanged);
            // 
            // labelNote
            // 
            this.labelNote.Location = new System.Drawing.Point(20, 344);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(252, 26);
            this.labelNote.TabIndex = 4;
            this.labelNote.Text = "(Changes will be effective from next search.)";
            this.labelNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.tabGeneral);
            this.tabConfig.Controls.Add(this.tabList);
            this.tabConfig.Controls.Add(this.tabViewer);
            this.tabConfig.Controls.Add(this.tabAdvanced);
            this.tabConfig.Location = new System.Drawing.Point(7, 9);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(402, 326);
            this.tabConfig.TabIndex = 7;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupLayout);
            this.tabGeneral.Controls.Add(this.groupEditor);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(394, 298);
            this.tabGeneral.TabIndex = 2;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.groupListMaxChars);
            this.tabList.Controls.Add(this.groupListPath);
            this.tabList.Location = new System.Drawing.Point(4, 24);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(394, 298);
            this.tabList.TabIndex = 3;
            this.tabList.Text = "List";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // groupListMaxChars
            // 
            this.groupListMaxChars.Controls.Add(this.numMaxCharsRight);
            this.groupListMaxChars.Controls.Add(this.numMaxCharsLeft);
            this.groupListMaxChars.Controls.Add(this.labelListMaxCharsRight);
            this.groupListMaxChars.Controls.Add(this.labelListMaxCharsLeft);
            this.groupListMaxChars.Location = new System.Drawing.Point(8, 64);
            this.groupListMaxChars.Name = "groupListMaxChars";
            this.groupListMaxChars.Size = new System.Drawing.Size(256, 88);
            this.groupListMaxChars.TabIndex = 3;
            this.groupListMaxChars.TabStop = false;
            this.groupListMaxChars.Text = "\"Content\" max number of chars shown";
            // 
            // numMaxCharsRight
            // 
            this.numMaxCharsRight.Location = new System.Drawing.Point(168, 54);
            this.numMaxCharsRight.Name = "numMaxCharsRight";
            this.numMaxCharsRight.Size = new System.Drawing.Size(72, 23);
            this.numMaxCharsRight.TabIndex = 3;
            this.numMaxCharsRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numMaxCharsLeft
            // 
            this.numMaxCharsLeft.Location = new System.Drawing.Point(168, 22);
            this.numMaxCharsLeft.Name = "numMaxCharsLeft";
            this.numMaxCharsLeft.Size = new System.Drawing.Size(72, 23);
            this.numMaxCharsLeft.TabIndex = 1;
            this.numMaxCharsLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelListMaxCharsRight
            // 
            this.labelListMaxCharsRight.Location = new System.Drawing.Point(16, 56);
            this.labelListMaxCharsRight.Name = "labelListMaxCharsRight";
            this.labelListMaxCharsRight.Size = new System.Drawing.Size(144, 24);
            this.labelListMaxCharsRight.TabIndex = 2;
            this.labelListMaxCharsRight.Text = "On the right of keywords";
            // 
            // labelListMaxCharsLeft
            // 
            this.labelListMaxCharsLeft.Location = new System.Drawing.Point(16, 24);
            this.labelListMaxCharsLeft.Name = "labelListMaxCharsLeft";
            this.labelListMaxCharsLeft.Size = new System.Drawing.Size(144, 24);
            this.labelListMaxCharsLeft.TabIndex = 0;
            this.labelListMaxCharsLeft.Text = "On the left of keywords";
            // 
            // groupListPath
            // 
            this.groupListPath.Controls.Add(this.chkListFullPath);
            this.groupListPath.Location = new System.Drawing.Point(8, 8);
            this.groupListPath.Name = "groupListPath";
            this.groupListPath.Size = new System.Drawing.Size(256, 48);
            this.groupListPath.TabIndex = 2;
            this.groupListPath.TabStop = false;
            this.groupListPath.Text = "\"Filename\"";
            // 
            // chkListFullPath
            // 
            this.chkListFullPath.Location = new System.Drawing.Point(16, 18);
            this.chkListFullPath.Name = "chkListFullPath";
            this.chkListFullPath.Size = new System.Drawing.Size(120, 24);
            this.chkListFullPath.TabIndex = 0;
            this.chkListFullPath.Text = "Show full path";
            this.chkListFullPath.UseVisualStyleBackColor = true;
            // 
            // tabViewer
            // 
            this.tabViewer.Controls.Add(this.labelWrapNote);
            this.tabViewer.Controls.Add(this.labelAzuki);
            this.tabViewer.Controls.Add(this.groupTabWidth);
            this.tabViewer.Controls.Add(this.groupViewerFont);
            this.tabViewer.Controls.Add(this.groupViewerShow);
            this.tabViewer.Controls.Add(this.groupViewerWrap);
            this.tabViewer.Location = new System.Drawing.Point(4, 24);
            this.tabViewer.Name = "tabViewer";
            this.tabViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabViewer.Size = new System.Drawing.Size(394, 298);
            this.tabViewer.TabIndex = 1;
            this.tabViewer.Text = "Viewer";
            this.tabViewer.UseVisualStyleBackColor = true;
            // 
            // labelWrapNote
            // 
            this.labelWrapNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelWrapNote.Location = new System.Drawing.Point(8, 256);
            this.labelWrapNote.Name = "labelWrapNote";
            this.labelWrapNote.Size = new System.Drawing.Size(164, 36);
            this.labelWrapNote.TabIndex = 8;
            this.labelWrapNote.Text = "(Wrapping may take a long time for large files.)";
            this.labelWrapNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelAzuki
            // 
            this.labelAzuki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAzuki.Location = new System.Drawing.Point(176, 9);
            this.labelAzuki.Name = "labelAzuki";
            this.labelAzuki.Size = new System.Drawing.Size(216, 283);
            this.labelAzuki.TabIndex = 4;
            this.labelAzuki.Text = "azuki";
            this.labelAzuki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAzuki.Visible = false;
            // 
            // groupTabWidth
            // 
            this.groupTabWidth.Controls.Add(this.numTabWidth);
            this.groupTabWidth.Location = new System.Drawing.Point(8, 136);
            this.groupTabWidth.Name = "groupTabWidth";
            this.groupTabWidth.Size = new System.Drawing.Size(164, 52);
            this.groupTabWidth.TabIndex = 1;
            this.groupTabWidth.TabStop = false;
            this.groupTabWidth.Text = "Tab Width";
            // 
            // numTabWidth
            // 
            this.numTabWidth.Location = new System.Drawing.Point(48, 20);
            this.numTabWidth.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numTabWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTabWidth.Name = "numTabWidth";
            this.numTabWidth.Size = new System.Drawing.Size(64, 23);
            this.numTabWidth.TabIndex = 0;
            this.numTabWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTabWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTabWidth.ValueChanged += new System.EventHandler(this.numTabWidth_ValueChanged);
            // 
            // groupViewerFont
            // 
            this.groupViewerFont.Controls.Add(this.textViewerFontName);
            this.groupViewerFont.Controls.Add(this.btnViewerFont);
            this.groupViewerFont.Location = new System.Drawing.Point(8, 8);
            this.groupViewerFont.Name = "groupViewerFont";
            this.groupViewerFont.Size = new System.Drawing.Size(164, 52);
            this.groupViewerFont.TabIndex = 0;
            this.groupViewerFont.TabStop = false;
            this.groupViewerFont.Text = "Font";
            // 
            // textViewerFontName
            // 
            this.textViewerFontName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textViewerFontName.Location = new System.Drawing.Point(7, 21);
            this.textViewerFontName.Name = "textViewerFontName";
            this.textViewerFontName.Size = new System.Drawing.Size(121, 16);
            this.textViewerFontName.TabIndex = 0;
            // 
            // btnViewerFont
            // 
            this.btnViewerFont.Image = global::TabTextFinder.Properties.Resources.Img_edit;
            this.btnViewerFont.Location = new System.Drawing.Point(128, 16);
            this.btnViewerFont.Name = "btnViewerFont";
            this.btnViewerFont.Size = new System.Drawing.Size(28, 28);
            this.btnViewerFont.TabIndex = 1;
            this.btnViewerFont.UseVisualStyleBackColor = true;
            this.btnViewerFont.Click += new System.EventHandler(this.btnViewerFont_Click);
            // 
            // groupViewerShow
            // 
            this.groupViewerShow.Controls.Add(this.chkShowTab);
            this.groupViewerShow.Controls.Add(this.chkShowSpc);
            this.groupViewerShow.Controls.Add(this.chkShowEol);
            this.groupViewerShow.Location = new System.Drawing.Point(8, 64);
            this.groupViewerShow.Name = "groupViewerShow";
            this.groupViewerShow.Size = new System.Drawing.Size(164, 64);
            this.groupViewerShow.TabIndex = 3;
            this.groupViewerShow.TabStop = false;
            this.groupViewerShow.Text = "Show";
            // 
            // chkShowTab
            // 
            this.chkShowTab.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowTab.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkShowTab.Location = new System.Drawing.Point(60, 20);
            this.chkShowTab.Name = "chkShowTab";
            this.chkShowTab.Size = new System.Drawing.Size(48, 28);
            this.chkShowTab.TabIndex = 1;
            this.chkShowTab.Text = "Tab";
            this.chkShowTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkShowTab.UseVisualStyleBackColor = true;
            this.chkShowTab.CheckedChanged += new System.EventHandler(this.chkShowTab_CheckedChanged);
            // 
            // chkShowSpc
            // 
            this.chkShowSpc.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowSpc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkShowSpc.Location = new System.Drawing.Point(112, 20);
            this.chkShowSpc.Name = "chkShowSpc";
            this.chkShowSpc.Size = new System.Drawing.Size(48, 28);
            this.chkShowSpc.TabIndex = 2;
            this.chkShowSpc.Text = "Space";
            this.chkShowSpc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkShowSpc.UseVisualStyleBackColor = true;
            this.chkShowSpc.CheckedChanged += new System.EventHandler(this.chkShowSpc_CheckedChanged);
            // 
            // chkShowEol
            // 
            this.chkShowEol.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowEol.Location = new System.Drawing.Point(8, 20);
            this.chkShowEol.Name = "chkShowEol";
            this.chkShowEol.Size = new System.Drawing.Size(48, 28);
            this.chkShowEol.TabIndex = 0;
            this.chkShowEol.Text = "EOL";
            this.chkShowEol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkShowEol.UseVisualStyleBackColor = true;
            this.chkShowEol.CheckedChanged += new System.EventHandler(this.chkShowEol_CheckedChanged);
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.groupCache);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 24);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdvanced.Size = new System.Drawing.Size(394, 298);
            this.tabAdvanced.TabIndex = 0;
            this.tabAdvanced.Text = "Advanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(416, 382);
            this.Controls.Add(this.tabConfig);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).EndInit();
            this.groupEditor.ResumeLayout(false);
            this.groupEditor.PerformLayout();
            this.groupCache.ResumeLayout(false);
            this.groupCache.PerformLayout();
            this.groupLayout.ResumeLayout(false);
            this.groupViewerWrap.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this.groupListMaxChars.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCharsRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCharsLeft)).EndInit();
            this.groupListPath.ResumeLayout(false);
            this.tabViewer.ResumeLayout(false);
            this.groupTabWidth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTabWidth)).EndInit();
            this.groupViewerFont.ResumeLayout(false);
            this.groupViewerFont.PerformLayout();
            this.groupViewerShow.ResumeLayout(false);
            this.tabAdvanced.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCacheFile;
        private System.Windows.Forms.Label labelCacheSize;
        private System.Windows.Forms.Label labelCacheSizeUnit;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numCacheSize;
        private System.Windows.Forms.TextBox textEditorArgument;
        private System.Windows.Forms.TextBox textEditorCommand;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label labelCmd;
        private System.Windows.Forms.Label labelArg;
        private System.Windows.Forms.GroupBox groupEditor;
        private System.Windows.Forms.GroupBox groupCache;
        private System.Windows.Forms.GroupBox groupLayout;
        private System.Windows.Forms.RadioButton btnLayoutHorz;
        private System.Windows.Forms.RadioButton btnLayoutVert;
        private System.Windows.Forms.GroupBox groupViewerWrap;
        private System.Windows.Forms.CheckBox chkWrapWords;
        private System.Windows.Forms.CheckBox chkWrapLines;
        private System.Windows.Forms.Label labelEditorDesc;
        private System.Windows.Forms.Label labelCache;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabViewer;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.GroupBox groupViewerShow;
        private System.Windows.Forms.CheckBox chkShowEol;
        private System.Windows.Forms.GroupBox groupViewerFont;
        private System.Windows.Forms.CheckBox chkShowTab;
        private System.Windows.Forms.CheckBox chkShowSpc;
        private System.Windows.Forms.Label labelAzuki;
        private System.Windows.Forms.GroupBox groupTabWidth;
        private System.Windows.Forms.NumericUpDown numTabWidth;
        private System.Windows.Forms.Button btnViewerFont;
        private System.Windows.Forms.Label labelWrapAnd;
        private System.Windows.Forms.TextBox textViewerFontName;
        private System.Windows.Forms.Label labelWrapNote;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.GroupBox groupListPath;
        private System.Windows.Forms.GroupBox groupListMaxChars;
        private System.Windows.Forms.Label labelListMaxCharsLeft;
        private System.Windows.Forms.CheckBox chkListFullPath;
        private System.Windows.Forms.Label labelListMaxCharsRight;
        private System.Windows.Forms.NumericUpDown numMaxCharsRight;
        private System.Windows.Forms.NumericUpDown numMaxCharsLeft;
    }
}