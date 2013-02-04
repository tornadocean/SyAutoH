using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using BaseRailElement;
using System.Diagnostics;

namespace RailDraw
{
    public class WorkRegUserDef : DockContent
    {
        private System.Windows.Forms.Panel panelUserDef;
        public System.Windows.Forms.PictureBox picBoxUserDef;
        private DrawDocOp docOp = new DrawDocOp();
        private Rectangle rcScope = new Rectangle() { X = 0, Y = 0, Width = 0, Height = 0 };
        private Rectangle rcScopeTemp = new Rectangle() { X = 0, Y = 0, Width = 0, Height = 0 };

        public WorkRegUserDef()
        {
            InitializeComponent();
        }
    
        private void InitializeComponent()
        {
            this.picBoxUserDef = new System.Windows.Forms.PictureBox();
            this.panelUserDef = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUserDef)).BeginInit();
            this.panelUserDef.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBoxUserDef
            // 
            this.picBoxUserDef.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxUserDef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxUserDef.Location = new System.Drawing.Point(75, 53);
            this.picBoxUserDef.Name = "picBoxUserDef";
            this.picBoxUserDef.Size = new System.Drawing.Size(100, 50);
            this.picBoxUserDef.TabIndex = 0;
            this.picBoxUserDef.TabStop = false;
            this.picBoxUserDef.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoxUserDef_Paint);
            this.picBoxUserDef.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBoxUserDef_MouseClick);
            this.picBoxUserDef.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxUserDef_MouseDown);
            this.picBoxUserDef.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBoxUserDef_MouseMove);
            this.picBoxUserDef.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxUserDef_MouseUp);
            this.picBoxUserDef.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.picBoxUserDef_PreviewKeyDown);
            // 
            // panelUserDef
            // 
            this.panelUserDef.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelUserDef.Controls.Add(this.picBoxUserDef);
            this.panelUserDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserDef.Location = new System.Drawing.Point(0, 0);
            this.panelUserDef.Name = "panelUserDef";
            this.panelUserDef.Size = new System.Drawing.Size(314, 282);
            this.panelUserDef.TabIndex = 1;
            // 
            // WorkRegUserDef
            // 
            this.ClientSize = new System.Drawing.Size(314, 282);
            this.Controls.Add(this.panelUserDef);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "WorkRegUserDef";
            this.Text = "UserDef";
            this.Activated += new System.EventHandler(this.WorkRegUserDef_Activated);
            this.Deactivate += new System.EventHandler(this.WorkRegUserDef_Deactivate);
            this.Load += new System.EventHandler(this.WorkRegUserDef_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUserDef)).EndInit();
            this.panelUserDef.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void WorkRegUserDef_Load(object sender, EventArgs e)
        {
            this.picBoxUserDef.Size = this.Size;
            this.picBoxUserDef.Location = new Point(0);
            this.KeyPreview = true;
        }

        private void picBoxUserDef_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            docOp.Draw(e.Graphics);
            g.ResetTransform();
            base.OnPaint(e);
        }

        private void picBoxUserDef_MouseDown(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockPanel.Parent;
            switch (e.Button)
            { 
                case MouseButtons.Left:
                    father.objectEvent.OnLButtonDown(e.Location);
                    father.objectEvent.MouseLDown = true;
                    if (father.objectEvent.MouseLMove)
                        this.Cursor = CommonFunction.CreatCursor("draw");
                    break;
                case MouseButtons.Right:
                    father.objectEvent.OnRButtonDown(e.Location);
                    father.objectEvent.MouseRDown = true;
                    break;
            }
        }

        private void picBoxUserDef_MouseMove(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            if (father.objectEvent.MouseLDown && !father.objectEvent.MouseLMove)
            {
                father.objectEvent.OnMouseMoveLeft(e.Location);
                rcScopeTemp.X = docOp.DownPoint.X < docOp.LastPoint.X ? docOp.DownPoint.X : docOp.LastPoint.X;
                rcScopeTemp.Y = docOp.DownPoint.Y < docOp.LastPoint.Y ? docOp.DownPoint.Y : docOp.LastPoint.Y;
                rcScopeTemp.Width = Math.Abs(docOp.DownPoint.X - docOp.LastPoint.X);
                rcScopeTemp.Height = Math.Abs(docOp.DownPoint.Y - docOp.LastPoint.Y);
                this.picBoxUserDef.Invalidate();
            }
            else if (father.objectEvent.MouseLDown && father.objectEvent.MouseLMove)
            {
                this.picBoxUserDef.Left += (e.X - BaseEvents.LastPoint.X);
                this.picBoxUserDef.Top += (e.Y - BaseEvents.LastPoint.Y);
            }
            else if (father.objectEvent.MouseRDown && (e.Location != BaseEvents.LastPoint))
            {
                father.objectEvent.MouseRMove = true;
                this.Cursor = CommonFunction.CreatCursor("draw");
                this.picBoxUserDef.Left += (e.X - BaseEvents.LastPoint.X);
                this.picBoxUserDef.Top += (e.Y - BaseEvents.LastPoint.Y);
                this.picBoxUserDef.Invalidate();
            }
        }

        private void picBoxUserDef_MouseUp(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (father.objectEvent.MouseLMove
                        && father.objectEvent.MouseLDown
                        && !father.objectEvent.MouseRDown)
                    {
                        father.objectEvent.MouseLMove = true;
                        this.Cursor = Cursors.Default;
                    }
                    if (father.objectEvent.MouseLDown)
                    {
                        father.objectEvent.MouseLDown = false;
                        rcScopeTemp.X = 0;
                        rcScopeTemp.Y = 0;
                        rcScopeTemp.Width = 0;
                        rcScopeTemp.Height = 0;
                    }
                    father.objectEvent.OnLButtonUp(e.Location);
                    break;
                case MouseButtons.Right:
                    father.objectEvent.MouseRDown = false;
                    father.objectEvent.MouseRMove = false;
                    this.Cursor = Cursors.Default;
                    break;
            }
            this.picBoxUserDef.Invalidate();
            this.picBoxUserDef.Focus();
        }

        private void picBoxUserDef_MouseClick(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            if (e.Button == MouseButtons.Left)
            {
                if (1 == docOp.SelectedDrawObjectList.Count)
                {
                    Mcs.RailSystem.Common.BaseRailEle baseEle = docOp.SelectedDrawObjectList[0];
                    Int16 i = Convert.ToInt16(docOp.DrawObjectList.IndexOf(docOp.SelectedDrawObjectList[0]));
                    if (i >= 0)
                        father.proRegion.SelectedElement(i);
                    else
                    {
                        i = Convert.ToInt16(docOp.ListAuxiliaryDraw.IndexOf(docOp.SelectedDrawObjectList[0]));
                        if (i >= 0)
                        {
                            docOp.SelectedDrawObjectList.Clear();
                            docOp.SelectedDrawObjectList.Add(docOp.ListAuxiliaryDraw[i]);
                        }
                    }
                }
            }
            this.Activate();
        }

        private void WorkRegUserDef_Activated(object sender, EventArgs e)
        {
            BaseRailElement.ObjectBaseEvents.DocumentOp = docOp;
            ((FatherWindow)this.DockPanel.Parent).activeWindowHandler = this.DockHandler;
            ((FatherWindow)this.DockPanel.Parent).proPage.propertyGrid1.SelectedObject = null;
            ((FatherWindow)this.DockPanel.Parent).proRegion.RefreshTreeView();
            ((FatherWindow)this.DockPanel.Parent).tools.ClearTool();
            ((FatherWindow)this.DockPanel.Parent).EnbleMenuItems(false);
        }

        private void WorkRegUserDef_Deactivate(object sender, EventArgs e)
        {
            ((FatherWindow)this.DockPanel.Parent).EnbleMenuItems(true);
        }

        public void DeleteElement()
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            for (Int16 i = 0; i < docOp.SelectedDrawObjectList.Count; )
            {
                Int16 num = Convert.ToInt16(docOp.DrawObjectList.IndexOf(docOp.SelectedDrawObjectList[0]));
                if (-1 == num)
                {
                    num = Convert.ToInt16(docOp.ListAuxiliaryDraw.IndexOf(docOp.SelectedDrawObjectList[0]));
                    if (-1 != num)
                    {
                        num += 10000;
                    }
                }
                docOp.Delete(num);

            }
            this.picBoxUserDef.Invalidate();
        }

        public void CreateUserDefinedEle(Point offset, Size workRegionSize, Image image)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            BaseRailElement.RailEleUserDef userDef = new RailEleUserDef();
            Mcs.RailSystem.Common.EleUserDef.UserDefType type = Mcs.RailSystem.Common.EleUserDef.UserDefType.picture;
            userDef.CreateEle(type, offset, workRegionSize, father.drawDocOp.DrawMultiFactor, "", image);
            docOp.ListAuxiliaryDraw.Add(userDef);
            this.picBoxUserDef.Invalidate();
        }

        public void SavePicFromUserDef(SaveFileDialog sFile)
        {
            string pathStart = Application.StartupPath;
            sFile.Filter = "picture (*.bmp)|*.bmp";
            sFile.InitialDirectory = pathStart.Substring(0, pathStart.IndexOf("bin\\")) + @"src\RailSystem\Mcs.RailSystem.Common\Resources\userdef";
            sFile.Title = "Save User defined";
            sFile.FileName = "";
            if (sFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string projectpath = sFile.FileName;
                    docOp.SelectedDrawObjectList.Clear();
                    this.picBoxUserDef.Invalidate();

                    Bitmap bit = new Bitmap(picBoxUserDef.Width, picBoxUserDef.Height);
                    picBoxUserDef.DrawToBitmap(bit, picBoxUserDef.ClientRectangle);
                    if (!(0 == rcScope.X
                    && 0 == rcScope.Y
                    && 0 == rcScope.Width
                    && 0 == rcScope.Height))
                    {
                        Bitmap bit1 = new Bitmap(rcScope.Width, rcScope.Height);
                        Graphics g = Graphics.FromImage(bit1);
                        g.DrawImage(bit, 0, 0, new Rectangle(rcScope.X, rcScope.Y, rcScope.Width, rcScope.Height), GraphicsUnit.Pixel);
                        bit1.Save(projectpath, System.Drawing.Imaging.ImageFormat.Bmp);
                        bit1.Dispose();
                    }
                    else
                    {
                        bit.Save(projectpath, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                    bit.Dispose();
                }
                catch
                {
                    MessageBox.Show("there is an error");
                }
            }
        }

        private void picBoxUserDef_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter
                && rcScopeTemp.Width != 0
                && rcScopeTemp.Height != 0)
            {
                rcScope.X = rcScopeTemp.X + 1;
                rcScope.Y = rcScopeTemp.Y + 1;
                rcScope.Width = rcScopeTemp.Width - 2;
                rcScope.Height = rcScopeTemp.Height - 2;
            }
        }


    }
}
