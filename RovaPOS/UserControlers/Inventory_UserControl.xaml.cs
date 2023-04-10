using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
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
    /// Interaction logic for Inventory_UserControl.xaml
    /// </summary>
    public partial class Inventory_UserControl : UserControl
    {
        Manager.Database.Goods goods = new Manager.Database.Goods();
        List<Models.Goods> goods_list = new List<Models.Goods>();
        Manager.GoodsViewModel pendingListViewModel;
        Manager.ImagenSQLite ImagenSQLite = new Manager.ImagenSQLite();
        Manager.Database.Categories categories_db = new Manager.Database.Categories();
        List<Models.Categories> categories = new List<Models.Categories>();
       // byte[] pic;
        int selected_id = 0;
        public Inventory_UserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;


            categories = categories_db.ReadCategoriesPic("categories");
            for (int i = 0; i < categories.Count; i++)
            {
                Combo_Cata.Items.Add(categories[i].Name);
                TXT_Cata_Search.Items.Add(categories[i].Name);
            }
            //goods_list = goods.ReadAllGoodsPic("goods");
            //GridView1.ItemsSource = goods_list;
            pendingListViewModel = new Manager.GoodsViewModel(goods_list, goods_list);
            DataContext = pendingListViewModel;
            ResetValues();
            Panel_Add.IsEnabled = false;
            IsEnabled = true;
        }
       
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ادخل هنا رقم الباركود الموجود خلف السلعة - يستخدم هذا للبيع لكن هنا لغاية التاكد من هوية السلعة وانة صادر من محلك");
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

        public void notifydatasetchanged()
        {
            pendingListViewModel = new Manager.GoodsViewModel(goods_list, goods_list);
            DataContext = pendingListViewModel;
        }
        private string name;
        private string category;
        private double quantity;
        private double cost;
        private double price;
        private string type;
        private string barcode;
        private double earned;
        private string datex;
        private string datee;
        private byte[] image;

        public string Name { get => TXT_Name.Text; set => TXT_Name.Text = value; }
        public string Category { get => Combo_Cata.Text; set => Combo_Cata.Text = value; }
        public double Quantity { get => GetDoubleValue(TXT_Qua); }
        public double Cost { get => GetDoubleValue(TXT_Cost); }
        public double Price { get => GetDoubleValue(TXT_Price); }
        public string Type { get => combo_Type.Text; set => combo_Type.Text = value; }
        public string Barcode { get => TXT_Barcode.Text; set => TXT_Barcode.Text = value; }
        public double Earned { get => GetDoubleValue(TXT_Price) - GetDoubleValue(TXT_Cost); }
        public string Datex { get => datex; set => datex = value; }
        public string Datee { get => datee; set => datee = value; }
        //public byte[] Image { get => pic; set => pic = value; }
        private string GetRandomPass(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        private void TXT_Cost_TextChanged(object sender, TextChangedEventArgs e)
        {

            TXT_Earn.Text = (GetDoubleValue(TXT_Price) - GetDoubleValue(TXT_Cost)).ToString();
        }

        private void TXT_Cost_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void TXT_Price_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void TXT_Price_TextChanged(object sender, TextChangedEventArgs e)
        {

            TXT_Earn.Text = (GetDoubleValue(TXT_Price) - GetDoubleValue(TXT_Cost)).ToString();


        }

        private void TXT_Cost_TouchEnter(object sender, TouchEventArgs e)
        {

        }

        private void Btn_Cata_Click(object sender, RoutedEventArgs e)
        {
            AddCategories addCategories = new AddCategories();
            addCategories.ShowDialog();
        }

        private void button_Choose_PIC(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                MessageBox.Show(op.FileName.ToString());
                Image_product.Source = new BitmapImage(new Uri(op.FileName));
                System.Drawing.Image photo = new Bitmap(op.FileName);
                //pic = ImagenSQLite.ImageToByte(photo, System.Drawing.Imaging.ImageFormat.Jpeg);
                //ImagenSQLite.SaveImage(pic , "info" , "Name");
                //Image_product.Source = ImagenSQLite.LoadImageByte(ImagenSQLite.LoadImage("info", "Name" , 2));
                //BLOB in DB
                //Image_product.Source = ImagenSQLite.LoadImage("info", "Name", 2);

            }
        }

        private void button_random_number(object sender, RoutedEventArgs e)
        {
            TXT_Barcode.Text = GetRandomPass(13);
        }

        private void button_Add(object sender, RoutedEventArgs e)
        {
            InsertGoods();
        }
        public void InsertGoods()
        {

            if (Name != "" && Category != "" && Type != "" && Barcode != "" )
            {
                if (Cost != 0 && Price != 0 && Quantity != 0)
                {
                    try
                    {

                        goods.InsertGoods("goods", Name, Category, Quantity, Cost, Price, Type, Barcode, Earned, DateTime.Now.ToString("yyyy/MM/dd"), "");
                        MessageBox.Show("تمت عملية الاضافة بنحاخ");
                        ResetValues();
                        GridView1.IsEnabled = true;
                        Panel_Add.IsEnabled = false;
                        Menu_2_Panel.Visibility = Visibility.Hidden;
                        Menu_panel.Visibility = Visibility.Visible;
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("حدثت مشكلة اثناء عملية الاضافة برجاء الاتصال بالمبرمج فى حالة تكرار هذة المشكلة");
                    }


                }
                else
                {
                    MessageBox.Show("برجاء مراجعة الكمية والسعر مرة اخرى");
                }
            }
            else
            {
                MessageBox.Show("تاكد ان جميع الخانات ليست فارغة");
            }
            //string buf = ((BitmapFrame)(Image_product as System.Windows.Controls.Image).Source).BaseUri.ToString();
            //Image_product.Source = new BitmapImage(new Uri(op.FileName));

          
        }
        public void ResetValues()
        {
            GridView1.UnselectAll();
            TXT_Barcode.Text = "";
            TXT_Cost.Text = "";
            TXT_Name.Text = "";
            TXT_NPrice.Text = "";
            TXT_NQUA.Text = "";
            TXT_Price.Text = "";
            TXT_Qua.Text = "";
            Combo_Cata.Text = "";
            combo_Type.Text = "";
           
            selected_id = 0;
            OpenFileDialog op = new OpenFileDialog();
            op.FileName = "/RovaPOS;component/Models/el-bostan.jpg";
            Image_product.Source = new BitmapImage(new Uri(op.FileName, UriKind.Relative));
            //BitmapImage photo = new BitmapImage(new Uri(op.FileName, UriKind.Relative));
            //pic = ImagenSQLite.ImageBitmapToByte(photo, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void GridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = GridView1.SelectedItem as Models.Goods;
            if (selectedItem != null)
            {
                ResetValues();
                TXT_Barcode.Text = selectedItem.Barcode.ToString();
                TXT_Cost.Text = selectedItem.Cost.ToString();
                TXT_Name.Text = selectedItem.Name.ToString();
                TXT_Price.Text = selectedItem.Price.ToString();
                TXT_Qua.Text = selectedItem.Quantity.ToString();
                Combo_Cata.Text = selectedItem.Category.ToString();
                combo_Type.Text = selectedItem.Type.ToString();
                //Image_product.Source = new BitmapImage((ImagenSQLite.BitmapImageFromBytes(selectedItem.Image));
              
                BitmapImage image = ImagenSQLite.LoadImage("goods", TXT_Barcode.Text);
                if (image != null)
                {
                    Image_product.Source = image;
                }
                  
                
              
                selected_id = selectedItem.Id;
            }
        }

        private void button_Edit(object sender, RoutedEventArgs e)
        {
            if(selected_id != 0)
            {
                if (Name != "" && Category != "" && Type != "" && Barcode != "")
                {
                    if (Cost != 0 && Price != 0)
                    {
                        try
                        {
                           
                            bool succ =  goods.UpdateGoods("goods", selected_id, Name, Category, Quantity, Cost, Price, Type, Barcode, Earned);
                            if (succ == true)
                            {
                                MessageBox.Show("تمت عملية التعديل بنحاخ");
                                ResetValues();
                                GridView1.IsEnabled = true;
                                Panel_Add.IsEnabled = false;
                                Menu_2_Panel.Visibility = Visibility.Hidden;
                                Menu_panel.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                MessageBox.Show("لم تتم عملية التعديل بنجاح برجاء مراجعة البيانات مرة اخرى");
                            }
                           
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("حدثت مشكلة اثناء عملية الاضافة برجاء الاتصال بالمبرمج فى حالة تكرار هذة المشكلة");
                        }


                    }
                    else
                    {
                        MessageBox.Show("برجاء مراجعة السعر مرة اخرى");
                    }
                }
                else
                {
                    MessageBox.Show("تاكد ان جميع الخانات ليست فارغة");
                }
            }
            else
            {
                MessageBox.Show("برجاء تحديد السلعة اولا");
            }
           
        }

        private void button_Remove(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(" هل انت متاكد من حذف هذة السلعة ؟؟", "تنبية !! ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
               


            }
            else
            {
                //do yes stuff
                bool succ = goods.RemoveGoods("goods", Barcode);
                if (succ == true)
                {
                    MessageBox.Show("تمت عملية الحذف بنحاخ");
                    ResetValues();
                    
                }
                else
                {
                    MessageBox.Show("لم تتم عملية الحذف بنجاح برجاء مراجعة البيانات مرة اخرى");
                }
            }
        }
       
        private void button_close(object sender, RoutedEventArgs e)
        {
            var mywindow = Window.GetWindow(this);
            mywindow.Close();

        }

        private void Button_Barcode_Click(object sender, RoutedEventArgs e)
        {
            if(TXT_Barcode_Search.Text != "")
            {
                if(Radio_B_E.IsChecked == true)
                {
                    ExactSearch_B();
                }
                else if (Radio_B_L.IsChecked == true)
                {
                    GeneralSearch_B();
                }
            }
            else
            {
                MessageBox.Show("ادخل معيار البحث");
            }
        }
        private void ExactSearch_B()
        {
            try
            {
                IsEnabled = false;

                goods_list = goods.ReadGoodsPic("goods", "Barcode", TXT_Barcode_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
           
        }
        private void GeneralSearch_B()
        {
            try
            {
                IsEnabled = false;

                goods_list = goods.ReadGoodsPic_Like("goods", "Barcode", TXT_Barcode_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
           
        }

        private void Button_Name_Click(object sender, RoutedEventArgs e)
        {
            if (TXT_Name_Search.Text != "")
            {
                if (Radio_N_E.IsChecked == true)
                {
                    ExactSearch_N();
                }
                else if (Radio_N_L.IsChecked == true)
                {
                    GeneralSearch_N();
                }
            }
            else
            {
                MessageBox.Show("ادخل معيار البحث");
            }
        }
        private void ExactSearch_N()
        {
            try
            {
                IsEnabled = false;

                goods_list = goods.ReadGoodsPic("goods", "Name", TXT_Name_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
          
        }
        private void GeneralSearch_N()
        {
            try
            {
                IsEnabled = false;

                goods_list = goods.ReadGoodsPic_Like("goods", "Name", TXT_Name_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
          
        }

        private void Button_Cata_Click(object sender, RoutedEventArgs e)
        {
            if (TXT_Cata_Search.Text != "")
            {
                if (Radio_C_E.IsChecked == true)
                {
                    ExactSearch_C();
                }
                else if (Radio_C_L.IsChecked == true)
                {
                    GeneralSearch_C();
                }
            }
            else
            {
                MessageBox.Show("ادخل معيار البحث");
            }
        }
        private void ExactSearch_C()
        {
            try
            {
                IsEnabled = false;

                goods_list = goods.ReadGoodsPic("goods", "Category", TXT_Cata_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
           
        }
        private void GeneralSearch_C()
        {
            try
            {
                IsEnabled = false;

                goods_list = goods.ReadGoodsPic_Like("goods", "Category", TXT_Cata_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
           
        }

        private void Button_Quantity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;

                goods_list = goods.ReadAllGoodsQuantity("goods");
                notifydatasetchanged();

                double sum_C, sum_E = 0;

                //for (int i = 0; i < GridView1.Items.Count - 1; i++)
                //{
                //    sum += (double.Parse((GridView1.Columns[5].GetCellContent(GridView1.Items[i]) as TextBlock).Text));
                //}
              
                sum_C = goods_list.Sum(item => item.Cost * item.Quantity);


                Button_Quantity.Content = sum_C.ToString();
              

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
           
        }

        private void button_Add_P(object sender, RoutedEventArgs e)
        {
            GridView1.IsEnabled = false;
            Panel_Add.IsEnabled = true;
            ResetValues();
            Menu_panel.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Hidden;
            btn_Add.Visibility = Visibility.Visible;
            Menu_2_Panel.Visibility = Visibility.Visible;

        }

        private void button_End(object sender, RoutedEventArgs e)
        {
            GridView1.IsEnabled = true;
            Panel_Add.IsEnabled = false;
            Menu_2_Panel.Visibility = Visibility.Hidden;
            Menu_panel.Visibility = Visibility.Visible;
        }

        private void button_Edit_P(object sender, RoutedEventArgs e)
        {
            //ResetValues();
            if(selected_id != 0)
            {
                GridView1.IsEnabled = false;
                Panel_Add.IsEnabled = true;

                Menu_panel.Visibility = Visibility.Hidden;
                btn_Add.Visibility = Visibility.Hidden;
                btn_Edit.Visibility = Visibility.Visible;
                Menu_2_Panel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("برجاء تحديد سلعة اولا لتعديلها");
            }
         
        }
    }
}
