namespace Alivever.Com.WinBrowserCrawler
{
    partial class MainDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Test = new System.Windows.Forms.Button();
            this.Lab_DownloadRootDir = new System.Windows.Forms.Label();
            this.Txt_DownloadRoot = new System.Windows.Forms.TextBox();
            this.Btn_Test3Website = new System.Windows.Forms.Button();
            this.Btn_CreateAllNewsSite = new System.Windows.Forms.Button();
            this.CMBox_SaveDir = new System.Windows.Forms.ComboBox();
            this.Btn_PauseAll = new System.Windows.Forms.Button();
            this.Btn_StartProject = new System.Windows.Forms.Button();
            this.Btn_GoOnAll = new System.Windows.Forms.Button();
            this.Btn_CloseAll = new System.Windows.Forms.Button();
            this.LstView_SiteTasks = new System.Windows.Forms.ListView();
            this.TableCol_SiteRoot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_Exe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_Active = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_Done = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_Error = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_Pending = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_BlackList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_LoadMs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableCol_CrrPageUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lab_SystemUsage = new System.Windows.Forms.Label();
            this.Menu_Top = new System.Windows.Forms.MenuStrip();
            this.Menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_NewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_LoadProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_MergeProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_CloseProject = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Version = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_UserManual = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Donate = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_AddSite = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Activate = new System.Windows.Forms.Button();
            this.Btn_SelectAllInListView = new System.Windows.Forms.CheckBox();
            this.Btn_OpenProjectLog = new System.Windows.Forms.Button();
            this.Btn_Edit = new System.Windows.Forms.Button();
            this.Menu_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Test
            // 
            this.Btn_Test.BackColor = System.Drawing.Color.Silver;
            this.Btn_Test.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Test.Location = new System.Drawing.Point(754, 568);
            this.Btn_Test.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Btn_Test.Name = "Btn_Test";
            this.Btn_Test.Size = new System.Drawing.Size(62, 26);
            this.Btn_Test.TabIndex = 0;
            this.Btn_Test.Text = "Test 1";
            this.Btn_Test.UseVisualStyleBackColor = false;
            this.Btn_Test.Visible = false;
            this.Btn_Test.Click += new System.EventHandler(this.Btn_Test_Click);
            // 
            // Lab_DownloadRootDir
            // 
            this.Lab_DownloadRootDir.AutoSize = true;
            this.Lab_DownloadRootDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_DownloadRootDir.Location = new System.Drawing.Point(29, 44);
            this.Lab_DownloadRootDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lab_DownloadRootDir.Name = "Lab_DownloadRootDir";
            this.Lab_DownloadRootDir.Size = new System.Drawing.Size(81, 16);
            this.Lab_DownloadRootDir.TabIndex = 1;
            this.Lab_DownloadRootDir.Text = "Project Root";
            // 
            // Txt_DownloadRoot
            // 
            this.Txt_DownloadRoot.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Txt_DownloadRoot.Enabled = false;
            this.Txt_DownloadRoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_DownloadRoot.Location = new System.Drawing.Point(118, 44);
            this.Txt_DownloadRoot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Txt_DownloadRoot.Name = "Txt_DownloadRoot";
            this.Txt_DownloadRoot.Size = new System.Drawing.Size(556, 22);
            this.Txt_DownloadRoot.TabIndex = 2;
            // 
            // Btn_Test3Website
            // 
            this.Btn_Test3Website.BackColor = System.Drawing.Color.Silver;
            this.Btn_Test3Website.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Test3Website.Location = new System.Drawing.Point(835, 570);
            this.Btn_Test3Website.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Btn_Test3Website.Name = "Btn_Test3Website";
            this.Btn_Test3Website.Size = new System.Drawing.Size(62, 24);
            this.Btn_Test3Website.TabIndex = 3;
            this.Btn_Test3Website.Text = "Test 3";
            this.Btn_Test3Website.UseVisualStyleBackColor = false;
            this.Btn_Test3Website.Visible = false;
            this.Btn_Test3Website.Click += new System.EventHandler(this.Btn_Test3Website_Click);
            // 
            // Btn_CreateAllNewsSite
            // 
            this.Btn_CreateAllNewsSite.BackColor = System.Drawing.Color.Silver;
            this.Btn_CreateAllNewsSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CreateAllNewsSite.Location = new System.Drawing.Point(532, 568);
            this.Btn_CreateAllNewsSite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Btn_CreateAllNewsSite.Name = "Btn_CreateAllNewsSite";
            this.Btn_CreateAllNewsSite.Size = new System.Drawing.Size(127, 32);
            this.Btn_CreateAllNewsSite.TabIndex = 4;
            this.Btn_CreateAllNewsSite.Text = "All NewsSite";
            this.Btn_CreateAllNewsSite.UseVisualStyleBackColor = false;
            this.Btn_CreateAllNewsSite.Visible = false;
            this.Btn_CreateAllNewsSite.Click += new System.EventHandler(this.Btn_CreateAllNewsSite_Click);
            // 
            // CMBox_SaveDir
            // 
            this.CMBox_SaveDir.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.CMBox_SaveDir.Enabled = false;
            this.CMBox_SaveDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBox_SaveDir.FormattingEnabled = true;
            this.CMBox_SaveDir.Items.AddRange(new object[] {
            "c:\\temp\\WebSipder_test_1",
            "c:\\temp\\WebSipder_test_3",
            "d:\\WebSipder_AllNewsSite"});
            this.CMBox_SaveDir.Location = new System.Drawing.Point(389, 574);
            this.CMBox_SaveDir.Name = "CMBox_SaveDir";
            this.CMBox_SaveDir.Size = new System.Drawing.Size(121, 24);
            this.CMBox_SaveDir.TabIndex = 5;
            this.CMBox_SaveDir.Visible = false;
            this.CMBox_SaveDir.SelectedIndexChanged += new System.EventHandler(this.CMBox_SaveDir_SelectedIndexChanged);
            // 
            // Btn_PauseAll
            // 
            this.Btn_PauseAll.BackColor = System.Drawing.Color.LightSalmon;
            this.Btn_PauseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_PauseAll.Location = new System.Drawing.Point(701, 92);
            this.Btn_PauseAll.Name = "Btn_PauseAll";
            this.Btn_PauseAll.Size = new System.Drawing.Size(91, 41);
            this.Btn_PauseAll.TabIndex = 6;
            this.Btn_PauseAll.Text = "Pause";
            this.Btn_PauseAll.UseVisualStyleBackColor = false;
            this.Btn_PauseAll.Visible = false;
            this.Btn_PauseAll.Click += new System.EventHandler(this.Btn_PauseAll_Click);
            // 
            // Btn_StartProject
            // 
            this.Btn_StartProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_StartProject.Location = new System.Drawing.Point(593, 93);
            this.Btn_StartProject.Name = "Btn_StartProject";
            this.Btn_StartProject.Size = new System.Drawing.Size(91, 37);
            this.Btn_StartProject.TabIndex = 7;
            this.Btn_StartProject.Text = "Start";
            this.Btn_StartProject.UseVisualStyleBackColor = true;
            this.Btn_StartProject.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // Btn_GoOnAll
            // 
            this.Btn_GoOnAll.BackColor = System.Drawing.Color.LightGreen;
            this.Btn_GoOnAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_GoOnAll.Location = new System.Drawing.Point(717, 92);
            this.Btn_GoOnAll.Name = "Btn_GoOnAll";
            this.Btn_GoOnAll.Size = new System.Drawing.Size(87, 39);
            this.Btn_GoOnAll.TabIndex = 8;
            this.Btn_GoOnAll.Text = "Go On";
            this.Btn_GoOnAll.UseVisualStyleBackColor = false;
            this.Btn_GoOnAll.Visible = false;
            this.Btn_GoOnAll.Click += new System.EventHandler(this.Btn_GoOnAll_Click);
            // 
            // Btn_CloseAll
            // 
            this.Btn_CloseAll.BackColor = System.Drawing.Color.Red;
            this.Btn_CloseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CloseAll.Location = new System.Drawing.Point(810, 94);
            this.Btn_CloseAll.Name = "Btn_CloseAll";
            this.Btn_CloseAll.Size = new System.Drawing.Size(87, 39);
            this.Btn_CloseAll.TabIndex = 10;
            this.Btn_CloseAll.Text = "Close";
            this.Btn_CloseAll.UseVisualStyleBackColor = false;
            this.Btn_CloseAll.Visible = false;
            this.Btn_CloseAll.Click += new System.EventHandler(this.Btn_CloseAll_Click);
            // 
            // LstView_SiteTasks
            // 
            this.LstView_SiteTasks.CheckBoxes = true;
            this.LstView_SiteTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TableCol_SiteRoot,
            this.TableCol_Exe,
            this.TableCol_Active,
            this.TableCol_Done,
            this.TableCol_Error,
            this.TableCol_Pending,
            this.TableCol_BlackList,
            this.TableCol_LoadMs,
            this.TableCol_CrrPageUrl});
            this.LstView_SiteTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstView_SiteTasks.HideSelection = false;
            this.LstView_SiteTasks.Location = new System.Drawing.Point(26, 150);
            this.LstView_SiteTasks.Name = "LstView_SiteTasks";
            this.LstView_SiteTasks.Size = new System.Drawing.Size(881, 411);
            this.LstView_SiteTasks.TabIndex = 11;
            this.LstView_SiteTasks.UseCompatibleStateImageBehavior = false;
            this.LstView_SiteTasks.View = System.Windows.Forms.View.Details;
            // 
            // TableCol_SiteRoot
            // 
            this.TableCol_SiteRoot.Text = "       Site";
            this.TableCol_SiteRoot.Width = 194;
            // 
            // TableCol_Exe
            // 
            this.TableCol_Exe.Text = "Status";
            this.TableCol_Exe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TableCol_Active
            // 
            this.TableCol_Active.Text = "Active";
            this.TableCol_Active.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TableCol_Done
            // 
            this.TableCol_Done.Text = "Done";
            this.TableCol_Done.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TableCol_Error
            // 
            this.TableCol_Error.Text = "Error";
            this.TableCol_Error.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TableCol_Pending
            // 
            this.TableCol_Pending.Text = "Pending";
            this.TableCol_Pending.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TableCol_Pending.Width = 70;
            // 
            // TableCol_BlackList
            // 
            this.TableCol_BlackList.Text = "Blacklist";
            this.TableCol_BlackList.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TableCol_BlackList.Width = 69;
            // 
            // TableCol_LoadMs
            // 
            this.TableCol_LoadMs.Text = "Speed";
            this.TableCol_LoadMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TableCol_LoadMs.Width = 85;
            // 
            // TableCol_CrrPageUrl
            // 
            this.TableCol_CrrPageUrl.Text = "Current Page";
            this.TableCol_CrrPageUrl.Width = 305;
            // 
            // Lab_SystemUsage
            // 
            this.Lab_SystemUsage.AutoSize = true;
            this.Lab_SystemUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_SystemUsage.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Lab_SystemUsage.Location = new System.Drawing.Point(29, 576);
            this.Lab_SystemUsage.Name = "Lab_SystemUsage";
            this.Lab_SystemUsage.Size = new System.Drawing.Size(149, 18);
            this.Lab_SystemUsage.TabIndex = 12;
            this.Lab_SystemUsage.Text = "CPU:  % ;   Mem GB ";
            // 
            // Menu_Top
            // 
            this.Menu_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_File,
            this.Menu_About});
            this.Menu_Top.Location = new System.Drawing.Point(0, 0);
            this.Menu_Top.Name = "Menu_Top";
            this.Menu_Top.Size = new System.Drawing.Size(933, 24);
            this.Menu_Top.TabIndex = 13;
            this.Menu_Top.Text = "Menu Top";
            // 
            // Menu_File
            // 
            this.Menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_NewProject,
            this.MenuItem_LoadProject,
            this.MenuItem_MergeProject,
            this.MenuItem_CloseProject});
            this.Menu_File.Name = "Menu_File";
            this.Menu_File.Size = new System.Drawing.Size(73, 20);
            this.Menu_File.Text = "Menu_File";
            // 
            // MenuItem_NewProject
            // 
            this.MenuItem_NewProject.Name = "MenuItem_NewProject";
            this.MenuItem_NewProject.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_NewProject.Text = "New Project";
            this.MenuItem_NewProject.Click += new System.EventHandler(this.MenuItem_NewProject_Click);
            // 
            // MenuItem_LoadProject
            // 
            this.MenuItem_LoadProject.Name = "MenuItem_LoadProject";
            this.MenuItem_LoadProject.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_LoadProject.Text = "Load Project";
            this.MenuItem_LoadProject.Click += new System.EventHandler(this.MenuItem_LoadProject_Click);
            // 
            // MenuItem_MergeProject
            // 
            this.MenuItem_MergeProject.Name = "MenuItem_MergeProject";
            this.MenuItem_MergeProject.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_MergeProject.Text = "Merge Project";
            this.MenuItem_MergeProject.Click += new System.EventHandler(this.MenuItem_MergeProject_Click);
            // 
            // MenuItem_CloseProject
            // 
            this.MenuItem_CloseProject.Name = "MenuItem_CloseProject";
            this.MenuItem_CloseProject.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_CloseProject.Text = "Close Project";
            this.MenuItem_CloseProject.Click += new System.EventHandler(this.MenuItem_CloseProject_Click);
            // 
            // Menu_About
            // 
            this.Menu_About.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Version,
            this.MenuItem_UserManual,
            this.MenuItem_Donate});
            this.Menu_About.Name = "Menu_About";
            this.Menu_About.Size = new System.Drawing.Size(52, 20);
            this.Menu_About.Text = "About";
            // 
            // MenuItem_Version
            // 
            this.MenuItem_Version.Name = "MenuItem_Version";
            this.MenuItem_Version.Size = new System.Drawing.Size(140, 22);
            this.MenuItem_Version.Text = "Version";
            this.MenuItem_Version.Click += new System.EventHandler(this.MenuItem_Version_Click);
            // 
            // MenuItem_UserManual
            // 
            this.MenuItem_UserManual.Name = "MenuItem_UserManual";
            this.MenuItem_UserManual.Size = new System.Drawing.Size(140, 22);
            this.MenuItem_UserManual.Text = "User manual";
            this.MenuItem_UserManual.Click += new System.EventHandler(this.MenuItem_UserManual_Click);
            // 
            // MenuItem_Donate
            // 
            this.MenuItem_Donate.Name = "MenuItem_Donate";
            this.MenuItem_Donate.Size = new System.Drawing.Size(140, 22);
            this.MenuItem_Donate.Text = "Donate us";
            this.MenuItem_Donate.Click += new System.EventHandler(this.MenuItem_Donate_Click);
            // 
            // Btn_AddSite
            // 
            this.Btn_AddSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_AddSite.Location = new System.Drawing.Point(37, 93);
            this.Btn_AddSite.Name = "Btn_AddSite";
            this.Btn_AddSite.Size = new System.Drawing.Size(75, 40);
            this.Btn_AddSite.TabIndex = 14;
            this.Btn_AddSite.Text = "Add";
            this.Btn_AddSite.UseVisualStyleBackColor = true;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.Red;
            this.Btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Cancel.Location = new System.Drawing.Point(134, 94);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 39);
            this.Btn_Cancel.TabIndex = 15;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            // 
            // Btn_Activate
            // 
            this.Btn_Activate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Btn_Activate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Activate.Location = new System.Drawing.Point(227, 94);
            this.Btn_Activate.Name = "Btn_Activate";
            this.Btn_Activate.Size = new System.Drawing.Size(75, 39);
            this.Btn_Activate.TabIndex = 16;
            this.Btn_Activate.Text = "Activate";
            this.Btn_Activate.UseVisualStyleBackColor = false;
            this.Btn_Activate.Click += new System.EventHandler(this.Btn_Activate_Click);
            // 
            // Btn_SelectAllInListView
            // 
            this.Btn_SelectAllInListView.AutoSize = true;
            this.Btn_SelectAllInListView.Location = new System.Drawing.Point(33, 158);
            this.Btn_SelectAllInListView.Name = "Btn_SelectAllInListView";
            this.Btn_SelectAllInListView.Size = new System.Drawing.Size(15, 14);
            this.Btn_SelectAllInListView.TabIndex = 17;
            this.Btn_SelectAllInListView.UseVisualStyleBackColor = true;
            this.Btn_SelectAllInListView.CheckedChanged += new System.EventHandler(this.Btn_SelectAllInListView_CheckedChanged);
            // 
            // Btn_OpenProjectLog
            // 
            this.Btn_OpenProjectLog.Location = new System.Drawing.Point(733, 44);
            this.Btn_OpenProjectLog.Name = "Btn_OpenProjectLog";
            this.Btn_OpenProjectLog.Size = new System.Drawing.Size(83, 28);
            this.Btn_OpenProjectLog.TabIndex = 18;
            this.Btn_OpenProjectLog.Text = "See Log";
            this.Btn_OpenProjectLog.UseVisualStyleBackColor = true;
            this.Btn_OpenProjectLog.Click += new System.EventHandler(this.Btn_OpenProjectLog_Click);
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.Location = new System.Drawing.Point(321, 99);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(75, 31);
            this.Btn_Edit.TabIndex = 19;
            this.Btn_Edit.Text = "Edit";
            this.Btn_Edit.UseVisualStyleBackColor = true;
            this.Btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // MainDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 606);
            this.Controls.Add(this.Btn_Edit);
            this.Controls.Add(this.Btn_OpenProjectLog);
            this.Controls.Add(this.Btn_SelectAllInListView);
            this.Controls.Add(this.Btn_Activate);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_AddSite);
            this.Controls.Add(this.Lab_SystemUsage);
            this.Controls.Add(this.LstView_SiteTasks);
            this.Controls.Add(this.Btn_CloseAll);
            this.Controls.Add(this.Btn_GoOnAll);
            this.Controls.Add(this.Btn_StartProject);
            this.Controls.Add(this.Btn_PauseAll);
            this.Controls.Add(this.CMBox_SaveDir);
            this.Controls.Add(this.Btn_CreateAllNewsSite);
            this.Controls.Add(this.Btn_Test3Website);
            this.Controls.Add(this.Txt_DownloadRoot);
            this.Controls.Add(this.Lab_DownloadRootDir);
            this.Controls.Add(this.Btn_Test);
            this.Controls.Add(this.Menu_Top);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainDlg";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Menu_Top.ResumeLayout(false);
            this.Menu_Top.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Test;
        private System.Windows.Forms.Label Lab_DownloadRootDir;
        private System.Windows.Forms.TextBox Txt_DownloadRoot;
        private System.Windows.Forms.Button Btn_Test3Website;
        private System.Windows.Forms.Button Btn_CreateAllNewsSite;
        private System.Windows.Forms.ComboBox CMBox_SaveDir;
        private System.Windows.Forms.Button Btn_PauseAll;
        private System.Windows.Forms.Button Btn_StartProject;
        private System.Windows.Forms.Button Btn_GoOnAll;
        private System.Windows.Forms.Button Btn_CloseAll;
        private System.Windows.Forms.ListView LstView_SiteTasks;
        private System.Windows.Forms.ColumnHeader TableCol_SiteRoot;
        private System.Windows.Forms.ColumnHeader TableCol_Active;
        private System.Windows.Forms.ColumnHeader TableCol_Done;
        private System.Windows.Forms.ColumnHeader TableCol_Pending;
        private System.Windows.Forms.ColumnHeader TableCol_Error;
        private System.Windows.Forms.ColumnHeader TableCol_BlackList;
        private System.Windows.Forms.ColumnHeader TableCol_LoadMs;
        private System.Windows.Forms.ColumnHeader TableCol_CrrPageUrl;
        private System.Windows.Forms.Label Lab_SystemUsage;
        private System.Windows.Forms.MenuStrip Menu_Top;
        private System.Windows.Forms.ToolStripMenuItem Menu_File;
        private System.Windows.Forms.ToolStripMenuItem Menu_About;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_NewProject;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_LoadProject;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_MergeProject;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Version;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_UserManual;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Donate;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_CloseProject;
        private System.Windows.Forms.ColumnHeader TableCol_Exe;
        private System.Windows.Forms.Button Btn_AddSite;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Activate;
        private System.Windows.Forms.CheckBox Btn_SelectAllInListView;
        private System.Windows.Forms.Button Btn_OpenProjectLog;
        private System.Windows.Forms.Button Btn_Edit;
    }
}

