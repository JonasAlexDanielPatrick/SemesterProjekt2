using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Main
{
    class ControllerConnection
    // Skrevet Af Jonas
    {

        static string strconn = "Server=den1.mssql1.gear.host;Database=sweethome;Uid=sweethome;Pwd=sweethome123!;";


        public static SqlConnection conn = new SqlConnection(strconn);

        public static void Connect()
        {
            try
            {
                conn.Open();
                Debug.WriteLine("Succesfully connected to the database" + "\n");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to connect to the database" + "\n");
            }

        }

        public static void Postnr()
        {
            string sSQL = "SELECT * FROM Postnummer;";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Debug.WriteLine("| {0} | {1} |", reader.GetInt32(0).ToString(), reader.GetString(1));
            }
            reader.Close();
        }

    }
}
