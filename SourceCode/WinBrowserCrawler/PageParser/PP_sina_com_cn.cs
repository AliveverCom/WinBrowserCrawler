using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Alivever.Com.WinBrowserCrawler.CUrlListBrowserDlg;

namespace Alivever.Com.WinBrowserCrawler.PageParser
{
    public class CPP_sina_com_cn : CPageParser_Base
    {

        public override string GetSrcMeida(HtmlDocument doc)
        {
            string key = "mediaid";
            return this.GetContent_ByMetaName(doc, key);
        }

        /// <summary>
        /// if you have to process all Hhml Elements in the doc. please overwrite it.
        /// </summary>
        protected override void FillAttributesByAllHhmlElements(
            HtmlDocument _doc, 
            CSiteTask _siteTask, 
            CPageInfo _pi)
        {
            HtmlElementCollection elements = _doc.GetElementsByTagName("*");
            var htmlElements = elements.Cast<HtmlElement>();
            //    .Where(el => el.GetAttribute("className") == "main-title")
            //    .ToList();


            foreach (var cttItem in htmlElements)
            {
                ///// get real tital<div class="article" id="article">
                if (cttItem.GetAttribute("className") == "main-title")
                {
                    _pi.Title = cttItem.InnerText;
                    continue;
                }

                ///// get real article text <p class="show_author">AAA：AuthorName SN241</p>
                if (cttItem.Id == "article")
                {
                    StringBuilder sb = new StringBuilder();
                    foreach( var crrP in cttItem.GetElementsByTagName("p").Cast<HtmlElement>())
                    {
                        if (crrP.GetAttribute("className") == "show_author")
                        {
                            _pi.Author = ParseAuthor(crrP.InnerText);
                            continue;
                        }

                        sb.AppendLine(crrP.InnerText);
                    }

                    string preTxt = sb.ToString();
                    _pi.Text = CPageParser_Base.DeleteAllHtmlElements(preTxt);

                    sb.Clear();
                    break;
                }

            }//foreach (var cttItem in htmlElements)

        }////FillAttributesByAllHhmlElements()

        public override string GetLanguage(HtmlDocument document)
        {
            string tpStr = base.GetLanguage(document);
            if (string.IsNullOrEmpty(tpStr))
                return "zh-CN";

            return tpStr;
        }
        private string ParseAuthor(string txt)
        {
            if (!txt.Contains("："))
                return txt;

            string[] segs = txt.Split('：');

            if (segs.Length > 1)
                return segs.Last();
            else
                return txt;
        }
    }//class
}//namespace
