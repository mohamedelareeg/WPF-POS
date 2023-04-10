using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Models
{
    public class Purchase
    {
        private int id;
        private string seller;
        private string phonenumber;
        private string datex;
        private double cost;
        private double paid;
        private double remain;
        private double tax;
        public Purchase()
        {

        }
        public Purchase(int id, string seller, string phonenumber, string datex, double cost, double paid, double remain, double tax)
        {
            this.id = id;
            this.seller = seller;
            this.phonenumber = phonenumber;
            this.datex = datex;
            this.cost = cost;
            this.paid = paid;
            this.remain = remain;
            this.tax = tax;
        }

        public int Id { get => id; set => id = value; }
        public string Seller { get => seller; set => seller = value; }
        public string Phonenumber { get => phonenumber; set => phonenumber = value; }
        public string Datex { get => datex; set => datex = value; }
        public double Cost { get => cost; set => cost = value; }
        public double Paid { get => paid; set => paid = value; }
        public double Remain { get => remain; set => remain = value; }
        public double Tax { get => tax; set => tax = value; }
    }
}
