using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MCS.GuiHub;

namespace McsRemote.Control
{
    /// <summary>
    /// PageOHTInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PageOHTInfo : Page, IPageGuiAccess
    {
        public PageOHTInfo()
        {
            InitializeComponent();
        }

        protected GuiAccess.DataHubCli m_dataHub = null;
        public GuiAccess.DataHubCli DataHub
        {
            set
            {
                m_dataHub = value;
            }
            get
            {
                return m_dataHub;
            }
        }

        DataTable m_tabOhtInfo = null;
        private void pgOHTInfo_Loaded(object sender, RoutedEventArgs e)
        {
            if (null != m_dataHub)
            {
                m_dataHub.Async_SetCallBack();
            }

            m_tabOhtInfo = m_dataHub.DataSource.Tables["OHTInfo"].Clone();
            dgOHT.ItemsSource = m_tabOhtInfo.DefaultView;
            dgPos.DataContext = m_dataHub.DataSource.Tables["OHTKeyPos"];

            m_dataHub.DataSetUpdate += m_dataHub_DataSetUpdate;

            PushData[] cmds = new PushData[] { PushData.upOhtInfo, PushData.upOhtPos };
            m_dataHub.Async_SetPushCmdList(cmds);
        }

        void m_dataHub_DataSetUpdate(PushData enumPush)
        {
            switch (enumPush)
            {
                case PushData.upOhtInfo:
                    dgOHT.Dispatcher.BeginInvoke(new Action(() => Process_upOhtInfo()));
                    break;
                case PushData.upOhtPos:
                    dgOHT.Dispatcher.BeginInvoke(new Action(() => Process_upOhtInfo()));
                    break;
                case PushData.upOhtPosTable:
                    dgPos.Dispatcher.BeginInvoke(new Action(() => dgPos.Items.Refresh()));
                    break;
            }
        }

        public void ProcessGuiData()
        {

        }

        private void Process_upOhtInfo()
        {
            DataTable dt = m_dataHub.DataSource.Tables["OHTInfo"];
            foreach (DataRow row in dt.Rows)
            {
               uint nID = UInt32.Parse( row[0].ToString() );
               DataRow rFind =  m_tabOhtInfo.Rows.Find(nID);
               int nCount = dt.Columns.Count;
               if (null != rFind)
               {
                   for (int i = 1; i < nCount; i++)
                   {
                       rFind[i] = row[i];
                   }
               }
               else
               {
                   DataRow rNew = m_tabOhtInfo.NewRow();
                   rNew[0] = row[0];
                   for (int i = 1; i < nCount; i++)
                   {
                       rNew[i] = row[i];
                   }
                   m_tabOhtInfo.Rows.Add(rNew);
                   m_tabOhtInfo.AcceptChanges();
               }
            }
        }

        private void pgOHTInfo_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void bnGetLoc_Click(object sender, RoutedEventArgs e)
        {
            m_dataHub.Async_WriteData(GuiCommand.OhtGetPosTable, "");
        }

        private void tbOHTID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbOHTID.Text != "254")
            {
                cbAllOHT.IsChecked = false;
            }
            else
            {
                cbAllOHT.IsChecked = true;
            }
        }

        private void bnSetFrom_Click(object sender, RoutedEventArgs e)
        {
            int nSel = dgPos.SelectedIndex;
            if ((nSel < dgPos.Items.Count) && (nSel >= 0))
            {
                string strPos = GetDataGridText(nSel, 0);
                tbFrom.Text = strPos;
            }
            else
            {
                MessageBox.Show("Select the position for FROM.");
            }

        }

        private string GetDataGridText(int nIndex, int Col)
        {
            DataRowView obItem = dgPos.Items.GetItemAt(nIndex) as DataRowView;
            if (null != obItem)
            {
                string str = obItem[Col].ToString();
                return str;
            }
            else
            {
                return null;
            }
        }

        private void bnSetTo_Click(object sender, RoutedEventArgs e)
        {
            int nSel = dgPos.SelectedIndex;
            if ((nSel < dgPos.Items.Count) && (nSel >= 0))
            {
                string strPos = GetDataGridText(nSel, 0);
                tbTo.Text = strPos;
            }
            else
            {
                MessageBox.Show("Select the position for TO.");
            }
        }

        private void cbAllOHT_Checked(object sender, RoutedEventArgs e)
        {
            tbOHTID.Text = "254";
        }
    }
}
