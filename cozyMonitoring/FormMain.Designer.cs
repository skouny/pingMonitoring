namespace cozyMonitoring
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControlResults = new System.Windows.Forms.TabControl();
            this.tabLastMinute = new System.Windows.Forms.TabPage();
            this.tabLastHour = new System.Windows.Forms.TabPage();
            this.tabLastDay = new System.Windows.Forms.TabPage();
            this.tabArchive = new System.Windows.Forms.TabPage();
            this.buttonArchiveRefresh = new System.Windows.Forms.Button();
            this.panelArchive = new System.Windows.Forms.Panel();
            this.dtArchive = new System.Windows.Forms.DateTimePicker();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mikroTikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControlResults.SuspendLayout();
            this.tabArchive.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(860, 438);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(860, 484);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(860, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControlResults);
            this.splitContainer.Size = new System.Drawing.Size(860, 438);
            this.splitContainer.SplitterDistance = 303;
            this.splitContainer.TabIndex = 0;
            // 
            // tabControlResults
            // 
            this.tabControlResults.Controls.Add(this.tabLastMinute);
            this.tabControlResults.Controls.Add(this.tabLastHour);
            this.tabControlResults.Controls.Add(this.tabLastDay);
            this.tabControlResults.Controls.Add(this.tabArchive);
            this.tabControlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlResults.Location = new System.Drawing.Point(0, 0);
            this.tabControlResults.Name = "tabControlResults";
            this.tabControlResults.SelectedIndex = 0;
            this.tabControlResults.Size = new System.Drawing.Size(553, 438);
            this.tabControlResults.TabIndex = 0;
            this.tabControlResults.SelectedIndexChanged += new System.EventHandler(this.TabControlResults_SelectedIndexChanged);
            // 
            // tabLastMinute
            // 
            this.tabLastMinute.Location = new System.Drawing.Point(4, 23);
            this.tabLastMinute.Name = "tabLastMinute";
            this.tabLastMinute.Size = new System.Drawing.Size(545, 411);
            this.tabLastMinute.TabIndex = 0;
            this.tabLastMinute.Text = "Last 60 Seconds";
            this.tabLastMinute.UseVisualStyleBackColor = true;
            // 
            // tabLastHour
            // 
            this.tabLastHour.Location = new System.Drawing.Point(4, 22);
            this.tabLastHour.Name = "tabLastHour";
            this.tabLastHour.Size = new System.Drawing.Size(545, 412);
            this.tabLastHour.TabIndex = 1;
            this.tabLastHour.Text = "Last 60 Minutes";
            this.tabLastHour.UseVisualStyleBackColor = true;
            // 
            // tabLastDay
            // 
            this.tabLastDay.Location = new System.Drawing.Point(4, 22);
            this.tabLastDay.Name = "tabLastDay";
            this.tabLastDay.Size = new System.Drawing.Size(545, 412);
            this.tabLastDay.TabIndex = 2;
            this.tabLastDay.Text = "Last 24 Hours";
            this.tabLastDay.UseVisualStyleBackColor = true;
            // 
            // tabArchive
            // 
            this.tabArchive.Controls.Add(this.buttonArchiveRefresh);
            this.tabArchive.Controls.Add(this.panelArchive);
            this.tabArchive.Controls.Add(this.dtArchive);
            this.tabArchive.Location = new System.Drawing.Point(4, 23);
            this.tabArchive.Name = "tabArchive";
            this.tabArchive.Size = new System.Drawing.Size(545, 411);
            this.tabArchive.TabIndex = 5;
            this.tabArchive.Text = "Archive";
            this.tabArchive.UseVisualStyleBackColor = true;
            // 
            // buttonArchiveRefresh
            // 
            this.buttonArchiveRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonArchiveRefresh.Location = new System.Drawing.Point(451, 5);
            this.buttonArchiveRefresh.Name = "buttonArchiveRefresh";
            this.buttonArchiveRefresh.Size = new System.Drawing.Size(86, 23);
            this.buttonArchiveRefresh.TabIndex = 2;
            this.buttonArchiveRefresh.Text = "Refresh";
            this.buttonArchiveRefresh.UseVisualStyleBackColor = true;
            this.buttonArchiveRefresh.Click += new System.EventHandler(this.buttonArchiveRefresh_Click);
            // 
            // panelArchive
            // 
            this.panelArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelArchive.Location = new System.Drawing.Point(0, 31);
            this.panelArchive.Name = "panelArchive";
            this.panelArchive.Size = new System.Drawing.Size(545, 380);
            this.panelArchive.TabIndex = 1;
            // 
            // dtArchive
            // 
            this.dtArchive.CustomFormat = "";
            this.dtArchive.Location = new System.Drawing.Point(3, 3);
            this.dtArchive.MaxDate = new System.DateTime(2019, 10, 15, 0, 0, 0, 0);
            this.dtArchive.MinDate = new System.DateTime(2019, 10, 1, 0, 0, 0, 0);
            this.dtArchive.Name = "dtArchive";
            this.dtArchive.Size = new System.Drawing.Size(279, 22);
            this.dtArchive.TabIndex = 0;
            this.dtArchive.ValueChanged += new System.EventHandler(this.dtArchive_ValueChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(860, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.dataPathToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // dataPathToolStripMenuItem
            // 
            this.dataPathToolStripMenuItem.Name = "dataPathToolStripMenuItem";
            this.dataPathToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.dataPathToolStripMenuItem.Text = "Data Path";
            this.dataPathToolStripMenuItem.Click += new System.EventHandler(this.DataPathToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mikroTikToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mikroTikToolStripMenuItem
            // 
            this.mikroTikToolStripMenuItem.Name = "mikroTikToolStripMenuItem";
            this.mikroTikToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.mikroTikToolStripMenuItem.Text = "MikroTik";
            this.mikroTikToolStripMenuItem.Click += new System.EventHandler(this.MikroTikToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoRefreshToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // autoRefreshToolStripMenuItem
            // 
            this.autoRefreshToolStripMenuItem.Checked = true;
            this.autoRefreshToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRefreshToolStripMenuItem.Name = "autoRefreshToolStripMenuItem";
            this.autoRefreshToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.autoRefreshToolStripMenuItem.Text = "Auto Refresh";
            this.autoRefreshToolStripMenuItem.Click += new System.EventHandler(this.AutoRefreshToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.HideToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.donateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.donateToolStripMenuItem.Text = "Donate!";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "cozyMonitoring";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 484);
            this.Controls.Add(this.toolStripContainer);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cozyMonitoring";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControlResults.ResumeLayout(false);
            this.tabArchive.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.TabControl tabControlResults;
        private System.Windows.Forms.TabPage tabLastMinute;
        private System.Windows.Forms.TabPage tabLastHour;
        private System.Windows.Forms.TabPage tabLastDay;
        private System.Windows.Forms.TabPage tabArchive;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoRefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mikroTikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtArchive;
        private System.Windows.Forms.Panel panelArchive;
        private System.Windows.Forms.Button buttonArchiveRefresh;
    }
}

