using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Models
{
    public class Bills : INotifyPropertyChanged
    {
        private int id;
        private int billnumber;
        private double billcost;
        private string time;
        private string datex;
        private string ownername;
        private string ownerid;
        private string ownernumber;
        private double paid;
        private double remain;
        private double earned;
        private double tax;
        private double discount;
        private string details;

        public int Id { get => id; set => id = value; }
        public int Billnumber { get => billnumber; set => billnumber = value; }
        public double Billcost { get => billcost; set => billcost = value; }
        public string Time { get => time; set => time = value; }
        public string Datex { get => datex; set => datex = value; }
        public string Ownername { get => ownername; set => ownername = value; }
        public string Ownerid { get => ownerid; set => ownerid = value; }
        public string Ownernumber { get => ownernumber; set => ownernumber = value; }
        public double Paid { get => paid; set => paid = value; }
        public double Remain { get => remain; set => remain = value; }
        public double Earned { get => earned; set => earned = value; }
        public double Tax { get => tax; set => tax = value; }
        public double Discount { get => discount; set => discount = value; }
        public string Details { get => details; set => details = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public Bills()
        {
        }

        public Bills(int id, int billnumber, double billcost, string time, string datex, string ownername, string ownerid, string ownernumber, double paid, double remain, double earned, double tax, double discount, string details)
        {
            this.id = id;
            this.billnumber = billnumber;
            this.billcost = billcost;
            this.time = time;
            this.datex = datex;
            this.ownername = ownername;
            this.ownerid = ownerid;
            this.ownernumber = ownernumber;
            this.paid = paid;
            this.remain = remain;
            this.earned = earned;
            this.tax = tax;
            this.discount = discount;
            this.details = details;
        }
    }
}