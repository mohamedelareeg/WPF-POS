using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using RovaPOS.Models;

namespace RovaPOS.Manager
{
    class PendingListViewModel : INotifyPropertyChanged
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
        public ObservableCollection<Models.PendingSell> ConnectionItems
        {
            get { return _connectionitems; }
            set { _connectionitems = value;
                OnPropertyChanged("ConnectionItems");
            }
        }
        
        public PendingListViewModel(List<Models.Categories> categories, List<Models.PendingSell> _connectionitems)
        {
           
            this.ConnectionItems = new ObservableCollection<Models.PendingSell>(_connectionitems);
            this._view = new ListCollectionView(this._connectionitems);
            categoriesList = new ObservableCollection<Models.Categories>(categories);
            this._view_cata = new ListCollectionView(this.categoriesList);
            

        }

        private ListCollectionView _employeeCol;
        public ICollectionView EmployeeCollection
        {
            get { return this._employeeCol; }
        }

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
                    //System.Windows.MessageBox.Show("EE");
                }

                else
                {
                    //System.Windows.MessageBox.Show(value);
                    _view_cata.Filter = new Predicate<object>(o => ((Models.Categories)o).Name == value); // try SubString'
                    SearchValue = value;



                }

            }
        }
    }
}
