using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Models
{
    public class Customers
    {
        private int id;
        private string ownername;
        private string ownerid;
        private string ownernumber;
        private double paid;
        private double remain;

        public Customers()
        {

        }

        public Customers(int id, string ownername, string ownerid, string ownernumber, double paid, double remain)
        {
            this.id = id;
            this.ownername = ownername;
            this.ownerid = ownerid;
            this.ownernumber = ownernumber;
            this.paid = paid;
            this.remain = remain;
        }

        public int Id { get => id; set => id = value; }
        public string Ownername { get => ownername; set => ownername = value; }
        public string Ownerid { get => ownerid; set => ownerid = value; }
        public string Ownernumber { get => ownernumber; set => ownernumber = value; }
        public double Paid { get => paid; set => paid = value; }
        public double Remain { get => remain; set => remain = value; }
    }
}
