using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RailDraw
{
    public partial class ProgramRegion : DockContent
    {
        public List<TreeNode> treeNodeList = new List<TreeNode>();
        public bool winShown = false;
        static Int16 nodeNum = 0;

        public ProgramRegion()
        {
            InitializeComponent();
        }

        private void ProgramRegion_Load(object sender, EventArgs e)
        {
            TreeNode rootNode = new TreeNode(((FatherWindow)this.ParentForm).workRegion.Text);
            this.treeView1.Nodes.Add(rootNode);
            rootNode.Nodes.Add("Line");
            rootNode.Nodes.Add("Curve");
            rootNode.Nodes.Add("Cross");
        }

        private void ProgramRegion_Shown(object sender, EventArgs e)
        {
            winShown = true;
        }

        private void ProgramRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            winShown = false;
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                Int16 index = Convert.ToInt16(treeNodeList.IndexOf(treeView1.SelectedNode));
                ((FatherWindow)this.ParentForm).SelectedElement(index);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo info = this.treeView1.HitTest(e.Location);
            this.treeView1.SelectedNode = info.Node;
            if (this.treeView1.SelectedNode != null)
            {
                Int16 index = Convert.ToInt16(treeNodeList.IndexOf(this.treeView1.SelectedNode));
                ((FatherWindow)this.ParentForm).SelectedElement(index);
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeView tempTree = sender as TreeView;
            TreeNode node = tempTree.SelectedNode;
            if (node != null
                && MouseButtons.Right == e.Button 
                && node.Text != ((FatherWindow)this.ParentForm).workRegion.Text
                && 2 == node.Level)
            {
                string str = node.Text;
                int lenght = str.IndexOf('_');
                if (-1 != lenght)
                {
                    str = str.Substring(0, lenght);
                }
                switch(str)
                {
                    case "Line":
                    case "Curve":
                    case "Cross":
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add("delete");
                        for (Int16 i = 0; i < contextMenuStrip1.Items.Count; i++)
                        {
                            contextMenuStrip1.Items[i].Click += new EventHandler(contextmenu_Click);
                        }
                        contextMenuStrip1.Show(Cursor.Position);
                        break;
                    default:
                        break;
                }
            }
        }

        private void contextmenu_Click(object sender, EventArgs e)
        {
            ((FatherWindow)this.ParentForm).DeleteElement();
        }

        public void AddElementNode(string fatherName, string str)
        {
            TreeNode tempTreeNode;
            tempTreeNode = new TreeNode(str);
            tempTreeNode.Name = str + nodeNum.ToString();
            treeNodeList.Add(tempTreeNode);
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                if (node.Text == fatherName)
                {
                    node.Nodes.Add(tempTreeNode);
                    tempTreeNode.Parent.ExpandAll();
                }
            }
            this.treeView1.SelectedNode = tempTreeNode;
        }

        public void DeleteElementNode(string fatherName, Int16 index)
        {
            TreeNode tempTreeNode;
            tempTreeNode = treeNodeList[index];
            treeNodeList.RemoveAt(index);
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                if (node.Text == fatherName)
                {
                    node.Nodes.Remove(tempTreeNode);
                }
            }
            this.treeView1.SelectedNode = null;
        }
    }
}
