namespace RailDraw
{
    partial class Tools
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolImageList = new System.Windows.Forms.ImageList(this.components);
            this.eleBtn = new System.Windows.Forms.Button();
            this.others = new System.Windows.Forms.Button();
            this.contextMenuStripTool = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.LargeImageList = this.toolImageList;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(284, 262);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // toolImageList
            // 
            this.toolImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.toolImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.toolImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // eleBtn
            // 
            this.eleBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.eleBtn.Location = new System.Drawing.Point(0, 0);
            this.eleBtn.Name = "eleBtn";
            this.eleBtn.Size = new System.Drawing.Size(284, 23);
            this.eleBtn.TabIndex = 1;
            this.eleBtn.Text = "track";
            this.eleBtn.UseVisualStyleBackColor = true;
            this.eleBtn.Click += new System.EventHandler(this.eleBtn_Click);
            // 
            // others
            // 
            this.others.Dock = System.Windows.Forms.DockStyle.Top;
            this.others.Location = new System.Drawing.Point(0, 23);
            this.others.Name = "others";
            this.others.Size = new System.Drawing.Size(284, 23);
            this.others.TabIndex = 2;
            this.others.Text = "device";
            this.others.UseVisualStyleBackColor = true;
            this.others.Click += new System.EventHandler(this.others_Click);
            // 
            // contextMenuStripTool
            // 
            this.contextMenuStripTool.Name = "contextMenuStripTool";
            this.contextMenuStripTool.Size = new System.Drawing.Size(153, 26);
            // 
            // Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.others);
            this.Controls.Add(this.eleBtn);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tools";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tools_FormClosing);
            this.Load += new System.EventHandler(this.Tools_Load);
            this.Shown += new System.EventHandler(this.Tools_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button eleBtn;
        private System.Windows.Forms.Button others;
        private System.Windows.Forms.ImageList toolImageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTool;

    }
}