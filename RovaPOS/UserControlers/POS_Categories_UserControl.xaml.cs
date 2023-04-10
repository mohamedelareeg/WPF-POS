using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for POS_Categories_UserControl.xaml
    /// </summary>
    public partial class POS_Categories_UserControl : UserControl
    {
        int rowIndex = -1;
        #region categories
            List<Models.Categories> cata_list = new List<Models.Categories>();
            Manager.Database.Categories categories = new Manager.Database.Categories();
        #endregion
        #region goods
            List<Models.Goods> goods_list = new List<Models.Goods>();
            Manager.Database.Goods goods = new Manager.Database.Goods();
        #endregion
        #region PendingSells
        Manager.Database.PendingSell pendingSell = new Manager.Database.PendingSell();
        List<Models.PendingSell> list_pending = new List<Models.PendingSell>();
        Manager.PendingListViewModel pendingListViewModel;
        #endregion
       
        // Create the `DataTable` structure according to your data source
        DataTable dt = new DataTable();
        DataView dv = new DataView();

        Manager.ImagenSQLite ImagenSQLite = new Manager.ImagenSQLite();
        public List<Models.PendingSell> pending_goods = new List<Models.PendingSell>();
        private string name_G;
        private double quantity;
        private double cost;
        private double price;
        private string barcode;
        private double earned;

        #region Getter/Setter
           
            public double Quantity { get => quantity; set => quantity = value; }
            public double Cost { get => cost; set => cost = value; }
            public double Price { get => price; set => price = value; }

            public string Barcode { get => barcode; set => barcode = value; }
            public double Earned { get => earned; set => earned = value; }
        public string Name_G { get => name_G; set => name_G = value; }

        #endregion


        public POS_Categories_UserControl()
        {
            InitializeComponent();
            cata_list = categories.ReadCategoriesPic("categories");

            list_pending = pendingSell.ReadPendingSell("sells");
            //MessageBox.Show(list_pending.Count.ToString());




            DataTable DT = pendingSell.ReadAdapter("sells");
            pendingListViewModel = new Manager.PendingListViewModel(cata_list, pending_goods);
            DataContext = pendingListViewModel;
            //if (pendingListViewModel.ConnectionItems.Count > 0)
            //{

            //    GridView1.ItemsSource = DT.AsDataView();
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
        //Generate Data Filling A List
        private List<Models.Categories> GenerateData()
        {
            cata_list = new List<Models.Categories>();
            {
                new Models.Categories(1,"Mohaa","");
            }
            return cata_list;
        }
        //Transfer The Data  From List To DataTable
        private void fillDataTable(List<Models.Categories> categories)
        {
            foreach (var item in categories)
            {
                dt.Rows.Add(item.Id,item.Name,item.Type,item.Image);
            }
        }
        private void bindlist_cata()
        {
            //string filename = "D:/Mohamed/Projects/DesktopApplication/RovaPOS/RovaPOS/RovaPOS/Resources/product.png";

            //goods.ReadGoodsPic("goods");
            //byte[] photo = File.ReadAllBytes(filename);
            cata_list = categories.ReadCategoriesPic("categories");
            //images.Add(new Models.Goods(DT.get) { Name = "Goods" , Category = filename });
            lsNames.ItemsSource = cata_list;

            //dt = new DataTable();
            //dt.Columns.Add("ID", typeof(string));
            //dt.Columns.Add("Name", typeof(string));
            //dt.Columns.Add("Type", typeof(string));
            //dt.Columns.Add("Image", typeof(string));

            //fillDataTable(cata_list);
            //dv = new DataView();
            //populateListView(dv);
            // Iterate through data source object and fill the table

        }
        public void bindlist_goods(string cata)
        {
            //string filename = "D:/Mohamed/Projects/DesktopApplication/RovaPOS/RovaPOS/RovaPOS/Resources/product.png";

            //goods.ReadGoodsPic("goods");
            //byte[] photo = File.ReadAllBytes(filename);
            goods_list = goods.ReadGoodsPic("goods", cata);
            //images.Add(new Models.Goods(DT.get) { Name = "Goods" , Category = filename });
            ListViewGoods.ItemsSource = goods_list;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");
        }
        public class Images
        {
            public string ImageSource { get; set; }
        }

        private void MySelectionChanged_Cat(object sender, SelectionChangedEventArgs e)
        {
            //String text = lsNames.SelectedItems[0].ToString();
            //var item = lsNames.SelectedItem;
            //MessageBox.Show(item.ToString());

            
            Models.Categories result = cata_list.Find(x => x.Name == pendingListViewModel.SearchValue);//lambda expression
            if (lsNames.SelectedIndex != -1)
            {
                if(pendingListViewModel.SearchValue ==null)
                {
                    //MessageBox.Show(cata_list[lsNames.SelectedIndex].Name);
                    bindlist_goods(cata_list[lsNames.SelectedIndex].Name);
                  
                }
                else
                {
                    //MessageBox.Show(result.Name);
                    bindlist_goods(result.Name);
                   
                }
             

                //bindlist_goods(employeeListViewModel.EmployeeList[lsNames.SelectedIndex].Name);
            }

            //MessageBox.Show("Selected: {0}", e.AddedItems[0]);
        }

        private void MySelectionChanged_Goods(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewGoods.SelectedIndex != -1)
            {
                resetSelection();
                barcode = goods_list[ListViewGoods.SelectedIndex].Barcode;
                MessageBox.Show(barcode);
                Models.PendingSell result_pending = pending_goods.Find(x => x.Barcode == barcode);//lambda expression
                if(pending_goods.Count > 0)
                {
                    if (result_pending.Barcode != null)
                    {
                        Name_G = result_pending.Name;
                        quantity = result_pending.Quantity;
                        cost = result_pending.Cost;
                        price = result_pending.Price;
                        earned = result_pending.Earned;
                    }
                }
              

                else
                {
                    Name_G = goods_list[ListViewGoods.SelectedIndex].Name;
                    quantity = goods_list[ListViewGoods.SelectedIndex].Quantity;
                    cost = goods_list[ListViewGoods.SelectedIndex].Cost;
                    price = goods_list[ListViewGoods.SelectedIndex].Price;
                    earned = goods_list[ListViewGoods.SelectedIndex].Earned;
                }

                MessageBox.Show(Name_G);
                TXT_Price.Text = price.ToString();
            }
               
        }
        //Perform Filtering
        private void TXT_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //dv.RowFilter = string.Format("Name Like '%{0}%'", TXT_Search.Text);
            //populateListView(dv);
        }
        //Fill ListView From DataView
        private void populateListView(DataView dv)
        {
            //if(ListViewCata.Items.Count > 0)
            //{

            //    ListViewCata.Items.Clear();
            //}
            //ListViewCata.Items.Clear();
            string[] product_info = new string[7];

            foreach (DataRow row in dv.ToTable().Rows)
            {
                product_info[1] = (row[0].ToString());

                product_info[2] = (row[1].ToString());
                product_info[3] = (row[2].ToString());
                product_info[4] = (row[3].ToString());
                this.lsNames.Items.Add(new ListViewItem { Content = product_info[1] + product_info[2] + product_info[3] + product_info[4] });

            }
        }
        public double GetDoubleValue(TextBox tb)
        {
            try
            {
                return Convert.ToDouble(tb.Text);
            }
            catch (Exception ex)
            {
                //This is called if the converting failed for some reason
            }

            return 0; //This should only return 0 if the textbox does not contain a valid integer value
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if(rowIndex != -1)
            //{
            Models.PendingSell result_pending = pending_goods.Find(x => x.Barcode == barcode);//lambda expression
            if (pending_goods.Count > 0)
            {
                if (result_pending.Barcode != null)
                {
                    result_pending.Quantity = GetDoubleValue(TXT_QUA);
                    result_pending.Price = GetDoubleValue(TXT_Price);
                    result_pending.Details = TXT_Details.Text;
                    notifydatasetchanged();
                }
            }
            else
            {
                var goods_List = new Models.PendingSell();
                goods_List.Name = Name_G;
                goods_List.Quantity = Quantity;
                goods_List.Cost = cost;
                goods_List.Price = price;
                goods_List.Barcode = barcode;
                goods_List.Earned = earned;
                pending_goods.Add(goods_List);
                notifydatasetchanged();
            }
                    //pendingListViewModel.ConnectionItems[rowIndex].Quantity = GetDoubleValue(TXT_QUA);
                    //pendingListViewModel.ConnectionItems[rowIndex].Price = GetDoubleValue(TXT_Price);
                    //pendingListViewModel.ConnectionItems[rowIndex].Details = TXT_Details.Text;
                //}
          
        }

        private void GridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = sender as DataGrid;
            if (dg == null) return;
            var index = dg.SelectedIndex;
            //here we get the actual row at selected index
            DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;

            //here we get the actual data item behind the selected row
            var item = dg.ItemContainerGenerator.ItemFromContainer(row);

            //rowIndex = GridView1.SelectedIndex;
            TXT_QUA.Text = (pendingListViewModel.ConnectionItems[index].Quantity).ToString();
            TXT_Price.Text = (pendingListViewModel.ConnectionItems[index].Price).ToString();
            TXT_Details.Text = pendingListViewModel.ConnectionItems[index].Details;
            //MessageBox.Show(pendingListViewModel.ConnectionItems[rowIndex].Barcode.ToString());
        }

        private void Button_Click_close(object sender, RoutedEventArgs e)
        {
            var mywindow = Window.GetWindow(this);
            mywindow.Close();
        }
        public void resetSelection()
        {
            Name_G = "";
            quantity = 0;
            cost = 0;
            price = 0;
            barcode = "";
            earned = 0;
            TXT_QUA.Text = "1";
            TXT_Price.Text = "0.00";
            TXT_Details.Text = "";
        }
        public void notifydatasetchanged()
        {
            pendingListViewModel = new Manager.PendingListViewModel(cata_list, pending_goods);
            DataContext = pendingListViewModel;
            MessageBox.Show("notifydatasetchanged");
        }
    }
}
