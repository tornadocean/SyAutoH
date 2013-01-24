using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCSControlLib;
using McsRemote.Control;

namespace MCSControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           try
           {
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
