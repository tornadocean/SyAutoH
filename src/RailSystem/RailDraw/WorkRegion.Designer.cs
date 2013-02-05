namespace RailDraw
{
    partial class WorkRegion
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
            this.picBoxCanvas = new System.Windows.Forms.PictureBox();
            this.contextMenuStripWorkReg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelCanvas = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCanvas)).BeginInit();
            this.panelCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBoxCanvas
            // 
            this.picBoxCanvas.BackColor = System.Drawing.Color.White;
            this.picBoxCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxCanvas.Location = new System.Drawing.Point(54, 26);
            this.picBoxCanvas.Name = "picBoxCanvas";
            this.picBoxCanvas.Size = new System.Drawing.Size(100, 50);
            this.picBoxCanvas.TabIndex = 0;
            this.picBoxCanvas.TabStop = false;
            this.picBoxCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoxCanvas_Paint);
            this.picBoxCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBoxCanvas_MouseClick);
            this.picBoxCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxCanvas_MouseDown);
            this.picBoxCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBoxCanvas_MouseMove);
            this.picBoxCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxCanvas_MouseUp);
            this.picBoxCanvas.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.picBoxCanvas_PreviewKeyDown);
            // 
            // contextMenuStripWorkReg
            // 
            this.contextMenuStripWorkReg.Name = "contextMenuStripWorkReg";
            this.contextMenuStripWorkReg.Size = new System.Drawing.Size(61, 4);
            // 
            // panelCanvas
            // 
            this.panelCanvas.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelCanvas.Controls.Add(this.picBoxCanvas);
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCanvas.Location = new System.Drawing.Point(0, 0);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(284, 262);
            this.panelCanvas.TabIndex = 1;
            // 
            // WorkRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.panelCanvas);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkRegion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WorkRegion";
            this.Activated += new System.EventHandler(this.WorkRegion_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkRegion_FormClosing);
            this.Load += new System.EventHandler(this.WorkRegion_Load);
            this.Shown += new System.EventHandler(this.WorkRegion_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCanvas)).EndInit();
            this.panelCanvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picBoxCanvas;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripWorkReg;
        public System.Windows.Forms.Panel panelCanvas;
    }
}