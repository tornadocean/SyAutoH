using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gemma;

namespace MCSControlLib
{
    public partial class pageLogOnline : baseControlPage, IMcsControlBase
    {
        private LogWinClientCS.LogWinClient logClient = new LogWinClientCS.LogWinClient();
        private System.Windows.Forms.Timer timer2 = new Timer();
        private event EventHandler UpdateList;
        private event EventHandler GetNowMsg;
       // private int m_nQueryType = 0;
        private int m_nPageStartID = 0;
        private int m_nPageEndID = 0;
        private int m_nPageNowID = 0;
        LoggerFilter filterShow;
        public pageLogOnline()
        {
            InitializeComponent();
            tb_EventID.Text = "-1";
            timer2.Interval = 500;
            UpdateList += new EventHandler(EvUpdate);
            GetNowMsg += new EventHandler(Form1_GetNowMsg);
            timer2.Tick += new EventHandler(timer2_Tick);
        }

         public override void PageInit()
        {
            logClient.ConnectServer();
            timer2.Enabled = true;
        }

         public override void PageExit()
         {
             base.PageExit();
             timer2.Enabled = false;
             logClient.Disconnect();
         }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.GetNowMsg.Invoke(null, null);
        }

        void Form1_GetNowMsg(object sender, EventArgs e)
        {
            lock (this)
            {
                // if (bBusy == true)
                // {
                //     return;
                // }
                try
                {
                    //bBusy = true;
                    int nNow = logClient.GetNowID();
                    if (nNow < 0)
                    {
                        return;
                    }
                    if (nNow != m_nPageNowID)
                    {
                        AddMsgList(nNow, 10);
                    }
                    m_nPageNowID = nNow;
                }
                catch (System.Exception)
                {

                }

                // bBusy = false;
            }
        }

        private void EvUpdate(object ob, EventArgs args)
        {
            lock (this)
            {
                // if (bBusyList == true)
                // {
                //     return;
                // }

                // bBusyList = true;
                try
                {
                    InfoCall info = ob as InfoCall;

                    listOnlineLog.BeginUpdate();
                    listOnlineLog.Items.Clear();
                    LogMsg[] msgList = logClient.GetLog(info.nStart, info.nCount, info.types, info.ids, "");
                    if (null == msgList)
                    {
                        listOnlineLog.EndUpdate();
                        return;
                    }

                    if (msgList.Length <= 0)
                    {
                        listOnlineLog.EndUpdate();
                        return;
                    }

                    m_nPageStartID = info.nStart;

                    foreach (LogMsg msg in msgList)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = msg.nID.ToString();
                        //Int64 ntime = 0
                        m_nPageEndID = msg.nID;
                        item.SubItems.Add(msg.lTime.ToString());
                        item.SubItems.Add(msg.nEventID.ToString());
                        item.SubItems.Add(msg.strMsg);
                        item.SubItems.Add(msg.strUser);
                        item.SubItems.Add(msg.nType.ToString());
                        listOnlineLog.Items.Add(item);
                        //Invalidate();
                    }

                    listOnlineLog.EndUpdate();

                    //RefreshAlarmView();
                }
                catch (System.Exception)
                {

                }

                //bBusyList = false;
            }

        }

        private void AddMsgList(int nStart, int nCount)
        {
            int[] ids = GetIDFilter(tb_EventID.Text);
            string strType = GetTypeFilter();
            int[] types = GetIDFilter(strType);
            InfoCall info = new InfoCall();
            info.nStart = nStart;
            info.nCount = nCount;
            info.types = types;
            info.ids = ids;
            this.UpdateList.Invoke(info, null);
        }

        private int[] GetIDFilter(string strIDFilter)
        {
            char[] chFilter = new char[] { ',' };
            string[] strIDS = strIDFilter.Split(chFilter);
            int nIDSLen = strIDS.Length;
            List<int> listIDS = new List<int>();
            foreach (string strID in strIDS)
            {
                if (strID.Length > 0)
                {
                    try
                    {
                        int nID = Convert.ToInt32(strID);
                        listIDS.Add(nID);
                    }
                    catch (System.Exception ex)
                    {
                        string str = ex.Message;
                    }
                }
            }
            return listIDS.ToArray();
        }

        private string GetTypeFilter()
        {
            string strType = "";
            if (true == checkBoxInfo.Checked)
            {
                strType += "1, ";
            }
            if (true == checkBoxWarning.Checked)
            {
                strType += "2, ";
            }
            if (true == checkBoxError.Checked)
            {
                strType += "3, ";
            }
            if (true == checkBoxDebug.Checked)
            {
                strType += "4, ";
            }

            return strType;
        }

        private void bnPause_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled == true)
            {
                timer2.Enabled = false;
            }
            else
            {
                timer2.Enabled = true;
            }
            if (bnPause.Text == "Pause")
            {
                this.Invoke(new EventHandler(delegate { bnPause.Text = "Start"; }));
            }
            else 
            {
                this.Invoke(new EventHandler(delegate { bnPause.Text = "Pasue"; }));
            }
        }

        class InfoCall
        {
            public int nStart = 0;
            public int nCount = 0;
            public int[] types = null;
            public int[] ids = null;
        };

        private void CanCheckedAll()
        {
            bool bCheckAll = false;
            bCheckAll = checkBoxInfo.Checked
                & checkBoxWarning.Checked
                & checkBoxError.Checked
                & checkBoxDebug.Checked;

            checkBoxAll.Checked = bCheckAll;
            m_nPageNowID = 0;

        }

        private void checkBoxInfo_CheckedChanged(object sender, EventArgs e)
        {
            CanCheckedAll();
        }

        private void checkBoxWarning_CheckedChanged(object sender, EventArgs e)
        {
            CanCheckedAll();
        }

        private void checkBoxError_CheckedChanged(object sender, EventArgs e)
        {
            CanCheckedAll();
        }

        private void checkBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            CanCheckedAll();
        }

        private void bn_Filter_Click(object sender, EventArgs e)
        {
            filterShow = new LoggerFilter();
            filterShow.ShowDialog();
        }
    }
}
