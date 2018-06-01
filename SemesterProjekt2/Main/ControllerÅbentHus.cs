using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using Models;
using System.Collections.Generic;

namespace Main
{
    class ControllerÅbentHus
    {
        public static List<ModelÅbentHusMægler> mæglere = new List<ModelÅbentHusMægler>();
        public static List<ModelÅbentHusEjendom> ejendomme = new List<ModelÅbentHusEjendom>();

        public static void FyldMæglerDatagrid(DataGrid dg)
        {
            string sSQL = "select ID, Navn from Mægler;";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Mæglere");
            sda.Fill(dt);

            mæglere.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object[] array = row.ItemArray;
                mæglere.Add(new ModelÅbentHusMægler((int)array[0], array[1].ToString()));
            }
            dg.DataContext = mæglere;
        }

        public static void FyldEjendomDatagrid(DataGrid dg)
        {
            string sSQL = "select Sagsnr, Adresse, OmrådeNavn, Postnummer.ByNavn, NuværendePris from Ejendom, Postnummer where Salgsdato = '' AND Ejendom.Postnr = Postnummer.Postnr;";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Ejendomme");
            sda.Fill(dt);

            ejendomme.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object[] array = row.ItemArray;
                ejendomme.Add(new ModelÅbentHusEjendom((int)array[0], array[1].ToString(), array[2].ToString(), array[3].ToString(), (int)array[4]));
            }
            dg.DataContext = ejendomme;

        }

        public static void GenererListe(DataGrid dg, string udfil)
        {
            Udskriv(dg, udfil);
        }

        public static void Udskriv(DataGrid dg, string udfil)
        {
            TælAntalValgteMæglere(dg);

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

        public static void TælAntalValgteMæglere(DataGrid dg)
        {
            DataTable dt = new DataTable();
            dt = ((DataView)dg.ItemsSource).ToTable();

            foreach (DataRow row in dt.Rows)
            {
                Debug.WriteLine(row.Table.Columns.Count);
                object[] array = row.ItemArray;

                Debug.WriteLine(array[0] + " | " + array[1]);
            }
        }

    }
}
