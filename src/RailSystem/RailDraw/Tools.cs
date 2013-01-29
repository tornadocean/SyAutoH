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
            toolImageList.Images.Add("line", Properties.Resources.line);
            toolImageList.Images.Add("curve", Properties.Resources.curve);
            toolImageList.Images.Add("cross", Properties.Resources.cross);
            toolImageList.Images.Add("foupWay", Properties.Resources.foupWay);
            toolImageList.Images.Add("device", Properties.Resources.devicebig);
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
            if (e.Button == MouseButtons.Left)
            {
                itemSelected = listView1.GetItemAt(e.X, e.Y);
                if (itemSelected != null
                    && e.Button == MouseButtons.Left 
                    && !((FatherWindow)this.ParentForm).workRegion.MouseLMove)
                {
                    this.Cursor = CommonFunction.CreatCursor("draw");
                    picLine = true;
                    this.listView1.MouseLeave += new EventHandler(listView1_MouseLeave);
                }
                else if (((FatherWindow)this.ParentForm).workRegion.MouseLMove)
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
        }

        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            this.listView1.MouseLeave -= new EventHandler(listView1_MouseLeave);
            ReleaseCapture();
            SetCapture(((FatherWindow)(this.ParentForm)).workRegion.picBoxCanvas.Handle);
            ((FatherWindow)this.ParentForm).workRegion.picBoxCanvas.MouseUp +=
                new MouseEventHandler(((FatherWindow)this.ParentForm).workRegion.ElementDraw_MouseUp);
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

            ListViewItem item3 = new ListViewItem();
            item3.Text = "FoupWay";
            item3.ImageKey = "foupWay";
            listView1.Items.Add(item3);

            ListViewItem item4 = new ListViewItem();
            item4.Text = "Device";
            item4.ImageKey = "device";
            listView1.Items.Add(item4);
        }

        

    }
}
