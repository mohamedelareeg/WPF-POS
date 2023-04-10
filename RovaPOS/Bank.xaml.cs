using RovaPOS.UserControlers;
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

namespace RovaPOS
{
    /// <summary>
    /// Interaction logic for POS.xaml
    /// </summary>
    public partial class Bank : Window
    {
        public Bank()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bank_UserControl main_UserControl = new Bank_UserControl();
            Panel_Sell.Child = main_UserControl;
        }
    }
}
