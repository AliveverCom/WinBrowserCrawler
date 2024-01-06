using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization; 

namespace Alivever.Com.WinBrowserCrawler
{
    public class CPageTask
    {
        #region public attributs
        /// <summary>
        /// The auto-incremented ID of PageTask , in order to associate the PageTask with pageInfo.
        /// </summary>
        public int TaskId;

        private string m_PageUrl;

        public string Url { get { return this.m_PageUrl; } set { this.m_PageUrl = value; } }

        public EExecuteStatus Exe = EExecuteStatus.Created;

        /// <summary>
        /// Content Type got from http header.
        /// </summary>
        public string DocType;

        /// <summary>
        /// in DB, we only compare UrlMD5, but not Url. because Url could be 1k long.
        /// </summary>
        public string UrlMD5;

        /// <summary>
        /// before save Html file, we check if same content is already exists.
        /// </summary>
        public string HtmlDM5;

        /// <summary>
        /// how many ms did this page loaded.
        /// </summary>
        public int Load_ms = 0;

        /// <summary>
        /// How long it spent for this page. including download, process, save to file, etc.
        /// </summary>
        public int Total_ms = 0;


        /// <summary>
        /// file size
        /// </summary>
        public int HtmlKB = 0;

        /// <summary>
        /// How many New html Links found in this task.
        /// </summary>
        public int NewHtmLnk = 0;

        /// <summary>
        /// when this task is created.
        /// </summary>
        public DateTime TaskTime = DateTime.Now; //CLogFile.GetTimeStr_FileName();

        /// <summary>
        /// download time.
        /// </summary>
        public DateTime DownTime = DateTime.MinValue; //string.Empty;


        ///// <summary>
        ///// where this web page are saved on the local disk.
        ///// This is a relative path under path of SiteTask, not an absolute path .
        ///// </summary>
        //public string SavedFile;

        #endregion//public attributs

        //#region private attributes

        //private string PageHtmlStr;

        ////private CSiteTask SiteTask;
        //#endregion//private attributes

        //public void SetSiteTask(CSiteTask _siteTask)
        //{
        //    this.SiteTask = _siteTask;
        //}

        public void SetPageUrl(string _url)
        {
            this.m_PageUrl = _url;
            this.UrlMD5 =GHelper. CalculateMD5(_url, Encoding.ASCII);
        }

        public void SetPageHtmlStr(string _HtmlDM5, int _HtmlKB)
        {
            this.HtmlDM5 =_HtmlDM5 ;
            this.HtmlKB = _HtmlKB;
        }

        public string ToLineString()
        {
            return this.Url;
        }

        public void SetFinished(EExecuteStatus execStatus)
        {
            this.Exe= execStatus;
            //this.DownTime= CLogFile.GetNow_FileStr();

        }

    }//class PageTask
}
