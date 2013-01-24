using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;

namespace RailDraw
{
    public partial class WorkRegion : DockContent
    {
        public bool winShown = false;

        public WorkRegion()
        {
            InitializeComponent();
        }

        private void WorkRegion_Load(object sender, EventArgs e)
        {
            this.picBoxCanvas.Size = this.Size;
            this.picBoxCanvas.Location = new Point(0);
            this.KeyPreview = true;
        }

        private void WorkRegion_Shown(object sender, EventArgs e)
        {
            winShown = true;
        }

        private void WorkRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            winShown = false;
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr h);

        public void ElementDraw_MouseUp(object sender, MouseEventArgs e)
        {
            this.picBoxCanvas.MouseUp -= new MouseEventHandler(this.ElementDraw_MouseUp);
            
            this.Cursor = System.Windows.Forms.Cursors.Default;
            Point pt = e.Location;
            Rectangle rc = this.picBoxCanvas.ClientRectangle;
            if (((FatherWindow)this.ParentForm).tools.PicLine && rc.Contains(pt))
            {
                ((FatherWindow)this.ParentForm).CreateElement(e.Location, this.picBoxCanvas.ClientSize);
                this.Activate();
            }            

            ReleaseCapture();
        }

        private void picBoxCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
     

            ((FatherWindow)this.ParentForm).drawDoc.Draw(e.Graphics);
            g.ResetTransform();
            base.OnPaint(e);
        }

        private void picBoxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            ((FatherWindow)this.ParentForm).CanvasMouseDown(sender, e);
        }

        private void picBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            ((FatherWindow)this.ParentForm).CanvasMouseMove(sender, e);
        }

        private void picBoxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            ((FatherWindow)this.ParentForm).CanvasMouseUp(sender, e);
        }

        private void picBoxCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            ((FatherWindow)this.ParentForm).CanvasMouseClick(sender, e);
        }

        private void contextmenustrip_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ToolStripMenuItem)
            {
                System.Windows.Forms.ToolStripMenuItem item = (System.Windows.Forms.ToolStripMenuItem)sender;
                System.Windows.Forms.ContextMenuStrip parent = (System.Windows.Forms.ContextMenuStrip)item.GetCurrentParent();
                int i = parent.Items.IndexOf(item);
                Int16 multi = Convert.ToInt16(i + 1);
                switch (multi)
                {
                    case 1:
                        ((FatherWindow)this.ParentForm).CutElement();
                        break;
                    case 2:
                        ((FatherWindow)this.ParentForm).CopyElement();
                        break;
                    case 3:
                        ((FatherWindow)this.ParentForm).PasteElement();
                        break;
                    case 4:
                        ((FatherWindow)this.ParentForm).DeleteElement();
                        break;
                }
            }
        }

        private void ContextMenuStripProperty_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ToolStripMenuItem)
            {
                DeviceFoupWayInfo formFoupWayInfo = new DeviceFoupWayInfo();
                int num = -1;
                string str = "";
                switch (formFoupWayInfo.ShowDialog(this.ParentForm))
                { 
                    case DialogResult.OK:
                        str = "FoupWay_" + formFoupWayInfo.FoupWayName.Text;
                        Mcs.RailSystem.Common.EleDevice deviceOk = (Mcs.RailSystem.Common.EleDevice)(((FatherWindow)(this.ParentForm)).drawDoc.LastHitedObject);
                        if (deviceOk.ListFoupDot != null)
                        {
                            num = deviceOk.ListFoupDot.Count;
                            for (int i = 0; i < num; i++)
                            {
                                if (deviceOk.ListFoupDot[i].railText == str)
                                {
                                    MessageBox.Show("there is a same one in system,please check again");
                                    return;
                                }
                            }
                        }
                        num = ((FatherWindow)(this.ParentForm)).drawDoc.DrawObjectList.Count;
                        for (int i = 0; i < num; i++)
                        {
                            if (((FatherWindow)(this.ParentForm)).drawDoc.DrawObjectList[i].railText == str)
                            {
                                Mcs.RailSystem.Common.EleFoupDot dot = ((Mcs.RailSystem.Common.EleFoupDot)((FatherWindow)(this.ParentForm)).drawDoc.DrawObjectList[i]);
                                if (dot.DeviceNum != 0)
                                {
                                    MessageBox.Show("this one belongs another device");
                                    return;
                                }
                                deviceOk.ListFoupDot.Add(dot);
                                dot.DeviceNum = deviceOk.DeviecID;
                                if (1 == deviceOk.ListFoupDot.Count)
                                    deviceOk.FoupDotFirst = dot;
                                return;
                            }
                        }
                        MessageBox.Show("there is a one in system,please check again");
                        break;
                    case DialogResult.Cancel:
                        if (formFoupWayInfo.FoupWayName.Text == "")
                            return;
                        str = "FoupWay_" + formFoupWayInfo.FoupWayName.Text;
                        Mcs.RailSystem.Common.EleDevice deviceDel = (Mcs.RailSystem.Common.EleDevice)(((FatherWindow)(this.ParentForm)).drawDoc.LastHitedObject);
                        if (deviceDel.ListFoupDot != null)
                        {
                            num = deviceDel.ListFoupDot.Count;
                            for (int i = 0; i < num; i++)
                            {
                                if (deviceDel.ListFoupDot[i].railText == str)
                                {
                                    deviceDel.ListFoupDot[i].DeviceNum = 0;
                                    deviceDel.ListFoupDot.RemoveAt(i);
                                    return;
                                }
                            }
                            MessageBox.Show("there is no one in list");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void DeleteElement()
        {
            ((FatherWindow)this.ParentForm).DeleteElement();
        }

        public void ContextMenuStripCreate(bool var)
        {
            contextMenuStripWorkReg.Items.Clear();
            Int16 type=0;
            Mcs.RailSystem.Common.BaseRailEle obj = ((FatherWindow)(this.ParentForm)).drawDoc.LastHitedObject;
            if (obj != null)
                type = (Int16)(((FatherWindow)(this.ParentForm)).drawDoc.LastHitedObject.GraphType);
            if (6!=type)
            {
                contextMenuStripWorkReg.Items.Add("cut", global::RailDraw.Properties.Resources.cut);
                contextMenuStripWorkReg.Items.Add("copy", global::RailDraw.Properties.Resources.Copy);
                contextMenuStripWorkReg.Items.Add("paste", global::RailDraw.Properties.Resources.Paste);
                contextMenuStripWorkReg.Items.Add("delete", global::RailDraw.Properties.Resources.delete);
                for (Int16 i = 0; i < contextMenuStripWorkReg.Items.Count; i++)
                {
                    contextMenuStripWorkReg.Items[i].Click += new EventHandler(contextmenustrip_Click);
                }
                contextMenuStripWorkReg.Show(Cursor.Position);
                if (var)
                {
                    contextMenuStripWorkReg.Items[2].Enabled = false;
                }
                else
                {
                    if (((FatherWindow)this.ParentForm).drawDoc.CutAndCopyObjectList.Count > 0)
                    {
                        contextMenuStripWorkReg.Items[0].Enabled = false;
                        contextMenuStripWorkReg.Items[1].Enabled = false;
                        contextMenuStripWorkReg.Items[3].Enabled = false;
                    }
                    else
                    {
                        contextMenuStripWorkReg.Items[0].Enabled = false;
                        contextMenuStripWorkReg.Items[1].Enabled = false;
                        contextMenuStripWorkReg.Items[2].Enabled = false;
                        contextMenuStripWorkReg.Items[3].Enabled = false;
                    }
                }
            }
            else if (6 == type)
            {
                contextMenuStripWorkReg.Items.Add("set device foupWay");
                contextMenuStripWorkReg.Items[0].Click += new EventHandler(ContextMenuStripProperty_Click);
                contextMenuStripWorkReg.Show(Cursor.Position);
            }
        }

        private void picBoxCanvas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (((FatherWindow)this.ParentForm).drawDoc.SelectedDrawObjectList.Count != 0)
            {
                ((FatherWindow)this.ParentForm).WorkRegionKeyMove(e.KeyCode);
            }
        }


    }
}
