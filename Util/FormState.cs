using System;
using System.Drawing;
using System.Windows.Forms;
using TabTextFinder.Properties;

namespace TabTextFinder.Util
{
    class FormState
    {
        private Form form;
        private Settings settings;

        private Size size;
        private Point location;
        private FormWindowState state;

        public FormState( Form form, Settings settings )
        {
            this.form = form;
            this.settings = settings;

            form.Load += new EventHandler( form_Load );

            Load( settings );
            Restore();
        }

        private void form_Load( object sender, EventArgs e )
        {
            form.Move += new EventHandler( form_StateChanged );
            form.SizeChanged += new EventHandler( form_StateChanged );
            form.FormClosing += new FormClosingEventHandler( form_FormClosing );
        }

        private void form_FormClosing( object sender, FormClosingEventArgs e )
        {
            Store();
            Save( settings );
            settings.Save();
        }

        private void form_StateChanged( object sender, EventArgs e )
        {
            Store();
        }

        private void Store()
        {
            state = form.WindowState;
            if (state == FormWindowState.Normal) {
                size = form.Size;
                location = form.Location;
            }
        }

        private void Restore()
        {
            // should be here as Size/Location change will call Store()
            if (state != FormWindowState.Minimized) {
                form.WindowState = state;
            }
            bool visible = false;
            {
                Rectangle rc = new Rectangle( location, size );
                foreach (Screen scr in Screen.AllScreens) {
                    if (scr.Bounds.IntersectsWith( rc )) {
                        visible = true;
                        break;
                    }
                }
            }
            form.Size = size;
            form.Location = location;
            form.StartPosition = (visible) ? FormStartPosition.Manual : FormStartPosition.WindowsDefaultLocation;
        }

        private void Save( Settings settings )
        {
            settings.FormSize = size;
            settings.FormState = state;
            settings.FormLocation = location;
        }

        private void Load( Settings settings )
        {
            size = settings.FormSize;
            state = settings.FormState;
            location = settings.FormLocation;
        }
    }
}
