using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class ModelTestDataEjendom
    {
        public int Sagsnr { get; }
        public int MæglerID { get; }
        public int HusejerID { get; }
        public ModelTestDataOmråde Område { get; }
        public string EnergiMærke { get; }
        public string StartDato { get; }
        public string SlutDato { get; }
        public string Adresse { get; }
        public int StartPris { get; }
        public int NuværendePris { get; }
        public int GrundAreal { get; }
        public int KælderAreal { get; }
        public int BoligAreal { get; }
        public int Byggeår { get; }
        public bool GarageCarport { get; }


        public ModelTestDataEjendom(int sagsnr, int mæglerId, int husejerId, ModelTestDataOmråde område, string energimærke, string startdato, string slutdato, string adresse, int startpris, int nuværendepris, int grundareal, int kælderareal, int boligareal, int byggeår, bool garageCarport)
        {
            Sagsnr = sagsnr;
            MæglerID = mæglerId;
            HusejerID = husejerId;
            Område = område;
            EnergiMærke = energimærke;
            StartDato = startdato;
            SlutDato = slutdato;
            Adresse = adresse;
            StartPris = startpris;
            NuværendePris = nuværendepris;
            GrundAreal = grundareal;
            KælderAreal = kælderareal;
            BoligAreal = boligareal;
            Byggeår = byggeår;
            GarageCarport = garageCarport;
        }

    }
}
