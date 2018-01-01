using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TabTextFinder.Finder;
using TabTextFinder.Properties;

namespace TabTextFinder.UIControl
{
    enum FindTabImageType
    {
        FindNew,
        FindFound,
        FilterInclude,
        FilterExclude,
        Help,
        CrossColor,
        CrossGray,
    }

    static class TabImageTypeUtil
    {
        static public FindTabImageType FromFindType( FindType type )
        {
            return (FindTabImageType) (int) type;
        }
    }

    class FindTabControl : HistoryTabControl
    {
        private Container components = new Container();
        private ContextMenuStrip menu;
        private ToolStripMenuItem itemCloseAll = new ToolStripMenuItem( Resources.Menu_CloseAllTabs );
        private ToolStripMenuItem itemCloseThis = new ToolStripMenuItem( Resources.Menu_CloseTab, Resources.Img_ui_tab__minus );
        private ToolStripMenuItem itemCloseAllButThis = new ToolStripMenuItem( Resources.Menu_CloseAllTabsButThis );

        // for close button
        private TabPage page_hover;
        private TabPage page_focus;

        public FindTabControl()
        {
            InitializeComponent();

            ImageList = new ImageList( components ) {
                ColorDepth = ColorDepth.Depth32Bit,
            };
            ImageList.Images.AddRange( new Image[] {
                Resources.Img_magnifier,
                Resources.Img_document_search_result,
                Resources.Img_plus_white,
                Resources.Img_minus_white,
                Resources.Img_information,
                Resources.Img_cross_button,
                ImageUtil.GrayScaleImage( Resources.Img_cross_button ),
            } );
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            {// menu items
                itemCloseAll.Click += new EventHandler( itemCloseAll_Click );
                itemCloseThis.Click += new EventHandler( itemCloseThis_Click );
                itemCloseAllButThis.Click += new EventHandler( itemCloseAllButThis_Click );
            }
            {
                menu = new ContextMenuStrip( components );
                menu.Items.AddRange( new ToolStripItem[] { itemCloseThis, itemCloseAllButThis, itemCloseAll } );
            }
            ResumeLayout( false );
        }

        protected override void Dispose( bool disposing )
        {
            if (components != null) {
                components.Dispose();
                components = null;
            }
            // column headers are disposed here
            base.Dispose( disposing );
        }

        public void RemovePage( TabPage page )
        {
            SetMenuItemTag( null );
            TabPages.Remove( page );
            {
                FindTabPage ftp = page as FindTabPage;
                if (ftp != null) {
                    // when closing a tab, abort the query
                    ftp.Query.Abort = true;
                }
            }
            page.Dispose();
        }

        #region IconImageIndex

        private void updateImageIndex( Point point, bool btn_down )
        {
            page_hover = null;
            for (int n = 0; n < TabPages.Count; ++n) {
                if (getImageRect( n ).Contains( point )) {
                    page_hover = TabPages[n];
                    break;
                }
            }
            if (btn_down) {
                page_focus = page_hover;
            }
            for (int n = 0; n < TabCount; ++n) {
                TabPage page = TabPages[n];
                FindTabImageType type;
                if (page == page_focus) {
                    type = (page == page_hover) ? FindTabImageType.CrossGray : FindTabImageType.CrossColor;
                } else if (page == page_hover && page_focus == null) {
                    type = FindTabImageType.CrossColor;
                } else {
                    FindTabPage ftp = page as FindTabPage;
                    if (ftp == null) {
                        type = FindTabImageType.Help;
                    } else {
                        type = TabImageTypeUtil.FromFindType( ftp.Query.Type );
                    }
                }
                int index = (int) type;
                if (page.ImageIndex != index) {
                    page.ImageIndex = index;
                }
            }
        }

        protected override void OnMouseDown( MouseEventArgs e )
        {
            base.OnMouseDown( e );
            if (e.Button == MouseButtons.Left) {
                updateImageIndex( e.Location, true );
            }
        }

        protected override void OnMouseUp( MouseEventArgs e )
        {
            base.OnMouseUp( e );
            updateImageIndex( e.Location, false );
            if (page_focus == page_hover && page_focus != null) {
                RemovePage( page_focus );
            }
            page_focus = null;
        }

        protected override void OnMouseMove( MouseEventArgs e )
        {
            base.OnMouseMove( e );
            updateImageIndex( e.Location, false );
        }

        protected override void OnMouseLeave( EventArgs e )
        {
            base.OnMouseLeave( e );
            updateImageIndex( Point.Empty, false );
        }

        private Rectangle getImageRect( int n )
        {
            Point pt = GetTabRect( n ).Location + new Size( Padding );
            return new Rectangle( pt, ImageList.ImageSize );
        }

        #endregion
        #region ContextMenu

        protected override void OnMouseClick( MouseEventArgs e )
        {
            if (e.Button == MouseButtons.Right) {
                // set the selected page to the menu item
                TabPage page = null;
                for (int i = 0; i < TabPages.Count; ++i) {
                    if (GetTabRect( i ).Contains( e.Location )) {
                        page = TabPages[i];
                        break;
                    }
                }
                SetMenuItemTag( page );
                {
                    itemCloseThis.Enabled = (page != null);
                    itemCloseAllButThis.Enabled = (page != null && TabPages.Count >= 2);
                }
                menu.Show( PointToScreen( e.Location ) );
            }

            base.OnMouseClick( e );
        }

        private void SetMenuItemTag( TabPage page )
        {
            foreach (ToolStripMenuItem item in menu.Items) {
                item.Tag = page;
            }
        }

        private object GetMenuItemTag( object sender )
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            return (item == null) ? null : item.Tag;
        }

        private void RemovePages( Predicate<object> ToRemove )
        {
            foreach (TabPage page in TabPages) {
                if (ToRemove( page )) {
                    RemovePage( page );
                }
            }
        }

        private void itemCloseAll_Click( object sender, EventArgs e )
        {
            RemovePages( ( page ) => true );
        }

        private void itemCloseThis_Click( object sender, EventArgs e )
        {
            TabPage target = GetMenuItemTag( sender ) as TabPage;
            RemovePages( ( page ) => (page == target) );
        }

        private void itemCloseAllButThis_Click( object sender, EventArgs e )
        {
            TabPage target = GetMenuItemTag( sender ) as TabPage;
            RemovePages( ( page ) => (page != target) );
        }

        #endregion
    }

    static class ImageUtil
    {
        private static ImageAttributes gray;

        static ImageUtil()
        {
            const float a = 0.4f;
            const float b = 0.05f;
            gray = new ImageAttributes();
            gray.SetColorMatrix( new ColorMatrix( new float[][] {
                new float[]{ a, b, b, 0, 0 },
                new float[]{ b, a, b, 0, 0 },
                new float[]{ b, b, a, 0, 0 },
                new float[]{ 0, 0, 0, 1, 0 },
                new float[]{ 0, 0, 0, 0, 1 },
            } ) );
        }

        public static Bitmap GrayScaleImage( Image img )
        {
            Bitmap bmp = new Bitmap( img.Width, img.Height );
            using (Graphics g = Graphics.FromImage( bmp )) {
                g.DrawImage( img, new Rectangle( Point.Empty, bmp.Size ), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, gray );
            }
            return bmp;
        }
    }
}
