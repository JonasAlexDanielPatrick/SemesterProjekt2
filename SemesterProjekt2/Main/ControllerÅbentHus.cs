using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    class ControllerÅbentHus
    // Skrevet af Jonas og Alex
    {
        public static int antalMæglereValgt = 0;
        public static int antalEjendommeValgt = 0;
        public static List<ModelÅbentHusMægler> mæglere = new List<ModelÅbentHusMægler>();
        public static List<ModelÅbentHusEjendom> ejendomme = new List<ModelÅbentHusEjendom>();
        public static List<ModelÅbentHusMægler> valgteMæglere = new List<ModelÅbentHusMægler>();
        public static List<ModelÅbentHusEjendom> valgteEjendomme = new List<ModelÅbentHusEjendom>();
        public static List<ModelÅbentHus> åbentHusListe = new List<ModelÅbentHus>();

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

        public static void GenererListe(string udfil)
        {
            TagCheckedMæglereOgEjendomme();

            List<ModelÅbentHusEjendom> sorteretListeAfEjendomme = valgteEjendomme.OrderBy(ejendom => ejendom.Pris).ToList();
            sorteretListeAfEjendomme.Reverse();

            int mæglerNr = 0;
            while (sorteretListeAfEjendomme.Count > 0)
            {
                ModelÅbentHusEjendom ejendom = sorteretListeAfEjendomme[0];
                sorteretListeAfEjendomme.RemoveAt(0);
                ModelÅbentHusMægler mægler = valgteMæglere[mæglerNr];
                mæglerNr = (mæglerNr + 1) % 6;
                ModelÅbentHus åbenthus = new ModelÅbentHus(mægler.ID, mægler.Navn, ejendom.Sagsnr, ejendom.Adresse, ejendom.Område, ejendom.By, ejendom.Pris);
                åbentHusListe.Add(åbenthus);                
            }
            List<ModelÅbentHus> sorteretÅbentHusListe = åbentHusListe.OrderBy(sag => sag.MæglerId).ToList();

            Udskriv(udfil, sorteretÅbentHusListe);
        }

        public static void Udskriv(string udfil, List<ModelÅbentHus> liste)
        {
            StreamWriter stream = null;

            try
            {
                stream = new StreamWriter(udfil);
                stream.WriteLine(" Mægler ID  | Mægler Navn | SagsNR |          Adresse          |   Område   |     By     |   Pris");

                foreach (ModelÅbentHus item in liste)
                {
                    stream.Write(string.Format(" {0,-10} | ", item.MæglerId.ToString()));
                    stream.Write(string.Format("{0,-11} | ", item.MæglerNavn.ToString()));
                    stream.Write(string.Format("{0,-6} | ", item.Sagsnr.ToString()));
                    stream.Write(string.Format("{0,-25} | ", item.Adresse.ToString()));
                    stream.Write(string.Format("{0,-10} | ", item.Område.ToString()));
                    stream.Write(string.Format("{0,-10} | ", item.By.ToString()));
                    stream.WriteLine(string.Format("{0,-8}", item.Pris.ToString()));
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


        public static void TagCheckedMæglereOgEjendomme()
        {
            if (antalMæglereValgt == 6 && antalEjendommeValgt == 30)
            {
                foreach (ModelÅbentHusMægler mægler in mæglere)
                {
                    if (mægler.IsChecked == true)
                    {
                        valgteMæglere.Add(mægler);
                    }
                }

                foreach (ModelÅbentHusEjendom ejendom in ejendomme)
                {
                    if (ejendom.IsChecked == true)
                    {
                        valgteEjendomme.Add(ejendom);
                    }
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
