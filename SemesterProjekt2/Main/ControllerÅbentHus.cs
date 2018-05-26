using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;

namespace Main
{
    class ControllerÅbentHus
    {
        public static void FyldMæglerDatagrid(DataGrid dg)
        {

        }

        public static void FyldEjendomDatagrid(DataGrid dg)
        {
            string sSQL = "select Sagsnr, Adresse, OmrådeNavn, Postnummer.ByNavn, NuværendePris from Ejendom, Postnummer where Salgsdato = '' AND Ejendom.Postnr = Postnummer.Postnr;";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Ejendom");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }

        public static void GenererListe(string udfil)
        {


            Udskriv(udfil);
        }

        public static void Udskriv(string udfil)
        {
            StreamWriter stream = null;

            try
            {
                // Udskriv fil her!

            }
            catch (System.Exception)
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

    }
}
