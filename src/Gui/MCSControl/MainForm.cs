using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCSControlLib;

namespace MCSControl
{
    public partial class MainForm : Form
    {
        public int Session
        {
            get { return m_nSession; }
            set { m_nSession = value; }
        }
        private string m_strUserName = "";
        public string UserName
        {
            get { return m_strUserName; }
            set { m_strUserName = value; }
        }
        private bool m_bNeedLogin = false;
        public bool NeedLogin
        {
            get { return m_bNeedLogin; }
        }

        private GuiAccess.DataHubCli m_dataHub = new GuiAccess.DataHubCli();
        private Queue<MCS.GuiDataItem> buf = new Queue<MCS.GuiDataItem>();
        private long m_ltime64 = 0;
        private int m_nSession = -1;
        private Dictionary<string, baseControlPage> m_dictMcsControl = 
            new Dictionary<string, baseControlPage>();
        private baseControlPage _ctrl = null;
        private IMcsControlBase m_ctrlBase = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void GuiDataUpdate(long lTime, MCS.GuiDataItem item)
        {
            lock (buf)
            {
                buf.Enqueue(item);
                m_ltime64 = lTime;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_dataHub.ConnectServer();
            toolStripStatusLabel_User.Text = "Logined User: " + m_strUserName + " ";
            m_dataHub.DataUpdater += new GuiAccess.DataUpdaterHander(GuiDataUpdate);
            m_dataHub.Async_SetCallBack();
            m_dataHub.Session = m_nSession;

            InitMcsControlDictionary();
            this.timer1.Start();
        }

        private void OnDataChange(object sender, object obData1, object obData2)
        {
            baseControlPage ctrl = sender as baseControlPage;
            if (ctrl.Name.CompareTo("pageSTKInfo") == 0)
            {
                TreeNode nodeSTK = this.treeViewPage.Nodes["nodeSTKInfo"];
                if (null != nodeSTK)
                {
                    nodeSTK.Nodes.Clear();
                    ArrayList list = obData1 as ArrayList;
                    foreach (object item in list)
                    {
                        byte nID = (byte)item;
                        TreeNode node = new TreeNode();
                        string strNodeName = string.Format("nodeSTKItem_{0}", nID);
                        node.Name = strNodeName;
                        node.Text = string.Format("STK_{0}", nID);
                        nodeSTK.Nodes.Add(node);
                        baseControlPage tmpCtrl = null;
                        if (m_dictMcsControl.TryGetValue(strNodeName, out tmpCtrl) == false)
                        {
                            m_dictMcsControl.Add(strNodeName, new pageStockerOpt(nID));
                        }
                    }
                }
            }
        }

        private void InitMcsControlDictionary()
        {
            m_dictMcsControl.Add("nodeOHTInfo", new pageOHTInfo() );
            m_dictMcsControl.Add("nodeMesCommand", new pageMesCommand() );
            m_dictMcsControl.Add("nodeSTKInfo", new pageSTKInfo());
            m_dictMcsControl.Add("nodeLogOffline", new pageLogOffline());
            m_dictMcsControl.Add("nodeLogOnline", new pageLogOnline());
            m_dictMcsControl.Add("nodeAlarm", new pageAlarm());
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bNeedLogin = true;
            this.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bNeedLogin = false;
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strSelect = e.Node.Name;
            bool bFind = m_dictMcsControl.TryGetValue(strSelect, out _ctrl);

            if (null != _ctrl)
            {
                if (null != m_ctrlBase)
                {
                    m_ctrlBase.DataChange -= new DataChangeHander(OnDataChange);
                    m_ctrlBase.PageExit();
                }
             
                m_ctrlBase = _ctrl as IMcsControlBase;
                m_ctrlBase.DataHub = m_dataHub;   
                m_ctrlBase.DataChange += new DataChangeHander(OnDataChange);
                _ctrl.Location = new Point(10, 10);
                _ctrl.Size = new Size(10, 10);
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(_ctrl);
                _ctrl.Dock = DockStyle.Fill;
                m_ctrlBase.PageInit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<MCS.GuiDataItem> listGuiData = new List<MCS.GuiDataItem>();
            lock (buf)
            {
                DateTime dt = DateTime.MinValue;
                dt = dt.AddYears(1969);
                dt = dt.AddSeconds(m_ltime64);
                dt = dt.ToLocalTime();
                this.toolStripStatusLabel_PushTime.Text = "Last Push: " + dt.ToString() + " ";
                while (buf.Count != 0)
                {
                    MCS.GuiDataItem item = buf.Dequeue();
                    listGuiData.Add(item);
                }
            }

            if (null != m_ctrlBase)
            {
                m_ctrlBase.ProcessGuiData(listGuiData);
            }

            DateTime dtNow = DateTime.Now;
            TimeSpan span = dtNow - m_dataHub.UpdateTime;
            if (span.TotalSeconds > 5)
            {
                m_dataHub.Async_SetCallBack();
            }
        }
    }
}
