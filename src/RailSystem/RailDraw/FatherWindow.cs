using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
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
        private bool drawToolCreated = false;
        private string sProjectPath = "";
        private Size drawregOrigSize = new Size();
        private const Int16 CONST_MULTI_FACTOR = 1;
        private Int16 multiFactor = 1;
        private Point workSize = Point.Empty;

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


        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!drawToolCreated)
            {
                InitDrawToolsComponent();
                drawToolCreated = true;
            }
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
                        drawDoc.ListTreeNode.Clear();
                        proRegion.treeView1.Nodes[0].Nodes.Clear();
                        this.workRegion.picBoxCanvas.Invalidate();
                        this.proRegion.Invalidate();
                        break;
                    case DialogResult.No:
                        drawDoc.DrawObjectList.Clear();
                        drawDoc.SelectedDrawObjectList.Clear();
                        drawDoc.ListTreeNode.Clear();
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
                drawDoc.ListTreeNode.Clear();
                drawDoc.SelectedDrawObjectList.Clear();
                proRegion.treeView1.Nodes[0].Nodes.Clear();
                this.proRegion.InitTreeView(proRegion.treeView1.Nodes[0]);
                this.workRegion.picBoxCanvas.Top = 0;
                this.workRegion.picBoxCanvas.Left = 0;
                this.workRegion.picBoxCanvas.Width = drawregOrigSize.Width;
                this.workRegion.picBoxCanvas.Height = drawregOrigSize.Height;
                this.workRegion.MouseLMove = false;
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
                    Debug.WriteLine(string.Format("error is: {0}", ex));
                }
            }
        }

        private void cut_Click(object sender, EventArgs e)
        {
            this.workRegion.CutElement();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            this.workRegion.CopyElement();
        }

        private void paste_Click(object sender, EventArgs e)
        {
            this.workRegion.PasteElement();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            this.workRegion.DeleteElement();
        }

        private void enlarge_Click(object sender, EventArgs e)
        {
            if (this.workRegion.picBoxCanvas.Width < drawregOrigSize.Width * 4)
            {
                this.workRegion.picBoxCanvas.Width += (drawregOrigSize.Width * CONST_MULTI_FACTOR);
                this.workRegion.picBoxCanvas.Height += (drawregOrigSize.Height * CONST_MULTI_FACTOR);
                multiFactor = Convert.ToInt16(this.workRegion.picBoxCanvas.Width / drawregOrigSize.Width);
                this.workRegion.MultiFactor = Convert.ToInt16(this.workRegion.picBoxCanvas.Width / drawregOrigSize.Width);
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
                this.workRegion.MultiFactor = Convert.ToInt16(this.workRegion.picBoxCanvas.Width / drawregOrigSize.Width);
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
            this.workRegion.MouseLMove = true;
            this.drawCanvas.BackColor = SystemColors.ControlDark;
        }

        private void mouse_Click(object sender, EventArgs e)
        {
            this.workRegion.MouseLMove = false;
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
    //        railLalal.CreatEle(multiFactor, this.tools.itemSelected.Text);
    //        railLalal.CreatEle(multiFactor, null);
    //        this.drawDoc.DrawObjectList.Add(railLalal.CreatEle(multiFactor, this.tools.itemSelected.Text));
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
                            this.workRegion.AddElement(line);
                            this.workRegion.LineNumber++;
                            break;
                        case 2:
                            RailEleCurve curve = new RailEleCurve();
                            doc.ReadDataFromRow(i, curve);
                            this.workRegion.AddElement(curve);
                            this.workRegion.CurveNumber++;
                            break;
                        case 3:
                            RailEleCross cross = new RailEleCross();
                            doc.ReadDataFromRow(i, cross);
                            this.workRegion.AddElement(cross);
                            this.workRegion.CrossNumber++;
                            break;
                        case 5:
                            RailEleFoupDot foupDot = new RailEleFoupDot();
                            doc.ReadDataFromRow(i, foupDot);
                            this.workRegion.AddElement(foupDot);
                            this.workRegion.FoupDotNumber++;
                            break;
                        case 6:
                            RailEleDevice device = new RailEleDevice();
                            doc.ReadDataFromRow(i, device);
                            this.workRegion.AddElement(device);
                            this.workRegion.DeviceNumber++;
                            break;
                    }
                }
                
                foreach (Mcs.RailSystem.Common.BaseRailEle obj in drawDoc.DrawObjectList)
                {
                    if (5 == obj.GraphType)
                    {
                        RailEleFoupDot dot = (RailEleFoupDot)obj;
                        foreach (Mcs.RailSystem.Common.BaseRailEle objchild in drawDoc.DrawObjectList)
                        {
                            if (6 == objchild.GraphType && ((RailEleDevice)objchild).DeviecID == dot.DeviceNum)
                            {
                                ((RailEleDevice)objchild).ListFoupDot.Add(dot);
                                if (((RailEleDevice)objchild).FoupDotFirst.railText == dot.railText)
                                {
                                    ((RailEleDevice)objchild).FoupDotFirst = dot;
                                }
                                break;
                            }
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
            this.workRegion.MultiFactor = drawMulti;
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

        private void InitDrawToolsComponent()
        {
            ToolStripButton toolStripBtnRectangle = new ToolStripButton();
            ToolStripButton toolStripBtnEllipse = new ToolStripButton();
            ToolStripButton toolStripBtnLine = new ToolStripButton();
            ToolStripButton toolStripBtnCurve = new ToolStripButton();
            ToolStripButton toolStripBtnCurSor = new ToolStripButton();
            ToolStrip toolStripDraw = new ToolStrip();

            toolStripBtnRectangle.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnRectangle.Image = global::RailDraw.Properties.Resources.rectange;
            toolStripBtnRectangle.ImageTransparentColor = Color.Magenta;
            toolStripBtnRectangle.Name = "toolStripBtnRectangle";
            toolStripBtnRectangle.Size = new System.Drawing.Size(23, 22);
            toolStripBtnRectangle.Text = "rectangle";
            toolStripBtnRectangle.Click += new System.EventHandler(toolStripDrawTool_Click);

            toolStripBtnEllipse.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnEllipse.Image = global::RailDraw.Properties.Resources.ellipse;
            toolStripBtnEllipse.ImageTransparentColor = Color.Magenta;
            toolStripBtnEllipse.Name = "toolStripBtnEllipse";
            toolStripBtnEllipse.Size = new System.Drawing.Size(23, 22);
            toolStripBtnEllipse.Text = "ellipse";
            toolStripBtnEllipse.Click += new System.EventHandler(toolStripDrawTool_Click);

            toolStripBtnLine.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnLine.Image = global::RailDraw.Properties.Resources.drawLine;
            toolStripBtnLine.ImageTransparentColor = Color.Magenta;
            toolStripBtnLine.Name = "toolStripBtnLine";
            toolStripBtnLine.Size = new System.Drawing.Size(23, 22);
            toolStripBtnLine.Text = "line";
            toolStripBtnLine.Click += new System.EventHandler(toolStripDrawTool_Click);

            toolStripBtnCurve.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnCurve.Image = global::RailDraw.Properties.Resources.drawCurve;
            toolStripBtnCurve.ImageTransparentColor = Color.Magenta;
            toolStripBtnCurve.Name = "toolStripBtnCurve";
            toolStripBtnCurve.Size = new System.Drawing.Size(23, 22);
            toolStripBtnCurve.Text = "curve";
            toolStripBtnCurve.Click += new System.EventHandler(toolStripDrawTool_Click);

            toolStripBtnCurSor.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnCurSor.Image = global::RailDraw.Properties.Resources.arrow;
            toolStripBtnCurSor.ImageTransparentColor = Color.Magenta;
            toolStripBtnCurSor.Name = "toolStripBtnCurSor";
            toolStripBtnCurSor.Size = new System.Drawing.Size(23, 22);
            toolStripBtnCurSor.Text = "arrow";
            toolStripBtnCurSor.Click += new System.EventHandler(toolStripDrawTool_Click);

            toolStripDraw.Items.AddRange(new System.Windows.Forms.ToolStripItem[]{
            toolStripBtnRectangle,
            toolStripBtnEllipse,
            toolStripBtnLine,
            toolStripBtnCurve,
            toolStripBtnCurSor});
            toolStripDraw.TabIndex = 0;
            toolStripDraw.Size = new System.Drawing.Size(140, 22);

            Form drawForm = new Form();
            drawForm.StartPosition = FormStartPosition.Manual;
            drawForm.Owner = this.FindForm();
            Point pt = Point.Empty;
            pt.Offset(10, 10);
            drawForm.Location = pt;
            drawForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            drawForm.ClientSize = toolStripDraw.Size;
            drawForm.Text = "Draw";
            drawForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(DrawToolForm_Closing);
            drawForm.Controls.Add(toolStripDraw);
            toolStripDraw.ResumeLayout(false);
            toolStripDraw.PerformLayout();
            drawForm.ResumeLayout(false);
            drawForm.PerformLayout();
            drawForm.Show(this);


        }

        private void toolStripDrawTool_Click(object sender, EventArgs e)
        {

            ToolStripItem item = sender as ToolStripItem;
            if (item != null)
            {
                switch (item.Text)
                {
                    case "rectangle":
                        objectEvent.DrawToolType = 0;
                        break;
                    case "ellipse":
                        objectEvent.DrawToolType = 1;
                        break;
                    case "line":
                        objectEvent.DrawToolType = 2;
                        break;
                    case "curve":
                        objectEvent.DrawToolType = 3;
                        break;
                    case "arrow":
                        objectEvent.DrawToolType = 4;
                        break;
                }
                this.drawDoc.SelectedDrawObjectList.Clear();
                this.drawDoc.LastHitedObject = null;
            }
            this.workRegion.picBoxCanvas.Invalidate();
        }

        private void DrawToolForm_Closing(object sender, FormClosingEventArgs e)
        {
            drawToolCreated = false;
        }

        private void importDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objectEvent.ImportKeyDotFromDB();
            this.proRegion.RefreshTreeView();
        }

        private void importPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image imageExPic;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "picture film(*.jpg,*.gif,*.bmp,*.png)|*.jpg;*.gif;*.bmp;*.png";
            openFile.InitialDirectory = "";
            openFile.Title = "open pic file";
            openFile.FileName = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string openPath = openFile.FileName;
                try
                {
                    imageExPic = Image.FromFile(openPath);
                }
                catch
                {
                    MessageBox.Show("import picture failed");
                    return;
                }
                //Point pt = this.workRegion.picBoxCanvas.Location;
                //pt.Offset(5, 5);
                Point pt=Point.Empty;
                this.workRegion.CreateUserDefinedEle(pt, this.workRegion.picBoxCanvas.Size, imageExPic);
            }
        }

    }
}
