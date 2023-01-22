using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazeneo4j.Models
{
    public class Klub
    {
        public string Naziv { get; }
        public int Broj_klubova { get; }
        public string Drzava { get; }

        public Klub(string naziv, int broj_klubova, string drzava)
        {
            Naziv = naziv;
            Broj_klubova = broj_klubova;
            Drzava = drzava;
        }
    }
}