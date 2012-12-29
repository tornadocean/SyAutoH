using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCS.GuiHub;

namespace MCSControlLib
{
    public partial class pageStockerOpt : baseControlPage, IMcsControlBase
    {
        private byte stockorId = 0;

        private const string TKey_BarCode = "BarCode";
        private const string TKey_FoupRoom = "FoupRoom";
        private const string TKey_Lot = "Lot";
        private const string TKey_Status = "Status";

        private DataTable m_tableFoupsInfo = null;
        private List<int> listFoupBarCode = new List<int>();
        private List<int> listFoupRoom = new List<int>();
        private List<int> listFoupLot = new List<int>();
        private List<Int32> listStkRoom = new List<Int32>(141);

        formSTKAlarmHistory hisAlarm;

        public pageStockerOpt(byte id)
        {
            InitializeComponent();
            stockorId = id;
        }

        public override void PageInit()
        {
            PushData[] cmds = new PushData[] 
            { 
                PushData.upStkFoupsInfo, 
                PushData.upStkLastOptFoup, 
                PushData.upStkStatus, 
                PushData.upStkInputStatus 
            };
            m_dataHub.Async_SetPushCmdList(cmds);
        }

        private void pageStockerOpt_Load(object sender, EventArgs e)
        {
            InitFoupsInfoTable();
            dataGridViewFoupsInStocker.DataSource = m_tableFoupsInfo;
        }

        protected override void InitProcessDictionary()
        {
            m_dictProcess.Add(PushData.upStkFoupsInfo, ProcessFoupsTable);
            m_dictProcess.Add(PushData.upStkLastOptFoup, ProcessLastOptFoup);
            m_dictProcess.Add(PushData.upStkStatus, ProcessStkStatus);
            m_dictProcess.Add(PushData.upStkInputStatus, ProcessStkInputStatus);
            m_dictProcess.Add(PushData.upStkFoupInSys, ProcessFoupInSys);
            m_dictProcess.Add(PushData.upStkRoomStatus, ProcessStkRoomStatus);
        }

        private void InitFoupsInfoTable()
        {
            if (null == m_tableFoupsInfo)
            {
                m_tableFoupsInfo = new DataTable("Foups");
                m_tableFoupsInfo.Columns.Add(TKey_BarCode, typeof(System.UInt16));
                m_tableFoupsInfo.Columns[TKey_BarCode].AllowDBNull = false;
                m_tableFoupsInfo.PrimaryKey = new DataColumn[] { m_tableFoupsInfo.Columns[TKey_BarCode] };
                m_tableFoupsInfo.Columns.Add(TKey_FoupRoom, typeof(UInt16));
                m_tableFoupsInfo.Columns.Add(TKey_Lot, typeof(System.UInt16));
                m_tableFoupsInfo.Columns.Add(TKey_Status, typeof(System.UInt16));

                m_tableFoupsInfo.AcceptChanges();
            }
        }

        private void InitImageListRoom()
        {
            imageListPageStkRoom.Images.Add("green", Properties.Resources.green);
            imageListPageStkRoom.Images.Add("blue", Properties.Resources.blue);
            imageListPageStkRoom.Images.Add("red", Properties.Resources.red);
        }

        private void ProcessFoupsTable(ArrayList item)
        {
            if (item.Count > 1)
            {
                byte nID = TryConver.ToByte(item[0].ToString());
                if (nID == stockorId)
                {
                    UInt16 nBarCode = Convert.ToUInt16(item[1]);
                    UInt16 nFoupRoom = Convert.ToUInt16(item[2]);
                    UInt16 nLot = Convert.ToUInt16(item[3]);
                    UInt16 nStatus = Convert.ToUInt16(item[4]);
                    UInt16 nIsErase = Convert.ToUInt16(item[5]);
                    DataRow row = m_tableFoupsInfo.Rows.Find(nBarCode);
                    if (null != row)
                    {
                        if (0 == nIsErase)
                        {
                            row[TKey_BarCode] = nBarCode;
                            row[TKey_FoupRoom] = nFoupRoom;
                            row[TKey_Lot] = nLot;
                            row[TKey_Status] = nStatus;
                            row.AcceptChanges();
                        }
                        else if (1 == nIsErase)
                        {
                            m_tableFoupsInfo.Rows.Remove(row);
                            m_tableFoupsInfo.AcceptChanges();
                        }
                    }
                    else
                    {
                        row = m_tableFoupsInfo.NewRow();
                        row[TKey_BarCode] = nBarCode;
                        row[TKey_FoupRoom] = nFoupRoom;
                        row[TKey_Lot] = nLot;
                        row[TKey_Status] = nStatus;
                        m_tableFoupsInfo.Rows.Add(row);
                        m_tableFoupsInfo.AcceptChanges();
                    }
                }
            }
        }

        private void ProcessLastOptFoup(ArrayList item)
        {
            if (item.Count > 1)
            {
                byte nID = TryConver.ToByte(item[0].ToString());
                if (nID == stockorId)
                {
                    tBLastBarCode.Text = item[1].ToString();
                    tBLastFoupID.Text = item[2].ToString();
                    tBLastLot.Text = item[3].ToString();
                    tBLastFoupEventDir.Text = item[4].ToString();
                    UInt16 nInput=Convert.ToUInt16(item[5].ToString());
                    if (1 == nInput)
                    {
                        tBLastFoupAuto.Text = "1";
                        tBLastFoupManu.Text = "0";
                    }
                    else if (2 == nInput)
                    {
                        tBLastFoupAuto.Text = "2";
                        tBLastFoupManu.Text = "0";
                    }
                    else if (3 == nInput)
                    {
                        tBLastFoupAuto.Text = "3";
                        tBLastFoupManu.Text = "0";
                    }
                    else if (4 == nInput)
                    {
                        tBLastFoupAuto.Text = "4";
                        tBLastFoupManu.Text = "0";
                    }
                    else if (5 == nInput)
                    {
                        tBLastFoupAuto.Text = "0";
                        tBLastFoupManu.Text = "1";
                    }
                    else if (6 == nInput)
                    {
                        tBLastFoupAuto.Text = "0";
                        tBLastFoupManu.Text = "2";
                    }
                    else if (7 == nInput)
                    {
                        tBLastFoupAuto.Text = "0";
                        tBLastFoupManu.Text = "3";
                    }
                    else if (8 == nInput)
                    {
                        tBLastFoupAuto.Text = "0";
                        tBLastFoupManu.Text = "4";
                    }
                }
            }
        }

        private void ProcessStkStatus(ArrayList item)
        {
            if (item.Count == 2)
            {
                byte nID = TryConver.ToByte(item[0].ToString());
                if (nID == stockorId)
                {
                    tBStkInfoStatus.Text = item[1].ToString();
                    switch (Convert.ToInt16(item[1]))
                    {
                        case 0:
                            tBStkInfoStatus.Text = "正常运行";
                            break;
                        case 1:
                            tBStkInfoStatus.Text = "报警运行";
                            break;
                        case 2:
                            tBStkInfoStatus.Text = "故障停机";
                            break;
                        default:
                            tBStkInfoStatus.Text = "";
                            break;
                    }
                }
            }
        }

        private void ProcessStkInputStatus(ArrayList item)
        {
            if (item.Count > 1)
            {
                byte nID = TryConver.ToByte(item[0].ToString());
                if (nID == stockorId)
                {
                    switch (Convert.ToInt16(item[1]))
                    {
                        case 0:
                            tBPortStatusAuto.Text = "空闲";
                            break;
                        case 1:
                            tBPortStatusAuto.Text = "繁忙";
                            break;
                        case 2:
                            tBPortStatusAuto.Text = "故障";
                            break;
                        default:
                            tBPortStatusAuto.Text = "";
                            break;
                    }
                    switch (Convert.ToInt16(item[2]))
                    {
                        case 0:
                            tBPortStatusManu.Text = "空闲";
                            break;
                        case 1:
                            tBPortStatusManu.Text = "繁忙";
                            break;
                        case 2:
                            tBPortStatusManu.Text = "故障";
                            break;
                        default:
                            tBPortStatusManu.Text = "";
                            break;
                    }
                }
            }
        }

        private void ProcessFoupInSys(ArrayList item)
        {
            int nBarCode = TryConver.ToInt32(item[0].ToString());
            int nFoupRoom = TryConver.ToInt32(item[1].ToString());
            int nLot = TryConver.ToInt32(item[2].ToString());
            listFoupBarCode.Add(nBarCode);
            listFoupRoom.Add(nFoupRoom);
            listFoupLot.Add(nLot);
        }

        private void ProcessStkRoomStatus(ArrayList item)
        {
            string nID=item[0].ToString();
            Int32 nStatus=TryConver.ToInt32(item[1].ToString());
            ListViewItem listItem = new ListViewItem();
            listItem.Text = nID;
            switch(nStatus)
            {
                case 0:
                    listItem.ImageKey = "green";
                    break;
                case 1:
                    listItem.ImageKey = "blue";
                    break;
                case 2:
                    listItem.ImageKey = "red";
                    break;
            }
            listViewPageStkRoom.Items.Add(listItem);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void bnAlarmHistory_Click(object sender, EventArgs e)
        {
            hisAlarm = new formSTKAlarmHistory();
            hisAlarm.ShowDialog();
        }

        public void AlarmHistorySendData(string str)
        {
            byte nID = stockorId;
            string strVal = string.Format("<{0},{1}>", nID, str);

            int nWRet = m_dataHub.WriteData(GuiCommand.StkAlarmHistory, strVal);
        }

        private void bnFoupHistory_Click(object sender, EventArgs e)
        {
            formSTKFoupHistory hisFoup = new formSTKFoupHistory();
            hisFoup.ShowDialog();
        }

        private void linkLabelStkFoupRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            byte nID = stockorId;
            string strVal;
            strVal = string.Format("<{0}>", nID);

            int nWRet = m_dataHub.WriteData(GuiCommand.StkInquiryStorage, strVal);

            m_tableFoupsInfo.Rows.Clear();
        }

        private void btnBackTimeStatus_Click(object sender, EventArgs e)
        {
            string strTime = tBBackTimeStatus.Text;
            byte nID = stockorId;
            Int32 nTime = 0;
            nTime = TryConver.ToInt32(strTime);
            string strVal;
            strVal = string.Format("<{0},{1}>", nID, nTime);

            int nWRet = m_dataHub.WriteData(GuiCommand.StkStatusTime, strVal);
        }

        private void btnBackTimeFoups_Click(object sender, EventArgs e)
        {
            string strTime = tBBackTimeFoups.Text;
            byte nID = stockorId;
            Int32 nTime = 0;
            nTime = TryConver.ToInt32(strTime);
            string strVal;
            strVal = string.Format("<{0},{1}>", nID, nTime);

            int nWRet = m_dataHub.WriteData(GuiCommand.StkSetFoupInfoBackTime, strVal);
        }

        private void lLStkInfoStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            byte nID = stockorId;
            string strVal = string.Format("<{0}>", nID);

            int nWRet = m_dataHub.WriteData(GuiCommand.StkInquiryStatus, strVal);
        }

        private void btnFoupMoveInOut_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            byte nID = stockorId;
            byte nOpt = 2;
            byte nMode = TryConver.ToByte(cBFoupMove.SelectedIndex.ToString());
            int nData = TryConver.ToInt32(tBFoupMove.Text);
            if (btn.Name == "btnFoupMoveIn")
            {
                nOpt = 0;
            }
            else if (btn.Name == "btnFoupMoveOut")
            {
                nOpt = 1;
            }
            string strVal;
            strVal = string.Format("<{0},{1},{2},{3}>", nID, nOpt, nMode, nData);

            int nWRet = m_dataHub.WriteData(GuiCommand.StkHandFoup, strVal);

        }

        private void btnGetPortStatus_Click(object sender, EventArgs e)
        {
            byte nID = stockorId;
            string strVal = string.Format("<{0}>", nID);

            int nWRet = m_dataHub.WriteData(GuiCommand.StkInquiryInputStatus, strVal);
        }

        private void dataGridViewFoupsInStocker_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewFoupsInStocker.SelectedRows;
            if (rows.Count > 0)
            {
                DataGridViewRow row = rows[0];
                tBSelBarCode.Text = row.Cells[0].Value.ToString();
                tBSelFoupID.Text = row.Cells[1].Value.ToString();
                tBSelLot.Text = row.Cells[2].Value.ToString();
                tBSelStatus.Text = row.Cells[3].Value.ToString();
            }
        }

        private void tBFoupMove_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void tBFoupMove_TextChanged(object sender, EventArgs e)
        {
        }

        
    }
}
