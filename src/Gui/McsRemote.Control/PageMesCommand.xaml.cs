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

namespace McsRemote.Control
{
    /// <summary>
    /// PageMesCommand.xaml 的交互逻辑
    /// </summary>
    public partial class PageMesCommand : Page
    {
        public PageMesCommand()
        {
            InitializeComponent();
        }

        private void pageMesCmd_Unloaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mes Command unloaded.");
        }

        private void pageMesCmd_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mes Command Loaded.");
        }
    }
}
