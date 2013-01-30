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
using BaseRailElement;

namespace RailDraw
{
    public partial class WorkRegion : DockContent
    {
        public bool winShown = false;
        private bool mouseLDown = false;
        private bool mouseRDown = false;
        private bool mouseLMove = false;
        private bool mouseRMove = false;
        private Int16 lineNumber = 100;
        private Int16 curveNumber = 200;
        private Int16 crossNumber = 300;
        private Int16 foupDotNumber = 400;
        private Int16 deviceNumber = 500;
        private Int16 userDefNumber = 600;
        private Int16 multiFactor = 1;

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

        public bool MouseLMove
        {
            get { return mouseLMove; }
            set { mouseLMove = value; }
        }
        public Int16 MultiFactor
        {
            get { return multiFactor; }
            set { multiFactor = value; }
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
            if (((FatherWindow)this.ParentForm).tools.PicLine && rc.Contains(pt))
            {
                CreateBaseElement(e.Location, this.picBoxCanvas.ClientSize);
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
            switch (e.Button)
            { 
                case MouseButtons.Left:
                    ((FatherWindow)this.ParentForm).objectEvent.OnLButtonDown(e.Location);
                    mouseLDown = true;
                    if (mouseLMove)
                        this.Cursor = CommonFunction.CreatCursor("draw");
                    break;
                case MouseButtons.Right:
                    ((FatherWindow)this.ParentForm).objectEvent.OnRButtonDown(e.Location);
                    mouseRDown = true;
                    break;
            }
        }

        private void picBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseLDown && !mouseLMove)
            {
                ((FatherWindow)this.ParentForm).objectEvent.OnMouseMoveLeft(e.Location);
                this.picBoxCanvas.Invalidate();
            }
            else if (mouseLDown && mouseLMove)
            {
                this.picBoxCanvas.Left += (e.X - BaseEvents.LastPoint.X);
                this.picBoxCanvas.Top += (e.Y - BaseEvents.LastPoint.Y);
            }
            else if (mouseRDown && (e.Location != BaseEvents.LastPoint))
            {
                mouseRMove = true;
                this.Cursor = CommonFunction.CreatCursor("draw");
                this.picBoxCanvas.Left += (e.X - BaseEvents.LastPoint.X);
                this.picBoxCanvas.Top += (e.Y - BaseEvents.LastPoint.Y);
                this.picBoxCanvas.Invalidate();
            }
        }

        private void picBoxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.ParentForm;
            switch (e.Button)
            { 
                case MouseButtons.Left:
                    if (mouseLMove && mouseLDown && !mouseRDown)
                    {
                        mouseLMove = true;
                        this.Cursor = Cursors.Default;
                    }
                    if (mouseLDown)
                    {
                        mouseLDown = false;
                    }
                    father.objectEvent.OnLButtonUp(e.Location);
                    break;
                case MouseButtons.Right:
                    if (!mouseRMove && !mouseLDown && mouseRDown)
                    {
                        this.ContextMenuStripCreate(father.objectEvent.OnRButtonDown(e.Location));
                    }
                    mouseRDown = false;
                    mouseRMove = false;
                    this.Cursor = Cursors.Default;
                    break;
            }
            father.proPage.propertyGrid1.Refresh();
            this.picBoxCanvas.Invalidate();
            this.picBoxCanvas.Focus();
        }

        private void picBoxCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.ParentForm;
            if (e.Button == MouseButtons.Left)
            {
                if (1 == father.drawDoc.SelectedDrawObjectList.Count)
                {
                    Mcs.RailSystem.Common.BaseRailEle baseEle =father.drawDoc.SelectedDrawObjectList[0];
                    Int16 i = Convert.ToInt16(father.drawDoc.DrawObjectList.IndexOf(father.drawDoc.SelectedDrawObjectList[0]));
                    if (i >= 0)
                        father.proRegion.SelectedElement(i);
                    //else if (-1 == i)
                    //{
                    //    i = Convert.ToInt16(father.drawDoc.ListAuxiliaryDraw.IndexOf(father.drawDoc.SelectedDrawObjectList[0]));
                    //}
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
                        MessageBox.Show("there is no one foupWay in system,please check again");
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
            FatherWindow father = (FatherWindow)this.ParentForm;
            if (father.drawDoc.SelectedDrawObjectList.Count != 0)
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
        }

        private void CreateBaseElement(Point mousePt, Size workRegionSize)
        {
            FatherWindow father = (FatherWindow)this.ParentForm;
            string str = father.tools.itemSelected.Text;
            switch (father.tools.itemSelected.Text)
            {
                case "Line":
                    BaseRailElement.RailEleLine line = new RailEleLine();
                    ++lineNumber;
                    str += "_" + lineNumber.ToString();
                    line.CreateEle(mousePt, workRegionSize, father.drawDoc.DrawMultiFactor, str);
                    AddElement(line);
                    father.drawDoc.SelectOne(line);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = line;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                case "Curve":
                    BaseRailElement.RailEleCurve curve = new RailEleCurve();
                    ++curveNumber;
                    str += "_" + curveNumber.ToString();
                    curve.CreateEle(mousePt, workRegionSize, father.drawDoc.DrawMultiFactor, str);
                    AddElement(curve);
                    father.drawDoc.SelectOne(curve);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = curve;
                    father.proPage.Refresh();
                    break;
                case "Cross":
                    BaseRailElement.RailEleCross cross = new RailEleCross();
                    ++crossNumber;
                    str += "_" + crossNumber.ToString();
                    cross.CreateEle(mousePt, workRegionSize, father.drawDoc.DrawMultiFactor, str);
                    AddElement(cross);
                    father.drawDoc.SelectOne(cross);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = cross;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                case "FoupWay":
                    BaseRailElement.RailEleFoupDot dot = new RailEleFoupDot();
                    ++foupDotNumber;
                    str += "_" + foupDotNumber.ToString();
                    dot.CreateEle(mousePt, workRegionSize, father.drawDoc.DrawMultiFactor, str);
                    AddElement(dot);
                    father.drawDoc.SelectOne(dot);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = dot;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                case "Device":
                    BaseRailElement.RailEleDevice device = new RailEleDevice();
                    ++deviceNumber;
                    str += "_" + deviceNumber.ToString();
                    device.CreateEle(mousePt, workRegionSize, father.drawDoc.DrawMultiFactor, str);
                    AddElement(device);
                    father.drawDoc.SelectOne(device);
                    this.picBoxCanvas.Invalidate();
                    father.proPage.propertyGrid1.SelectedObject = device;
                    father.proPage.propertyGrid1.Refresh();
                    break;
                default:
                    break;
            }
            father.tools.itemSelected = null;
        }

        public void AddElement(Mcs.RailSystem.Common.BaseRailEle baseRailEle)
        {
            FatherWindow father = (FatherWindow)this.ParentForm;
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
                    father.drawDoc.DrawObjectList.Add(line);
                    father.objectEvent.ProRegionAddNode(line.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "Curve":
                    Mcs.RailSystem.Common.EleCurve curve = (Mcs.RailSystem.Common.EleCurve)baseRailEle;
                    father.drawDoc.DrawObjectList.Add(curve);
                    father.objectEvent.ProRegionAddNode(curve.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "Cross":
                    Mcs.RailSystem.Common.EleCross cross = (Mcs.RailSystem.Common.EleCross)baseRailEle;
                    father.drawDoc.DrawObjectList.Add(cross);
                    father.objectEvent.ProRegionAddNode(cross.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "FoupWay":
                    Mcs.RailSystem.Common.EleFoupDot dot = (Mcs.RailSystem.Common.EleFoupDot)baseRailEle;
                    father.drawDoc.DrawObjectList.Add(dot);
                    father.objectEvent.ProRegionAddNode(dot.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "Device":
                    Mcs.RailSystem.Common.EleDevice device = (Mcs.RailSystem.Common.EleDevice)baseRailEle;
                    father.drawDoc.DrawObjectList.Add(device);
                    father.objectEvent.ProRegionAddNode(device.railText);
                    father.proRegion.RefreshTreeView();
                    break;
                case "UserDef":
                    Mcs.RailSystem.Common.EleUserDef userDef = (Mcs.RailSystem.Common.EleUserDef)baseRailEle;
                    father.drawDoc.DrawObjectList.Add(userDef);
                    father.objectEvent.ProRegionAddNode(userDef.railText);
                    father.proRegion.RefreshTreeView();

                    break;
                    
            }
        }

        public void CutElement()
        {
            ((FatherWindow)this.ParentForm).drawDoc.Cut();
            this.picBoxCanvas.Invalidate();
        }

        public void CopyElement()
        {
            ((FatherWindow)this.ParentForm).drawDoc.Copy();
        }

        public void PasteElement()
        { 
            FatherWindow father = (FatherWindow)this.ParentForm;
            for (Int16 i = 0; i < father.drawDoc.CutAndCopyObjectList.Count; )
            {
                string str = father.drawDoc.CutAndCopyObjectList[0].railText;
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
                father.drawDoc.Paste(str);
                father.objectEvent.ProRegionAddNode(str);
                father.proRegion.RefreshTreeView();
                this.picBoxCanvas.Invalidate();
                father.proPage.propertyGrid1.SelectedObject = father.drawDoc.SelectedDrawObjectList[0];
            }
        }

        public void DeleteElement()
        {
            FatherWindow father = (FatherWindow)this.ParentForm;
            for (Int16 i = 0; i < father.drawDoc.SelectedDrawObjectList.Count; )
            {
                if (5==father.drawDoc.SelectedDrawObjectList[0].GraphType
                    && 0!=((Mcs.RailSystem.Common.EleFoupDot)(father.drawDoc.SelectedDrawObjectList[0])).DeviceNum)
                {
                    MessageBox.Show("please remove the FoupWay from device first");
                    return;
                }
                Int16 num = Convert.ToInt16(father.drawDoc.DrawObjectList.IndexOf(father.drawDoc.SelectedDrawObjectList[0]));
                if (-1 == num)
                {
                    num = Convert.ToInt16(father.drawDoc.ListAuxiliaryDraw.IndexOf(father.drawDoc.SelectedDrawObjectList[0]));
                    if (-1 != num)
                    {
                        num += 10000;
                    }
                }
                father.drawDoc.Delete(num);
                
            }
            foreach (Mcs.RailSystem.Common.BaseRailEle obj in father.drawDoc.DrawObjectList)
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
            FatherWindow father = (FatherWindow)this.ParentForm;
            BaseRailElement.RailEleUserDef userDef = new RailEleUserDef();
            Mcs.RailSystem.Common.EleUserDef.UserDefType type = Mcs.RailSystem.Common.EleUserDef.UserDefType.picture;
            if (600 == userDefNumber)
            {
                father.proRegion.treeView1.Nodes[0].Nodes.Add("UserDef");
            }
            ++userDefNumber;
            string str = "UserDef";
            str += "_" + userDefNumber.ToString();
            userDef.CreateEle(type, offset, workRegionSize, father.drawDoc.DrawMultiFactor, str, image);
            AddElement(userDef);
            father.drawDoc.SelectOne(userDef);
            this.picBoxCanvas.Invalidate();
        }


    }
}
