using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Models
{
    public class Categories
    {
        private int id;
        private string name;
        private string type;
        private byte[] image;
      

        public Categories()
        {
        }

        public Categories(int id, string name, string type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }

        public Categories(int id, string name, string type, byte[] image)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.image = image;
        }

        public string Type { get => type; set => type = value; }
        public byte[] Image { get => image; set => image = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
