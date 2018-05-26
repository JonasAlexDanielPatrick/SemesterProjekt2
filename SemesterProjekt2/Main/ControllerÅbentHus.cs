using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace Main
{
    class ControllerÅbentHus
    {

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
