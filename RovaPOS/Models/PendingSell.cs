using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Models
{
    public class PendingSell : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string category;
        private double quantity;
        private double cost;
        private double price;
        private string type;
        private string time;
        private string datex;
        private string barcode;
        private double earned;
        private string details;
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public PendingSell()
        {
        }

        public PendingSell(int id, string name, string category, double quantity, double cost, double price, string type, string time, string datex, string barcode, double earned, string details)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.quantity = quantity;
            this.cost = cost;
            this.price = price;
            this.type = type;
            this.time = time;
            this.datex = datex;
            this.barcode = barcode;
            this.earned = earned;
            this.details = details;
        }

        public int Id { get => id; set => id = value;  }
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }
        public string Category { get => category; set => category = value; }
       
        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged("Quantity"); }
        }

        public double Cost
        {
            get { return cost; }
            set { cost = value; NotifyPropertyChanged("Cost"); }
        }
        public double Price
        {
            get { return price; }
            set { price = value; NotifyPropertyChanged("Price");
                NotifyPropertyChanged("PriceBrush");
            }
        }
        public string PriceBrush
        {
            get
            {
                if (Price < (Cost * Quantity))
                {

                    return "Red";
                }
                else
                {
                    return "Black";
                }
               
            }
        }
        public string Type { get => type; set => type = value; }
        public string Time { get => time; set => time = value; }
        public string Datex { get => datex; set => datex = value; }
        public string Barcode { get => barcode; set => barcode = value; }
        public double Earned
        {
            get { return earned; }
            set { earned = value; NotifyPropertyChanged("Earned"); }
        }
        public string Details
        {
            get { return details; }
            set { details = value; NotifyPropertyChanged("Details"); }
        }
    }
       
}
