using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

namespace RovaPOS
{
    /// <summary>
    /// Interaction logic for AddCategories.xaml
    /// </summary>
    public partial class AddCategories : Window
    {
        Manager.ImagenSQLite ImagenSQLite = new Manager.ImagenSQLite();
        Manager.Database.Categories categories = new Manager.Database.Categories();
        byte[] pic;
        public AddCategories()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            categories.InserCategories("categories" , TXT_CATA.Text , "" , pic);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = new DataSet();
            DataTable DT = categories.ReadAdapter("categories");

            DataGridView1.ItemsSource = DT.AsDataView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Image_product.Source = new BitmapImage(new Uri(op.FileName));
                System.Drawing.Image photo = new Bitmap(op.FileName);
                pic = ImagenSQLite.ImageToByte(photo, System.Drawing.Imaging.ImageFormat.Jpeg);
                //ImagenSQLite.SaveImage(pic , "info" , "Name");
                //Image_product.Source = ImagenSQLite.LoadImageByte(ImagenSQLite.LoadImage("info", "Name" , 2));
                //BLOB in DB
                //Image_product.Source = ImagenSQLite.LoadImage("info", "Name", 2);

            }
        }
    }
}
