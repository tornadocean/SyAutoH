namespace MCSControl
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("OHT Info");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Stocker Info");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Fork Info");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Mes Command");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Online Log");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Offine Log");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Alarm");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("System Log", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLable_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_NULL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_PushTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_User = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewPage = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerGuiUpdateData = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1166, 499);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1166, 546);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLable_Status,
            this.toolStripStatusLabel_NULL,
            this.toolStripStatusLabel_PushTime,
            this.toolStripStatusLabel_User});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1166, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLable_Status
            // 
            this.toolStripStatusLable_Status.Name = "toolStripStatusLable_Status";
            this.toolStripStatusLable_Status.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLable_Status.Text = "Ready";
            // 
            // toolStripStatusLabel_NULL
            // 
            this.toolStripStatusLabel_NULL.Name = "toolStripStatusLabel_NULL";
            this.toolStripStatusLabel_NULL.Size = new System.Drawing.Size(1009, 17);
            this.toolStripStatusLabel_NULL.Spring = true;
            // 
            // toolStripStatusLabel_PushTime
            // 
            this.toolStripStatusLabel_PushTime.Name = "toolStripStatusLabel_PushTime";
            this.toolStripStatusLabel_PushTime.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel_PushTime.Text = "PushTime";
            // 
            // toolStripStatusLabel_User
            // 
            this.toolStripStatusLabel_User.Name = "toolStripStatusLabel_User";
            this.toolStripStatusLabel_User.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel_User.Text = "User";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewPage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Bisque;
            this.splitContainer1.Size = new System.Drawing.Size(1166, 499);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewPage
            // 
            this.treeViewPage.BackColor = System.Drawing.Color.Beige;
            this.treeViewPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewPage.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeViewPage.FullRowSelect = true;
            this.treeViewPage.HideSelection = false;
            this.treeViewPage.LineColor = System.Drawing.Color.DodgerBlue;
            this.treeViewPage.Location = new System.Drawing.Point(0, 0);
            this.treeViewPage.Name = "treeViewPage";
            treeNode1.Name = "nodeOHTInfo";
            treeNode1.Text = "OHT Info";
            treeNode2.Name = "nodeSTKInfo";
            treeNode2.Text = "Stocker Info";
            treeNode3.Name = "nodeForkInfo";
            treeNode3.Text = "Fork Info";
            treeNode4.Name = "nodeMesCommand";
            treeNode4.Text = "Mes Command";
            treeNode5.Name = "nodeLogOnline";
            treeNode5.Text = "Online Log";
            treeNode6.Name = "nodeLogOffline";
            treeNode6.Text = "Offine Log";
            treeNode7.Name = "nodeAlarm";
            treeNode7.Text = "Alarm";
            treeNode8.Name = "nodeLogSystem";
            treeNode8.Text = "System Log";
            this.treeViewPage.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode8});
            this.treeViewPage.Size = new System.Drawing.Size(169, 499);
            this.treeViewPage.TabIndex = 0;
            this.treeViewPage.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1166, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.userToolStripMenuItem.Text = "File";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timerGuiUpdateData.Interval = 300;
            this.timerGuiUpdateData.Enabled = true;
            this.timerGuiUpdateData.Tick += new System.EventHandler(this.timerGuiUpdateData_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 546);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLable_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_NULL;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_User;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewPage;
        private System.Windows.Forms.Timer timerGuiUpdateData;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_PushTime;
    }
}