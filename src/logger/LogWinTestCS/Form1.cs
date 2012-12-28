using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gemma;


namespace LogWinTestCS
{

    public partial class Form1 : Form
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


        public Form1()
        {
            InitializeComponent();
            UpdateList += new EventHandler(EvUpdate);
            GetNowMsg += new EventHandler(Form1_GetNowMsg);
            timer1.Tick += new EventHandler(timer1_Tick);
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

        private void bnPrev_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            AddMsgList(m_nPageEndID - 1, 10);
        }

        private void bnNext_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            AddMsgList(m_nPageStartID + 10, 10);
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

        private void bnNow_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            DateTime dt = DateTime.Now;
            int nNow = logClient.GetNowID();
            AddMsgList(nNow, 1000);
            DateTime dt2 = DateTime.Now;

            TimeSpan dd = dt2 - dt;

            double dbT = dd.TotalMilliseconds;

            this.listLog.Items.Add(dbT.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logClient.ConnectServer();
            cmbOnLineType.SelectedIndex = 0;

            tBQueryIDS.Text = "-1";
            tBOnlineIDS.Text = "-1";

            this.dtpBegin.Format = DateTimePickerFormat.Custom;
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Value = DateTime.Now;

            this.dtpEnd.Format = DateTimePickerFormat.Custom;
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Value = DateTime.Now;
        }

        private void bnSendCus_Click(object sender, EventArgs e)
        {
            int nType = cmbOnLineType.SelectedIndex + 1;
            if (nType <= 0)
            {
                nType = 1;
            }
            logClient.Log(100, (byte)nType, "logclient", txtLog.Text);
        }

        private void bnQuickSend_Click(object sender, EventArgs e)
        {
            // int n = logClient.GetNowID();
            logClient.Log(100, 1, "logclient", "default log message");
        }

        private void bnAuto_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.GetNowMsg.Invoke(null, null);
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            //MessageBox.Show(dateTimePicker1.Text);
        }

        private void tBQueryType_TextChanged(object sender, EventArgs e)
        {
            m_nQueryType = System.Convert.ToByte(tBQueryIDS.Text);
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

        private void bnAlmLast_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            RefreshAlarmView();
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            List<int> idList = new List<int>();
            foreach (ListViewItem item in listViewAlarm.SelectedItems)
            {
                int nEventID = Convert.ToInt32(item.SubItems[1].Text);
                idList.Add(nEventID);
            }

            logClient.ClearAlarm(idList.ToArray());

            RefreshAlarmView();
        }

        private void bnAlmRT_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Enabled = true;
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

        private void GetOfflineCount()
        {
            long llTimeEnd = 0;
            long llTimeBegin = 0;

            GetTimePeriod(out llTimeEnd, out llTimeBegin);
            string strKey = this.tBKey.Text;

            int[] nIDFilter = GetIDFilter(tBQueryIDS.Text);
            int[] nTypeFilter = GetIDFilter(tBTypes.Text);

            int nCount = logClient.GetOfflineCount(llTimeEnd, llTimeBegin, nTypeFilter, nIDFilter, strKey);
            m_nOfflineCount = nCount;

            int nPageCount = m_nOfflineCount / m_nOfflinePageLimit;
            int nPageNum = m_nPageStart / m_nOfflinePageLimit;
        }

        private void GetTimePeriod(out long lTimeEnd, out long lTimeBegin)
        {
            DateTime dt = dtpBegin.Value;
            string strTBegin = string.Format("{0:000#}{1:0#}{2:0#}{3:0#}{4:0#}{5:0#}{6:00#}", dt.Year, dt.Month, dt.Day,
                dt.Hour, dt.Minute, dt.Second, dt.Millisecond);

            DateTime dtEnd = dtpEnd.Value;
            string strTEnd = string.Format("{0:000#}{1:0#}{2:0#}{3:0#}{4:0#}{5:0#}{6:00#}", dtEnd.Year, dtEnd.Month, dtEnd.Day,
                dtEnd.Hour, dtEnd.Minute, dtEnd.Second, dtEnd.Millisecond);

            lTimeEnd = Convert.ToInt64(strTEnd);
            lTimeBegin = Convert.ToInt64(strTBegin);
        }

        private void GetOfflineLog()
        {
            long llTimeEnd = 0;
            long llTimeBegin = 0;

            GetTimePeriod(out llTimeEnd, out llTimeBegin);
            string strKey = this.tBKey.Text;

            int[] nIDFilter = GetIDFilter(tBQueryIDS.Text);
            int[] nTypeFilter = GetIDFilter(tBTypes.Text);

            int nCount = logClient.GetOfflineCount(llTimeEnd, llTimeBegin, nTypeFilter, nIDFilter, strKey);
            m_nOfflineCount = nCount;

            int nPageCount = m_nOfflineCount / m_nOfflinePageLimit;
            int nPageNum = m_nPageStart / m_nOfflinePageLimit;

            string strPageMark = nPageNum.ToString() + "/" + nPageCount.ToString();
            label4.Text = strPageMark;

            label3.Text = nCount.ToString();
            int nPage = nCount / 100;

            LogMsg[] msgList = logClient.GetOfflineLog(m_nPageStart, m_nOfflinePageLimit, llTimeEnd, llTimeBegin,
                nTypeFilter, nIDFilter, strKey);

            if (null == msgList)
            {
                return;
            }

            listOfflineLog.Items.Clear();
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
                listOfflineLog.Items.Add(item);
                //Invalidate();
            }
        }

        private void bnGetOffline_Click(object sender, EventArgs e)
        {
            GetOfflineLog();
        }

        private void bnHome_Click(object sender, EventArgs e)
        {
            m_nPageStart = 0;
            GetOfflineLog();
        }

        private void bnPageUp_Click(object sender, EventArgs e)
        {
            m_nPageStart += m_nOfflinePageLimit;
            if (m_nPageStart < m_nOfflineCount)
            {

            }
            else
            {
                m_nPageStart -= m_nOfflinePageLimit;
            }

            GetOfflineLog();
        }

        private void bnPageDown_Click(object sender, EventArgs e)
        {
            m_nPageStart -= m_nOfflinePageLimit;
            if (m_nPageStart < 0)
            {
                m_nPageStart = 0;
            }
            GetOfflineLog();

        }

        private void bnEnd_Click(object sender, EventArgs e)
        {
            GetOfflineCount();
            m_nPageStart = (m_nOfflineCount / m_nOfflinePageLimit) * m_nOfflinePageLimit;
            GetOfflineLog();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    //if (false == checkBoxAll.Checked)
        //   // {
        //   //     checkBoxAll.Checked = true;
        //   // }
        //    bool bCheck = checkBoxAll.Checked;
        //    checkBoxInfo.Checked = bCheck;
        //    checkBoxWarning.Checked = bCheck;
        //    checkBoxError.Checked = bCheck;
        //    checkBoxDebug.Checked = bCheck;
        //}

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

        class InfoCall
        {
            public int nStart = 0;
            public int nCount = 0;
            public int[] types = null;
            public int[] ids = null;
        };
    }
}