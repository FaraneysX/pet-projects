using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Disk
    {
        public enum DiskFormat
        {
            CD,
            DVD,
            BlueRay
        }

        public string Name { get; set; }
        public DiskFormat Type { get; set; }
        public Film Film { get; set; }

        public Disk() { }
        public Disk(string name) => Name = name;
        public Disk(string name, DiskFormat type)
        {
            Name = name;
            Type = type;
        }
        public Disk(string name, DiskFormat type, Film film)
        {
            Name = name;
            Type = type;
            Film = film;
        }
    }
}
