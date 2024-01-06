using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Alivever.Com.WinBrowserCrawler
{
    public class CSiteTaskMgr
    {
        /// <summary>
        /// singelten instance
        /// </summary>
        public static CSiteTaskMgr Ins = null;// new CSiteTaskMgr();

        /// <summary>
        /// RootPath of all downloads. SiteTasks will create their own subfolder in SiteTasks.
        /// </summary>
        public string StorageRootPath { get { return this.m_StorageRootPath; } }
        private string m_StorageRootPath;


        protected CLogFile LogFile;

        /// <summary>
        /// the time that this task is created. this time will never be changed.
        /// </summary>
        public DateTime CreateTime = DateTime.Now;

        /// <summary>
        /// a task may be executed for many times. this time will be updated when task is reloaded.
        /// </summary>
        public DateTime CurrentRunTime = DateTime.Now;

        public List<string> SiteUrls = new List<string>();

        #region private attributes for runtime
        /// <summary>
        /// all tasks that are running.
        /// </summary>
        private readonly Dictionary<string, CSiteTask> SiteTasks = new Dictionary<string, CSiteTask>();

        #endregion //private attributes for runtime

        #region static attributes
        public static readonly string FileName_SiteTaskMgr = "Project_{0}.json";

        public static readonly int Cfg_MaxSiteTasks = 20;

        //public static readonly string GLogFileName = $"SiteTaskMgr_{CLogFile.GetTimeStr_FileName()}.log";

        #endregion //static attributes

        #region constructor
        public string GetFileFullPath_SiteTaskMgr() 
        {
            return Path.Combine(this.StorageRootPath,
                string.Format(FileName_SiteTaskMgr, CLogFile.GetTimeStr_FileName(CurrentRunTime) ) );
        }

        public CLogFile GetLogFile()
        {
            return this.LogFile;
        }

        /// <summary>
        /// save this obj as a big json into file of this.GetFileFullPath_SiteTaskMgr()
        /// </summary>
        public void Save2File()
        {
            string file = GetFileFullPath_SiteTaskMgr();
            string json = GJson.ToJson(this);
            File.WriteAllText(file, json);

            this.LogFile.AppendMessage("Save Task Manager to file.");

        }

        /// <summary>
        /// 1. auto find the last version of project file in that Dir.
        /// 2. Load the lastest project file and create a new object and return.
        /// </summary>
        /// <param name="projectDir"></param>
        /// <exception cref="Exception"></exception>
        public static CSiteTaskMgr LoadFromFolder(string projectDir)
        {
            if (!Directory.Exists(projectDir))
                throw new Exception("directory not found : "+ projectDir);

            // get last version of project file in the directory
            string fileTpl = FileName_SiteTaskMgr.Replace("{0}", "*");
            //string[] projectFiles = Directory.GetFiles(projectDir, fileTpl);
            //string projFile = projectFiles.OrderBy(x => x).LastOrDefault();

            string projFile = GHelper.GetLastVersionFile(projectDir, fileTpl);

            if (projFile == null )
                throw new Exception($"Project file not found.Please check '{fileTpl}' in {projectDir}");

            CSiteTaskMgr rstMgr = GJson.LoadObjFromFile<CSiteTaskMgr>(projFile);
            rstMgr.CurrentRunTime = DateTime.Now;
            rstMgr.SetStorageRootPath(projectDir);

            //// load site tasks from sub folders.
            string[] subFolder = Directory.GetDirectories(projectDir);

            foreach (string crrDir in subFolder)
            {
                CSiteTask st = CSiteTask.LoadFromFile_Json_TaskFolder( crrDir, true);
                //st.SetCrrRunStorageRoot(crrDir);

                if (st == null)
                {
                    rstMgr.LogFile.AppendError("'crrDir' is not a SiteTask Folder! Project loading Tried to load SiteTask from this Folder Failed.");
                    continue;
                }

                //if (!st.SetSiteRootUrl(crrDir))
                //{
                //    throw new Exception ($"CSiteTaskMgr can't be created." +
                //        $"LoadFromFolder(baseUrl='{projectDir}') => SetSiteRootUrl(\"{crrDir} failed.\"). ");
                //    //return null;
                //}

                if (st == null)
                {
                    rstMgr.LogFile.AppendWarning($"Try to load SiteTask but failed. dir={crrDir}");
                    continue;
                }

                rstMgr.SiteTasks.Add(st.SiteRootUrl, st);

                if ( !rstMgr.SiteUrls.Contains(st.SiteRootUrl))
                    rstMgr.SiteUrls.Add(st.SiteRootUrl);
            }

            rstMgr.Save2File();

            return rstMgr;

        }//LoadFromFile()

        /// <summary>
        /// 1. auto find the last version of project file in that Dir.
        /// 2. merge all 'New things' from file into Current Object. Auto deduplicate.
        /// </summary>
        /// <param name="projectDir"></param>
        public  void Merge(CSiteTaskMgr mgr)
        {
            //CSiteTaskMgr mgr = LoadFromFolder(projectDir);

            //// merge basic attributes
            this.CreateTime = this.CreateTime > mgr.CreateTime ? mgr.CreateTime : this.CreateTime;
            this.CurrentRunTime = this.CurrentRunTime > mgr.CurrentRunTime ?this.CurrentRunTime : mgr.CurrentRunTime ;

            IEnumerable<string> newSiteUrls = from a in mgr.SiteUrls
                                              where !this.SiteUrls.Contains(a)
                                              select a;

            this.SiteUrls.AddRange(newSiteUrls);

            //// Add new site tasks from mgr.SiteTasks
            if (mgr.SiteTasks != null && mgr.SiteTasks.Count > 0)
            {
                foreach( CSiteTask crrSite in mgr.SiteTasks.Values)
                {
                    // if new siteTask is not in this.SiteTasks, then add it in to this.SiteTasks
                    if (this.SiteTasks.Keys.FirstOrDefault(a => mgr.SiteTasks.ContainsKey(a)) == null)
                        this.SiteTasks.Add(crrSite.SiteRootUrl, crrSite);
                    else
                        this.SiteTasks[crrSite.SiteRootUrl].Merge(crrSite);
                }
            }

            //// merge new page tasks from mgr.SiteTasks, if both mgr.SiteTasks and this.SiteTasks have a same SiteTask.

            //// load site tasks from sub folders to see if there are any Site has folder but not in mgr.SiteTasks;

        }//MergeFromFolder(string projectDir)


        public void SetStorageRootPath(string _StorageRootPath)
        {
            this.m_StorageRootPath = _StorageRootPath ;

            this.LogFile = new CLogFile();
            LogFile.GenerateFileUrl(_StorageRootPath, 
                string.Format("SiteTaskMgr_{0}.log", CLogFile.GetTimeStr_FileName( this.CurrentRunTime ) ) );
            LogFile.AppendMessage("Begin SiteTaskMgr");

        }

        public CSiteTaskMgr()
        {
            //GLogFileName = $"SiteTaskMgr_{CLogFile.GetTimeStr_FileName(this.CurrentRunTime)}.log";
        }
        #endregion //constructor

        public IEnumerable<CSiteTask> GetSiteTasks()
        {
            return this.SiteTasks.Values;
        }

        public CSiteTask GetSiteTask(string url)
        {
            return this.SiteTasks[url];
        }

        public string GetLogFileUrl()
        {
            return Path.Combine(this.StorageRootPath, CLogFile.GetTimeStr_FileName(this.CurrentRunTime));
        }

        /// <summary>
        /// auto parse each urls to get their domain, and all url into a siteTask of that domain.
        /// if a url belong to multi site root url, than it will be added into the longest site root url of task.
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public int AddNewPageTasks(string baseUrl, List<string> urls)
        {
            int i = 0;
            foreach (string crrUrl in urls)
            {
                //string siteUrl;
                var siteTask= FindSiteTask(crrUrl);

                if (siteTask == null)
                {
                    string siteUrl = GetSiteURLFromPageUrl(crrUrl);
                    siteTask = this.AddNewSiteTask(siteUrl, null);
                }
                
                siteTask.AddNewPageTasks(baseUrl, crrUrl);

            }//foreach (string crrUrl in urls)
            
            //Save2File();

            return i;
        }//AddNewPageTasks(List<string> urls)

        private CSiteTask FindSiteTask(string crrUrl)
        {
            IEnumerable<CSiteTask> siteUrlList = from crrSiteRoot in this.SiteTasks.Keys
                                              where crrUrl.StartsWith(crrSiteRoot)
                                              select this.SiteTasks[crrSiteRoot];
            //if there is no site obj before
            //string siteUrl;
            CSiteTask rstTask;

            if (siteUrlList.Count() == 0)
            {
                return null;
            }
            else // if a url belong to multi site root url, than it will be added into the longest site root url of task.
            {
                rstTask = siteUrlList.OrderByDescending(a => a.SiteRootUrl.Length).First();
            }

            return rstTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageUrl"></param>
        /// <returns> null means url is not a leagel URL that can be downloaded</returns>
        /// <exception cref="Exception"></exception>
        public  string GetSiteURLFromPageUrl(string pageUrl)
        {
            Uri uri;
            if (Uri.TryCreate(pageUrl, UriKind.Absolute, out uri))
            {
                
                string rst = $"{uri.Scheme}://{uri.Host}";
                return rst;
            }
            else 
            {
                this.LogFile.AppendError("Invailid Url={pageUrl}");
                return null;
            }
        }

        /// <summary>
        /// auto create a new site task, and added it into this.SiteTask.
        /// </summary>
        /// <param name="_siteRootUrl"></param>
        public CSiteTask AddNewSiteTask(string _siteRootUrl, IEnumerable<string> _highPriorityList)
        {
            // if this site is already exits.
            CSiteTask st= FindSiteTask(_siteRootUrl);

            if (st != null)
            {
                this.LogFile.AppendWarning("Tring to creat a existed site: " + _siteRootUrl);
                return st;
            }
            //if (SiteTasks.ContainsKey(_siteRootUrl))
            //    return this.SiteTasks[_siteRootUrl];

            this.LogFile.AppendMessage("Add new site task: " + _siteRootUrl);

            st = new CSiteTask()
            {
                //SiteUrlPrefix = _siteRootUrl,
            };
            st.SetCrrRunProject_StorageRoot(this.StorageRootPath);

            if (! st.SetSiteRootUrl(_siteRootUrl))
            {
                this.LogFile.AppendError( $"I have to ignor and this SiteTask will not be created. AddNewSiteTask(baseUrl='{_siteRootUrl}') => SetSiteRootUrl(\"{ _siteRootUrl} failed.\"). ");
                return null;
            }

            //st.AddNewPageTasks(baseUrl, new List<string>() { _siteRootUrl });

            this.SiteTasks.Add(st.SiteRootUrl,st);
            this.SiteUrls.Add(st.SiteRootUrl);

            if (_highPriorityList != null)
                st.Cfg_PriorityList.AddRange(_highPriorityList);

            //this.Save2File();
            return st;

        }//AddNewSiteTask(string _siteRootUrl)

    }//class SiteTaskMgr
}//namespace
