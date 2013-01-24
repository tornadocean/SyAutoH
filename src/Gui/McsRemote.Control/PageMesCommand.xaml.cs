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

        public PageMesCommand(GuiAccess.DataHubCli dc)
        {
            InitializeComponent();
            m_dataHub = dc;
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
         
        }
    }
}
