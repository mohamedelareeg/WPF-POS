using RovaPOS.Manager;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RovaPOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Server server = new Server();
        
        Manager.Database.Info info = new Manager.Database.Info();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome");
        }

        private void Btn_menu_close_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_menu_close_Click_1(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Welcome");
            //Panel_Menu.Visibility = Visibility.Hidden;
            //Rec_One.Visibility = Visibility.Hidden;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main_UserControl userControl = new Main_UserControl();
            Main_panel.Children.Add(userControl);
            server.CreateDatabase("CREATE TABLE IF NOT EXISTS `info` (`ID`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                "`Name`	TEXT," +
                "`OwnerName`	TEXT," +
                "`OwnerPhone`	TEXT," +
                "`Location`	TEXT," +
                "`Password`	TEXT," +
                "`Managerpass`	TEXT" +
                "); ");
            Label_Owner_name.Content = info.ReadString("info", "Ownername", 1);

       }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.ShowDialog();
        }
    }
}
