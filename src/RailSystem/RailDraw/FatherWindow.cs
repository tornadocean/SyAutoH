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
        public BaseRailElement.DrawDoc drawDoc = new BaseRailElement.DrawDoc();
        public BaseRailElement.ObjectBaseEvents objectEvent = new BaseRailElement.ObjectBaseEvents();
        private bool mouseIsDown = false;
        private bool drapIsDown = false;
        private string sProjectPath = "";
        private Size drawregOrigSize = new Size();
        private const Int16 CONST_MULTI_FACTOR = 1;
        private Int16 multiFactor = 1;
        private Point workSize = Point.Empty;
        private Int16 lineNumber = 100;
        private Int16 curveNumber = 200;
        private Int16 CrossNumber = 300;

        //using for test
        SaveCodingRail saveCodingRail = new SaveCodingRail();

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

            drawregOrigSize.Width = this.workRegion.pictureBox1.Width;
            drawregOrigSize.Height = this.workRegion.pictureBox1.Height;
        }

        #region  workRegion Operation

        public void PicMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    objectEvent.OnLButtonDown(e.Location);
                    mouseIsDown = true;
                    break;
                case MouseButtons.Right:
                    this.workRegion.contextMenuStripWorkReg.Items.Clear();
                    this.workRegion.ContextMenuStripCreate(objectEvent.OnRButtonDown(e.Location));
                    mouseIsDown = true;
                    break;
            }
            this.workRegion.pictureBox1.Invalidate();
        }

        public void PicMouseUp(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                mouseIsDown = false;
            }
            if (drapIsDown)
            {
                Point pt_offset = objectEvent.DrapDrawRegion(e.Location);
                Point picLoc = this.workRegion.pictureBox1.Location;
                this.workRegion.pictureBox1.Location = new Point(picLoc.X + pt_offset.X, picLoc.Y + pt_offset.Y);
                drapIsDown = true;
            }
            objectEvent.OnLButtonUp(e.Location);
            this.workRegion.pictureBox1.Invalidate();
            this.workRegion.pictureBox1.Focus();
        }

        public void PicMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown && !drapIsDown)
            {
                objectEvent.OnMouseMove(e.Location);
                this.workRegion.pictureBox1.Invalidate();
            }
        }

        public void PicMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (drawDoc.SelectedDrawObjectList.Count == 1)
                {
                    BaseRailElement.BaseRailEle baseEle = drawDoc.SelectedDrawObjectList[0];
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
            this.workRegion.pictureBox1.Invalidate();
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
                foreach (BaseRailElement.BaseRailEle o in this.drawDoc.DrawObjectList)
                    this.drawDoc.SelectedDrawObjectList.Add(o);
            }
            this.workRegion.pictureBox1.Invalidate();
        }

        private void menupercentone_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ToolStripMenuItem)
            {
                System.Windows.Forms.ToolStripMenuItem item = (System.Windows.Forms.ToolStripMenuItem)sender;
                System.Windows.Forms.ToolStrip parent = item.GetCurrentParent();
                int i = parent.Items.IndexOf(item);
                Int16 multi = Convert.ToInt16(i + 1);
                this.workRegion.pictureBox1.Width = drawregOrigSize.Width * multi;
                this.workRegion.pictureBox1.Height = drawregOrigSize.Height * multi;
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
                        this.workRegion.pictureBox1.Invalidate();
                        this.proRegion.Invalidate();
                        break;
                    case DialogResult.No:
                        drawDoc.DrawObjectList.Clear();
                        drawDoc.SelectedDrawObjectList.Clear();
                        proRegion.treeNodeList.Clear();
                        proRegion.treeView1.Nodes[0].Nodes.Clear();
                        this.workRegion.pictureBox1.Invalidate();
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
                this.workRegion.pictureBox1.Top = 0;
                this.workRegion.pictureBox1.Left = 0;
                this.workRegion.pictureBox1.Width = drawregOrigSize.Width;
                this.workRegion.pictureBox1.Height = drawregOrigSize.Height;
                drapIsDown = false;
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
            this.workRegion.pictureBox1.Invalidate();
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
                    drawDoc.dsEle.WriteXml(projectpath);
                    drawDoc.dsEleCoding.WriteXml(projectcodingpath);
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
            this.workRegion.pictureBox1.Invalidate();
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
            if (this.workRegion.pictureBox1.Width < drawregOrigSize.Width * 4)
            {
                this.workRegion.pictureBox1.Width += (drawregOrigSize.Width * CONST_MULTI_FACTOR);
                this.workRegion.pictureBox1.Height += (drawregOrigSize.Height * CONST_MULTI_FACTOR);
                multiFactor = Convert.ToInt16(this.workRegion.pictureBox1.Width / drawregOrigSize.Width);
                this.drawDoc.DrawMultiFactor = multiFactor;
                int n = this.drawDoc.DrawObjectList.Count;
                for (int i = 0; i < n; i++)
                {
                    this.drawDoc.DrawObjectList[i].DrawMultiFactor = multiFactor;
                }
                ChangeDrawRegionLoction();
                this.workRegion.pictureBox1.Invalidate();
            }
        }

        private void shorten_Click(object sender, EventArgs e)
        {
            if (this.workRegion.pictureBox1.Width > drawregOrigSize.Width)
            {
                this.workRegion.pictureBox1.Width -= (drawregOrigSize.Width * CONST_MULTI_FACTOR);
                this.workRegion.pictureBox1.Height -= (drawregOrigSize.Height * CONST_MULTI_FACTOR);
                multiFactor = Convert.ToInt16(this.workRegion.pictureBox1.Width / drawregOrigSize.Width);
                this.drawDoc.DrawMultiFactor = multiFactor;
                int n = this.drawDoc.DrawObjectList.Count;
                for (int i = 0; i < n; i++)
                    this.drawDoc.DrawObjectList[i].DrawMultiFactor = multiFactor;
                this.workRegion.pictureBox1.Invalidate();
                ChangeDrawRegionLoction();
            }
        }

        private void counter_clw_Click(object sender, EventArgs e)
        {
            if (this.drawDoc.SelectedDrawObjectList.Count > 0)
            {
                this.drawDoc.SelectedDrawObjectList[0].RotateCounterClw();
                this.workRegion.pictureBox1.Invalidate();
                this.proPage.propertyGrid1.Refresh();
            }   
        }

        private void clw_Click(object sender, EventArgs e)
        {
            if (this.drawDoc.SelectedDrawObjectList.Count > 0)
            {
                this.drawDoc.SelectedDrawObjectList[0].RotateClw();
                this.workRegion.pictureBox1.Invalidate();
                this.proPage.propertyGrid1.Refresh();
            }
        }

        private void drap_Click(object sender, EventArgs e)
        {
            drapIsDown = true;
        }

        private void mouse_Click(object sender, EventArgs e)
        {
            drapIsDown = false;
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            if (this.drawDoc.SelectedDrawObjectList.Count > 0)
            {
                this.drawDoc.SelectedDrawObjectList[0].ObjectMirror();
                this.workRegion.pictureBox1.Invalidate();
            }
        }

        private void addtext_Click(object sender, EventArgs e)
        {
            BaseRailElement.RailLabal railLalal = new BaseRailElement.RailLabal();
    //        railLalal.CreatEle(multiFactor, this.tools.itemSelected.Text);
    //        railLalal.CreatEle(multiFactor, null);
    //        this.drawDoc.DrawObjectList.Add(railLalal.CreatEle(multiFactor, this.tools.itemSelected.Text));
            this.drawDoc.DrawObjectList.Add(railLalal.CreatEle(multiFactor, null));
            drawDoc.SelectOne(railLalal);
            proRegion.AddElementNode(this.workRegion.Text, railLalal.railText);
            this.workRegion.pictureBox1.Invalidate();
            proPage.propertyGrid1.SelectedObject = railLalal;
            proPage.propertyGrid1.Refresh();
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
                    drawDoc.dsEle.WriteXml(projectpath);
                    drawDoc.dsEleCoding.WriteXml(projectcodingpath);
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
            try
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    DataColumn dc = dt.Columns[0];
                    if (dc.ColumnName == "GraphType")
                    {
                        switch (dt.Rows[i][0].ToString())
                        {
                            case "1":
                                BaseRailElement.StraightRailEle strTemp = new BaseRailElement.StraightRailEle();
                                string str = "";
                                string[] strPointArray = { };
                                Point ptTemp = Point.Empty;
                                Int16 pointListVolStr = 0;
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    switch (dt.Columns[j].ColumnName)
                                    {
                                        case "GraphType":
                                            strTemp.GraphType = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "Speed":
                                            strTemp.Speed = Convert.ToSingle(dt.Rows[i][j]);
                                            break;
                                        //case "SegmentNumber":
                                        //    strTemp.SegmentNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        //    break;
                                        //case "TagNumber":
                                        //    strTemp.TagNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        //    break;
                                        case "Lenght":
                                            strTemp.Lenght = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "StartAngle":
                                            strTemp.StartAngle = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        //case "StartDot":
                                        //    strTemp.StartDot = Convert.ToString(dt.Rows[i][j]);
                                        //    break;
                                        case "PointListVol":
                                            pointListVolStr = Convert.ToInt16(dt.Rows[i][j]);
                                            for (int k = 0; k < pointListVolStr; k++)
                                            {
                                                str = dt.Rows[i][j + k + 1].ToString();
                                                str = str.Substring(1, str.Length - 2);
                                                strPointArray = str.Split(',');
                                                ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                                strTemp.PointList.Add(ptTemp);
                                            }
                                            break;
                                        case "DrawMultiFactor":
                                            strTemp.DrawMultiFactor = Convert.ToInt16(dt.Rows[i][j]);
                                            break;
                                        case "startPoint":
                                            str = dt.Rows[i][j].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            strPointArray = str.Split(',');
                                            ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                            strTemp.StartPoint = ptTemp;
                                            break;
                                        case "endPoint":
                                            str = dt.Rows[i][j].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            strPointArray = str.Split(',');
                                            ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                            strTemp.EndPoint = ptTemp;
                                            break;
                                        case "railText":
                                            strTemp.railText = dt.Rows[i][j].ToString();
                                            break;
                                        case "rotateAngle":
                                            strTemp.RotateAngle = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingBegin":
                                            strTemp.CodingBegin = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingEnd":
                                            strTemp.CodingEnd = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingNext":
                                            strTemp.CodingNext = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "prevCoding":
                                            strTemp.CodingPrev = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "Color":
                                            strTemp.PenColor = ColorTranslator.FromHtml(dt.Rows[i][j].ToString());
                                            break;
                                        case "DashStyle":
                                            strTemp.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dt.Rows[i][j]));
                                            break;
                                        case "PenWidth":
                                            strTemp.PenWidth = Convert.ToSingle(dt.Rows[i][j]);
                                            break;
                                    }
                                }
                                AddElement(strTemp);
                                break;
                            case "2":
                                BaseRailElement.CurvedRailEle curTemp = new BaseRailElement.CurvedRailEle();
                                string strcur = "";
                                string[] strPointArrayCur = { };
                                Point ptcur = Point.Empty;
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    switch (dt.Columns[j].ColumnName)
                                    {
                                        case "GraphType":
                                            curTemp.GraphType = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "Speed":
                                            curTemp.Speed = Convert.ToSingle(dt.Rows[i][j]);
                                            break;
                                        //case "SegmentNumber":
                                        //    curTemp.SegmentNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        //    break;
                                        //case "TagNumber":
                                        //    curTemp.TagNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        //    break;
                                        case "StartAngle":
                                            curTemp.StartAngle = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "SweepAngle":
                                            curTemp.SweepAngle = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "Radiu":
                                            curTemp.Radiu = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "Center":
                                            strcur = dt.Rows[i][j].ToString();
                                            strcur = strcur.Substring(1, strcur.Length - 2);
                                            strPointArrayCur = strcur.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.Center = ptcur;
                                            break;
                                        case "FirstDot":
                                            strcur = dt.Rows[i][j].ToString();
                                            strcur = strcur.Substring(1, strcur.Length - 2);
                                            strPointArrayCur = strcur.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.FirstDot = ptcur;
                                            break;
                                        case "SecDot":
                                            strcur = dt.Rows[i][j].ToString();
                                            strcur = strcur.Substring(1, strcur.Length - 2);
                                            strPointArrayCur = strcur.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.SecDot = ptcur;
                                            break;
                                        case "DirectionCurvedAttribute":
                                            curTemp.DirectionCurvedAttribute = (BaseRailElement.CurvedRailEle.DirectonCurved)Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "startPoint":
                                            str = dt.Rows[i][j].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            strPointArrayCur = str.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.StartPoint = ptcur;
                                            break;
                                        case "endPoint":
                                            str = dt.Rows[i][j].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            strPointArrayCur = str.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.EndPoint = ptcur;
                                            break;
                                        case "CodingBegin":
                                            curTemp.CodingBegin = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingEnd":
                                            curTemp.CodingEnd = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingNext":
                                            curTemp.CodingNext = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingPrev":
                                            curTemp.CodingPrev = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "railText":
                                            curTemp.railText = dt.Rows[i][j].ToString();
                                            break;
                                        case "rotateAngle":
                                            curTemp.RotateAngle = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "oldRadiu":
                                            curTemp.oldRadiu = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "oldCenter":
                                            str = dt.Rows[i][j].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            strPointArrayCur = str.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.oldCenter = ptcur;
                                            break;
                                        case "oldFirstDot":
                                            str = dt.Rows[i][j].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            strPointArrayCur = str.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.oldFirstDot = ptcur;
                                            break;
                                        case "oldSecDot":
                                            str = dt.Rows[i][j].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            strPointArrayCur = str.Split(',');
                                            ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                            curTemp.oldSecDot = ptcur;
                                            break;
                                        case "Color":
                                            curTemp.PenColor = ColorTranslator.FromHtml(dt.Rows[i][j].ToString());
                                            break;
                                        case "DashStyle":
                                            curTemp.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dt.Rows[i][j]));
                                            break;
                                        case "PenWidth":
                                            curTemp.PenWidth = Convert.ToSingle(dt.Rows[i][j]);
                                            break;
                                    }
                                }
                                AddElement(curTemp);
                                break;
                            case "3":
                                BaseRailElement.CrossEle croTemp = new BaseRailElement.CrossEle();
                                string strcro = "";
                                string[] strPointArrayCro = { };
                                Point ptcro = Point.Empty;
                                Int16 pointListVolCro = 0;
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    switch (dt.Columns[j].ColumnName)
                                    {
                                        case "GraphType":
                                            croTemp.GraphType = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "Speed":
                                            croTemp.Speed = Convert.ToSingle(dt.Rows[i][j]);
                                            break;
                                        //case "SegmentNumber":
                                        //    croTemp.SegmentNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        //    break;
                                        //case "TagNumber":
                                        //    croTemp.TagNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        //    break;
                                        case "Mirror":
                                            croTemp.Mirror = Convert.ToBoolean(dt.Rows[i][j]);
                                            break;
                                        case "FirstPart":
                                            croTemp.FirstPart = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "SecPart":
                                            croTemp.SecPart = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "ThPart":
                                            croTemp.ThPart = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "FourPart":
                                            strcro = dt.Rows[i][j].ToString();
                                            strcro = strcro.Substring(1, strcro.Length - 2);
                                            strPointArrayCro = strcro.Split(',');
                                            ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                            croTemp.FourPart = ptcro;
                                            break;
                                        case "StartAngle":
                                            croTemp.StartAngle = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "RotateAngle":
                                            croTemp.RotateAngle = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "DirectionOfCross":
                                            croTemp.DirectionOfCross = (BaseRailElement.CrossEle.DirectionCross)Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "PointListVol":
                                            pointListVolCro = Convert.ToInt16(dt.Rows[i][j]);
                                            for (Int16 k = 0; k < pointListVolCro; k++)
                                            {
                                                strcro = dt.Rows[i][j + k + 1].ToString();
                                                strcro = strcro.Substring(1, strcro.Length - 2);
                                                strPointArrayCro = strcro.Split(',');
                                                ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                                croTemp.PointList.Add(ptcro);
                                            }
                                            break;
                                        case "drawMultiFactor":
                                            croTemp.DrawMultiFactor = Convert.ToInt16(dt.Rows[i][j]);
                                            break;
                                        case "startPoint":
                                            strcro = dt.Rows[i][j].ToString();
                                            strcro = strcro.Substring(1, strcro.Length - 2);
                                            strPointArrayCro = strcro.Split(',');
                                            ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                            croTemp.StartPoint = ptcro;
                                            break;
                                        case "endPoint":
                                            strcro = dt.Rows[i][j].ToString();
                                            strcro = strcro.Substring(1, strcro.Length - 2);
                                            strPointArrayCro = strcro.Split(',');
                                            ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                            croTemp.EndPoint = ptcro;
                                            break;
                                        case "CodingBegin":
                                            croTemp.CodingBegin = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingEnd":
                                            croTemp.CodingEnd = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingEndS":
                                            croTemp.CodingEndS = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingNext":
                                            croTemp.CodingNext = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingPrev":
                                            croTemp.CodingPrev = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "CodingNextS":
                                            croTemp.CodingNextS = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        //case "startDot":
                                        //    croTemp.StartDot = dt.Rows[i][j].ToString();
                                        //    break;
                                        case "railText":
                                            croTemp.railText = dt.Rows[i][j].ToString();
                                            break;
                                        case "lenghtOfStrai":
                                            croTemp.LenghtOfStrai = Convert.ToInt32(dt.Rows[i][j]);
                                            break;
                                        case "Color":
                                            croTemp.PenColor = ColorTranslator.FromHtml(dt.Rows[i][j].ToString());
                                            break;
                                        case "DashStyle":
                                            croTemp.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dt.Rows[i][j]));
                                            break;
                                        case "PenWidth":
                                            croTemp.PenWidth = Convert.ToSingle(dt.Rows[i][j]);
                                            break;
                                    }
                                }
                                AddElement(croTemp);
                                break;
                        }
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
            this.workRegion.pictureBox1.Invalidate();
            ChangeDrawRegionLoction();
        }

        private void ChangeDrawRegionLoction()
        {
            Point drawRegionLoc = Point.Empty;
            Point drawRegionSize = (Point)this.workRegion.pictureBox1.Size;
            Point workRegSize = (Point)this.workRegion.panel1.Size;
            Point centerDrawRegion = new Point(drawRegionSize.X / 2, drawRegionSize.Y / 2);
            Point centerWorkRegSize = new Point(workRegSize.X / 2, workRegSize.Y / 2);
            int dx = centerWorkRegSize.X - centerDrawRegion.X;
            int dy = centerWorkRegSize.Y - centerDrawRegion.Y;
            drawRegionLoc.Offset(dx, dy);
            this.workRegion.pictureBox1.Location = drawRegionLoc;
        }

        public void CreateElement(Point mousePt, Size workRegionSize)
        {
            string str = this.tools.itemSelected.Text;
            switch (this.tools.itemSelected.Text)
            {
                case "Line":
                    BaseRailElement.StraightRailEle strRailEle = new BaseRailElement.StraightRailEle();
                    ++lineNumber;
                    str += "_" + lineNumber.ToString();
                    strRailEle.CreateEle(mousePt, workRegionSize, multiFactor, str);
                    AddElement(strRailEle);
                    drawDoc.SelectOne(strRailEle);
                    workRegion.pictureBox1.Invalidate();
                    proPage.propertyGrid1.SelectedObject = strRailEle;
                    proPage.propertyGrid1.Refresh();
                    break;
                case "Curve":
                    BaseRailElement.CurvedRailEle curRailEle = new BaseRailElement.CurvedRailEle();
                    ++curveNumber;
                    str += "_" + curveNumber.ToString();
                    curRailEle.CreateEle(mousePt, workRegionSize, multiFactor, str);
                    AddElement(curRailEle);
                    drawDoc.SelectOne(curRailEle);
                    workRegion.pictureBox1.Invalidate();
                    proPage.propertyGrid1.SelectedObject = curRailEle;
                    proPage.propertyGrid1.Refresh();
                    break;
                case "Cross":
                    BaseRailElement.CrossEle croRailEle = new BaseRailElement.CrossEle();
                    ++CrossNumber;
                    str += "_" + CrossNumber.ToString();
                    croRailEle.CreateEle(mousePt, workRegionSize, multiFactor, str);
                    AddElement(croRailEle);
                    drawDoc.SelectOne(croRailEle);
                    workRegion.pictureBox1.Invalidate();
                    proPage.propertyGrid1.SelectedObject = croRailEle;
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

        private void AddElement(BaseRailEle baseRailEle)
        {
            string str = baseRailEle.railText;
            int lenght = str.IndexOf('_');
            if (-1!=lenght)
            {
                str = str.Substring(0,lenght);
            }
            switch(str)
            {
                case "Line":
                    BaseRailElement.StraightRailEle strRailEle = (BaseRailElement.StraightRailEle)baseRailEle;
                    drawDoc.DrawObjectList.Add(strRailEle);
                    proRegion.AddElementNode(this.workRegion.Text, strRailEle.railText);
                    break;
                case "Curve":
                    BaseRailElement.CurvedRailEle curRailEle = (BaseRailElement.CurvedRailEle)baseRailEle;
                    drawDoc.DrawObjectList.Add(curRailEle);
                    proRegion.AddElementNode(this.workRegion.Text, curRailEle.railText);
                    break;
                case "Cross":
                    BaseRailElement.CrossEle croRailEle = (BaseRailElement.CrossEle)baseRailEle;
                    drawDoc.DrawObjectList.Add(croRailEle);
                    proRegion.AddElementNode(this.workRegion.Text, croRailEle.railText);
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
            this.workRegion.pictureBox1.Invalidate();
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
                    CrossNumber++;
                    str += "_" + CrossNumber.ToString();
                }
                this.proRegion.AddElementNode(this.workRegion.Text, str);
                this.drawDoc.Paste(str);
                this.workRegion.pictureBox1.Invalidate();
            }
        }

        public void DeleteElement()
        {
            for (Int16 i = 0; i < drawDoc.SelectedDrawObjectList.Count; )
            {
                Int16 num = Convert.ToInt16(drawDoc.DrawObjectList.IndexOf(drawDoc.SelectedDrawObjectList[0]));
                this.proRegion.DeleteElementNode(this.workRegion.Text, num);
                this.drawDoc.Delete(num);
                this.workRegion.pictureBox1.Invalidate();
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
            this.workRegion.pictureBox1.Invalidate();
        }
    }
}
