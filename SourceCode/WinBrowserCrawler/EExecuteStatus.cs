using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alivever.Com.WinBrowserCrawler
{
    public enum EExecuteStatus
    {
        Created = 1,
        Doing =2,
        Finshed=3,
        Error=4,
        Canceled=5,
        Pending=6,
        Duplicated =7,
        Parsed = 8,
    }
}
