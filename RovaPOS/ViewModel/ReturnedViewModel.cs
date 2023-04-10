using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using RovaPOS.Models;

namespace RovaPOS.Manager
{
    class ReturnedViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Models.Returned> _connectionitems = new ObservableCollection<Models.Returned>();
        Manager.Database.Categories categories = new Manager.Database.Categories();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        private ListCollectionView _view;
        public ICollectionView View
        {
            get { return this._view; }
        }
        private ListCollectionView _view_goods;
        public ICollectionView View_Goods
        {
            get { return this._view_goods; }

        }
        public ObservableCollection<Models.Returned> ConnectionItems
        {
            get { return _connectionitems; }
            set { _connectionitems = value;
                OnPropertyChanged("ConnectionItems");
            }
        }
        
        public ReturnedViewModel(List<Models.Returned> goods, List<Models.Returned> _connectionitems)
        {
           
            this.ConnectionItems = new ObservableCollection<Models.Returned>(_connectionitems);
            this._view = new ListCollectionView(this._connectionitems);
            goodsList = new ObservableCollection<Models.Returned>(goods);
            this._view_goods = new ListCollectionView(this.goodsList);

        }

       

        //private ListCollectionView _employeeCol;
        //public ICollectionView EmployeeCollection
        //{
        //    get { return this._employeeCol; }
        //}

      
        private ObservableCollection<Models.Returned> goodsList;
        public ObservableCollection<Models.Returned> GoodsList
        {
            get { return goodsList; }
            set
            {
                goodsList = value;
                OnPropertyChanged("GoodsList");
            }
        }

      
       
        private Models.Returned selectedgoods = null;
        public Models.Returned SelectedGoods
        {
            get { return selectedgoods; }
            set
            {
                selectedgoods = value;

                OnPropertyChanged("SelectedGoods");
                //MessageBox.Show(selectedEmployee.Name);
                //_view_cata.Filter = (item) => { return (item as Models.Categories).Name.StartsWith(value.Name) ? true : false; };

                // CategoriesList view has changed, Notify UI
                //OnPropertyChanged("CategoriesList");
            }
        }
       
    }
}
