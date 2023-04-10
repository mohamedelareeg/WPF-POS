using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Models
{
    public class Info
    {
        private int id;
        private string name;
        private string ownername;
        private string ownerphone;
        private string location;
        private string password;
        private string managerpass;

        public Info(int id, string name, string ownername, string ownerphone, string location, string password, string managerpass)
        {
            this.id = id;
            this.name = name;
            this.ownername = ownername;
            this.ownerphone = ownerphone;
            this.location = location;
            this.password = password;
            this.managerpass = managerpass;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Ownername { get => ownername; set => ownername = value; }
        public string Ownerphone { get => ownerphone; set => ownerphone = value; }
        public string Location { get => location; set => location = value; }
        public string Password { get => password; set => password = value; }
        public string Managerpass { get => managerpass; set => managerpass = value; }
    }
}
