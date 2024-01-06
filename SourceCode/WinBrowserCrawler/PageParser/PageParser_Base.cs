using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Alivever.Com.WinBrowserCrawler.CUrlListBrowserDlg;

namespace Alivever.Com.WinBrowserCrawler.PageParser
{
    public class CPageParser_Base
    {
        public static readonly string MainDomain = "sina.com.cn";

        /// <summary>
        /// parse all page task attributes from html and retun links
        /// </summary>
        /// <param name="_doc"></param>
        /// <param name="_siteTask"></param>
        /// <returns>(innerLinkes, outLinkes, innerImageLinkes, outImageLinkes)</returns>
        public (List<string>, List<string>, List<string>, List<string>) ParsingHtmlDocument(HtmlDocument _doc, CSiteTask _siteTask, CPageInfo _pi)
        {
            //_pi.Exe = EExecuteStatus.Finshed;

            _pi.Title = this.GetTital(_doc); 

            _pi.Langrage = GetLanguage(_doc);

            _pi.Charset = GetCharset(_doc);

            _pi.DocType = GetContentType(_pi.Url);

            string htmlText = _doc.Body.Parent.OuterHtml;

            _pi.HtmlMD5 = GHelper.CalculateMD5(htmlText, Encoding.UTF8);

            _pi.HtmlKB = htmlText.Length / 1024 < 1 ? 0 : htmlText.Length / 1024;

            _pi.Summary = this.GetSummary(_doc);

            _pi.Text = this.GetText(_doc);

            _pi.Author = this.GetAuthor(_doc);

            _pi.SrcMeida =  this.GetSrcMeida(_doc);

            _pi.Keywords = this.GetKeywords(_doc);

            _pi.PageTime = this.GetPageTime(_doc);

            //FillAttributesByAllHhmlElements(_doc,_siteTask, _pi);

            ///// post process

            if (_pi.Title == _pi.Summary)
                _pi.Summary = null;

            //this.FileExt 
            // Extract all the links (URLs)
            List<string> links = GetLinks(_doc);

            // Extract all the image URLs
            List<string> imageUrls = GetImageUrls(_doc);

            List<string> innerLinkes = _siteTask.GetInnerURLs(links);
            List<string> outLinkes = _siteTask.GetOutURLs(links);

            List<string> innerImageLinkes = _siteTask.GetInnerURLs(imageUrls);
            List<string> outImageLinkes = _siteTask.GetOutURLs(imageUrls);

            _pi.InUrls = innerLinkes.Count;
            _pi.OutUrls = outLinkes.Count;
            _pi.InImages = innerImageLinkes.Count;
            _pi.OutImages = outImageLinkes.Count;

            var OutSitesList = from a in outLinkes where _siteTask.IsUrlInDomain(a) select a;
            _pi.OutSites = OutSitesList.Count();
            //string htmlText = _doc.Body.Parent.OuterHtml;
            //this.SetPageHtmlStr(_doc.Body.Parent.OuterHtml);
            //this.Bytes = htmlText.Length;


            return (innerLinkes, outLinkes, innerImageLinkes, outImageLinkes);

        }//ParsingHtmlDocument()

        /// <summary>
        /// if you have to process all Hhml Elements in the doc. please overwrite it.
        /// </summary>
        protected virtual void FillAttributesByAllHhmlElements(
            HtmlDocument _doc, CSiteTask _siteTask, CPageInfo _pi)
        {

        }//FillAttributesByAllHhmlElements()

        public static string DeleteAllHtmlElements(string htmlText)
        {
            string pattern = @"<[^<>]+>";

            MatchCollection matches = Regex.Matches(htmlText, pattern);

            string tpStr = htmlText;
            foreach (Match match in matches)
            {
                tpStr = tpStr.Replace( match.Value, string.Empty);
            }

            return tpStr;
        }//DeleteAllHtmlElements(string preTxt)

        public  List<string> GetLinks(HtmlDocument document)
        {
            List<string> links = new List<string>();

            foreach (HtmlElement link in document.Links)
            {
                string href = link.GetAttribute("href");
                if (!string.IsNullOrEmpty(href))
                {
                    links.Add(href);
                }
            }

            return links;
        }

        public  List<string> GetImageUrls(HtmlDocument document)
        {
            List<string> imageUrls = new List<string>();

            foreach (HtmlElement img in document.Images)
            {
                string src = img.GetAttribute("src");
                if (!string.IsNullOrEmpty(src))
                {
                    imageUrls.Add(src);
                }
            }

            return imageUrls;
        }//ExtractImageUrls(HtmlDocument document)

        public  string GetContentType( string url)
        {
            //if (!string.IsNullOrEmpty(doc.ContentType))
            //    return doc.ContentType;

            // Create a WebRequest to retrieve the headers
            WebRequest request = WebRequest.Create(url);
            request.Method = "HEAD"; // Use HEAD method to retrieve only headers

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    // Extract content type from the headers
                    return response.ContentType;
                }//using (WebResponse response = request.GetResponse())
            }
            catch
            {
                return "text/html";
            }
            //return null;
        }//ParseContentType()


        // Get encoding type from meta tag
        public  string GetCharset(HtmlDocument document)
        {
            foreach (HtmlElement metaTag in document.GetElementsByTagName("meta"))
            {
                string charset = metaTag.GetAttribute("charset");
                if (!string.IsNullOrEmpty(charset))
                {
                    return charset;
                }

                string httpEquiv = metaTag.GetAttribute("http-equiv");
                if (httpEquiv.ToLower() == "content-type")
                {
                    string content = metaTag.GetAttribute("content");
                    int charsetIndex = content.IndexOf("charset=");
                    if (charsetIndex >= 0)
                    {
                        return content.Substring(charsetIndex + 8).Trim();
                    }
                }
            }

            // If encoding type is not found, UTF-8 is returned by default
            return "UTF-8";
        }

        // Get language type from meta tag
        public virtual string GetLanguage(HtmlDocument document)
        {
            foreach (HtmlElement metaTag in document.GetElementsByTagName("html"))
            {
                string language = metaTag.GetAttribute("lang");
                if (!string.IsNullOrEmpty(language))
                {
                    return language;
                }
            }

            // If the language type is not found, an empty string is returned by default.
            return string.Empty;
        }//ParsingHtml_GetLanguage

        //public virtual string GetContentType(string url, HtmlDocument doc)
        //{

        //}

        public virtual string GetTital(HtmlDocument doc)
        {
            string og_key = "og:title";
            string og_value = GetContent_ByMetaProperty(doc, og_key);

            if (!string.IsNullOrEmpty(og_value))
                return og_value;
            else
                return doc.Title;

        }//GetTital(HtmlDocument doc)

        public virtual string GetSrcMeida(HtmlDocument doc)
        {
            return null;

        }//GetTital(HtmlDocument doc)

        public virtual string GetSummary(HtmlDocument doc)
        {
            string og_key = "og:description";
            string og_value = GetContent_ByMetaProperty(doc, og_key);

            return og_value;

        }//GetTital(HtmlDocument doc)

        public virtual string GetPageTime(HtmlDocument doc)
        {
            string og_key = "article:published_time";
            string og_value = GetContent_ByMetaProperty(doc, og_key);

            if (og_value == null)
                return null;

            DateTime dt;
            if (DateTime.TryParse(og_value, out dt))
                return GetTimeStr_UTC(dt);
            else
                return og_value;

        }//GetTital(HtmlDocument doc)

        public static string GetTimeStr_UTC(DateTime _dt)
        {
            return _dt.ToUniversalTime().ToString("yyyyMMdd-HHmmss");
        }


        public virtual string GetKeywords(HtmlDocument doc)
        {
            string keyStr = "keywords";
            string valueStr = GetContent_ByMetaName(doc, keyStr);

            return valueStr;

        }//GetTital(HtmlDocument doc)

        public virtual string GetText(HtmlDocument doc)
        {
            return null;
        }//GetTital(HtmlDocument doc)


        public string GetContent_ByMetaProperty(HtmlDocument doc, string _keyName)
        {
            string attributeType = "property";
            return GetContent_ByMetaProperty(doc, attributeType, _keyName);
        }

        public string GetContent_ByMetaName(HtmlDocument doc, string _keyName)
        {
            string attributeType = "name";
            return GetContent_ByMetaProperty(doc, attributeType, _keyName);
        }


        public string GetContent_ByMetaProperty(HtmlDocument doc,string attributeType, string _keyName)
        {
            string valueType = "content";

            string og_value = null;
            HtmlElementCollection metaTags = doc.GetElementsByTagName("meta");
            foreach (HtmlElement tag in metaTags)
            {
                string property = tag.GetAttribute(attributeType);
                if (property == _keyName)
                {
                    og_value = tag.GetAttribute(valueType);
                    break;
                }
            }

            return og_value;
        }


        public virtual string GetImageUrl(HtmlDocument doc)
        {
            return null;
        }

        public virtual string GetVideoUrl(HtmlDocument doc)
        {
            return null;
        }

        //public virtual string GetPageDate(HtmlDocument doc)
        //{

        //}

        public virtual string GetAuthor(HtmlDocument doc)
        {
            string key = "article:author";
            return this.GetContent_ByMetaProperty(doc, key);
        }

        public virtual string GetArticalText(HtmlDocument doc)
        {
            return null;
        }

    }//class CPageParser_Base
}//namespace
