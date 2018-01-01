using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TabTextFinder.Properties;

namespace TabTextFinder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            if (VerifySettings( Settings.Default ) == false) { return; }
            Application.Run( new TabTextFinderForm() );
        }

        static bool VerifySettings( Settings settings )
        {
            bool bPassed = false;
            try {// to load any property
                System.Drawing.Size size = settings.FormSize;
                bPassed = true;
            }
            catch (Exception e) {
                Assembly asm = Assembly.GetExecutingAssembly();
                AssemblyCompanyAttribute company = (AssemblyCompanyAttribute) Attribute.GetCustomAttribute( asm, typeof( AssemblyCompanyAttribute ) );
                string path = Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData )
                    + "\\" + company.Company
                    + "\\" + Path.GetFileName( System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName )
                    + "_StrongName_udcc0m3nt0vi4koz20hplq3exc0ibo0o"
                    + "\\" + asm.GetName().Version.ToString()
                    + "\\user.config";
                string mesg = Resources.Mesg_InvalidSettings + "\n" + path;
                MessageBox.Show( mesg, Resources.Title_Application, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            return bPassed;
        }
    }
}
