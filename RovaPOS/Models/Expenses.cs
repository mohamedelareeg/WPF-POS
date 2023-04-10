using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Models
{
    public class Expenses
    {
        private int id;
        private string name;
        private string owner;
        private string datex;
        private double cost;
        private string details;
     
        public Expenses()
        {

        }

        public Expenses(int id, string name, string owner, string datex, double cost, string details)
        {
            this.id = id;
            this.name = name;
            this.owner = owner;
            this.datex = datex;
            this.cost = cost;
            this.details = details;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Owner { get => owner; set => owner = value; }
        public string Datex { get => datex; set => datex = value; }
        public double Cost { get => cost; set => cost = value; }
        public string Details { get => details; set => details = value; }
    }
}
