using System.Configuration;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;

namespace Main
{
    class ControllerSalgsstatistik
    {
        public static void Vis(DataGrid dg, string startDato, string slutDato)
        {
            string sSQL = "select Sagsnr, Adresse, OmrådeNavn, Ejendom.Postnr, Postnummer.ByNavn, NuværendePris, SalgsDato, Mægler.Navn from Ejendom, Postnummer, Mægler where SalgsDato != '' AND Ejendom.Postnr = Postnummer.Postnr AND Ejendom.MæglerID = Mægler.ID and SalgsDato between '" + startDato + "' and '" + slutDato + "';";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }

    }
}
