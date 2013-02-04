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
        private List<TreeNode> listTreeNode = new List<TreeNode>();
        public bool winShown = false;

        public ProgramRegion()
        {
            InitializeComponent();
        }

        private void ProgramRegion_Load(object sender, EventArgs e)
        {
            TreeNode rootNode = new TreeNode(((FatherWindow)this.DockPanel.Parent).workRegion.Text);
            this.treeView1.Nodes.Add(rootNode);
            InitTreeView(rootNode);
        }

        public void InitTreeView(TreeNode rootNode)
        {
            rootNode.Nodes.Add("Line");
            rootNode.Nodes.Add("Curve");
            rootNode.Nodes.Add("Cross");
            rootNode.Nodes.Add("FoupWay");
            rootNode.Nodes.Add("Device");
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
                Int16 index = Convert.ToInt16(listTreeNode.IndexOf(treeView1.SelectedNode));
                SelectedElement(index);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo info = this.treeView1.HitTest(e.Location);
            this.treeView1.SelectedNode = info.Node;
            if (this.treeView1.SelectedNode != null)
            {
                Int16 index = Convert.ToInt16(listTreeNode.IndexOf(this.treeView1.SelectedNode));
                SelectedElement(index);
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeView tempTree = sender as TreeView;
            TreeNode node = tempTree.SelectedNode;
            if (node != null
                && MouseButtons.Right == e.Button 
                && node.Text != ((FatherWindow)this.DockPanel.Parent).workRegion.Text
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
                    case "FoupWay":
                    case "Device":
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
            ((FatherWindow)this.DockPanel.Parent).workRegion.DeleteElement();
        }

        public void SelectedElement(Int16 index)
        {
            FatherWindow father = (FatherWindow)this.DockPanel.Parent;
            father.drawDocOp.SelectedDrawObjectList.Clear();
            if (index >= 0)
            {
                father.drawDocOp.SelectedDrawObjectList.Add(father.drawDocOp.DrawObjectList[index]);
                this.treeView1.SelectedNode = this.listTreeNode[index];
                father.proPage.propertyGrid1.SelectedObject=father.drawDocOp.SelectedDrawObjectList[0];
            }
            else
            {
                father.proPage.propertyGrid1.SelectedObject = null;
            }
            father.workRegion.picBoxCanvas.Invalidate();
        }

        public void RefreshTreeView()
        {
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                for (int i = node.Nodes.Count - 1; i > -1; i--)
                {
                    node.Nodes.RemoveAt(i);
                }
            }
            listTreeNode.Clear();
            listTreeNode.AddRange(BaseRailElement.ObjectBaseEvents.DocumentOp.ListTreeNode);
            foreach (TreeNode node in listTreeNode)
            {
                string str = node.Text.Substring(0, node.Text.IndexOf('_'));
                foreach (TreeNode root in treeView1.Nodes[0].Nodes)
                {
                    if (root.Text == str)
                    {
                        root.Nodes.Add(node);
                    }
                }
            }
            treeView1.Nodes[0].ExpandAll();
            SetSelectedNode();
        }

        public void SetSelectedNode()
        {
            for (int i = 0; i < listTreeNode.Count; i++)
            {
                if (listTreeNode[i].Text == ((FatherWindow)this.DockPanel.Parent).drawDocOp.NodeSelected.Text)
                {
                    treeView1.SelectedNode = listTreeNode[i];
                    break;
                }
            }
        }



    }
}
