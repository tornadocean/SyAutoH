using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BaseRailElement;
using WeifenLuo.WinFormsUI.Docking;
using System.Diagnostics;

namespace RailDraw
{
    public partial class WorkRegion : DockContent
    {
        public bool winShown = false;
        private Int16 lineNumber = 100;
        private Int16 curveNumber = 200;
        private Int16 crossNumber = 300;
        private Int16 foupDotNumber = 400;
        private Int16 deviceNumber = 500;
        private Int16 userDefNumber = 600;

        public Int16 LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }
        public Int16 CurveNumber
        {
            get { return curveNumber; }
            set { curveNumber = value; }
        }
        public Int16 CrossNumber
        {
            get { return crossNumber; }
            set { crossNumber = value; }
        }
        public Int16 FoupDotNumber
        {
            get { return foupDotNumber; }
            set { foupDotNumber = value; }
        }
        public Int16 DeviceNumber
        {
            get { return deviceNumber; }
            set { deviceNumber = value; }
        }
        public Int16 UserDefNumber
        {
            get { return userDefNumber; }
            set { userDefNumber = value; }
        }

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
            if (((FatherWindow)this.DockPanel.Parent).tools.PicLine && rc.Contains(pt) && this.ClientRectangle.Contains(pt))
            {
                CreateBaseElement(e.Location, this.picBoxCanvas.ClientSize);
                this.Activate();
            }            
            ReleaseCapture();
        }

        private void picBoxCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ((FatherWindow)this.DockHandler.DockPanel.Parent).drawDocOp.Draw(e.Graphics);
            g.ResetTransform();
            base.OnPaint(e);
        }

        private void picBoxCanvas_MouseDown(object sender, MouseEventArgs e)
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

        private void picBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            if (father.objectEvent.MouseLDown && !father.objectEvent.MouseLMove)
            {
                father.objectEvent.OnMouseMoveLeft(e.Location);
                this.picBoxCanvas.Invalidate();
            }
            else if (father.objectEvent.MouseLDown && father.objectEvent.MouseLMove)
            {
                this.picBoxCanvas.Left += (e.X - BaseEvents.LastPoint.X);
                this.picBoxCanvas.Top += (e.Y - BaseEvents.LastPoint.Y);
            }
            else if (father.objectEvent.MouseRDown && (e.Location != BaseEvents.LastPoint))
            {
                father.objectEvent.MouseRMove = true;
                this.Cursor = CommonFunction.CreatCursor("draw");
                this.picBoxCanvas.Left += (e.X - BaseEvents.LastPoint.X);
                this.picBoxCanvas.Top += (e.Y - BaseEvents.LastPoint.Y);
                this.picBoxCanvas.Invalidate();
            }
        }

        private void picBoxCanvas_MouseUp(object sender, MouseEventArgs e)
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
                    }
                    father.objectEvent.OnLButtonUp(e.Location);
                    break;
                case MouseButtons.Right:
                    if (!father.objectEvent.MouseRMove
                        && !father.objectEvent.MouseLDown
                        && father.objectEvent.MouseRDown)
                    {
                        this.ContextMenuStripCreate(father.objectEvent.OnRButtonDown(e.Location));
                    }
                    father.objectEvent.MouseRDown = false;
                    father.objectEvent.MouseRMove = false;
                    this.Cursor = Cursors.Default;
                    break;
            }
            father.proPage.propertyGrid1.Refresh();
            this.picBoxCanvas.Invalidate();
            this.picBoxCanvas.Focus();
        }

        private void picBoxCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            if (e.Button == MouseButtons.Left)
            {
                if (1 == father.drawDocOp.SelectedDrawObjectList.Count)
                {
                    Mcs.RailSystem.Common.BaseRailEle baseEle =father.drawDocOp.SelectedDrawObjectList[0];
                    Int16 i = Convert.ToInt16(father.drawDocOp.DrawObjectList.IndexOf(father.drawDocOp.SelectedDrawObjectList[0]));
                    if (i >= 0)
                        father.proRegion.SelectedElement(i);
                }
                else
                {
                    father.proPage.propertyGrid1.SelectedObject = null;
                    father.proRegion.treeView1.SelectedNode = null;
                    father.proPage.propertyGrid1.Refresh();
                }
            }
            this.Activate();
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
                        CutElement();
                        break;
                    case 2:
                        CopyElement();
                        break;
                    case 3:
                        PasteElement();
                        break;
                    case 4:
                        DeleteElement();
                        break;
                }
            }
        }

        private void ContextMenuStripProperty_Click(object sender, EventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            if (sender is System.Windows.Forms.ToolStripMenuItem)
            {
                DeviceFoupWayInfo formFoupWayInfo = new DeviceFoupWayInfo();
                int num = -1;
                string str = "";
                switch (formFoupWayInfo.ShowDialog(father))
                { 
                    case DialogResult.OK:
                        str = "FoupWay_" + formFoupWayInfo.FoupWayName.Text;
                        Mcs.RailSystem.Common.EleDevice deviceOk = (Mcs.RailSystem.Common.EleDevice)(father.drawDocOp.LastHitedObject);
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
                        num = father.drawDocOp.DrawObjectList.Count;
                        for (int i = 0; i < num; i++)
                        {
                            if (father.drawDocOp.DrawObjectList[i].railText == str)
                            {
                                Mcs.RailSystem.Common.EleFoupDot dot = ((Mcs.RailSystem.Common.EleFoupDot)father.drawDocOp.DrawObjectList[i]);
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
                        MessageBox.Show("there is no one foupWay in system,please check again");
                        break;
                    case DialogResult.Cancel:
                        if (formFoupWayInfo.FoupWayName.Text == "")
                            return;
                        str = "FoupWay_" + formFoupWayInfo.FoupWayName.Text;
                        Mcs.RailSystem.Common.EleDevice deviceDel = (Mcs.RailSystem.Common.EleDevice)(father.drawDocOp.LastHitedObject);
                        if (deviceDel.ListFoupDot != null)
                        {
                            num = deviceDel.ListFoupDot.Count;
                            for (int i = 0; i < num; i++)
                            {
                                if (deviceDel.ListFoupDot[i].railText == str)
                                {
                                    deviceDel.ListFoupDot[i].DeviceNum = 0;
                                    deviceDel.ListFoupDot.RemoveAt(i);
                                    if (deviceDel.FoupDotFirst.railText == str)
                                    {
                                        if (0 != deviceDel.ListFoupDot.Count)
                                        {
                                            deviceDel.FoupDotFirst = deviceDel.ListFoupDot[0];
                                        }
                                        else
                                        {
                                            deviceDel.FoupDotFirst = null;
                                        }
                                    }
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

        public void ContextMenuStripCreate(bool var)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            contextMenuStripWorkReg.Items.Clear();
            Int16 type=0;
            Mcs.RailSystem.Common.BaseRailEle obj = father.drawDocOp.LastHitedObject;
            if (obj != null)
                type = (Int16)(father.drawDocOp.LastHitedObject.GraphType);
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
                    if (father.drawDocOp.CutAndCopyObjectList.Count > 0)
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
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            if (father.drawDocOp.SelectedDrawObjectList.Count != 0)
            {
                BaseRailElement.ObjectBaseEvents.Direction direction = BaseRailElement.ObjectBaseEvents.Direction.Null;
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        direction = BaseRailElement.ObjectBaseEvents.Direction.up;
                        break;
                    case Keys.Down:
                        direction = BaseRailElement.ObjectBaseEvents.Direction.down;
                        break;
                    case Keys.Left:
                        direction = BaseRailElement.ObjectBaseEvents.Direction.left;
                        break;
                    case Keys.Right:
                        direction = BaseRailElement.ObjectBaseEvents.Direction.right;
                        break;
                    default:
                        break;
                }
                father.objectEvent.WorkRegionKeyDown(direction);
                this.picBoxCanvas.Invalidate();
            }
            return;
        }

        private void CreateBaseElement(Point mousePt, Size workRegionSize)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            string str = father.tools.itemSelected.Text;
            string strPath = "";


            foreach (string strUserDef in father.drawDocOp.ListUserDefAdd)
            {
                string strPathTemp = strUserDef;
                string strTemp = strUserDef.Substring(strUserDef.IndexOf("userdef\\") + 8);
                strTemp = strTemp.Substring(0, strTemp.IndexOf(".bmp"));
                if (strTemp == str)
                {
                    str = "Device_userDef";
                    strPath = strPathTemp;
                }
            }

            switch (str)
            {
                case "Line":
                    BaseRailElement.RailEleLine line = new RailEleLine();
                    ++lineNumber;
                    str += "_" + lineNumber.ToString();
                    line.CreateEle(mousePt, workRegionSize, father.drawDocOp.DrawMultiFactor, str);
                    AddElement(line);
                    father.drawDocOp.SelectOne(line);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = line;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                case "Curve":
                    BaseRailElement.RailEleCurve curve = new RailEleCurve();
                    ++curveNumber;
                    str += "_" + curveNumber.ToString();
                    curve.CreateEle(mousePt, workRegionSize, father.drawDocOp.DrawMultiFactor, str);
                    AddElement(curve);
                    father.drawDocOp.SelectOne(curve);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = curve;
                    father.proPage.Refresh();
                    break;
                case "Cross":
                    BaseRailElement.RailEleCross cross = new RailEleCross();
                    ++crossNumber;
                    str += "_" + crossNumber.ToString();
                    cross.CreateEle(mousePt, workRegionSize, father.drawDocOp.DrawMultiFactor, str);
                    AddElement(cross);
                    father.drawDocOp.SelectOne(cross);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = cross;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                case "FoupWay":
                    BaseRailElement.RailEleFoupDot dot = new RailEleFoupDot();
                    ++foupDotNumber;
                    str += "_" + foupDotNumber.ToString();
                    dot.CreateEle(mousePt, workRegionSize, father.drawDocOp.DrawMultiFactor, str);
                    AddElement(dot);
                    father.drawDocOp.SelectOne(dot);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = dot;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                case "Device":
                    BaseRailElement.RailEleDevice device = new RailEleDevice();
                    ++deviceNumber;
                    str += "_" + deviceNumber.ToString();
                    device.CreateEle(mousePt, workRegionSize, father.drawDocOp.DrawMultiFactor, str);
                    AddElement(device);
                    father.drawDocOp.SelectOne(device);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = device;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                case "Device_userDef":
                    BaseRailElement.RailEleDevice device_userdef = new RailEleDevice();
                    ++deviceNumber;
                    str = str.Substring(0, str.IndexOf("_userDef"));
                    str += "_" + deviceNumber.ToString();
                    device_userdef.CreateEle(mousePt, workRegionSize, father.drawDocOp.DrawMultiFactor, str, strPath);
                    AddElement(device_userdef);
                    father.drawDocOp.SelectOne(device_userdef);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = device_userdef;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                default:
                    break;
            }
            father.tools.itemSelected = null;
        }

        public void AddElement(Mcs.RailSystem.Common.BaseRailEle baseRailEle)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            string str = baseRailEle.railText;
            int lenght = str.IndexOf('_');
            if (-1 != lenght)
            {
                str = str.Substring(0, lenght);
            }
            switch (str)
            {
                case "Line":
                    Mcs.RailSystem.Common.EleLine line = (Mcs.RailSystem.Common.EleLine)baseRailEle;
                    father.drawDocOp.DrawObjectList.Add(line);
                    father.objectEvent.ProRegionAddNode(line.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "Curve":
                    Mcs.RailSystem.Common.EleCurve curve = (Mcs.RailSystem.Common.EleCurve)baseRailEle;
                    father.drawDocOp.DrawObjectList.Add(curve);
                    father.objectEvent.ProRegionAddNode(curve.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "Cross":
                    Mcs.RailSystem.Common.EleCross cross = (Mcs.RailSystem.Common.EleCross)baseRailEle;
                    father.drawDocOp.DrawObjectList.Add(cross);
                    father.objectEvent.ProRegionAddNode(cross.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "FoupWay":
                    Mcs.RailSystem.Common.EleFoupDot dot = (Mcs.RailSystem.Common.EleFoupDot)baseRailEle;
                    father.drawDocOp.DrawObjectList.Add(dot);
                    father.objectEvent.ProRegionAddNode(dot.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "Device":
                    Mcs.RailSystem.Common.EleDevice device = (Mcs.RailSystem.Common.EleDevice)baseRailEle;
                    father.drawDocOp.DrawObjectList.Add(device);
                    father.objectEvent.ProRegionAddNode(device.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "UserDef":
                    Mcs.RailSystem.Common.EleUserDef userDef = (Mcs.RailSystem.Common.EleUserDef)baseRailEle;
                    father.drawDocOp.DrawObjectList.Add(userDef);
                    father.objectEvent.ProRegionAddNode(userDef.railText);
                    father.proRegion.RefreshTreeView();
                    break;
            }
        }

        public void CutElement()
        {
            ((FatherWindow)this.DockHandler.DockPanel.Parent).drawDocOp.Cut();
            this.picBoxCanvas.Invalidate();
        }

        public void CopyElement()
        {
            ((FatherWindow)this.DockHandler.DockPanel.Parent).drawDocOp.Copy();
        }

        public void PasteElement()
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            for (Int16 i = 0; i < father.drawDocOp.CutAndCopyObjectList.Count; )
            {
                string str = father.drawDocOp.CutAndCopyObjectList[0].railText;
                int lenght = str.IndexOf('_');
                if (-1 != lenght)
                {
                    str = str.Substring(0, lenght);
                }
                if (str == "Line")
                {
                    lineNumber++;
                    str += "_" + lineNumber.ToString();
                }
                else if (str == "Curve")
                {
                    curveNumber++;
                    str += "_" + curveNumber.ToString();
                }
                else if (str == "Cross")
                {
                    crossNumber++;
                    str += "_" + crossNumber.ToString();
                }
                else if (str == "FoupWay")
                {
                    foupDotNumber++;
                    str += "_" + foupDotNumber.ToString();
                }
                else if (str == "Device")
                {
                    deviceNumber++;
                    str += "_" + deviceNumber.ToString();
                }
                father.drawDocOp.Paste(str);
                father.objectEvent.ProRegionAddNode(str);
                father.proRegion.RefreshTreeView();
                this.picBoxCanvas.Invalidate();
                father.proPage.propertyGrid1.SelectedObject = father.drawDocOp.SelectedDrawObjectList[0];
            }
        }

        public void DeleteElement()
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            for (Int16 i = 0; i < father.drawDocOp.SelectedDrawObjectList.Count; )
            {
                if (5==father.drawDocOp.SelectedDrawObjectList[0].GraphType
                    && 0!=((Mcs.RailSystem.Common.EleFoupDot)(father.drawDocOp.SelectedDrawObjectList[0])).DeviceNum)
                {
                    MessageBox.Show("please remove the FoupWay from device first");
                    return;
                }
                Int16 num = Convert.ToInt16(father.drawDocOp.DrawObjectList.IndexOf(father.drawDocOp.SelectedDrawObjectList[0]));
                if (-1 == num)
                {
                    num = Convert.ToInt16(father.drawDocOp.ListAuxiliaryDraw.IndexOf(father.drawDocOp.SelectedDrawObjectList[0]));
                    if (-1 != num)
                    {
                        num += 10000;
                    }
                }
                father.drawDocOp.Delete(num);
            }
            foreach (Mcs.RailSystem.Common.BaseRailEle obj in father.drawDocOp.DrawObjectList)
            {
                if (7 == obj.GraphType)
                {
                    father.proRegion.RefreshTreeView();
                    this.picBoxCanvas.Invalidate();
                    return;
                }
            }

            foreach(TreeNode nd in father.proRegion.treeView1.Nodes[0].Nodes)
            {
                if (nd.Text == "UserDef")
                {
                    father.proRegion.treeView1.Nodes[0].Nodes.Remove(nd);
                    userDefNumber = 600;
                    break;
                }
            }
            this.picBoxCanvas.Invalidate();
        }

        public void CreateUserDefinedEle(Point offset,Size workRegionSize,Image image)
        {
            FatherWindow father = (FatherWindow)this.DockHandler.DockPanel.Parent;
            BaseRailElement.RailEleUserDef userDef = new RailEleUserDef();
            Mcs.RailSystem.Common.EleUserDef.UserDefType type = Mcs.RailSystem.Common.EleUserDef.UserDefType.picture;
            if (600 == userDefNumber)
            {
                father.proRegion.treeView1.Nodes[0].Nodes.Add("UserDef");
            }
            ++userDefNumber;
            string str = "UserDef";
            str += "_" + userDefNumber.ToString();
            userDef.CreateEle(type, offset, workRegionSize, father.drawDocOp.DrawMultiFactor, str, image);
            AddElement(userDef);
            father.drawDocOp.SelectOne(userDef);
            this.picBoxCanvas.Invalidate();
        }

        private void WorkRegion_Activated(object sender, EventArgs e)
        {
            BaseRailElement.ObjectBaseEvents.DocumentOp = ((FatherWindow)this.DockPanel.Parent).drawDocOp;
            ((FatherWindow)this.DockPanel.Parent).activeWindowHandler = this.DockHandler;
            ((FatherWindow)this.DockPanel.Parent).proRegion.RefreshTreeView();
            ((FatherWindow)this.DockPanel.Parent).tools.InitTools(sender, e);
        }


    }
}
