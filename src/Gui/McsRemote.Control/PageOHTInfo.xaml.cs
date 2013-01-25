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
        System.Timers.Timer m_timer = new System.Timers.Timer();
        private void pgOHTInfo_Loaded(object sender, RoutedEventArgs e)
        {
            if (null != m_dataHub)
            {
                m_dataHub.Async_SetCallBack();
            }

            m_tabOhtInfo = m_dataHub.DataSource.Tables["OHTInfo"].Clone();
            dgOHT.DataContext = m_tabOhtInfo;
            dgPos.DataContext = m_dataHub.DataSource.Tables["OHTKeyPos"];

            m_dataHub.DataSetUpdate += m_dataHub_DataSetUpdate;

            PushData[] cmds = new PushData[] { PushData.upOhtInfo, PushData.upOhtPos };
            m_dataHub.Async_SetPushCmdList(cmds);

            
        }

        public void ProcessGuiData()
        {
            addData();
        }

        void m_dataHub_DataSetUpdate(PushData enumPush)
        {
            //switch (enumPush)
            //{
            //    case PushData.upOhtInfo:
            //        dgOHT.Dispatcher.BeginInvoke(new Action(() => dgOHT.Items.Refresh()));
            //        break;
            //    case PushData.upOhtPos:
            //        dgOHT.Dispatcher.BeginInvoke(new Action(() => dgOHT.Items.Refresh()));
            //        break;
            //    case PushData.upOhtPosTable:
            //        dgPos.Dispatcher.BeginInvoke(new Action(() => dgPos.Items.Refresh()));
            //        break;
            //}
           //addData();
        }

        private void addData()
        {
            lock (this)
            {
                if (m_tabOhtInfo.Rows.Count > 0)
                {
                    DataRow row = m_tabOhtInfo.Rows[0];
                    uint nP = UInt32.Parse(row[1].ToString());
                    nP++;
                    row[1] = nP;
                    row.AcceptChanges();
                }
                else
                {
                    DataRow row = m_tabOhtInfo.NewRow();
                    row[0] = 255;
                    row[1] = 1;
                    m_tabOhtInfo.Rows.Add(row);

                    row = m_tabOhtInfo.NewRow();
                    row[0] = 254;
                    row[1] = 1;
                    m_tabOhtInfo.Rows.Add(row);
                    m_tabOhtInfo.AcceptChanges();
                }
            }
        }

     

        private void pgOHTInfo_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void bnGetLoc_Click(object sender, RoutedEventArgs e)
        {

            addData();
            dgOHT.Items.Refresh();
        }
    }
}
