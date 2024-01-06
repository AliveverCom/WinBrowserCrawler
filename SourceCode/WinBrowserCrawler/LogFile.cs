using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace Alivever.Com.WinBrowserCrawler
{
    public class CLogFile
    {
        private string FileUrl;

        public Encoding DftEncoding = Encoding.ASCII;

        public CLogFile()
        {
        }

        public void AppendError(string _str) { this.WriteLog("err", _str); }
        public void AppendMessage(string _str) { this.WriteLog("msg", _str); }
        public void AppendWarning(string _str) { this.WriteLog("warn", _str); }

        public void WriteLog(string _msgType, string _str) 
        { 
            string linestr = $"{GetTimeStr_DisplayStr(DateTime.Now)}\t{_msgType}\t{_str}";
            File.AppendAllLines( this.FileUrl,new string[]{ linestr } , this.DftEncoding); 
        }

        #region Static Methods
        public static string GetTimeStr_FileName(DateTime _dt)
        {
            return _dt.ToString("yyyyMMdd-HHmmss");
        }

        //public static string GetMinTime_CodeStr(DateTime _dt)
        //{
        //    return DateTime.MinValue.ToString("yyyyMMdd-HHmmss").Replace("_","");
        //}

        public static string GetTimeStr_DisplayStr(DateTime _dt)
        {
            return _dt.ToString("yyyy-MM-dd HH:mm:ss");
        }


        #endregion //Static Methods

        public string GetFileUrl() { return this.FileUrl; }
        /// <summary>
        /// log file url = CSiteTaskMgr.Ins.StorageRootPath + dirname + filename
        /// </summary>
        /// <param name="_fileNameTemple">if it inclues a {}, then {} will be replace as Datetime.Now with filename format.</param>
        /// <param name="_path"></param>
        public void GenerateFileUrl(string _path, string _fileNameTemple)
        {
            string filename;
            if (_fileNameTemple.IndexOf("{0}") >= 0)
                filename = string.Format(_fileNameTemple, GetTimeStr_FileName(DateTime.Now));
            else
                filename = _fileNameTemple;

            this.FileUrl = Path.Combine(_path, filename);

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            //if (!File.Exists(this.FileUrl))
            //    File.Create(this.FileUrl).Close();

        }

        /// <summary>
        ///   log file url = CSiteTaskMgr.Ins.StorageRootPath + filename.
        ///    
        /// </summary>
        /// <param name="fullPathAndFileNameTemplate">
        /// if it inclues a {}, then {} will be replace as Datetime.Now with filename format.
        /// Note! this paramater MUST be an 
        /// 
        /// </param>
        public void GenerateFileUrl(string fullPathAndFileNameTemplate)
        {
            if (!Path.IsPathRooted(fullPathAndFileNameTemplate))
                throw new Exception("Here need a absolute Path. but value is not. value="+ fullPathAndFileNameTemplate);

            string directoryPath = Path.GetDirectoryName(fullPathAndFileNameTemplate);
            this.GenerateFileUrl(directoryPath, fullPathAndFileNameTemplate);

        }

    }//class CLogHelper
}
