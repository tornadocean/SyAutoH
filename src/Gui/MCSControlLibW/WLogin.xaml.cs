using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MCSControlLibW
{
    /// <summary>
    /// WLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WLogin : Window
    {
        private bool m_bIsLogin = false;
        public bool IsLogin
        {
            get { return m_bIsLogin; }
            set { m_bIsLogin = value; }
        }
        public WLogin()
        {
            InitializeComponent();
        }
    }
}
