using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alivever.Com.WinBrowserCrawler
{
    public partial class Form_SiteTaskEditor : Form
    {
        private CSiteTask CrrSiteTask ;

        public Form_SiteTaskEditor(CSiteTask _siteTask)
        {
            InitializeComponent();
            CrrSiteTask = _siteTask;
        }

        private void Form_SiteTaskEditor_Load(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = this.CrrSiteTask;
        }//Form_SiteTaskEditor_Load()

    }//class Form_SiteTaskEditor 
}//namespace
