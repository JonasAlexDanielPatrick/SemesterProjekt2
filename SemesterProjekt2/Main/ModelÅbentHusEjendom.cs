namespace Models
{
    class ModelÅbentHusEjendom
    // Skrevet af Jonas
    {
        public int Sagsnr { get; }
        public string Adresse { get; }
        public string Område { get; }
        public string By { get; }
        public int Pris { get; }
        public bool IsChecked { get; set; }

        public ModelÅbentHusEjendom(int sagsnr, string adresse, string område, string by, int pris)
        {
            Sagsnr = sagsnr;
            Adresse = adresse;
            Område = område;
            By = by;
            Pris = pris;
            IsChecked = false;
        }

    }
}
