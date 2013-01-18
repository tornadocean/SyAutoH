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

namespace MCSControlW
{
    /// <summary>
    /// WBackForm.xaml 的交互逻辑
    /// </summary>
    public partial class WBackForm : Window
    {
        public WBackForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                GuiAccess.UserCli userLink = new GuiAccess.UserCli();
                userLink.ConnectServer();
                bool bNeedLogin = true;
                while (true == bNeedLogin)
                {
                    LoginForm login = new LoginForm();
                    login.UserManagement = userLink;
                    login.ShowDialog();
                    if (login.IsLogin == false)
                    {
                        this.Close();
                        bNeedLogin = false;
                    }
                    else
                    {
                        try
                        {
                            MainForm mainForm = new MainForm();
                            mainForm.UserName = login.UserName;
                            mainForm.Session = login.Session;
                            mainForm.ShowDialog();
                            userLink.Logout(mainForm.Session);
                            bNeedLogin = mainForm.NeedLogin;
                        }
                        catch (System.Exception ex)
                        {
                            string strEx;
                            strEx = ex.Message;
                            strEx += "\r\n";
                            strEx += ex.StackTrace;
                            MessageBox.Show(strEx);
                            MessageBox.Show("System will be restarted.");
                        }
                    }
                }

                userLink.Disconnect();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
