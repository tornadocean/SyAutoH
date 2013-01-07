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
    public partial class pageMesCommand : baseControlPage, IMcsControlBase
    {
        private const string TKeyP_Pos = "Position";
        private const string TKeyP_Name = "Name";
        private const string TKeyP_Type = "Type";
        private const string TKeyP_Speed = "Speed";

        private DataTable m_tableKeyPos = null;
        private DataTable m_tableFoup = null;

        public pageMesCommand()
        {
            InitializeComponent();
        }

        protected override void InitProcessDictionary()
        {
            m_dictProcess.Add(PushData.upMesPosTable, ProcessPosTable);
            m_dictProcess.Add(PushData.upMesFoupTable, ProcessFoupTable);
        }

        private void InitKeyPosTable()
        {
            if (null == m_tableKeyPos)
            {
                m_tableKeyPos = new DataTable("KeyPos");
                m_tableKeyPos.Columns.Add(TKeyP_Pos, typeof(System.UInt32));
                m_tableKeyPos.Columns[TKeyP_Pos].AllowDBNull = false;
                m_tableKeyPos.PrimaryKey = new DataColumn[] { m_tableKeyPos.Columns[TKeyP_Pos] };
                m_tableKeyPos.Columns.Add(TKeyP_Name, typeof(System.String));
                m_tableKeyPos.Columns.Add(TKeyP_Type, typeof(System.Byte));
                m_tableKeyPos.Columns.Add(TKeyP_Speed, typeof(System.Byte));

                m_tableKeyPos.AcceptChanges();
            }
        }

        private void InitFoupTable()
        {
            if (null == m_tableFoup)
            {
                m_tableFoup = new DataTable("Foup");
                m_tableFoup.Columns.Add("BarCode", typeof(System.UInt32));
                m_tableFoup.Columns["BarCode"].AllowDBNull = false;
                m_tableFoup.PrimaryKey = new DataColumn[] { m_tableFoup.Columns["BarCode"] };
                m_tableFoup.Columns.Add("Lot", typeof(System.UInt32));
                m_tableFoup.Columns.Add("Carrier", typeof(System.UInt32));
                m_tableFoup.Columns.Add("Port", typeof(System.UInt32));
                m_tableFoup.Columns.Add("Location", typeof(System.Int32));
                m_tableFoup.Columns.Add("LocType", typeof(System.UInt32));
                m_tableFoup.Columns.Add("Status", typeof(System.UInt32));

                m_tableFoup.AcceptChanges();
            }
        }

        private void ProcessFoupTable(ArrayList item)
        {
            if (7 == item.Count)
            {
                UInt32 uBarCode = TryConver.ToUInt32(item[0].ToString());
                UInt32 uLot = TryConver.ToUInt32(item[1].ToString());
                UInt32 uCarrier = TryConver.ToUInt32(item[2].ToString());
                uint uPort = TryConver.ToUInt32(item[3].ToString());
                int nLocation = TryConver.ToInt32(item[4].ToString());
                uint uLocType = TryConver.ToUInt32(item[5].ToString());
                uint uStatus = TryConver.ToUInt32(item[6].ToString());
                DataRow row = m_tableFoup.Rows.Find(uBarCode);
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
                    row = m_tableFoup.NewRow();
                    row[0] = uBarCode;
                    row[1] = uLot;
                    row[2] = uCarrier;
                    row[3] = uPort;
                    row[4] = nLocation;
                    row[5] = uLocType;
                    row[6] = uStatus;
                    m_tableFoup.Rows.Add(row);
                    m_tableFoup.AcceptChanges();
                }

            }
        }

        private void ProcessPosTable(ArrayList item)
        {
            if (4 == item.Count)
            {
                UInt32 uPos = Convert.ToUInt32(item[1]);
                Byte uType = TryConver.ToByte(item[2].ToString());
                Byte uSpeed = TryConver.ToByte(item[3].ToString());
                DataRow row = m_tableKeyPos.Rows.Find(uPos);
                if (null != row)
                {
                    row[TKeyP_Name] = item[0].ToString();
                    row[TKeyP_Type] = uType;
                    row[TKeyP_Speed] = uSpeed;
                    row.AcceptChanges();
                }
                else
                {
                    row = m_tableKeyPos.NewRow();
                    row[TKeyP_Pos] = uPos;
                    row[TKeyP_Name] = item[0].ToString();
                    row[TKeyP_Type] = uType;
                    row[TKeyP_Speed] = uSpeed;
                    m_tableKeyPos.Rows.Add(row);
                    m_tableKeyPos.AcceptChanges();
                }
            }
        }

        private void linkLabelFoupRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int nWRet = m_dataHub.WriteData(GuiCommand.MesGetFoupTable, "");
        }

        private void linkLabelLocationRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // get keypoints where type = 0x20, (pick and place)
            int nWRet = m_dataHub.WriteData(GuiCommand.MesGetPosTable, "");
        }

        private void pageMesCommand_Load(object sender, EventArgs e)
        {
            InitKeyPosTable();
            InitFoupTable();

            dataGridViewKeyPos.DataSource = m_tableKeyPos;
            dataGridViewFoup.DataSource = m_tableFoup;
        }

        private void dataGridViewKeyPos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewKeyPos.SelectedRows;
            if (rows.Count > 0)
            {
                DataGridViewRow row = rows[0];
                tbLocPosition.Text = row.Cells[0].Value.ToString();
                tbLocType.Text = row.Cells[2].Value.ToString();
                tbLocName.Text = row.Cells[1].Value.ToString();
            }
        }

        private void dataGridViewFoup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewFoup.SelectedRows;
            if (rows.Count > 0)
            {
                DataGridViewRow row = rows[0];
                tbFoupBarCode.Text = row.Cells[0].Value.ToString();
                tbFoupLot.Text = row.Cells[1].Value.ToString();
                tbFoupStatus.Text = row.Cells[4].Value.ToString();
                MCS.dbcli.LoctionType nLocType = (MCS.dbcli.LoctionType)TryConver.ToInt32(row.Cells[5].Value.ToString());
                switch(nLocType)
                {
                    case MCS.dbcli.LoctionType.loctypeStocker:
                        tbFoupLocType.Text = "Stocker";
                        break;
                    case MCS.dbcli.LoctionType.loctypeOHT:
                        tbFoupLocType.Text = "OHT";
                        break;
                    default:
                        tbFoupLocType.Text = "Unknown";
                        break;
                }
                tbFoupLocation.Text = row.Cells[2].Value.ToString();
            }
        }

        private void bnFoupMove_Click(object sender, EventArgs e)
        {

        }

        private void bnFoupMovePause_Click(object sender, EventArgs e)
        {

        }

        private void bnFoupMoveContinue_Click(object sender, EventArgs e)
        {

        }

        private void bnFoupMoveStop_Click(object sender, EventArgs e)
        {

        }
    }
}
