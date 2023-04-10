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
    public partial class Main : Window
    {
        Manager.Database.Info info = new Manager.Database.Info();
        public Main()
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

        private void Btn_POS_Click(object sender, RoutedEventArgs e)
        {
            POS pos = new POS();
            pos.ShowDialog();
        }

        private void Btn_Inventory_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.ShowDialog();
        }

        private void Btn_Sells_Click(object sender, RoutedEventArgs e)
        {
            Sell_C sell_c = new Sell_C();
            sell_c.ShowDialog();
        }

        private void Btn_Purchase_Click(object sender, RoutedEventArgs e)
        {
            Purchases_C purchases_C = new Purchases_C();
            purchases_C.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Label_Owner.Content = info.ReadString("info", "Name", 1);
        }

        private void Btn_Bank_Click(object sender, RoutedEventArgs e)
        {
            Bank bank = new Bank();
            bank.ShowDialog();
        }

        private void Btn_Customers_Click(object sender, RoutedEventArgs e)
        {
            Customers_C customers_C = new Customers_C();
            customers_C.ShowDialog();
        }

        private void Btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            Backup backup = new Backup();
            backup.ShowDialog();
        }
    }
}
