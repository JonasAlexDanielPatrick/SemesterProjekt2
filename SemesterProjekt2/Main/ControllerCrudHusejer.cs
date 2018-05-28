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
        //public static List<ModelHusejer> Husejer;

        public static void HusejerSqlDataReader()
        {

            //string sSQL = "SELECT * FROM Husejer";
            //SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            //SqlDataAdapter sda = new SqlDataAdapter(command);
            //SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    Husejer.Add(new ModelHusejer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                   
            //}
            //UpdateDG();
        }
        
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

            
        }
    }
}
