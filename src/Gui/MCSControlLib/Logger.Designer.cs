namespace MCSControlLib
{
    partial class Logger
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.tBOnlineIDS = new System.Windows.Forms.TextBox();
            this.Filter_button = new System.Windows.Forms.Button();
            this.button_Pause = new System.Windows.Forms.Button();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.checkBoxError = new System.Windows.Forms.CheckBox();
            this.checkBoxWarning = new System.Windows.Forms.CheckBox();
            this.checkBoxInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.listLog = new System.Windows.Forms.ListView();
            this.column_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_Timer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_EventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_User = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonclearall = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.buttonHome = new System.Windows.Forms.Button();
            this.next_button = new System.Windows.Forms.Button();
            this.former_button = new System.Windows.Forms.Button();
            this.listViewAlarm = new System.Windows.Forms.ListView();
            this.column_Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_AlarmID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_LastTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_AlarmMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_FirstTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_Debug = new System.Windows.Forms.CheckBox();
            this.checkBox_Error = new System.Windows.Forms.CheckBox();
            this.checkBox_Warning = new System.Windows.Forms.CheckBox();
            this.checkBox_Info = new System.Windows.Forms.CheckBox();
            this.button_Filter = new System.Windows.Forms.Button();
            this.button_Statistic = new System.Windows.Forms.Button();
            this.button_Goto = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.bnEnd = new System.Windows.Forms.Button();
            this.bnPageDown = new System.Windows.Forms.Button();
            this.bnPageUp = new System.Windows.Forms.Button();
            this.bnHome = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listOfflineLog = new System.Windows.Forms.ListView();
            this.column_OfflineID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_OfflineTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_OfflineEventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_OfflineMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_OfflineUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_OfflineType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(847, 572);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tBOnlineIDS);
            this.tabPage1.Controls.Add(this.Filter_button);
            this.tabPage1.Controls.Add(this.button_Pause);
            this.tabPage1.Controls.Add(this.checkBoxDebug);
            this.tabPage1.Controls.Add(this.checkBoxError);
            this.tabPage1.Controls.Add(this.checkBoxWarning);
            this.tabPage1.Controls.Add(this.checkBoxInfo);
            this.tabPage1.Controls.Add(this.checkBoxAll);
            this.tabPage1.Controls.Add(this.listLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(839, 546);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Online Log";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(718, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "EventID:";
            // 
            // tBOnlineIDS
            // 
            this.tBOnlineIDS.Location = new System.Drawing.Point(720, 40);
            this.tBOnlineIDS.Multiline = true;
            this.tBOnlineIDS.Name = "tBOnlineIDS";
            this.tBOnlineIDS.Size = new System.Drawing.Size(100, 26);
            this.tBOnlineIDS.TabIndex = 11;
            // 
            // Filter_button
            // 
            this.Filter_button.Location = new System.Drawing.Point(720, 111);
            this.Filter_button.Name = "Filter_button";
            this.Filter_button.Size = new System.Drawing.Size(103, 33);
            this.Filter_button.TabIndex = 10;
            this.Filter_button.Text = "Filter";
            this.Filter_button.UseVisualStyleBackColor = true;
            // 
            // button_Pause
            // 
            this.button_Pause.Location = new System.Drawing.Point(720, 72);
            this.button_Pause.Name = "button_Pause";
            this.button_Pause.Size = new System.Drawing.Size(103, 33);
            this.button_Pause.TabIndex = 9;
            this.button_Pause.Text = "Pause";
            this.button_Pause.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(726, 238);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(54, 16);
            this.checkBoxDebug.TabIndex = 5;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // checkBoxError
            // 
            this.checkBoxError.AutoSize = true;
            this.checkBoxError.Location = new System.Drawing.Point(726, 216);
            this.checkBoxError.Name = "checkBoxError";
            this.checkBoxError.Size = new System.Drawing.Size(54, 16);
            this.checkBoxError.TabIndex = 4;
            this.checkBoxError.Text = "Error";
            this.checkBoxError.UseVisualStyleBackColor = true;
            // 
            // checkBoxWarning
            // 
            this.checkBoxWarning.AutoSize = true;
            this.checkBoxWarning.Location = new System.Drawing.Point(726, 194);
            this.checkBoxWarning.Name = "checkBoxWarning";
            this.checkBoxWarning.Size = new System.Drawing.Size(66, 16);
            this.checkBoxWarning.TabIndex = 3;
            this.checkBoxWarning.Text = "Warning";
            this.checkBoxWarning.UseVisualStyleBackColor = true;
            // 
            // checkBoxInfo
            // 
            this.checkBoxInfo.AutoSize = true;
            this.checkBoxInfo.Location = new System.Drawing.Point(726, 172);
            this.checkBoxInfo.Name = "checkBoxInfo";
            this.checkBoxInfo.Size = new System.Drawing.Size(48, 16);
            this.checkBoxInfo.TabIndex = 2;
            this.checkBoxInfo.Text = "Info";
            this.checkBoxInfo.UseVisualStyleBackColor = true;
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(726, 150);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(42, 16);
            this.checkBoxAll.TabIndex = 1;
            this.checkBoxAll.Text = "All";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            // 
            // listLog
            // 
            this.listLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listLog.BackColor = System.Drawing.SystemColors.Info;
            this.listLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_ID,
            this.column_Timer,
            this.column_EventID,
            this.column_Message,
            this.column_User,
            this.column_Type});
            this.listLog.GridLines = true;
            this.listLog.Location = new System.Drawing.Point(6, 6);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(694, 534);
            this.listLog.TabIndex = 0;
            this.listLog.UseCompatibleStateImageBehavior = false;
            this.listLog.View = System.Windows.Forms.View.Details;
            // 
            // column_ID
            // 
            this.column_ID.Text = "ID";
            this.column_ID.Width = 74;
            // 
            // column_Timer
            // 
            this.column_Timer.Text = "Time";
            this.column_Timer.Width = 132;
            // 
            // column_EventID
            // 
            this.column_EventID.Text = "EventID";
            this.column_EventID.Width = 88;
            // 
            // column_Message
            // 
            this.column_Message.Text = "Message";
            this.column_Message.Width = 200;
            // 
            // column_User
            // 
            this.column_User.Text = "User";
            this.column_User.Width = 97;
            // 
            // column_Type
            // 
            this.column_Type.Text = "Type";
            this.column_Type.Width = 98;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.buttonclearall);
            this.tabPage2.Controls.Add(this.button_clear);
            this.tabPage2.Controls.Add(this.textBox4);
            this.tabPage2.Controls.Add(this.buttonEnd);
            this.tabPage2.Controls.Add(this.buttonHome);
            this.tabPage2.Controls.Add(this.next_button);
            this.tabPage2.Controls.Add(this.former_button);
            this.tabPage2.Controls.Add(this.listViewAlarm);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(839, 546);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Alarm View";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(710, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "EventID:";
            // 
            // buttonclearall
            // 
            this.buttonclearall.Location = new System.Drawing.Point(712, 126);
            this.buttonclearall.Name = "buttonclearall";
            this.buttonclearall.Size = new System.Drawing.Size(116, 30);
            this.buttonclearall.TabIndex = 8;
            this.buttonclearall.Text = "Clear All";
            this.buttonclearall.UseVisualStyleBackColor = true;
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(712, 90);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(116, 30);
            this.button_clear.TabIndex = 7;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(710, 44);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(116, 28);
            this.textBox4.TabIndex = 6;
            // 
            // buttonEnd
            // 
            this.buttonEnd.Location = new System.Drawing.Point(259, 517);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(75, 23);
            this.buttonEnd.TabIndex = 5;
            this.buttonEnd.Text = ">|";
            this.buttonEnd.UseVisualStyleBackColor = true;
            // 
            // buttonHome
            // 
            this.buttonHome.Location = new System.Drawing.Point(16, 517);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(75, 23);
            this.buttonHome.TabIndex = 4;
            this.buttonHome.Text = "|<";
            this.buttonHome.UseVisualStyleBackColor = true;
            // 
            // next_button
            // 
            this.next_button.Location = new System.Drawing.Point(178, 518);
            this.next_button.Name = "next_button";
            this.next_button.Size = new System.Drawing.Size(75, 23);
            this.next_button.TabIndex = 2;
            this.next_button.Text = ">";
            this.next_button.UseVisualStyleBackColor = true;
            // 
            // former_button
            // 
            this.former_button.Location = new System.Drawing.Point(97, 517);
            this.former_button.Name = "former_button";
            this.former_button.Size = new System.Drawing.Size(75, 23);
            this.former_button.TabIndex = 1;
            this.former_button.Text = "<";
            this.former_button.UseVisualStyleBackColor = true;
            // 
            // listViewAlarm
            // 
            this.listViewAlarm.BackColor = System.Drawing.SystemColors.Info;
            this.listViewAlarm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_Index,
            this.column_AlarmID,
            this.column_LastTime,
            this.column_AlarmMessage,
            this.column_FirstTime,
            this.column_Count});
            this.listViewAlarm.FullRowSelect = true;
            this.listViewAlarm.GridLines = true;
            this.listViewAlarm.Location = new System.Drawing.Point(6, 6);
            this.listViewAlarm.Name = "listViewAlarm";
            this.listViewAlarm.Size = new System.Drawing.Size(690, 505);
            this.listViewAlarm.TabIndex = 0;
            this.listViewAlarm.UseCompatibleStateImageBehavior = false;
            this.listViewAlarm.View = System.Windows.Forms.View.Details;
            // 
            // column_Index
            // 
            this.column_Index.Text = "Index";
            this.column_Index.Width = 65;
            // 
            // column_AlarmID
            // 
            this.column_AlarmID.Text = "ID";
            this.column_AlarmID.Width = 62;
            // 
            // column_LastTime
            // 
            this.column_LastTime.Text = "LastTime";
            this.column_LastTime.Width = 123;
            // 
            // column_AlarmMessage
            // 
            this.column_AlarmMessage.Text = "Message";
            this.column_AlarmMessage.Width = 209;
            // 
            // column_FirstTime
            // 
            this.column_FirstTime.Text = "FirstTime";
            this.column_FirstTime.Width = 134;
            // 
            // column_Count
            // 
            this.column_Count.Text = "Count";
            this.column_Count.Width = 92;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.LightGray;
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.checkBox_Debug);
            this.tabPage3.Controls.Add(this.checkBox_Error);
            this.tabPage3.Controls.Add(this.checkBox_Warning);
            this.tabPage3.Controls.Add(this.checkBox_Info);
            this.tabPage3.Controls.Add(this.button_Filter);
            this.tabPage3.Controls.Add(this.button_Statistic);
            this.tabPage3.Controls.Add(this.button_Goto);
            this.tabPage3.Controls.Add(this.button_Search);
            this.tabPage3.Controls.Add(this.bnEnd);
            this.tabPage3.Controls.Add(this.bnPageDown);
            this.tabPage3.Controls.Add(this.bnPageUp);
            this.tabPage3.Controls.Add(this.bnHome);
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.dtpEnd);
            this.tabPage3.Controls.Add(this.dtpBegin);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.listOfflineLog);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(839, 546);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Offline Log";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "Page Number: 1/44";
            // 
            // checkBox_Debug
            // 
            this.checkBox_Debug.AutoSize = true;
            this.checkBox_Debug.Location = new System.Drawing.Point(720, 232);
            this.checkBox_Debug.Name = "checkBox_Debug";
            this.checkBox_Debug.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Debug.TabIndex = 22;
            this.checkBox_Debug.Text = "Debug";
            this.checkBox_Debug.UseVisualStyleBackColor = true;
            // 
            // checkBox_Error
            // 
            this.checkBox_Error.AutoSize = true;
            this.checkBox_Error.Location = new System.Drawing.Point(720, 210);
            this.checkBox_Error.Name = "checkBox_Error";
            this.checkBox_Error.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Error.TabIndex = 21;
            this.checkBox_Error.Text = "Error";
            this.checkBox_Error.UseVisualStyleBackColor = true;
            // 
            // checkBox_Warning
            // 
            this.checkBox_Warning.AutoSize = true;
            this.checkBox_Warning.Location = new System.Drawing.Point(720, 188);
            this.checkBox_Warning.Name = "checkBox_Warning";
            this.checkBox_Warning.Size = new System.Drawing.Size(66, 16);
            this.checkBox_Warning.TabIndex = 20;
            this.checkBox_Warning.Text = "Warning";
            this.checkBox_Warning.UseVisualStyleBackColor = true;
            // 
            // checkBox_Info
            // 
            this.checkBox_Info.AutoSize = true;
            this.checkBox_Info.Location = new System.Drawing.Point(720, 166);
            this.checkBox_Info.Name = "checkBox_Info";
            this.checkBox_Info.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Info.TabIndex = 19;
            this.checkBox_Info.Text = "Info";
            this.checkBox_Info.UseVisualStyleBackColor = true;
            // 
            // button_Filter
            // 
            this.button_Filter.Location = new System.Drawing.Point(720, 116);
            this.button_Filter.Name = "button_Filter";
            this.button_Filter.Size = new System.Drawing.Size(91, 32);
            this.button_Filter.TabIndex = 18;
            this.button_Filter.Text = "Filter";
            this.button_Filter.UseVisualStyleBackColor = true;
            // 
            // button_Statistic
            // 
            this.button_Statistic.Location = new System.Drawing.Point(720, 78);
            this.button_Statistic.Name = "button_Statistic";
            this.button_Statistic.Size = new System.Drawing.Size(91, 32);
            this.button_Statistic.TabIndex = 17;
            this.button_Statistic.Text = "Statistic";
            this.button_Statistic.UseVisualStyleBackColor = true;
            // 
            // button_Goto
            // 
            this.button_Goto.Location = new System.Drawing.Point(540, 38);
            this.button_Goto.Name = "button_Goto";
            this.button_Goto.Size = new System.Drawing.Size(75, 23);
            this.button_Goto.TabIndex = 16;
            this.button_Goto.Text = "Go to";
            this.button_Goto.UseVisualStyleBackColor = true;
            // 
            // button_Search
            // 
            this.button_Search.BackColor = System.Drawing.Color.Transparent;
            this.button_Search.Location = new System.Drawing.Point(540, 11);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(75, 23);
            this.button_Search.TabIndex = 15;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = false;
            // 
            // bnEnd
            // 
            this.bnEnd.Location = new System.Drawing.Point(206, 38);
            this.bnEnd.Name = "bnEnd";
            this.bnEnd.Size = new System.Drawing.Size(36, 23);
            this.bnEnd.TabIndex = 14;
            this.bnEnd.Text = ">|";
            this.bnEnd.UseVisualStyleBackColor = true;
            // 
            // bnPageDown
            // 
            this.bnPageDown.Location = new System.Drawing.Point(248, 38);
            this.bnPageDown.Name = "bnPageDown";
            this.bnPageDown.Size = new System.Drawing.Size(36, 23);
            this.bnPageDown.TabIndex = 13;
            this.bnPageDown.Text = ">";
            this.bnPageDown.UseVisualStyleBackColor = true;
            // 
            // bnPageUp
            // 
            this.bnPageUp.Location = new System.Drawing.Point(290, 38);
            this.bnPageUp.Name = "bnPageUp";
            this.bnPageUp.Size = new System.Drawing.Size(35, 23);
            this.bnPageUp.TabIndex = 12;
            this.bnPageUp.Text = "<";
            this.bnPageUp.UseVisualStyleBackColor = true;
            // 
            // bnHome
            // 
            this.bnHome.Location = new System.Drawing.Point(331, 38);
            this.bnHome.Name = "bnHome";
            this.bnHome.Size = new System.Drawing.Size(36, 23);
            this.bnHome.TabIndex = 11;
            this.bnHome.Text = "|<";
            this.bnHome.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(429, 11);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(105, 21);
            this.textBox3.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(429, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(105, 21);
            this.textBox1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(394, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Key:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(227, 11);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(140, 21);
            this.dtpEnd.TabIndex = 4;
            // 
            // dtpBegin
            // 
            this.dtpBegin.Location = new System.Drawing.Point(41, 11);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(151, 21);
            this.dtpBegin.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "to:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "From:";
            // 
            // listOfflineLog
            // 
            this.listOfflineLog.BackColor = System.Drawing.SystemColors.Info;
            this.listOfflineLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_OfflineID,
            this.column_OfflineTime,
            this.column_OfflineEventID,
            this.column_OfflineMessage,
            this.column_OfflineUser,
            this.column_OfflineType});
            this.listOfflineLog.FullRowSelect = true;
            this.listOfflineLog.GridLines = true;
            this.listOfflineLog.Location = new System.Drawing.Point(5, 65);
            this.listOfflineLog.Name = "listOfflineLog";
            this.listOfflineLog.Size = new System.Drawing.Size(691, 476);
            this.listOfflineLog.TabIndex = 0;
            this.listOfflineLog.UseCompatibleStateImageBehavior = false;
            this.listOfflineLog.View = System.Windows.Forms.View.Details;
            // 
            // column_OfflineID
            // 
            this.column_OfflineID.Text = "ID";
            // 
            // column_OfflineTime
            // 
            this.column_OfflineTime.Text = "Time";
            this.column_OfflineTime.Width = 110;
            // 
            // column_OfflineEventID
            // 
            this.column_OfflineEventID.Text = "EventID";
            // 
            // column_OfflineMessage
            // 
            this.column_OfflineMessage.Text = "Message";
            this.column_OfflineMessage.Width = 245;
            // 
            // column_OfflineUser
            // 
            this.column_OfflineUser.Text = "User";
            this.column_OfflineUser.Width = 122;
            // 
            // column_OfflineType
            // 
            this.column_OfflineType.Text = "Type";
            this.column_OfflineType.Width = 90;
            // 
            // Logger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(845, 578);
            this.Controls.Add(this.tabControl1);
            this.Name = "Logger";
            this.Text = "Logger";
            //this.Load += new System.EventHandler(this.Logger_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listLog;
        private System.Windows.Forms.ColumnHeader column_ID;
        private System.Windows.Forms.ColumnHeader column_Timer;
        private System.Windows.Forms.ColumnHeader column_EventID;
        private System.Windows.Forms.ColumnHeader column_Message;
        private System.Windows.Forms.ColumnHeader column_User;
        private System.Windows.Forms.ColumnHeader column_Type;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.CheckBox checkBoxError;
        private System.Windows.Forms.CheckBox checkBoxWarning;
        private System.Windows.Forms.CheckBox checkBoxInfo;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.ListView listViewAlarm;
        private System.Windows.Forms.ColumnHeader column_Index;
        private System.Windows.Forms.ColumnHeader column_AlarmID;
        private System.Windows.Forms.ColumnHeader column_LastTime;
        private System.Windows.Forms.ColumnHeader column_AlarmMessage;
        private System.Windows.Forms.ColumnHeader column_FirstTime;
        private System.Windows.Forms.ColumnHeader column_Count;
        private System.Windows.Forms.Button next_button;
        private System.Windows.Forms.Button former_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listOfflineLog;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ColumnHeader column_OfflineID;
        private System.Windows.Forms.ColumnHeader column_OfflineTime;
        private System.Windows.Forms.ColumnHeader column_OfflineEventID;
        private System.Windows.Forms.ColumnHeader column_OfflineMessage;
        private System.Windows.Forms.ColumnHeader column_OfflineUser;
        private System.Windows.Forms.ColumnHeader column_OfflineType;
        private System.Windows.Forms.Button bnEnd;
        private System.Windows.Forms.Button bnPageDown;
        private System.Windows.Forms.Button bnPageUp;
        private System.Windows.Forms.Button bnHome;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button Filter_button;
        private System.Windows.Forms.Button button_Pause;
        private System.Windows.Forms.Button buttonclearall;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox checkBox_Debug;
        private System.Windows.Forms.CheckBox checkBox_Error;
        private System.Windows.Forms.CheckBox checkBox_Warning;
        private System.Windows.Forms.CheckBox checkBox_Info;
        private System.Windows.Forms.Button button_Filter;
        private System.Windows.Forms.Button button_Statistic;
        private System.Windows.Forms.Button button_Goto;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tBOnlineIDS;
    }
}