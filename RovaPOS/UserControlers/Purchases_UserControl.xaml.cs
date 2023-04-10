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
    public partial class Purchases_UserControl : UserControl
    {

        Manager.Database.Purchase purchase = new Manager.Database.Purchase();
        List<Models.Purchase> purchase_list = new List<Models.Purchase>();

        Manager.PurchaseViewModel pendingListViewModel;
        public Purchases_UserControl()
        {
            InitializeComponent();



        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;


            Label_BIllnumber.Content = purchase.ReadBillnumber("purchase", "ID");
            Label_BIllnumber.Content = GetDoubleValueLabel(Label_BIllnumber) + 1;
            TXT_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");

            purchase_list = purchase.ReadPurchase("purchase");
            pendingListViewModel = new Manager.PurchaseViewModel(purchase_list, purchase_list);
            DataContext = pendingListViewModel;

            Menu_Panel.IsEnabled = false;
            IsEnabled = true;
        }
        public void notifydatasetchanged()
        {
            pendingListViewModel = new Manager.PurchaseViewModel(purchase_list, purchase_list);
            DataContext = pendingListViewModel;
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
        public int GetIntValueLabel(Label tb)
        {
            try
            {
                return Convert.ToInt32(tb.Content);
            }
            catch (Exception ex)
            {
                //This is called if the converting failed for some reason
            }

            return 0; //This should only return 0 if the textbox does not contain a valid integer value
        }
        private void Button_Click_refresh(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            
                if (TXT_Name.Text != "" && TXT_Phone.Text != "" && TXT_Date.Text != "")
                {
                    if (TXT_Cost.Text != "" || TXT_Cost.Text != "0")
                    {
                        try
                        {
                            purchase.InsertPurchases("purchase", TXT_Name.Text, TXT_Phone.Text, TXT_Date.Text, GetDoubleValue(TXT_Cost), GetDoubleValue(TXT_Paid), GetDoubleValue(TXT_Remain), GetDoubleValue(TXT_Tax));
                            MessageBox.Show("تمت عملية الاضافة بنحاخ");
                            ResetValues();
                            GridView1.IsEnabled = true;
                            Menu_Panel.IsEnabled = false;
                            notifydatasetchanged();
                            Label_BIllnumber.Content = GetDoubleValueLabel(Label_BIllnumber) + 1;
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("حدثت مشكلة اثناء عملية الاضافة برجاء الاتصال بالمبرمج فى حالة تكرار هذة المشكلة");
                        }
                    }
                    else
                    {
                        MessageBox.Show("برجاء عدم ترك اى خانة فارغة");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء عدم ترك اى خانة فارغة");
                }
           
        }
        public void ResetValues()
        {
            GridView1.UnselectAll();
            TXT_Cost.Text = "";
            TXT_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            TXT_Name.Text = "";
            TXT_Paid.Text = "";
            TXT_Phone.Text = "";
            TXT_Pre.Text = "";
            TXT_Remain.Text = "";
            TXT_Tax.Text = "";
            Label_ID.Content = "0";
        }
        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            ResetValues();
            GridView1.IsEnabled = true;
            Menu_Panel.IsEnabled = false;
            btn_ADD.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Hidden;
            btn_ADDP.Visibility = Visibility.Visible;
            btn_EditP.Visibility = Visibility.Visible;

        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            var mywindow = Window.GetWindow(this);
            mywindow.Close();
        }

        private void Date_Picker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Date_Picker.SelectedDate.HasValue)
            {
                TXT_Date.Text = Date_Picker.SelectedDate.Value.ToString("yyyy/MM/dd");
            }
        }

        private void Btn_Date_Click(object sender, RoutedEventArgs e)
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

        private void GridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = GridView1.SelectedItem as Models.Purchase;
            if (selectedItem != null)
            {
                ResetValues();
                Btn_Delete.Visibility = Visibility.Visible;
                TXT_Cost.Text = selectedItem.Cost.ToString();
                TXT_Date.Text = selectedItem.Datex.ToString();
                TXT_Name.Text = selectedItem.Seller.ToString();
                TXT_Paid.Text = selectedItem.Paid.ToString();
                TXT_Phone.Text = selectedItem.Phonenumber.ToString();
                TXT_Remain.Text = selectedItem.Remain.ToString();
                TXT_Tax.Text = selectedItem.Tax.ToString();
                Label_ID.Content = selectedItem.Id.ToString();
                //Image_product.Source = new BitmapImage((ImagenSQLite.BitmapImageFromBytes(selectedItem.Image));


            }
        }

        private void TXT_Cost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXT_Cost.Text != "")
            {
                TXT_Paid.Text = (GetDoubleValue(TXT_Cost) - GetDoubleValue(TXT_Remain)).ToString();
                TXT_Remain.Text = (GetDoubleValue(TXT_Cost) - GetDoubleValue(TXT_Paid)).ToString();
            }
        }

        private void TXT_Paid_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXT_Paid.Text != "")
            {
                TXT_Remain.Text = (GetDoubleValue(TXT_Cost) - GetDoubleValue(TXT_Paid)).ToString();
            }

        }

        private void TXT_Remain_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXT_Remain.Text != "")
            {
                TXT_Paid.Text = (GetDoubleValue(TXT_Cost) - GetDoubleValue(TXT_Remain)).ToString();
            }
        }

        private void TXT_Tax_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextBox7.Text = (Val(TextBox4.Text) * (CDbl(TextBox9.Text) / 100))

        }

        private void TXT_Pre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXT_Pre.Text != "")
            {
                TXT_Tax.Text = (GetDoubleValue(TXT_Cost) * (GetDoubleValue(TXT_Pre) / 100)).ToString();
            }
        }

        private void Btn_Tax_Click(object sender, RoutedEventArgs e)
        {
            if (TXT_Pre.Text != "")
            {
                TXT_Tax.Text = (GetDoubleValue(TXT_Cost) * (GetDoubleValue(TXT_Pre) / 100)).ToString();
            }
        }

        private void Button_Edit_Add(object sender, RoutedEventArgs e)
        {
            if (GetIntValueLabel(Label_ID) != 0)
            {
                if (TXT_Name.Text != "" && TXT_Date.Text != "" && TXT_Phone.Text != "")
                {
                    if (GetDoubleValue(TXT_Cost) != 0)
                    {
                        try
                        {

                            bool succ = purchase.UpdatePurchase("purchase", GetIntValueLabel(Label_ID), TXT_Name.Text, TXT_Phone.Text, TXT_Date.Text, GetDoubleValue(TXT_Cost), GetDoubleValue(TXT_Paid), GetDoubleValue(TXT_Remain), GetDoubleValue(TXT_Tax));
                            if (succ == true)
                            {
                                MessageBox.Show("تمت عملية التعديل بنحاخ");
                                ResetValues();
                                notifydatasetchanged();
                                GridView1.IsEnabled = true;
                                Menu_Panel.IsEnabled = false;
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

        private void Label_ID_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((GetDoubleValueLabel(Label_ID) < GetDoubleValueLabel(Label_BIllnumber)))
            {
                Btn_Delete.Visibility = Visibility.Visible;
            }
            else
            {
                Btn_Delete.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_AddP(object sender, RoutedEventArgs e)
        {
            GridView1.IsEnabled = false;
            Menu_Panel.IsEnabled = true;
            ResetValues();
            Btn_Delete.Visibility = Visibility.Hidden;

            btn_ADDP.Visibility = Visibility.Hidden;
            btn_EditP.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Hidden;

            btn_ADD.Visibility = Visibility.Visible;
          
        }

        private void Button_Edit_AddP(object sender, RoutedEventArgs e)
        {
            if (GetIntValueLabel(Label_ID) != 0)
            {
               
                    GridView1.IsEnabled = false;
                    Menu_Panel.IsEnabled = true;
                    btn_ADDP.Visibility = Visibility.Hidden;
                    btn_EditP.Visibility = Visibility.Hidden;
                    btn_ADD.Visibility = Visibility.Hidden;
                    btn_Edit.Visibility = Visibility.Visible;
               
              
            }
            else
            {
                MessageBox.Show("برجاء تحديد سلعة اولا لتعديلها");
            }
        }

        private void TXT_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
