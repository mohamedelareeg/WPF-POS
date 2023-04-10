using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    /// Interaction logic for POS_UserControl.xaml
    /// </summary>
    public partial class POS_UserControl : UserControl
    {
        Manager.Database.PendingSell pendingSell = new Manager.Database.PendingSell();
        List<Models.PendingSell> list_pending = new List<Models.PendingSell>();
      
        public POS_UserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            POS_Categories_UserControl main_UserControl = new POS_Categories_UserControl();
            Panel_Cata.Child = main_UserControl;
            POS_ADDBILL main_UserControl_2 = new POS_ADDBILL();
            Panel_bill.Child = main_UserControl_2;

            DataSet dataSet = new DataSet();
            //DataTable DT = Info.ReadAdapter("sells");
            //list_pending = pendingSell.ReadPendingSell("sells");
            //MessageBox.Show(list_pending.Count.ToString());
            //pendingListViewModel = new Manager.PendingListViewModel(list_pending);
            //DataContext = pendingListViewModel;
            //if (pendingListViewModel.ConnectionItems.Count > 0)
            //{
            //    //GridView1.ItemsSource = DT.AsDataView();
            //    GridView1.Columns[0].Visibility = Visibility.Hidden;
            //    GridView1.Columns[1].Header = "اسم السلعة";
            //    GridView1.Columns[2].Visibility = Visibility.Hidden;
            //    GridView1.Columns[3].Header = "الكمية";
            //    GridView1.Columns[4].Visibility = Visibility.Hidden;
            //    GridView1.Columns[5].Header = "سعر البيع";
            //    GridView1.Columns[6].Visibility = Visibility.Hidden;
            //    GridView1.Columns[7].Visibility = Visibility.Hidden;
            //    GridView1.Columns[8].Visibility = Visibility.Hidden;
            //    GridView1.Columns[9].Visibility = Visibility.Hidden;
            //    GridView1.Columns[10].Visibility = Visibility.Hidden;
            //    GridView1.Columns[11].Visibility = Visibility.Hidden;

            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
              var mywindow = Window.GetWindow(this);
            mywindow.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //pendingListViewModel.ConnectionItems[0].Name = "new ping :)";
        }
    }
}
