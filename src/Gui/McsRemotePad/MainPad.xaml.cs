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
using System.Windows.Shapes;
using McsRemote.Control;

namespace McsRemotePad
{
    /// <summary>
    /// MainPad.xaml 的交互逻辑
    /// </summary>
    public partial class MainPad : Window
    {
        public int Session
        {
            get { return m_nSession; }
            set { m_nSession = value; }
        }
       
        public string UserName
        {
            get { return m_strUserName; }
            set { m_strUserName = value; }
        }
       
        public bool NeedLogin
        {
            get { return m_bNeedLogin; }
        }
 
        private string m_strUserName = "";
        private bool m_bNeedLogin = false;
        //private long m_ltime64 = 0;
        private int m_nSession = -1;
        private GuiAccess.DataHubCli m_dataHub = new GuiAccess.DataHubCli();

        public MainPad()
        {
            InitializeComponent();
        }

        private void muLogout_Click(object sender, RoutedEventArgs e)
        {
            m_bNeedLogin = true;
            this.Close();
        }

        private void muClose_Click(object sender, RoutedEventArgs e)
        {
            m_bNeedLogin = false;
            this.Close();
        }

        private void tvCommand_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem secItem = e.NewValue as TreeViewItem;
            string strHead = secItem.Name;
            Uri uriPage = null;
            if(strHead.CompareTo("tviMESCommand") == 0)
            {
                //uriPage = new Uri("pack://application:,,,/McsRemote.Control;component/PageMesCommand.xaml");
                //PageMesCommand pMesCmd = new PageMesCommand();
                //pMesCmd.DataHub = m_dataHub;
                //framePage.Navigate(pMesCmd);
                Page pp = new PageMesCommand();
                IPageGuiAccess ip = pp as IPageGuiAccess;
                ip.DataHub = m_dataHub;
                framePage.Navigate(pp);
            }
            else if (strHead.CompareTo("tviOHTInfo") == 0)
            {
                uriPage = new Uri("pack://application:,,,/McsRemote.Control;component/PageOHTInfo.xaml");
            }
            else if (strHead.CompareTo("tviStockerInfo") == 0)
            {
                uriPage = new Uri("pack://application:,,,/McsRemote.Control;component/PageStockerInfo.xaml");
            }
            else if (strHead.CompareTo("tviForkInfo") == 0)
            {
                uriPage = new Uri("pack://application:,,,/McsRemote.Control;component/PageForkInfo.xaml");
            }

            if(null != uriPage)
            {
                framePage.Navigate(uriPage);
            }
        }

        private void winMainPad_Loaded(object sender, RoutedEventArgs e)
        {
            m_dataHub.ConnectServer();
            //toolStripStatusLabel_User.Text = "Logined User: " + m_strUserName + " ";
            //m_dataHub.DataUpdater += new GuiAccess.DataUpdaterHander(GuiDataUpdate);
            m_dataHub.Async_SetCallBack();
            m_dataHub.Session = m_nSession;
        }
    }
}
