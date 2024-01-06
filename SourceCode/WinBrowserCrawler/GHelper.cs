using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alivever.Com.WinBrowserCrawler
{
    /// <summary>
    /// global functions
    /// </summary>
    public class GHelper
    {
        public static List<string> gTLDs = new List<string> { "com", "net", "org", "edu", "gov", "biz" };

        public static string GetMainHost(string url)
        {
            Uri uri = new Uri(url);

            // Get the main host name
            string hostName = uri.Host.ToLower();

            if (Uri.CheckHostName(hostName) == UriHostNameType.IPv4
                || Uri.CheckHostName(hostName) == UriHostNameType.IPv6)
                return hostName;

            string[] segList = hostName.Split('.');

            if (segList.Length <= 2)
                return hostName;

            int len = segList.Length;
            //string mainHost = string.Empty;

            //int idxTopDomain = 1;

            string mainHost;
            //if com.cn or net.cn
            if (gTLDs.Contains(segList[len - 2]))
                mainHost = $"{segList[len - 3]}.{segList[len - 2]}.{segList[len - 1]}";

            else
                mainHost = $"{segList[len - 2]}.{segList[len - 1]}";

            return mainHost;
        }//GetMainHost(string url)

        public static string CalculateMD5(string input, Encoding _ed)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = _ed.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // 将每个字节转换为十六进制字符串
                }

                return sb.ToString();
            }//using (MD5 md5 = MD5.Create())
        }//CalculateMD5(string input)

        /// <summary>
        /// RelativePath = this.url - host - paramaters
        /// </summary>
        /// <returns></returns>
        public static void ExtractRelativePathFromUrl(string url, out string dir, out string fileName)
        {
            Uri uri = new Uri(url);
            dir = uri.AbsolutePath;

            if (string.IsNullOrEmpty( uri.AbsolutePath))
            {
                dir = string.Empty;
                fileName = string.Empty;
                return;
            }

            // 获取目录路径
            if (uri.AbsolutePath.StartsWith("/"))
                dir = uri.AbsolutePath.Substring(1).Replace("%", "_").Replace("?", "_");

            if (dir.Contains('/'))
            {
                fileName = dir.Substring(dir.LastIndexOf('/') + 1);
                dir = dir.Substring( 0, dir.LastIndexOf('/')).Replace("%", "_").Replace("?", "_");
                return;

            }
            else
            {
                dir = string.Empty;
                fileName = uri.AbsolutePath;
                return;
            }

        }// ExtractRelativePathsFromUrl(string url)

        /// <summary>
        /// it will use fileTpl as a searching template to get files in _folder, 
        /// and return file name of the last one, base on order by file name desc. 
        /// </summary>
        /// <param name="_taskFolder"></param>
        /// <param name="fileTpl"></param>
        /// <returns></returns>
        public static string GetLastVersionFile(string _folder, string fileTpl)
        {
            // if there are {} in the template.
            if (fileTpl.IndexOf("{") >= 0)
                fileTpl = fileTpl.Replace("{}", "*");

            string[] files = Directory.GetFiles(_folder, fileTpl);

            if (files.Length == 0)
                return null;

            string lastFile = files.OrderByDescending(a => a).First();

            return lastFile;

        }

        public static bool IsUrlContainsDirectory(string absoluteUrl)
        {
            Uri uri;
            if (Uri.TryCreate(absoluteUrl, UriKind.Absolute, out uri))
            {
                // Check if it has an absolute path (directory)
                return !string.IsNullOrEmpty(uri.AbsolutePath) && uri.AbsolutePath != "/";
            }

            return false; // If the URL is invalid or cannot be parsed
        }

        /// <summary>
        /// use regulerExp to try to match only one from str. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regulerExp"></param>
        /// <returns>return matched content. Null means not natched.</returns>
        public static string RegulerExpMatch(string str, string regulerExp)
        {
            //string pattern = @"\\u0026spm=smpc\.content\.share\..{22}"; // Regular expression pattern

            // Perform the regular expression match
            Match match = Regex.Match(str, regulerExp);

            // Check if the match is successful
            if (match.Success)
            {
                // Get the matched string using the Value property
                string matchedString = match.Value;
                return match.Value;
            }

            return null;
        }//RegulerExpMatch(string str, string regulerExp)

        /// <summary>
        /// apply this.AutoReplaceUrl rules to replace url. return replaced new string.
        /// </summary>
        /// <param name="url"></param>
        public static string AutoReplaceUrlByCfg(string url, Dictionary<string,string> replaceRules)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            foreach (string regExp in replaceRules.Keys)
            {
                string matchStr = GHelper.RegulerExpMatch(url, regExp);

                if (matchStr != null)
                {
                    url = url.Replace(matchStr, replaceRules[regExp]);
                }
            }

            return url;
        }//AutoReplaceUrlByCfg(string url)

        public static string PrintUnknowException(Exception ex)
        {
            string logStr = "App Crashed! ex=" + ex.Message; 
            if (ex.StackTrace != null)
            {
                // Get the stack trace information
                var stackTrace = new System.Diagnostics.StackTrace(ex, true);

                // Get the top (most recent) frame from the stack trace
                foreach (var topFrame in stackTrace.GetFrames())
                {
                    //var topFrame = stackTrace.GetFrame(0);
                    if (topFrame != null)
                    {
                        var fileName = topFrame.GetFileName();
                        var lineNumber = topFrame.GetFileLineNumber();
                        var columnNumber = topFrame.GetFileColumnNumber();

                        if (!string.IsNullOrEmpty(fileName) && lineNumber > 0)
                        {
                            logStr += $"; File: {fileName}, Line: {lineNumber}, Column: {columnNumber}";
                        }
                        else
                            logStr += "; No StackTrace: File Line Column; ";
                    }//if (topFrame != null)
                }
            }//if (ex.StackTrace != null)

            CSiteTaskMgr.Ins?.GetLogFile().AppendError(logStr);
            return logStr;
        }// PrintUnknowException(Exception ex)
    }//class GHelper
}
