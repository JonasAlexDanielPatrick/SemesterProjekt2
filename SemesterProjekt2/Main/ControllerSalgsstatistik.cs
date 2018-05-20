using System.Configuration;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;

namespace Main
{
    class ControllerSalgsstatistik
    {
        public static void Vis(DataGrid dg)
        {
            string sSQL = "select Sagsnr, Adresse, OmrådeNavn, Ejendom.Postnr, Postnummer.ByNavn, NuværendePris, SalgsDato, Mægler.Navn from Ejendom, Postnummer, Mægler where SalgsDato != '' AND Ejendom.Postnr = Postnummer.Postnr AND Ejendom.MæglerID = Mægler.ID;";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }

    }
}
