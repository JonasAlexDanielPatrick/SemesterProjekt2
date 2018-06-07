using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Controls;

namespace Controllers
{
    class ControllerPrisBeregner
    // Skrevet af Patrick
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
            SqlDataReader reader = null;
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        prisFaktor = Convert.ToDouble((reader["PrisFaktor"]));

                    }
                }
                return prisFaktor;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to connect to the database" + "\n");
                return 0;
            }
            finally
            {
                reader.Close();
            }




        }

        public static void ComboBoxOpretPostnr(ComboBox comboboxPrisBeregner_Postnr)
        {
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Postnr FROM Område;", ControllerConnection.conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int sPostnr = reader.GetInt32(0);
                    bool hasItem = comboboxPrisBeregner_Postnr.Items.Contains(sPostnr);

                    if (hasItem)
                    {
                        Debug.WriteLine("Postnummer findes allerede!");
                    }
                    else
                    {
                        comboboxPrisBeregner_Postnr.Items.Add(sPostnr);
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Could not find database/table" + "\n");
            }
            finally
            {
                reader.Close();
            }

        }

        public static void ComboBoxOpretNavn(ComboBox comboboxPrisBeregner_Navn, ComboBox comboboxPrisBeregner_Postnr)
        {
            comboboxPrisBeregner_Navn.Items.Clear();
            SqlCommand cmd = new SqlCommand("SELECT Navn FROM Område WHERE Postnr = " + comboboxPrisBeregner_Postnr.Text + ";", ControllerConnection.conn);
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    string sName = reader.GetString(reader.GetOrdinal("Navn"));
                    comboboxPrisBeregner_Navn.Items.Add(sName);

                }
               
            }
            catch (Exception)
            {
                Debug.WriteLine("Could not find database/table - CLOSE");
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
