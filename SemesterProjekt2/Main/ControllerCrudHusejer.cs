using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data;
using System.Diagnostics;


namespace Main
{
    class ControllerCrudHusejer
    {
        public static void LæsHusejer(DataGrid dg)
        {
            string sSQL = "SELECT * FROM Husejer";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Husejer");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }

        public static void OpretHusejer(string navn, string email, string telefon)
        {
            string sSQL = "INSERT INTO Husejer VALUES ('" + navn + "', '" + email + "', '" + telefon + "';";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
        }

        public static void OpdaterHusejer(int id, string navn, string email, string telefon)
        {
            if (navn != "Navn" && navn != "" && email != "Email" && email != "" && telefon != "Telefon" && telefon != "")
            {
                string sSQL = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
                              "BEGIN TRANSACTION" +
                              "UPDATE Husejer SET Navn ='" + navn + "', Email = '" + email + "', Telefon = '" + telefon + "' Where ID =" + id + ";" +
                              "COMMIT TRANSACTION;";

                SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
                command.ExecuteNonQuery();
            }
        }
    }
}
