namespace Main
{
    class PrisBeregner
    {
        public static object BeregnPris(int postnummer, string område, int kvm)
        {

            double prisFaktor = FindPrisFaktor(postnummer, område);
            double kvmPris = 22000;
            //Udregning
            double pris = (kvm * kvmPris) * prisFaktor;
            return pris;
            
        }

        private static double FindPrisFaktor(int postnummer, string område)
        {

            //SQL code here

            //Temp code
            double prisFaktor;
            if (postnummer == 7100 && område == "Vestbyen")
            {
                prisFaktor = 0.75;
                return prisFaktor;
            }

            else if (postnummer == 7120 && område == "Bredballe")
            {
                prisFaktor = 1.25;
                return prisFaktor;
            }

            else if (postnummer == 7120 && område == "Midbyen")
            {
                prisFaktor = 1.25;
                return prisFaktor;
            }
            else
                return 0;

        }

        
    }
}
