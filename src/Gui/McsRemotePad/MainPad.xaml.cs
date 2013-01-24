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

    }
}
