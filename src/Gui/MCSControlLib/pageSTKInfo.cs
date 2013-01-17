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
using MCSControlLib.Common;

namespace MCSControlLib.Page
{
    public partial class pageSTKInfo : baseControlPage, IMcsControlBase
    {
        private const string TKey_ID = "ID";

        private DataTable m_tableStocker = null;

        public pageSTKInfo()
        {
            InitializeComponent();
        }

        public override void PageInit()
        {
            PushData[] cmds = new PushData[] { PushData.upStkInfo };
            m_dataHub.Async_SetPushCmdList(cmds);
        }

        protected override void InitProcessDictionary()
        {
            m_dictProcess.Add(PushData.upStkInfo, ProcessStkInfo);
        }

        private void NotifyMainStockerChange()
        {
            ArrayList stkList = new ArrayList();
            foreach (DataRow row in m_tableStocker.Rows)
            {
                stkList.Add((byte)row[TKey_ID]);
            }
            NotifyDataChange(stkList, null);
        }

         private void ProcessStkInfo(ArrayList item)
        {
             if (item.Count > 1)
             {
                 int nID = TryConver.ToByte(item[0].ToString());
                 DataRow row = m_tableStocker.Rows.Find(nID);
                 if (null != row)
                 {
                     row[TKey_ID] = item[0].ToString();
                     row.AcceptChanges();
                 }
                 else
                 {
                     row = m_tableStocker.NewRow();
                     row[TKey_ID] = nID;
                     m_tableStocker.Rows.Add(row);
                     m_tableStocker.AcceptChanges();
                     NotifyMainStockerChange();
                 }

             }
        }

        private void InitStockerTable()
         {
             if (null == m_tableStocker)
             {
                 m_tableStocker = new DataTable("Stocker");
                 m_tableStocker.Columns.Add(TKey_ID, typeof(System.Byte));
                 m_tableStocker.Columns[TKey_ID].AllowDBNull = false;
                 m_tableStocker.PrimaryKey = new DataColumn[] { m_tableStocker.Columns[TKey_ID] };
                 m_tableStocker.AcceptChanges();
             }
         }

         private void pageSTKInfo_Load(object sender, EventArgs e)
         {
             InitStockerTable();
             dataGridView_Stocker.DataSource = m_tableStocker;
         }
    }
}
