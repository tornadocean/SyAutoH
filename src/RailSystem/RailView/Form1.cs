using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using GuiAccess;
using WinFormElement;

namespace RailView
{
    public partial class Form1 : Form
    {
        WinFormElement.FormOperation formOperation = new WinFormElement.FormOperation();
        public Form1()
        {
            InitializeComponent();
        }

        private DataHubCli dataHubLink = new DataHubCli();
        private Queue<MCS.GuiDataItem> quGuiData = new Queue<MCS.GuiDataItem>();

        private void InitForm()
        {
            //ComponentLocChanged();        
            formOperation.FormShowRegionInit(this.showPic.Size);
            //test using, finally delete
            TestRailDrawCoor();
            this.showPic.Invalidate();
          //  this.Invalidate();
        }

        private void showPic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            formOperation.ShowRegion(g);
            base.OnPaint(e);
        }

        //test using, finally delete
        public void TestRailDrawCoor()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Interval = 200;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        //test using, finally delete
        public void OnTimer(object source, System.Timers.ElapsedEventArgs e)
        {
            ProcessGuiDataBuf();
            this.showPic.Invalidate();
        }

        private void ProcessGuiDataBuf()
        {
            lock (quGuiData)
            {
                while (quGuiData.Count != 0)
                {
                    MCS.GuiDataItem item = quGuiData.Dequeue();
                    UpdateOHTPosition(item);
                }
            }
        }

        private void UpdateOHTPosition(MCS.GuiDataItem guiDataItem)
        {
                if (guiDataItem.enumTag.CompareTo(MCS.GuiHub.PushData.upOhtPos) != 0)
                {
                    return;
                }
                if (guiDataItem.sVal.Length < 1)
                {
                    return;
                }
                string strVal = guiDataItem.sVal;
               
                string strSplit = "<>";
                char[] spliter = strSplit.ToCharArray();
                string[] strItem = strVal.Split(spliter);

                List<OhtPos> listOht = new List<OhtPos>();
                foreach (string strOht in strItem)
                {
                    if (strOht.Length > 0)
                    {
                        string[] strParams = strOht.Split(',');
                        if (strParams.Length > 1)   //???
                        {
                            OhtPos oht = new OhtPos();
                            oht.nID = Convert.ToByte(strParams[0]);
                            oht.nPos = Convert.ToUInt32(strParams[1]);
                            oht.nHand = Convert.ToByte(strParams[2]);
                            listOht.Add(oht);
                        }
                    }
                }

           
               formOperation.UpdateOHTPos(listOht);
          
               
        }

        private void baseInfoTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            TreeView tempTree = sender as TreeView;
            Point pt = tempTree.PointToClient(Cursor.Position);
            TreeViewHitTestInfo info = tempTree.HitTest(pt.X, pt.Y);
            TreeNode node = info.Node;
            if (node != null && MouseButtons.Right == e.Button)
            {
                tempTree.SelectedNode = node;
                //switch (node.Name)
                //{
                //    case "CarsOnline":
                //        baseInfoMenu.Items.Clear();
                //        baseInfoMenu.Items.Add("add vehicle");
                //        for (int i = 0; i < baseInfoMenu.Items.Count; i++)
                //        {
                //            baseInfoMenu.Items[i].Click += new EventHandler(contextmenu_Click);
                //        }
                //        baseInfoMenu.Show(Cursor.Position.X, Cursor.Position.Y);
                //        break;
                //    case "QueueAssign":
                //        baseInfoMenu.Items.Clear();
                //        for (int i = 0; i < baseInfoMenu.Items.Count; i++)
                //        {
                //            baseInfoMenu.Items[i].Click += new EventHandler(contextmenu_Click);
                //        }
                //        baseInfoMenu.Show(Cursor.Position.X, Cursor.Position.Y);
                //        break;
                //    case "Alarm":
                //        baseInfoMenu.Items.Clear();
                //        for (int i = 0; i < baseInfoMenu.Items.Count; i++)
                //        {
                //            baseInfoMenu.Items[i].Click += new EventHandler(contextmenu_Click);
                //        }
                //        baseInfoMenu.Show(Cursor.Position.X, Cursor.Position.Y);
                //        break;
                //    default:
                //        break;
                //}
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitForm();

            dataHubLink.ConnectServer();
            dataHubLink.DataUpdater += new DataUpdaterHander(GuiDataUpdate);
            dataHubLink.Async_SetCallBack();
            MCS.GuiHub.PushData[] cmds = 
                new MCS.GuiHub.PushData[] 
                { 
                    MCS.GuiHub.PushData.upOhtPos
                };
            dataHubLink.Async_SetPushCmdList(cmds);
        }

        private void GuiDataUpdate(long lTime, MCS.GuiDataItem guiData)
        {
            lock (quGuiData)
            {
                quGuiData.Enqueue(guiData);
            }
        }

        private void quitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Want to Quit?", "Warnning", MessageBoxButtons.OKCancel) )
            {
                this.Close();
            }
        }

        private void screenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (FormBorderStyle.None == this.FormBorderStyle)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.screenToolStripMenuItem.Text = "Full Screen";
                this.screenToolStripMenuItem.Image = global::RailView.Properties.Resources.full_screen2;
            }
            else
            {
                this.TopMost = true;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.screenToolStripMenuItem.Text = "Normal Screen";
                this.screenToolStripMenuItem.Image = global::RailView.Properties.Resources.normal_screen2;
            }
        }

        private void showPic_Resize(object sender, EventArgs e)
        {
            formOperation.AdjustCanvasSize(this.showPic.Size);
        }
        #region //set navigation keys

        private void btn_up_Layout(object sender, LayoutEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            List<Point> arrPoints = new List<Point>();
            arrPoints.Add(new Point(1, 18));
            arrPoints.Add(new Point(19, 18));
            arrPoints.Add(new Point(10, 9));
            gp.AddLines(arrPoints.ToArray());
            gp.CloseFigure();
            btn_up.Region = new Region(gp);
            this.btn_up.BackColor = Color.Gray;
        }

        private void btn_down_Layout(object sender, LayoutEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            List<Point> arrPoints = new List<Point>();
            arrPoints.Add(new Point(1, 2));
            arrPoints.Add(new Point(19, 2));
            arrPoints.Add(new Point(10, 11));
            gp.AddLines(arrPoints.ToArray());
            gp.CloseFigure();
            btn_down.Region = new Region(gp);
            this.btn_down.BackColor = Color.Gray;
        }

        private void btn_left_Layout(object sender, LayoutEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            List<Point> arrPoints = new List<Point>();
            arrPoints.Add(new Point(18, 1));
            arrPoints.Add(new Point(18, 19));
            arrPoints.Add(new Point(9, 9));
            gp.AddLines(arrPoints.ToArray());
            gp.CloseFigure();
            btn_left.Region = new Region(gp);
            this.btn_left.BackColor = Color.Gray;
        }

        private void btn_right_Layout(object sender, LayoutEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            List<Point> arrPoints = new List<Point>();
            arrPoints.Add(new Point(2, 1));
            arrPoints.Add(new Point(2, 19));
            arrPoints.Add(new Point(11, 10));
            gp.AddLines(arrPoints.ToArray());
            gp.CloseFigure();
            btn_right.Region = new Region(gp);
            this.btn_right.BackColor = Color.Gray;
        }

        private void btn_center_Layout(object sender, LayoutEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            List<Point> arrPoints = new List<Point>();
            arrPoints.Add(new Point(2, 2));
            arrPoints.Add(new Point(2, 18));
            arrPoints.Add(new Point(18, 18));
            arrPoints.Add(new Point(18, 2));
            gp.AddLines(arrPoints.ToArray());
            gp.CloseFigure();
            btn_center.Region = new Region(gp);
            this.btn_center.BackColor = Color.Gray;
        }

        private void btn_up_MouseEnter(object sender, EventArgs e)
        {
            this.btn_up.BackColor = SystemColors.ActiveBorder;
        }

        private void btn_up_MouseLeave(object sender, EventArgs e)
        {
            this.btn_up.BackColor = Color.Gray;
        }

        private void btn_down_MouseEnter(object sender, EventArgs e)
        {
            this.btn_down.BackColor = SystemColors.ActiveBorder;
        }

        private void btn_down_MouseLeave(object sender, EventArgs e)
        {
            this.btn_down.BackColor = Color.Gray;
        }

        private void btn_left_MouseEnter(object sender, EventArgs e)
        {
            this.btn_left.BackColor = SystemColors.ActiveBorder;
        }

        private void btn_left_MouseLeave(object sender, EventArgs e)
        {
            this.btn_left.BackColor = Color.Gray;
        }

        private void btn_right_MouseEnter(object sender, EventArgs e)
        {
            this.btn_right.BackColor = SystemColors.ActiveBorder;
        }

        private void btn_right_MouseLeave(object sender, EventArgs e)
        {
            this.btn_right.BackColor = Color.Gray;
        }

        private void btn_center_MouseEnter(object sender, EventArgs e)
        {
            this.btn_center.BackColor = SystemColors.ActiveBorder;
        }

        private void btn_center_MouseLeave(object sender, EventArgs e)
        {
            this.btn_center.BackColor = Color.Gray;
        }

        #endregion


        #region // navigation keys operation


        private void btn_up_MouseClick(object sender, MouseEventArgs e)
        {
            formOperation.BtnCanvasMove("up");
            this.showPic.Invalidate();
        }

        private void btn_down_MouseClick(object sender, MouseEventArgs e)
        {
            formOperation.BtnCanvasMove("down");
            this.showPic.Invalidate();
        }

        private void btn_left_MouseClick(object sender, MouseEventArgs e)
        {
            formOperation.BtnCanvasMove("left");
            this.showPic.Invalidate();
        }

        private void btn_right_MouseClick(object sender, MouseEventArgs e)
        {
            formOperation.BtnCanvasMove("right");
            this.showPic.Invalidate();
        }

        private void btn_center_MouseClick(object sender, MouseEventArgs e)
        {
            formOperation.BtnCanvasMove("center");
            this.showPic.Invalidate();
        }

        #endregion

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int trackBarValue = trackBar1.Value;
            formOperation.ChangeCanvasScale(trackBarValue);
        }

        private void btn_plus_MouseClick(object sender, MouseEventArgs e)
        {
            if (trackBar1.Value < trackBar1.Maximum)
            {
                this.trackBar1.Value += 1;
                formOperation.ChangeCanvasScale(this.trackBar1.Value);
            }

        }

        private void btn_negative_MouseClick(object sender, MouseEventArgs e)
        {
            if (trackBar1.Value > trackBar1.Minimum)
            {
                this.trackBar1.Value -= 1;
                formOperation.ChangeCanvasScale(this.trackBar1.Value);
            }
        }
    }   
}
