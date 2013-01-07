namespace LogWinTestCS
{
    partial class Form1
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
            logClient.Disconnect();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listLog = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.User = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bnNow = new System.Windows.Forms.Button();
            this.bnQuickSend = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.bnSendCus = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bnAuto = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.checkBoxError = new System.Windows.Forms.CheckBox();
            this.checkBoxWarning = new System.Windows.Forms.CheckBox();
            this.checkBoxInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tBOnlineIDS = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bnClear = new System.Windows.Forms.Button();
            this.bnAlmLast = new System.Windows.Forms.Button();
            this.bnAlmRT = new System.Windows.Forms.Button();
            this.bnAlmNext = new System.Windows.Forms.Button();
            this.bnAlmPrev = new System.Windows.Forms.Button();
            this.listViewAlarm = new System.Windows.Forms.ListView();
            this.colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.collastTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colfirstTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colcount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.tBTypes = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tBKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bnGetOffline = new System.Windows.Forms.Button();
            this.bnHome = new System.Windows.Forms.Button();
            this.bnEnd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tBQueryIDS = new System.Windows.Forms.TextBox();
            this.bnPageUp = new System.Windows.Forms.Button();
            this.bnPageDown = new System.Windows.Forms.Button();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.listOfflineLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbOnLineType = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listLog
            // 
            this.listLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Time,
            this.EventID,
            this.Message,
            this.User,
            this.Type});
            this.listLog.Location = new System.Drawing.Point(0, 24);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(793, 273);
            this.listLog.TabIndex = 0;
            this.listLog.UseCompatibleStateImageBehavior = false;
            this.listLog.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 150;
            // 
            // EventID
            // 
            this.EventID.Text = "EventID";
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 153;
            // 
            // User
            // 
            this.User.Text = "User";
            // 
            // Type
            // 
            this.Type.Text = "Type";
            // 
            // bnNow
            // 
            this.bnNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnNow.Location = new System.Drawing.Point(459, 302);
            this.bnNow.Name = "bnNow";
            this.bnNow.Size = new System.Drawing.Size(94, 24);
            this.bnNow.TabIndex = 3;
            this.bnNow.Text = "Last";
            this.bnNow.UseVisualStyleBackColor = true;
            this.bnNow.Click += new System.EventHandler(this.bnNow_Click);
            // 
            // bnQuickSend
            // 
            this.bnQuickSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnQuickSend.Location = new System.Drawing.Point(12, 388);
            this.bnQuickSend.Name = "bnQuickSend";
            this.bnQuickSend.Size = new System.Drawing.Size(75, 59);
            this.bnQuickSend.TabIndex = 4;
            this.bnQuickSend.Text = "Send Default  test Log";
            this.bnQuickSend.UseVisualStyleBackColor = true;
            this.bnQuickSend.Click += new System.EventHandler(this.bnQuickSend_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(93, 388);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(626, 59);
            this.txtLog.TabIndex = 5;
            // 
            // bnSendCus
            // 
            this.bnSendCus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnSendCus.Location = new System.Drawing.Point(725, 385);
            this.bnSendCus.Name = "bnSendCus";
            this.bnSendCus.Size = new System.Drawing.Size(75, 62);
            this.bnSendCus.TabIndex = 6;
            this.bnSendCus.Text = "Send user log";
            this.bnSendCus.UseVisualStyleBackColor = true;
            this.bnSendCus.Click += new System.EventHandler(this.bnSendCus_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(9, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Log Send function for test:";
            // 
            // bnAuto
            // 
            this.bnAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnAuto.Location = new System.Drawing.Point(359, 302);
            this.bnAuto.Name = "bnAuto";
            this.bnAuto.Size = new System.Drawing.Size(94, 24);
            this.bnAuto.TabIndex = 8;
            this.bnAuto.Text = "RealTimeView";
            this.bnAuto.UseVisualStyleBackColor = true;
            this.bnAuto.Click += new System.EventHandler(this.bnAuto_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(804, 356);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxDebug);
            this.tabPage1.Controls.Add(this.checkBoxError);
            this.tabPage1.Controls.Add(this.checkBoxWarning);
            this.tabPage1.Controls.Add(this.checkBoxInfo);
            this.tabPage1.Controls.Add(this.checkBoxAll);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tBOnlineIDS);
            this.tabPage1.Controls.Add(this.listLog);
            this.tabPage1.Controls.Add(this.bnNow);
            this.tabPage1.Controls.Add(this.bnAuto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(796, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OnLine Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(265, 6);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(54, 16);
            this.checkBoxDebug.TabIndex = 24;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.CheckedChanged += new System.EventHandler(this.checkBoxDebug_CheckedChanged);
            // 
            // checkBoxError
            // 
            this.checkBoxError.AutoSize = true;
            this.checkBoxError.Location = new System.Drawing.Point(205, 6);
            this.checkBoxError.Name = "checkBoxError";
            this.checkBoxError.Size = new System.Drawing.Size(54, 16);
            this.checkBoxError.TabIndex = 23;
            this.checkBoxError.Text = "Error";
            this.checkBoxError.UseVisualStyleBackColor = true;
            this.checkBoxError.CheckedChanged += new System.EventHandler(this.checkBoxError_CheckedChanged);
            // 
            // checkBoxWarning
            // 
            this.checkBoxWarning.AutoSize = true;
            this.checkBoxWarning.Location = new System.Drawing.Point(133, 6);
            this.checkBoxWarning.Name = "checkBoxWarning";
            this.checkBoxWarning.Size = new System.Drawing.Size(66, 16);
            this.checkBoxWarning.TabIndex = 22;
            this.checkBoxWarning.Text = "Warning";
            this.checkBoxWarning.UseVisualStyleBackColor = true;
            this.checkBoxWarning.CheckedChanged += new System.EventHandler(this.checkBoxWarning_CheckedChanged);
            // 
            // checkBoxInfo
            // 
            this.checkBoxInfo.AutoSize = true;
            this.checkBoxInfo.Location = new System.Drawing.Point(79, 5);
            this.checkBoxInfo.Name = "checkBoxInfo";
            this.checkBoxInfo.Size = new System.Drawing.Size(48, 16);
            this.checkBoxInfo.TabIndex = 21;
            this.checkBoxInfo.Text = "Info";
            this.checkBoxInfo.UseVisualStyleBackColor = true;
            this.checkBoxInfo.CheckedChanged += new System.EventHandler(this.checkBoxInfo_CheckedChanged);
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(31, 5);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(42, 16);
            this.checkBoxAll.TabIndex = 20;
            this.checkBoxAll.Text = "All";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(558, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "EventID:";
            // 
            // tBOnlineIDS
            // 
            this.tBOnlineIDS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tBOnlineIDS.Location = new System.Drawing.Point(617, 1);
            this.tBOnlineIDS.Name = "tBOnlineIDS";
            this.tBOnlineIDS.Size = new System.Drawing.Size(83, 21);
            this.tBOnlineIDS.TabIndex = 18;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.bnClear);
            this.tabPage2.Controls.Add(this.bnAlmLast);
            this.tabPage2.Controls.Add(this.bnAlmRT);
            this.tabPage2.Controls.Add(this.bnAlmNext);
            this.tabPage2.Controls.Add(this.bnAlmPrev);
            this.tabPage2.Controls.Add(this.listViewAlarm);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(796, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Alarm View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bnClear
            // 
            this.bnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnClear.Location = new System.Drawing.Point(262, 302);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(94, 24);
            this.bnClear.TabIndex = 13;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = true;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // bnAlmLast
            // 
            this.bnAlmLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnAlmLast.Location = new System.Drawing.Point(462, 302);
            this.bnAlmLast.Name = "bnAlmLast";
            this.bnAlmLast.Size = new System.Drawing.Size(94, 24);
            this.bnAlmLast.TabIndex = 11;
            this.bnAlmLast.Text = "Last";
            this.bnAlmLast.UseVisualStyleBackColor = true;
            this.bnAlmLast.Click += new System.EventHandler(this.bnAlmLast_Click);
            // 
            // bnAlmRT
            // 
            this.bnAlmRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnAlmRT.Location = new System.Drawing.Point(362, 302);
            this.bnAlmRT.Name = "bnAlmRT";
            this.bnAlmRT.Size = new System.Drawing.Size(94, 24);
            this.bnAlmRT.TabIndex = 12;
            this.bnAlmRT.Text = "RealTimeView";
            this.bnAlmRT.UseVisualStyleBackColor = true;
            this.bnAlmRT.Click += new System.EventHandler(this.bnAlmRT_Click);
            // 
            // bnAlmNext
            // 
            this.bnAlmNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnAlmNext.Location = new System.Drawing.Point(103, 302);
            this.bnAlmNext.Name = "bnAlmNext";
            this.bnAlmNext.Size = new System.Drawing.Size(94, 24);
            this.bnAlmNext.TabIndex = 10;
            this.bnAlmNext.Text = ">";
            this.bnAlmNext.UseVisualStyleBackColor = true;
            // 
            // bnAlmPrev
            // 
            this.bnAlmPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnAlmPrev.Location = new System.Drawing.Point(3, 302);
            this.bnAlmPrev.Name = "bnAlmPrev";
            this.bnAlmPrev.Size = new System.Drawing.Size(94, 24);
            this.bnAlmPrev.TabIndex = 9;
            this.bnAlmPrev.Text = "<";
            this.bnAlmPrev.UseVisualStyleBackColor = true;
            //this.bnAlmPrev.Click += new System.EventHandler(this.bnAlmPrev_Click);
            // 
            // listViewAlarm
            // 
            this.listViewAlarm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colEventID,
            this.collastTime,
            this.colMessage,
            this.colfirstTime,
            this.colcount});
            this.listViewAlarm.FullRowSelect = true;
            this.listViewAlarm.GridLines = true;
            this.listViewAlarm.HideSelection = false;
            this.listViewAlarm.Location = new System.Drawing.Point(7, 6);
            this.listViewAlarm.Name = "listViewAlarm";
            this.listViewAlarm.Size = new System.Drawing.Size(783, 279);
            this.listViewAlarm.TabIndex = 0;
            this.listViewAlarm.UseCompatibleStateImageBehavior = false;
            this.listViewAlarm.View = System.Windows.Forms.View.Details;
            // 
            // colIndex
            // 
            this.colIndex.Text = "Index";
            this.colIndex.Width = 50;
            // 
            // colEventID
            // 
            this.colEventID.Text = "ID";
            this.colEventID.Width = 42;
            // 
            // collastTime
            // 
            this.collastTime.Text = "Last Time";
            this.collastTime.Width = 92;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 221;
            // 
            // colfirstTime
            // 
            this.colfirstTime.Text = "First Time";
            this.colfirstTime.Width = 82;
            // 
            // colcount
            // 
            this.colcount.Text = "Count";
            this.colcount.Width = 47;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.tBTypes);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.tBKey);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.dtpEnd);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.bnGetOffline);
            this.tabPage3.Controls.Add(this.bnHome);
            this.tabPage3.Controls.Add(this.bnEnd);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.tBQueryIDS);
            this.tabPage3.Controls.Add(this.bnPageUp);
            this.tabPage3.Controls.Add(this.bnPageDown);
            this.tabPage3.Controls.Add(this.dtpBegin);
            this.tabPage3.Controls.Add(this.listOfflineLog);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(796, 330);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Offline Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(393, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 27;
            this.label9.Text = "Type";
            // 
            // tBTypes
            // 
            this.tBTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tBTypes.Location = new System.Drawing.Point(428, 3);
            this.tBTypes.Name = "tBTypes";
            this.tBTypes.Size = new System.Drawing.Size(83, 21);
            this.tBTypes.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(678, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "Key";
            // 
            // tBKey
            // 
            this.tBKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tBKey.Location = new System.Drawing.Point(707, 1);
            this.tBKey.Name = "tBKey";
            this.tBKey.Size = new System.Drawing.Size(83, 21);
            this.tBKey.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "From";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "";
            this.dtpEnd.Location = new System.Drawing.Point(218, 1);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(154, 21);
            this.dtpEnd.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(479, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "label3";
            // 
            // bnGetOffline
            // 
            this.bnGetOffline.Location = new System.Drawing.Point(715, 309);
            this.bnGetOffline.Name = "bnGetOffline";
            this.bnGetOffline.Size = new System.Drawing.Size(75, 21);
            this.bnGetOffline.TabIndex = 18;
            this.bnGetOffline.Text = "GetOffline";
            this.bnGetOffline.UseVisualStyleBackColor = true;
            this.bnGetOffline.Click += new System.EventHandler(this.bnGetOffline_Click);
            // 
            // bnHome
            // 
            this.bnHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnHome.Location = new System.Drawing.Point(30, 305);
            this.bnHome.Name = "bnHome";
            this.bnHome.Size = new System.Drawing.Size(54, 24);
            this.bnHome.TabIndex = 16;
            this.bnHome.Text = "|<";
            this.bnHome.UseVisualStyleBackColor = true;
            this.bnHome.Click += new System.EventHandler(this.bnHome_Click);
            // 
            // bnEnd
            // 
            this.bnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnEnd.Location = new System.Drawing.Point(221, 305);
            this.bnEnd.Name = "bnEnd";
            this.bnEnd.Size = new System.Drawing.Size(54, 24);
            this.bnEnd.TabIndex = 15;
            this.bnEnd.Text = ">|";
            this.bnEnd.UseVisualStyleBackColor = true;
            this.bnEnd.Click += new System.EventHandler(this.bnEnd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(536, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "EventID";
            // 
            // tBQueryIDS
            // 
            this.tBQueryIDS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tBQueryIDS.Location = new System.Drawing.Point(589, 2);
            this.tBQueryIDS.Name = "tBQueryIDS";
            this.tBQueryIDS.Size = new System.Drawing.Size(83, 21);
            this.tBQueryIDS.TabIndex = 13;
            // 
            // bnPageUp
            // 
            this.bnPageUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnPageUp.Location = new System.Drawing.Point(161, 305);
            this.bnPageUp.Name = "bnPageUp";
            this.bnPageUp.Size = new System.Drawing.Size(54, 24);
            this.bnPageUp.TabIndex = 12;
            this.bnPageUp.Text = ">";
            this.bnPageUp.UseVisualStyleBackColor = true;
            this.bnPageUp.Click += new System.EventHandler(this.bnPageUp_Click);
            // 
            // bnPageDown
            // 
            this.bnPageDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnPageDown.Location = new System.Drawing.Point(90, 305);
            this.bnPageDown.Name = "bnPageDown";
            this.bnPageDown.Size = new System.Drawing.Size(54, 24);
            this.bnPageDown.TabIndex = 11;
            this.bnPageDown.Text = "<";
            this.bnPageDown.UseVisualStyleBackColor = true;
            this.bnPageDown.Click += new System.EventHandler(this.bnPageDown_Click);
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "";
            this.dtpBegin.Location = new System.Drawing.Point(35, 2);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(154, 21);
            this.dtpBegin.TabIndex = 10;
            // 
            // listOfflineLog
            // 
            this.listOfflineLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listOfflineLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader5});
            this.listOfflineLog.Location = new System.Drawing.Point(3, 30);
            this.listOfflineLog.Name = "listOfflineLog";
            this.listOfflineLog.Size = new System.Drawing.Size(793, 273);
            this.listOfflineLog.TabIndex = 1;
            this.listOfflineLog.UseCompatibleStateImageBehavior = false;
            this.listOfflineLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "EventID";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Message";
            this.columnHeader4.Width = 153;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "User";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            // 
            // cmbOnLineType
            // 
            this.cmbOnLineType.FormattingEnabled = true;
            this.cmbOnLineType.Items.AddRange(new object[] {
            "Info",
            "Warrning",
            "Error",
            "Debug"});
            this.cmbOnLineType.Location = new System.Drawing.Point(195, 367);
            this.cmbOnLineType.Name = "cmbOnLineType";
            this.cmbOnLineType.Size = new System.Drawing.Size(91, 20);
            this.cmbOnLineType.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 458);
            this.Controls.Add(this.cmbOnLineType);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnSendCus);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.bnQuickSend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listLog;
        private System.Windows.Forms.Button bnNow;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Message;
        private System.Windows.Forms.Button bnQuickSend;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button bnSendCus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnAuto;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ColumnHeader EventID;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button bnAlmLast;
        private System.Windows.Forms.Button bnAlmRT;
        private System.Windows.Forms.Button bnAlmNext;
        private System.Windows.Forms.Button bnAlmPrev;
        private System.Windows.Forms.ListView listViewAlarm;
        private System.Windows.Forms.Button bnClear;
        private System.Windows.Forms.ColumnHeader colIndex;
        private System.Windows.Forms.ColumnHeader colEventID;
        private System.Windows.Forms.ColumnHeader collastTime;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ColumnHeader colfirstTime;
        private System.Windows.Forms.ColumnHeader colcount;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.ListView listOfflineLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button bnHome;
        private System.Windows.Forms.Button bnEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBQueryIDS;
        private System.Windows.Forms.Button bnPageUp;
        private System.Windows.Forms.Button bnPageDown;
        private System.Windows.Forms.Button bnGetOffline;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbOnLineType;
        private System.Windows.Forms.TextBox tBOnlineIDS;
        private System.Windows.Forms.ColumnHeader User;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.TextBox tBKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.CheckBox checkBoxError;
        private System.Windows.Forms.CheckBox checkBoxWarning;
        private System.Windows.Forms.CheckBox checkBoxInfo;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tBTypes;
        private System.Windows.Forms.Label label8;
    }
}

