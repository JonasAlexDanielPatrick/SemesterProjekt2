using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelOmråde
    {
        public string Navn { get; }
        public int Postnr { get; }
        public string By { get; }
        public float PrisFaktor { get; }

        public ModelOmråde(string navn, int postnr, string by, float prisfaktor)
        {
            Navn = navn;
            Postnr = postnr;
            By = by;
            PrisFaktor = prisfaktor;
        }

    }
}
