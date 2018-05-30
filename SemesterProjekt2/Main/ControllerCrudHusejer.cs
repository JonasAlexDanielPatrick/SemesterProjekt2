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

        public static void OpretHusejer(string navn, string email, string telefon) // virker
        {
            string sSQL = "INSERT INTO Husejer VALUES ('" + navn + "', '" + email + "', '" + telefon + "');";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
        }

        public static void OpdaterHusejer(int id, string navn, string email, string telefon) // virker
        {
            string tempSSQL = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
                              "BEGIN TRANSACTION;" +
                              "UPDATE Husejer SET";

            if (navn != "Navn" && navn != "")
            {
                tempSSQL += " Navn = '" + navn + "'";
            }

            if(email != "Email" && email != "")
            {
                if (tempSSQL.Contains("Navn"))
                {
                    tempSSQL += ", Email = '" + email + "'";
                }
                else
                {
                    tempSSQL += " Email = '" + email + "'";
                }
            }

            if (telefon != "Telefon" && telefon != "")
            {
                if (tempSSQL.Contains("Email") || tempSSQL.Contains("Navn"))
                {
                    tempSSQL += ", Telefon = '" + telefon + "'";
                }
                else
                {
                    tempSSQL += " Telefon = '" + telefon + "'";
                }
                
            }

            tempSSQL += "Where ID = '" + id + "' COMMIT TRANSACTION;";

            string sSQL = tempSSQL;
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
           
        }

        public static void SletHusEjer(int id)  // virker
        {
            string sSQL = "DELETE FROM Husejer WHERE ID = '" + id + "';";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
        }

        public static void SøgHusejer(string id, string navn, string email, string telefon, DataGrid dg) 
        {
            string tempSSQL = "SELECT * FROM Husejer WHERE";

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

            if (email != "Email" && email != "")
            {
                if (tempSSQL.Contains("ID") || tempSSQL.Contains("Navn"))
                {
                    tempSSQL += " AND Email LIKE '%" + email + "%'";
                }
                else
                {
                    tempSSQL += " Email LIKE '%" + email + "%'";
                }
            }

            if (telefon != "Telefon" && telefon != "")
            {
                if (tempSSQL.Contains("ID") || tempSSQL.Contains("Navn") || tempSSQL.Contains("Email"))
                {
                    tempSSQL += " AND Telefon LIKE '" + telefon + "%'";
                }
                else
                {
                    tempSSQL += " Telefon LIKE '" + telefon + "%'";
                }

            }

            string sSQL = tempSSQL;
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Husejer");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;

        }
    }
}
