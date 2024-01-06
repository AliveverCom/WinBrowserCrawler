using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alivever.Com.WinBrowserCrawler
{
    public enum EPageType
    {
        Unknown = 0,

        /// <summary>
        /// a tipical Article or News Page. it will have topic, 
        /// summary, text, author, page date, etc.
        /// </summary>
        OneArticle = 1,

        /// <summary>
        /// just like blog page. that are many Articles with their full contents in the page.
        /// </summary>
        MultiArticle =2 ,

        /// <summary>
        /// It is similar to the list page of a news column, 
        /// or the table of contents of a book. It provides many links, 
        /// each link leading to a specific news or article.
        ///  Notice!
        /// 1. The content of ‘ListPage’ only focuses on a specific topic area. It is depth first.
        /// 2. The content of ‘Portol’ will span multiple different topics, and it is breadth first.
        /// </summary>
        ListPage = 3,

        /// <summary>
        /// It is similar to a sitemap or the homepage of a large news website. 
        /// Its main purpose is to lead you to as many columns or topics as possible.
        ///  Notice!
        /// 1. The content of ‘ListPage’ only focuses on a specific topic area. It is depth first.
        /// 2. The content of ‘Portol’ will span multiple different topics, and it is breadth first.

        /// </summary>
        Protal = 4 ,
    }//enum EPageType
}
