using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

namespace TabTextFinder.UIControl
{
    /// <summary>
    /// Helper class for combobox containing  text items only.
    /// </summary>
    class TextComboBox : ComboBox
    {
        private const int MaxCount = 32;

        // prevent the text being selected on resize
        protected override void OnResize( EventArgs e )
        {
            if (!Focused) {
                SelectionLength = 0;
            }

            base.OnResize( e );
        }

        // add the current Text to Items
        public void AddTextToItems( string text )
        {
            while (true) {
                int idx = -1;
                for (int i = 0; i < Items.Count; ++i) {
                    if (text == Items[i] as string) {
                        idx = i;
                        break;
                    }
                }
                if (idx < 0) { break; }
                Items.RemoveAt( idx );
            }
            Items.Insert( 0, text );
            Text = text;
        }

        public void AddTextToItems()
        {
            AddTextToItems( Text );
        }

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public StringCollection TextItems
        {
            get
            {
                StringCollection texts = new StringCollection();
                foreach (object str in Items) {
                    texts.Add( (string) str );
                    if (texts.Count >= MaxCount) { break; }
                }
                return texts;
            }

            set
            {
                BeginUpdate();

                Items.Clear();
                if (value != null) {
                    foreach (string str in value) {
                        Items.Add( str );
                        if (Items.Count >= MaxCount) { break; }
                    }
                }

                if (Items.Count > 0) {
                    Text = (string) Items[0];
                } else {
                    Text = null;
                }

                EndUpdate();
            }
        }
    }
}
