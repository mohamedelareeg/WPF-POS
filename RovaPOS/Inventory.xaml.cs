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
using System.Windows.Shapes;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

using System.Threading;
using FireSharp;
using NUnit.Framework;

namespace RovaPOS
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "MkDMPYqhwJr1q6naSn4f95Uz41Q4ZGuUK7nAwkTz",
            BasePath = "https://rovapos.firebaseio.com/"

        };
        IFirebaseClient client;
        List<Models.Goods> goods_list = new List<Models.Goods>();
        UserControlers.Inventory_UserControl _UserControl = new UserControlers.Inventory_UserControl();
        UserControlers.Inventory_Load_UserControl _UserControl2 = new UserControlers.Inventory_Load_UserControl();
        Manager.Database.Goods goods = new Manager.Database.Goods();
        public Inventory()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            if(client !=null)
            {
                //MessageBox.Show("Connection Established");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            Panel_Add.Child = _UserControl;
            //Panel_Load.Child = _UserControl2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InsertGoods();
        }
        public void InsertGoods()
        {
            //string buf = ((BitmapFrame)(Image_product as System.Windows.Controls.Image).Source).BaseUri.ToString();
            //Image_product.Source = new BitmapImage(new Uri(op.FileName));
            goods.InsertGoods("goods", _UserControl.Name, _UserControl.Category, _UserControl.Quantity, _UserControl.Cost, _UserControl.Price, _UserControl.Type, _UserControl.Barcode, _UserControl.Earned, DateTime.Now.ToString("yyyy/MM/dd"), "");
            InserCounterFirebase();
        }
        public async void insertFirebase()
        {
            var goods_List = new Models.Goods();
            goods_List.Name = _UserControl.Name;
            goods_List.Category = _UserControl.Category;
            goods_List.Quantity = _UserControl.Quantity;
            goods_List.Cost = _UserControl.Cost;
            goods_List.Price = _UserControl.Price;
            goods_List.Type = _UserControl.Type;
            goods_List.Datex = DateTime.Now.ToString("yyyy/MM/dd");
            goods_List.Datee = "";
            goods_List.Barcode = _UserControl.Barcode;
            goods_List.Earned = _UserControl.Earned;
            SetResponse response = await client.SetAsync("Goods/" + _UserControl.Barcode, goods_List);
            Models.Goods result = response.ResultAs<Models.Goods>();
            MessageBox.Show("تم الاضافة بنجاح الى قاعدة البيانات");
        }
        public async void ReadFirebase()
        {
            FirebaseResponse response = await client.GetAsync("Goods/" + _UserControl.Barcode);
            Models.Goods result = response.ResultAs<Models.Goods>();
            MessageBox.Show(result.Barcode);
        }
        public async void InsertFirebase(List<Models.Goods> goods)
        {
            for (int i = 0; i < goods.Count; i++)
            {
                
                SetResponse response = await client.SetAsync("Goods/" + goods[i].Barcode, goods[i]);
                Models.Goods result = response.ResultAs<Models.Goods>();
                //MessageBox.Show(result.Barcode);
            }



        }
        public async void ReadFirebase(List<Models.Goods> goods)
        {
            for (int i = 0; i < goods.Count; i++)
            {
                FirebaseResponse response = await client.GetAsync("Goods/" + goods[i].Barcode);
                Models.Goods result = response.ResultAs<Models.Goods>();
                //MessageBox.Show(result.Barcode);
                if(result.Quantity == 0)
                {
                    MessageBox.Show(result.Name);
                }
            }
           
          

        }

        public async void InserCounterFirebase()
        {
            FirebaseResponse cnt = await client.GetAsync("Counter/Goods");
            Models.Counter counter = cnt.ResultAs<Models.Counter>();
            var goods_List = new Models.Goods();
            goods_List.Id = (Convert.ToInt32(counter.Cnt) + 1);
            goods_List.Name = _UserControl.Name;
            goods_List.Category = _UserControl.Category;
            goods_List.Quantity = _UserControl.Quantity;
            goods_List.Cost = _UserControl.Cost;
            goods_List.Price = _UserControl.Price;
            goods_List.Type = _UserControl.Type;
            goods_List.Datex = DateTime.Now.ToString("yyyy/MM/dd");
            goods_List.Datee = "";
            goods_List.Barcode = _UserControl.Barcode;
            goods_List.Earned = _UserControl.Earned;
            SetResponse response = await client.SetAsync("Goods/" + _UserControl.Barcode, goods_List);
            var cont = new Models.Counter
            {
                Cnt = goods_List.Id
            };
            SetResponse response_cont = await client.SetAsync("Counter/Goods" ,cont);
            Models.Goods result = response.ResultAs<Models.Goods>();
        }
        public async void UpdateFirebase()
        {
            var goods_List = new Models.Goods();
            goods_List.Name = _UserControl.Name;
            goods_List.Category = _UserControl.Category;
            goods_List.Quantity = _UserControl.Quantity;
            goods_List.Cost = _UserControl.Cost;
            goods_List.Price = _UserControl.Price;
            goods_List.Type = _UserControl.Type;
            goods_List.Datex = DateTime.Now.ToString("yyyy/MM/dd");
            goods_List.Datee = "";
            goods_List.Barcode = _UserControl.Barcode;
            goods_List.Earned = _UserControl.Earned;
            FirebaseResponse response = await client.UpdateAsync("Goods/" + _UserControl.Barcode, goods_List);
            Models.Goods result = response.ResultAs<Models.Goods>();
            MessageBox.Show("تم التعديل بنجاح فى قاعدة البيانات ");
        }
        public async void DeleteFirebase()
        {
            FirebaseResponse response = await client.DeleteAsync("Goods/" + _UserControl.Barcode);
          
            MessageBox.Show("Deleted ");
        }
        public async void DeleteAllFirebase()
        {
            FirebaseResponse response = await client.DeleteAsync("Goods");

            MessageBox.Show("Deleted ");
        }
        public async void GetWithQueryAsync()
        {
            await client.PushAsync("todos/get/pushAsync", new Todo
            {
                name = "Execute PUSH4GET",
                priority = 2
            });

            await client.PushAsync("todos/get/pushAsync", new Todo
            {
                name = "You PUSH4GET",
                priority = 2
            });

            Thread.Sleep(400);

            var response = await client.GetAsync("todos", QueryBuilder.New().OrderBy("$key").StartAt("Exe"));
            Assert.NotNull(response);
            Assert.IsTrue(response.Body.Contains("name"));
            Todo result = response.ResultAs<Todo>();
            MessageBox.Show(result.name);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            goods_list = _UserControl2.Goods_list;
            ReadFirebase(goods_list);
            //ReadListFirebase();
            //MessageBox.Show(goods_list.Count.ToString());
        }
      
        private async void ReadListFirebase()
        {
          
            int i = 0;
            FirebaseResponse response = await client.GetAsync("Counter/Goods");
            Models.Counter cont = response.ResultAs<Models.Counter>();
            int cnt = cont.Cnt;
            while (true)
            {
                i++;
                try
                {
                    FirebaseResponse response_query = await client.GetAsync("Goods/", QueryBuilder.New());
                    Models.Goods goods = response_query.ResultAs<Models.Goods>();
                    MessageBox.Show(goods.Barcode);
                    goods_list.Add(goods);
                }
                catch (Exception)
                {

                    throw;
                }
            }

           
        }
    }
}
