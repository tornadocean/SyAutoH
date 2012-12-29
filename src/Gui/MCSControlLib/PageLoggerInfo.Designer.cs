namespace MCSControlLib
{
    partial class PageLoggerInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.listLog = new System.Windows.Forms.TabPage();
            this.listViewAlarm = new System.Windows.Forms.TabPage();
            this.listOfflineLog = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.User = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tBOnlineIDS = new System.Windows.Forms.TextBox();
            this.Pause_button = new System.Windows.Forms.Button();
            this.Filter_button = new System.Windows.Forms.Button();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.checkBoxInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxWarning = new System.Windows.Forms.CheckBox();
            this.checkBoxError = new System.Windows.Forms.CheckBox();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.tabControl1.SuspendLayout();
            this.listLog.SuspendLayout();
            this.listViewAlarm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.listLog);
            this.tabControl1.Controls.Add(this.listViewAlarm);
            this.tabControl1.Controls.Add(this.listOfflineLog);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(884, 590);
            this.tabControl1.TabIndex = 0;
            // 
            // listLog
            // 
            this.listLog.BackColor = System.Drawing.Color.LightGray;
            this.listLog.Controls.Add(this.checkBoxDebug);
            this.listLog.Controls.Add(this.checkBoxError);
            this.listLog.Controls.Add(this.checkBoxWarning);
            this.listLog.Controls.Add(this.checkBoxInfo);
            this.listLog.Controls.Add(this.checkBoxAll);
            this.listLog.Controls.Add(this.Filter_button);
            this.listLog.Controls.Add(this.Pause_button);
            this.listLog.Controls.Add(this.tBOnlineIDS);
            this.listLog.Controls.Add(this.listView1);
            this.listLog.Location = new System.Drawing.Point(4, 22);
            this.listLog.Name = "listLog";
            this.listLog.Padding = new System.Windows.Forms.Padding(3);
            this.listLog.Size = new System.Drawing.Size(876, 564);
            this.listLog.TabIndex = 0;
            this.listLog.Text = "OnLine Log";
            // 
            // listViewAlarm
            // 
            this.listViewAlarm.BackColor = System.Drawing.Color.LightGray;
            this.listViewAlarm.Controls.Add(this.listView2);
            this.listViewAlarm.Location = new System.Drawing.Point(4, 22);
            this.listViewAlarm.Name = "listViewAlarm";
            this.listViewAlarm.Padding = new System.Windows.Forms.Padding(3);
            this.listViewAlarm.Size = new System.Drawing.Size(876, 564);
            this.listViewAlarm.TabIndex = 1;
            this.listViewAlarm.Text = "AlarmView";
            // 
            // listOfflineLog
            // 
            this.listOfflineLog.Location = new System.Drawing.Point(4, 22);
            this.listOfflineLog.Name = "listOfflineLog";
            this.listOfflineLog.Padding = new System.Windows.Forms.Padding(3);
            this.listOfflineLog.Size = new System.Drawing.Size(907, 564);
            this.listOfflineLog.TabIndex = 2;
            this.listOfflineLog.Text = "OffLine Log";
            this.listOfflineLog.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Info;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Time,
            this.EventID,
            this.Message,
            this.User,
            this.Type});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(6, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(698, 552);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 66;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 142;
            // 
            // EventID
            // 
            this.EventID.Text = "EventID";
            this.EventID.Width = 73;
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 227;
            // 
            // User
            // 
            this.User.Text = "User";
            this.User.Width = 91;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 95;
            // 
            // tBOnlineIDS
            // 
            this.tBOnlineIDS.Location = new System.Drawing.Point(725, 18);
            this.tBOnlineIDS.Name = "tBOnlineIDS";
            this.tBOnlineIDS.Size = new System.Drawing.Size(100, 21);
            this.tBOnlineIDS.TabIndex = 1;
            // 
            // Pause_button
            // 
            this.Pause_button.Location = new System.Drawing.Point(725, 54);
            this.Pause_button.Name = "Pause_button";
            this.Pause_button.Size = new System.Drawing.Size(100, 34);
            this.Pause_button.TabIndex = 2;
            this.Pause_button.Text = "Pause";
            this.Pause_button.UseVisualStyleBackColor = true;
            // 
            // Filter_button
            // 
            this.Filter_button.Location = new System.Drawing.Point(725, 94);
            this.Filter_button.Name = "Filter_button";
            this.Filter_button.Size = new System.Drawing.Size(100, 35);
            this.Filter_button.TabIndex = 3;
            this.Filter_button.Text = "Filter";
            this.Filter_button.UseVisualStyleBackColor = true;
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(725, 145);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(42, 16);
            this.checkBoxAll.TabIndex = 4;
            this.checkBoxAll.Text = "All";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            // 
            // checkBoxInfo
            // 
            this.checkBoxInfo.AutoSize = true;
            this.checkBoxInfo.Location = new System.Drawing.Point(725, 167);
            this.checkBoxInfo.Name = "checkBoxInfo";
            this.checkBoxInfo.Size = new System.Drawing.Size(48, 16);
            this.checkBoxInfo.TabIndex = 5;
            this.checkBoxInfo.Text = "Info";
            this.checkBoxInfo.UseVisualStyleBackColor = true;
            // 
            // checkBoxWarning
            // 
            this.checkBoxWarning.AutoSize = true;
            this.checkBoxWarning.Location = new System.Drawing.Point(725, 189);
            this.checkBoxWarning.Name = "checkBoxWarning";
            this.checkBoxWarning.Size = new System.Drawing.Size(66, 16);
            this.checkBoxWarning.TabIndex = 6;
            this.checkBoxWarning.Text = "Warning";
            this.checkBoxWarning.UseVisualStyleBackColor = true;
            // 
            // checkBoxError
            // 
            this.checkBoxError.AutoSize = true;
            this.checkBoxError.Location = new System.Drawing.Point(725, 211);
            this.checkBoxError.Name = "checkBoxError";
            this.checkBoxError.Size = new System.Drawing.Size(54, 16);
            this.checkBoxError.TabIndex = 7;
            this.checkBoxError.Text = "Error";
            this.checkBoxError.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(725, 233);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(54, 16);
            this.checkBoxDebug.TabIndex = 8;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.SystemColors.Info;
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(6, 6);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(702, 492);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // PageLoggerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.tabControl1);
            this.Name = "PageLoggerInfo";
            this.Size = new System.Drawing.Size(887, 596);
            this.tabControl1.ResumeLayout(false);
            this.listLog.ResumeLayout(false);
            this.listLog.PerformLayout();
            this.listViewAlarm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage listLog;
        private System.Windows.Forms.TabPage listViewAlarm;
        private System.Windows.Forms.TabPage listOfflineLog;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader EventID;
        private System.Windows.Forms.ColumnHeader Message;
        private System.Windows.Forms.ColumnHeader User;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.CheckBox checkBoxError;
        private System.Windows.Forms.CheckBox checkBoxWarning;
        private System.Windows.Forms.CheckBox checkBoxInfo;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Button Filter_button;
        private System.Windows.Forms.Button Pause_button;
        private System.Windows.Forms.TextBox tBOnlineIDS;
        private System.Windows.Forms.ListView listView2;
    }
}
