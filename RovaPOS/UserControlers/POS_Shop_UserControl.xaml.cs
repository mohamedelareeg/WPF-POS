using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
    public partial class POS_Shop_UserControl : UserControl
    {
        #region Firebase
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "MkDMPYqhwJr1q6naSn4f95Uz41Q4ZGuUK7nAwkTz",
            BasePath = "https://rovapos.firebaseio.com/"

        };
        IFirebaseClient client;
        #endregion

        int index = 0;
        #region categories
        List<Models.Categories> cata_list = new List<Models.Categories>();
            Manager.Database.Categories categories = new Manager.Database.Categories();
        #endregion
        #region goods
            List<Models.Goods> goods_list = new List<Models.Goods>();
        List<Models.Goods> Allgoods_list = new List<Models.Goods>();
        Manager.Database.Goods goods = new Manager.Database.Goods();
        #endregion
        Manager.Database.Sells Sells = new Manager.Database.Sells();
        Manager.Database.SellsOffline SellsOffline = new Manager.Database.SellsOffline();
        #region PendingSells
        Manager.Database.PendingSell pendingSell = new Manager.Database.PendingSell();
        List<Models.PendingSell> list_pending = new List<Models.PendingSell>();
      
        Manager.PendingListViewModel pendingListViewModel;
        #endregion
        Manager.Database.Bills Bills = new Manager.Database.Bills();
        // Create the `DataTable` structure according to your data source
        DataTable dt = new DataTable();
        DataView dv = new DataView();

        Manager.ImagenSQLite ImagenSQLite = new Manager.ImagenSQLite();
        public List<Models.PendingSell> pending_goods = new List<Models.PendingSell>();
        List<Models.PendingSell> pending_goods_remain = new List<Models.PendingSell>();
        private string name_G;
        private string catag;
        private string type;
        private double quantity;
        private double quantity_T;
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
        public double Quantity_T { get => quantity_T; set => quantity_T = value; }
        public string Catag { get => catag; set => catag = value; }
        public string Type { get => type; set => type = value; }

        #endregion

        bool Avaliable = false;
        public POS_Shop_UserControl()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);

            cata_list = categories.ReadCategoriesPic("categories");

            list_pending = pendingSell.ReadPendingSell("sells");
            //MessageBox.Show(list_pending.Count.ToString());
            Label_BIllnumber.Content = Bills.ReadBillnumber("bills", "Billnumber");
            Label_BIllnumber.Content = GetDoubleValueLabel(Label_BIllnumber) + 1;

            readAllGoods();
            DataTable DT = pendingSell.ReadAdapter("sells");
            pendingListViewModel = new Manager.PendingListViewModel(cata_list, goods_list, pending_goods);
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

        private void readAllGoods()
        {
            Allgoods_list = goods.ReadAllGoodsPic("goods");//ReadAllGoodsPic
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
            goods_list = goods.ReadGoodsPic("goods", cata);//ReadAllGoodsPic
            //images.Add(new Models.Goods(DT.get) { Name = "Goods" , Category = filename });
            //ListViewGoods.ItemsSource = goods_list;
            notifydatasetchanged();
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
                    //GridView1.UnselectAll();

                }
                else
                {
                    //var vw = CollectionViewSource.GetDefaultView(pendingListViewModel.View_Cata);

                    //int index = pendingListViewModel.CategoriesList.IndexOf(vw.CurrentItem);
                    //MessageBox.Show(result.Name);
                    //bindlist_goods(result.Name);
                    bindlist_goods(pendingListViewModel.SelectedCata.Name);
                    //GridView1.UnselectAll();
                    //MessageBox.Show(pendingListViewModel.CategoriesList[lsNames.SelectedIndex].Name);


                }
             

                //bindlist_goods(employeeListViewModel.EmployeeList[lsNames.SelectedIndex].Name);
            }

            //MessageBox.Show("Selected: {0}", e.AddedItems[0]);
        }
      
        private void MySelectionChanged_Goods(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewGoods.SelectedIndex != -1 )
            {
              
                resetSelection();
                barcode = pendingListViewModel.SelectedGoods.Barcode;
                index = 0;
                //MessageBox.Show(barcode);
                index = pending_goods.FindIndex(a => a.Barcode == barcode);
                //int index_q = goods_list.FindIndex(a => a.Barcode == barcode);
                bool selected_barcode = pending_goods.Any(cus => cus.Barcode == barcode);
                //var selected_barcode = pending_goods.Select(sublist => sublist.Barcode == barcode);
               
                if (selected_barcode != false)
                {
                   
                    if (pending_goods[index].Barcode != null)
                    {
                        Btn_Delete.Visibility = Visibility.Visible;
                        Name_G = pending_goods[index].Name;
                        catag = pending_goods[index].Category;
                        quantity = pending_goods[index].Quantity;
                        type = pending_goods[index].Type;
                        quantity_T = pendingListViewModel.SelectedGoods.Quantity;

                        cost = pending_goods[index].Cost;
                        price = pending_goods[index].Price;
                        earned = pending_goods[index].Earned;
                        selected_barcode = false;

                        //if (list_pending.Any(f => f.Barcode == barcode))
                        //{
                        //    var file = list_pending.First(f => f.Barcode == barcode);
                        //    int index_q = list_pending.IndexOf(file);
                        //    TXT_QUA.Text = (list_pending[index_q].Quantity).ToString();
                        //}
                        TXT_QUA.Text = quantity.ToString();
                    }
                }

                else
                {
                    Btn_Delete.Visibility = Visibility.Hidden;
                    Name_G = pendingListViewModel.SelectedGoods.Name;
                    catag = pendingListViewModel.SelectedGoods.Category;
                    quantity = pendingListViewModel.SelectedGoods.Quantity;
                    quantity_T = pendingListViewModel.SelectedGoods.Quantity;
                    cost = pendingListViewModel.SelectedGoods.Cost;
                    price = pendingListViewModel.SelectedGoods.Price;
                    type = pendingListViewModel.SelectedGoods.Type;
                    earned = pendingListViewModel.SelectedGoods.Earned;
                    if (Quantity_T < 1)
                    {
                        TXT_QUA.Text = quantity_T.ToString();
                    }
                    else
                    {
                        TXT_QUA.Text = "1";
                    }
                }
               
                Label_Quantity.Content = TruncateLongString ((quantity_T - GetDoubleValue(TXT_QUA)).ToString() , 8 );
                //MessageBox.Show(quantity_T.ToString());
               
                Label_Barcode.Content = barcode;
                //if (quantity - GetDoubleValue(TXT_QUA) >= 0)
                //{

                //}
                //MessageBox.Show(Name_G);
                TXT_Price.Text = (price * GetDoubleValue(TXT_QUA)).ToString();
                Label_Name.Content = name_G;

                //TXT_QUA.Text = (goods_list[ListViewGoods.SelectedIndex].Quantity).ToString();
            }
               
        }
        private void barcode_Selection()
        {


            resetSelection_G();
            barcode = Search_TXT.Text.ToString();
            index = 0;
            //MessageBox.Show(barcode);
            index = pending_goods.FindIndex(a => a.Barcode == barcode);
            int index_Bar = 0;
            index_Bar = Allgoods_list.FindIndex(a => a.Barcode == barcode);
            //MessageBox.Show(index_Bar.ToString());
            if(index_Bar != -1)
            {
                //Allgoods_list
                //int index_q = goods_list.FindIndex(a => a.Barcode == barcode);
                bool selected_barcode = pending_goods.Any(cus => cus.Barcode == barcode);
                //var selected_barcode = pending_goods.Select(sublist => sublist.Barcode == barcode);

                if (selected_barcode != false)
                {

                    if (pending_goods[index].Barcode != null)
                    {
                        Btn_Delete.Visibility = Visibility.Visible;
                        Name_G = pending_goods[index].Name;
                        catag = pending_goods[index].Category;
                        quantity = pending_goods[index].Quantity;
                        type = pending_goods[index].Type;
                        quantity_T = Allgoods_list[index_Bar].Quantity;

                        cost = pending_goods[index].Cost;
                        price = pending_goods[index].Price;
                        earned = pending_goods[index].Earned;
                        selected_barcode = false;

                        //if (list_pending.Any(f => f.Barcode == barcode))
                        //{
                        //    var file = list_pending.First(f => f.Barcode == barcode);
                        //    int index_q = list_pending.IndexOf(file);
                        //    TXT_QUA.Text = (list_pending[index_q].Quantity).ToString();
                        //}
                        TXT_QUA.Text = quantity.ToString();
                    }
                }

                else
                {
                    Btn_Delete.Visibility = Visibility.Hidden;
                    Name_G = Allgoods_list[index_Bar].Name;
                    catag = Allgoods_list[index_Bar].Category;
                    quantity = Allgoods_list[index_Bar].Quantity;
                    quantity_T = Allgoods_list[index_Bar].Quantity;
                    cost = Allgoods_list[index_Bar].Cost;
                    price = Allgoods_list[index_Bar].Price;
                    type = Allgoods_list[index_Bar].Type;
                    earned = Allgoods_list[index_Bar].Earned;
                    if (Quantity_T < 1)
                    {
                        TXT_QUA.Text = quantity_T.ToString();
                    }
                    else
                    {
                        TXT_QUA.Text = "1";
                    }
                }

                Label_Quantity.Content = TruncateLongString ( (quantity_T - GetDoubleValue(TXT_QUA)).ToString() , 8 );
                //MessageBox.Show(quantity_T.ToString());

                Label_Barcode.Content = barcode;
                //if (quantity - GetDoubleValue(TXT_QUA) >= 0)
                //{

                //}
                //MessageBox.Show(Name_G);
                TXT_Price.Text = (price * GetDoubleValue(TXT_QUA)).ToString();
                Label_Name.Content = name_G;
                Search_TXT.Text = "";
                //TXT_QUA.Text = (goods_list[ListViewGoods.SelectedIndex].Quantity).ToString();
            }
            else
            {
                MessageBox.Show("الباركود الذى ادخلتة غير صحيح برجاء المحاولة مرة اخرى");
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
        public double GetDoubleValueLabel(Label tb)
        {
            try
            {
                return Convert.ToDouble(tb.Content);
            }
            catch (Exception ex)
            {
                //This is called if the converting failed for some reason
            }

            return 0; //This should only return 0 if the textbox does not contain a valid integer value
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!Label_Barcode.Content.Equals("0"))
            {
                if(!TXT_QUA.Text.Equals("0"))
                {
                    var goods_List = new Models.PendingSell();
                    goods_List.Name = Name_G;
                    goods_List.Category = Catag;
                    goods_List.Quantity = GetDoubleValue(TXT_QUA);
                    goods_List.Cost = cost;
                    goods_List.Price = GetDoubleValue(TXT_Price);
                    goods_List.Barcode = barcode;
                    goods_List.Earned = earned;
                    goods_List.Type = type;
                    goods_List.Details = TXT_Details.Text;
                    bool selected_barcode = pending_goods.Any(cus => cus.Barcode == barcode);
                    if (selected_barcode == true)
                    {
                        index = pending_goods.FindIndex(a => a.Barcode == barcode);
                        pending_goods[index].Quantity = GetDoubleValue(TXT_QUA);
                        pending_goods[index].Price = GetDoubleValue(TXT_Price);
                        pending_goods[index].Details = TXT_Details.Text;

                        //object item = GridView1.Items[index];
                        //GridView1.SelectedItem = item;
                        //GridView1.ScrollIntoView(item);


                        notifydatasetchanged();
                        selected_barcode = false;
                    }
                    else
                    {
                        pending_goods.Add(goods_List);

                        notifydatasetchanged();
                        Btn_Delete.Visibility = Visibility.Visible;
                        //Name_G = "";
                        //quantity = 0;
                        //cost = 0;
                        //price = 0;
                        //earned = 0;
                        //barcode = "";
                    }
                    index = pending_goods.FindIndex(a => a.Barcode == barcode);

                    //MessageBox.Show(index.ToString());
                    //MessageBox.Show(pending_goods.Count.ToString());
                    GridView1.SelectedIndex = pending_goods.FindIndex(a => a.Barcode == goods_List.Barcode);
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(System.AppDomain.CurrentDomain.BaseDirectory + "\\Audio\\scanner.wav");
                    player.Play();
                    //pendingListViewModel.ConnectionItems[rowIndex].Quantity = GetDoubleValue(TXT_QUA);
                    //pendingListViewModel.ConnectionItems[rowIndex].Price = GetDoubleValue(TXT_Price);
                    //pendingListViewModel.ConnectionItems[rowIndex].Details = TXT_Details.Text;
                    //}
                }
                else
                {
                    MessageBox.Show("هذة السلعة غير موجود منها اى كمية بالمخزن");
                }


            }
            else
            {
                MessageBox.Show("برجاء تحديد سلعة لبيعها اولا");
            }
            //if(rowIndex != -1)
            //{


        }

        private void GridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            //var dg = sender as DataGrid;
            //if (dg == null) return;
            //var index = dg.SelectedIndex;
            ////here we get the actual row at selected index
            //DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;

            ////here we get the actual data item behind the selected row
            //var item = dg.ItemContainerGenerator.ItemFromContainer(row);
           

            //TXT_QUA.Text = (pendingListViewModel.ConnectionItems[index].Quantity).ToString();
            //TXT_Price.Text = (pendingListViewModel.ConnectionItems[index].Price).ToString();
            //TXT_Details.Text = pendingListViewModel.ConnectionItems[index].Details;
            //MessageBox.Show(pendingListViewModel.ConnectionItems[rowIndex].Barcode.ToString());
        }

        private void Button_Click_close(object sender, RoutedEventArgs e)
        {
            var mywindow = Window.GetWindow(this);
            mywindow.Close();
        }
        public void resetSelection()
        {
            GridView1.UnselectAll();
            combo_Type.SelectedIndex = 0;
            Name_G = "";
            catag = "";
            type = "";
            quantity = 1;
            Label_Quantity.Content = "0";
            Label_Barcode.Content = "0";
            cost = 0;
            price = 0;
            barcode = "";
            earned = 0;
            //TXT_QUA.Text = "1";
            TXT_Price.Text = "0.00";
            TXT_Details.Text = "";
            Label_Name.Content = "اختر سلعة عن طريق اللمس او الباركود";
        }
        public void resetSelection_G()
        {
            combo_Type.SelectedIndex = 0;
            GridView1.UnselectAll();
            ListViewGoods.UnselectAll();
            Name_G = "";
            catag = "";
            type = "";
            quantity = 1;
            Label_Quantity.Content = "0";
            Label_Barcode.Content = "0";
            cost = 0;
            price = 0;
            barcode = "";
            earned = 0;
            //TXT_QUA.Text = "1";
            TXT_Price.Text = "0.00";
            TXT_Details.Text = "";
            Label_Name.Content = "اختر سلعة عن طريق اللمس او الباركود";
        }
        private void notifydatasetchanged()
        {
            pendingListViewModel = new Manager.PendingListViewModel(cata_list ,goods_list , pending_goods);
           
            DataContext = pendingListViewModel;
           
            //MessageBox.Show("notifydatasetchanged");
        }
        private void notifydatasetchanged_GOODs()
        {
            pendingListViewModel = new Manager.PendingListViewModel(goods_list, pending_goods);

            DataContext = pendingListViewModel;
            //MessageBox.Show("notifydatasetchanged");
        }
        private string TruncateLongString(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
        private void TXT_QUA_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(quantity != 0 || cost !=0 )
            {

                Label_Quantity.Content = TruncateLongString((quantity_T - GetDoubleValue(TXT_QUA)).ToString() , 8);

                if (quantity_T - GetDoubleValue(TXT_QUA) < 0)
                {
                    MessageBox.Show("لا يوجد كمية كافية .. تم اضافة الحد الاقصى من هذة السلعة");
                    TXT_QUA.Text = quantity_T.ToString();
                }
                earned = GetDoubleValue(TXT_Price) - (cost * GetDoubleValue(TXT_QUA));
                if (Allgoods_list.Any(f => f.Barcode == barcode))
                {
                    var file = Allgoods_list.First(f => f.Barcode == barcode);
                    int index_q = Allgoods_list.IndexOf(file);
                    price = Allgoods_list[index_q].Price;
                    TXT_Price.Text = (price * GetDoubleValue(TXT_QUA)).ToString();
                }
                
                if (earned < 0)
                {
                    Label_Lose.Content = earned.ToString();
                    Label_Lose.Visibility = Visibility.Visible;
                    Label_Status.Content = "خسارة";
                    Label_Status.Foreground = Brushes.Red;
                }
                else
                {
                    Label_Lose.Visibility = Visibility.Hidden;
                    Label_Status.Content = "مكسب";
                    Label_Status.Foreground = Brushes.Blue;
                }
            }
        }

        private void TXT_Price_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (quantity != 0 || cost != 0)
            {
               
                earned = GetDoubleValue(TXT_Price) - (cost * GetDoubleValue(TXT_QUA));
                if (earned < 0)
                {
                    Label_Lose.Content = earned.ToString();
                    Label_Lose.Visibility = Visibility.Visible;
                    Label_Status.Content = "خسارة";
                    Label_Status.Foreground = Brushes.Red;
                }
                else
                {
                   
                    Label_Lose.Visibility = Visibility.Hidden;
                    Label_Status.Content = "مكسب";
                    Label_Status.Foreground = Brushes.Blue;
                }
            }
        }

        private void GridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = GridView1.SelectedItem as Models.PendingSell;
            if (selectedItem != null)
            {
               
                if(!Label_Barcode.Content.Equals("") )
                {
                    resetSelection_G();
                    barcode = selectedItem.Barcode.ToString();
                    if (barcode != null)
                    {
                       
                        bool selected_barcode = pending_goods.Any(cus => cus.Barcode == barcode);
                        //var selected_barcode = pending_goods.Select(sublist => sublist.Barcode == barcode);

                        if (selected_barcode != false)
                        {

                            if (selectedItem.Barcode != null)
                            {
                                Name_G = selectedItem.Name;
                                Catag = selectedItem.Category;
                                quantity = selectedItem.Quantity;
                                if (Allgoods_list.Any(f => f.Barcode == barcode))
                                {
                                    var file = Allgoods_list.First(f => f.Barcode == barcode);
                                    int index_q = Allgoods_list.IndexOf(file);
                                    quantity_T = Allgoods_list[index_q].Quantity;
                                }
                              
                                
                                  
                                
                                cost = selectedItem.Cost;
                                price = selectedItem.Price;
                                earned = selectedItem.Earned;
                                TXT_QUA.Text = quantity.ToString();
                                selected_barcode = false;
                            }
                        }

                        else
                        {
                            Name_G = pendingListViewModel.SelectedGoods.Name;
                            catag = pendingListViewModel.SelectedGoods.Category;
                            quantity = pendingListViewModel.SelectedGoods.Quantity;

                            quantity_T = pendingListViewModel.SelectedGoods.Quantity;
                            type = pendingListViewModel.SelectedGoods.Type;
                            cost = pendingListViewModel.SelectedGoods.Cost;
                            price = pendingListViewModel.SelectedGoods.Price;
                            earned = pendingListViewModel.SelectedGoods.Earned;
                        }

                        //MessageBox.Show(Name_G);
                        TXT_Price.Text = price.ToString();
                        Label_Name.Content = name_G;
                        Label_Quantity.Content = TruncateLongString((quantity_T - GetDoubleValue(TXT_QUA)).ToString() , 8);
                        Label_Barcode.Content = barcode;
                        Btn_Delete.Visibility = Visibility.Visible;
                    }
                    //index = 0;
                    //index = GridView1.SelectedIndex;
                    //MessageBox.Show(index.ToString());
                    //if (index != -1)
                    //{



                    //}
                }

            }

        }

        private void GridView1_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //search the object hierarchy for a datagrid row
            DependencyObject source = (DependencyObject)e.OriginalSource;
            var row = DataGridTextBox.Helpers.UIHelpers.TryFindParent<DataGridRow>(source);

            //the user did not click on a row
            if (row == null) return;

            //[insert great code here...]

            e.Handled = true;
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_Delete.Visibility == Visibility.Visible)
            {
                if (barcode != "")
                {
                    index = pending_goods.FindIndex(a => a.Barcode == barcode);
                    pending_goods.RemoveAt(index);
                    notifydatasetchanged();
                    resetSelection_G();
                    Btn_Delete.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("برجاء تحديد سلعة لحذفها اولا");
                }
            }
        }

        private void MySelectionChanged_Select(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GridView1_Selected(object sender, RoutedEventArgs e)
        {
           
        }

        private void GridView1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            double sum, sum_C, sum_E = 0;

            //for (int i = 0; i < GridView1.Items.Count - 1; i++)
            //{
            //    sum += (double.Parse((GridView1.Columns[5].GetCellContent(GridView1.Items[i]) as TextBlock).Text));
            //}
            sum = pending_goods.Sum(item => item.Price);
            sum_C = pending_goods.Sum(item => item.Cost * item.Quantity);
            sum_E = sum - sum_C;
            
            Label_TOT.Content = sum.ToString();
            Label_Earn.Content = sum_E.ToString();

        }

        private void GridView1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            double sum , sum_C , sum_E = 0;

            //for (int i = 0; i < GridView1.Items.Count - 1; i++)
            //{
            //    sum += (double.Parse((GridView1.Columns[5].GetCellContent(GridView1.Items[i]) as TextBlock).Text));
            //}
            sum = pending_goods.Sum(item => item.Price);
            sum_C = pending_goods.Sum(item => item.Cost * item.Quantity);
            sum_E = sum - sum_C;
            Label_TOT.Content = sum.ToString();
            //Label_TOT_C.Content = sum_E.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "osk.exe");
        }

        private void Combo_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string content = ((ComboBoxItem)combo_Type.SelectedItem).Content as string;
            TXT_QUA.Text = content;
        }

        private void Combo_Type_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            TXT_QUA.Text = (GetDoubleValue(TXT_QUA) + 1).ToString();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control)
                 && (Keyboard.IsKeyDown(Key.G)))
            {
                if(!barcode.Equals(""))
                {
                    index = pending_goods.FindIndex(a => a.Barcode == barcode);
                    pending_goods.RemoveAt(index);
                    notifydatasetchanged();
                    resetSelection_G();
                    Btn_Delete.Visibility = Visibility.Hidden;
                    
                }
                else
                {
                    MessageBox.Show("برجاء تحديد سلعة لحذفها اولا");
                }
             
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control)
                 && (Keyboard.IsKeyDown(Key.Delete)))
            {
                pending_goods.Clear();
                notifydatasetchanged();
                resetSelection_G();
                Btn_Delete.Visibility = Visibility.Hidden;
            }
            //Btn_Delete_Click
            else if ((Keyboard.IsKeyDown(Key.F1)))
            {
                Button_Click_1(sender, e);
            }
            else if ((Keyboard.IsKeyDown(Key.Delete)))
            {
                Btn_Delete_Click(sender, e);
            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("https://rovapos.firebaseio.com/"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void checkConnection()
        {

        }
        private async void SaveOnline()
        {
            if (pending_goods.Count > 0)
            {
                IsEnabled = false;
                if (MessageBox.Show(" هل تريد تسجيل هذة البضاعة على المخزن المتصل بالانترنت ام لا ... تاكد اولا ان الانترنت متصل بشكل جيد", "المخزن الالكترونى", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                    for (int i = 0; i < pending_goods.Count; i++)
                    {
                        pendingSell.InsertPendingSell("pending_sells", pending_goods[i].Name, pending_goods[i].Category, pending_goods[i].Quantity, pending_goods[i].Cost, pending_goods[i].Price, pending_goods[i].Type, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"), pending_goods[i].Barcode, pending_goods[i].Earned, pending_goods[i].Details);
                        //pending_goods.RemoveAt(i);

                        //pending_goods_remain.Add(pending_goods[i]);
                    }


                }
                else
                {

                    if (client != null)
                    {
                        for (int i = 0; i < pending_goods.Count; i++)
                        {
                            try
                            {

                                FirebaseResponse response = await client.GetAsync("Goods/" + pending_goods[i].Barcode);
                                Models.Goods result = response.ResultAs<Models.Goods>();
                                //MessageBox.Show(result.Barcode);
                                if (result.Quantity >= pending_goods[i].Quantity)
                                {
                                    double new_quantity = result.Quantity - pending_goods[i].Quantity;
                                    var goods_List = new Models.Goods();
                                    goods_List.Id = result.Id;
                                    goods_List.Name = result.Name;
                                    goods_List.Category = result.Category;
                                    goods_List.Quantity = new_quantity;
                                    goods_List.Cost = result.Cost;
                                    goods_List.Price = result.Price;
                                    goods_List.Type = result.Type;
                                    goods_List.Datex = DateTime.Now.ToString("yyyy/MM/dd");
                                    goods_List.Datee = "";
                                    goods_List.Barcode = result.Barcode;
                                    goods_List.Earned = result.Earned;
                                    SetResponse response_set = await client.SetAsync("Goods/" + pending_goods[i].Barcode, goods_List);
                                    Models.Goods result_set = response_set.ResultAs<Models.Goods>();
                                    //MessageBox.Show(goods_List.Quantity.ToString());
                                    Sells.InsertSells("sells", pending_goods[i].Name, pending_goods[i].Category, pending_goods[i].Quantity, pending_goods[i].Cost, pending_goods[i].Price, goods_List.Type, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"), goods_List.Barcode, Convert.ToInt32(Label_BIllnumber.Content), pending_goods[i].Earned, "مباع", pending_goods[i].Details);
                                    if (new_quantity < 0.0009)
                                    {
                                        new_quantity = 0;
                                    }
                                    goods.UpdateGoodCount("goods", result.Barcode, new_quantity);
                                    //Name ,Category ,Quantity ,Cost ,Price ,Type  , Time ,  Datex ,Barcode , Earned , Details

                                    //pending_goods.RemoveAt(i);
                                }
                                else
                                {
                                    //pending_goods[i].Details = "اقصى كمية موجودة بالمخزن هى " + result.Quantity + " ";
                                    pending_goods_remain.Add(pending_goods[i]);
                                    MessageBox.Show("اقصى كمية موجودة بالمخزن من  " + result.Name + "  هى  " + result.Quantity + " ");
                                }

                            }
                            catch (Exception)
                            {
                                pendingSell.InsertPendingSell("pending_sells", pending_goods[i].Name, pending_goods[i].Category, pending_goods[i].Quantity, pending_goods[i].Cost, pending_goods[i].Price, pending_goods[i].Type, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"), pending_goods[i].Barcode, pending_goods[i].Earned, pending_goods[i].Details);
                                //pending_goods.RemoveAt(i);
                            }
                        }


                    }

                }

                IsEnabled = true;
            }
            else
            {
                MessageBox.Show("قم بادراج منتج اولا الى سلة المبيعات");
            }

            if (pending_goods.Count > pending_goods_remain.Count)
            {
                double sum = pending_goods_remain.Sum(item => item.Price);
                double sum_C = pending_goods_remain.Sum(item => item.Cost * item.Quantity);
                double sum_E = (sum - sum_C);
                double sum_F = GetDoubleValueLabel(Label_TOT) - sum;
                double sum_E_F = GetDoubleValueLabel(Label_Earn) - sum_E;
                //string billnumber, double billcost, string time, string datex, string ownername, string ownerid, string ownernumber, double paid, double remain, double earned, double tax, double discount
                Bills.InsertBills("bills", 0, Convert.ToInt32(Label_BIllnumber.Content), sum_F, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"), "", "", "", sum_F, 0, sum_E_F, 0, 0, "");
                Label_BIllnumber.Content = GetDoubleValueLabel(Label_BIllnumber) + 1;
                pending_goods.Clear();
                for (int i = 0; i < pending_goods_remain.Count; i++)
                {
                    pending_goods.Add(pending_goods_remain[i]);
                }
            }
            goods_list.Clear();
            Allgoods_list.Clear();
            readAllGoods();
            notifydatasetchanged();
            resetSelection_G();
            Btn_Delete.Visibility = Visibility.Hidden;
            pending_goods_remain.Clear();
            
        }
        private void SaveOffline()
        {
            if (pending_goods.Count > 0)
            {
                IsEnabled = false;
                for (int i = 0; i < pending_goods.Count; i++)
                {
                    double _Quantity = goods.ReadGoodsQuantity("goods", pending_goods[i].Barcode);
                    if (_Quantity >= pending_goods[i].Quantity)
                    {
                        if((_Quantity - pending_goods[i].Quantity) >= 0)
                        {
                            double new_quantity = _Quantity - pending_goods[i].Quantity;

                            SellsOffline.InsertSellsOffline("sellsoffline", pending_goods[i].Name, pending_goods[i].Category, pending_goods[i].Quantity,
                                pending_goods[i].Cost, pending_goods[i].Price, pending_goods[i].Type, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"),
                                pending_goods[i].Barcode, Convert.ToInt32(Label_BIllnumber.Content), pending_goods[i].Earned, "مباع", pending_goods[i].Details);

                            if (new_quantity < 0.0009)
                            {
                                new_quantity = 0;
                            }
                            goods.UpdateGoodCount("goods", pending_goods[i].Barcode, new_quantity);
                            //int index_Sell = Allgoods_list.FindIndex(a => a.Barcode == pending_goods[i].Barcode);
                            //Allgoods_list[index_Sell].Quantity = new_quantity;
                        }
                        else
                        {
                            pending_goods_remain.Add(pending_goods[i]);
                            MessageBox.Show("اقصى كمية موجودة بالمخزن من  " + pending_goods[i].Name + "  هى  " + _Quantity + " ");
                        }
                        
                    }
                    else
                    {
                        pending_goods_remain.Add(pending_goods[i]);
                        MessageBox.Show("اقصى كمية موجودة بالمخزن من  " + pending_goods[i].Name + "  هى  " + _Quantity + " ");
                    }
                    // public void InsertSellsOffline(string TableName, string Name, string Category, double Quantity, double Cost, double Price, string Type, string Time, string Datex, string Barcode ,int Billnumber, double Earned ,string Returned , string Details)


                    //pending_goods_remain.Add(pending_goods[i]);
                }
                IsEnabled = true;
            }
            else
            {
                MessageBox.Show("قم بادراج منتج اولا الى سلة المبيعات");
            }

            if (pending_goods.Count > pending_goods_remain.Count)
            {
                double sum = pending_goods_remain.Sum(item => item.Price);
                double sum_C = pending_goods_remain.Sum(item => item.Cost * item.Quantity);
                double sum_E = (sum - sum_C);
                double sum_F = GetDoubleValueLabel(Label_TOT) - sum;
                double sum_E_F = GetDoubleValueLabel(Label_Earn) - sum_E;
                //string billnumber, double billcost, string time, string datex, string ownername, string ownerid, string ownernumber, double paid, double remain, double earned, double tax, double discount
                Bills.InsertBills("bills", 0, Convert.ToInt32(Label_BIllnumber.Content), sum_F, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"), "", "", "", sum_F, 0, sum_E_F, 0, 0, "");
                Label_BIllnumber.Content = GetDoubleValueLabel(Label_BIllnumber) + 1;
                pending_goods.Clear();
                for (int i = 0; i < pending_goods_remain.Count; i++)
                {
                    pending_goods.Add(pending_goods_remain[i]);
                }
                if(pending_goods_remain.Count > 0)
                {
                    if (MessageBox.Show(" هناك بضاعة لم تباع بسبب عدم وجود كمية متوفرة منها .. هل تريد تسجيلها موقتا فى المبيعات المعلقة حتى تتوافر كمية منها ( لا تحسب مع مجموع المبيعات الفعلية ) ", "بضاعة لم تباع", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no
                    }
                    else
                    {
                        //do yes
                        for (int i = 0; i < pending_goods.Count; i++)
                        {
                            pendingSell.InsertPendingSell("pending_sells", pending_goods[i].Name, pending_goods[i].Category, pending_goods[i].Quantity, pending_goods[i].Cost,
                           pending_goods[i].Price, pending_goods[i].Type, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"), pending_goods[i].Barcode, pending_goods[i].Earned, pending_goods[i].Details);
                        }
                        pending_goods.Clear();

                    }
                }
                
            }
            goods_list.Clear();
            Allgoods_list.Clear();
            readAllGoods();
            notifydatasetchanged();
            resetSelection_G();
            Btn_Delete.Visibility = Visibility.Hidden;
            pending_goods_remain.Clear();
           

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SaveOnline();

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            resetSelection_G();
            Btn_Delete.Visibility = Visibility.Hidden;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            pending_goods.Clear();
            notifydatasetchanged();
            resetSelection_G();
            Btn_Delete.Visibility = Visibility.Hidden;
        }

        private void Search_TXT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_TXT.Text != "")
            {
                if (Max_GOODS.IsChecked == true)
                {
                    if (Search_TXT.Text.Length == 13)
                        barcode_Selection();
                }
                else if (Small_GOODS.IsChecked == true)
                {
                    if (Search_TXT.Text.Length == 2)
                        barcode_Selection();
                }
            }
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            SaveOffline();
        }
    }
    class CopyDataGridView : DataGrid
    {
        public  CopyDataGridView() : base()
        {
            this.AddHandler(CopyDataGridView.KeyDownEvent, new RoutedEventHandler(HandleHandledKeyDown), true);
        }

        public void HandleHandledKeyDown(object sender, RoutedEventArgs e)
        {
            KeyEventArgs ke = e as KeyEventArgs;
            if ((Keyboard.Modifiers == ModifierKeys.Control)
                 && (Keyboard.IsKeyDown(Key.Delete)))
            {

            }
        }
    }
}
