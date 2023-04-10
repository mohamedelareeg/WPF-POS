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
    public partial class Purchases_C : Window
    {
        public Purchases_C()
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
            Purchases purchases = new Purchases();
            purchases.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Expenses expenses = new Expenses();
            expenses.ShowDialog();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
