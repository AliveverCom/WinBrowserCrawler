using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alivever.Com.WinBrowserCrawler.PageParser
{
    public class CPP_bbc_com : CPageParser_Base
    {
        /// <summary>
        /// if you have to process all Hhml Elements in the doc. please overwrite it.
        /// </summary>
        protected override void FillAttributesByAllHhmlElements(
            HtmlDocument _doc,
            CSiteTask _siteTask,
            CPageInfo _pi)
        {
            HtmlElement idxElem = _doc.GetElementById("index-page");
            if (idxElem != null)
            {
                _pi.PageType = EPageType.Protal;
                return;
            }


            HtmlElementCollection articleElem = _doc.GetElementsByTagName("article");
            if (articleElem.Count > 3)
            {
                _pi.PageType = EPageType.ListPage;
                return;
            }


            HtmlElementCollection elements = _doc.GetElementsByTagName("*");
            var htmlElements = elements.Cast<HtmlElement>();
            //    .Where(el => el.GetAttribute("className") == "main-title")
            //    .ToList();

            bool bBeginArticle = false;
                    StringBuilder sbTxt = new StringBuilder();
            foreach (var cttItem in htmlElements)
            {
                string lowTag = cttItem.TagName.ToLower();
                ///// get real article text <p class="show_author">AAA：AuthorName SN241</p>
                if (lowTag == "article" )
                {
                    bBeginArticle = true;
                    continue;
                }

                if (bBeginArticle && lowTag == "p" )
                {
                    sbTxt.AppendLine(cttItem.InnerText);
                    continue;
                }

                if (bBeginArticle && lowTag == "img"&& _pi.ImgUrl == null)
                {
                    int nWidth;
                    if ( int.TryParse( cttItem.GetAttribute("width"), out nWidth) )
                        _pi.ImgUrl = cttItem.GetAttribute("src");
                    continue;
                }

                if (cttItem.GetAttribute("data-component") == "topic-list"
                    || lowTag == "/article")
                {
                    break;
                }

            }//foreach (var cttItem in htmlElements)
            string preTxt = sbTxt.ToString();

            if (bBeginArticle)
            {
                _pi.PageType = EPageType.OneArticle;
                _pi.Text = preTxt;
                sbTxt.Clear();
            }
        }//FillAttributesByAllHhmlElements()

    }//class CPP_bbc_com
}
