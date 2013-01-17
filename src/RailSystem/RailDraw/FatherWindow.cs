using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BaseRailElement;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace RailDraw
{
    public partial class FatherWindow : Form
    {
        public ProgramRegion proRegion = new ProgramRegion();
        public PropertyPage proPage = new PropertyPage();
        public WorkRegion workRegion = new WorkRegion();
        public Tools tools = new Tools();
        public BaseRailElement.DrawDocOp drawDoc = new BaseRailElement.DrawDocOp();
        public BaseRailElement.ObjectBaseEvents objectEvent = new BaseRailElement.ObjectBaseEvents();
        private bool mouseLDown = false;
        private bool mouseRDown = false;
        private bool canvasMove = false;
        private bool mouseRMove = false;
        private string sProjectPath = "";
        private Size drawregOrigSize = new Size();
        private const Int16 CONST_MULTI_FACTOR = 1;
        private Int16 multiFactor = 1;
        private Point workSize = Point.Empty;
        private Int16 lineNumber = 100;
        private Int16 curveNumber = 200;
        private Int16 crossNumber = 300;
        public bool DrapIsDown
        {
            get { return canvasMove; }
        }

        public FatherWindow()
        {
            InitializeComponent();
        }

        private void FatherWindow_Load(object sender, EventArgs e)
        {
            BaseRailElement.ObjectBaseEvents.Document = drawDoc;
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            this.Size = (Size)new Point(screen.Width, screen.Height);
            
            workSize = new Point(((Point)this.ClientSize).X - 4, ((Point)this.ClientSize).Y - this.menuStrip1.Height - this.toolStrip1.Height);

            this.Location = new Point(0, 0);

            proRegion.Show(this.dockPanel1);
            proRegion.DockTo(this.dockPanel1, DockStyle.Left);

            proPage.Show(this.dockPanel1);
            proPage.DockTo(this.dockPanel1, DockStyle.Left);

            workRegion.Size = (Size)new Point(workSize.X / 6 * 4, workSize.Y);
            workRegion.Show(this.dockPanel1);
            workRegion.DockTo(this.dockPanel1, DockStyle.Fill);

            tools.Show(this.dockPanel1);
            tools.DockTo(this.dockPanel1, DockStyle.Right);

            drawregOrigSize.Width = this.workRegion.picBoxCanvas.Width;
            drawregOrigSize.Height = this.workRegion.picBoxCanvas.Height;
        }

        #region  workRegion Operation

        public void CanvasMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    objectEvent.OnLButtonDown(e.Location);
                    mouseLDown = true;
                    if (canvasMove)
                        this.Cursor = CommonFunction.CreatCursor("draw");
                    break;
                case MouseButtons.Right:
                    objectEvent.OnRButtonDown(e.Location);
                    mouseRDown = true;
                    break;
            }
            this.workRegion.picBoxCanvas.Invalidate();
        }

        public void CanvasMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (canvasMove && mouseLDown && !mouseRDown)
                    {
                        canvasMove = true;
                        this.Cursor = Cursors.Default;
                    }
                    if (mouseLDown)
                    {
                        mouseLDown = false;
                    }
                    objectEvent.OnLButtonUp(e.Location);
                    break;
                case MouseButtons.Right:
                    if (!mouseRMove && !mouseLDown && mouseRDown)
                    {
                        this.workRegion.ContextMenuStripCreate(objectEvent.OnRButtonDown(e.Location));
                    }
                    mouseRDown = false;
                    mouseRMove = false;
                    this.Cursor = Cursors.Default;
                    break;
            }
            this.workRegion.picBoxCanvas.Invalidate();
            this.workRegion.picBoxCanvas.Focus();
        }

        public void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseLDown && !canvasMove)
            {
                objectEvent.OnMouseMoveLeft(e.Location);
                this.workRegion.picBoxCanvas.Invalidate();
            }
            else if (mouseLDown && canvasMove)
            {
                this.workRegion.picBoxCanvas.Left += (e.X - BaseEvents.LastPoint.X);
                this.workRegion.picBoxCanvas.Top += (e.Y - BaseEvents.LastPoint.Y);
            }
            else if (mouseRDown && (e.Location!=BaseEvents.LastPoint))
            {
                mouseRMove = true;
                this.Cursor = CommonFunction.CreatCursor("draw");
                this.workRegion.picBoxCanvas.Left += (e.X - BaseEvents.LastPoint.X);
                this.workRegion.picBoxCanvas.Top += (e.Y - BaseEvents.LastPoint.Y);
                this.workRegion.picBoxCanvas.Invalidate();
                Debug.WriteLine(string.Format("mouse move is {0}", e.Location));
            }
            Debug.WriteLine(string.Format("mouseRDown is {0}", e.Location));
        }

        public void CanvasMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (drawDoc.SelectedDrawObjectList.Count == 1)
                {
                    Mcs.RailSystem.Common.BaseRailEle baseEle = drawDoc.SelectedDrawObjectList[0];
                    Int16 i = Convert.ToInt16(drawDoc.DrawObjectList.IndexOf(drawDoc.SelectedDrawObjectList[0]));
                    SelectedElement(i);
                }
                else
                {
                    this.proPage.propertyGrid1.SelectedObject = null;
                    this.proRegion.treeView1.SelectedNode = null;
                    this.proPage.propertyGrid1.Refresh();
                }
            }
            this.workRegion.Activate();
        }
        #endregion

        public void ChangePropertyValue()
        {
            objectEvent.ChangePropertyValue();
            this.workRegion.picBoxCanvas.Invalidate();
            this.proPage.propertyGrid1.Refresh();
        }

        #region 菜单操作
        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            sProjectPath = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "configuration (*.xml)|*.xml";
            saveFile.InitialDirectory = "";
            saveFile.Title = "另存为文件";
            saveFile.FileName = "";
            SaveFile(saveFile);
            UpdateFormTitle();
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void menuChooseAll_Click(object sender, EventArgs e)
        {
            this.drawDoc.SelectedDrawObjectList.Clear();
            if (this.drawDoc.DrawObjectList.Count > 0)
            {
                foreach (BaseRailElement.RailEleLine o in this.drawDoc.DrawObjectList)
                    this.drawDoc.SelectedDrawObjectList.Add(o);
            }
            this.workRegion.picBoxCanvas.Invalidate();
        }

        private void menupercentone_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ToolStripMenuItem)
            {
                System.Windows.Forms.ToolStripMenuItem item = (System.Windows.Forms.ToolStripMenuItem)sender;
                System.Windows.Forms.ToolStrip parent = item.GetCurrentParent();
                int i = parent.Items.IndexOf(item);
                Int16 multi = Convert.ToInt16(i + 1);
                this.workRegion.picBoxCanvas.Width = drawregOrigSize.Width * multi;
                this.workRegion.picBoxCanvas.Height = drawregOrigSize.Height * multi;
                EnlargeAndShortenCanvas(multi);
            }
        }

        private void programRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!proRegion.winShown)
            {
                proRegion.Show();
            }
            proRegion.Activate();
        }

        private void propertyPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!proPage.winShown)
            {
                proPage.Show();
            }
            proPage.Activate();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tools.winShown)
            {
                tools.Show();
            }
            tools.Activate();
        }

        private void workRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!workRegion.winShown)
            {
                workRegion.Show();
            }
            workRegion.Activate();
        }

        private void FatherWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
        #endregion

        #region 工具栏操作
        private void new_btn_Click(object sender, EventArgs e)
        {
            if (drawDoc.DrawObjectList.Count > 0)
            {
                SaveOfNew save_form = new SaveOfNew();
                save_form.StartPosition = FormStartPosition.CenterParent;
                switch (save_form.ShowDialog())
                {
                    case DialogResult.Yes:
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.Filter = "configuration (*.xml)|*.xml";
                        saveFile.InitialDirectory = "";
                        saveFile.Title = "存储文件";
                        saveFile.FileName = "";
                        SaveFile(saveFile);
                        UpdateFormTitle();
                        drawDoc.DrawObjectList.Clear();
                        drawDoc.SelectedDrawObjectList.Clear();
                        proRegion.treeNodeList.Clear();
                        proRegion.treeView1.Nodes[0].Nodes.Clear();
                        this.workRegion.picBoxCanvas.Invalidate();
                        this.proRegion.Invalidate();
                        break;
                    case DialogResult.No:
                        drawDoc.DrawObjectList.Clear();
                        drawDoc.SelectedDrawObjectList.Clear();
                        proRegion.treeNodeList.Clear();
                        proRegion.treeView1.Nodes[0].Nodes.Clear();
                        this.workRegion.picBoxCanvas.Invalidate();
                        this.proRegion.Invalidate();
                        break;
                }
            }
        }
        private void UpdateFormTitle()
        {
            this.Text = sProjectPath + "  -  Rails Map Editor";
        }

        private void open_Click(object sender, EventArgs e)
        {
            sProjectPath = "";
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "configuration (*.xml)|*.xml";
            openFile.InitialDirectory = "";
            openFile.Title = "open files";
            openFile.FileName = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string projectpath = openFile.FileName;
                drawDoc.DrawObjectList.Clear();
                drawDoc.SelectedDrawObjectList.Clear();
                proRegion.treeNodeList.Clear();
                proRegion.treeView1.Nodes[0].Nodes.Clear();
                this.workRegion.picBoxCanvas.Top = 0;
                this.workRegion.picBoxCanvas.Left = 0;
                this.workRegion.picBoxCanvas.Width = drawregOrigSize.Width;
                this.workRegion.picBoxCanvas.Height = drawregOrigSize.Height;
                canvasMove = false;
                this.Cursor = System.Windows.Forms.Cursors.Default;
                try
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(projectpath);
                    if (OpenXmlFile(ds))
                        BaseRailElement.ObjectBaseEvents.Document = drawDoc;
                    sProjectPath = projectpath;
                    UpdateFormTitle();
                }
                catch
                {
                    MessageBox.Show("open error");
                }
            }
            this.workRegion.picBoxCanvas.Invalidate();
            this.proRegion.Invalidate();
            this.proRegion.treeView1.Invalidate();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (sProjectPath == "")
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "configuration (*.xml)|*.xml";
                saveFile.InitialDirectory = "";
                saveFile.Title = "存储文件";
                saveFile.FileName = "";
                SaveFile(saveFile);
            }
            else
            {
                try
                {
                    string projectpath = sProjectPath;
                    string projectcodingpath = projectpath.Substring(0, projectpath.Length - 4) + "_coding.xml";
                    drawDoc.DataXmlSave();
                    drawDoc.DsEle.WriteXml(projectpath);
                    drawDoc.DsEleCoding.WriteXml(projectcodingpath);
                }
                catch(Exception ex)
                {
                    //MessageBox.Show("save error");
                    Debug.WriteLine(string.Format("error is: {0}", ex));
                }
            }
        }

        private void cut_Click(object sender, EventArgs e)
        {
            this.drawDoc.Cut();
            this.workRegion.picBoxCanvas.Invalidate();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            CopyElement();
        }

        private void paste_Click(object sender, EventArgs e)
        {
            PasteElement();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DeleteElement();
        }

        private void enlarge_Click(object sender, EventArgs e)
        {
            if (this.workRegion.picBoxCanvas.Width < drawregOrigSize.Width * 4)
            {
                this.workRegion.picBoxCanvas.Width += (drawregOrigSize.Width * CONST_MULTI_FACTOR);
                this.workRegion.picBoxCanvas.Height += (drawregOrigSize.Height * CONST_MULTI_FACTOR);
                multiFactor = Convert.ToInt16(this.workRegion.picBoxCanvas.Width / drawregOrigSize.Width);
                this.drawDoc.DrawMultiFactor = multiFactor;
                int n = this.drawDoc.DrawObjectList.Count;
                for (int i = 0; i < n; i++)
                {
                    this.drawDoc.DrawObjectList[i].DrawMultiFactor = multiFactor;
                }
                ChangeDrawRegionLoction();
                this.workRegion.picBoxCanvas.Invalidate();
            }
        }

        private void shorten_Click(object sender, EventArgs e)
        {
            if (this.workRegion.picBoxCanvas.Width > drawregOrigSize.Width)
            {
                this.workRegion.picBoxCanvas.Width -= (drawregOrigSize.Width * CONST_MULTI_FACTOR);
                this.workRegion.picBoxCanvas.Height -= (drawregOrigSize.Height * CONST_MULTI_FACTOR);
                multiFactor = Convert.ToInt16(this.workRegion.picBoxCanvas.Width / drawregOrigSize.Width);
                this.drawDoc.DrawMultiFactor = multiFactor;
                int n = this.drawDoc.DrawObjectList.Count;
                for (int i = 0; i < n; i++)
                    this.drawDoc.DrawObjectList[i].DrawMultiFactor = multiFactor;
                this.workRegion.picBoxCanvas.Invalidate();
                ChangeDrawRegionLoction();
            }
        }

        private void counter_clw_Click(object sender, EventArgs e)
        {
            if (this.drawDoc.SelectedDrawObjectList.Count > 0)
            {
                this.drawDoc.SelectedDrawObjectList[0].RotateCounterClw();
                this.workRegion.picBoxCanvas.Invalidate();
                this.proPage.propertyGrid1.Refresh();
            }   
        }

        private void clw_Click(object sender, EventArgs e)
        {
            if (this.drawDoc.SelectedDrawObjectList.Count > 0)
            {
                this.drawDoc.SelectedDrawObjectList[0].RotateClw();
                this.workRegion.picBoxCanvas.Invalidate();
                this.proPage.propertyGrid1.Refresh();
            }
        }

        private void drap_Click(object sender, EventArgs e)
        {
            canvasMove = true;
            this.drawCanvas.BackColor = SystemColors.ControlDark;
        }

        private void mouse_Click(object sender, EventArgs e)
        {
            canvasMove = false;
            this.drawCanvas.Enabled = true;
            this.drawCanvas.BackColor = SystemColors.Control;
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            if (this.drawDoc.SelectedDrawObjectList.Count > 0)
            {
                this.drawDoc.SelectedDrawObjectList[0].ObjectMirror();
                this.workRegion.picBoxCanvas.Invalidate();
            }
        }

        private void addtext_Click(object sender, EventArgs e)
        {
    //        BaseRailElement.RailLabal railLalal = new BaseRailElement.RailLabal();
    ////        railLalal.CreatEle(multiFactor, this.tools.itemSelected.Text);
    ////        railLalal.CreatEle(multiFactor, null);
    ////        this.drawDoc.DrawObjectList.Add(railLalal.CreatEle(multiFactor, this.tools.itemSelected.Text));
    //        this.drawDoc.DrawObjectList.Add(railLalal.CreatEle(multiFactor, null));
    //        drawDoc.SelectOne(railLalal);
    //        proRegion.AddElementNode(this.workRegion.Text, railLalal.railText);
    //        this.workRegion.pictureBox1.Invalidate();
    //        proPage.propertyGrid1.SelectedObject = railLalal;
    //        proPage.propertyGrid1.Refresh();
        }
        #endregion

        private void SaveFile(SaveFileDialog sFile)
        {
            if (sFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string projectpath = sFile.FileName;
                    string projectcodingpath = projectpath.Substring(0, projectpath.Length - 4) + "_coding.xml";
                    drawDoc.DataXmlSave();
                    drawDoc.DsEle.WriteXml(projectpath);
                    drawDoc.DsEleCoding.WriteXml(projectcodingpath);
                    sProjectPath = projectpath;
                    UpdateFormTitle();
                }
                catch
                {
                    MessageBox.Show("save error");
                }
            }
        }

        private bool OpenXmlFile(DataSet ds)
        {
            Mcs.RailSystem.Common.DrawDoc doc = new Mcs.RailSystem.Common.DrawDoc();
            DataTable dt = ds.Tables[0];
            doc.InitDataTable(dt);

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (Convert.ToInt16(dt.Rows[i][0]))
                    {
                        case 1:
                            RailEleLine line = new RailEleLine();
                            doc.ReadDataFromRow(i, line);
                            AddElement(line);
                            break;
                        case 2:
                            RailEleCurve curve = new RailEleCurve();
                            doc.ReadDataFromRow(i, curve);
                            AddElement(curve);
                            break;
                        case 3:
                            RailEleCross cross = new RailEleCross();
                            doc.ReadDataFromRow(i, cross);
                            AddElement(cross);
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("this is a error when open xml save file");
                return false;
            }
            return true;
        }

        private void EnlargeAndShortenCanvas(Int16 drawMulti)
        {
            int n = this.drawDoc.DrawObjectList.Count;
            for (int i = 0; i < n; i++)
                this.drawDoc.DrawObjectList[i].DrawMultiFactor = drawMulti;
            this.workRegion.picBoxCanvas.Invalidate();
            ChangeDrawRegionLoction();
        }

        private void ChangeDrawRegionLoction()
        {
            Point drawRegionLoc = Point.Empty;
            Point drawRegionSize = (Point)this.workRegion.picBoxCanvas.Size;
            Point workRegSize = (Point)this.workRegion.panelCanvas.Size;
            Point centerDrawRegion = new Point(drawRegionSize.X / 2, drawRegionSize.Y / 2);
            Point centerWorkRegSize = new Point(workRegSize.X / 2, workRegSize.Y / 2);
            int dx = centerWorkRegSize.X - centerDrawRegion.X;
            int dy = centerWorkRegSize.Y - centerDrawRegion.Y;
            drawRegionLoc.Offset(dx, dy);
            this.workRegion.picBoxCanvas.Location = drawRegionLoc;
        }

        public void CreateElement(Point mousePt, Size workRegionSize)
        {
            string str = this.tools.itemSelected.Text;
            switch (this.tools.itemSelected.Text)
            {
                case "Line":
                    BaseRailElement.RailEleLine line = new BaseRailElement.RailEleLine();
                    ++lineNumber;
                    str += "_" + lineNumber.ToString();
                    line.CreateEle(mousePt, workRegionSize, multiFactor, str);
                    AddElement(line);
                    drawDoc.SelectOne(line);
                    workRegion.picBoxCanvas.Invalidate();
                    proPage.propertyGrid1.SelectedObject = line;
                    proPage.propertyGrid1.Refresh();
                    break;
                case "Curve":
                    BaseRailElement.RailEleCurve curve = new BaseRailElement.RailEleCurve();
                    ++curveNumber;
                    str += "_" + curveNumber.ToString();
                    curve.CreateEle(mousePt, workRegionSize, multiFactor, str);
                    AddElement(curve);
                    drawDoc.SelectOne(curve);
                    workRegion.picBoxCanvas.Invalidate();
                    proPage.propertyGrid1.SelectedObject = curve;
                    proPage.propertyGrid1.Refresh();
                    break;
                case "Cross":
                    BaseRailElement.RailEleCross cross = new BaseRailElement.RailEleCross();
                    ++crossNumber;
                    str += "_" + crossNumber.ToString();
                    cross.CreateEle(mousePt, workRegionSize, multiFactor, str);
                    AddElement(cross);
                    drawDoc.SelectOne(cross);
                    workRegion.picBoxCanvas.Invalidate();
                    proPage.propertyGrid1.SelectedObject = cross;
                    proPage.propertyGrid1.Refresh();
                    break;
                case "Device":
                    MessageBox.Show("a");
                    break;
                default:
                    break;
            }
            this.tools.itemSelected = null;
        }

        private void AddElement(Mcs.RailSystem.Common.BaseRailEle baseRailEle)
        {
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
                    drawDoc.DrawObjectList.Add(line);
                    proRegion.AddElementNode(this.workRegion.Text, line.railText);
                    break;
                case "Curve":
                    Mcs.RailSystem.Common.EleCurve curve = (Mcs.RailSystem.Common.EleCurve)baseRailEle;
                    drawDoc.DrawObjectList.Add(curve);
                    proRegion.AddElementNode(this.workRegion.Text, curve.railText);
                    break;
                case "Cross":
                    Mcs.RailSystem.Common.EleCross cross = (Mcs.RailSystem.Common.EleCross)baseRailEle;
                    drawDoc.DrawObjectList.Add(cross);
                    proRegion.AddElementNode(this.workRegion.Text, cross.railText);
                    break;
            }
        }

        public void SelectedElement(Int16 index)
        {
            drawDoc.SelectedDrawObjectList.Clear();
            if (index >= 0)
            {
                drawDoc.SelectedDrawObjectList.Add(drawDoc.DrawObjectList[index]);
                this.proRegion.treeView1.SelectedNode = this.proRegion.treeNodeList[index];
                this.proPage.propertyGrid1.SelectedObject = drawDoc.SelectedDrawObjectList[0];
            }
            else
            {
                this.proPage.propertyGrid1.SelectedObject = null;
            }
            this.proPage.propertyGrid1.Refresh();
            this.workRegion.picBoxCanvas.Invalidate();
        }

        public void CutElement()
        {
        }

        public void CopyElement()
        {
            this.drawDoc.Copy();
        }

        public void PasteElement()
        {
            for (Int16 i = 0; i < drawDoc.CutAndCopyObjectList.Count; )
            {
                string str = drawDoc.CutAndCopyObjectList[0].railText;
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
                this.proRegion.AddElementNode(this.workRegion.Text, str);
                this.drawDoc.Paste(str);
                this.workRegion.picBoxCanvas.Invalidate();
                this.proPage.propertyGrid1.SelectedObject = drawDoc.SelectedDrawObjectList[0];
            }
        }

        public void DeleteElement()
        {
            for (Int16 i = 0; i < drawDoc.SelectedDrawObjectList.Count; )
            {
                Int16 num = Convert.ToInt16(drawDoc.DrawObjectList.IndexOf(drawDoc.SelectedDrawObjectList[0]));
                this.proRegion.DeleteElementNode(this.workRegion.Text, num);
                this.drawDoc.Delete(num);
                this.workRegion.picBoxCanvas.Invalidate();
            }
        }

        public void WorkRegionKeyMove(Keys key)
        {
            BaseRailElement.ObjectBaseEvents.Direction direction = BaseRailElement.ObjectBaseEvents.Direction.Null;
            switch (key)
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
            objectEvent.WorkRegionKeyDown(direction);
            this.workRegion.picBoxCanvas.Invalidate();
        }
    }
}
