namespace MCSControlLib
{
    partial class pageAlarm
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
            this.bnClearAll = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tBEventID = new System.Windows.Forms.TextBox();
            this.bnClear = new System.Windows.Forms.Button();
            this.bnNext = new System.Windows.Forms.Button();
            this.bnEnd = new System.Windows.Forms.Button();
            this.bnFormer = new System.Windows.Forms.Button();
            this.bnFirstPage = new System.Windows.Forms.Button();
            this.listViewAlarm = new System.Windows.Forms.ListView();
            this.colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colfirstTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.collastTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colcount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alarms ";
            // 
            // bnClearAll
            // 
            this.bnClearAll.Location = new System.Drawing.Point(648, 126);
            this.bnClearAll.Name = "bnClearAll";
            this.bnClearAll.Size = new System.Drawing.Size(112, 31);
            this.bnClearAll.TabIndex = 31;
            this.bnClearAll.Text = "Clear All";
            this.bnClearAll.UseVisualStyleBackColor = true;
            this.bnClearAll.Click += new System.EventHandler(this.bnClearAll_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(648, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "EventID:";
            // 
            // tBEventID
            // 
            this.tBEventID.Location = new System.Drawing.Point(648, 47);
            this.tBEventID.Multiline = true;
            this.tBEventID.Name = "tBEventID";
            this.tBEventID.Size = new System.Drawing.Size(112, 27);
            this.tBEventID.TabIndex = 29;
            // 
            // bnClear
            // 
            this.bnClear.Location = new System.Drawing.Point(648, 89);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(112, 31);
            this.bnClear.TabIndex = 28;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = true;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // bnNext
            // 
            this.bnNext.Location = new System.Drawing.Point(132, 467);
            this.bnNext.Name = "bnNext";
            this.bnNext.Size = new System.Drawing.Size(44, 23);
            this.bnNext.TabIndex = 27;
            this.bnNext.Text = ">";
            this.bnNext.UseVisualStyleBackColor = true;
            this.bnNext.Click += new System.EventHandler(this.bnNext_Click);
            // 
            // bnEnd
            // 
            this.bnEnd.Location = new System.Drawing.Point(199, 467);
            this.bnEnd.Name = "bnEnd";
            this.bnEnd.Size = new System.Drawing.Size(44, 23);
            this.bnEnd.TabIndex = 26;
            this.bnEnd.Text = ">|";
            this.bnEnd.UseVisualStyleBackColor = true;
            this.bnEnd.Click += new System.EventHandler(this.bnEnd_Click);
            // 
            // bnFormer
            // 
            this.bnFormer.Location = new System.Drawing.Point(67, 467);
            this.bnFormer.Name = "bnFormer";
            this.bnFormer.Size = new System.Drawing.Size(44, 23);
            this.bnFormer.TabIndex = 25;
            this.bnFormer.Text = "<";
            this.bnFormer.UseVisualStyleBackColor = true;
            this.bnFormer.Click += new System.EventHandler(this.bnFormer_Click);
            // 
            // bnFirstPage
            // 
            this.bnFirstPage.Location = new System.Drawing.Point(3, 467);
            this.bnFirstPage.Name = "bnFirstPage";
            this.bnFirstPage.Size = new System.Drawing.Size(47, 23);
            this.bnFirstPage.TabIndex = 24;
            this.bnFirstPage.Text = "|<";
            this.bnFirstPage.UseVisualStyleBackColor = true;
            this.bnFirstPage.Click += new System.EventHandler(this.bnFirstPage_Click);
            // 
            // listViewAlarm
            // 
            this.listViewAlarm.BackColor = System.Drawing.SystemColors.Info;
            this.listViewAlarm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colEventID,
            this.colfirstTime,
            this.colMessage,
            this.collastTime,
            this.colcount});
            this.listViewAlarm.GridLines = true;
            this.listViewAlarm.Location = new System.Drawing.Point(3, 15);
            this.listViewAlarm.Name = "listViewAlarm";
            this.listViewAlarm.Size = new System.Drawing.Size(639, 444);
            this.listViewAlarm.TabIndex = 23;
            this.listViewAlarm.UseCompatibleStateImageBehavior = false;
            this.listViewAlarm.View = System.Windows.Forms.View.Details;
            // 
            // colIndex
            // 
            this.colIndex.Text = "Index";
            // 
            // colEventID
            // 
            this.colEventID.Text = "ID";
            // 
            // colfirstTime
            // 
            this.colfirstTime.Text = "FirstTime";
            this.colfirstTime.Width = 127;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 196;
            // 
            // collastTime
            // 
            this.collastTime.Text = "LastTime";
            this.collastTime.Width = 132;
            // 
            // colcount
            // 
            this.colcount.Text = "Count";
            // 
            // pageAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.bnClearAll);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tBEventID);
            this.Controls.Add(this.bnClear);
            this.Controls.Add(this.bnNext);
            this.Controls.Add(this.bnEnd);
            this.Controls.Add(this.bnFormer);
            this.Controls.Add(this.bnFirstPage);
            this.Controls.Add(this.listViewAlarm);
            this.Controls.Add(this.label1);
            this.Name = "pageAlarm";
            this.Size = new System.Drawing.Size(774, 506);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnClearAll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tBEventID;
        private System.Windows.Forms.Button bnClear;
        private System.Windows.Forms.Button bnNext;
        private System.Windows.Forms.Button bnEnd;
        private System.Windows.Forms.Button bnFormer;
        private System.Windows.Forms.Button bnFirstPage;
        private System.Windows.Forms.ListView listViewAlarm;
        private System.Windows.Forms.ColumnHeader colIndex;
        private System.Windows.Forms.ColumnHeader colEventID;
        private System.Windows.Forms.ColumnHeader colfirstTime;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ColumnHeader collastTime;
        private System.Windows.Forms.ColumnHeader colcount;
    }
}
