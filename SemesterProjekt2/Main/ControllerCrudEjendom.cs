using System;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data;
using System.Linq;

namespace Main
{
    class ControllerCrudEjendom
    {
        public static void AddItem(ComboBox comboBoxEjendomGarageCarport)
        {
            comboBoxEjendomGarageCarport.Items.Add("True");
            comboBoxEjendomGarageCarport.Items.Add("False");
            comboBoxEjendomGarageCarport.Items.Add("Ligegyldig");
        }
        public static void LæsEjendom(DataGrid dg) //
        {
            string sSQL = "SELECT * FROM Ejendom";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Ejendom");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }

        public static void OpretEjendom(string mæglerID, string husejerID, string områdeNavn, string postnr, string energiMærke, 
                                        string startDato, string salgsDato, string adresse, string startPris, string nuværendePris, string grundAreal,  
                                        string kælderAreal, string boligAreal, string byggeår, string garageCarport) 
        {
            string tempSSQLOne = "INSERT INTO Mægler (";
            string tempSSQLTwo = ") VALUES (";

            tempSSQLOne += "MæglerID";
            tempSSQLTwo += mæglerID;
            tempSSQLOne += ", HusejerID";
            tempSSQLTwo += ", " + husejerID;

            if(områdeNavn != "Områdenavn" && områdeNavn != "")
            {
                tempSSQLOne += ", OmrådeNavn";
                tempSSQLTwo += ", " + områdeNavn;
            }

            if (postnr != "Postnr" && postnr != "")
            {
                tempSSQLOne += ", Postnr";
                tempSSQLTwo += ", " + postnr;
            }

            if (energiMærke != "Energimærke" && energiMærke != "")
            {
                tempSSQLOne += ", EnergiMærke";
                tempSSQLTwo += ", " + energiMærke;
            }

            if (startDato != "Startdato" && startDato != "")
            {
                tempSSQLOne += ", StartDato";
                tempSSQLTwo += ", " + startDato;
            }

            if (salgsDato != "Salgsdato" && salgsDato != "")
            {
                tempSSQLOne += ", SalgsDato";
                tempSSQLTwo += ", " + salgsDato;
            }

            if (adresse != "Adresse" && adresse != "")
            {
                tempSSQLOne += ", Adresse";
                tempSSQLTwo += ", " + adresse;
            }

            if (startPris != "Startpris" && startPris != "")
            {
                tempSSQLOne += ", StartPris";
                tempSSQLTwo += ", " + startPris;
            }

            if (nuværendePris != "Nuværende pris" && nuværendePris != "")
            {
                tempSSQLOne += ", NuværendePris";
                tempSSQLTwo += ", " + nuværendePris;
            }

            if (grundAreal != "Grundareal" && grundAreal != "")
            {
                tempSSQLOne += ", GrundAreal";
                tempSSQLTwo += ", " + grundAreal;
            }

            if (kælderAreal != "Kælderareal" && kælderAreal != "")
            {
                tempSSQLOne += ", KælderAreal";
                tempSSQLTwo += ", " + kælderAreal;
            }

            if (boligAreal != "Boligareal" && boligAreal != "")
            {
                tempSSQLOne += ", BoligAreal";
                tempSSQLTwo += ", " + boligAreal;
            }

            if (byggeår != "Byggeår" && byggeår != "")
            {
                tempSSQLOne += ", Byggeår";
                tempSSQLTwo += ", " + byggeår;
            }

            if (garageCarport != "Garage/carport" && garageCarport != "")
            {
                tempSSQLOne += ", GarageCarport";
                tempSSQLTwo += ", " + garageCarport;
            }

            string sSQL = tempSSQLOne + tempSSQLTwo + ");";
              
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
        }

        public static void OpdaterEjendom(string id, string mæglerID, string husejerID, string områdeNavn, string postnr, string energiMærke,
                                          string startDato, string salgsDato, string adresse, string startPris, string nuværendePris, string grundAreal,
                                          string kælderAreal, string boligAreal, string byggeår, string garageCarport) // 
        {
            string[] CheckIfContains = { "MæglerID", "HusejerID", "OmrådeNavn", "Postnr", "EnergiMærke", "StartDato",
                                         "SalgsDato", "Adresse", "StartPris", "NuværendePris", "GrundAreal", "KælderAreal",
                                         "BoligAreal","Byggeår", "GarageCarport" };

            string tempSSQL = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
                              "BEGIN TRANSACTION;" +
                              "UPDATE Ejendom SET";

            if (mæglerID != "Mægler ID (Påkrævet)" && mæglerID != "")
            {
                tempSSQL += " MæglerID = '" + mæglerID + "'";
            }

            if (husejerID != "Husejer ID (Påkrævet)" && husejerID != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", HusejerID = '" + husejerID + "'";
                }
                else
                {
                    tempSSQL += " HusejerID = '" + husejerID + "'";
                }
            }

            if (områdeNavn != "Områdenavn" && områdeNavn != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", OmrådeNavn = '" + områdeNavn + "'";
                }
                else
                {
                    tempSSQL += " OmrådeNavn = '" + områdeNavn + "'";
                }
            }

            if (postnr != "Postnr" && postnr != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", Postnr = '" + postnr + "'";
                }
                else
                {
                    tempSSQL += " Postnr = '" + postnr + "'";
                }
            }

            if (energiMærke != "Energimærke" && energiMærke != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", EnergiMærke = '" + energiMærke + "'";
                }
                else
                {
                    tempSSQL += " EnergiMærke = '" + energiMærke + "'";
                }
            }

            if (startDato != "Startdato" && startDato != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", StartDato = '" + startDato + "'";
                }
                else
                {
                    tempSSQL += " StartDato = '" + startDato + "'";
                }
            }

            if (salgsDato != "Salgsdato" && salgsDato != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", SalgsDato = '" + salgsDato + "'";
                }
                else
                {
                    tempSSQL += " SalgsDato = '" + salgsDato + "'";
                }
            }

            if (adresse != "Adresse" && adresse != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", Adresse = '" + adresse + "'";
                }
                else
                {
                    tempSSQL += " Adresse = '" + adresse + "'";
                }
            }

            if (startPris != "Startpris" && startPris != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", StartPris = '" + startPris + "'";
                }
                else
                {
                    tempSSQL += " StartPris = '" + startPris + "'";
                }
            }

            if (nuværendePris != "Nuværende pris" && nuværendePris != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", NuværendePris = '" + nuværendePris + "'";
                }
                else
                {
                    tempSSQL += " NuværendePris = '" + nuværendePris + "'";
                }
            }

            if (grundAreal != "Grundareal" && grundAreal != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", GrundAreal = '" + grundAreal + "'";
                }
                else
                {
                    tempSSQL += " GrundAreal = '" + grundAreal + "'";
                }
            }

            if (kælderAreal != "Kælderareal" && kælderAreal != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", KælderAreal = '" + kælderAreal + "'";
                }
                else
                {
                    tempSSQL += " KælderAreal = '" + kælderAreal + "'";
                }
            }

            if (boligAreal != "Boligareal" && boligAreal != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", BoligAreal = '" + boligAreal + "'";
                }
                else
                {
                    tempSSQL += " BoligAreal = '" + boligAreal + "'";
                }
            }

            if (byggeår != "Byggeår" && byggeår != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", Byggeår = '" + byggeår + "'";
                }
                else
                {
                    tempSSQL += " Byggeår = '" + byggeår + "'";
                }
            }

            if (garageCarport != "Garage/carport" && garageCarport != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += ", GarageCarport = '" + garageCarport + "'";
                }
                else
                {
                    tempSSQL += " GarageCarport = '" + garageCarport + "'";
                }
            }
            
            tempSSQL += "Where ID = '" + id + "' COMMIT TRANSACTION;";

            string sSQL = tempSSQL;
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();

        }

        public static void SletEjendom(int id)  //
        {
            string sSQL = "DELETE FROM Ejendom WHERE ID = '" + id + "';";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            command.ExecuteNonQuery();
        }

        public static void SøgEjendom(string sagsnr, string mæglerID, string husejerID, string områdeNavn, string postnr, string energiMærke,
                                          string startDato, string salgsDato, string adresse, string startPris, string nuværendePris, string grundAreal,
                                          string kælderAreal, string boligAreal, string byggeår, string garageCarport, DataGrid dg)
        {
            string[] CheckIfContains = { "MæglerID", "HusejerID", "OmrådeNavn", "Postnr", "EnergiMærke", "StartDato",
                                         "SalgsDato", "Adresse", "StartPris", "NuværendePris", "GrundAreal", "KælderAreal",
                                         "BoligAreal","Byggeår", "GarageCarport" };

            string tempSSQL = "SELECT * FROM Ejendom WHERE";

            if (sagsnr != "Sagsnr (Autogenereres)" && sagsnr != "")
            {
                tempSSQL += " Sagsnr = '" + sagsnr + "'";
            }

            if (mæglerID != "Mægler ID (Påkrævet)" && mæglerID != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND MæglerID LIKE '" + mæglerID + "'";
                }
                else
                {
                    tempSSQL += " MæglerID LIKE '" + mæglerID + "'";
                }
            }

            if (husejerID != "Husejer ID (Påkrævet)" && husejerID != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND HusejerID LIKE '" + husejerID + "'";
                }
                else
                {
                    tempSSQL += " HusejerID LIKE '" + husejerID + "'";
                }
            }

            if (områdeNavn != "Områdenavn" && områdeNavn != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND OmrådeNavn LIKE '" + områdeNavn + "'";
                }
                else
                {
                    tempSSQL += " OmrådeNavn LIKE '" + områdeNavn + "'";
                }
            }

            if (postnr != "Postnr" && postnr != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND Postnr LIKE '" + postnr + "'";
                }
                else
                {
                    tempSSQL += " Postnr LIKE '" + postnr + "'";
                }
            }

            if (energiMærke != "Energimærke" && energiMærke != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND EnergiMærke LIKE '" + energiMærke + "'";
                }
                else
                {
                    tempSSQL += " EnergiMærke LIKE '" + energiMærke + "'";
                }
            }

            if (startDato != "Startdato" && startDato != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND StartDato LIKE '" + startDato + "'";
                }
                else
                {
                    tempSSQL += " StartDato LIKE '" + startDato + "'";
                }
            }

            if (salgsDato != "Salgsdato" && salgsDato != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND SalgsDato LIKE '" + salgsDato + "'";
                }
                else
                {
                    tempSSQL += " SalgsDato LIKE '" + salgsDato + "'";
                }
            }

            if (adresse != "Adresse" && adresse != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND Adresse LIKE '" + adresse + "'";
                }
                else
                {
                    tempSSQL += " Adresse LIKE '" + adresse + "'";
                }
            }

            if (startPris != "Startpris" && startPris != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND StartPris LIKE '" + startPris + "'";
                }
                else
                {
                    tempSSQL += " StartPris LIKE '" + startPris + "'";
                }
            }

            if (nuværendePris != "Nuværende pris" && nuværendePris != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND NuværendePris LIKE '" + nuværendePris + "'";
                }
                else
                {
                    tempSSQL += " NuværendePris LIKE '" + nuværendePris + "'";
                }
            }

            if (grundAreal != "Grundareal" && grundAreal != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND GrundAreal LIKE '" + grundAreal + "'";
                }
                else
                {
                    tempSSQL += " Grundareal LIKE '" + grundAreal + "'";
                }
            }

            if (kælderAreal != "Kælderareal" && kælderAreal != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND KælderAreal LIKE '" + kælderAreal + "'";
                }
                else
                {
                    tempSSQL += " KælderAreal LIKE '" + kælderAreal + "'";
                }
            }

            if (boligAreal != "Boligareal)" && boligAreal != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND BoligAreal LIKE '" + boligAreal + "'";
                }
                else
                {
                    tempSSQL += " BoligAreal LIKE '" + boligAreal + "'";
                }
            }

            if (byggeår != "Byggeår" && byggeår != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND Byggeår LIKE '" + byggeår + "'";
                }
                else
                {
                    tempSSQL += " Byggeår LIKE '" + byggeår + "'";
                }
            }

            if (garageCarport != "Garage/carport" && garageCarport != "")
            {
                if ((CheckIfContains.Any(tempSSQL.Contains)))
                {
                    tempSSQL += " AND GarageCarport LIKE '" + garageCarport + "'";
                }
                else
                {
                    tempSSQL += " GarageCarport LIKE '" + garageCarport + "'";
                }
            }

            string sSQL = tempSSQL + ";";
            SqlCommand command = new SqlCommand(sSQL, ControllerConnection.conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Ejendom");
            sda.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
        }
    }
}
