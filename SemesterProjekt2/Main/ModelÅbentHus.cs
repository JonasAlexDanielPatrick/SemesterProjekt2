using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelÅbentHus
    {
        public string Mægler { get; }
        public int Sagsnr { get; }
        public string Adresse { get; }
        public string Område { get; }
        public string By { get; }
        public int Pris { get; }
        

        public ModelÅbentHus(string mægler, int sagsnr, string adresse, string område, string by, int pris)
        {
            Mægler = mægler;
            Sagsnr = sagsnr;
            Adresse = adresse;
            Område = område;
            By = by;
            Pris = pris;
        }
    }
}
