using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;

namespace RailDraw
{
    public partial class Tools : DockContent
    {
        private bool picLine = false;
        public bool winShown = false;
        public ListViewItem itemSelected = null;

        public bool PicLine
        {
            get { return picLine; }
        }

        public Tools()
        {
            InitializeComponent();
        }

        private void Tools_Load(object sender, EventArgs e)
        {
            RefreshToolImageList();
            InitTools(sender, e);
        }

        public void InitTools(object sender, EventArgs e)
        {
            eleBtn.Show();
            others.Show();
            eleBtn_Click(sender, e);
        }

        private void Tools_Shown(object sender, EventArgs e)
        {
            winShown = true;
        }

        private void Tools_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            winShown = false;
        }

        [DllImport("user32")]
        private static extern IntPtr LoadCursorFromFile(string fileName);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr h);

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockPanel.Parent;
            if (e.Button == MouseButtons.Left)
            {
                itemSelected = listView1.GetItemAt(e.X, e.Y);
                if (itemSelected != null
                    && e.Button == MouseButtons.Left
                    && !father.objectEvent.MouseLMove
                    && 4 == father.objectEvent.DrawToolType)
                {
                    this.Cursor = CommonFunction.CreatCursor("draw");
                    picLine = true;
                    this.listView1.MouseLeave += new EventHandler(listView1_MouseLeave);
                }
                else if (father.objectEvent.MouseLMove
                    || 4 != father.objectEvent.DrawToolType)
                {
                    this.Cursor = Cursors.No;
                }
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.listView1.MouseLeave -= new EventHandler(listView1_MouseLeave);
                this.Cursor = System.Windows.Forms.Cursors.Default;
                picLine = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenuStripTool.Items.Clear();
                contextMenuStripTool.Items.Add("refresh");
                for (Int16 i = 0; i < contextMenuStripTool.Items.Count; i++)
                {
                    contextMenuStripTool.Items[i].Click += new EventHandler(RefreshToolItems);
                }
                contextMenuStripTool.Show(Cursor.Position);
            }
        }

        private void RefreshToolItems(object sender, EventArgs e)
        {
            RefreshToolImageList();
            eleBtn_Click(sender, e);
        }

        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockPanel.Parent;
            this.listView1.MouseLeave -= new EventHandler(listView1_MouseLeave);
            ReleaseCapture();
            SetCapture(father.workRegion.picBoxCanvas.Handle);
            father.workRegion.picBoxCanvas.MouseUp +=
                new MouseEventHandler(father.workRegion.ElementDraw_MouseUp);
        }

        private void eleBtn_Click(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.None;
            eleBtn.Dock = DockStyle.Top;
            others.Dock = DockStyle.Bottom;
            listView1.BringToFront();
            listView1.Dock = DockStyle.Fill;
            listView1.Clear();

            ListViewItem item = new ListViewItem();
            item.Text = "Line";
            item.ImageKey = "line";
            listView1.Items.Add(item);

            ListViewItem item1 = new ListViewItem();
            item1.Text = "Curve";
            item1.ImageKey = "curve";
            listView1.Items.Add(item1);

            ListViewItem item2 = new ListViewItem();
            item2.Text = "Cross";
            item2.ImageKey = "cross";
            listView1.Items.Add(item2);  
        }

        private void others_Click(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.None;
            eleBtn.Dock = DockStyle.Top;
            others.Dock = DockStyle.Top;
            listView1.BringToFront();
            listView1.Dock = DockStyle.Fill;
            listView1.Clear();
            int num = toolImageList.Images.Count;

            ListViewItem item3 = new ListViewItem();
            item3.Text = "FoupWay";
            item3.ImageKey = "foupWay";
            listView1.Items.Add(item3);

            ListViewItem item4 = new ListViewItem();
            item4.Text = "Device";
            item4.ImageKey = "device";
            listView1.Items.Add(item4);
            
            for (int i = 5; i < num; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = toolImageList.Images.Keys[i];
                item.ImageIndex = i;
                listView1.Items.Add(item);
            }
        }

        public void ClearTool()
        {
            listView1.Dock = DockStyle.None;
            eleBtn.Hide();
            others.Hide();
            listView1.Dock = DockStyle.Fill;
            listView1.Clear();
        }

        public void RefreshToolImageList()
        {
            FatherWindow father = (FatherWindow)this.DockPanel.Parent;
            father.drawDocOp.RefreshLastUserDefAdd();
            toolImageList.Images.Clear();
            toolImageList.Images.Add("line", Properties.Resources.line);
            toolImageList.Images.Add("curve", Properties.Resources.curve);
            toolImageList.Images.Add("cross", Properties.Resources.cross);
            toolImageList.Images.Add("foupWay", Properties.Resources.foupWay);
            toolImageList.Images.Add("device", Properties.Resources.devicebig);
            foreach (string str in father.drawDocOp.ListUserDefAdd)
            {
                string strTemp = str.Substring(str.IndexOf("userdef\\") + 8);
                strTemp = strTemp.Substring(0, strTemp.IndexOf(".bmp"));
                toolImageList.Images.Add(strTemp, Properties.Resources.devicebig);
            }
        }

        

    }
}
