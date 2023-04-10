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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Sell_C : Window
    {
        public Sell_C()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Sells sells = new Sells();
            sells.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            PendingSells psells = new PendingSells();
            psells.ShowDialog();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Returned returned = new Returned();
            returned.ShowDialog();
        }
    }
}
