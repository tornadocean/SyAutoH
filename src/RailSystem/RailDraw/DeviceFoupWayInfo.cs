using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RailDraw
{
    public partial class DeviceFoupWayInfo : Form
    {
        private const string Key_Name = "foupWayName";
        private const string Key_Coding = "foupWayCoding";
        private const string Key_Point = "foupPoing";
        private DataTable m_tabelFoupWay = null;
        List<Mcs.RailSystem.Common.EleFoupDot> listFoupWay = new List<Mcs.RailSystem.Common.EleFoupDot>();

        public DeviceFoupWayInfo()
        {
            InitializeComponent();
        }

        private void DeviceFoupWayInfo_Load(object sender, EventArgs e)
        {
            InitDeviceForm();
        }


        private void InitDeviceForm()
        {
            m_tabelFoupWay = new DataTable("Foups");
            m_tabelFoupWay.Columns.Add(Key_Name, typeof(string));
            m_tabelFoupWay.Columns[Key_Name].AllowDBNull = false;
            m_tabelFoupWay.PrimaryKey = new DataColumn[] { m_tabelFoupWay.Columns[Key_Name] };
            m_tabelFoupWay.Columns.Add(Key_Coding, typeof(Int32));
            m_tabelFoupWay.Columns.Add(Key_Point, typeof(Point));
            Mcs.RailSystem.Common.EleDevice device = (Mcs.RailSystem.Common.EleDevice)(((FatherWindow)(this.Owner)).drawDocOp.LastHitedObject);
            listFoupWay.AddRange(device.ListFoupDot);
            DataRow row;
            foreach(Mcs.RailSystem.Common.EleFoupDot obj in listFoupWay)
            {
                row = m_tabelFoupWay.NewRow();
                row[Key_Name] = obj.railText;
                row[Key_Coding] = obj.CodingScratchDot;
                row[Key_Point] = obj.PtScratchDot;
                m_tabelFoupWay.Rows.Add(row);
            }
            dataGridViewDeviceWay.DataSource = m_tabelFoupWay;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


    }
}
