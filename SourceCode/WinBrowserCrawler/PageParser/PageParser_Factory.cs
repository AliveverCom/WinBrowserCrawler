using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alivever.Com.WinBrowserCrawler.PageParser
{
    public class CPageParser_Factory
    {
        public static CPageParser_Base CreatePageParser(string mainDomain)
        {
            switch(mainDomain)
            {
                case "sina.com.cn": return new CPP_sina_com_cn();
                case "bbc.com": return new CPP_bbc_com();
            }

            return new CPageParser_Base();
        }

    }//class CPageParser_Factory
}//namespace
