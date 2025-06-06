using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Film
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public Performer Performer { get; set; }

        public Film() { }
        public Film(string name)
        {
            Name = name;
        }
        public Film(string name, Genre genre)
        {
            Name = name;
            Genre = genre;
        }
        public Film(string name, Genre genre, Performer performer)
        {
            Name = name;
            Genre = genre;
            Performer = performer;
        }
    }
}
