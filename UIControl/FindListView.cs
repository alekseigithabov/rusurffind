using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TabTextFinder.Finder;
using TabTextFinder.Properties;
using TabTextFinder.Util.PInvokeAPI;

namespace TabTextFinder.UIControl
{
    class FindListView : ListView
    {
        private FindQuery query;
        private FoundLineComparer comparer;
        private int[] index_map;

        private ColumnHeader clmFile = new ColumnHeader();
        private ColumnHeader clmLine = new ColumnHeader();
        private ColumnHeader clmColm = new ColumnHeader();
        private ColumnHeader clmText = new ColumnHeader();

        private ContextMenuStrip menu;
        private ToolStripMenuItem itemCopyPath = new ToolStripMenuItem( Resources.Menu_CopyPath, Resources.Img_clipboard );
        private ToolStripMenuItem itemOpenFolder = new ToolStripMenuItem( Resources.Menu_OpenFolder, Resources.Img_folder_horizontal_open );
        private ToolStripMenuItem itemShowProperty = new ToolStripMenuItem( Resources.Menu_ShowProperty, Resources.Img_information );
        private ToolStripMenuItem itemExternalEditor = new ToolStripMenuItem( Resources.Menu_ExternalEditor, Resources.Img_editor );
        private ToolStripMenuItem itemChooseFont = new ToolStripMenuItem( Resources.Menu_ChooseFont, Resources.Img_edit );
        private ToolStripMenuItem itemExport = new ToolStripMenuItem( Resources.Menu_Export, Resources.Img_drive_download );

        private IContainer components = new Container();
        private bool disposed = false;

        public FindListView( FindQuery query )
        {
            this.query = query;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            Settings settings = Settings.Default;
            {// columns
                clmFile.Text = Resources.Label_FileName;
                clmLine.Text = Resources.Label_Line;
                clmColm.Text = Resources.Label_Column;
                clmText.Text = Resources.Label_Content;

                clmLine.TextAlign = HorizontalAlignment.Right;
                clmColm.TextAlign = HorizontalAlignment.Right;

                clmFile.Width = settings.ListWidthFile;
                clmLine.Width = settings.ListWidthLine;
                clmColm.Width = settings.ListWidthColumn;
                clmText.Width = settings.ListWidthText;

                Columns.AddRange( new ColumnHeader[] { clmFile, clmLine, clmColm, clmText } );
            }
            {// set line height
                SmallImageList = new ImageList( components );
                SmallImageList.ImageSize = new Size( 1, FontHeight + 2 );
            }
            {// menu items
                itemCopyPath.Click += new EventHandler( itemCopyPath_Click );
                itemOpenFolder.Click += new EventHandler( itemOpenFolder_Click );
                itemShowProperty.Click += new EventHandler( itemShowProperty_Click );
                itemExternalEditor.Click += new EventHandler( itemExternalEditor_Click );
                itemChooseFont.Click += new EventHandler( itemChooseFont_Click );
                itemExport.Click += new EventHandler( itemExport_Click );
            }
            {// menu
                menu = new ContextMenuStrip( components );
                menu.Items.AddRange( new ToolStripItem[] {
                    itemCopyPath, itemOpenFolder, itemExternalEditor, itemShowProperty,
                    new ToolStripSeparator(), itemChooseFont, itemExport
                } );
                menu.Opening += new CancelEventHandler( menu_Opening );
                ContextMenuStrip = menu;
            }
            {// FindListView
                Font = settings.ListFont.Clone() as Font;
                View = View.Details;
                GridLines = true;
                MultiSelect = false;
                VirtualMode = true;
                FullRowSelect = true;
                HideSelection = false;
                DoubleBuffered = true;
                ShowItemToolTips = true;
            }
            ResumeLayout( false );
        }

        protected override void Dispose( bool disposing )
        {
            if (disposed) { return; }
            try {
                if (disposing) {
                    menu.Opening -= new CancelEventHandler( menu_Opening );
                    itemCopyPath.Click -= new EventHandler( itemCopyPath_Click );
                    itemOpenFolder.Click -= new EventHandler( itemOpenFolder_Click );
                    itemShowProperty.Click -= new EventHandler( itemShowProperty_Click );
                    itemExternalEditor.Click -= new EventHandler( itemExternalEditor_Click );
                    itemChooseFont.Click -= new EventHandler( itemChooseFont_Click );
                    itemExport.Click -= new EventHandler( itemExport_Click );

                    components.Dispose();
                }
                disposed = true;
            }
            finally {
                // column headers are disposed here
                base.Dispose( disposing );
            }
        }

        #region Resize

        // Not working correctly.
        // When the horizontal scrollbar was shown, and whenthe clmText width was changed,
        // the list view location was moved to the right somehow??
        //protected override void OnResize( EventArgs e )
        //{
        //    // if the Text colum width is narrower than available, extend it
        //    int width = ClientSize.Width - (clmFile.Width + clmLine.Width + clmColm.Width);
        //    if (clmText.Width < width) {
        //        clmText.Width = width;
        //    }
        //    base.OnResize( e );
        //}

        protected override void OnColumnWidthChanged( ColumnWidthChangedEventArgs e )
        {
            Settings settings = Settings.Default;
            {
                settings.ListWidthFile = clmFile.Width;
                settings.ListWidthLine = clmLine.Width;
                settings.ListWidthColumn = clmColm.Width;
                settings.ListWidthText = clmText.Width;
            }
            base.OnColumnWidthChanged( e );
        }

        #endregion
        #region ListItem & Sort

        private int MapIndex( int list_index )
        {
            if (index_map == null || list_index < 0 || index_map.Length <= list_index) {
                return list_index;
            } else {
                return index_map[list_index];
            }
        }

        public int GetSelectedLineIndex()
        {
            return (SelectedIndices.Count == 0) ? -1 : MapIndex( SelectedIndices[0] );
        }

        private FoundLine GetFoundLine( int line_index )
        {
            return query.FoundLines[line_index];
        }

        private string GetFoundPath( FoundLine line )
        {
            return query.FoundFiles[line.FileIdx].Info.FullName;
        }

        protected override void OnRetrieveVirtualItem( RetrieveVirtualItemEventArgs e )
        {
            {// set item
                e.Item = GetFoundLine( MapIndex( e.ItemIndex ) ).Item;
            }
            base.OnRetrieveVirtualItem( e );
        }

        protected override void OnColumnClick( ColumnClickEventArgs e )
        {
            if (!query.Working) {
                Cursor = Cursors.WaitCursor;
                FoundLineCollection lines = query.FoundLines;
                {// preparation
                    if (index_map == null) {
                        index_map = new int[lines.Count];
                        for (int i = 0; i < index_map.Length; ++i) {
                            index_map[i] = i;
                        }
                    }
                    if (comparer == null) {
                        comparer = new FoundLineComparer( lines );
                    }
                }
                // store selection
                int selected = GetSelectedLineIndex();
                {// sort
                    comparer.Column = e.Column;
                    Array.Sort( index_map, comparer );
                }
                {// restore selection
                    SelectedIndices.Clear();
                    if (selected >= 0) {
                        int idx = Array.IndexOf( index_map, selected );
                        if (idx >= 0) {
                            SelectedIndices.Add( idx );
                            EnsureVisible( idx );
                        }
                    }
                }
                Refresh();
                Cursor = Cursors.Default;
            }
            base.OnColumnClick( e );
        }

        #endregion
        #region Item Handling

        protected override void OnItemDrag( ItemDragEventArgs e )
        {
            ListViewItem item = e.Item as ListViewItem;
            if (item != null) {
                string path = GetFoundPath( GetFoundLine( Items.IndexOf( item ) ) );
                DataObject data = new DataObject( DataFormats.FileDrop, new string[] { path } );
                DragDropEffects effect = DragDropEffects.Copy | DragDropEffects.Move;
                DoDragDrop( data, effect );
            }
            base.OnItemDrag( e );
        }

        protected override void OnMouseDoubleClick( MouseEventArgs e )
        {
            if (e.Button == MouseButtons.Left) {
                itemExternalEditor_Click( null, null );
            }
            base.OnMouseDoubleClick( e );
        }

        #endregion

        /// <summary>
        /// lazy update
        /// update the list size only when the cursor is close to the top / the bottom
        /// </summary>
        public void UpdateListSize( bool bForce )
        {
            if (disposed) { return; }

            bool bUpdate = bForce;
            if (!bUpdate) {
                ListViewItem top = GetItemAt( 0, 0 );
                ListViewItem bot = GetItemAt( 0, ClientSize.Height - 1 );
                if (top == null || bot == null) {
                    bUpdate = true;
                } else {
                    bUpdate = (VirtualListSize < bot.Index + (bot.Index - top.Index));
                }
            }
            if (bUpdate) {
                BeginUpdate();
                VirtualListSize = query.Status.FoundLinesCount;
                EndUpdate();
            }
        }

        #region ContextMenu

        private void menu_Opening( object sender, CancelEventArgs e )
        {
            bool bEnabled = (GetSelectedLineIndex() >= 0);
            itemCopyPath.Enabled = bEnabled;
            itemOpenFolder.Enabled = bEnabled;
            itemShowProperty.Enabled = bEnabled;
            itemExternalEditor.Enabled = bEnabled && (Settings.Default.EditorCommand.Length > 0);
            itemExport.Enabled = (query.Working == false);
        }

        private void ExecWithFoundPath( Action<string> action )
        {
            int sel = GetSelectedLineIndex();
            if (sel >= 0) {
                action( GetFoundPath( GetFoundLine( sel ) ) );
            }
        }

        private void itemCopyPath_Click( object sender, EventArgs e )
        {
            ExecWithFoundPath( ( path ) => Clipboard.SetText( path ) );
        }

        private void itemOpenFolder_Click( object sender, EventArgs e )
        {
            ExecWithFoundPath( ( path ) => Process.Start( "explorer.exe", "/select," + path ) );
        }

        private void itemShowProperty_Click( object sender, EventArgs e )
        {
            ExecWithFoundPath( ( path ) => PropertiesDialog.ShowDialog( path ) );
        }

        private void itemExternalEditor_Click( object sender, EventArgs _e )
        {
            FoundLine fl;
            {
                int sel = GetSelectedLineIndex();
                if (sel < 0) { return; }
                fl = GetFoundLine( sel );
            }
            string cmd = Settings.Default.EditorCommand;
            {
                cmd = cmd.Trim();
                if (cmd.Length == 0) { return; }
            }
            string arg = Settings.Default.EditorArgument;
            {
                arg = arg.Replace( "%file", string.Format( "\"{0}\"", GetFoundPath( fl ) ) );
                arg = arg.Replace( "%line", (fl.Line + 1).ToString() );
                arg = arg.Replace( "%colm", (fl.Column + 1).ToString() );
                arg = arg.Trim();
            }
            {// exec
                Process proc = null;
                try {
                    proc = Process.Start( cmd, arg );
                }
                catch (Exception e) {
                    string mesg = string.Format( "{0}\ncmd = {1}\narg = {2}", e.Message, cmd, arg );
                    MessageBox.Show( this, mesg, Resources.Title_Application, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void itemChooseFont_Click( object sender, EventArgs e )
        {
            Font font = Font;
            using (FontDialog dlg = new FontDialog()) {
                dlg.Font = Font;
                dlg.Apply += delegate { Font = dlg.Font; };
                dlg.ShowApply = true;
                dlg.ShowEffects = false;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    Font = dlg.Font;
                    Settings.Default.ListFont = dlg.Font;
                } else {
                    Font = font;
                }
            }
        }

        private void itemExport_Click( object sender, EventArgs _e )
        {
            using (SaveFileDialog dlg = new SaveFileDialog()) {
                dlg.Filter = Resources.Filter_TextFile;
                dlg.FilterIndex = 0;
                dlg.ValidateNames = true;
                dlg.OverwritePrompt = true;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    try {
                        Export( dlg.FileName );
                    }
                    catch (Exception e) {
                        MessageBox.Show( this, e.Message, Resources.Title_Application, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                 }
             }
         }
 
        private void Export( string path )
        {
            using (StreamWriter writer = new StreamWriter( path )) {
                for (int i = 0; i < query.FoundLines.Count; ++i) {
                    int n = MapIndex( i );
                    FoundLine fl = GetFoundLine( n );
                    writer.WriteLine( "{0}({1}):{2}", GetFoundPath( fl ), fl.Line + 1, fl.Content );
                }
            }
        }

        #endregion
    }

    class FoundLineComparer : IComparer<int>
    {
        private SortOrder Order { get; set; }
        private FoundLineCollection FoundLines { get; set; }

        public int Column
        {
            get { return column; }
            set
            {
                if (column == value) {
                    Order = (Order == SortOrder.Ascending) ? SortOrder.Descending : Order = SortOrder.Ascending;
                } else {
                    column = value;
                }
            }
        }
        private int column;

        public FoundLineComparer( FoundLineCollection found_lines )
        {
            Order = SortOrder.Ascending;
            FoundLines = found_lines;
        }

        public int Compare( int na, int nb )
        {
            int result = 0;
            {
                FoundLine a = FoundLines[na];
                FoundLine b = FoundLines[nb];
                if (Column == 0) {
                    result = string.Compare( a.FileName, b.FileName, StringComparison.Ordinal );
                } else if (Column == 1) {
                    result = a.Line - b.Line;
                } else if (Column == 2) {
                    result = a.Column - b.Column;
                } else if (Column == 3) {
                    result = string.Compare( a.Preview, b.Preview, StringComparison.Ordinal );
                }
            }
            if (Order == SortOrder.Descending) {
                result = -result;
            }
            if (result == 0) {
                result = na - nb;
            }
            return result;
        }
    }
}
