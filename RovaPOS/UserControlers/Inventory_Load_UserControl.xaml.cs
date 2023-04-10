using System;
using System.Collections.Generic;
using System.Data;
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
using RovaPOS.Models;

namespace RovaPOS.UserControlers
{
    /// <summary>
    /// Interaction logic for Inventory_Load_UserControl.xaml
    /// </summary>
    public partial class Inventory_Load_UserControl : UserControl
    {
        Manager.Database.Goods goods = new Manager.Database.Goods();
        List<Models.Goods> goods_list = new List<Models.Goods>();
        public Inventory_Load_UserControl()
        {
            InitializeComponent();
        }

        public List<Goods> Goods_list { get => goods_list; set => goods_list = value; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //DataSet dataSet = new DataSet();
            //DataTable DT = goods.ReadAdapter("goods");
            goods_list = goods.ReadAllGoodsPic("goods");
            GridView1.ItemsSource = goods_list;


        }
    }
}
