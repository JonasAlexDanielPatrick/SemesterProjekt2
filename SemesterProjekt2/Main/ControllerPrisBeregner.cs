
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Main
{
    class ControllerPrisBeregner
    {
      
        public static object BeregnPris(int postnr, string navn, int antalKvm)
        {

            double prisFaktor = FindPrisFaktor(postnr, navn);
            double kvmPris = 22000;
            //Udregning
            double vurderingsPris = (antalKvm * kvmPris) * prisFaktor;
            return vurderingsPris;

        }

        private static double FindPrisFaktor(int postnr, string navn)
        {

            //SQL code here
            SqlCommand cmd = new SqlCommand("SELECT PrisFaktor FROM Område WHERE Navn = '" + navn + "' AND Postnr = " + postnr + ";", ControllerConnection.conn);
            double prisFaktor = 0;
            try
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        prisFaktor = Convert.ToDouble((read["PrisFaktor"]));

                    }
                }
                return prisFaktor;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to connect to the database" + "\n");
                return 0;
            }




        }


    }
}
