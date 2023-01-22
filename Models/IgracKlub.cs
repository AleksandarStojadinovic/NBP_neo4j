using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazeneo4j.Models
{
    public class IgracKlub
    {
        public string Klub { get; }
        public int Od { get; }
        public int Do { get; }
        public int Broj_dresa { get; }

        public IgracKlub(string klub, int od,int doo, int broj_dresa)
        {
            Klub = klub;
            Od = od;
            Do = doo;
            Broj_dresa = broj_dresa;
        }
    }
}
