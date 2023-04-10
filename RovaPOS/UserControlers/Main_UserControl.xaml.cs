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

namespace RovaPOS.UserControlers
{
    /// <summary>
    /// Interaction logic for Main_UserControl.xaml
    /// </summary>
    public partial class Main_UserControl : UserControl
    {
        public Main_UserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            POS pos = new POS();
            pos.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Sells sells = new Sells();
            sells.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Returned sells = new Returned();
            sells.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PendingSells sells = new PendingSells();
            sells.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Purchases sells = new Purchases();
            sells.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Expenses sells = new Expenses();
            sells.ShowDialog();
        }
    }
}
