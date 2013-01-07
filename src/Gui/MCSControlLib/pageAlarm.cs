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
    public partial class pageAlarm : baseControlPage, IMcsControlBase
    {
        private LogWinClientCS.LogWinClient logClient = new LogWinClientCS.LogWinClient();
        System.Windows.Forms.Timer timer3;
        private int m_nPageStartID = 0;
        private int m_nPageNowID = 0;
        private int m_nPageStart = 0;
        private int m_nOfflinePageLimit = 0;

        public pageAlarm()
        {
            InitializeComponent();
            tBEventID.Text = "-1";
            timer3 = new Timer();
            timer3.Enabled = true;
            timer3.Interval = 3000;
            timer3.Tick += new EventHandler(timer3_Tick);
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            int[] ids = GetIDSelect(tBEventID.Text);
            List<int> idList = new List<int>();
            foreach (ListViewItem item in listViewAlarm.Items)
            {
                foreach (int id in ids)
                {
                    if(id == Convert.ToInt32(item.SubItems[1].Text))
                    idList.Add(id);
                }
            }
            logClient.ClearAlarm(idList.ToArray());
            //RefreshAlarmView();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //RefreshAlarmView();
        }
        /*
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
        */

        private int[] GetIDSelect(string strIDSelect)
        {
            char[] chSelect = new char[] { ',' };
            string[] strIDS = strIDSelect.Split(chSelect);
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

        private void bnClearAll_Click(object sender, EventArgs e)
        {
            List<int> idList = new List<int>();
            foreach (ListViewItem item in listViewAlarm.Items)
            {
                int nEventID = Convert.ToInt32(item.SubItems[1].Text);
                idList.Add(nEventID);
            }
            logClient.ClearAlarm(idList.ToArray());
           // RefreshAlarmView();
        }

        private void bnFormer_Click(object sender, EventArgs e)
        {
           
        }

        private void bnFirstPage_Click(object sender, EventArgs e)
        {
            m_nPageStartID = 0;
           // RefreshAlarmView();
        }

        private void bnNext_Click(object sender, EventArgs e)
        {

        }

        private void bnEnd_Click(object sender, EventArgs e)
        {

        }

    }
}
