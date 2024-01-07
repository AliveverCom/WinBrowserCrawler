==================================


2004-01-07
1. Completed: EnhancedCPageTaskList and CSiteTask.LoadFromFile()。Improved project loading speed by more than 100 times.
2. Pending Bug：WebBrowser.Title,WebBrowser.DocumentText,they often occur random Exception: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.


==================================


2024-01-02 and 2024-01-03

1. Debuging.
2. Tested: 10 different types of websites (news, e-commerce) in different languages, each with 10,000 pages.
3. Completed: The PageParser series has been added, providing general web page parsing functions and customizable web page parsing subclasses.


==================================


2024-01-01

1. Debuging. 
2. Test passed: Downloaded 150 websites at the same time, and each website downloaded 2,000 pages.


==================================


2023-12-26

1. Completed: Enhanced UI, it can now display a list of all executing Site tasks.
2. Completed: Added the Save and Load functions of Project and SiteTask so that the crawler task can be terminated at any time or started again at any time.


==================================


2023-12-25

1. Completed: Enhanced UrlListBrowserDlg. Enables it to handle various web page errors.
        a. Web pages or links are redirected maliciously.
	c. The web page or link is maliciously refreshed.
	d, 404 or any error page.
	e. Malicious duplicate content pages, although their URLs are different.


2. Completed: Enhanced CSiteTaskMgr.
        a. Added new attribute ErrorURLs and its related methods. Used to manage various web page errors.
	b. Added the function of finding duplicate web content.
	c. Added a mechanism to automatically replace fields in url. Used to deal with malicious URLs on websites


==================================


2023-12-24

1. Plan: Add function: Save SiteTask to file, in the site task folder.
2. Plan: I enhanced the CSiteTask class and added depth-first list, blacklist, and PendingList.


==================================


2023-12-23
1. Completed: I enhanced the CUrlListBrowserDlg class so that it can prevent WebBrowser from loading images in web pages.
2. Completed: I enhanced the CPageTask class so that it can parse the basic information of the web page from the HtmlDocument.
3. Completed: I created the CPageInfo class and transferred many task-independent properties and parsing functions in PageTask to CPageInfo.
4. Completed: I analyzed the html of bbc.com. And found that the article page contains the following characteristics.
   <main id="main-content" data-testid="main-content"><article class="ssrcss-pv1rh6-ArticleWrapper e1nh2i2l5">

1. Erledigt: Ich habe die Klasse CUrlListBrowserDlg erweitert, sodass sie WebBrowser daran hindern kann, Bilder in Webseiten zu laden.
2. Erledigt: Ich habe die CPageTask-Klasse erweitert, sodass sie die grundlegenden Informationen der Webseite aus dem HtmlDocument analysieren kann.
3. Erledigt: Ich habe die CPageInfo-Klasse erstellt und viele aufgabenunabhängige Eigenschaften und Parsing-Funktionen in PageTask nach CPageInfo übertragen.
4. Erledigt: Ich habe den HTML-Code von bbc.com analysiert. Ich habe festgestellt, dass die Artikelseite die folgenden Merkmale aufweist.
   <main id="main-content" data-testid="main-content"><article class="ssrcss-pv1rh6-ArticleWrapper e1nh2i2l5">


==================================


2023-12-22
1. Completed: I created the CLogFile. And change all Excetpions to non-blocking logging.
2. Completed: I created CSiteTaskMgr to manage concurrent crawling of multiple websites.
3. Completed: I enhanced the CUrlListBrowserDlg class so that this window can run in another separate thread and no longer blocks the main thread.

1. Erledigt: Ich habe die CLogFile erstellt. Und ändern Sie alle Ausnahmen auf nicht blockierende Protokollierung.
2. Erledigt: Ich habe CSiteTaskMgr erstellt, um das gleichzeitige Crawlen mehrerer Websites zu verwalten.
3. Erledigt: Ich habe die Klasse CUrlListBrowserDlg erweitert, sodass dieses Fenster in einem anderen separaten Thread ausgeführt werden kann und den Hauptthread nicht länger blockiert.


==================================


2023-12-17
1. Completed: The browser crawler project was created.
2. Completed: Created CUrlListBrowserDlg to implement the most basic WebBrowser function of downloading the specified URL.
3. Completed: Created and implemented CPageTask, CSiteTask.
4. Completed: Automatic breadth-first downloading implemented.

1. Erledigt: Das Browser-Crawler-Projekt wurde erstellt.
2. Erledigt: CUrlListBrowserDlg erstellt, um die grundlegendste WebBrowser-Funktion zum Herunterladen der angegebenen URL zu implementieren.
3. Erledigt: CPageTask, CSiteTask erstellt und implementiert.
4. Erledigt: Automatisches Breiten-First-Download implementiert.
