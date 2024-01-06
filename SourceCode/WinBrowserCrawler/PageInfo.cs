using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Alivever.Com.WinBrowserCrawler.CUrlListBrowserDlg;
using static System.Net.Mime.MediaTypeNames;

namespace Alivever.Com.WinBrowserCrawler
{
    /// <summary>
    /// parsing basic infomation from Html page. 
    /// such as : page date, title , author, language, links, images etc.
    /// </summary>
    public class CPageInfo
    {
        #region public attributs

        /// <summary>
        /// The auto-incremented ID of PageTask , in order to associate the PageTask with pageInfo.
        /// </summary>
        public int TaskId;

        public string Url;

        //public string Url { get { return this.m_PageUrl; } set { this.m_PageUrl = value; } }

        /// <summary>
        ///  relative file paths in the site folder.
        ///  it will have value Only after the file is save into file system, 
        /// </summary>
        public string SavedFile;

        /// <summary>
        /// file extension got from url
        /// </summary>
        public string FileExt;

        public EExecuteStatus Exe = EExecuteStatus.Created;

        public string Title;

        public EPageType PageType = EPageType.Unknown;

        public string Summary;

        public string Text;

        public string Author;

        /// <summary>
        /// the title image url. not all images in this page.
        /// </summary>
        public string ImgUrl;

        /// <summary>
        /// the title video url. not all video in this page.
        /// </summary>
        public string VideoUrl;

        public string Charset;

        public string DocType;

        public string Langrage;

        public string SrcMeida;

        public string Keywords;

        public string UrlMD5;

        public string HtmlMD5;

        /// <summary>
        /// how many ms did this page loaded.
        /// </summary>
        public int Load_ms = 0;

        /// <summary>
        /// file size
        /// </summary>
        public int HtmlKB = 0;

        public int InUrls = 0;

        public int OutUrls = 0;

        public int OutSites = 0;

        public int InImages = 0;

        public int OutImages = 0;

        /// <summary>
        /// download time.
        /// </summary>
        public DateTime DownTime = DateTime.Now;

        /// <summary>
        /// the date that this page is published. this time is passed from html text.
        /// </summary>
        public string PageTime = "00000000-000000";

        #endregion//public attributs

        #region Constructors and attributs settings
        /// <summary>
        /// Copy usable infomation from it's CPageTask.
        /// </summary>
        /// <param name="_pt"></param>
        public void FromPageTask(CPageTask _pt)
        {
            this.TaskId = _pt.TaskId;
            this.Url = _pt.Url;
            this.DownTime = _pt.DownTime;
            this.UrlMD5 = _pt.UrlMD5;
            this.Load_ms = _pt.Load_ms;
            this.Exe = _pt.Exe;
            //this.HtmlKB = _pt.HtmlBytes;
        }//FromPageTask( CPageTask _pt)

        ///// <summary>
        ///// parse all page task attributes from html and retun links
        ///// </summary>
        ///// <param name="_doc"></param>
        ///// <param name="_siteTask"></param>
        ///// <returns>(innerLinkes, outLinkes, innerImageLinkes, outImageLinkes)</returns>
        //public (List<string>, List<string>, List<string>, List<string>) ParsingHtmlDocument(HtmlDocument _doc, CSiteTask _siteTask)
        //{
        //    this.Exe = EExecuteStatus.Finshed;

            //this.Title = _doc.Title;

            //this.Langrage = ParsingHtml_GetLanguage(_doc);

            //this.Charset = ParsingHtml_GetCharset(_doc);

            //this.DocType = ParseContentType(_siteTask.SiteRootUrl);

            //string htmlText = _doc.Body.Parent.OuterHtml;

            //this.HtmlMD5 = GHelper.CalculateMD5(htmlText, Encoding.GetEncoding(this.Charset));

            //this.HtmlKB = htmlText.Length / 1024 < 1 ? 0 : htmlText.Length / 1024;

            //// Get the file name from the URL
            //string fileName = Path.GetFileName(_doc.Url.AbsolutePath);

            //// Get the file extension from the file name
            //if (!string.IsNullOrEmpty(fileName))
            //    this.FileExt = Path.GetExtension(fileName);

            ////this.FileExt 
            //// Extract all the links (URLs)
            //List<string> links = ExtractLinks(_doc);

            //// Extract all the image URLs
            //List<string> imageUrls = ExtractImageUrls(_doc);

            //List<string> innerLinkes = _siteTask.GetInnerURLs(links);
            //List<string> outLinkes = _siteTask.GetOutURLs(links);

            //List<string> innerImageLinkes = _siteTask.GetInnerURLs(imageUrls);
            //List<string> outImageLinkes = _siteTask.GetOutURLs(imageUrls);

            //this.InUrls = innerLinkes.Count;
            //this.OutUrls = outLinkes.Count;
            //this.InImages = innerImageLinkes.Count;
            //this.OutImages = outImageLinkes.Count;

            //var OutSitesList = from a in outLinkes where _siteTask.IsUrlInDomain(a) select a;
            //this.OutSites = OutSitesList.Count();
            ////string htmlText = _doc.Body.Parent.OuterHtml;
            ////this.SetPageHtmlStr(_doc.Body.Parent.OuterHtml);
            ////this.Bytes = htmlText.Length;

            //return (innerLinkes, outLinkes, innerImageLinkes, outImageLinkes);

        //}//ParsingHtmlDocument()

        #endregion//Constructors



        /// <summary>
        /// Save this.PageText into _fileUrl with Encoding of this.EncodeName
        /// </summary>
        /// <param name="_fileUrl"></param>
        public void SavePage2File_Html(string _siteRootDir, string _htmlStr, int _maxPathDeep)// HtmlDocument _doc)
        {
            Encoding ed = this.GetEncoding();
            string relativePath = this.GetRelativePath(_maxPathDeep);
            string dir = Path.Combine(_siteRootDir, relativePath);// "page_files");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string filename = this.GetRelativeFile();
            string fileUrl = Path.Combine(dir, filename);

            this.SavedFile = Path.Combine(relativePath, filename); //fileUrl.Replace("page_files\\", "");//Path.Combine( this.GetRelativePath(_maxPathDeep).Replace("page_files\\",""), this.GetRelativeFile());

            File.WriteAllText(fileUrl, _htmlStr, ed);// _doc.Body.Parent.OuterHtml, ed);

            //if (_releasePageTextAfterSaving)
            //    this.PageHtmlStr = null;
        }


        /// <summary>
        /// default file name = p[pageTime]_[UrlMD5]_d[downloadTime].html
        /// </summary>
        /// <returns></returns>
        public string GetRelativeFile()
        {
            // Calculate relative paths

            string fileName;

            if ( this.DownTime != DateTime.MinValue)
                fileName = $"d{CLogFile.GetTimeStr_FileName( this.DownTime)}";
            else
                fileName = $"d00000000-000000";

            fileName += $"_{this.UrlMD5}.html";


            return fileName;
        }

        /// <summary>
        /// default file name = p[pageTime]_[UrlMD5]_d[downloadTime].html
        /// </summary>
        /// <param name="_maxPathDeep">
        /// How many folder level will you follow the url folders?
        /// it because some website have so many folder deeps.
        /// I recommand you to set it as 1 or 2. 
        /// -1 means all pass as same as url.
        /// </param>
        /// <returns></returns>
        public string GetRelativePath(int _maxPathDeep)
        {
            string rPath, filename;


            string pagePath = "page_files";

            if (this.Exe == EExecuteStatus.Parsed)
                pagePath = "page_files_parsed";

            // Calculate relative paths
            GHelper.ExtractRelativePathFromUrl(this.Url, out rPath, out filename);

            if (rPath.Contains('/') && _maxPathDeep > 0)
            {
                // Remove the leading slash if present
                if (rPath.StartsWith("/"))
                {
                    rPath = rPath.Substring(1);
                }

                // Split the path into directory components
                string[] pathComponents = rPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                string[] folders = rPath.Split('/');

                string r2Path = Path.Combine(folders.Take(
                    _maxPathDeep)
                    .ToArray());
                return Path.Combine(pagePath, r2Path);
            }

            return Path.Combine(pagePath, rPath);
        }//GetRelativePath(int _maxPathDeep)

        /// <summary>
        /// return a Encoding object base on this.EncodeName. If this.EncodeName is unavailable than return UTF8 as default
        /// </summary>
        /// <returns></returns>
        public Encoding GetEncoding()
        {
            if (string.IsNullOrEmpty(this.Charset))
                return Encoding.UTF8;

            Encoding ed = Encoding.UTF8;

            try
            {
                return Encoding.GetEncoding(this.Charset);
            }
            catch 
            {
                return Encoding.UTF8;
            }
        }

    }// class


}//namespace
