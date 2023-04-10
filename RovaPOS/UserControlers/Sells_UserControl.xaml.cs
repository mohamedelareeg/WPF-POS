using Microsoft.Win32;
using System;
using System.Collections.Generic;

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
    public partial class Sells_UserControl : UserControl
    {
        Manager.Database.SellsOffline sellsOffline = new Manager.Database.SellsOffline();
        Manager.Database.Returned returned = new Manager.Database.Returned();
        List<Models.SellsOffline> goods_list = new List<Models.SellsOffline>();
        Manager.SellsOfflineViewModel pendingListViewModel;
        Manager.ImagenSQLite ImagenSQLite = new Manager.ImagenSQLite();
        Manager.Database.Categories categories_db = new Manager.Database.Categories();
        List<Models.Categories> categories = new List<Models.Categories>();
        byte[] pic;
        int selected_id = 0;
        public Sells_UserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            TXT_Date_Search.Text = DateTime.Now.ToString("yyyy/MM/dd");
            categories = categories_db.ReadCategoriesPic("categories");
            for (int i = 0; i < categories.Count; i++)
            {
              
                TXT_Cata_Search.Items.Add(categories[i].Name);
            }
            //goods_list = goods.ReadAllGoodsPic("goods");
            //GridView1.ItemsSource = goods_list;
            pendingListViewModel = new Manager.SellsOfflineViewModel(goods_list, goods_list);
            DataContext = pendingListViewModel;


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
            pendingListViewModel = new Manager.SellsOfflineViewModel(goods_list, goods_list);
            DataContext = pendingListViewModel;
        }
      

     
        private string GetRandomPass(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

      

        private void button_Add(object sender, RoutedEventArgs e)
        {
           
        }
      

        private void GridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = GridView1.SelectedItem as Models.Goods;
            if (selectedItem != null)
            {
               
            }
        }

        private void button_Edit(object sender, RoutedEventArgs e)
        {
          
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
                //bool succ = goods.RemoveGoods("goods", Barcode);
                //if (succ == true)
                //{
                //    MessageBox.Show("تمت عملية الحذف بنحاخ");
                //    ResetValues();
                    
                //}
                //else
                //{
                //    MessageBox.Show("لم تتم عملية الحذف بنجاح برجاء مراجعة البيانات مرة اخرى");
                //}
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

                goods_list = sellsOffline.ReadSellsOffline("sellsoffline", "Barcode", TXT_Barcode_Search.Text);
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

                goods_list = sellsOffline.ReadSellsOffline_Like("sellsoffline", "Barcode", TXT_Barcode_Search.Text);
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

                goods_list = sellsOffline.ReadSellsOffline("sellsoffline", "Name", TXT_Name_Search.Text);
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

                goods_list = sellsOffline.ReadSellsOffline_Like("sellsoffline", "Name", TXT_Name_Search.Text);
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

                goods_list = sellsOffline.ReadSellsOffline("sellsoffline", "Category", TXT_Cata_Search.Text);
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

                goods_list = sellsOffline.ReadSellsOffline_Like("sellsoffline", "Category", TXT_Cata_Search.Text);
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

                goods_list = sellsOffline.ReadAllSellsOfflineQuantity("sellsoffline");
                notifydatasetchanged();
                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }
           
        }

        private void GridView1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            double sum, sum_C, sum_E = 0;

            //for (int i = 0; i < GridView1.Items.Count - 1; i++)
            //{
            //    sum += (double.Parse((GridView1.Columns[5].GetCellContent(GridView1.Items[i]) as TextBlock).Text));
            //}
            sum = goods_list.Sum(item => item.Price);
            sum_C = goods_list.Sum(item => item.Cost * item.Quantity);
            sum_E = sum - sum_C;

            Label_TOT.Content = sum.ToString();
            Label_TOT_E.Content = sum_E.ToString();
        }
        private string barcode = "";
        private void Btn_Return_Click(object sender, RoutedEventArgs e)
        {
            barcode = "";
            var selectedItem = GridView1.SelectedItem as Models.SellsOffline;
            if (selectedItem != null)
            {
                barcode = selectedItem.Barcode.ToString();
                sellsOffline.UpdateReturnedQuantity("goods", barcode, selectedItem.Quantity);

                returned.InsertReturned("returned", selectedItem.Name, selectedItem.Category, selectedItem.Quantity,
                                selectedItem.Cost, selectedItem.Price, selectedItem.Type, DateTime.Now.ToString("hh : mm"), DateTime.Now.ToString("yyyy/MM/dd"),
                                selectedItem.Barcode, selectedItem.Billnumber, selectedItem.Earned, selectedItem.Details);

                sellsOffline.RemoveGoods("sellsoffline", barcode, selectedItem.Billnumber.ToString());

                int index = goods_list.FindIndex(l => l.Barcode == selectedItem.Barcode && l.Billnumber == selectedItem.Billnumber);
                goods_list.RemoveAt(index);
                notifydatasetchanged();
                GridView1.UnselectAll();

                barcode = "";
                MessageBox.Show("تم نقل السلعة الى المرتجعات");

            }
        }

        private void Button_Date_Click(object sender, RoutedEventArgs e)
        {
            if (TXT_Date_Search.Text != "")
            {
                if (Radio_D_E.IsChecked == true)
                {
                    ExactSearch_D();
                }
                else if (Radio_D_L.IsChecked == true)
                {
                    GeneralSearch_D();
                }
            }
            else
            {
                MessageBox.Show("ادخل معيار البحث");
            }

        }
        private void ExactSearch_D()
        {
            try
            {
                IsEnabled = false;

                goods_list = sellsOffline.ReadSellsOffline("sellsoffline", "Datex", TXT_Date_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }

        }
        private void GeneralSearch_D()
        {
            try
            {
                IsEnabled = false;

                goods_list = sellsOffline.ReadSellsOffline_Like("sellsoffline", "Datex", TXT_Date_Search.Text);
                notifydatasetchanged();

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }

        }

        private void Button_DatePicker_Click(object sender, RoutedEventArgs e)
        {
            if (Date_Picker.Visibility == Visibility.Hidden)
            {
                Date_Picker.Visibility = Visibility.Visible;
            }
            else
            {
                Date_Picker.Visibility = Visibility.Hidden;
            }
        }

      

        private void Date_Picker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Date_Picker.SelectedDate.HasValue)
            {
                TXT_Date_Search.Text = Date_Picker.SelectedDate.Value.ToString("yyyy/MM/dd");
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {

        }
        private void copyAlltoClipboard()
        {
            //GridView1.SelectAll();
            //DataObject dataObj = GridView1.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
        }

        private void Btn_Excel_Click(object sender, RoutedEventArgs e)
        {
            //copyAlltoClipboard();
            //Microsoft.Office.Interop.Excel.Application xlexcel;
            //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;
            //xlexcel = new Microsoft.Office.Interop.Excel.Application();
            //xlexcel.Visible = true;
            //xlWorkBook = xlexcel.Workbooks.Add(misValue);
            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            //CR.Select();
            //xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }
    }
}
