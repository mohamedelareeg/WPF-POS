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
    public partial class Sells : Window
    {
        
      
        UserControlers.Sells_UserControl _UserControl = new UserControlers.Sells_UserControl();

        public Sells()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            Panel_Add.Child = _UserControl;
            //Panel_Load.Child = _UserControl2;
        }


    }
}
