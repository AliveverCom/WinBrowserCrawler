using Alivever.Com.WinBrowserCrawler.PageParser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace Alivever.Com.WinBrowserCrawler
{
    public class CSiteTask
    {
        #region Public Attributes

        //public string SiteUrlPrefix;

        private string m_SiteRootUrl;

        [Browsable(false)]
        public string SiteRootUrl { 
            get { return this.m_SiteRootUrl; } 
            set { this.m_SiteRootUrl = value; } 
        }

        #region Task runned Attributes
        /// <summary>
        /// indicate wether this site task is working or not.
        /// true means this site has some page tasks or some root pages are waiting for next timer.
        /// false means this task will not run anymore,and will not be loaded when CSiteTaskMgr is loading.
        /// </summary>
        public EExecuteStatus Exe = EExecuteStatus.Created; //public bool IsActive = true;

        /// <summary>
        /// the time that this task is created. this time will never be changed.
        /// </summary>
        [ReadOnly(true)]
        public DateTime CreateTime = DateTime.Now;

        /// <summary>
        /// a task may be executed for many times. this time will be updated when task is reloaded.
        /// </summary>
        [ReadOnly(true)]
        public DateTime CurrentRunTime = DateTime.Now;

        #endregion //Task runned Attributes

        #region Config Attributes

        /// <summary>
        /// how long will the spider sleep between 2 tasks.
        /// minimum shoud be this.Min_PageTaskInterval_s; any value less then Min_PageTaskInterval_s will be see as Min_PageTaskInterval_s. 
        /// </summary>
        public int Cfg_PageTaskInterval_s = 6;

        public int Cfg_SiteTaskInterval_s = 60 * 60;

        /// <summary>
        /// how long will the spider try to reload SiteRootUrl.
        /// minimum shoud be this.Min_SiteRootReloadInterval_s; if value (0,Min_SiteRootReloadInterval_s) will be see as Min_SiteRootReloadInterval_s.
        /// if value = -1, it will not be reloaded.
        /// </summary>
        public int Cfg_PeriodicallyPages_Interval_s = 60 * 30;

        /// <summary>
        /// if page loding time over this value, the browser will think timeout and failed.
        /// </summary>
        public int Cfg_MaxPageLoadTimeout_s = 120;

        [Browsable(false)]
        public const int Cfg_Min_SiteRootReloadInterval_s = 600;

        [Browsable(false)]
        public const int Cfg_Min_PageTaskInterval_s = 1;

        /// <summary>
        /// after how many time a page or url erro, it will be added into ErrorUrls.
        /// </summary>
        public int Cfg_MaxPageError2BlackList = 10;


        /// <summary>
        /// True means if it find more new url of this site, all these new url will be added as new page tasks of this site. 
        /// False means no new url will be add. if all tasks are finished, the spider will be closed.
        /// </summary>
        public bool Cfg_IsAutoDeepMiningInnerUrls = true;

        public bool Cfg_IsAutoWidenMiningOuterUrls = false;


        /// <summary>
        /// True: when this site is first loaded, the browser will stoped and wait for manually login, then it will go on downloading pages.
        /// False: the browser will directly downloading pages without any stop for manual operation.
        /// </summary>
        public bool Cfg_IsNeedLogin = false;

        /// <summary>
        /// true means web browser will not load image when download html pages
        /// </summary>
        public bool Cfg_IsDisableLoadImages = false;


        /// <summary>
        /// All url begin with any string in this list will take high priority for downloading -- add into Tasks_Active .
        /// anything not string in this list will be added ubti Tasks_pending;
        /// </summary>
        public List<string> Cfg_PriorityList = new List<string>();


        /// <summary>
        /// All url begin with any string in this list will take canceled -- add into Tasks_History.
        /// </summary>
        public List<string> Cfg_BlackList = new List<string>();

        /// <summary>
        /// all pages in this is should be refresh Periodically. 
        /// the timer will follow this.PeriodicallyPages_Interval_s.
        /// </summary>
        public List<string> Cfg_PeriodicallyPages = new List<string>();

        /// <summary>
        /// Dictionary[string RegularExpression,string ReplaceToBe>.
        /// this attribute will be used AddNewPageTask() to check if replaced url is duplicated from history.
        /// </summary>
        public Dictionary<string, string> Cfg_UrlAutoReplaceRules = new Dictionary<string, string>();

        /// <summary>
        /// How many folder level will you follow the url folders?
        /// it because some website have so many folder deeps.
        /// I recommand you to set it as 1 or 2. 
        /// -1 means all pass as same as url.
        /// </summary>
        public short Cfg_maxPathDeep = 1;

        /// <summary>
        /// this value will be updated only when this task is saving to file, 
        /// so that user can read and know how may items in such list.
        /// Don't read this attribute in runtime!
        /// </summary>
        [Browsable(false)]
        public int Cnt_PendingPageTasks = -1;

        /// <summary>
        /// this value will be updated only when this task is saving to file, 
        /// so that user can read and know how may items in such list.
        /// Don't read this attribute in runtime!
        /// </summary>
        [Browsable(false)]
        public int Cnt_ActivePageTasks = -1;

        /// <summary>
        /// this value will be updated only when this task is saving to file, 
        /// so that user can read and know how may items in such list.
        /// Don't read this attribute in runtime!
        /// </summary>
        [Browsable(false)]
        public int Cnt_HistoryPageTasks = -1;


        /// <summary>
        /// this value will be updated only when this task is saving to file, 
        /// so that user can read and know how may items in such list.
        /// Don't read this attribute in runtime!
        /// </summary>
        [Browsable(false)]
        public int Cnt_ErrorUrls = -1;

        /// <summary>
        /// True means if downloader find some link belong to the same MainDomain,
        /// but the second level Domain is different, then that link will also be downloaded.
        /// </summary>
        public bool IsExtend2SaveMainDomain = true;


        #endregion // configure attributes

        ///// <summary>
        ///// one Run can Save multi times. base on this.SiteTaskInterval_s
        ///// </summary>
        //public DateTime LastSaveTime = DateTime.Now;
        public int LastPageTaskId = 1;

        #endregion //Public Attributes

        #region Runtime private Attributes



        //private string PageText;

        /// <summary>
        /// low priority for downloading. they will be processed only when Tasks_Active is empty.
        /// </summary>
        private CPageTaskList Tasks_Pending = new CPageTaskList();

        /// <summary>
        /// high priority for downloading
        /// </summary>
        private CPageTaskList Tasks_Active = new CPageTaskList();

        /// <summary>
        /// it saves tasks are done: finished, error, canceled.
        /// after one item is add into this list, it will also be append to list file.
        /// </summary>
        private CPageTaskList Tasks_History = new CPageTaskList();

        /// <summary>
        /// all error page url 
        /// </summary>
        private Dictionary<string, Int32> ErrorUrls = new  Dictionary<string, int>();

        /// <summary>
        /// it is the page now downloading.
        /// </summary>
        private CPageTask CrrPageTask;

        private CLogFile LogFile = new CLogFile();

        private string CrrRunProject_StorageRoot;

        private string MainDomain;

        private CPageParser_Base PageParser;

        #endregion//Runtime private Attributes

        #region static attributes
        private static readonly string FileName_PageTasks_History = "History_PageTasks_{0}.txt";
        //private static readonly string PageTasks_Active_FileName = "PageTasks_Active_Jsonlist.txt";
        private static readonly string FileName_PageTasks_Active = "Active_PageTasks_{0}.txt";
        private static readonly string FileName_PageTasks_Pending = "Pending_PageTasks_{0}.txt";
        private static readonly string FileName_PageInfos = "PageInfos_{0}.txt";
        private static readonly string FileName_SiteTask = "SiteTask_{0}.json";
        private static readonly string FileName_LogFile = "SiteLog_{0}.log";
        private static readonly string FileName_ErrorUrl = "ErrorUrl_{0}.csv";

        private static readonly string[] SkipUrlEndWith = new string[] 
            { ".jpg", ".xml", ".css", ".json", ".zip", ".rar", ".7z", ".pdf", ".pptx","/rss" };
        //private static readonly string SubDir_Run = "Run_{0}";

        #endregion//static attributes

        public void ResetCurrentRunTime()
        {
            this.CurrentRunTime = DateTime.Now;
            this.ResetPageTaskListFiles();
        }

        public void SetCrrRunProject_StorageRoot(string dir) 
        { 
            this.CrrRunProject_StorageRoot = dir; 
            this.ResetPageTaskListFiles();
        }

        public string GetCrrRun_SiteStorageRoot()
        {
            return Path.Combine(this.CrrRunProject_StorageRoot, this.GetSiteCodeName() );
        }

        public int GetNewPageTaskId() { return ++this.LastPageTaskId; }

        public CPageTask GetCrrPageTask() { return this.CrrPageTask; }

        public void SetCrrPageTask(CPageTask pt) {  this.CrrPageTask = pt; }

        public int GetCount_Task_Active()
        {
            this.Cnt_ActivePageTasks = this.Tasks_Active.Count();
            return this.Cnt_ActivePageTasks;
        }

        public int GetCount_Task_History()
        {
            this.Cnt_HistoryPageTasks = this.Tasks_History.Count();
            return this.Cnt_HistoryPageTasks;
        }

        public int GetCount_Task_Pending()
        {
            this.Cnt_PendingPageTasks = this.Tasks_Pending.Count();
            return this.Cnt_PendingPageTasks;
        }

        public int GetCount_ErrorUrls()
        {
            this.Cnt_ErrorUrls = this.ErrorUrls.Count();
            return this.Cnt_ErrorUrls;
        }

        public int GetCount_Blacklist()
        {
            this.Cnt_ErrorUrls = this.Cfg_BlackList.Count();
            return this.Cnt_ErrorUrls;
        }

        public int GetCount_Whitelist()
        {
            this.Cnt_ErrorUrls = this.Cfg_PriorityList.Count();
            return this.Cnt_ErrorUrls;
        }

        public CPageTask GetLastPageTask_History()
        {
            return this.Tasks_History.LastOrDefault();
        }
        /// <summary>
        /// add url into ErrorUrl, and append a record into ErrorUrl file.csv
        /// if a url error too many times. then automaicly add this url into black list.
        /// </summary>
        /// <param name="url"></param>
        /// <returns> how many times this error happend.</returns>
        public int AddErrorUrl (string url, int nPlus=1)
        {
            if (this.ErrorUrls.ContainsKey(url))
                this.ErrorUrls[url] += nPlus;
            else
                this.ErrorUrls.Add( url, nPlus);

            // if a url error too many times. then automaicly add this url into black list.
            if ( this.ErrorUrls[url] > Cfg_MaxPageError2BlackList
                && !this.Cfg_BlackList.Contains(url)
                && !this.Cfg_PriorityList.Contains(url))
                this.Cfg_BlackList.Add(url);

            //append csv file
            string filePath = this.GetFullFileName( FileName_ErrorUrl);
            string line = $"{this.ErrorUrls[url]}\t{url}\n";
            File.AppendAllText(filePath, line, Encoding.ASCII);

            return this.ErrorUrls[url];
        }//AddErrorUrl (string url)

        public void RedownloadPeriodicallyPages()
        {
            foreach (var crrUrl in this.Cfg_PeriodicallyPages)
            {
                if (!this.AddNewPageTasks2ActiveTaskList_force(crrUrl))
                    this.LogFile.AppendError($"RedownloadPeriodicallyPages() => " +
                        $"AddNewPageTasks2ActiveTaskList_force({crrUrl})");
            }
        }//RedownloadPeriodicallyPages()

        public CSiteTask()
        {

        }

        private void ResetPageTaskListFiles()
        {
            this.Tasks_Active.SetAsAutoSaveFile(this.GetFullFileName( FileName_PageTasks_Active));
            this.Tasks_History.SetAsAutoSaveFile(this.GetFullFileName( FileName_PageTasks_History));
            this.Tasks_Pending.SetAsAutoSaveFile(this.GetFullFileName( FileName_PageTasks_Pending));
            this.LogFile.GenerateFileUrl(this.GetFullFileName(FileName_LogFile));//, CLogFile.GetTimeStr_FileName(this.CurrentRunTime)););

            //FileName_LogFile);
        }

        public CLogFile GetLogFile() { return this.LogFile; }

        ///// <summary>
        ///// debug function.
        ///// save current Tasks_Active into a text file.
        ///// each page task will be a row line
        ///// </summary>
        //public void SaveFile_TasksActive()
        //{
        //    string dirpath = Path.Combine(this.GetStorageRootDir(), "debug");
        //    if (!Directory.Exists(dirpath))
        //        Directory.CreateDirectory(dirpath);

        //    string filename = Path.Combine(dirpath, PageTasks_Active_FilePrefix + CLogFile.GetTimeStr_FileName() + ".json");
        //    List<string> rstList = new List<string>();

        //    foreach (CPageTask crrTask in this.Tasks_Active.TaskList)
        //        rstList.Add( CLogFile.ToJson(crrTask));

        //    File.WriteAllLines(filename, rstList, Encoding.UTF8);
        //}

        //private string GetFullFileName_PageInfos()
        //{
        //    return Path.Combine(this.GetStorageRootDir(),
        //       string.Format(PageInfos_FileName, CLogFile.GetTimeStr_FileName(this.CurrentRunTime) ));
        //}

        //private string GetFullFileName_PageTasks_Active()
        //{
        //    return Path.Combine(this.GetStorageRootDir(), 
        //        string.Format( PageTasks_Active_FileName, CLogFile.GetTimeStr_FileName( this.CurrentRunTime)) );
        //}

        //private string GetFullFileName_PageTasks_History()
        //{
        //    return Path.Combine(this.GetStorageRootDir(),
        //        string.Format(PageTasks_History_FileName, CLogFile.GetTimeStr_FileName(this.CurrentRunTime) )); ;
        //}

        //private string GetFullFileName_PageTasks_Pending()
        //{
        //    return Path.Combine(this.GetStorageRootDir(),
        //        string.Format(PageTasks_Pending_FileName, CLogFile.GetTimeStr_FileName(this.CurrentRunTime) )); ;
        //}

        private string GetFullFileName(
            //string siteTaskMgr_StorageRoot,
            string filenameTemplate)
        {
            //this.GetStorageRootDir()
            return Path.Combine(this.GetCrrRun_SiteStorageRoot(), //siteTaskMgr_StorageRoot == null? this.GetStorageRootDir() : siteTaskMgr_StorageRoot,
                string.Format(filenameTemplate, CLogFile.GetTimeStr_FileName(this.CurrentRunTime))); ;
        }


        //public string GetPageText() { return this.PageText; }

        //public void SetPageText(string str) { this.PageText = str; }

        public string GenerateStorageRootDir()
        {
            string rootDir = Path.Combine(CSiteTaskMgr.Ins.StorageRootPath, this.GetSiteCodeName());
            if (Directory.Exists(rootDir))
                Directory.CreateDirectory(rootDir);

            return rootDir;
        }

        public bool  SetSiteRootUrl(string rootUrl) 
        { 
            this.m_SiteRootUrl = rootUrl;
            if (LogFile == null)
                this.LogFile = new CLogFile();

            ResetPageTaskListFiles();
            //this.SiteUrlPrefix= rootUrl;

            this.MainDomain = GHelper.GetMainHost(rootUrl);
            this.PageParser = CPageParser_Factory.CreatePageParser(MainDomain);

            //this.LogFile.GenerateFileUrl(this.GetCrrRun_SiteStorageRoot(), FileName_LogFile );
            this.LogFile.AppendMessage($"Begin SiteTask. SiteRootUrl = {this.SiteRootUrl}");

            //add  rootUrl as the first page task.
            if (! this.AddNewPageTasks2ActiveTaskList_force(rootUrl))
                {
                    this.LogFile.AppendError($"SetSiteRootUrl( rootUrl = {rootUrl} ) failed.");
                    return false;
                }


            return true;
        }

        /// <summary>
        /// get a new page tash that should be executed.
        /// return null if no any new task found.
        /// </summary>
        public CPageTask GetNextPageTask_Created()
        {
            CPageTask rst = this.Tasks_Active.GetNextPageTask_Created();

            // if no active task, then try one pending tasks.
            if (rst == null)
            {
                rst = this.Tasks_Pending.GetNextPageTask_Created();

                if (rst != null)
                {
                    this.Tasks_Active.Add(rst);
                    this.Tasks_Pending.Remove(rst);
                }
            }//if (rst == null)

            return rst;

        }//GetNextPageTask_Created()

        /// <summary>
        /// filter all inner site urls from urlList, which is parsed from a site page.
        /// </summary>
        /// <param name="urlList"></param>
        /// <returns></returns>
        public List<string> GetInnerURLs(List<string> urlList) 
        { 
            return (from a in urlList 
                    where (this.IsBelongToThisHost(a)  || a.IndexOf("://")< 0)
                        && Uri.IsWellFormedUriString(a, UriKind.RelativeOrAbsolute)
                        && !a.StartsWith("javascript:") && !a.StartsWith("mailto:")
                    select a).ToList();
        }

        /// <summary>
        /// filter all out site urls from urlList, which is parsed from a site page.
        /// </summary>
        /// <param name="urlList"></param>
        /// <returns></returns>
        public List<string> GetOutURLs(List<string> urlList)
        {
            return (from a in urlList 
                    where a.IndexOf("://")>-1 && !this.IsBelongToThisHost(a)
                        && Uri.IsWellFormedUriString(a, UriKind.RelativeOrAbsolute) 
                    select a).ToList();
        }

        /// <summary>
        /// all urls as new CPageTask object into PageTasks. Automatically remove duplicates url.
        /// return objects count that is added.
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public int AddNewPageTasks(string baseUrl, IEnumerable<string> urls)
        {
            int i = 0;
            //string fixSharp = this.SiteUrlPrefix + "#";
            foreach (var crrUrl in urls)
            {
                i += this.AddNewPageTasks(baseUrl ,crrUrl) != null ? 1 : 0;
                
            }


            return i;
        }//AddNewPageTasks(List<string> urls)

        /// <summary>
        /// directly all crrUrl into this.Tasks_Active. 
        /// Without any check. Note! crrUrl must be an Absolute path, 
        /// otherwhise it will be egnored..
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="crrUrl"></param>
        /// <returns></returns>
        public bool AddNewPageTasks2ActiveTaskList_force(string crrUrl)
        {
            Uri result;
            if (! Uri.TryCreate(crrUrl, UriKind.Absolute, out result))
            {
                this.LogFile.AppendError($"it's not an Absolute URL. AddNewPageTasks2ActiveTaskList_force( crrUrl = {crrUrl} )");
                return false;
            }

            if ( !this.IsBelongToThisHost(crrUrl))
            {
                this.LogFile.AppendError($"URL's host is not current site. AddNewPageTasks2ActiveTaskList_force( crrUrl = {crrUrl} )");
                return false;
            }

            CPageTask pt = new CPageTask() { TaskId = this.GetNewPageTaskId() };
            pt.SetPageUrl(crrUrl);

            //if(this.Tasks_Active.Contains(crrUrl))

            this.Tasks_Active.Add(pt,false);

            return true;
        }


        /// <summary>
        /// all urls as new CPageTask object into PageTasks. Automatically remove duplicates url.
        /// return objects count that is added.
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public CPageTask AddNewPageTasks(string baseUrl, string crrUrl)
        {
            string originalUrl = crrUrl;

            crrUrl = GetAbsoluteUrl(baseUrl, crrUrl);

            int nSharp = crrUrl.IndexOf('#');
            if (nSharp > -1)
                crrUrl = crrUrl.Remove(nSharp);

            crrUrl = crrUrl.Trim();
            if (string.IsNullOrEmpty(crrUrl))
                return null;

            crrUrl = GetAbsoluteUrl(baseUrl, crrUrl);

            if (crrUrl == null)
            {
                this.LogFile.AppendMessage("AddNewPageTasks ignored 1: " + originalUrl);
                return null;
            }
            // skip if url is not belong to current site, or url is only a page mark as #
            if (  ( IsExtend2SaveMainDomain && !this.IsBelongToThisHost(crrUrl) )
                || (!IsExtend2SaveMainDomain && !crrUrl.StartsWith(this.SiteRootUrl)) )
                //|| !crrUrl.StartsWith(this.SiteRootUrl))
            {
                string str = "AddNewPageTasks url is not in site. url= " + crrUrl + "; siteUrl= " + this.SiteRootUrl;
                this.LogFile.AppendError(str);
                return null;

            }

            string endStr = SkipUrlEndWith.FirstOrDefault(a => crrUrl.EndsWith(a));
            if (endStr != null)
            {
                this.LogFile.AppendWarning($"AddNewPageTasks Skip URL end with '{endStr}'; URL= {crrUrl}");
                return null;
            }

            // skip if url is already in the task list.
            if (this.IsPageUrlExist(crrUrl))
                return null;

            CPageTask pt = new CPageTask() { TaskId = this.GetNewPageTaskId() };

            pt.SetPageUrl(crrUrl);

            //// add CPageTask into one task list
            string replacedUrl = GHelper.AutoReplaceUrlByCfg(crrUrl, this.Cfg_UrlAutoReplaceRules);

            // if replacedUrl is duplicated with exist urls, then add it into Tasks_History
            if (this.IsPageUrlExist(replacedUrl))
            {
                pt.Exe = EExecuteStatus.Duplicated;
                this.Tasks_History.Add(pt);
            }
            else // otherwise try to add it into other task lists.
            {
                if (this.Cfg_PriorityList.Count() == 0 || this.IsUrlBelong2List(crrUrl, this.Cfg_PriorityList))
                    this.Tasks_Active.Add(pt);
                else if (this.IsUrlBelong2List(crrUrl, this.Cfg_BlackList))
                    pt = null;
                else
                    this.Tasks_Pending.Add(pt);
            }

            return pt;
        }//AddNewPageTasks(string crrUrl)

        public CPageParser_Base GetPageParser()
        {
            return this.PageParser;
        }

        public bool IsBelongToThisHost(string crrUrl)
        {
            //string siteHost = GHelper.GetMainHost(this.SiteRootUrl);
            string urlHost = GHelper.GetMainHost(crrUrl);

            return this.MainDomain == urlHost;
        }


        #region Finalizer

        /// <summary>
        /// Finalizer
        /// </summary>
        ~CSiteTask()
        {
            Dispose(false);
        }

        // IDisposable 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// clear memory buffered resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Tasks_Active.Clear();
                this.Tasks_History.Clear();
                this.Tasks_Pending.Clear();
                this.Cfg_PriorityList.Clear();
                this.Cfg_BlackList.Clear();
            }
            // Release unmanaged resources
            // ...
        }

        #endregion //finalizer

        private bool IsUrlBelong2List( string url, IEnumerable<string> list)
        {
            if (list == null || list.Count() == 0)
                return false;

            var rst = list.FirstOrDefault(a => url.StartsWith(a));

            if (rst == null)
                return false;
            else
                return true;
        }

        public  bool IsUrlInDomain(string absoluteUrl)
        {
           return IsUrlInDomain( (new Uri(this.SiteRootUrl)).Host.ToLower(), absoluteUrl);
        }

        public static bool IsUrlInDomain(string domain, string absoluteUrl)
        {
            Uri uri;
            if (Uri.TryCreate(absoluteUrl, UriKind.Absolute, out uri))
            {
                string host = uri.Host.ToLower(); 
                return host.EndsWith(domain.ToLower()) || host.Equals(domain.ToLower());
            }
            else
            {
                //Invalid URL
                return false;
            }
        }
        static string GetAbsoluteUrl(string baseUrl, string inputUrl)
        {
            Uri baseUri;
            Uri resultUri;


            if (string.IsNullOrEmpty(baseUrl))
                if (string.IsNullOrEmpty(inputUrl))
                    return string.Empty;
                else
                    return inputUrl;

            // Try to parse the input URL
            if (!Uri.TryCreate(inputUrl, UriKind.Absolute, out resultUri))
            {
                // The entered URL is not an absolute URL, try resolving it as a relative URL
                baseUri = new Uri(baseUrl);
                if (Uri.TryCreate(baseUri, inputUrl, out resultUri))
                {
                    return resultUri.AbsoluteUri;
                }
                else
                {
                    // Returns an error or custom value if it cannot resolve to a valid relative URL
                    return null;
                }
            }
            else
            {
                // The entered URL is already an absolute URL and will be returned directly.
                return resultUri.AbsoluteUri;
            }
        }//GetAbsoluteUrl(string baseUrl, string inputUrl)

        public bool IsPageUrlExist(string _rul)
        {
            return this.Tasks_Active.IsUrlExist(_rul) 
                || this.Tasks_History.IsUrlExist(_rul) 
                || this.Tasks_Pending.IsUrlExist(_rul);
        }//

        /// <summary>
        /// check all page task Histroy to find if any page html MD5 is the same.
        /// </summary>
        /// <param name="_htmlMD5"></param>
        /// <returns></returns>
        public bool HasSameHtmlInHistory(string _htmlMD5)
        {
            return this.Tasks_History.HasSameHtmlMD5(_htmlMD5);
        }

        /// <summary>
        /// transfer this.SiteRootUrl into code name. it will replace all Punctuation into '_', 
        /// and delete  "http" and delete all things after "#?="
        /// so that developer can use this name to be folder name or attribute name or table name, etc. 
        /// </summary>
        /// <returns></returns>
        public string GetSiteCodeName()
        {
            if (string.IsNullOrEmpty(this.SiteRootUrl))
                return "EmptySiteRootUrl";

            string tpStr;
            //if (this.SiteRootUrl.IndexOfAny("#?=".ToCharArray()) >= 0)
            //    throw new Exception($"SiteRootUrl2CodeName() doesn't support \"#?=\". ErrURL={this.SiteRootUrl}");

            //delete all things after "#?="
            int errIndex = this.SiteRootUrl.IndexOfAny("#?=".ToCharArray());
            if (errIndex >= 0)
                tpStr = this.SiteRootUrl.Remove(errIndex);
            else
                tpStr = this.SiteRootUrl;


            return tpStr.ToLower().Replace("http", "").Replace("://", "_").Replace('/','_');
        }//SiteRootUrl2CodeName()



        /// <summary>
        /// after a page is downloaged, this funtion will do following things.
        /// 1. write html text into file.
        /// 2. append page task into Old task file.
        /// 3. move this task from TaskList into Old task list
        /// 4. general a new PageInfo to parse html page.
        /// 5. append PageInfo into PageInfoList file.
        /// </summary>
        /// <param name="_pt"></param>
        public void FinishActivePageTask(CPageTask _pt)
        {

            // 2. append page task into Old task file.
            //File.AppendAllLines(
            //    GetFullFileName_PageTasks_History(), 
            //    new string[] { GJson.ToJson(_pt)  },
            //    Encoding.ASCII);

            // 3. move this task from TaskList into Old task list
            this.Tasks_History.Add(_pt);
            this.Tasks_Active.Remove(_pt);

            //this.LogFile.AppendMessage("Finish page: " + _pt.Url + $"; DocType={_pt.DocType} ; {_pt.HtmlKB}KB");
        }//FinishPageTask(CPageTask _pt)

        /// <summary>
        /// try to find page task in active tasks, then move it to History.
        /// if page task is not found in active tasks, do nothing.
        /// </summary>
        /// <param name="url"></param>
        public void FinishActivePageTask(string url, EExecuteStatus exeStatus, bool bLogDetails)
        {
            CPageTask pt = this.Tasks_Active.GetTaskByUrl(url);

            if (pt == null)
            {
                if (bLogDetails)
                    this.LogFile.AppendWarning("Didn't find page task by url. " +
                        $"FinishActivePageTask( rul= {url} , exeStatus= {exeStatus} )");
                return;
            }

            this.Tasks_History.Add(pt);
            this.Tasks_Active.Remove(pt);

            this.LogFile.AppendMessage($"FinishActivePageTask: rul= {url} , exeStatus= {exeStatus}");

        }


        public void AppendPageInfo2File(CPageInfo pi)
        {

            File.AppendAllLines(
                GetFullFileName( FileName_PageInfos),
                new string[] { GJson.ToJson(pi) },
                pi.GetEncoding());

        }//AppendPageInfo2File

        #region Obj Save and load
        /// <summary>
        /// 1. save this site task to json file in the folder of this task.
        /// 2. save this.Tasks_Active and Tasks_Pending to file.
        ///  this function will use default task file name and default task folder.
        /// </summary>
        public void Save2File()
        {
            //refresh count attributes.
            this.Cnt_ActivePageTasks = this.Tasks_Active.Count();
            this.Cnt_HistoryPageTasks = this.Tasks_History.Count();
            this.Cnt_PendingPageTasks = this.Tasks_Pending.Count();
            this.Cnt_ErrorUrls = this.ErrorUrls.Count;

            string filePath = Path.Combine(this.GetCrrRun_SiteStorageRoot(), 
                string.Format(FileName_SiteTask, CLogFile.GetTimeStr_FileName(this.CurrentRunTime)) );
            File.WriteAllText(filePath, GJson.ToJson(this), Encoding.UTF8);

            // over write task lists into files.
            this.Tasks_Active.Save2File();
            this.Tasks_Pending.Save2File();
            this.SaveErrorUrl();

            this.LogFile.AppendMessage("Save Site Task to file." + 
                "AutoSaveSiteTasksTimer_Tick()" +
                    $"Active {this.GetCount_Task_Active()} / " +
                    $"History {this.GetCount_Task_History()} / " +
                    $"Error {this.GetCount_ErrorUrls()} / " +
                    $"Pending {this.GetCount_Task_Pending()}/ " +
                    $"Blacklist {this.GetCount_Blacklist()}");
            //// Note! this.Tasks_History don't need to rewrite from memory, 
            ////  because it is already when anything add into History list.

        }//Save2File() 

        private void MergeErrorUrls(Dictionary<string, int> newErrorList)
        {
            foreach(string url in newErrorList.Keys)
            {
                this.AddErrorUrl(url, newErrorList[url]);
            }
        }

        /// <summary>
        /// Merge all new things from _st into this.
        /// </summary>
        /// <param name="_st"></param>
        public void Merge(CSiteTask _st)
        {
            this.Tasks_Active.Merge(_st.Tasks_Active, this.GetNewPageTaskId);
            this.Tasks_History.Merge(_st.Tasks_History, this.GetNewPageTaskId);
            this.Tasks_Pending.Merge(_st.Tasks_Pending, this.GetNewPageTaskId);
            this.MergeErrorUrls(_st.ErrorUrls);

            _st.Cfg_PriorityList.ForEach(a => { if (!this.Cfg_PriorityList.Contains(a)) this.Cfg_PriorityList.Add(a); });
            _st.Cfg_BlackList.ForEach(a => { if (!this.Cfg_BlackList.Contains(a)) this.Cfg_BlackList.Add(a); });

            foreach (string crrRule in _st.Cfg_UrlAutoReplaceRules.Keys)
            {
                 if (!this.Cfg_UrlAutoReplaceRules.Keys.Contains(crrRule)) 
                    this.Cfg_UrlAutoReplaceRules.Add(crrRule, _st.Cfg_UrlAutoReplaceRules[crrRule]);
            }
            

        }//Merge(CSiteTask _st)

        ///// <summary>
        ///// load a site task from json file
        ///// </summary>
        //public static CSiteTask LoadFromFile_Json(string _fileFullPath)
        //{
        //}////Save2File() 

        /// <summary>
        /// load a site task from task folder. this function will use default task file name to get json file.
        /// and also load all task lists from files.
        /// </summary>
        public static CSiteTask LoadFromFile_Json_TaskFolder( 
            string _taskFolder,
            bool bIncludePageTasks, 
            bool reCheckSiteRule)
        {
            //load site task obj form last version file.
            string lastFile = GHelper.GetLastVersionFile(_taskFolder, CSiteTask.FileName_SiteTask.Replace("{0}", "*"));

            if (lastFile == null)
                return null;

            string jsonStr = File.ReadAllText(lastFile, Encoding.UTF8);
            CSiteTask rstSiteTask = GJson.FromJson<CSiteTask>(jsonStr);
            string Project_StorageRoot = _taskFolder.Substring(0, _taskFolder.LastIndexOfAny("\\/".ToCharArray()));
            rstSiteTask.SetCrrRunProject_StorageRoot(Project_StorageRoot);

            //reset RootUrl it self, so that all releated file path in runtime will be set.
            rstSiteTask.SetSiteRootUrl(rstSiteTask.SiteRootUrl);

            //rstSiteTask.CurrentRunTime = DateTime.Now;

            LoadFolder_HistoryTasks(_taskFolder, rstSiteTask);

            LoadFolder_PendingTasks(_taskFolder, rstSiteTask);

            LoadFolder_ActiveTasks(_taskFolder, rstSiteTask,  reCheckSiteRule);

            rstSiteTask.CurrentRunTime = DateTime.Now;

            rstSiteTask.ReActive_PeriodicallyPages();

            rstSiteTask.ResetPageTaskListFiles();

            rstSiteTask.LoadErrorUrl(_taskFolder, true);

            return rstSiteTask;

        }////Save2File() 

        /// <summary>
        /// load Active task lists from files.
        /// 1. find last version of task file.
        /// 2. remove tasks if they are already in history tasks.
        /// </summary>
        /// <param name="_taskFolder"></param>
        /// <param name="rstSiteTask"></param>
        /// <exception cref="Exception"></exception>
        private static void LoadFolder_PendingTasks(string _taskFolder, CSiteTask rstSiteTask)
        {
            //load Pending task lists from files.
            string Last_PendingFile = GHelper.GetLastVersionFile(_taskFolder,
                CSiteTask.FileName_PageTasks_Pending.Replace("{0}", "*"));

            if (string.IsNullOrEmpty(Last_PendingFile))
                throw new Exception("Can't find Tasks_Pending file: " + _taskFolder);

            rstSiteTask.Tasks_Pending.LoadFromFile(Last_PendingFile, false, rstSiteTask.Tasks_Pending, true);

            ////// 2. remove tasks if they are already in history tasks.
            //rstSiteTask.Tasks_Pending.RemoveRange(a => rstSiteTask.Tasks_History.Contains(a.Url));
            IEnumerable<string> delTasks = from a in rstSiteTask.Tasks_Pending.GetReadonlyList()
                                           where rstSiteTask.Tasks_History.Contains(a.Url)
                                           select a.Url;
            rstSiteTask.Tasks_Pending.RemoveRange(delTasks);

        }

        /// <summary>
        /// load Active task lists from files.
        /// 1. find last version of task file.
        /// 2. remove tasks if they are already in history tasks.
        /// </summary>
        /// <param name="_taskFolder"></param>
        /// <param name="rstSiteTask"></param>
        /// <exception cref="Exception"></exception>
        private static void LoadFolder_ActiveTasks(string _taskFolder, CSiteTask rstSiteTask, bool reCheckSiteRule)
        {
            //1. find last version of task file.
            string last_ActiveFile = GHelper.GetLastVersionFile(_taskFolder,
                CSiteTask.FileName_PageTasks_Active.Replace("{0}", "*"));

            if (string.IsNullOrEmpty(last_ActiveFile))
                throw new Exception("Can't find Tasks_Active file: " + _taskFolder);

            //load Active task lists from files.
            if (!reCheckSiteRule)
            {
                rstSiteTask.Tasks_Active.LoadFromFile(last_ActiveFile, false, rstSiteTask.Tasks_Active, true);
                //2. remove tasks if they are already in history tasks.

                IEnumerable<string> delTasks = from a in rstSiteTask.Tasks_Active.GetReadonlyList()
                                               where rstSiteTask.Tasks_History.Contains(a.Url)
                                               select a.Url;
                //foreach (var crrTask in rstSiteTask.Tasks_Active)
                //{
                //    //rstSiteTask.Tasks_Active.RemoveRange(a => rstSiteTask.Tasks_History.Contains(a.Url));
                //    if (rstSiteTask.Tasks_History.Contains(crrTask.Url))
                //}
                rstSiteTask.Tasks_Active.RemoveRange(delTasks);
            }
            else
            {
                CPageTaskList ptl = new CPageTaskList();
                ptl.LoadFromFile(last_ActiveFile, true, rstSiteTask.Tasks_Active, true);

                foreach (CPageTask crrPt in ptl.GetReadonlyList())
                {
                    CPageTask pt  = rstSiteTask.AddNewPageTasks(null, crrPt.Url);

                    if( pt != null)
                        pt.TaskTime = crrPt.TaskTime;
                }

                //rstSiteTask.Tasks_Active.AddRange(ptl.GetReadonlyList());
                ptl.Clear();
            }
            //2. remove tasks if they are already in history tasks.
            //List<CPageTask> rmList = new List<CPageTask>();
            //foreach(CPageTask crrActive in rstSiteTask.Tasks_Active.GetReadonlyList())
            //{
            //    if ( rstSiteTask.Tasks_History.Contains(crrActive.Url))
            //        rmList.Add(crrActive);
            //}


        }//LoadFolder_ActiveTasks()

        private static void LoadFolder_HistoryTasks(string _taskFolder, CSiteTask rstSiteTask)
        {
            ///load Active task lists from files. 
            ///Note! All Tasks_History 's file should be loaded, not only the last version.
            string[] historyFiles = Directory.GetFiles(_taskFolder,
                CSiteTask.FileName_PageTasks_History.Replace("{0}", "*"));
            if (historyFiles == null || historyFiles.Length == 0)
                throw new Exception("Can't find PageTasks_History file: " + _taskFolder);

            foreach (string crrFile in historyFiles)
                rstSiteTask.Tasks_History.LoadFromFile(crrFile, false, null, false);
        }

        private void ReActive_PeriodicallyPages()
        {
            foreach (string crrUrl in this.Cfg_PeriodicallyPages)
                this.AddNewPageTasks2ActiveTaskList_force(crrUrl);
        }

        /// <summary>
        /// Save this.ErrorUrls to file.csv
        /// </summary>
        private void SaveErrorUrl()
        {
            //append csv file
            string filePath = this.GetFullFileName(  FileName_ErrorUrl);
            List<string> lines = new List<string>();

            foreach (var crrPair in this.ErrorUrls)
                lines.Add($"{crrPair.Value}\t{crrPair.Key}");

            File.WriteAllLines(filePath, lines, Encoding.ASCII);

        }//SaveErrorUrl()

        /// <summary>
        /// load Error Url list form csv file
        /// </summary>
        /// <param name="csvFilePath"></param>
        /// <param name="bClearBeforeLoading"></param>
        /// <exception cref="Exception"></exception>
        private void LoadErrorUrl(string _taskFolder, bool bClearBeforeLoading)
        {
            if (bClearBeforeLoading)
                this.ErrorUrls.Clear();

            //string filePath = this.GetFullFileName(_taskFolder, FileName_ErrorUrl);
            string filePath = GHelper.GetLastVersionFile(_taskFolder, FileName_ErrorUrl.Replace("{0}", "*"));

            //try
            //{
            string[] lines = File.ReadAllLines(filePath);
            //using (StreamReader sr = new StreamReader(filePath))
            //{
            //    string line;

            //    // Read the file content line by line until the end of the file
            int i = 1;
            //    while ((line = sr.ReadLine()) != null)
            foreach (string line in lines)
            {
                string[] cloumns = line.Split('\t');
                if (cloumns.Length != 2)
                {
                    string errStr = $"LoadErrorUrl( file={filePath}), line[{i}] = {line}";
                    this.LogFile.AppendError(errStr);
                    throw new Exception(errStr);
                }

                string url = cloumns[1];
                int errTimes = int.Parse(cloumns[0]);

                    if (!this.ErrorUrls.ContainsKey(url))
                        this.ErrorUrls.Add(url, errTimes);
                    else
                        this.ErrorUrls[url] = Math.Max(this.ErrorUrls[url], errTimes);
                i++;
            }//while ((line = sr.ReadLine()) != null)
             //}//using (StreamReader sr = new StreamReader(csvFilePath))
             //}
             //catch (Exception ex)
             //{
             //    string errStr = $"LoadErrorUrl( file={filePath}). {ex.Message}";
             //    this.LogFile?.AppendError(errStr);
             //    throw new Exception(errStr,ex);
             //}
        }//LoadErrorUrl(string csvFilePath, bool bClearBeforeLoading)

        #endregion    //Obj Save and load



    }//class SiteTask
}//namespace
