using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RovaPOS.Manager
{
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        Manager.Database.Categories categories = new Manager.Database.Categories();
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public string SearchValue;

        public void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        public EmployeeListViewModel(List<Models.Categories> categories)//modified to public
        {
            EmployeeList = new ObservableCollection<Models.Categories>(categories);
            this._view = new ListCollectionView(this.employeeList);
        }

        #region nonModifiedCode

        private ListCollectionView _employeeCol;
        public ICollectionView EmployeeCollection
        {
            get { return this._employeeCol; }
        }

        private ObservableCollection<Models.Categories> employeeList;
        public ObservableCollection<Models.Categories> EmployeeList
        {
            get { return employeeList; }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private ListCollectionView _view;
        public ICollectionView View
        {
            get { return this._view; }
        }

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
                    View.Filter = null;
                    SearchValue = null;
                    //System.Windows.MessageBox.Show("EE");
                }
                    
                else
                {
                    //System.Windows.MessageBox.Show(value);
                    View.Filter = new Predicate<object>(o => ((Models.Categories)o).Name == value); // try SubString'
                    SearchValue = value;
                 


                }
                  
            }
        }

        #endregion

        //created for testing
        private List<Models.Categories> GetEmployees()
        {
           
            var mylist = new List<Models.Categories>();
            //mylist = categories.ReadCategoriesPic("categories");
            //mylist.Add(new Models.Categories() { Name = "nummer1" });
            //mylist.Add(new Models.Categories() { Name = "nummer2" });

            return mylist;
        }
    }
}
