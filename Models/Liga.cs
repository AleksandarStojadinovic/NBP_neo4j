using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazeneo4j.Models
{
    public class Liga
    {
        public string Naziv { get; }
        public int Osnovan { get; }
        public string Stadion { get; }

        public Liga(string naziv, int osnovan, string stadion)
        {
            Naziv = naziv;
            Osnovan = osnovan;
            Stadion = stadion;
        }
    }
}
