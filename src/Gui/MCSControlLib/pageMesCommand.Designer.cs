namespace MCSControlLib
{
    partial class pageMesCommand
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.bnFoupMoveContinue = new System.Windows.Forms.Button();
            this.bnFoupMovePause = new System.Windows.Forms.Button();
            this.bnFoupMoveStop = new System.Windows.Forms.Button();
            this.bnFoupMove = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbLocDesp = new System.Windows.Forms.TextBox();
            this.tbLocName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLocType = new System.Windows.Forms.TextBox();
            this.tbLocPosition = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.linkLabelFoupRefresh = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbFoupLocation = new System.Windows.Forms.TextBox();
            this.tbFoupLocType = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbFoupStatus = new System.Windows.Forms.TextBox();
            this.tbFoupBarCode = new System.Windows.Forms.TextBox();
            this.tbFoupLot = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.linkLabelLocationRefresh = new System.Windows.Forms.LinkLabel();
            this.dataGridViewKeyPos = new System.Windows.Forms.DataGridView();
            this.dataGridViewFoup = new System.Windows.Forms.DataGridView();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeyPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFoup)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.textBox3);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Location = new System.Drawing.Point(189, 283);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(164, 134);
            this.groupBox5.TabIndex = 36;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Map Info";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(50, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(108, 20);
            this.comboBox1.TabIndex = 29;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(11, 49);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(147, 78);
            this.textBox3.TabIndex = 28;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 12);
            this.label19.TabIndex = 26;
            this.label19.Text = "Name:";
            // 
            // bnFoupMoveContinue
            // 
            this.bnFoupMoveContinue.Location = new System.Drawing.Point(217, 91);
            this.bnFoupMoveContinue.Name = "bnFoupMoveContinue";
            this.bnFoupMoveContinue.Size = new System.Drawing.Size(97, 23);
            this.bnFoupMoveContinue.TabIndex = 35;
            this.bnFoupMoveContinue.Text = "Continue";
            this.bnFoupMoveContinue.UseVisualStyleBackColor = true;
            this.bnFoupMoveContinue.Click += new System.EventHandler(this.bnFoupMoveContinue_Click);
            // 
            // bnFoupMovePause
            // 
            this.bnFoupMovePause.Location = new System.Drawing.Point(217, 62);
            this.bnFoupMovePause.Name = "bnFoupMovePause";
            this.bnFoupMovePause.Size = new System.Drawing.Size(97, 23);
            this.bnFoupMovePause.TabIndex = 34;
            this.bnFoupMovePause.Text = "Pause";
            this.bnFoupMovePause.UseVisualStyleBackColor = true;
            this.bnFoupMovePause.Click += new System.EventHandler(this.bnFoupMovePause_Click);
            // 
            // bnFoupMoveStop
            // 
            this.bnFoupMoveStop.Location = new System.Drawing.Point(203, 120);
            this.bnFoupMoveStop.Name = "bnFoupMoveStop";
            this.bnFoupMoveStop.Size = new System.Drawing.Size(130, 23);
            this.bnFoupMoveStop.TabIndex = 33;
            this.bnFoupMoveStop.Text = "Stop";
            this.bnFoupMoveStop.UseVisualStyleBackColor = true;
            this.bnFoupMoveStop.Click += new System.EventHandler(this.bnFoupMoveStop_Click);
            // 
            // bnFoupMove
            // 
            this.bnFoupMove.Location = new System.Drawing.Point(203, 33);
            this.bnFoupMove.Name = "bnFoupMove";
            this.bnFoupMove.Size = new System.Drawing.Size(130, 23);
            this.bnFoupMove.TabIndex = 32;
            this.bnFoupMove.Text = "Foup to Location";
            this.bnFoupMove.UseVisualStyleBackColor = true;
            this.bnFoupMove.Click += new System.EventHandler(this.bnFoupMove_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbLocDesp);
            this.groupBox4.Controls.Add(this.tbLocName);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tbLocType);
            this.groupBox4.Controls.Add(this.tbLocPosition);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(180, 183);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Location Selected";
            // 
            // tbLocDesp
            // 
            this.tbLocDesp.Location = new System.Drawing.Point(14, 99);
            this.tbLocDesp.Multiline = true;
            this.tbLocDesp.Name = "tbLocDesp";
            this.tbLocDesp.ReadOnly = true;
            this.tbLocDesp.Size = new System.Drawing.Size(147, 78);
            this.tbLocDesp.TabIndex = 25;
            // 
            // tbLocName
            // 
            this.tbLocName.Location = new System.Drawing.Point(77, 72);
            this.tbLocName.Name = "tbLocName";
            this.tbLocName.ReadOnly = true;
            this.tbLocName.Size = new System.Drawing.Size(84, 21);
            this.tbLocName.TabIndex = 23;
            this.tbLocName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "Name:";
            // 
            // tbLocType
            // 
            this.tbLocType.Location = new System.Drawing.Point(77, 45);
            this.tbLocType.Name = "tbLocType";
            this.tbLocType.ReadOnly = true;
            this.tbLocType.Size = new System.Drawing.Size(84, 21);
            this.tbLocType.TabIndex = 21;
            this.tbLocType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLocPosition
            // 
            this.tbLocPosition.Location = new System.Drawing.Point(77, 20);
            this.tbLocPosition.Name = "tbLocPosition";
            this.tbLocPosition.ReadOnly = true;
            this.tbLocPosition.Size = new System.Drawing.Size(84, 21);
            this.tbLocPosition.TabIndex = 19;
            this.tbLocPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "Type:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "BarCode:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(357, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 12);
            this.label15.TabIndex = 30;
            this.label15.Text = "Locations in MCS:";
            // 
            // linkLabelFoupRefresh
            // 
            this.linkLabelFoupRefresh.AutoSize = true;
            this.linkLabelFoupRefresh.Location = new System.Drawing.Point(894, 3);
            this.linkLabelFoupRefresh.Name = "linkLabelFoupRefresh";
            this.linkLabelFoupRefresh.Size = new System.Drawing.Size(47, 12);
            this.linkLabelFoupRefresh.TabIndex = 28;
            this.linkLabelFoupRefresh.TabStop = true;
            this.linkLabelFoupRefresh.Text = "Refresh";
            this.linkLabelFoupRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFoupRefresh_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.tbFoupLocation);
            this.groupBox3.Controls.Add(this.tbFoupLocType);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.tbFoupStatus);
            this.groupBox3.Controls.Add(this.tbFoupBarCode);
            this.groupBox3.Controls.Add(this.tbFoupLot);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(3, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(180, 225);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Foup Selected";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 141);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(131, 12);
            this.label18.TabIndex = 23;
            this.label18.Text = "Real Time Motion Info";
            // 
            // tbFoupLocation
            // 
            this.tbFoupLocation.Location = new System.Drawing.Point(77, 193);
            this.tbFoupLocation.Name = "tbFoupLocation";
            this.tbFoupLocation.ReadOnly = true;
            this.tbFoupLocation.Size = new System.Drawing.Size(84, 21);
            this.tbFoupLocation.TabIndex = 22;
            this.tbFoupLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbFoupLocType
            // 
            this.tbFoupLocType.Location = new System.Drawing.Point(77, 169);
            this.tbFoupLocType.Name = "tbFoupLocType";
            this.tbFoupLocType.ReadOnly = true;
            this.tbFoupLocType.Size = new System.Drawing.Size(84, 21);
            this.tbFoupLocType.TabIndex = 21;
            this.tbFoupLocType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 196);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 12);
            this.label17.TabIndex = 20;
            this.label17.Text = "Position:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 172);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "Carrier:";
            // 
            // tbFoupStatus
            // 
            this.tbFoupStatus.Location = new System.Drawing.Point(77, 67);
            this.tbFoupStatus.Name = "tbFoupStatus";
            this.tbFoupStatus.ReadOnly = true;
            this.tbFoupStatus.Size = new System.Drawing.Size(84, 21);
            this.tbFoupStatus.TabIndex = 18;
            this.tbFoupStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbFoupBarCode
            // 
            this.tbFoupBarCode.Location = new System.Drawing.Point(77, 43);
            this.tbFoupBarCode.Name = "tbFoupBarCode";
            this.tbFoupBarCode.ReadOnly = true;
            this.tbFoupBarCode.Size = new System.Drawing.Size(84, 21);
            this.tbFoupBarCode.TabIndex = 17;
            this.tbFoupBarCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbFoupLot
            // 
            this.tbFoupLot.Location = new System.Drawing.Point(77, 19);
            this.tbFoupLot.Name = "tbFoupLot";
            this.tbFoupLot.ReadOnly = true;
            this.tbFoupLot.Size = new System.Drawing.Size(84, 21);
            this.tbFoupLot.TabIndex = 16;
            this.tbFoupLot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 16;
            this.label14.Text = "BarCode:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 15;
            this.label13.Text = "Lot:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "Status:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(584, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "Foups in MCS:";
            // 
            // linkLabelLocationRefresh
            // 
            this.linkLabelLocationRefresh.AutoSize = true;
            this.linkLabelLocationRefresh.Location = new System.Drawing.Point(531, 3);
            this.linkLabelLocationRefresh.Name = "linkLabelLocationRefresh";
            this.linkLabelLocationRefresh.Size = new System.Drawing.Size(47, 12);
            this.linkLabelLocationRefresh.TabIndex = 37;
            this.linkLabelLocationRefresh.TabStop = true;
            this.linkLabelLocationRefresh.Text = "Refresh";
            this.linkLabelLocationRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLocationRefresh_LinkClicked);
            // 
            // dataGridViewKeyPos
            // 
            this.dataGridViewKeyPos.AllowUserToAddRows = false;
            this.dataGridViewKeyPos.AllowUserToDeleteRows = false;
            this.dataGridViewKeyPos.AllowUserToResizeRows = false;
            this.dataGridViewKeyPos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewKeyPos.BackgroundColor = System.Drawing.Color.MistyRose;
            this.dataGridViewKeyPos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewKeyPos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewKeyPos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewKeyPos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKeyPos.Location = new System.Drawing.Point(353, 18);
            this.dataGridViewKeyPos.MultiSelect = false;
            this.dataGridViewKeyPos.Name = "dataGridViewKeyPos";
            this.dataGridViewKeyPos.ReadOnly = true;
            this.dataGridViewKeyPos.RowHeadersVisible = false;
            this.dataGridViewKeyPos.RowTemplate.Height = 23;
            this.dataGridViewKeyPos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewKeyPos.Size = new System.Drawing.Size(225, 399);
            this.dataGridViewKeyPos.TabIndex = 38;
            this.dataGridViewKeyPos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewKeyPos_CellClick);
            // 
            // dataGridViewFoup
            // 
            this.dataGridViewFoup.AllowUserToAddRows = false;
            this.dataGridViewFoup.AllowUserToDeleteRows = false;
            this.dataGridViewFoup.AllowUserToResizeRows = false;
            this.dataGridViewFoup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewFoup.BackgroundColor = System.Drawing.Color.PaleTurquoise;
            this.dataGridViewFoup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewFoup.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFoup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewFoup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFoup.Location = new System.Drawing.Point(586, 18);
            this.dataGridViewFoup.MultiSelect = false;
            this.dataGridViewFoup.Name = "dataGridViewFoup";
            this.dataGridViewFoup.ReadOnly = true;
            this.dataGridViewFoup.RowHeadersVisible = false;
            this.dataGridViewFoup.RowTemplate.Height = 23;
            this.dataGridViewFoup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFoup.Size = new System.Drawing.Size(355, 399);
            this.dataGridViewFoup.TabIndex = 39;
            this.dataGridViewFoup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFoup_CellClick);
            // 
            // pageMesCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewFoup);
            this.Controls.Add(this.dataGridViewKeyPos);
            this.Controls.Add(this.linkLabelLocationRefresh);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.bnFoupMoveContinue);
            this.Controls.Add(this.bnFoupMovePause);
            this.Controls.Add(this.bnFoupMoveStop);
            this.Controls.Add(this.bnFoupMove);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.linkLabelFoupRefresh);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label12);
            this.Name = "pageMesCommand";
            this.Size = new System.Drawing.Size(958, 430);
            this.Load += new System.EventHandler(this.pageMesCommand_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeyPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFoup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button bnFoupMoveContinue;
        private System.Windows.Forms.Button bnFoupMovePause;
        private System.Windows.Forms.Button bnFoupMoveStop;
        private System.Windows.Forms.Button bnFoupMove;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbLocDesp;
        private System.Windows.Forms.TextBox tbLocName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLocType;
        private System.Windows.Forms.TextBox tbLocPosition;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.LinkLabel linkLabelFoupRefresh;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbFoupLocation;
        private System.Windows.Forms.TextBox tbFoupLocType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbFoupStatus;
        private System.Windows.Forms.TextBox tbFoupBarCode;
        private System.Windows.Forms.TextBox tbFoupLot;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel linkLabelLocationRefresh;
        private System.Windows.Forms.DataGridView dataGridViewKeyPos;
        private System.Windows.Forms.DataGridView dataGridViewFoup;

    }
}
