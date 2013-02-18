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
            dgOHT.ItemsSource = m_tabOhtInfo.DefaultView;
            dgPos.DataContext = m_dataHub.DataSource.Tables["OHTKeyPos"];

            m_dataHub.DataSetUpdate += m_dataHub_DataSetUpdate;

            PushData[] cmds = new PushData[] { PushData.upOhtInfo, PushData.upOhtPos };
            m_dataHub.Async_SetPushCmdList(cmds);

            m_timer.Interval = 500;
            m_timer.Elapsed += m_timer_Elapsed;
            m_timer.Enabled = true;

            //dgOHT.Items.Refresh();
           // addData();
            
        }

        void m_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
            //addData();
            //dgOHT.Dispatcher.BeginInvoke(new Action(() => dgOHT.Items.Refresh()));
            //dgOHT.Dispatcher.BeginInvoke(new Action(() => addData()));
            //dgOHT.Items.Refresh();
        }

        public void ProcessGuiData()
        {
           // addData();
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
           //addData();
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
                    if (nP % 20 == 1)
                    {
                        row = m_tabOhtInfo.NewRow();
                        row[0] = nP;
                        row[1] = 1;
                        m_tabOhtInfo.Rows.Add(row);
                    }
                    //row.AcceptChanges();
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

           // addData();
            dgOHT.Items.Refresh();
        }
    }
}
