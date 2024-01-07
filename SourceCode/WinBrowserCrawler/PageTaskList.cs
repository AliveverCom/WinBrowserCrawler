using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alivever.Com.WinBrowserCrawler
{
    public class CPageTaskList//: IEnumerator<CPageTask>
    {
        /// <summary>
        /// It will be file full path when this.SaveRute = FileSystem.
        /// It will be Database and table name when this.SaveRute = xxDB.
        /// </summary>
        public string SavePath;

        public ESaveRute SaveRute = ESaveRute.None;

        /// <summary>
        /// True: after a new item is add into TaskList, then this item will be auto-write into file(this.FileFullPath).
        /// </summary>
        public bool IsAutoAppendNewItem2File = false;

        private readonly List<CPageTask> TaskList = new List<CPageTask>();

        /// <summary>
        /// this HashSet only save urls in TaskList, to accellrate the fun IsExists();
        /// </summary>
        private readonly HashSet<string> Urls = new HashSet<string>();

        public IEnumerable<string> GetAllUrls()
        {
            return this.Urls;
        }

        public void SetAsAutoSaveFile(string fileFullPath)
        {
            this.SaveRute= ESaveRute.FileSystem;
            this.SavePath = fileFullPath;
            this.IsAutoAppendNewItem2File = true;
        }

        public bool Contains(string url)
        {
            //return this.TaskList.FirstOrDefault(a => a.Url == url) != null;
            return Urls.Contains(url);
        }

        public IEnumerable<CPageTask> GetReadonlyList()
        {
            return this.TaskList.Select(a =>a);
        }

        public int Count()
        {
           return  this.TaskList.Count;
        }

        public void Remove(CPageTask _task)
        {
            this.Urls.Remove(_task.Url);
            this.TaskList.Remove(_task);
        }

        public void RemoveRange(IEnumerable<string> _urls)
        {
            List<string> urlList = _urls.ToList();
            this.TaskList.RemoveAll( a => urlList.Contains(a.Url));
            urlList.ForEach(a=> this.Urls.Remove(a));
        }

        /// <summary>
        /// add one new task object into list
        /// </summary>
        /// <param name="_task"></param>
        public void Add(CPageTask _task, bool bAppend2File = true)
        {
            TaskList.Add(_task);
            this.Urls.Add(_task.Url);

            if (!this.IsAutoAppendNewItem2File)
                return;

            if (string.IsNullOrEmpty(SavePath))
                throw new Exception("SavePath can't be null or empty");

            if(bAppend2File && this.IsAutoAppendNewItem2File)
                GJson.Append2File(SavePath, new List<CPageTask>() { _task });

        }//Add(CPageTask _task)

        /// <summary>
        /// Merge all new PageTasks from _ptl in to this.CPageTask
        /// </summary>
        /// <param name="_ptl"></param>
        public void Merge( CPageTaskList _ptl, Func<int> FunNewID)
        {
            foreach (CPageTask crrPage in _ptl.TaskList)
            {
                // if new siteTask is not in this.SiteTasks, then add it in to this.SiteTasks
                if ( !this.Urls.Contains(crrPage.Url) )
                {
                    crrPage.TaskId = FunNewID();
                    this.Add(crrPage);
                    continue;
                }

                if (this.TaskList.FirstOrDefault(a => a.Url == crrPage.Url && a.DownTime == crrPage.DownTime) == null)
                {
                    crrPage.TaskId = FunNewID();
                    this.Add(crrPage);
                    continue;
                }
            }//foreach (CPageTask crrPage in _ptl.TaskList)

        }//Merge( CPageTaskList _ptl)

        /// <summary>
        /// add one new task object into list
        /// </summary>
        /// <param name="_task"></param>
        public void AddRange(IEnumerable< CPageTask> _taskList ,bool bAppend2File = true)
        {
            TaskList.AddRange(_taskList);
            foreach( var crrItem in _taskList)
                this.Urls.Add(crrItem.Url);

            if (!this.IsAutoAppendNewItem2File)
                return;

            if (string.IsNullOrEmpty(SavePath))
                throw new Exception("SavePath can't be null or empty");

            if (bAppend2File && this.IsAutoAppendNewItem2File)
                GJson.Append2File(SavePath, _taskList);

        }//Add(CPageTask _task)

        /// <summary>
        /// check all page task to find if any page html MD5 is the same.
        /// </summary>
        /// <param name="_htmlMD5"></param>
        /// <returns></returns>
        public bool HasSameHtmlMD5(string _htmlMD5)
        {
            if (string.IsNullOrEmpty(_htmlMD5))
                return false;

            var rst = this.TaskList.FirstOrDefault(a => a.HtmlDM5 == _htmlMD5);

            return rst != null;
        }


        /// <summary>
        /// get a new page tash that should be executed.
        /// return null if no any new task found.
        /// </summary>
        public CPageTask GetNextPageTask_Created()
        {
            var result = this.TaskList.LastOrDefault(a => a.Exe == EExecuteStatus.Created);

            if (result == null)
                return null;
            else
                return result;
        }//GetNextPageTask_Created()

        public bool IsUrlExist(string url)
        {
            return this.Urls.Contains(url);
            //var result = this.TaskList.FirstOrDefault(a => a.Url == url);

            //if (result == null )
            //    return false;
            //else
            //    return true;
        }//GetNextPageTask_Created()

        public void Clear()
        {
            this.TaskList.Clear();
            this.Urls.Clear();
        }

        public void Save2File()
        {
            if (this.SaveRute != ESaveRute.FileSystem)
                throw new Exception("Can't save PageTaskList to file because this.SaveRute != ESaveRute.FileSystem.");
            //string dirpath = Path.Combine(this.GetStorageRootDir(), "debug");
            lock (this.TaskList)
                GJson.Write2File(this.SavePath, this.TaskList);
        }//Save2File(string _fullFilePath)

        public void Save2File( DateTime _tBegin)
        {
            if (this.SaveRute != ESaveRute.FileSystem)
                throw new Exception("Can't save PageTaskList to file because this.SaveRute != ESaveRute.FileSystem.");
            //string dirpath = Path.Combine(this.GetStorageRootDir(), "debug");

            lock (this.TaskList)
            {
                IEnumerable<CPageTask> tasks = from a in this.TaskList where a.TaskTime > _tBegin select a;
                GJson.Write2File(this.SavePath, tasks);
            }
        }//Save2File(string _fullFilePath)

        public CPageTask LastOrDefault()
        {
            return this.TaskList.LastOrDefault();
        }

        /// <summary>
        /// Load From a File , in wich each row is a json that can be desirilized into an object. 
        /// </summary>
        /// <param name="_fullFilePath"></param>
        /// <param name="_bClearCurrentListBeforLoading"></param>
        /// <param name="filterList"> if a loaeded object is already exits in filterList, then this item will not be added into PageList </param>
        /// <exception cref="Exception"></exception>
        public void LoadFromFile(string _fullFilePath, 
            bool _bClearCurrentListBeforLoading,
            CPageTaskList filterList,
            bool _bDeduplicate )
        {
            if (!File.Exists(_fullFilePath))
                throw new Exception("File doesn't exist. path= "+ _fullFilePath);

            if (_bClearCurrentListBeforLoading)
                this.Clear();

            List<CPageTask> rstList = GJson.LoadListFromFile<CPageTask>(_fullFilePath);

            // use filterList and add loaded items
            if (filterList != null && filterList.Count() != 0)
            {
                foreach (CPageTask crrTask in rstList)
                {
                    if (_bDeduplicate && filterList.Contains(crrTask.Url)) //filterList.TaskList.FirstOrDefault(a => a.Url == crrTask.Url) != null)
                        continue;
                    else
                        this.Add(crrTask, false);
                }
            }
            else
                this.AddRange(rstList, false);

        }//LoadFromFile

        public CPageTask GetTaskByUrl(string url)
        {
            if (!this.Urls.Contains(url))
                return null;

            return this.TaskList.FirstOrDefault(a => a.Url == url);
        }

    }//class CPageTaskList
}
