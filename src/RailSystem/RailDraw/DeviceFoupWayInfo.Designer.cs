namespace RailDraw
{
    partial class DeviceFoupWayInfo
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
            this.dataGridViewDeviceWay = new System.Windows.Forms.DataGridView();
            this.FoupWayName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeviceWay)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDeviceWay
            // 
            this.dataGridViewDeviceWay.AllowUserToAddRows = false;
            this.dataGridViewDeviceWay.AllowUserToDeleteRows = false;
            this.dataGridViewDeviceWay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDeviceWay.Location = new System.Drawing.Point(1, 2);
            this.dataGridViewDeviceWay.MultiSelect = false;
            this.dataGridViewDeviceWay.Name = "dataGridViewDeviceWay";
            this.dataGridViewDeviceWay.ReadOnly = true;
            this.dataGridViewDeviceWay.RowTemplate.Height = 23;
            this.dataGridViewDeviceWay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDeviceWay.Size = new System.Drawing.Size(280, 150);
            this.dataGridViewDeviceWay.TabIndex = 0;
            // 
            // FoupWayName
            // 
            this.FoupWayName.Location = new System.Drawing.Point(94, 183);
            this.FoupWayName.Name = "FoupWayName";
            this.FoupWayName.Size = new System.Drawing.Size(100, 21);
            this.FoupWayName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "FoupWay";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(35, 227);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(161, 227);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // DeviceFoupWayInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FoupWayName);
            this.Controls.Add(this.dataGridViewDeviceWay);
            this.Name = "DeviceFoupWayInfo";
            this.Text = "DeviceFoupWayInfo";
            this.Load += new System.EventHandler(this.DeviceFoupWayInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeviceWay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDeviceWay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.TextBox FoupWayName;
    }
}