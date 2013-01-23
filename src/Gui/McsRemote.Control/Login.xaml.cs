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

namespace McsRemote.Control
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        private bool m_isLogin = false;
        private GuiAccess.UserCli userMge = null;
        public GuiAccess.UserCli UserManagement
        {
            get { return userMge; }
            set { userMge = value; }
        }
        public bool IsLogin
        {
            get { return m_isLogin; }
        }
        private int m_nSession = 0;
        public int Session
        {
            get { return m_nSession; }
        }
        private string m_strUserName = "";
        public string UserName
        {
            get { return m_strUserName; }
        }
        private int m_nUserID = 0;
        public int UserID
        {
            get { return m_nUserID; }
        }
        public Login()
        {
            InitializeComponent();
        }

        private void UserLogin()
        {
            string strHash = GuiAccess.UserHash.HashUserInfo(this.tbUserName.Text,
                this.pdbPassword.Password);

            m_nSession = userMge.Login(this.tbUserName.Text, strHash);

            if (m_nSession > 0)
            {
                m_strUserName = this.tbUserName.Text;
                //m_nUserID = userMge.g
                m_isLogin = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed, please check your user name and password.");
            }
        }

        private void bnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserLogin();
        }

        private void bnExit_Click(object sender, RoutedEventArgs e)
        {
            userMge.Logout(m_nSession);
            this.Close();
        }

        private void pdbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UserLogin();
            }
        }
    }
}
