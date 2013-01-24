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
    /// PageMesCommand.xaml 的交互逻辑
    /// </summary>
    public partial class PageMesCommand : Page, IPageGuiAccess
    {
        public PageMesCommand()
        {
            InitializeComponent();
        }

        public PageMesCommand(GuiAccess.DataHubCli dhCli)
        {
            InitializeComponent();
            m_dataHub = dhCli;
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

        private void pageMesCmd_Unloaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Mes Command unloaded.");
        }

        private void pageMesCmd_Loaded(object sender, RoutedEventArgs e)
        {
            if (null != m_dataHub)
            {
                m_dataHub.Async_SetCallBack();
            }

            dgFoup.DataContext = m_dataHub.DataSource.Tables["MesFoup"];
            dgLocation.DataContext = m_dataHub.DataSource.Tables["MesPos"];
            m_dataHub.DataUpdater += m_dataHub_DataUpdater;
        }

        void m_dataHub_DataUpdater(long lTime, MCS.GuiDataItem item)
        {
            dgFoup.Dispatcher.BeginInvoke(new Action(() => dgFoup.Items.Refresh()));
            dgLocation.Dispatcher.BeginInvoke(new Action(() => dgLocation.Items.Refresh()));
        }

        private void bnGetFoup_Click(object sender, RoutedEventArgs e)
        {
            m_dataHub.Async_WriteData(GuiCommand.MesGetFoupTable, "");
        }

        private void bnGetLocation_Click(object sender, RoutedEventArgs e)
        {
            m_dataHub.Async_WriteData(GuiCommand.MesGetPosTable, "");
        }
    }
}
