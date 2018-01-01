using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TabTextFinder.Finder;
using TabTextFinder.Properties;
using TabTextFinder.UIControl;
using TabTextFinder.Util;
using TabTextFinder.Util.PInvokeAPI;

namespace TabTextFinder
{
    partial class TabTextFinderForm : Form
    {
        private FormState form_state;
        private FindWorker worker;
        private FindDispacher dispatcher;
        private System.Windows.Forms.Timer timer;
        private RecentHistory recent_items;
        private ContextMenuStrip recent_menu;

        private string clipboard;

        public TabTextFinderForm()
        {
            InitializeComponent();
            {// finder
                worker = new FindWorker();
                dispatcher = new FindDispacher( InvokeFind, delegate() { return worker.IsWorking; } );
            }
            {// form
                Icon = Resources.Icon_find_white;
                form_state = new FormState( this, Settings.Default );
            }
            {// timer to poll the worker thread
                timer = new System.Windows.Forms.Timer( components );
                timer.Tick += new EventHandler( timer_Tick );
                timer.Interval = 333;
            }
            {// tabs
                tabFinds.Size = new Size( tabFinds.Width, stripStatus.Top - tabFinds.Top );
                tabHelp.ImageIndex = (int) FindTabImageType.Help;
                richHelp.Text = Resources.Text_Help;
            }
            {// recent menu
                recent_items = new RecentHistory();
                recent_menu = new ContextMenuStrip( components );
                recent_menu.ShowCheckMargin = false;
                recent_menu.ShowImageMargin = false;
                recent_menu.Closed += menuRecent_Closed;
                recent_menu.ItemClicked += menuRecent_ItemClicked;
            }
        }

        private void setToolTip( Control ctrl, string str ) { toolTip.SetToolTip( ctrl, str ); }
        private void setToolTip( ToolStripItem item, string str ) { item.ToolTipText = str; }

        private string FileEncoding
        {
            get { return cmbEncoding.SelectedItem as string; }
            set { cmbEncoding.SelectedItem = value;           }
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            {// tool tips
                setToolTip( cmbRoot, Resources.ToolTip_Root );
                setToolTip( cmbFile, Resources.ToolTip_File );
                setToolTip( cmbText, Resources.ToolTip_Text );
                setToolTip( cmbExclude, Resources.ToolTip_ExcludeFile );
                setToolTip( cmbEncoding, Resources.ToolTip_Encoding );

                setToolTip( chkCase, Resources.ToolTip_Case );
                setToolTip( chkWord, Resources.ToolTip_Word );
                setToolTip( chkRegx, Resources.ToolTip_Regx );
                setToolTip( chkRecursive, Resources.ToolTip_Recursive );

                setToolTip( btnFind, Resources.ToolTip_StartFind );
                setToolTip( btnFindIncl, Resources.ToolTip_FindIncl );
                setToolTip( btnFindExcl, Resources.ToolTip_FindExcl );
                setToolTip( btnFindFound, Resources.ToolTip_FindFound );

                setToolTip( labelCase, Resources.ToolTip_Options );
                setToolTip( labelWord, Resources.ToolTip_Options );
                setToolTip( labelRegx, Resources.ToolTip_Options );

                setToolTip( labelFiles, Resources.ToolTip_Files );
                setToolTip( labelLines, Resources.ToolTip_Lines );
                setToolTip( labelBytes, Resources.ToolTip_Bytes );
                setToolTip( labelChars, Resources.ToolTip_Chars );
                setToolTip( labelMemory, Resources.ToolTip_Memory );
                setToolTip( labelElapsed, Resources.ToolTip_Elapsed );
            }
            {// const labels
                labelEsc.Text = Resources.Label_CancelFind;
                labelCPU.Text = String.Format( "{0} CPU", SystemInfo.NumberOfProcessors );
            }
            {// file encodings
                Encoding[] encodings = new Encoding[] {
                    Encoding.Default,
                    Encoding.UTF8,
                    Encoding.Unicode,
                    Encoding.ASCII, 
                    Encoding.GetEncoding( 1252 ),
                };
                //cmbEncoding.Items.Add( Resources.FileEncoding_AutoDetect );
                foreach (Encoding enc in encodings) {
                    cmbEncoding.Items.Add( enc.WebName );
                }
                cmbEncoding.SelectedIndex = 0;
            }

            LoadSettings();
            UpdateStatusBar();

            clipboard = Clipboard.GetText();    // init clipboard copy
            timer.Start();
        }

        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            worker.AbortQuery( true );
            SaveSettings();
        }

        private void MainForm_Activated( object sender, EventArgs e )
        {
            // repost to handle WM_SYSCOMMAND:SC_CLOSE post from the task bar
            WM.PostMessage( this, WM_USER_POST_ACTIVATED, IntPtr.Zero, IntPtr.Zero );
        }

        protected override void WndProc( ref Message m )
        {
            if (m.Msg == WM_USER_POST_ACTIVATED) {
                CheckClipboard();
            }
            base.WndProc( ref m );
        }

        #region Settings

        private void LoadSettings()
        {
            Settings settings = Settings.Default;
            {
                cmbRoot.TextItems = settings.FindComboRoots;
                cmbFile.TextItems = settings.FindComboFiles;
                cmbText.TextItems = settings.FindComboTexts;
                cmbExclude.TextItems = settings.FindComboExcludeFiles;
                FileEncoding = settings.FileEncoding;
            }
            {// recent items
                if (settings.RecentHistory == null) {
                    settings.RecentHistory = new RecentHistory();
                }
                recent_items.Clear();
                recent_items.AddRange( settings.RecentHistory );
                btnRecent.Enabled = (recent_items.Count > 0);
            }

            if (settings.ListFont == null) {
                settings.ListFont = Font;
            }
            if (settings.ViewerFont == null) {
                settings.ViewerFont = Font;
            }

            chkCase.DataBindings.Add( "Checked", settings, "FindCheckCase" );
            chkWord.DataBindings.Add( "Checked", settings, "FindCheckWord" );
            chkRegx.DataBindings.Add( "Checked", settings, "FindCheckRegx" );
            chkRecursive.DataBindings.Add( "Checked", settings, "FindCheckRecursive" );

            FileCache.Instance.SetPolicy( settings.FileCacheEnable, settings.FileCacheSize );
        }

        private void SaveSettings()
        {
            Settings settings = Settings.Default;
            {
                settings.FindComboRoots = cmbRoot.TextItems;
                settings.FindComboFiles = cmbFile.TextItems;
                settings.FindComboTexts = cmbText.TextItems;
                settings.FindComboExcludeFiles = cmbExclude.TextItems;
                settings.FileEncoding = FileEncoding;
            }
            {// recent items
                settings.RecentHistory.Clear();
                settings.RecentHistory.AddRange( recent_items );
            }
            settings.Save();
        }

        #endregion
        #region Click Handlers

        private void menuRecent_ItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            ToolStripItem item = e.ClickedItem;
            if (item == null) { return; }
            RecentItem recent = item.Tag as RecentItem;
            if (recent == null) { return; }
            cmbRoot.Text = recent.Root;
            cmbFile.Text = recent.File;
            cmbExclude.Text = recent.Exclude;
        }

        private void menuRecent_Closed( object sender, ToolStripDropDownClosedEventArgs e )
        {
            recent_menu.Items.Clear();
        }

        private void btnBrowse_Click( object sender, EventArgs e )
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog()) {
                dlg.SelectedPath = cmbRoot.Text;
                dlg.ShowNewFolderButton = false;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    cmbRoot.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnRecent_Click( object sender, EventArgs e )
        {
            for (int n = recent_items.Count - 1; n >= 0; --n) {
                RecentItem recent = recent_items[n];
                string text = string.Format( "[ {0} ] + [ {1} ] - [ {2} ]", recent.Root, recent.File, recent.Exclude );
                ToolStripItem item = recent_menu.Items.Add( text );
                item.Tag = recent;
            }
            Button btn = sender as Button;
            if (btn == null) { return; }
            recent_menu.Show( this, btn.Location.X, btn.Location.Y + btn.Height );
        }

        private void btnConfig_Click( object sender, EventArgs e )
        {
            using (ConfigForm form = new ConfigForm()) {
                Settings settings = Settings.Default;
                form.LoadFromSettings( settings );
                if (form.ShowDialog() == DialogResult.OK) {
                    bool cache_changed = form.IsFileCacheSettingsChanged( settings );
                    {// save
                        form.SaveToSettings( settings );
                        settings.Save();
                    }
                    if (cache_changed) {
                        FileCache.Instance.SetPolicy( settings.FileCacheEnable, settings.FileCacheSize );
                        GC.Collect( 3 );
                    }
                }
            }
        }

        private void btnFind_Click( object sender, EventArgs e )
        {
            if (worker.IsWorking) { return; }

            FindQuery query = null;
            if (sender == null) {// from keyboard
                FindTabPage page = tabFinds.SelectedTab as FindTabPage;
                query = (page == null) ? null : page.Query;
            }

            FindType type = FindType.NewPath;
            if (query != null) {
                bool bShift = KeyState.IsPressedSync( Keys.ShiftKey );
                bool bControl = KeyState.IsPressedSync( Keys.ControlKey );
                if (bShift && bControl) {
                    type = FindType.FoundPath;
                } else if (bShift) {
                    type = FindType.FilterInclude;
                } else if (bControl) {
                    type = FindType.FilterExclude;
                }
            }
            InvokeFind( cmbText.Text, type, query );
        }

        private void InvokeDerived( FindType type )
        {
            if (worker.IsWorking) { return; }
            FindTabPage page = tabFinds.SelectedTab as FindTabPage;
            if (page == null) { return; }
            InvokeFind( cmbText.Text, type, page.Query );
        }

        private void btnFindIncl_Click( object sender, EventArgs e ) { InvokeDerived( FindType.FilterInclude ); }
        private void btnFindExcl_Click( object sender, EventArgs e ) { InvokeDerived( FindType.FilterExclude ); }
        private void btnFindFound_Click( object sender, EventArgs e ) { InvokeDerived( FindType.FoundPath ); }

        private void labelMemory_Click( object sender, EventArgs e )
        {
            GC.Collect( 3 );
        }

        #endregion

        private void InvokeFind( string text, FindType type, FindQuery base_query )
        {
            if (worker.IsWorking) { return; }
            {// check text
                if (text == null) {
                    text = cmbText.Text;
                }
                text = StringUtil.GetFirstLine( text );
                if (string.IsNullOrEmpty( text )) { return; }
            }
            {// save combo-box values
                cmbText.AddTextToItems( text );
                cmbRoot.AddTextToItems();
                cmbFile.AddTextToItems();
                cmbExclude.AddTextToItems();
                recent_items.AddItem( new RecentItem() { Root = cmbRoot.Text, File = cmbFile.Text, Exclude = cmbExclude.Text } );
                btnRecent.Enabled = (recent_items.Count > 0);
                SaveSettings();
            }
            FindQuery query;
            {// create query
                TextFinder finder;
                try {
                    finder = new TextFinder( text, new FindTextOption( chkCase.Checked, chkWord.Checked, chkRegx.Checked ) );
                }
                catch (Exception e) {
                    MessageBox.Show( this, e.Message, Resources.Title_Application, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                Settings settings = Settings.Default;
                FoundLineOption line_option = new FoundLineOption( settings.ListPathFullName, settings.ListPreviewMaxCharsLeft, settings.ListPreviewMaxCharsRight );
                if (type == FindType.NewPath) {
                    FindPathParam param = new FindPathParam( cmbRoot.Text, cmbFile.Text, cmbExclude.Text, chkRecursive.Checked );
                    query = new FindQueryByPath( type, line_option, finder, finder, FileEncoding, param );
                } else if (type == FindType.FoundPath) {
                    if (base_query == null) { return; }
                    query = new FindQueryByPath( type, line_option, finder, finder, FileEncoding, base_query );
                } else {
                    FindFilterParam param = new FindFilterParam( base_query );
                    TextFinder keyword_finder = (type == FindType.FilterInclude) ? finder : base_query.KeywordFinder;
                    query = new FindQueryByFilter( type, line_option, finder, keyword_finder, param );
                }
            }
            {// dispatch the query
                FindTabPage page = worker.DispatchQuery( query, dispatcher );
                if (page != null) {
                    tabFinds.TabPages.Add( page );
                    tabFinds.SelectTab( page );
                }
            }
            UpdateStatusBar();
        }

        #region Timer & Update

        private void timer_Tick( object sender, EventArgs e )
        {
            {// update memory size
                labelMemory.Text = "GC: " + StringUtil.GetPrefixedString( GC.GetTotalMemory( false ) ) + "B";
            }
            FindTabPage page = worker.WorkingPage;
            if (page == null) { return; }   // not working now
            {// update status
                worker.UpdateWorkingStatus();
                UpdateStatusBar();
            }
            if (worker.IsWorking) { return; }   // still working
            {// check result
                FindQuery query = page.Query;
                if (query.FoundLines.Count > 0) { return; } // found some
                if (!query.Abort) {
                    string mesg = (query.Status.TotalFiles == 0) ? Resources.Mesg_NoFilesMatched : Resources.Mesg_TextNotFound;
                    MessageBox.Show( this, mesg, Resources.Title_Application, MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                tabFinds.RemovePage( page );
            }
        }

        // update the status bar when tab page selection is changed
        private void tabFinds_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            FindQuery query;
            {
                FindTabPage page = tabFinds.SelectedTab as FindTabPage;
                query = (page == null) ? null : page.Query;
            }
            bool bQuery = (query != null);
            bool bWorking = worker.IsWorking;

            {// options
                labelCase.Visible = bQuery;
                labelWord.Visible = bQuery;
                labelRegx.Visible = bQuery;

                if (bQuery) {
                    FindTextOption option = query.Finder.Option;
                    labelCase.Enabled = option.Case;
                    labelWord.Enabled = option.Word;
                    labelRegx.Enabled = option.Regx;
                }
            }
            {// status
                labelFiles.Visible = bQuery;
                labelLines.Visible = bQuery;
                labelBytes.Visible = bQuery && (query.Type == FindType.NewPath || query.Type == FindType.FoundPath);
                labelChars.Visible = bQuery && (query.Type == FindType.FilterInclude || query.Type == FindType.FilterExclude);
                labelElapsed.Visible = bQuery;

                if (bQuery) {
                    FindStatus status = query.Status;
                    labelFiles.Text = String.Format( "{0}: {1:N0}/{2:N0}", Resources.Label_Files, query.FoundFiles.Count, status.TotalFiles );
                    labelLines.Text = String.Format( "{0}: {1:N0}/{2:N0}", Resources.Label_Lines, query.FoundLines.Count, status.TotalLines );

                    string counts = StringUtil.GetPrefixedString( status.TotalCounts );
                    labelBytes.Text = String.Format( "{0}: {1:N0}B", Resources.Label_Bytes, counts );
                    labelChars.Text = String.Format( "{0}: {1:N0}ch", Resources.Label_Chars, counts );
                    labelElapsed.Text = StringUtil.GetTimeSpanString( status.Elapsed );
                }
            }
            {// [Esc] hint, progress bar
                labelEsc.Visible = bWorking;
                prgFind.Enabled = bWorking;
                prgFind.Visible = bWorking;
            }
            {// find buttons
                btnFind.Enabled = !bWorking;
                btnFindIncl.Enabled = !bWorking && bQuery;
                btnFindExcl.Enabled = !bWorking && bQuery;
                btnFindFound.Enabled = !bWorking && bQuery;
                btnConfig.Enabled = !bWorking;
            }
        }

        #endregion
        #region Keyboard Shortcut

        protected override bool ProcessDialogKey( Keys key )
        {
            // [Esc] to cancel
            if (worker.IsWorking && key == Keys.Escape) {
                worker.AbortQuery( false );
                return true;
            }
            return base.ProcessDialogKey( key );
        }

        // close the selcted tab by [Ctrl+F4]
        // if this operation is performed in MainForm, application will not terminate !?
        protected override bool ProcessCmdKey( ref Message msg, Keys keyData )
        {
            if (msg.Msg == WM.KEYDOWN || msg.Msg == WM.SYSKEYDOWN) {
                if (keyData == (Keys.F4 | Keys.Control)) {
                    if (tabFinds.SelectedTab != null) {
                        tabFinds.RemovePage( tabFinds.SelectedTab );
                    }
                    return true;
                }
            }
            if (msg.Msg == WM.KEYDOWN) {
                if (keyData == (Keys.Enter | Keys.Shift) ||
                    keyData == (Keys.Enter | Keys.Control) ||
                    keyData == (Keys.Enter | Keys.Shift | Keys.Control)) {
                    btnFind_Click( null, null );
                    return true;
                }
            }
            return base.ProcessCmdKey( ref msg, keyData );
        }

        #endregion
        #region Clipboard

        private const uint WM_USER_POST_ACTIVATED = WM.USER + 1;

        private void CheckClipboard()
        {
            string text = Clipboard.GetText();
            {// check and update the local copy
                if (clipboard == text) { return; }
                clipboard = text;
            }
            {// format the text
                text = StringUtil.GetFirstLine( text );
                text = text.Trim();
            }
            if (!string.IsNullOrEmpty( text )) {
                cmbText.Text = text;
                cmbText.Focus();
            }
        }

        #endregion
        #region Drag&Drop to cmbRoot

        private void cmbRoot_DragEnter( object sender, DragEventArgs e )
        {
            e.Effect = (e.Data.GetDataPresent( DataFormats.FileDrop )) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void cmbRoot_DragDrop( object sender, DragEventArgs e )
        {
            List<string> roots = new List<string>();
            string[] paths = e.Data.GetData( DataFormats.FileDrop, false ) as string[];
            if (paths == null) { return; }
            foreach (string path in paths) {
                {// directory name
                    DirectoryInfo di = new DirectoryInfo( path );
                    if (di.Exists && !roots.Contains( path )) {
                        roots.Add( path );
                        continue;
                    }
                }
                {// directory name of files
                    FileInfo fi = new FileInfo( path );
                    if (fi.Exists && !roots.Contains( fi.DirectoryName )) {
                        roots.Add( fi.DirectoryName );
                        continue;
                    }
                }
            }
            if (roots.Count >= 1) {
                cmbRoot.Text = string.Join( "|", roots.ToArray() );
            }
        }

        #endregion
    }

    class FindWorker
    {
        public bool IsWorking { get { return (WorkingPage != null); } }
        public FindTabPage WorkingPage { get; private set; }

        private Thread thread;  // make sure WorkingPage != null --> thread != null

        public FindTabPage DispatchQuery( FindQuery query, FindDispacher dispatcher )
        {
            if (IsWorking) { return null; }
            WorkingPage = new FindTabPage( dispatcher, query );
            {
                thread = new Thread( new ThreadStart( query.Find ) );
                thread.Start();
            }
            return WorkingPage;
        }

        public void AbortQuery( bool bWait )
        {
            if (WorkingPage != null) {
                WorkingPage.Query.Abort = true;
            }
            if (bWait && thread != null) {
                thread.Join();
            }
        }

        public bool UpdateWorkingStatus()
        {
            if (!IsWorking) { return true; }
            bool bAlive = thread.IsAlive;   // get local copy first
            WorkingPage.UpdateList( !bAlive );
            if (!bAlive) {
                thread = null;
                WorkingPage = null;
            }
            return true;
        }
    }

    public class RecentItem : IEquatable<RecentItem>
    {
        public string Root { get; set; }
        public string File { get; set; }
        public string Exclude { get; set; }

        public bool Equals( RecentItem other )
        {
            if (Root != other.Root) return false;
            if (File != other.File) return false;
            if (Exclude != other.Exclude) return false;
            return true;
        }
    }

    public class RecentHistory : List<RecentItem>
    {
        public void AddItem( RecentItem item )
        {
            RemoveAll( new Predicate<RecentItem>( delegate( RecentItem it ) { return item.Equals( it ); } ) );
            Add( item );
            while (Count > 10) {
                RemoveAt( 0 );
            }
        }
    }
}
