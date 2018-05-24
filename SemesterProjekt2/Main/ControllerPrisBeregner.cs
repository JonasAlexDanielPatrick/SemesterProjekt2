
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Main
{
    class ControllerPrisBeregner
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
            SqlCommand cmd = new SqlCommand("SELECT PrisFaktor FROM Område WHERE Navn = '" + område + "' AND Postnr = " + postnummer + ";", ControllerConnection.conn);
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
