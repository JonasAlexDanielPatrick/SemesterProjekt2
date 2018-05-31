using System;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data;
using System.Linq;

namespace Main
{
    class ControllerCrudMægler
    {
        public static void LæsMægler(DataGrid dg) //
        {
            string sSQL = "SELECT * FROM Mægler";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Mægler");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }

        public static void OpretMægler(string navn, string telefon, string email) //
        {
            string sSQL = "INSERT INTO Mægler VALUES ('" + navn + "', '" + telefon + "', '" + email + "');";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
        }

        public static void OpdaterMægler(int id, string navn, string telefon, string email) // 
        {
            string tempSSQL = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
                              "BEGIN TRANSACTION;" +
                              "UPDATE Mægler SET";

            if (navn != "Navn" && navn != "")
            {
                tempSSQL += " Navn = '" + navn + "'";
            }

            if (telefon != "Telefon" && telefon != "")
            {
                if (tempSSQL.Contains("Navn"))
                {
                    tempSSQL += ", Telefon = '" + telefon + "'";
                }
                else
                {
                    tempSSQL += " Telefon = '" + telefon + "'";
                }
            }

            if (email != "Email" && email != "")
            {
                if (tempSSQL.Contains("Navn") || tempSSQL.Contains("Telefon"))
                {
                    tempSSQL += ", Email = '" + email + "'";
                }
                else
                {
                    tempSSQL += " Email = '" + email + "'";
                }
            }

            tempSSQL += "Where ID = '" + id + "' COMMIT TRANSACTION;";

            string sSQL = tempSSQL;
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();

        }

        public static void SletMægler(int id)  //
        {
            string sSQL = "DELETE FROM Mægler WHERE ID = '" + id + "';";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
        }

        public static void SøgMægler(string id, string navn, string telefon, string email, DataGrid dg) // virker
        {
            string tempSSQL = "SELECT * FROM Mægler WHERE";

            if (Convert.ToString(id) != "ID (Autogenereres)" && Convert.ToString(id) != "")
            {
                tempSSQL += " ID = '" + id + "'";
            }

            if (navn != "Navn" && navn != "")
            {
                if (tempSSQL.Contains("ID"))
                {
                    tempSSQL += " AND Navn LIKE '" + navn + "%'";
                }
                else
                {
                    tempSSQL += " Navn LIKE '" + navn + "%'";
                }
            }

            if (telefon != "Telefon" && telefon != "")
            {
                if (tempSSQL.Contains("ID") || tempSSQL.Contains("Navn"))
                {
                    tempSSQL += " AND Telefon LIKE '" + telefon + "%'";
                }
                else
                {
                    tempSSQL += " Telefon LIKE '" + telefon + "%'";
                }

            }

            if (email != "Email" && email != "")
            {
                if (tempSSQL.Contains("ID") || tempSSQL.Contains("Navn") || tempSSQL.Contains("Telefon"))
                {
                    tempSSQL += " AND Email LIKE '%" + email + "%'";
                }
                else
                {
                    tempSSQL += " Email LIKE '%" + email + "%'";
                }
            }

            string sSQL = tempSSQL + ";";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Mægler");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;

        }
    }
}
