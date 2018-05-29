using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelÅbentHusEjendom
    {
        public int Sagsnr { get; }
        public string Adresse { get; }
        public string Område { get; }
        public string By { get; }
        public int Pris { get; }

        public ModelÅbentHusEjendom(int sagsnr, string adresse, string område, string by, int pris)
        {
            Sagsnr = sagsnr;
            Adresse = adresse;
            Område = område;
            By = by;
            Pris = pris;
        }

    }
}
