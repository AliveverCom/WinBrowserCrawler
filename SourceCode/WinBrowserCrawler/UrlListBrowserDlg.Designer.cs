namespace Alivever.Com.WinBrowserCrawler
{
    partial class CUrlListBrowserDlg
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.Lab_report = new System.Windows.Forms.Label();
            this.Lab_PageLoad = new System.Windows.Forms.Label();
            this.Btn_Pause = new System.Windows.Forms.Button();
            this.Btn_GoOn = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_GloFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(549, 306);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.TabStop = false;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            // 
            // Lab_report
            // 
            this.Lab_report.AutoSize = true;
            this.Lab_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_report.Location = new System.Drawing.Point(12, 20);
            this.Lab_report.Name = "Lab_report";
            this.Lab_report.Size = new System.Drawing.Size(233, 15);
            this.Lab_report.TabIndex = 1;
            this.Lab_report.Text = "Report: Total 000/ OK 000/ Err 000/ Q 000";
            // 
            // Lab_PageLoad
            // 
            this.Lab_PageLoad.AutoSize = true;
            this.Lab_PageLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_PageLoad.Location = new System.Drawing.Point(15, 56);
            this.Lab_PageLoad.Name = "Lab_PageLoad";
            this.Lab_PageLoad.Size = new System.Drawing.Size(347, 15);
            this.Lab_PageLoad.TabIndex = 2;
            this.Lab_PageLoad.Text = "CrrPage: Load 000s/ Parsing OK/  InnerURL 000/ OutURL 000/";
            // 
            // Btn_Pause
            // 
            this.Btn_Pause.BackColor = System.Drawing.Color.LightSalmon;
            this.Btn_Pause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Pause.Location = new System.Drawing.Point(15, 87);
            this.Btn_Pause.Name = "Btn_Pause";
            this.Btn_Pause.Size = new System.Drawing.Size(87, 41);
            this.Btn_Pause.TabIndex = 3;
            this.Btn_Pause.Text = "Pause";
            this.Btn_Pause.UseVisualStyleBackColor = false;
            this.Btn_Pause.Click += new System.EventHandler(this.Btn_Pause_Click);
            // 
            // Btn_GoOn
            // 
            this.Btn_GoOn.BackColor = System.Drawing.Color.YellowGreen;
            this.Btn_GoOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_GoOn.Location = new System.Drawing.Point(12, 139);
            this.Btn_GoOn.Name = "Btn_GoOn";
            this.Btn_GoOn.Size = new System.Drawing.Size(87, 41);
            this.Btn_GoOn.TabIndex = 4;
            this.Btn_GoOn.Text = "GoOn";
            this.Btn_GoOn.UseVisualStyleBackColor = false;
            this.Btn_GoOn.Click += new System.EventHandler(this.Btn_GoOn_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.OrangeRed;
            this.Btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Close.Location = new System.Drawing.Point(12, 186);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(87, 41);
            this.Btn_Close.TabIndex = 5;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_GloFile
            // 
            this.Btn_GloFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Btn_GloFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_GloFile.Location = new System.Drawing.Point(127, 87);
            this.Btn_GloFile.Name = "Btn_GloFile";
            this.Btn_GloFile.Size = new System.Drawing.Size(85, 41);
            this.Btn_GloFile.TabIndex = 6;
            this.Btn_GloFile.Text = "Log File";
            this.Btn_GloFile.UseVisualStyleBackColor = false;
            this.Btn_GloFile.Click += new System.EventHandler(this.Btn_GloFile_Click);
            // 
            // CUrlListBrowserDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 306);
            this.Controls.Add(this.Btn_GloFile);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_GoOn);
            this.Controls.Add(this.Btn_Pause);
            this.Controls.Add(this.Lab_PageLoad);
            this.Controls.Add(this.Lab_report);
            this.Controls.Add(this.webBrowser1);
            this.Name = "CUrlListBrowserDlg";
            this.Text = "UrlListBrowserDlg";
            this.Load += new System.EventHandler(this.UrlListBrowserDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label Lab_report;
        private System.Windows.Forms.Label Lab_PageLoad;
        private System.Windows.Forms.Button Btn_Pause;
        private System.Windows.Forms.Button Btn_GoOn;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_GloFile;
    }
}