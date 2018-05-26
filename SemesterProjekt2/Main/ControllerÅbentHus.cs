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
