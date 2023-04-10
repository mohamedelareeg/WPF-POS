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
    class PendingListViewModelR : INotifyPropertyChanged
    {
        private ObservableCollection<Models.PendingSell> _connectionitems = new ObservableCollection<Models.PendingSell>();
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
        private ListCollectionView _view_cata;
        public ICollectionView View_Cata
        {
            get { return this._view_cata; }
        }
        private ListCollectionView _view_goods;
        public ICollectionView View_Goods
        {
            get { return this._view_goods; }

        }
        public ObservableCollection<Models.PendingSell> ConnectionItems
        {
            get { return _connectionitems; }
            set { _connectionitems = value;
                OnPropertyChanged("ConnectionItems");
            }
        }
        
        public PendingListViewModelR(List<Models.Categories> categories , List<Models.GoodsR> goods, List<Models.PendingSell> _connectionitems)
        {
           
            this.ConnectionItems = new ObservableCollection<Models.PendingSell>(_connectionitems);
            this._view = new ListCollectionView(this._connectionitems);
            categoriesList = new ObservableCollection<Models.Categories>(categories);
            this._view_cata = new ListCollectionView(this.categoriesList);
            goodsList = new ObservableCollection<Models.GoodsR>(goods);
            this._view_goods = new ListCollectionView(this.goodsList);

        }

        public PendingListViewModelR(List<GoodsR> goods, List<PendingSell> _connectionitems)
        {
           
            goodsList = new ObservableCollection<Models.GoodsR>(goods);
            this._view_goods = new ListCollectionView(this.goodsList);
            this.ConnectionItems = new ObservableCollection<Models.PendingSell>(_connectionitems);
            this._view = new ListCollectionView(this._connectionitems);
        }

        //private ListCollectionView _employeeCol;
        //public ICollectionView EmployeeCollection
        //{
        //    get { return this._employeeCol; }
        //}

        private ObservableCollection<Models.Categories> categoriesList;
        public ObservableCollection<Models.Categories> CategoriesList
        {
            get { return categoriesList; }
            set
            {
                categoriesList = value;
                OnPropertyChanged("CategoriesList");
            }
        }
        private ObservableCollection<Models.GoodsR> goodsList;
        public ObservableCollection<Models.GoodsR> GoodsList
        {
            get { return goodsList; }
            set
            {
                goodsList = value;
                OnPropertyChanged("GoodsList");
            }
        }

        private Models.Categories selectedcata = null;
        public Models.Categories SelectedCata
        {
            get { return selectedcata; }
            set
            {

                selectedcata = value;
                OnPropertyChanged("SelectedCata");
                //_view_goods.Filter = (item) => { return (item as Models.Goods).Category.StartsWith(value.Name) ? true : false; };
               
                //MessageBox.Show(selectedEmployee.Name);
                //_view_cata.Filter = (item) => { return (item as Models.Categories).Name.StartsWith(value.Name) ? true : false; };

                // CategoriesList view has changed, Notify UI
                //OnPropertyChanged("CategoriesList");
            }
        }
        public void loadGoods()
        {
            if (selectedcata != null)
            {
                if (selectedcata.Id != 0)
                {

                    _view_goods.Filter = (item) => { return (item as Models.GoodsR).Type.Contains(selectedcata.Name) ? true : false; };
                }
            }
            selectedcata = null;
            OnPropertyChanged("SelectedCata");
        }
        private Models.GoodsR selectedgoods = null;
        public Models.GoodsR SelectedGoods
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
        public string SearchValue;
        private string _TextSearch;
        public string TextSearch
        {
            get { return _TextSearch; }
            set
            {
                _TextSearch = value;
                OnPropertyChanged("TextSearch");

                if (String.IsNullOrEmpty(value))
                {
                    _view_cata.Filter = null;
                    SearchValue = null;
                    selectedcata = null;
                    //System.Windows.MessageBox.Show("EE");
                }

                else
                {
                    //System.Windows.MessageBox.Show(value);
                    //_view_cata.Filter = new Predicate<object>(o => ((Models.Categories)o).Name == value); // try SubString'
                    _view_cata.Filter = (item) => { return (item as Models.Categories).Name.Contains(value) ? true : false; };
                    //OnPropertyChanged("CategoriesList");
                    SearchValue = value;
                    selectedcata = null;



                }

            }
        }
        private string _TextSearchGoods;
        private List<GoodsR> goods_list;
        private List<PendingSell> pending_goods;

        public string TextSearchGoods
        {
            get { return _TextSearchGoods; }
            set
            {
                _TextSearchGoods = value;
                OnPropertyChanged("TextSearchGoods");

                if (String.IsNullOrEmpty(value))
                {
                    _view_goods.Filter = null;

                    selectedgoods = null;
                    //System.Windows.MessageBox.Show("EE");
                }

                else
                {
                    //System.Windows.MessageBox.Show(value);
                    //_view_cata.Filter = new Predicate<object>(o => ((Models.Categories)o).Name == value); // try SubString'
                    _view_goods.Filter = (item) => { return (item as Models.GoodsR).Name.Contains(value) ? true : false; };
                    //OnPropertyChanged("CategoriesList");
                  
                    selectedgoods = null;



                }

            }
        }
    }
}
