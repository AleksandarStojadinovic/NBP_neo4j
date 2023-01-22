using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazeneo4j.Models
{
    public class LigaKlub
    {
        public Liga Liga { get; }
        public Klub Klub { get; }
        public int Broj_titula { get; }

        public LigaKlub(Liga liga, Klub klub, int broj_titula)
        {
            Liga = liga;
            Klub = Klub;
            Broj_titula = broj_titula;
        }
    }
}