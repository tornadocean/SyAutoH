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

        private string strFoupSelected = "";
        private string strLocationSelected = "";

        private void pageMesCmd_Unloaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Mes Command unloaded.");
        }

        public void ProcessGuiData()
        {
        }

        private void pageMesCmd_Loaded(object sender, RoutedEventArgs e)
        {
            if (null != m_dataHub)
            {
                m_dataHub.Async_SetCallBack();
            }

            dgFoup.DataContext = m_dataHub.DataSource.Tables["MesFoup"];
            dgLocation.DataContext = m_dataHub.DataSource.Tables["MesPos"];
            m_dataHub.DataSetUpdate += m_dataHub_DataSetUpdate;

            m_dataHub.Async_WriteData(GuiCommand.MesGetFoupTable, "");
            m_dataHub.Async_WriteData(GuiCommand.MesGetPosTable, "");
        }

        void m_dataHub_DataSetUpdate(PushData enumPush)
        {
            switch (enumPush)
            {
                case PushData.upMesFoupTable:
                    dgFoup.Dispatcher.BeginInvoke(new Action(() => dgFoup.Items.Refresh()));
                    break;
                case PushData.upMesPosTable:
                    dgLocation.Dispatcher.BeginInvoke(new Action(() => dgLocation.Items.Refresh()));
                    break;
            }
        }

        private void bnGetFoup_Click(object sender, RoutedEventArgs e)
        {
            m_dataHub.Async_WriteData(GuiCommand.MesGetFoupTable, "");
        }

        private void bnGetLocation_Click(object sender, RoutedEventArgs e)
        {
            m_dataHub.Async_WriteData(GuiCommand.MesGetPosTable, "");
        }

        private void dgFoup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView it in e.AddedItems)
            {
                strFoupSelected = it.Row[0].ToString();
                tbSelFoup.Text = strFoupSelected;
                break;
            }
           
        }

        private void dgLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView it in e.AddedItems)
            {
                strLocationSelected = it.Row[0].ToString();
                if (strLocationSelected.Length > 0)
                {
                    tbSelLocation.Text = strLocationSelected;
                }
             
                break;
            }
        }

        private void bnGo_Click(object sender, RoutedEventArgs e)
        {
            if (strFoupSelected.Length <= 0)
            {
                MessageBox.Show("No Foup Selected.");
                return;
            }

            if (strLocationSelected.Length <= 0)
            {
                MessageBox.Show("No Location Selected.");
                return;
            }

            string strFoupMove = string.Format("<{0},{1}>", strFoupSelected, strLocationSelected);
            m_dataHub.Async_WriteData(GuiCommand.MesFoupTransfer, strFoupMove);
        }
    }
}
