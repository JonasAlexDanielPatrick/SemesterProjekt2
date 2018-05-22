using System.Diagnostics;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Main
{
    class ControllerKvmPris
    {
        public static void Vis(DataGrid dg, string startDato, string slutDato)
        {
            string sSQL = "select Sagsnr, Adresse, OmrådeNavn, Ejendom.Postnr, Postnummer.ByNavn, NuværendePris, BoligAreal, (NuværendePris/BoligAreal) as 'KvmPris' from Ejendom, Postnummer, Mægler where SalgsDato != '' AND Ejendom.Postnr = Postnummer.Postnr AND Ejendom.MæglerID = Mægler.ID and SalgsDato between '" + startDato + "' and '" + slutDato + "';";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Salgsstatistik");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }

        public static void Udskriv(DataGrid dg, string startDato, string slutDato, string udfil)
        {
            StreamWriter stream = null;

            try
            {
                string sSQL = "select Sagsnr, Adresse, OmrådeNavn, Ejendom.Postnr, Postnummer.ByNavn, NuværendePris, BoligAreal, (NuværendePris / BoligAreal) as 'KvmPris' from Ejendom, Postnummer, Mægler where SalgsDato != '' AND Ejendom.Postnr = Postnummer.Postnr AND Ejendom.MæglerID = Mægler.ID and SalgsDato between '" + startDato + "' and '" + slutDato + "'; ";
                SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Salgsstatistik");
                sda.Fill(dt);

                stream = new StreamWriter(udfil);

                stream.WriteLine("  Sagsnr  |        Adresse       |        Område        |  Postnr  |          By          |  Antal Kvm.  |     Pris     |  Kvm Pris.  ");

                foreach (DataRow row in dt.Rows)
                {
                    object[] array = row.ItemArray;

                    stream.Write(string.Format("  {0,-7} | ", array[0].ToString()));
                    stream.Write(string.Format("{0,-20} | ", array[1].ToString()));
                    stream.Write(string.Format("{0,-20} | ", array[2].ToString()));
                    stream.Write(string.Format("  {0,-6} | ", array[3].ToString()));
                    stream.Write(string.Format("{0,-20} | ", array[4].ToString()));
                    stream.Write(string.Format("{0,-12} | ", array[5].ToString()));
                    stream.Write(string.Format("{0,-12} | ", array[6].ToString()));
                    stream.WriteLine(string.Format("{0,-11}", array[7].ToString()));
                }

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
