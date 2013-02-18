using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using MCS;
using MCS.GuiHub;

namespace GuiAccess
{
    public partial class OhtInfoData
    {
        private int nID;
        public int ID
        {
            get { return nID; }
            set { nID = value; }
        }
        private long nPosition;
        public long Position
        {
            get { return nPosition; }
            set { nPosition = value; }
        }
        private int nHand;
        public int Hand
        {
            get { return nHand; }
            set { nHand = value; }
        }
        private int nStatus;
        public int Status
        {
            get { return nStatus; }
            set { nStatus = value; }
        }
        private int nAlarm;
        public int Alarm
        {
            get { return nAlarm; }
            set { nAlarm = value; }
        }
        private string strTcpInfo;
        public string TcpInfo
        {
            get { return strTcpInfo; }
            set { strTcpInfo = value; }
        }
    }

    class GuiDataProcesser
    {
        private const string TKeyP_Pos = "Position";
        private const string TKeyP_Name = "Name";
        private const string TKeyP_Type = "Type";
        private const string TKeyP_Speed = "Speed";
        private DataSet m_ds = new DataSet();

        public DataSet DataSource
        {
            get { return m_ds; }
        }

        public GuiDataProcesser()
        {
            InitDataSet();
            InitProcessDictionary();
        }

        protected delegate void ProcessHandler(ArrayList item);
        protected Dictionary<PushData, ProcessHandler> m_dictProcess = new Dictionary<PushData, ProcessHandler>();

        private void InitProcessDictionary()
        {
            m_dictProcess.Add(PushData.upMesPosTable, ProcessPosTable);
            m_dictProcess.Add(PushData.upMesFoupTable, ProcessFoupTable);

            m_dictProcess.Add(PushData.upOhtInfo, ProcessOHTInfo);
            m_dictProcess.Add(PushData.upOhtPos, ProcessOHTPos);
            m_dictProcess.Add(PushData.upOhtPosTable, ProcessOHTPosTable);
        }

        private void InitDataSet()
        {
            InitTableMesFoup();
            InitTableMesPos();

            InitTableOHTInfo();
            InitTableOHTKeyPos();
        }
       

        public void ProcessData(GuiDataItem guiData)
        {
            ProcessHandler handler = null;
            bool bGet = m_dictProcess.TryGetValue(guiData.enumTag, out handler);
            if (true == bGet)
            {
                ArrayList alDatas = GuiAccess.DataHubCli.ConvertToArrayList(guiData.sVal);
                foreach (ArrayList item in alDatas)
                {
                    handler(item);
                }
            }
        }

        #region MES

        private void InitTableMesFoup()
        {
            DataTable table = m_ds.Tables["MesFoup"];
            if (null == table)
            {
                table = new DataTable("MesFoup");
                table.Columns.Add("BarCode", typeof(System.UInt32));
                table.Columns["BarCode"].AllowDBNull = false;
                table.PrimaryKey = new DataColumn[] { table.Columns["BarCode"] };
                table.Columns.Add("Lot", typeof(System.UInt32));
                table.Columns.Add("Carrier", typeof(System.UInt32));
                table.Columns.Add("Port", typeof(System.UInt32));
                table.Columns.Add("Location", typeof(System.Int32));
                table.Columns.Add("LocType", typeof(System.UInt32));
                table.Columns.Add("Status", typeof(System.UInt32));

                table.AcceptChanges();
                m_ds.Tables.Add(table);
            }
        }

        private void InitTableMesPos()
        {
            DataTable table = m_ds.Tables["MesPos"];
            if (null == table)
            {
                table = new DataTable("MesPos");
                table.Columns.Add(TKeyP_Pos, typeof(System.UInt32));
                table.Columns[TKeyP_Pos].AllowDBNull = false;
                table.PrimaryKey = new DataColumn[] { table.Columns[TKeyP_Pos] };
                table.Columns.Add(TKeyP_Name, typeof(System.String));
                table.Columns.Add(TKeyP_Type, typeof(System.Byte));
                table.Columns.Add(TKeyP_Speed, typeof(System.Byte));
                table.AcceptChanges();
                m_ds.Tables.Add(table);
                m_ds.AcceptChanges();
            }
        }

        private void ProcessFoupTable(ArrayList item)
        {
            if (7 == item.Count)
            {
                DataTable table = m_ds.Tables["MesFoup"];
                if (null == table)
                {
                    return;
                }

                UInt32 uBarCode = UInt32.Parse(item[0].ToString());
                UInt32 uLot = UInt32.Parse(item[1].ToString());
                UInt32 uCarrier = UInt32.Parse(item[2].ToString());
                uint uPort = UInt32.Parse(item[3].ToString());
                int nLocation = Int32.Parse(item[4].ToString());
                uint uLocType = UInt32.Parse(item[5].ToString());
                uint uStatus = UInt32.Parse(item[6].ToString());
                DataRow row = table.Rows.Find(uBarCode);
                if (null != row)
                {
                    row[1] = uLot;
                    row[2] = uCarrier;
                    row[3] = uPort;
                    row[4] = nLocation;
                    row[5] = uLocType;
                    row[6] = uStatus;
                    row.AcceptChanges();
                }
                else
                {
                    row = table.NewRow();
                    row[0] = uBarCode;
                    row[1] = uLot;
                    row[2] = uCarrier;
                    row[3] = uPort;
                    row[4] = nLocation;
                    row[5] = uLocType;
                    row[6] = uStatus;
                    table.Rows.Add(row);
                    table.AcceptChanges();
                }
            }
        }

        private void ProcessPosTable(ArrayList item)
        {
            if (4 == item.Count)
            {
                DataTable table = m_ds.Tables["MesPos"];
                UInt32 uPos = UInt32.Parse(item[1].ToString());
                Byte uType = Byte.Parse(item[2].ToString());
                Byte uSpeed = Byte.Parse(item[3].ToString());
                DataRow row = table.Rows.Find(uPos);
                if (null != row)
                {
                    row[TKeyP_Name] = item[0].ToString();
                    row[TKeyP_Type] = uType;
                    row[TKeyP_Speed] = uSpeed;
                    row.AcceptChanges();
                }
                else
                {
                    row = table.NewRow();
                    row[TKeyP_Pos] = uPos;
                    row[TKeyP_Name] = item[0].ToString();
                    row[TKeyP_Type] = uType;
                    row[TKeyP_Speed] = uSpeed;
                    table.Rows.Add(row);
                    table.AcceptChanges();
                }
                m_ds.AcceptChanges();
            }
        }

        #endregion

        #region OHT

        private void InitTableOHTKeyPos()
        {
            DataTable table = m_ds.Tables["OHTKeyPos"];
            if (null == table)
            {
                table = new DataTable("OHTKeyPos");
                table.Columns.Add(TKeyP_Pos, typeof(System.UInt32));
                table.Columns[TKeyP_Pos].AllowDBNull = false;
                table.PrimaryKey = new DataColumn[] { table.Columns[TKeyP_Pos] };
                table.Columns.Add(TKeyP_Name, typeof(System.String));
                table.Columns.Add(TKeyP_Type, typeof(System.Int32));
                table.Columns.Add(TKeyP_Speed, typeof(System.Byte));

                table.AcceptChanges();
                m_ds.Tables.Add(table);
                m_ds.AcceptChanges();
            }
        }

        private void InitTableOHTInfo()
        {
            DataTable table = m_ds.Tables["OHTInfo"];
            if (null == table)
            {
                table = new DataTable("OHTInfo");
                table.Columns.Add("ID", typeof(System.Byte));
                table.Columns["ID"].AllowDBNull = false;
                table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };
                table.Columns.Add("Position", typeof(System.Int32));
                table.Columns.Add("Hand", typeof(System.Byte));
                table.Columns.Add("Status", typeof(System.Byte));
                table.Columns.Add("Alarm", typeof(System.Byte));
                table.Columns.Add("TcpInfo", typeof(System.String));

                table.AcceptChanges();
                m_ds.Tables.Add(table);
                m_ds.AcceptChanges();
            }
        }

        private void ProcessOHTPosTable(ArrayList item)
        {
            DataTable table = m_ds.Tables["OHTKeyPos"];
            if (4 == item.Count)
            {
                UInt32 uPos = UInt32.Parse(item[1].ToString());
                int nType = Int32.Parse(item[2].ToString());
                Byte uSpeed =  Byte.Parse(item[3].ToString());
                DataRow row = table.Rows.Find(uPos);
                if (null != row)
                {
                    row[TKeyP_Name] = item[0].ToString();
                    row[TKeyP_Type] = nType;
                    row[TKeyP_Speed] = uSpeed;
                    row.AcceptChanges();
                }
                else
                {
                    row = table.NewRow();
                    row[TKeyP_Pos] = uPos;
                    row[TKeyP_Name] = item[0].ToString();
                    row[TKeyP_Type] = nType;
                    row[TKeyP_Speed] = uSpeed;
                    table.Rows.Add(row);
                    table.AcceptChanges();
                }
            }
        }

        private void ProcessOHTInfo(ArrayList item)
        {
            if (3 == item.Count)
            {
                OhtInfoData info = new OhtInfoData();
                string strTCP = item[1] + ":" + item[2];
                int nID = Convert.ToInt16(item[0]);

                info.ID = nID;
                info.TcpInfo = strTCP;

                UpdateOhtInfo(info, true);

            }
        }

        private void ProcessOHTPos(ArrayList item)
        {
            if (4 == item.Count)
            {
                OhtInfoData info = new OhtInfoData();
                int nID = Convert.ToInt16(item[0]);
                long nPosition = Convert.ToInt64(item[1]);
                int nHand = Convert.ToInt16(item[2]);
                int nStatus = Convert.ToByte(item[3].ToString());

                info.ID = nID;
                info.Position = nPosition;
                info.Hand = nHand;
                info.Status = nStatus;

                UpdateOhtInfo(info, false);
            }
        }

        private void UpdateOhtInfo(OhtInfoData info, bool bInfo)
        {
            DataTable table = m_ds.Tables["OHTInfo"];
            DataRow row = table.Rows.Find(info.ID);
            if (null != row)
            {
                SetRowOHTInfoData(row, info, bInfo);
                row.AcceptChanges();
            }
            else
            {
                row = table.NewRow();
                row["ID"] = info.ID;
                SetRowOHTInfoData(row, info, bInfo);
                table.Rows.Add(row);
                table.AcceptChanges();
            }
        }

        private void SetRowOHTInfoData(DataRow row, OhtInfoData info, bool bInfo)
        {
           
            if (true == bInfo)
            {
                row["TcpInfo"] = info.TcpInfo;
            }
            else
            {
                row["Position"] = info.Position;
                row["Hand"] = info.Hand;
                row["Status"] = info.Status;
                row["Alarm"] = info.Alarm;
            }
        }

        #endregion
    }
}
