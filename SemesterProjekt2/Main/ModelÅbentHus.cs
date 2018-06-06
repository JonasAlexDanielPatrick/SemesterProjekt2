
namespace Models
// Skrevet af Jonas
{
    class ModelÅbentHus
    {
        public int MæglerId { get; set; }
        public string MæglerNavn { get; }
        public int Sagsnr { get; }
        public string Adresse { get; }
        public string Område { get; }
        public string By { get; }
        public int Pris { get; }
        

        public ModelÅbentHus(int mæglerid, string mæglernavn, int sagsnr, string adresse, string område, string by, int pris)
        {
            MæglerId = mæglerid;
            MæglerNavn = mæglernavn;
            Sagsnr = sagsnr;
            Adresse = adresse;
            Område = område;
            By = by;
            Pris = pris;
        }
    }
}
