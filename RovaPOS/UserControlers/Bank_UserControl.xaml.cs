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
    public partial class Bank_UserControl : UserControl
    {

        Manager.Database.SellsOffline sellsOffline = new Manager.Database.SellsOffline();
        List<Models.SellsOffline> sellsoffline_list = new List<Models.SellsOffline>();

        //------------------------------------------------------------------------------

        Manager.Database.Purchase purchase = new Manager.Database.Purchase();
        List<Models.Purchase> purchases_list = new List<Models.Purchase>();

        //------------------------------------------------------------------------------

        Manager.Database.Expense expense = new Manager.Database.Expense();
        List<Models.Expenses> expenses_list = new List<Models.Expenses>();

        //------------------------------------------------------------------------------

        Manager.Database.Returned returned = new Manager.Database.Returned();
        List<Models.Returned> returned_list = new List<Models.Returned>();


        public Bank_UserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            TXT_Date_Search.Text = DateTime.Now.ToString("yyyy/MM/dd");
            TXT_Date_Search_2.Text = DateTime.Now.ToString("yyyy/MM/dd");

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

     
      

        private void button_Add(object sender, RoutedEventArgs e)
        {
           
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

       

        private void Button_Date_Click(object sender, RoutedEventArgs e)
        {
            if (TXT_Date_Search.Text != "" && TXT_Date_Search_2.Text != "" )
            {
                resetValues();
                resetVariablers();
                GeneralSearch_D();
            }
            else
            {
                MessageBox.Show("ادخل معيار البحث");
            }

        }
       

        private void resetValues()
        {
            Label_TOT_Bank.Content = "0";
            Label_Remain.Content = "0";
            Label_TOT_Discount.Content = "0";
            Label_TOT_E.Content = "0";
            Label_TOT_Expenses.Content = "0";
            Label_TOT_Income.Content = "0";
            Label_TOT_Purchase.Content = "0";
            Label_TOT_Purchase_Paid.Content = "0";
            Label_TOT_Purchase_Remain.Content = "0";
            Label_TOT_Returned.Content = "0";
            Label_TOT_Salaries.Content = "0";
            Label_TOT_Sells.Content = "0";
            Label_TOT_Tax.Content = "0";
            Label_TOT_Transport.Content = "0";
            Label_TOT_Used.Content = "0";
          
        }

        private void ExactSearch_D()
        {
            try
            {
                IsEnabled = false;

                //goods_list = sellsOffline.ReadSellsOffline("sellsoffline", "Datex", TXT_Date_Search.Text);
               

                IsEnabled = true;
            }
            catch (Exception)
            {

                IsEnabled = true;
            }

        }
        double total, cost, earn , purchase_T , purchase_paid , purchase_remain , expenses_T , returned_T;
        private void resetVariablers()
        {
            total = 0;
            cost = 0;
            earn = 0;
            purchase_T = 0;
            purchase_paid = 0;
            purchase_remain = 0;
            expenses_T = 0;
            returned_T = 0;


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

        private void Button_Date_All_Click(object sender, RoutedEventArgs e)
        {
            resetValues();
            resetVariablers();
            GeneralSearch_All_D();
        }

        private void GeneralSearch_All_D()
        {
            try
            {
                IsEnabled = false;



                sellsoffline_list = sellsOffline.ReadSellsOffline("sellsoffline");
                for (int i = 0; i < sellsoffline_list.Count; i++)
                {

                    total += sellsoffline_list[i].Price;
                    cost += sellsoffline_list[i].Cost * sellsoffline_list[i].Quantity;
                    earn = total - cost;

                }
                // ---------------------------------------------
                purchases_list = purchase.ReadPurchase("purchase");
                for (int i = 0; i < purchases_list.Count; i++)
                {

                    purchase_T += purchases_list[i].Cost;
                    purchase_paid += purchases_list[i].Paid;
                    purchase_remain = purchases_list[i].Remain;

                }
                // ---------------------------------------------
                expenses_list = expense.ReadExpenses("expenses");
                for (int i = 0; i < expenses_list.Count; i++)
                {

                    expenses_T += expenses_list[i].Cost;


                }
                // ---------------------------------------------
                returned_list = returned.ReadReturned("returned");
                for (int i = 0; i < returned_list.Count; i++)
                {

                    returned_T += returned_list[i].Price;


                }
                // ---------------------------------------------
                Label_TOT_Sells.Content = total.ToString();
                //Label_TOT_COST.Content = cost.ToString();
                Label_TOT_E.Content = earn.ToString();
                // ---------------------------------------------
                Label_TOT_Purchase.Content = purchase_T.ToString();
                Label_TOT_Purchase_Paid.Content = purchase_paid.ToString();
                Label_TOT_Purchase_Remain.Content = purchase_remain.ToString();
                // ---------------------------------------------
                Label_TOT_Expenses.Content = expenses_T.ToString();
                // ---------------------------------------------
                Label_TOT_Returned.Content = returned_T.ToString();
                // ---------------------------------------------
                Label_TOT_Income.Content = (total - (purchase_T + expenses_T)).ToString();
                Label_TOT_Bank.Content = (GetDoubleValueLabel(Label_TOT_Income) + (GetDoubleValueLabel(Label_TOT_Tax) - GetDoubleValueLabel(Label_TOT_Discount))).ToString();

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

              
                
                sellsoffline_list = sellsOffline.ReadSellsOffline_Range("sellsoffline", "Datex", TXT_Date_Search.Text, TXT_Date_Search_2.Text);
                for (int i = 0; i < sellsoffline_list.Count; i++)
                {

                    total += sellsoffline_list[i].Price;
                    cost += sellsoffline_list[i].Cost * sellsoffline_list[i].Quantity;
                    earn = total - cost;

                }
                // ---------------------------------------------
                purchases_list = purchase.ReadPurchase_Range("purchase", "Datex", TXT_Date_Search.Text, TXT_Date_Search_2.Text);
                for (int i = 0; i < purchases_list.Count; i++)
                {

                    purchase_T += purchases_list[i].Cost;
                    purchase_paid += purchases_list[i].Paid;
                    purchase_remain = purchases_list[i].Remain;

                }
                // ---------------------------------------------
                expenses_list = expense.ReadExpenses_Range("expenses", "Datex", TXT_Date_Search.Text, TXT_Date_Search_2.Text);
                for (int i = 0; i < expenses_list.Count; i++)
                {

                    expenses_T += expenses_list[i].Cost;
                  

                }
                // ---------------------------------------------
                returned_list = returned.ReadReturned_Range("returned", "Datex", TXT_Date_Search.Text, TXT_Date_Search_2.Text);
                for (int i = 0; i < returned_list.Count; i++)
                {

                    returned_T += returned_list[i].Price;


                }
                // ---------------------------------------------
                Label_TOT_Sells.Content = total.ToString();
                //Label_TOT_COST.Content = cost.ToString();
                Label_TOT_E.Content = earn.ToString();
                // ---------------------------------------------
                Label_TOT_Purchase.Content = purchase_T.ToString();
                Label_TOT_Purchase_Paid.Content = purchase_paid.ToString();
                Label_TOT_Purchase_Remain.Content = purchase_remain.ToString();
                // ---------------------------------------------
                Label_TOT_Expenses.Content = expenses_T.ToString();
                // ---------------------------------------------
                Label_TOT_Returned.Content = returned_T.ToString();
                // ---------------------------------------------
                Label_TOT_Income.Content = (total - (purchase_T + expenses_T)).ToString();
                Label_TOT_Bank.Content = (GetDoubleValueLabel(Label_TOT_Income) + (GetDoubleValueLabel(Label_TOT_Tax) - GetDoubleValueLabel(Label_TOT_Discount))).ToString();

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

        private void Button_DatePicker_Click_2(object sender, RoutedEventArgs e)
        {
            if (Date_Picker_2.Visibility == Visibility.Hidden)
            {
                Date_Picker_2.Visibility = Visibility.Visible;
            }
            else
            {
                Date_Picker_2.Visibility = Visibility.Hidden;
            }
        }

        private void Date_Picker_SelectedDatesChanged_2(object sender, SelectionChangedEventArgs e)
        {
            if (Date_Picker_2.SelectedDate.HasValue)
            {
                TXT_Date_Search_2.Text = Date_Picker_2.SelectedDate.Value.ToString("yyyy/MM/dd");
            }
        }
    }
}
