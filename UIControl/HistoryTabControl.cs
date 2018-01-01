using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace TabTextFinder.UIControl
{
    /// <summary>
    /// TabControl with tab-selection history in a transparent way.
    /// When one of the tab page is removed, the most-recent-tab will automaticaly be selected.
    /// </summary>
    class HistoryTabControl : TabControl
    {
        private List<TabPage> history = new List<TabPage>();
        private TabPage next_page;

        protected override void OnDeselected( TabControlEventArgs e )
        {
            // When the current page is deselected, add it to the history as the most-recent-page 
            // OnSelecting() will be called next.
            if (e.TabPage != null) {
                history.Insert( 0, e.TabPage );
            }

            base.OnDeselected( e );
        }

        protected override void OnControlRemoved( ControlEventArgs e )
        {
            // When a page is removed, delete its history entry,
            // and reserve the history front (the most-recent-page) for selection.
            // Then, OnSelecting() will be called with the system default SelectedTab,
            // but not OnDeselected().

            // Doing SelectedPage = page; here will let OnDeselected() be called first, and then OnSelecting(),
            // so the system default next page, which is not the user intention, will unwantedly be inserted to history.
            TabPage page = e.Control as TabPage;
            if (page != null) {
                history.Remove( page );
                if (page == SelectedTab) {
                    next_page = (history.Count == 0) ? null : history[0];
                }
            }

            base.OnControlRemoved( e );
        }

        protected override void OnSelecting( TabControlCancelEventArgs e )
        {
            // Change the selection on page removals
            if (next_page != null) {
                SelectedTab = next_page;
                next_page = null;
            }

            // Remove the page being selected from the history
            history.Remove( SelectedTab );
            Debug.Assert( TabPages.Count == 0 || TabPages.Count == history.Count + 1 );

            base.OnSelecting( e );
        }
    }
}
