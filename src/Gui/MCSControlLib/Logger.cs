using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gemma;

namespace MCSControlLib
{
    public partial class Logger : Form
    {
        private LogWinClientCS.LogWinClient logClient = new LogWinClientCS.LogWinClient();
        private int m_nPageStartID = 0;
        private int m_nPageEndID = 0;
        private int m_nPageNowID = 0;
        private event EventHandler UpdateList;
        private event EventHandler GetNowMsg;
        private int m_nQueryType = 0;
        private const int m_nOfflinePageLimit = 100;
        private int m_nPageStart = 0;
        private int m_nOfflineCount = 0;

        public Logger()
        {
            InitializeComponent();
            UpdateList += new EventHandler(EvUpdate);
            GetNowMsg += new EventHandler(Logger_GetNowMsg);
            timer1.Tick += new EventHandler(timer1_Tick);
        }
        void Logger_GetNowMsg(object sender, EventArgs e)
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

                    listLog.BeginUpdate();
                    listLog.Items.Clear();
                    LogMsg[] msgList = logClient.GetLog(info.nStart, info.nCount, info.types, info.ids, "");
                    if (null == msgList)
                    {
                        listLog.EndUpdate();
                        return;
                    }

                    if (msgList.Length <= 0)
                    {
                        listLog.EndUpdate();
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
                        listLog.Items.Add(item);
                        //Invalidate();
                    }

                    listLog.EndUpdate();

                    RefreshAlarmView();
                }
                catch (System.Exception)
                {

                }

                //bBusyList = false;
            }

        }
        private void AddMsgList(int nStart, int nCount)
        {
            int[] ids = GetIDFilter(tBOnlineIDS.Text);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.GetNowMsg.Invoke(null, null);
        }

        private void RefreshAlarmView()
        {
            int nCount = 0;
            nCount = logClient.GetAlarmCount();

            AlarmMsg[] almList = logClient.GetAlarm(0, nCount);
            int nIndex = 1;
            listViewAlarm.BeginUpdate();
            listViewAlarm.Items.Clear();
            foreach (AlarmMsg msg in almList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = nIndex.ToString();
                item.SubItems.Add(msg.nEventID.ToString());
                item.SubItems.Add(msg.lastTime.ToString());
                item.SubItems.Add(msg.strMsg);
                item.SubItems.Add(msg.firstTime.ToString());
                item.SubItems.Add(msg.nCount.ToString());
                listViewAlarm.Items.Add(item);
                //Invalidate();
                nIndex++;
            }
            listViewAlarm.EndUpdate();
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

        class InfoCall
        {
            public int nStart = 0;
            public int nCount = 0;
            public int[] types = null;
            public int[] ids = null;
        };
    }
}
