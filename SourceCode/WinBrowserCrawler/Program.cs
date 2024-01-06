using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alivever.Com.WinBrowserCrawler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            Application.Run(new MainDlg());
#else
            
            try
            {
                Application.Run(new MainDlg());
            }
            catch (Exception ex) 
            {
                GHelper.PrintUnknowException(ex);
            }//catch (Exception ex) 
#endif

        }//Main()

    }//class Program
}//namespace
