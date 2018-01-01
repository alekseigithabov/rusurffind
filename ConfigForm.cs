using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Sgry.Azuki;
using Sgry.Azuki.WinForms;
using TabTextFinder.Properties;

namespace TabTextFinder
{
    partial class ConfigForm : Form
    {
        private Orientation layout { get { return (btnLayoutVert.Checked) ? Orientation.Vertical : Orientation.Horizontal; } }

        private AzukiControl azuki;

        private void exchgSettings( bool save, Settings settings )
        {
            SettingDataExchange.ExchgText( save, settings, "EditorCommand", textEditorCommand );
            SettingDataExchange.ExchgText( save, settings, "EditorArgument", textEditorArgument );
            SettingDataExchange.ExchgChecked( save, settings, "ListPathFullName", chkListFullPath );
            SettingDataExchange.ExchgTextInt( save, settings, "ListPreviewMaxCharsLeft", numMaxCharsLeft );
            SettingDataExchange.ExchgTextInt( save, settings, "ListPreviewMaxCharsRight", numMaxCharsRight );
            SettingDataExchange.ExchgChecked( save, settings, "ViewerShowEOL", chkShowEol );
            SettingDataExchange.ExchgChecked( save, settings, "ViewerShowTabs", chkShowTab );
            SettingDataExchange.ExchgChecked( save, settings, "ViewerShowSpaces", chkShowSpc );
            SettingDataExchange.ExchgChecked( save, settings, "ViewerWrapLines", chkWrapLines );
            SettingDataExchange.ExchgChecked( save, settings, "ViewerWrapWords", chkWrapWords );
            SettingDataExchange.ExchgTextInt( save, settings, "ViewerTabWidth", numTabWidth );
            SettingDataExchange.ExchgChecked( save, settings, "FileCacheEnable", chkCacheFile );
            SettingDataExchange.ExchgTextLong( save, settings, "FileCacheSize", numCacheSize );
        }

        public ConfigForm()
        {
            InitializeComponent();
            Icon = Icon.FromHandle( Resources.Img_wrench_screwdriver.GetHicon() );
            {// list
                numMaxCharsLeft.Maximum = 512;
                numMaxCharsRight.Maximum = 512;
            }
            {// azuki
                azuki = new AzukiControl() {
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                    Size = labelAzuki.Size,
                    Location = labelAzuki.Location,
                    IsReadOnly = true,
                    IsRecordingHistory = false,
                    ShowsDirtBar = false,
                    HighlightsCurrentLine = false,
                    ContextMenuStrip = null,
                    Text = Resources.Label_WrapSample,
                };
                tabViewer.Controls.Add( azuki );
            }
        }

        public void LoadFromSettings( Settings settings )
        {
            exchgSettings( false, settings );

            btnLayoutVert.Checked = (settings.SplitOrientation == Orientation.Vertical);
            btnLayoutHorz.Checked = (settings.SplitOrientation == Orientation.Horizontal);

            setViewerFont( settings.ViewerFont.Clone() as Font );
        }

        public void SaveToSettings( Settings settings )
        {
            exchgSettings( true, settings );

            settings.SplitOrientation = layout;
            settings.ViewerFont = azuki.Font;
        }

        public bool IsFileCacheSettingsChanged( Settings settings )
        {
            return settings.FileCacheSize != (long) numCacheSize.Value
                || settings.FileCacheEnable != chkCacheFile.Checked;
        }

        private void ConfigForm_Load( object sender, EventArgs e )
        {
            UpdateControls();
            UpdateViewerControls();
        }
        // general & advanced
        private void chkCacheFile_CheckedChanged( object sender, EventArgs e ) { UpdateControls(); }
        // Viewer
        private void numTabWidth_ValueChanged( object sender, EventArgs e ) { UpdateViewerControls(); }
        private void chkShowEol_CheckedChanged( object sender, EventArgs e ) { UpdateViewerControls(); }
        private void chkShowTab_CheckedChanged( object sender, EventArgs e ) { UpdateViewerControls(); }
        private void chkShowSpc_CheckedChanged( object sender, EventArgs e ) { UpdateViewerControls(); }
        private void chkWrapLines_CheckedChanged( object sender, EventArgs e ) { UpdateViewerControls(); }
        private void chkWrapWords_CheckedChanged( object sender, EventArgs e ) { UpdateViewerControls(); }

        private void UpdateControls()
        {
            {// file cache
                bool bEnable = chkCacheFile.Checked;
                numCacheSize.Enabled = bEnable;
                labelCacheSize.Enabled = bEnable;
                labelCacheSizeUnit.Enabled = bEnable;
            }
        }

        private void UpdateViewerControls()
        {
            {// appearance
                azuki.DrawsEolCode = chkShowEol.Checked;
                azuki.DrawsTab = chkShowTab.Checked;
                azuki.DrawsSpace = chkShowSpc.Checked;
                azuki.DrawsFullWidthSpace = chkShowSpc.Checked;
            }
            {// tab width
                azuki.TabWidth = (int) numTabWidth.Value;
            }
            {// text wrapping
                bool bWrapLines = chkWrapLines.Checked;
                bool bWrapWords = bWrapLines && chkWrapWords.Checked;
                chkWrapWords.Enabled = bWrapLines;
                labelWrapAnd.Enabled = bWrapLines;
                {// word wrapping
                    IWordProc wp = azuki.Document.WordProc;
                    wp.EnableWordWrap = bWrapWords;
                    wp.EnableEolHanging = true;
                    wp.EnableCharacterHanging = false;
                    wp.EnableLineEndRestriction = false;
                    wp.EnableLineHeadRestriction = false;
                }
                {// apply word proc hack
                    int first_line = azuki.FirstVisibleLine;
                    azuki.ViewType = (bWrapLines) ? ViewType.WrappedProportional : ViewType.Proportional;
                    azuki.ViewWidth = azuki.ClientSize.Width;
                    azuki.Text = azuki.Text;
                    azuki.FirstVisibleLine = first_line;
                }
            }
        }

        private void btnBrowse_Click( object sender, EventArgs e )
        {
            using (OpenFileDialog dlg = new OpenFileDialog()) {
                if (dlg.ShowDialog( this ) == DialogResult.OK) {
                    textEditorCommand.Text = dlg.FileName;
                }
            }
        }

        #region Font
        private delegate void SetFontDelegate( Font font );
        private void btnFont( Font font, SetFontDelegate set_font )
        {
            using (FontDialog dlg = new FontDialog()) {
                dlg.Font = font;
                dlg.Apply += delegate { set_font( dlg.Font ); };
                dlg.ShowApply = true;
                dlg.ShowEffects = false;
                dlg.AllowVerticalFonts = false;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    set_font( dlg.Font );
                } else {
                    set_font( font );
                }
            }
        }

        private void setFontTextName( TextBox text, Font font )
        {
            text.Text = string.Format( "{0}, {1}pt", font.Name, font.SizeInPoints );
        }

        private void setViewerFont( Font font )
        {
            azuki.Font = font;
            azuki.Refresh();
            setFontTextName( textViewerFontName, font );
        }

        private void btnViewerFont_Click( object sender, EventArgs e ) { btnFont( azuki.Font, setViewerFont ); }
        #endregion
    }

    static class SettingDataExchange
    {
        private static T getProp<T>( object obj, string name )
        {
            PropertyInfo prop = obj.GetType().GetProperty( name );
            return (T) prop.GetValue( obj, null );
        }

        private static void setProp<T>( object obj, string name, T value )
        {
            PropertyInfo prop = obj.GetType().GetProperty( name );
            prop.SetValue( obj, value, null );
        }

        private static void exchg<T>( bool save, Settings settings, string key, object ctrl, string name )
        {
            if (save) {
                settings[key] = getProp<T>( ctrl, name );
            } else {
                setProp<T>( ctrl, name, (T) settings[key] );
            }
        }

        public static void ExchgText( bool save, Settings settings, string key, object ctrl )
        {
            exchg<string>( save, settings, key, ctrl, "Text" );
        }

        public static void ExchgChecked( bool save, Settings settings, string key, object ctrl )
        {
            exchg<bool>( save, settings, key, ctrl, "Checked" );
        }

        #region "Text" Properties
        private static string getTextProp( object obj )
        {
            return getProp<string>( obj, "Text" );
        }

        private static void setTextProp( object obj, string value )
        {
            setProp<string>( obj, "Text", value );
        }

        public static void ExchgTextInt( bool save, Settings settings, string key, object ctrl )
        {
            if (save) {
                settings[key] = int.Parse( getTextProp( ctrl ) );
            } else {
                setTextProp( ctrl, ((int) settings[key]).ToString() );
            }
        }

        public static void ExchgTextLong( bool save, Settings settings, string key, object ctrl )
        {
            if (save) {
                settings[key] = long.Parse( getTextProp( ctrl ) );
            } else {
                setTextProp( ctrl, ((long) settings[key]).ToString() );
            }
        }
        #endregion
    }
}
