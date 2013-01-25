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
        private Dictionary<string, Page> m_dyPage = new Dictionary<string, Page>();
        

        public MainPad()
        {
            InitializeComponent();
        }

        private void InitDictoryPage()
        {
            m_dyPage.Add("tviMESCommand", new PageMesCommand() );
            m_dyPage.Add("tviOHTInfo", new PageOHTInfo() );
            m_dyPage.Add("tviStockerInfo", new PageStockerInfo() );
            m_dyPage.Add("tviForkInfo", new PageForkInfo() );
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

        private System.Timers.Timer m_timer = new System.Timers.Timer();

        private IPageGuiAccess pga = null;
        private void NavigateToPage(string strPageID)
        {
            Page page = null;
            bool bGet = m_dyPage.TryGetValue(strPageID, out page);
            if (null != page)
            {
                pga = page as IPageGuiAccess;
                if (null != pga)
                {
                    pga.DataHub = m_dataHub;
                }
                framePage.Navigate(page); 
            }
        }

        private void tvCommand_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null)
            {
                TreeViewItem secItem = e.NewValue as TreeViewItem; 
                string strName = secItem.Name;

                NavigateToPage(strName);
            }
           
        }

        private void winMainPad_Loaded(object sender, RoutedEventArgs e)
        {
            m_dataHub.ConnectServer();
            //toolStripStatusLabel_User.Text = "Logined User: " + m_strUserName + " ";
            //m_dataHub.DataUpdater += new GuiAccess.DataUpdaterHander(GuiDataUpdate);
            m_dataHub.Async_SetCallBack();
            m_dataHub.Session = m_nSession;

            InitDictoryPage();

            NavigateToPage("tviOHTInfo");

            TreeViewItem newItem = new TreeViewItem();
            newItem.Name = "tvSockerOperation";
            newItem.Header = "Socker 100";
            tviStockerInfo.Items.Add(newItem);

            m_timer.Interval = 500;
            m_timer.Elapsed += m_timer_Elapsed;
            m_timer.Enabled = true;
        }

        void m_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (null != pga)
            {
                pga.ProcessGuiData();
            }
        }
    }
}
