using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Sgry.Azuki;
using Sgry.Azuki.WinForms;
using TabTextFinder.Finder;
using TabTextFinder.Properties;
using TabTextFinder.Util.PInvokeAPI;

namespace TabTextFinder.UIControl
{
    class FindTabPage : TabPage
    {
        public FindQuery Query { get; private set; }

        private FindDispacher dispatcher;
        private static FileInfoComparer comparer = new FileInfoComparer();

        private SplitContainer split;
        private FindListView list;
        private AzukiControl azuki;
        private TextBox textPath;
        private Label labelEncoding;

        private ContextMenuStrip menu;
        private ToolStripMenuItem itemCopy = new ToolStripMenuItem( Resources.Menu_CopySelection, Resources.Img_clipboard );
        private ToolStripMenuItem itemFind = new ToolStripMenuItem( Resources.Menu_FindSelection, Resources.Img_magnifier );
        private ToolStripMenuItem itemFound = new ToolStripMenuItem( Resources.Menu_FindSelectionInFound, Resources.Img_document_search_result );
        private ToolStripMenuItem itemInclude = new ToolStripMenuItem( Resources.Menu_IncludeSelection, Resources.Img_plus_white );
        private ToolStripMenuItem itemExclude = new ToolStripMenuItem( Resources.Menu_ExcludeSelection, Resources.Img_minus_white );

        private int file_idx = -1;  // currently loaded file on azuki
        private int split_width;
        private bool azuki_word_proc = false;

        private IContainer components = new Container();
        private bool disposed = false;

        public FindTabPage( FindDispacher dispatcher, FindQuery query )
        {
            this.dispatcher = dispatcher;
            Query = query;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            Settings settings = Settings.Default;
            Size = new Size( 640, 480 );
            {// menu items
                itemCopy.Click += new EventHandler( itemCopy_Click );
                itemFind.Click += new EventHandler( itemFind_Click );
                itemFound.Click += new EventHandler( itemFound_Click );
                itemInclude.Click += new EventHandler( itemInclude_Click );
                itemExclude.Click += new EventHandler( itemExclude_Click );
            }
            {// menu
                menu = new ContextMenuStrip( components );
                menu.Items.AddRange( new ToolStripItem[] { itemFind, itemInclude, itemExclude, itemFound, itemCopy } );
                menu.Opening += new CancelEventHandler( menu_Opening );
            }
            {// split
                split = new SplitContainer() {
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                    Orientation = settings.SplitOrientation,
                    Size = Size,
                    Panel1MinSize = 80,
                    Panel2MinSize = 160,
                };
                int dim = (split.Orientation == Orientation.Vertical) ? Size.Width : Size.Height;
                split.SplitterDistance = (int) (dim * settings.SplitRatio);
                split.SplitterMoved += new SplitterEventHandler( split_SplitterMoved );
                split.Resize += new EventHandler( split_Resize );
                components.Add( split );
            }
            {// labelEncoding
                labelEncoding = new Label() {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    BorderStyle = BorderStyle.FixedSingle,
                    AutoSize = false,
                    Enabled = false,
                    Image = Resources.Img_locale,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                components.Add( labelEncoding );
            }
            {// textPath
                textPath = new TextBox() {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    BorderStyle = BorderStyle.FixedSingle,
                    AutoSize = false,
                    ReadOnly = true,
                    TextAlign = HorizontalAlignment.Left,
                };
                components.Add( textPath );
            }
            resetTextLabelWidth();
            {// azuki
                int y = textPath.Height - 1;
                azuki = new AzukiControl() {
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                    Location = new Point( 0, y ),
                    Size = new Size( split.Panel2.Width, split.Panel2.Height - y ),
                    Font = settings.ViewerFont.Clone() as Font,
                    ShowsDirtBar = false,
                    HighlightsCurrentLine = true,
                    ContextMenuStrip = menu,
                    TabWidth = settings.ViewerTabWidth,
                    DrawsEolCode = settings.ViewerShowEOL,
                    DrawsTab = settings.ViewerShowTabs,
                    DrawsSpace = settings.ViewerShowSpaces,
                    DrawsFullWidthSpace = settings.ViewerShowSpaces,
                    ViewType = (settings.ViewerWrapLines) ? ViewType.WrappedProportional : ViewType.Proportional,
                };
                azuki.ColorScheme.SetColor( CharClass.Heading5, Color.Black, Color.Orange );
                azuki.ColorScheme.SetColor( CharClass.Heading6, Color.Black, Color.PaleGreen );
                if (settings.ViewerWrapLines) {
                    azuki.Resize += delegate {
                        azuki.ViewWidth = azuki.ClientSize.Width;
                    };
                }
                azuki_word_proc = settings.ViewerWrapLines && settings.ViewerWrapWords;
                components.Add( azuki );
            }
            {// listview
                list = new FindListView( Query ) {
                    Dock = DockStyle.Fill,
                    UseCompatibleStateImageBehavior = false,
                };
                list.SelectedIndexChanged += new EventHandler( list_SelectedIndexChanged );
                components.Add( list );
            }
            {// add controls
                split.Panel1.Controls.Add( list );
                split.Panel2.Controls.AddRange( new Control[] { textPath, labelEncoding, azuki } );
                Controls.Add( split );
            }
            {// page
                Text = Query.Finder.Text;
                Padding = new Padding( 0, 2, 1, 2 );
                Location = new Point( 4, 23 );
                ImageIndex = (int) TabImageTypeUtil.FromFindType( Query.Type );
                UseVisualStyleBackColor = true;
            }
            ResumeLayout( false );
        }

        protected override void Dispose( bool disposing )
        {
            if (disposed) { return; }
            try {
                if (disposing) {
                    list.SelectedIndexChanged -= new EventHandler( list_SelectedIndexChanged );
                    components.Dispose();
                }
                disposed = true;
            }
            finally {
                base.Dispose( disposing );
            }
        }

        #region Resize

        private void split_SplitterMoved( object sender, SplitterEventArgs e )
        {
            // avoids update when called while resizing
            if (split.Width == split_width) {
                int dim = (split.Orientation == Orientation.Vertical) ? split.Width : split.Height;
                Settings.Default.SplitRatio = (double) split.SplitterDistance / dim;
                resetTextLabelWidth();
            }
        }

        private void split_Resize( object sender, EventArgs e )
        {
            split_width = split.Width;
            resetTextLabelWidth();
        }

        private void resetTextLabelWidth()
        {
            int header_height = Math.Max( labelEncoding.Height, textPath.Height );
            labelEncoding.Size = new Size( 144, header_height );
            labelEncoding.Location = new Point( split.Panel2.Width - labelEncoding.Width, 0 );
            textPath.Size = new Size( split.Panel2.Width - labelEncoding.Width + 1, header_height );
        }

        #endregion
        #region ContextMenu

        /// <summary>
        /// Update Rich context menu items
        /// </summary>
        private void menu_Opening( object sender, System.ComponentModel.CancelEventArgs e )
        {
            bool bSelected;
            {
                int begin, end;
                azuki.GetSelection( out begin, out end );
                bSelected = (begin != end);
            }
            bool bCanQuery = (bSelected && !dispatcher.IsWorking());
            itemCopy.Enabled = bSelected;
            itemFind.Enabled = bCanQuery;
            itemFound.Enabled = bCanQuery;
            itemInclude.Enabled = bCanQuery;
            itemExclude.Enabled = bCanQuery;
        }

        private void invoke( FindType type, FindQuery query )
        {
            dispatcher.InvokeFind( azuki.GetSelectedText(), type, query );
        }

        private void itemCopy_Click( object sender, EventArgs e ) { azuki.Copy(); }
        private void itemFind_Click( object sender, EventArgs e ) { invoke( FindType.NewPath, null ); }
        private void itemFound_Click( object sender, EventArgs e ) { invoke( FindType.FoundPath, Query ); }
        private void itemInclude_Click( object sender, EventArgs e ) { invoke( FindType.FilterInclude, Query ); }
        private void itemExclude_Click( object sender, EventArgs e ) { invoke( FindType.FilterExclude, Query ); }

        #endregion

        public void UpdateList( bool bForce )
        {
            if (list.VirtualMode) {
                list.UpdateListSize( bForce );
            } else {
                //list.BeginUpdate();
                //int begin = list.Items.Count;
                //int end = Query.FoundLines.Count;
                //for (int i = begin; i < end; ++i) {
                //    list.Items.Add( Query.FoundLines[i].Item );
                //}
                //list.EndUpdate();
            }
        }

        private void ShowFoundLine( int idx )
        {
            Cursor = Cursors.WaitCursor;
            WM.SetRedraw( azuki, false );
            {
                FoundLine fl = Query.FoundLines[idx];
                if (SetAzukiContent( fl.FileIdx ) && fl.Line < azuki.LineCount) {
                    // ensure visible (always do this as, even when the line is visible,
                    // the column is not necessarily visible
                    Document doc = azuki.Document;
                    int length = doc.GetLineLength( fl.Line );
                    int column = Math.Min( fl.Column, length );
                    int begin = doc.GetCharIndexFromLineColumnIndex( fl.Line, column );
                    int end = doc.GetCharIndexFromLineColumnIndex( fl.Line, Math.Min( column + fl.Length, length ) );
                    {// bring the selection to the center
                        int vline = azuki.GetLineIndexFromCharIndex( begin );
                        int lines = azuki.View.VisibleTextAreaSize.Height / azuki.LineSpacing;
                        azuki.FirstVisibleLine = Math.Max( vline - lines / 2 + 1, 0 );
                    }
                    doc.SetSelection( begin, end );
                    azuki.ScrollToCaret();
                }
            }
            WM.SetRedraw( azuki, true );
            Cursor = Cursors.Default;
        }

        private bool SetAzukiContent( int fidx )
        {
            if (file_idx == fidx) { return true; }
            {// clear first
                file_idx = -1;
            }
            Document doc = new Document();
            {// prepare new document
                doc.IsReadOnly = true;
                doc.IsRecordingHistory = false;
            }
            {// wrap words
                IWordProc wp = doc.WordProc;
                wp.EnableWordWrap = azuki_word_proc;
                wp.EnableEolHanging = true;
                wp.EnableCharacterHanging = false;
                wp.EnableLineEndRestriction = false;
                wp.EnableLineHeadRestriction = false;
            }
            {// reset
                azuki.Document = doc;
                azuki.ViewWidth = azuki.ClientSize.Width;
            }
            FoundFile file = Query.FoundFiles[fidx];
            {// set info
                labelEncoding.Text = file.Encoding;// +((file.EncodingForced) ? " *" : "");
                labelEncoding.Enabled = true;
            }
            FileContentCache cache = FileCache.Instance.GetContent( file.Info.FullName, file.Encoding );
            if (cache == null) {
                textPath.BackColor = Color.PaleVioletRed;
                textPath.Text = Resources.Mesg_FileOpenFailed + " " + textPath.Text;
                return false;
            }
            if (comparer.Equals( file.Info, cache.Info )) {
                // cache is valid
                textPath.BackColor = SystemColors.Control;
                textPath.Text = file.Info.FullName;
            } else {
                // file has been modified
                textPath.BackColor = Color.PaleVioletRed;
                textPath.Text = Resources.Mesg_FileModified + " " + file.Info.FullName;
            }
            {// set azuki
                doc.Text = cache.Content;
                FoundHighLighter hiliter = new FoundHighLighter( file.Info.FullName, fidx, Query.FoundLines, Query.KeywordFinder );
                if (true) {
                    hiliter.Highlight( doc );
                } else {
                    doc.Highlighter = hiliter;
                }
                doc.SetSelection( 0, 0 );
            }
            file_idx = fidx;
            return true;
        }

        private void list_SelectedIndexChanged( object sender, EventArgs e )
        {
            int sel = list.GetSelectedLineIndex();
            if (sel >= 0) {
                ShowFoundLine( sel );
            }
        }
    }
}
