namespace Alivever.Com.WinBrowserCrawler
{
    partial class Form_SiteTaskEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_SiteUrl = new System.Windows.Forms.TextBox();
            this.Txt_SaveDir = new System.Windows.Forms.TextBox();
            this.Lab_SaveDir = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "SiteUrl";
            // 
            // Txt_SiteUrl
            // 
            this.Txt_SiteUrl.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Txt_SiteUrl.Enabled = false;
            this.Txt_SiteUrl.Location = new System.Drawing.Point(99, 34);
            this.Txt_SiteUrl.Name = "Txt_SiteUrl";
            this.Txt_SiteUrl.Size = new System.Drawing.Size(466, 22);
            this.Txt_SiteUrl.TabIndex = 1;
            // 
            // Txt_SaveDir
            // 
            this.Txt_SaveDir.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Txt_SaveDir.Enabled = false;
            this.Txt_SaveDir.Location = new System.Drawing.Point(99, 81);
            this.Txt_SaveDir.Name = "Txt_SaveDir";
            this.Txt_SaveDir.Size = new System.Drawing.Size(466, 22);
            this.Txt_SaveDir.TabIndex = 3;
            // 
            // Lab_SaveDir
            // 
            this.Lab_SaveDir.AutoSize = true;
            this.Lab_SaveDir.Location = new System.Drawing.Point(35, 81);
            this.Lab_SaveDir.Name = "Lab_SaveDir";
            this.Lab_SaveDir.Size = new System.Drawing.Size(39, 16);
            this.Lab_SaveDir.TabIndex = 2;
            this.Lab_SaveDir.Text = "Save";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(581, 34);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(265, 502);
            this.propertyGrid1.TabIndex = 4;
            // 
            // Form_SiteTaskEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 548);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.Txt_SaveDir);
            this.Controls.Add(this.Lab_SaveDir);
            this.Controls.Add(this.Txt_SiteUrl);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form_SiteTaskEditor";
            this.Text = "Form_SiteTaskEditor";
            this.Load += new System.EventHandler(this.Form_SiteTaskEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_SiteUrl;
        private System.Windows.Forms.TextBox Txt_SaveDir;
        private System.Windows.Forms.Label Lab_SaveDir;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}