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
        public static int antalMæglereValgt = 0;
        public static int antalEjendommeValgt = 0;
        public static List<ModelÅbentHusMægler> mæglere = new List<ModelÅbentHusMægler>();
        public static List<ModelÅbentHusEjendom> ejendomme = new List<ModelÅbentHusEjendom>();
        public static List<ModelÅbentHusMægler> valgteMæglere = new List<ModelÅbentHusMægler>();
        public static List<ModelÅbentHusEjendom> valgteEjendomme = new List<ModelÅbentHusEjendom>();

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

        public static void MæglerClicked(ModelÅbentHusMægler mægler)
        {
            if (mægler.IsChecked == false)
            {
                antalMæglereValgt--;
            }
            else
            {
                antalMæglereValgt++;
            }
        }

        }

        public static void TagCheckedMæglere(DataGrid dg)
        {
            foreach (ModelÅbentHusMægler mægler in mæglere)
            {
                if (mægler.IsChecked == false)
                {
                    Debug.Write("NO!");
                }
                else
                {
                    Debug.Write("WOOOOW!");
                }
            }


        public static void EjendomClicked(ModelÅbentHusEjendom ejendom)
        {
            if (ejendom.IsChecked == false)
            {
                antalEjendommeValgt--;
            }
            else
            {
                antalEjendommeValgt++;
            }
        }
    }
}
