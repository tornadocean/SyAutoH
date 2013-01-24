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
using System.Windows.Navigation;
using System.Windows.Shapes;
using McsRemote.Control;

namespace McsRemotePad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                GuiAccess.UserCli userLink = new GuiAccess.UserCli();
                userLink.ConnectServer();
                bool bNeedLogin = true;
                while (true == bNeedLogin)
                {
                    LoginWindow login = new LoginWindow();
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
                            MainPad mainPad = new MainPad();
                            mainPad.UserName = login.UserName;
                            mainPad.Session = login.Session;
                            mainPad.ShowDialog();
                            userLink.Logout(mainPad.Session);
                            bNeedLogin = mainPad.NeedLogin;
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
