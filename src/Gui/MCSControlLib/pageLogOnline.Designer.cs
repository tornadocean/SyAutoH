namespace MCSControlLib
{
    partial class pageLogOnline
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
            this.label1 = new System.Windows.Forms.Label();
            this.listOnlineLog = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.User = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tb_EventID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bnPause = new System.Windows.Forms.Button();
            this.bn_Filter = new System.Windows.Forms.Button();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.checkBoxInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxWarning = new System.Windows.Forms.CheckBox();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.checkBoxError = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log Online";
            // 
            // listOnlineLog
            // 
            this.listOnlineLog.BackColor = System.Drawing.SystemColors.Info;
            this.listOnlineLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Time,
            this.EventID,
            this.Message,
            this.User,
            this.Type});
            this.listOnlineLog.GridLines = true;
            this.listOnlineLog.Location = new System.Drawing.Point(5, 25);
            this.listOnlineLog.Name = "listOnlineLog";
            this.listOnlineLog.Size = new System.Drawing.Size(671, 486);
            this.listOnlineLog.TabIndex = 1;
            this.listOnlineLog.UseCompatibleStateImageBehavior = false;
            this.listOnlineLog.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 162;
            // 
            // EventID
            // 
            this.EventID.Text = "EventID";
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 242;
            // 
            // User
            // 
            this.User.Text = "User";
            this.User.Width = 82;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            // 
            // tb_EventID
            // 
            this.tb_EventID.Location = new System.Drawing.Point(691, 59);
            this.tb_EventID.Multiline = true;
            this.tb_EventID.Name = "tb_EventID";
            this.tb_EventID.Size = new System.Drawing.Size(113, 31);
            this.tb_EventID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(689, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "EventID:";
            // 
            // bnPause
            // 
            this.bnPause.Location = new System.Drawing.Point(691, 96);
            this.bnPause.Name = "bnPause";
            this.bnPause.Size = new System.Drawing.Size(113, 36);
            this.bnPause.TabIndex = 4;
            this.bnPause.Text = "Pause";
            this.bnPause.UseVisualStyleBackColor = true;
            this.bnPause.Click += new System.EventHandler(this.bnPause_Click);
            // 
            // bn_Filter
            // 
            this.bn_Filter.Location = new System.Drawing.Point(691, 138);
            this.bn_Filter.Name = "bn_Filter";
            this.bn_Filter.Size = new System.Drawing.Size(113, 36);
            this.bn_Filter.TabIndex = 5;
            this.bn_Filter.Text = "Filter";
            this.bn_Filter.UseVisualStyleBackColor = true;
            this.bn_Filter.Click += new System.EventHandler(this.bn_Filter_Click);
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(691, 190);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(42, 16);
            this.checkBoxAll.TabIndex = 6;
            this.checkBoxAll.Text = "All";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            // 
            // checkBoxInfo
            // 
            this.checkBoxInfo.AutoSize = true;
            this.checkBoxInfo.Location = new System.Drawing.Point(691, 209);
            this.checkBoxInfo.Name = "checkBoxInfo";
            this.checkBoxInfo.Size = new System.Drawing.Size(48, 16);
            this.checkBoxInfo.TabIndex = 7;
            this.checkBoxInfo.Text = "Info";
            this.checkBoxInfo.UseVisualStyleBackColor = true;
            // 
            // checkBoxWarning
            // 
            this.checkBoxWarning.AutoSize = true;
            this.checkBoxWarning.Location = new System.Drawing.Point(691, 231);
            this.checkBoxWarning.Name = "checkBoxWarning";
            this.checkBoxWarning.Size = new System.Drawing.Size(66, 16);
            this.checkBoxWarning.TabIndex = 8;
            this.checkBoxWarning.Text = "Warning";
            this.checkBoxWarning.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(691, 253);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(54, 16);
            this.checkBoxDebug.TabIndex = 9;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // checkBoxError
            // 
            this.checkBoxError.AutoSize = true;
            this.checkBoxError.Location = new System.Drawing.Point(691, 275);
            this.checkBoxError.Name = "checkBoxError";
            this.checkBoxError.Size = new System.Drawing.Size(54, 16);
            this.checkBoxError.TabIndex = 10;
            this.checkBoxError.Text = "Error";
            this.checkBoxError.UseVisualStyleBackColor = true;
            // 
            // pageLogOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxError);
            this.Controls.Add(this.checkBoxDebug);
            this.Controls.Add(this.checkBoxWarning);
            this.Controls.Add(this.checkBoxInfo);
            this.Controls.Add(this.checkBoxAll);
            this.Controls.Add(this.bn_Filter);
            this.Controls.Add(this.bnPause);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_EventID);
            this.Controls.Add(this.listOnlineLog);
            this.Controls.Add(this.label1);
            this.Name = "pageLogOnline";
            this.Size = new System.Drawing.Size(823, 527);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listOnlineLog;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader EventID;
        private System.Windows.Forms.ColumnHeader Message;
        private System.Windows.Forms.ColumnHeader User;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.TextBox tb_EventID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bnPause;
        private System.Windows.Forms.Button bn_Filter;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.CheckBox checkBoxInfo;
        private System.Windows.Forms.CheckBox checkBoxWarning;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.CheckBox checkBoxError;
    }
}
