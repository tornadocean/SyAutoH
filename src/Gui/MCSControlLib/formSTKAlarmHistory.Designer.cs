namespace MCSControlLib.Page.Stocker
{
    partial class formSTKAlarmHistory
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePickerAlarmST = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerAlarmET = new System.Windows.Forms.DateTimePicker();
            this.btnStkAlarmQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 92);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(341, 226);
            this.dataGridView1.TabIndex = 0;
            // 
            // dateTimePickerAlarmST
            // 
            this.dateTimePickerAlarmST.Location = new System.Drawing.Point(87, 12);
            this.dateTimePickerAlarmST.Name = "dateTimePickerAlarmST";
            this.dateTimePickerAlarmST.Size = new System.Drawing.Size(266, 21);
            this.dateTimePickerAlarmST.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Time:";
            // 
            // dateTimePickerAlarmET
            // 
            this.dateTimePickerAlarmET.Location = new System.Drawing.Point(87, 39);
            this.dateTimePickerAlarmET.Name = "dateTimePickerAlarmET";
            this.dateTimePickerAlarmET.Size = new System.Drawing.Size(266, 21);
            this.dateTimePickerAlarmET.TabIndex = 3;
            // 
            // btnStkAlarmQuery
            // 
            this.btnStkAlarmQuery.Location = new System.Drawing.Point(278, 66);
            this.btnStkAlarmQuery.Name = "btnStkAlarmQuery";
            this.btnStkAlarmQuery.Size = new System.Drawing.Size(75, 23);
            this.btnStkAlarmQuery.TabIndex = 5;
            this.btnStkAlarmQuery.Text = "Query";
            this.btnStkAlarmQuery.UseVisualStyleBackColor = true;
            this.btnStkAlarmQuery.Click += new System.EventHandler(this.btnStkAlarmQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Alarm List:";
            // 
            // formSTKAlarmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 330);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStkAlarmQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePickerAlarmET);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerAlarmST);
            this.Controls.Add(this.dataGridView1);
            this.Name = "formSTKAlarmHistory";
            this.Text = "formSTKAlarmHistory";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePickerAlarmST;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerAlarmET;
        private System.Windows.Forms.Button btnStkAlarmQuery;
        private System.Windows.Forms.Label label3;
    }
}