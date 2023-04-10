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
    public partial class POS : Window
    {
        public POS()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            POS_Shop_UserControl main_UserControl = new POS_Shop_UserControl();
            Panel_Sell.Child = main_UserControl;
        }
    }
}
