using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazeneo4j.Models
{
    public class Igrac
    {
        public string Ime { get; }
        public int Godina_rodjenja { get; }
        public string Nacionalnost { get; }

        public Igrac(string ime, int godina_rodjenja, string nacionalnost)
        {
            Ime = ime;
            Godina_rodjenja = godina_rodjenja;
            Nacionalnost = nacionalnost;
        }
    }
}
