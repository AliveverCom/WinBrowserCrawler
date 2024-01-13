using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Mime;

namespace Alivever.Com.WinBrowserCrawler
{
    public partial class CUrlListBrowserDlg : Form
    {

        #region Forbiden cookies
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        private const int INTERNET_OPTION_SUPPRESS_BEHAVIOR = 81;
        private const int INTERNET_SUPPRESS_COOKIE_PERSIST = 3;

        private void SuppressCookies()
        {
            int option = INTERNET_SUPPRESS_COOKIE_PERSIST;
            int size = sizeof(int);
            IntPtr optionPtr = Marshal.AllocCoTaskMem(size);
            Marshal.WriteInt32(optionPtr, option);

            InternetSetOption(0, INTERNET_OPTION_SUPPRESS_BEHAVIOR, optionPtr, size);

            Marshal.Release(optionPtr);
        }
        #endregion Forbiden cookies


        public CSiteTask SiteTask;

        private CPageTask CrrPageTask = null;

        private readonly System.Windows.Forms.Timer PageLoadTimer = new System.Windows.Forms.Timer();

        private bool IsPageLoaded = false;

        private DateTime PageLoadTimer_BeginTime;

        private readonly System.Windows.Forms.Timer RunNextTasksTimer = new System.Windows.Forms.Timer();

        private readonly System.Windows.Forms.Timer AutoSaveSiteTasksTimer = new System.Windows.Forms.Timer();

        private int nProcessPage = 0;
        private int nNavigatePage = 0;

        private string LastNavigateUrl = string.Empty;

        private DateTime LastNavigateTime = DateTime.Now;

        private bool IsPaused = false;

        private bool m_IsPauseFinished = false;

        public bool IsPauseFinished { get { return this.m_IsPauseFinished; } }

        public CUrlListBrowserDlg()
        {
            InitializeComponent();
            //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            //webBrowser1.Navigating += WebBrowser1_Navigating;
            webBrowser1.ScriptErrorsSuppressed = true;

            AutoSaveSiteTasksTimer.Tick += AutoSaveSiteTasksTimer_Tick;
            RunNextTasksTimer.Tick += RunNextTasks_Tick;
            this.PageLoadTimer.Tick += PageLoadTimer_Tick;

            //ResetRunNextTasksTimer();
        }

        /// <summary>
        /// reset RunNextTasksTimer for next run. If this.IsPaused == true, then it will not run. 
        /// if you do want to go on running, please use ForceGoOnRunning()
        /// </summary>
        /// <param name="bForceGoOn"></param>
        private void ResetRunNextTasksTimer()
        {
            if ( this.IsPaused == true)
                return;

            this.SetFormTitle("ResetRunNextTasksTimer(); ");

            RunNextTasksTimer.Stop();
            RunNextTasksTimer.Start(); 
        }

        ///// <summary>
        ///// Cancel Pause status and  reset RunNextTasksTimer for next run.  
        ///// </summary>
        ///// <param name="bForceGoOn"></param>
        //private void ForceGoOnRunning()
        //{
        //    this.IsPaused = false;

        //    this.ResetRunNextTasksTimer();
        //}


        private void RunNextTasks_Tick(object sender, EventArgs e)
        {
            this.SetFormTitle("RunNextTasks_Tick(); ");

            try
            {
                this.RunNextTasks();
            }
            catch(Exception ex)
            {
                // Get stack trace information
                LogUnexpectedError(ex);

                ForceSkipCurrentPageTask(true, true);

            }
        }

        private void LogUnexpectedError(Exception ex)
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
            this.SiteTask.GetLogFile().AppendError($"Unexpected: " +
                $"{(CrrPageTask == null ? "CrrPageTask==null" : CrrPageTask.Url)}; " +
                $"code line {lineNumber}, column{columnNumber}]; " +
                $"{ex.Message}");
        }

        private void RunNextTasks()
        {
             RunNextTasksTimer.Stop();
           //Thread.Sleep(this.SiteTask.PageTaskInterval_s * 1000);

            //this.SiteTask. SaveFile_TasksActive();
            CrrPageTask = SiteTask.GetNextPageTask_Created();
            this.SiteTask.SetCrrPageTask(CrrPageTask);

            // if no any page task, then set SiteTask.Acitve to false and close this form.
            if (CrrPageTask == null)
            {
                this.SiteTask.GetLogFile().AppendWarning("No anmore Url available. Form will be closed.");
                this.PageLoadTimer.Dispose();
                this.RunNextTasksTimer.Dispose();
                this.AutoSaveSiteTasksTimer.Dispose();
                this.webBrowser1.Dispose();
                this.webBrowser1 = null; 

                this.SiteTask.Exe = EExecuteStatus.Finshed;
                this.SiteTask.Save2File();
                //this.SiteTask.Dispose();

                this.Close();
                GC.Collect();
                return ;
            }

            string docType = this.SiteTask.GetPageParser().GetContentType(CrrPageTask.Url);
            CrrPageTask.DocType = docType;

            if (docType == null || ! docType.StartsWith("text/html", StringComparison.OrdinalIgnoreCase))  
            {
                CrrPageTask.DocType = docType;
                this.SiteTask.FinishActivePageTask(CrrPageTask.Url, EExecuteStatus.Canceled, false);
                this.SiteTask.GetLogFile().AppendWarning($"Cancel PageTask because docType = {docType}, not 'text/html'");
                return ;
            }

                CrrPageTask.Exe = EExecuteStatus.Doing;

            if (CrrPageTask == null)
                return;

            this.PageLoadTimer_BeginTime = DateTime.Now;
            this.CrrPageTask.DownTime = DateTime.Now;//CLogFile.GetTimeStr_FileName();

            // Set DocumentText to an empty string before loading a new page
            //this.webBrowser1.DocumentText = string.Empty;

            this.IsPageLoaded = false;
            this.webBrowser1.Navigate(CrrPageTask.Url);
            this.LastNavigateUrl = CrrPageTask.Url;
            this.LastNavigateTime = DateTime.Now;
            nNavigatePage++;

            this.PageLoadTimer.Stop();
            this.PageLoadTimer.Start();


        }//RunAllTasks()

        //private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{

        //}
        private void SetFormTitle(string _str)
        {
            this.Text = $"F{this.nProcessPage}; Site: " +
                $"A{this.SiteTask.GetCount_Task_Active()}/ " +
                $"H{this.SiteTask.GetCount_Task_History()}/ " +
                $"P{this.SiteTask.GetCount_Task_Pending()}/ " +
                $"E{this.SiteTask.GetCount_ErrorUrls()}; " +
                (this.CrrPageTask==null? "": this.CrrPageTask.Load_ms.ToString()) + "ms; " +
                _str + "; " +
                this.LastNavigateUrl;
        }

        private  void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.SetFormTitle("WebBrowser_DocumentCompleted()");
            this.RunNextTasksTimer.Stop();
            this.PageLoadTimer.Stop();
            this.webBrowser1.Stop();


            //// if this site si paused. do nothing.
            if (this.IsPaused == true)
                return;


            if (IsPageLoaded)
            {
                this.SiteTask.GetLogFile().AppendWarning(
                    "Get DocumentCompleted again after processed: "+ this.LastNavigateUrl);

                //this.SiteTask.AddErrorUrl(this.LastNavigateUrl);
                ForceSkipCurrentPageTask(true, false);
                return;
            }
            else
                IsPageLoaded = true;


            try
            {
                //DisableAutoPlayVideos();

                if (this.IsCurrentBrowserDocError())
                {
                    ForceSkipCurrentPageTask(true, false);
                    return;
                }

                string docUrl;
                    docUrl = this.webBrowser1.Document.Url.ToString();

                if (docUrl != null &&!docUrl.StartsWith( this.LastNavigateUrl))
                {
                    this.SiteTask.GetLogFile().AppendWarning(
                        $"PageTask redirected but still can be processed. " +
                        $"Navigate = {this.LastNavigateUrl} ; Skipped = {this.webBrowser1.Document.Url?.AbsoluteUri} ;" +
                    $"IsPageLoaded= {IsPageLoaded} ; ProcessPage= {nProcessPage}; NavigatePage= {this.nNavigatePage}");
                    this.SiteTask.AddErrorUrl(this.LastNavigateUrl);
                }

                if (this.SiteTask.Cfg_IsDisableLoadImages)
                    DisableImages(webBrowser1.ActiveXInstance);

               // await Task.Delay(2000);


                // Web page has completed loading
                //PageLoadTimer.Stop();


                // Process the loaded content
                ProcessLoadedPage();
                ResetRunNextTasksTimer();

            }
            catch (Exception ex)
            {
                // Get stack trace information
                LogUnexpectedError(ex);

                ForceSkipCurrentPageTask(true, false);
            }

        }

        private void ForceSkipCurrentPageTask(bool bResetNextPageTimer, bool bLogDetails)
        {

            webBrowser1.Stop();


            if (this.CrrPageTask != null)
            {
                this.SiteTask.FinishActivePageTask(this.CrrPageTask);
                this.CrrPageTask.Exe = EExecuteStatus.Error;
                
            }
            else
            {
                this.SiteTask.FinishActivePageTask(this.LastNavigateUrl, EExecuteStatus.Error, false);
            }

            if (bLogDetails)
                this.SiteTask.GetLogFile().AppendWarning(
                    "Force Skip Current Page Task."+
                    this.LastNavigateUrl);

            this.PageLoadTimer.Stop();
            this.RunNextTasksTimer.Stop();
            this.CrrPageTask = null;
            //this.SiteTask.SetCrrPageTask(CrrPageTask);
            //this.SiteTask.Save2File();
            //this.IsPageLoaded = false;

            if (bResetNextPageTimer)
                ResetRunNextTasksTimer();
        }//ForceSkipCurrentPageTask()

        private void DisableAutoPlayVideos()
        {
            try
            {
                // inject JavaScript to WebBrowser and run
                this.webBrowser1.Document.InvokeScript("eval", new object[] {
                @"
                var videos = document.getElementsByTagName('video');
                for (var i = 0; i < videos.length; i++) {
                    videos[i].autoplay = false;
                    videos[i].preload = 'none'; // 可选：停止预加载视频
                }
                "
                });
            }
            catch
            {
                this.SiteTask.GetLogFile().AppendError( "Failed to Disable Auto Play Videos. URL= " + this.LastNavigateUrl );
            }
        }
        private void PageLoadTimer_Tick(object sender, EventArgs e)
        {
                //DisableAutoPlayVideos();
            this.SetFormTitle("PageLoadTimer_Tick(); ");

            //// if this site si paused. do nothing.
            if (this.IsPaused == true)
                return;

            // Timeout reached

            if (IsPageLoaded)
                return;

            this.webBrowser1.Stop();
            this.RunNextTasksTimer.Stop();
            PageLoadTimer.Stop();
            ForceSkipCurrentPageTask(true, true);


            //try
            //{

            //    // chek if url load error
            //    if (this.IsCurrentBrowserDocError())
            //        return;

            //    if (this.SiteTask.Cfg_IsDisableLoadImages)
            //        DisableImages(webBrowser1.ActiveXInstance);


            //    /// Page hasn't finished loading within the specified time
            //    /// Treat the current loading content as loaded
            //    ProcessLoadedPage();
            //    ResetRunNextTasksTimer();
            //}
            //catch (Exception ex)
            //{
            //    // Get stack trace information
            //    LogUnexpectedError(ex);

            //    ForceSkipCurrentPageTask(true,false);

            //}


        }//PageLoadTimer_Tick(object sender, EventArgs e)

        private bool IsCurrentBrowserDocError()
        {
            lock (webBrowser1)
            {

                if (this.CrrPageTask == null)
                {
                    if (this.webBrowser1.Document.Url?.ToString() != this.LastNavigateUrl)
                    //&& this.webBrowser1.Document.Url?.ToString() != this.LastNavigateUrl+'/')
                    {
                        this.SiteTask.GetLogFile().AppendWarning(
                            $"PageTask redirected. " +
                            $"Navigate = {this.LastNavigateUrl} ; Skipped = {this.webBrowser1.Document.Url?.AbsoluteUri} ;" +
                        $"IsPageLoaded= {IsPageLoaded} ; ProcessPage= {nProcessPage}; NavigatePage= {this.nNavigatePage}");
                        this.SiteTask.AddErrorUrl(this.LastNavigateUrl);
                    }
                    else
                    {
                        this.SiteTask.GetLogFile().AppendError(
                            $"IsCurrentBrowserDocError() CrrPageTask==null ;" +
                            $"Navigate = {this.LastNavigateUrl} ; Skipped = {this.webBrowser1.Document.Url?.AbsoluteUri} ;" +
                        $"IsPageLoaded= {IsPageLoaded} ; ProcessPage= {nProcessPage}; NavigatePage= {this.nNavigatePage}");
                        this.SiteTask.AddErrorUrl(this.LastNavigateUrl);

                    }

                    nProcessPage++;
                    return true;
                }

                //string title2Lower = title2Lower = webBrowser1.Document.Title;
                
                //title2Lower = title2Lower.ToLower();

                //if (webBrowser1.Document != null
                //&& (title2Lower.Contains("error") || title2Lower.Contains("404")))
                //{
                //    this.SiteTask.GetLogFile().AppendWarning(
                //        $"404 or url error. " +
                //        $"Navigate = {this.LastNavigateUrl} ; Skipped = {this.webBrowser1.Document.Url?.AbsoluteUri} ;" +
                //        $"IsPageLoaded= {IsPageLoaded} ; ProcessPage= {nProcessPage}; NavigatePage= {this.nNavigatePage}");
                //    this.SiteTask.AddErrorUrl(this.LastNavigateUrl);

                //    this.CrrPageTask.Exe = EExecuteStatus.Error;
                //    this.SiteTask.FinishActivePageTask(this.CrrPageTask);
                //    this.SiteTask.AddErrorUrl(this.CrrPageTask.Url);
                //    return true;
                //}


                //if (this.webBrowser1.Document == null || this.webBrowser1.Document.Body == null)
                //{
                //    this.SiteTask.GetLogFile().AppendError(
                //        $"Document.Body==null ;" +
                //        $"Navigate= {this.LastNavigateUrl} ; " +
                //        $"IsPageLoaded= {IsPageLoaded} ; ProcessPage= {nProcessPage}; NavigatePage= {this.nNavigatePage}");

                //    this.SiteTask.AddErrorUrl(this.CrrPageTask.Url);

                //    this.CrrPageTask.Exe = EExecuteStatus.Error;
                //    this.SiteTask.FinishActivePageTask(this.CrrPageTask);
                //    nProcessPage++;
                //    return true;
                //}
            }//lock (webBrowser1.Document)

            return false;
        }//IsCurrentBrowserDocError()

        private void ProcessLoadedPage()
        {
            //// if this site si paused. do nothing.
            if (this.IsPaused == true)
                return;


            // Implement your logic for handling loaded content here
            // For example, get the title, language, and encoding

            //this.CrrPageTask.SetFinished( EExecuteStatus.Finshed);

            //this.CrrPageTask.Title = webBrowser1.DocumentTitle;

            //this.CrrPageTask.Langrage = webBrowser1.Document.Body?.GetAttribute("lang");

            //this.CrrPageTask.Encode = webBrowser1.Document.Encoding;

            //// Extract all the links (URLs)
            //List<string> links = CPageTask.ExtractLinks(webBrowser1.Document);

            //// Extract all the image URLs
            //List<string> imageUrls = CPageTask.ExtractImageUrls(webBrowser1.Document);

            //List<string> innerLikes = this.SiteTask.GetInnerURLs(links);
            //List<string> outLines = this.SiteTask.GetInnerURLs(links);

            //List<string> innerImageLikes = this.SiteTask.GetInnerURLs(imageUrls);
            //List<string> outImageLines = this.SiteTask.GetInnerURLs(imageUrls);

            //this.CrrPageTask.InnerUrls = innerLikes.Count;
            //this.CrrPageTask.OutUrls= outLines.Count;
            //this.CrrPageTask.InnerImages = innerLikes.Count;
            //this.CrrPageTask.OutImages = outLines.Count;
            //string htmlText = webBrowser1.DocumentText;
            //this.CrrPageTask.Bytes = htmlText.Length;
            //this.CrrPageTask.Load_ms = (int) ((DateTime.Now - this.PageLoadTimer_BeginTime).TotalMilliseconds);


            //this.CrrPageTask.SetPageHtmlStr(webBrowser1.DocumentText);
            this.CrrPageTask.Load_ms = (int)((DateTime.Now - this.PageLoadTimer_BeginTime).TotalMilliseconds);
            this.CrrPageTask.Exe = EExecuteStatus.Finshed;

            //// 4. general a new PageInfo to parse html page.
            CPageInfo pi = new CPageInfo();
            pi.FromPageTask(this.CrrPageTask);

            ///// 5. Parse everythin from Html code.
            pi.Exe = EExecuteStatus.Finshed;
            var parseRst = this.SiteTask.GetPageParser().
            ParsingHtmlDocument(webBrowser1.Document, this.SiteTask, pi);

            // Get the file extension from the file name
            //if (!string.IsNullOrEmpty(pi.))
            //    _pi.FileExt = Path.GetExtension(fileName);


            // Get the file name from the URL
            //string fileName = Path.GetFileName(webBrowser1.Document.Url.AbsolutePath);


            if (!string.IsNullOrEmpty(pi.Text) )
                pi.Exe = EExecuteStatus.Parsed;


            List<string> innerLinkes = parseRst.Item1; 
            List<string> outLinkes = parseRst.Item2; 
            List<string> innerImageLinkes = parseRst.Item3; 
            List<string> outImageLinkes = parseRst.Item4;

            this.CrrPageTask.SetPageHtmlStr(pi.HtmlMD5, pi.HtmlKB);

            if (innerLinkes.Count == 0)
                this.SiteTask.GetLogFile().AppendError($"Unnormal 0 inner links: {this.CrrPageTask.Url}");

            if (innerLinkes.Count > 100)
                this.SiteTask.GetLogFile().AppendWarning($"{innerLinkes.Count} inner links? Mybe List or Protol " +
                    $"{this.CrrPageTask.Url}");


            //if (innerImageLinkes.Count == 0)
            //    this.SiteTask.GetLogFile().AppendWarning($"Unnormal 0 image: {this.CrrPageTask.Url}");

            if (innerImageLinkes.Count >100)
                this.SiteTask.GetLogFile().AppendWarning($"Why {innerImageLinkes.Count} images? " +
                    $"{this.CrrPageTask.Url}");

            // if html content is duplicated in History with other pages, then don't save page html file.
            if (this.SiteTask.HasSameHtmlInHistory(pi.HtmlMD5))
            {
                this.CrrPageTask.Exe = EExecuteStatus.Duplicated;
                pi.Exe = EExecuteStatus.Duplicated;
            }
            else
            {
                string pageHtml=null;
                lock(webBrowser1)
                {
                    pageHtml = webBrowser1.DocumentText;
                }

                //// 1. write html text into file.
                pi.SavePage2File_Html(this.SiteTask.GetCrrRun_SiteStorageRoot(),
                    pageHtml,
                    this.SiteTask.Cfg_maxPathDeep);

                this.SiteTask.GetLogFile().AppendMessage($"Saved File: {pi.Url} ; " +
                    $"DocType={pi.DocType} ; {pi.HtmlKB}KB");
            }

            //// 5. append PageInfo into PageInfoList file.
            this.SiteTask.AppendPageInfo2File(pi);


            this.CrrPageTask.SetFinished(EExecuteStatus.Finshed);
            //this.CrrPageTask.SavedFile = pi.SavedFile;

            if (this.SiteTask.Cfg_IsAutoDeepMiningInnerUrls)
            {
                this.CrrPageTask.NewHtmLnk = this.SiteTask.AddNewPageTasks(this.CrrPageTask.Url, innerLinkes);
                //tmp++;
            }

            if ( this.SiteTask.Cfg_IsAutoWidenMiningOuterUrls )
                CSiteTaskMgr.Ins.AddNewPageTasks(this.CrrPageTask.Url, outLinkes);

            this.CrrPageTask.Total_ms = (int)(DateTime.Now - this.LastNavigateTime).TotalMilliseconds;
            this.SiteTask.FinishActivePageTask(this.CrrPageTask);

            this.CrrPageTask = null;
            this.SiteTask.SetCrrPageTask(null);


            nProcessPage++;
            //RunNextTasks();
        }//ProcessLoadedContent()


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void AutoSaveSiteTasksTimer_Tick(object sender, EventArgs e)
        {
            if (this.SiteTask.Exe !=  EExecuteStatus.Doing)
            {
                this.AutoSaveSiteTasksTimer.Stop();
                return;
            }

            try
            {
                this.SiteTask.Save2File();

                
            }
            catch (Exception ex)
            {
                // Get stack trace information
                LogUnexpectedError(ex);
            }

        }

        private void UrlListBrowserDlg_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

            if (this.SiteTask.Exe == EExecuteStatus.Doing || this.SiteTask.Exe == EExecuteStatus.Created)
                this.SiteTask.Exe = EExecuteStatus.Doing;
            else
            {
                this.SiteTask.GetLogFile().AppendError(
                    $"UrlListBrowserDlg_Load( SiteTask.Exe = {this.SiteTask.Exe} ). CUrlListBrowserDlg will not run and will be closed onLoading, and SiteTask.Dispose(). ");
                //this.SiteTask.Dispose();
                this.Close();
            }

                SuppressCookies();
            //set timer
            AutoSaveSiteTasksTimer.Interval = this.SiteTask.Cfg_SiteTaskInterval_s * 1000;


            RunNextTasksTimer.Interval = this.SiteTask.Cfg_PageTaskInterval_s * 1000;

            this.PageLoadTimer.Interval = this.SiteTask.Cfg_MaxPageLoadTimeout_s * 1000;

            //this.RunNextTasks();
            this.ResetRunNextTasksTimer();
            this.AutoSaveSiteTasksTimer.Start();
        }

        public void Btn_GoOn_Click(object sender, EventArgs e)
        {
            if (!this.IsPaused)
                return;

            if ( !this.m_IsPauseFinished )
            {
                MessageBox.Show("Can't Go on. Pause is not finished.");
                return;
            }

            this.Btn_GoOn.Visible = false;


            this.SetFormTitle("doing GoOn()");
            this.SiteTask.GetLogFile().AppendWarning(this.Text);
            //this.RunNextTasks();

            this.m_IsPauseFinished = false;
            this.IsPaused = false;

            ResetRunNextTasksTimer();

            this.Btn_GoOn.Visible = false;
            this.Btn_Pause.Visible = true;

        }

        #region Disable WebBrowser to load images

        //private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        //{
        //    //Intercept resource requests and cancel loading if it is an image resource
        //    if (e.Url != null && IsImageUrl(e.Url.AbsoluteUri))
        //    {
        //        e.Cancel = true; // Cancel loading image resources

        //    }
        //}

        //// Determine whether the URL is an image resource
        //private bool IsImageUrl(string url)
        //{
        //    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        //    foreach (string extension in imageExtensions)
        //    {
        //        if (url.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        [ComImport]
        [Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        public interface IWebBrowser2
        {
            void put_Property([MarshalAs(UnmanagedType.BStr)] string property, ref object value);
            // 其他方法和属性...
        }

        private void DisableImages(object browser)
        {
            if (browser != null)
            {
                try
                {
                    IWebBrowser2 webBrowser2 = (IWebBrowser2)browser;
                    object flags = 0; // 0 表示启用图片加载，1 表示禁用图片加载
                    webBrowser2.put_Property("Image", ref flags);
                    Console.WriteLine("已禁用图片加载");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("发生异常：" + ex.Message);
                }
            }
        }


        #endregion//Disable WebBrowser to load images

        public void Btn_Pause_Click(object sender, EventArgs e)
        {
            this.SetFormTitle("doing Fause(); ");
            this.SiteTask.GetLogFile().AppendWarning(Text);
            ForceSkipCurrentPageTask(false, true);

            this.SiteTask.Save2File();

            this.Btn_GoOn.Visible= false;

             this.m_IsPauseFinished = false;

            this.IsPaused = true;


            this.AutoSaveSiteTasksTimer.Stop();
            this.m_IsPauseFinished = true;

            this.Btn_GoOn.Visible = true;
            this.Btn_Pause.Visible = false;

        }

        public void Btn_Close_Click(object sender, EventArgs e)
        {

            Btn_Pause_Click(null, null);

            this.SetFormTitle("doing Close(); ");
            this.SiteTask.GetLogFile().AppendWarning(Text);
            this.SiteTask.Save2File();

            this.Close();
            //this.webBrowser1.Dispose();
        }

        private void Btn_GloFile_Click(object sender, EventArgs e)
        {
            string filepath = this.SiteTask.GetLogFile().GetFileUrl();
            try
            {
                Process.Start("notepad.exe", filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't open log file.\n{ex.Message}\n{filepath}");
            }

        }
    }//class UrlListBrowserDlg
}//namespace
