using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using static System.Net.WebRequestMethods;
using System.Security.Policy;
using System.Diagnostics;
using System.Net;

namespace Alivever.Com.WinBrowserCrawler
{
    public partial class MainDlg : Form
    {
        string[] NewSites_10 = {
            "https://www.nytimes.com",
            //"https://edition.cnn.com",
            //"https://news.sohu.com/",
            //"http://www.news.cn/",
            //"https://mainichi.jp/english/",
            "https://www.foxnews.com",
            //"https://www.washingtonpost.com",
            "https://www.lanacion.com.ar/",
            "https://www.lemonde.fr/",
                "https://elpais.com/",
                //"https://www.bbc.com",
            //"https://www.bbc.co.uk/news",
            //"https://www.ndtv.com/",
            //"https://www.aljazeera.net/",
            //"https://tass.com/" ,
                //"https://www.globo.com/",
                //"https://www.n-tv.de/",
        };

        string[] TopNewSites = { "https://www.nytimes.com",
            "https://www.cnn.com",
            "https://news.sohu.com/",
            "https://www.bbc.co.uk/news",
            "http://www.news.cn/",
            "https://www.foxnews.com",
            "https://www.washingtonpost.com",
            "https://www.nbcnews.com",
            "https://www.usatoday.com",
            "https://www.wsj.com",
            "https://abcnews.go.com",
            "https://www.bloomberg.com",
            "https://www.huffpost.com",
            "https://www.theguardian.com/uk",
            "https://www.telegraph.co.uk",
            "https://www.independent.co.uk",
            "https://www.ft.com",
            "https://www.standard.co.uk",
            "https://www.mirror.co.uk",
            "https://www.express.co.uk",
            "https://www.thetimes.co.uk",
            "https://www.dailymail.co.uk",
            "https://www.xinhuanet.com/",
            "https://www.globaltimes.cn/",
            "https://timesofindia.indiatimes.com/",
            "https://www.ndtv.com/",
            "https://www.spiegel.de/",
            "https://www.faz.net/",
            "https://www.lemonde.fr/",
            "https://www.lefigaro.fr/",
            "https://mainichi.jp/english/",
            "https://www.asahi.com/ajw/",
            "https://www.uol.com.br/",
            "https://www.globo.com/",
            "https://www.rt.com/",
            "https://tass.com/",
            "https://www.cbc.ca/",
            "https://www.theglobeandmail.com/",
            "https://www.abc.net.au/news/",
            "https://www.smh.com.au/",
            "https://www.news24.com/",
            "https://www.iol.co.za/",
            "https://www.eluniversal.com.mx/",
            "https://www.excelsior.com.mx/",
            "https://www.koreatimes.co.kr/",
            "https://www.chosun.com/",
            "https://www.corriere.it/",
            "https://www.repubblica.it/",
            "https://www.elpais.com/",
            "https://www.elperiodico.com/es/",
            "https://punchng.com/",
            "https://www.vanguardngr.com/",
            "https://www.clarin.com/",
            "https://www.lanacion.com.ar/",
            "https://www.arabnews.com/",
            "https://www.alriyadh.com/",
            "https://www.hurriyet.com.tr/",
            "https://www.sabah.com.tr/",
            "https://www.thejakartapost.com/",
            "https://www.kompas.com/",
            "https://www.almasryalyoum.com/",
            "https://www.shorouknews.com/",
            "https://www.dawn.com/",
            "https://www.geo.tv/",
            "https://vnexpress.net/",
            "https://tuoitre.vn/",
            "https://www.bangkokpost.com/",
            "https://www.thaipbsworld.com/",
            "https://www.tvn24.pl/",
            "https://www.rp.pl/",
            "https://www.volkskrant.nl/",
            "https://www.nrc.nl/",
            "https://www.swissinfo.ch/",
            "https://www.tagesanzeiger.ch/",
            "https://www.svt.se/",
            "https://www.aftonbladet.se/",
            "https://www.standaard.be/",
            "https://www.lesoir.be/",
            "https://www.derstandard.at/",
            "https://www.kurier.at/",
            "https://www.vg.no/",
            "https://www.aftenposten.no/",
            "https://www.dr.dk/",
            "https://www.berlingske.dk/",
            "https://yle.fi/uutiset/osasto/news/",
            "https://www.hs.fi/",
            "https://www.ekathimerini.com/",
            "https://www.tovima.gr/",
            "https://www.publico.pt/",
            "https://www.dn.pt/",
            "https://www.rte.ie/news/",
            "https://www.irishtimes.com/",
            "https://www.idnes.cz/",
            "https://www.lidovky.cz/",
            "https://index.hu/",
            "https://444.hu/",
            "https://www.pravda.com.ua/",
            "https://www.unian.info/",
            "https://www.hotnews.ro/",
            "https://adevarul.ro/",
            "https://www.straitstimes.com/",
            "https://www.channelnewsasia.com/",
            "https://www.thestar.com.my/",
            "https://www.nst.com.my/",
            "https://www.philstar.com/",
            "https://news.abs-cbn.com/",
            "https://www.haaretz.com/",
            "https://www.timesofisrael.com/",
            "https://www.thenationalnews.com/",
            "https://gulfnews.com/",
            "https://www.tehrantimes.com/",
            "https://en.mehrnews.com/",
            "https://www.iraqinews.com/",
            "https://www.rudaw.net/english",
            "https://www.dailystar.com.lb/",
            "https://www.aljoumhouria.com/",
            "https://www.jordantimes.com/",
            "https://en.royanews.tv/",
            "https://www.standardmedia.co.ke/",
            "https://www.nation.co.ke/",
            "https://www.latercera.com/",
            "https://www.emol.com/",
            "https://elcomercio.pe/",
            "https://www.larepublica.pe/",
            "https://www.eltiempo.com/",
            "https://www.eltiempo.com/",
            "https://www.elnacional.com/",
            "https://www.eluniversal.com/",
            "https://www.eluniverso.com/",
            "https://www.elcomercio.com/",
            "https://www.lostiempos.com/",
            "https://www.paginasiete.bo/",
            "https://www.ultimahora.com/",
            "https://www.abc.com.py/",
            "https://www.elobservador.com.uy/",
            "https://www.elpais.com.uy/",
            "https://www.nacion.com/",
            "https://www.larepublica.net/",
            "https://www.prensa.com/",
            "https://www.panamaamerica.com.pa/",
            "https://www.prensalibre.com/",
            "https://elperiodico.com.gt/",
            "https://www.laprensa.hn/",
            "https://www.elheraldo.hn/",
            "https://www.elsalvador.com/",
            "https://www.laprensagrafica.com/",
            "https://www.laprensa.com.ni/",
            "https://www.el19digital.com/",
            "https://listindiario.com/",
            "https://www.diariolibre.com/",
            "https://www.cibercuba.com/",
            "https://www.cubadebate.cu/",
            "http://jamaica-gleaner.com/",
            "https://www.jamaicaobserver.com/",
            "https://newsday.co.tt/",
            "https://www.guardian.co.tt/",
            "https://www.nationnews.com/",
            "https://barbadostoday.bb/",
            "https://thenassauguardian.com/",
            "https://ewnews.com/",
            "https://www.stabroeknews.com/",
            "https://www.kaieteurnewsonline.com/",
            "https://www.fijitimes.com/",
            "https://www.fbcnews.com.fj/",
            "https://www.nzherald.co.nz/",
            "https://www.stuff.co.nz/",
            "https://www.thenational.com.pg/",
            "https://postcourier.com.pg/",
            "https://www.samoaobserver.ws/",
            "https://www.sobserver.ws/",
            "https://www.solomonstarnews.com/",
            "https://theislandsun.com.sb/",
            "https://matangitonga.to/",
            "https://kanivatonga.nz/",
            "https://www.dailypost.vu/",
            "https://www.loopvanuatu.com/",
            "https://tuvalunews.tv/",
            "https://www.tuvalutoday.com/",
            "https://www.rnzi.com/",
            "https://www.kiribatijob.com/",
            "https://avas.mv/",
            "https://edition.mv/",
            "https://www.dailymirror.lk/",
            "https://www.dailynews.lk/",
            "https://www.thedailystar.net/",
            "https://www.prothomalo.com/",
            "https://www.mmtimes.com/",
            "https://www.irrawaddy.com/",
            "https://www.khmertimeskh.com/",
            "https://www.phnompenhpost.com/",
            "https://laotiantimes.com/",
            "https://www.vientianetimes.org.la/",
            "https://borneobulletin.com.bn/",
            "https://www.brudirect.com/",
            "https://www.tatoli.tl/en/",
            "https://www.sapo.tl/",
            "https://kuenselonline.com/",
            "https://thebhutanese.bt/",
            "https://www.montsame.mn/en/",
            "https://theubpost.mn/"};

        /// <summary>
        /// Dictionary[string SiteRootURL, CUrlListBrowserDlg] WebForms.
        /// you can use SiteRootURL to get their browser forms.
        /// </summary>
        private readonly Dictionary<string, CUrlListBrowserDlg> WebForms = new Dictionary<string, CUrlListBrowserDlg>();

        private readonly System.Windows.Forms.Timer RefreshCurrentProject2FormTimer = new System.Windows.Forms.Timer();
        //private CSiteTaskMgr siteTaskMgr;//= new CSiteTaskMgr();

        /// <summary>
        /// Dictionary[string siteUrl, Form] 
        /// </summary>
        private Dictionary<string, CUrlListBrowserDlg> Forms = new Dictionary<string, CUrlListBrowserDlg>();

        public MainDlg()
        {
            InitializeComponent();

            RefreshCurrentProject2FormTimer.Interval = 15 * 1000;
            RefreshCurrentProject2FormTimer.Tick += RefreshCurrentProject2FormTimer_Tick;

            LstView_SiteTasks.GridLines = true;
            LstView_SiteTasks.FullRowSelect = true;
            //LstView_SiteTasks.OwnerDraw = true; // Enable owner drawing
            //this.LstView_SiteTasks.DrawSubItem += LstView_SiteTasks_DrawSubItem;

            //this.Menu_File.DropDownItems.Add()
            
        }

        private void RefreshCurrentProject2FormTimer_Tick(object sender, EventArgs e)
        {
            this.Txt_DownloadRoot.Text = CSiteTaskMgr.Ins.StorageRootPath;
            this.Text = CSiteTaskMgr.Ins.StorageRootPath;

            //this.LstView_SiteTasks.BeginUpdate();
            try
            {
                int i = 0;
                foreach (CSiteTask crrSite in
                    CSiteTaskMgr.Ins.GetSiteTasks().OrderBy(a => a.Exe).ThenBy(a => a.SiteRootUrl))
                {
                    lock (crrSite)
                    {
                        ///// get or create current row from/in List View
                        ListViewItem crrRow;
                        if (this.LstView_SiteTasks.Items.Count > i)
                            crrRow = this.LstView_SiteTasks.Items[i];
                        else
                        {
                            crrRow = this.LstView_SiteTasks.Items.Add(" ");

                            for (int j = 0; j < 8; j++)
                                this.LstView_SiteTasks.Items[i].SubItems.Add(" ");
                        }

                        ///// update all columns of this row
                        //site
                        crrRow.SubItems[0].Text = crrSite.SiteRootUrl;

                        crrRow.SubItems[1 ].Text = crrSite.Exe.ToString();

                        // Active
                        crrRow.SubItems[2].Text = crrSite.GetCount_Task_Active().ToString();

                        // Done
                        crrRow.SubItems[3].Text = (crrSite.GetCount_Task_History() - crrSite.GetCount_ErrorUrls()).ToString();

                        // Error
                        crrRow.SubItems[4].Text = crrSite.GetCount_ErrorUrls().ToString();// crrSite.GetCount_ErrorUrls().ToString();

                        // Pending
                        crrRow.SubItems[5].Text = crrSite.GetCount_Task_Pending().ToString();

                        // BlackList
                        crrRow.SubItems[6].Text = $"W{crrSite.GetCount_Whitelist()} B{crrSite.GetCount_Blacklist()}";

                        CPageTask crrPage = crrSite.GetLastPageTask_History();

                        if (crrPage != null)
                        {
                            // Load ms
                            crrRow.SubItems[7].Text = 
                                (crrPage.Load_ms / 1000.1f).ToString("0.0") + $"s/ " +
                                (crrPage.Total_ms / 1000.1f).ToString("0.0") + $"s/ " +
                                $"{crrSite.Cfg_PageTaskInterval_s}s" ;

                            // current page url
                            crrRow.SubItems[8].Text = new Uri(crrPage.Url).PathAndQuery.ToString();
                        }
                        i++;
                    }//lock (crrSite)
                }//foreach( CSiteTask crrSite in 

                RefreshSystemUsageLabel();
            }
            catch(Exception ex) 
            {
                CSiteTaskMgr.Ins.GetLogFile().AppendError("RefreshListTimer_Tick() " + ex.Message);
            }

            //this.LstView_SiteTasks.EndUpdate();

        }//RefreshListTimer_Tick

        private void RefreshSystemUsageLabel()
        {
            if (CSiteTaskMgr.Ins.GetSiteTasks().Count() == 0)
                return;

            float cpuUsage;
            using (PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                // Call NextValue to initialize the PerformanceCounter
                cpuCounter.NextValue();
                // Wait for a short interval to obtain the updated value
                System.Threading.Thread.Sleep(1000); // You can adjust this interval if needed
                                                     // Obtain the current CPU usage percentage
                cpuUsage = cpuCounter.NextValue();
            }

            // Get available memory
            Process currentProcess = Process.GetCurrentProcess();
            double memoryUsed = currentProcess.WorkingSet64 / (double)(1024*1024*1024); //GB

            this.Lab_SystemUsage.Text = $"CPU: {cpuUsage.ToString("0")}% | Mem: {memoryUsed.ToString("0.00")}GB";
            CSiteTaskMgr.Ins.GetLogFile().AppendMessage(this.Lab_SystemUsage.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string rootDir = @"C:\temp\WebSiteDownload";

            //this.Txt_DownloadRoot.Text = rootDir;

            //if ( !Directory.Exists(rootDir))
            //    Directory.CreateDirectory(rootDir);

        }

        private void LstView_SiteTasks_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            
            // Set different colors for sub-items (columns)
            Color foreColor = Color.Black;
            Color backColor = Color.White;

            // Change colors based on column index
            switch (e.ColumnIndex)
            {
                //lan, lv , hong , hei, hong;
                case 1: // Column 2
                    foreColor = Color.Blue;
                    break;
                case 2: // Column 2
                    foreColor = Color.Green;
                    break;
                case 3: // Column 2
                    foreColor = Color.Red;
                    break;
                case 4: // Column 2
                    foreColor = Color.Black;
                    break;
                case 5: // Column 2
                    foreColor = Color.Red;
                    break;
                    // Add more cases for other columns if needed
            }

            // Set the colors for the sub-item
            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, this.LstView_SiteTasks.Font,
                                  e.Bounds, foreColor, TextFormatFlags.Left);

            // Draw the focus rectangle for the sub-item
            if ((e.ItemState & ListViewItemStates.Focused) != 0 && e.ColumnIndex == 0) // Draw focus rectangle only for the first column
            {
                e.DrawFocusRectangle(e.Bounds);
            }
        }

        private void Btn_Test_Click(object sender, EventArgs e)
        {
            //CSiteTask st = new CSiteTask()
            //{
            //    SiteUrlPrefix = "https://www.bbc.com/news",
            //};

            //st.AddNewPageTasks( new List<string>() { "https://www.bbc.com/news" });


            //this.SiteTasks.Add(st.SiteRootUrl, st);
            //if (!Directory.Exists(this.Txt_DownloadRoot.Text) )
            //{
            //    if (MessageBox.Show("Folder doesn't exists. Do you want to create it?",
            //        "Folder not found", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        Directory.CreateDirectory(this.Txt_DownloadRoot.Text);
            //    else
            //        return;
            //}

            ////this.siteTaskMgr = CSiteTaskMgr.Ins;
            //CSiteTaskMgr.Ins.SetStorageRootPath(this.Txt_DownloadRoot.Text);

            //CSiteTask st = CSiteTaskMgr.Ins.AddNewSiteTask("https://news.sohu.com/", null);
            //st.UrlAutoReplaceRules.Add(@"\\u0026spm=smpc\.content\.share\..{22}", string.Empty); // remove spm
            //st.UrlAutoReplaceRules.Add(@"^\/\?spm=smpc\.content\.content.{22}$", string.Empty); // remove spm

            //CSiteTask st = CSiteTaskMgr.Ins.AddNewSiteTask("https://news.sina.com.cn/", null);
            //st.UrlAutoReplaceRules.Add(@"\\u0026spm=smpc\.content\.share\..{22}", string.Empty); // remove spm
            //st.UrlAutoReplaceRules.Add(@"^\/\?spm=smpc\.content\.content.{22}$", string.Empty); // remove spm

            //CSiteTask st = CSiteTaskMgr.Ins.AddNewSiteTask("https://www.bbc.com", null);
            //st.Cfg_PriorityList.Add("https://www.bbc.com/news");
            //st.Cfg_BlackList.Add("https://www.bbc.com/weather");

            CSiteTask st = CSiteTaskMgr.Ins.AddNewSiteTask("https://www.amazon.de/", null);
            st.Cfg_PriorityList.Add("https://www.amazon.de/dp/");
            st.Cfg_PriorityList.Add("https://www.amazon.de/gp/yourstore/");

            st.Cfg_BlackList.Add("https://www.amazon.de/gp/video/");
            st.Cfg_BlackList.Add("https://www.amazon.de/hz/contact-us/");

            st.Cfg_PageTaskInterval_s = 5;

            //this.WebForms.Add(st.SiteRootUrl, ulb);
            CSiteTaskMgr.Ins.Save2File();
            st.Save2File();
            this.OpenNewWindowInMTAThread(st);

            RefreshCurrentProject2FormTimer_Tick(null,null);
            RefreshCurrentProject2FormTimer.Start();
            this.Btn_PauseAll.Visible = true;
            this.Btn_CloseAll.Visible = true;

        }//Btn_Test_Click


        private bool OpenNewWindowInMTAThread(CSiteTask _siteTask)
        {
            if (_siteTask.Exe != EExecuteStatus.Created && _siteTask.Exe != EExecuteStatus.Doing)
            {
                CSiteTaskMgr.Ins.GetLogFile().AppendMessage(
                    $"_siteTask = {_siteTask.SiteRootUrl}. Skip open Window to download, because SiteTask.Exe(={_siteTask.Exe})");
                return false;
            }

            if (this.Forms.ContainsKey(_siteTask.SiteRootUrl))
            {
                if (!this.Forms[_siteTask.SiteRootUrl].IsDisposed)
                    CSiteTaskMgr.Ins.GetLogFile().AppendMessage(
                        $"_siteTask = {_siteTask.SiteRootUrl}. Skip open Window to download, because it is openned.");
                else
                    this.Forms.Remove(_siteTask.SiteRootUrl);
                return false;
            }

            _siteTask.Exe = EExecuteStatus.Doing;
            //_siteTask.CurrentRunTime = DateTime.Now;
            _siteTask.ResetCurrentRunTime();

            Thread thread = new Thread(new ParameterizedThreadStart(OpenNewWindow));
            thread.SetApartmentState(ApartmentState.STA); // 设置单线程单元(STA)模式
            thread.Start(_siteTask);

            return true;
            
            //thread.Join();
        }//buttonOpenNewWindow_Click(object sender, EventArgs e)

        private void OpenNewWindow(object _siteTask)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CSiteTask st = _siteTask as CSiteTask;

            if (this.Forms.ContainsKey(st.SiteRootUrl))
                return;


            CUrlListBrowserDlg ulb = new CUrlListBrowserDlg()
            {
                SiteTask = st

            };
            this.WebForms.Add(st.SiteRootUrl, ulb);
            this.Forms.Add(st.SiteRootUrl, ulb);
            st.Save2File();

            try
            {
                Application.Run(ulb);
            }
            catch( Exception ex )
            {
                StackTrace stackTrace = new StackTrace(ex, true);
                StackFrame frame = stackTrace.GetFrame(0); // Get the top stack frame

                int lineNumber = -1, columnNumber = -1;
                if (frame != null)
                {
                    //string fileName = frame.GetFileName(); // File name where the exception occurred
                    lineNumber = frame.GetFileLineNumber(); // Line number where the exception occurred
                    columnNumber = frame.GetFileColumnNumber(); // Column number where the exception occurred
                }
                st.GetLogFile().AppendError($"OpenNewWindow: " +
                    $"{(st.GetCrrPageTask() == null ? "CrrPageTask==null" : st.GetCrrPageTask().Url)}; " +
                    $"code line {lineNumber}, column{columnNumber}]; " +
                    $"{ex.Message}");
            

            }
            // Show new window
            //ulb.Show();
            
        }//OpenNewWindowInMTAThread()

        private void Btn_Test3Website_Click(object sender, EventArgs e)
        {
            ////this.SiteTasks.Add(st.SiteRootUrl, st);
            //if (!Directory.Exists(this.Txt_DownloadRoot.Text))
            //{
            //    if (MessageBox.Show("Folder doesn't exists. Do you want to create it?",
            //        "Folder not found", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        Directory.CreateDirectory(this.Txt_DownloadRoot.Text);
            //    else
            //        return;
            //}

            ////this.siteTaskMgr = CSiteTaskMgr.Ins;
            //CSiteTaskMgr.Ins.SetStorageRootPath(this.Txt_DownloadRoot.Text);


            CSiteTask st1 = CSiteTaskMgr.Ins.AddNewSiteTask("https://www.bbc.com", null);
            st1.Cfg_PageTaskInterval_s = 5;

            //st.WhiteList.Add("https://news.bbc.com");
            st1.Cfg_PriorityList.Add("https://www.bbc.com/news");
            st1.Cfg_BlackList.Add("https://www.bbc.com/weather");
            st1.Save2File();


            CSiteTask st2 = CSiteTaskMgr.Ins.AddNewSiteTask("https://news.sohu.com/", null);
            st2.Cfg_PageTaskInterval_s = 5;
            st2.Save2File();



            CSiteTask st3 = CSiteTaskMgr.Ins.AddNewSiteTask("https://edition.cnn.com/", null);
            st3.Cfg_PageTaskInterval_s = 5;
            st3.Save2File();

            CSiteTaskMgr.Ins.Save2File();
            RefreshCurrentProject2FormTimer_Tick(null, null);

            this.OpenNewWindowInMTAThread(CSiteTaskMgr.Ins.GetSiteTasks());

            RefreshCurrentProject2FormTimer.Start();
            this.Btn_PauseAll.Visible = true;
            this.Btn_CloseAll.Visible = true;


        }

        void OpenNewWindowInMTAThread( IEnumerable<CSiteTask> siteList)
        {
            //List<string> failList = new List<string>();
            StringBuilder sb = new StringBuilder();
            int i=0;
            sb.AppendLine("Failed Open:");
            foreach (var crrTask in siteList)
            {
                if (this.OpenNewWindowInMTAThread(crrTask))
                    Thread.Sleep(3000);
                else
                {
                    sb.AppendLine(crrTask.SiteRootUrl);
                    i++;
                }
            }//foreach (var crrTask in siteList)

            sb.AppendLine($"Open: total {siteList.Count()} / OK {siteList.Count()-i}/ Fail {i}");


            if (i > 0)
                MessageBox.Show(sb.ToString());

        }

        private void CMBox_SaveDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Txt_DownloadRoot.Text = this.CMBox_SaveDir.Text;
        }

        private void Btn_CreateAllNewsSite_Click(object sender, EventArgs e)
        {
            //this.SiteTasks.Add(st.SiteRootUrl, st);
            //if (!Directory.Exists(this.Txt_DownloadRoot.Text))
            //{
            //    if (MessageBox.Show("Folder doesn't exists. Do you want to create it?",
            //        "Folder not found", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        Directory.CreateDirectory(this.Txt_DownloadRoot.Text);
            //    else
            //        return;
            //}

            ////this.siteTaskMgr = CSiteTaskMgr.Ins;
            //CSiteTaskMgr.Ins.SetStorageRootPath(this.Txt_DownloadRoot.Text);

            int i = 0;
            List<CSiteTask> siteTaskList = new List<CSiteTask>();
            foreach (string crrItem in this.NewSites_10) //this.NewSites_6)
            {
                string[] hightPriorityList = null;
                string crrUrl = crrItem;

                if ( GHelper.IsUrlContainsDirectory(crrUrl) || !crrUrl.Contains("www"))
                {
                    hightPriorityList = new string[1];

                    Uri newHost = new Uri(crrUrl);
                    crrUrl = newHost.GetLeftPart(UriPartial.Authority);
                    hightPriorityList[0] = crrItem;
                }

                CSiteTask st1 = CSiteTaskMgr.Ins.AddNewSiteTask(crrUrl, hightPriorityList);
                //st1.PageTaskInterval_s = 5;

                if (i++ < CSiteTaskMgr.Cfg_MaxSiteTasks)
                {
                    st1.Exe = EExecuteStatus.Created;
                }
                else
                    st1.Exe = EExecuteStatus.Pending;

                st1.Save2File();
                siteTaskList.Add(st1);

            }

            CSiteTaskMgr.Ins.Save2File();

            this.OpenNewWindowInMTAThread(siteTaskList);
            this.RefreshCurrentProject2FormTimer_Tick(null,null);
            RefreshCurrentProject2FormTimer.Start();
            this.Btn_PauseAll.Visible = true;
            this.Btn_CloseAll.Visible = true;

        }//Btn_CreateAllNewsSite_Click()


        private void Btn_PauseAll_Click(object sender, EventArgs e)
        {
            RefreshCurrentProject2FormTimer.Stop();
            this.Btn_GoOnAll.Visible = true;
            this.Btn_PauseAll.Visible = false;
            this.Btn_CloseAll.Visible = true;

            foreach(CUrlListBrowserDlg crrSiteForm in this.Forms.Values)
            {
                crrSiteForm.Btn_Pause_Click(null, null);
            }
        }

        private void Btn_GoOnAll_Click(object sender, EventArgs e)
        {
            RefreshCurrentProject2FormTimer.Start();
            this.Btn_GoOnAll.Visible = false;
            this.Btn_PauseAll.Visible = true;
            this.Btn_CloseAll.Visible = true;

            foreach (CUrlListBrowserDlg crrSiteForm in this.Forms.Values)
            {
                crrSiteForm.Btn_GoOn_Click(null, null);
            }

        }

        private void Btn_CloseAll_Click(object sender, EventArgs e)
        {
            RefreshCurrentProject2FormTimer.Stop();
            this.Btn_GoOnAll.Visible = false;
            this.Btn_PauseAll.Visible = false;
            this.Btn_CloseAll.Visible = false;

            foreach (CUrlListBrowserDlg crrSiteForm in this.Forms.Values)
            {
                crrSiteForm.Btn_Close_Click(null, null);
            }

            CSiteTaskMgr.Ins.Save2File();

            this.Forms.Clear();

        }

        private void MenuItem_NewProject_Click(object sender, EventArgs e)
        {
            if (!this.CloseCurrentProject())
                return;

            ///// select a directory for new project
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            // Set properties for the FolderBrowserDialog
            folderDlg.Description = "Select a folder";
            folderDlg.RootFolder = Environment.SpecialFolder.Desktop; // Set the root folder (e.g., Desktop)
            folderDlg.ShowNewFolderButton = true; // Enable the "New Folder" button
            folderDlg.SelectedPath = "C:\\temp\\";

            // Show the FolderBrowserDialog and check if the user selects a folder
            if (folderDlg.ShowDialog() != DialogResult.OK)
                return;

            string newFolder = folderDlg.SelectedPath;

            if (GHelper.GetLastVersionFile(newFolder, CSiteTaskMgr.FileName_SiteTaskMgr.Replace("{0}","*"))!=null)
            {
                MessageBox.Show(newFolder + "\nYou can't create a new project,\n" +
                    "because one project is already there.\n Please try another Folder.");
                return;
            }

            CSiteTaskMgr.Ins = new CSiteTaskMgr();
            CSiteTaskMgr.Ins.SetStorageRootPath(newFolder);
            CSiteTaskMgr.Ins.Save2File();

            ///// refresh form with new project
            this.RefreshCurrentProject2FormTimer_Tick(null,null);

            ///// show temp buttons
            this.Btn_Test.Visible = true;
            this.Btn_Test3Website.Visible = true;
            this.Btn_CreateAllNewsSite.Visible = true;

            MessageBox.Show("Project created.\nYou can add new sites to download them");
        }//MenuItem_NewProject_Click(object sender, EventArgs e)

        private void MenuItem_LoadProject_Click(object sender, EventArgs e)
        {
            // close current project first.
            if (!this.CloseCurrentProject())
                return;

            // let use to select a new and load it.
            CSiteTaskMgr stm = null;
            //try 
            //{
                stm = this.LoadProject();
            //}
            //catch (Exception ex)
            //{
            //    string logStr = GHelper.PrintUnknowException(ex);
            //    MessageBox.Show(logStr, "Error");
            //}

            if (stm == null)
                return;

            CSiteTaskMgr.Ins = stm;

            //RefreshListTimer_Tick
            this.RefreshCurrentProject2FormTimer_Tick(null, null);
            MessageBox.Show("Project loaded.\nYou can Start to download them");

        }//MenuItem_LoadProject_Click(object sender, EventArgs e)

        /// <summary>
        /// let use to select a new and load it.
        /// </summary>
        /// <returns>
        /// return new project object. return null means use don't want to load. 
        /// It may throw exception if load error.
        /// </returns>
        private CSiteTaskMgr LoadProject()
        {
            ///// select a directory for new project
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            // Set properties for the FolderBrowserDialog
            folderDlg.Description = "Select a folder";
            folderDlg.RootFolder = Environment.SpecialFolder.Desktop; // Set the root folder (e.g., Desktop)
            folderDlg.ShowNewFolderButton = true; // Enable the "New Folder" button
            folderDlg.SelectedPath = "C:\\temp\\";

            // Show the FolderBrowserDialog and check if the user selects a folder
            if (folderDlg.ShowDialog() != DialogResult.OK)
                return null ;

            string newFolder = folderDlg.SelectedPath;

            CSiteTaskMgr stm = null;
            stm = CSiteTaskMgr.LoadFromFolder(newFolder,true, false);

            return stm;
        }//LoadProject()

        private void MenuItem_MergeProject_Click(object sender, EventArgs e)
        {
            if ( CSiteTaskMgr.Ins == null)
            {
                MessageBox.Show("There is no project opend.\t Please open or create a project first.");
                return;
            }

            // close current project first.
            if (!this.CloseCurrentProject())
                return;

            // let use to select a new and load it.
            CSiteTaskMgr stm = null;
            try
            {
                stm = this.LoadProject();
            }
            catch (Exception ex)
            {
                string logStr = GHelper.PrintUnknowException(ex);
                MessageBox.Show(logStr, "Error" );
            }

            if (stm == null)
                return;

            CSiteTaskMgr.Ins.Merge( stm);

            //RefreshListTimer_Tick
            this.RefreshCurrentProject2FormTimer_Tick(null, null);


        }//MenuItem_MergeProject_Click(object sender, EventArgs e)

        //private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        private void MenuItem_UserManual_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_Donate_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_Version_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_CloseProject_Click(object sender, EventArgs e)
        {
            if (CSiteTaskMgr.Ins == null)
            {
                MessageBox.Show("There is no project opend.\t");
                return;
            }

            CloseCurrentProject();

        }

        /// <summary>
        /// Close Current Project. return closed or not.
        /// </summary>
        /// <returns></returns>
        private bool CloseCurrentProject()
        {
            // Check and ask to close current project
            if (this.Forms.Count() != 0)
            {

                if (MessageBox.Show("Stop Downloading",
                    "All Site Download Window should be closed.\n Do you want to Close them? \n It may tasks 30 senconds",
                     MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Btn_CloseAll_Click(null, null);
                    
                    //wait for all Forms are closed.
                    while(true)
                    {
                        //bool isAllClosed = false;
                        if (this.Forms.Values.FirstOrDefault(a => !a.IsDisposed) == null)
                            break;
                    }

                    this.Forms.Clear();
                }
                else
                    return false;
            }

            if (CSiteTaskMgr.Ins != null)
            {
                CSiteTaskMgr.Ins.Save2File();
                CSiteTaskMgr.Ins = null;
            }

            return true;
        }//CloseCurrentProject()

        private void Btn_SelectAllInListView_CheckedChanged(object sender, EventArgs e)
        {
            // Check or uncheck all items in the ListView based on the state of the "Select All" CheckBox
            foreach (ListViewItem item in this.LstView_SiteTasks.Items)
            {
                item.Checked = this.Btn_SelectAllInListView.Checked;
            }
        }

        private void Btn_Activate_Click(object sender, EventArgs e)
        {

        }

        private void Btn_OpenProjectLog_Click(object sender, EventArgs e)
        {
            if (CSiteTaskMgr.Ins == null)
                MessageBox.Show($"Can't open log file.\nNo project openned.");


            string filepath = CSiteTaskMgr.Ins.GetLogFile().GetFileUrl();
            try
            {
                Process.Start("notepad.exe", filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't open log file.\n{ex.Message}\n{filepath}");
            }
        }

        //private void Btn_Start_Click(object sender, EventArgs e)
        //{

        //}

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            RefreshCurrentProject2FormTimer.Start();
            this.Btn_PauseAll.Visible = true;
            this.Btn_CloseAll.Visible = true;

            List<CSiteTask> checkedTasks = GetCheckedSiteTasks();

            if (checkedTasks.Count == 0)
            {
                MessageBox.Show("Please check at least one site.");
                return;
            }

            this.OpenNewWindowInMTAThread(checkedTasks);
        }

        private List<CSiteTask> GetCheckedSiteTasks()
        {
            List<CSiteTask> checkedTasks = new List<CSiteTask>();

            foreach (ListViewItem item in this.LstView_SiteTasks.Items)
            {
                if (item.Checked)
                {
                    checkedTasks.Add(CSiteTaskMgr.Ins.GetSiteTask(item.Text));
                }
            }
            return checkedTasks;
        }//GetCheckedSiteTasks()

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            List<CSiteTask> checkedTasks = GetCheckedSiteTasks();

            if (checkedTasks.Count != 1)
            {
                MessageBox.Show("Please check Only One site.");
                return;
            }

            string siteUrl = checkedTasks.First().SiteRootUrl;

            if ( this.Forms.ContainsKey(siteUrl) && !this.Forms[siteUrl].IsDisposed )
            {
                MessageBox.Show("that task is running. To edit it, \n you have to stop it first.");
                return;
            }

            Form_SiteTaskEditor ste = new Form_SiteTaskEditor(checkedTasks.First());

            ste.ShowDialog();

        }//Btn_Edit_Click(object sender, EventArgs e)

    }//class MainDlg
}
