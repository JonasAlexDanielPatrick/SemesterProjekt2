using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using Models;
using System.Windows.Input;

namespace Main
{

    public partial class MainWindow : Window
    {
        public static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();

            instance = this;

            ControllerConnection.Connect();

            Thread threadLondonUr = new Thread(new ThreadStart(ControllerUr.London));
            Thread threadKøbenhavnUr = new Thread(new ThreadStart(ControllerUr.København));

            threadLondonUr.Start();
            threadKøbenhavnUr.Start();

            ButtonSalgsstatistik_Click(null, null);

        }

        internal string LondonUr
        {
            get { return LabelLondon.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { LabelLondon.Content = value; })); }
        }


        internal string KøbenhavnUr
        {
            get { return LabelKøbenhavn.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { LabelKøbenhavn.Content = value; })); }
        }

        private void ButtonSalgsstatistik_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Visible;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            CanvasTestData.Visibility = Visibility.Hidden;
            instance.Title = "SweetHome - Salgsstatistik";
        }

        private void ButtonKvmPris_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Visible;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            CanvasTestData.Visibility = Visibility.Hidden;
            instance.Title = "SweetHome - Kvm. Pris";
        }

        private void ButtonPrisBeregner_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Visible;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            CanvasTestData.Visibility = Visibility.Hidden;
            instance.Title = "SweetHome - Pris Beregner";
        }

        private void ButtonCRUD_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Visible;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            CanvasTestData.Visibility = Visibility.Hidden;
            instance.Title = "SweetHome - CRUD";
        }

        private void ButtonÅbentHus_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Visible;
            CanvasTestData.Visibility = Visibility.Hidden;
            ControllerÅbentHus.FyldEjendomDatagrid(DataGridÅbentHusEjendom);
            ControllerÅbentHus.FyldMæglerDatagrid(DataGridÅbentHusMægler);
            instance.Title = "SweetHome - Åbent Hus";
        }

        private void LabelKøbenhavn_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            CanvasTestData.Visibility = Visibility.Visible;
            instance.Title = "SweetHome - Test Data";
        }

        private void ButtonSalgsstatistikSøg_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerSalgsstatistikStartdato.ToString() != "" && DatePickerSalgsstatistikSlutdato.ToString() != "")
            {
                string tempStartDato = DatePickerSalgsstatistikStartdato.SelectedDate.ToString().Substring(0, 10);
                string startDato = tempStartDato.Substring(6, 4) + "-" + tempStartDato.Substring(3, 2) + "-" + tempStartDato.Substring(0, 2);

                string tempSlutDato = DatePickerSalgsstatistikSlutdato.SelectedDate.ToString().Substring(0, 10);
                string slutDato = tempSlutDato.Substring(6, 4) + "-" + tempSlutDato.Substring(3, 2) + "-" + tempSlutDato.Substring(0, 2);

                ControllerSalgsstatistik.Vis(DataGridSalgsstatistik, startDato, slutDato);
            }
        }

        private void ButtonSalgsstatistikUdskriv_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerSalgsstatistikStartdato.ToString() != "" && DatePickerSalgsstatistikSlutdato.ToString() != "")
            {
                string udfil = "";

                string tempStartDato = DatePickerSalgsstatistikStartdato.SelectedDate.ToString().Substring(0, 10);
                string startDato = tempStartDato.Substring(6, 4) + "-" + tempStartDato.Substring(3, 2) + "-" + tempStartDato.Substring(0, 2);

                string tempSlutDato = DatePickerSalgsstatistikSlutdato.SelectedDate.ToString().Substring(0, 10);
                string slutDato = tempSlutDato.Substring(6, 4) + "-" + tempSlutDato.Substring(3, 2) + "-" + tempSlutDato.Substring(0, 2);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Salgsstatistik";
                sfd.DefaultExt = ".txt";
                sfd.Filter = "Text documents (.txt)|*.txt";

                bool? isClosed = sfd.ShowDialog();

                if (isClosed == true)
                {
                    udfil = sfd.FileName;
                }

                ControllerSalgsstatistik.Udskriv(DataGridSalgsstatistik, startDato, slutDato, udfil);
            }
        }

        private void ButtonPrisBeregner_BeregnPrisClick(object sender, RoutedEventArgs e)
        {
            try
            {
                textboxPrisBeregner_Vurdering.Text = Convert.ToString(ControllerPrisBeregner.BeregnPris(Convert.ToInt32(comboboxPrisBeregner_Postnr.Text), comboboxPrisBeregner_Navn.Text, Convert.ToInt32(textbox_PrisBeregnerKVM.Text)));
            }
            catch (Exception)
            {
                Debug.WriteLine("Tager ikke imod string!");
                textbox_PrisBeregnerKVM.Clear();
            }

           
        }

        private void ButtonKvmPrisSøg_Click(object sender, RoutedEventArgs e)
        {
            string startDato = ((ComboBoxItem)ComboboxKvmPriserÅr.SelectedItem).Tag.ToString() + "-" + ((ComboBoxItem)ComboboxKvmPriserMåned.SelectedItem).Tag.ToString() + "-01";

            string slutDato = ((ComboBoxItem)ComboboxKvmPriserÅr.SelectedItem).Tag.ToString() + "-" + ((ComboBoxItem)ComboboxKvmPriserMåned.SelectedItem).Tag.ToString() + "-31";

            ControllerKvmPris.Vis(DataGridKvmPriser, startDato, slutDato);

        }

        private void ButtonKvmPrisUdskriv_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxKvmPriserÅr.ToString() != "" && ComboboxKvmPriserMåned.ToString() != "")
            {
                string udfil = "";

                string startDato = ((ComboBoxItem)ComboboxKvmPriserÅr.SelectedItem).Tag.ToString() + "-" + ((ComboBoxItem)ComboboxKvmPriserMåned.SelectedItem).Tag.ToString() + "-01";

                string slutDato = ((ComboBoxItem)ComboboxKvmPriserÅr.SelectedItem).Tag.ToString() + "-" + ((ComboBoxItem)ComboboxKvmPriserMåned.SelectedItem).Tag.ToString() + "-31";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "KvmPris";
                sfd.DefaultExt = ".txt";
                sfd.Filter = "Text documents (.txt)|*.txt";

                bool? isClosed = sfd.ShowDialog();

                if (isClosed == true)
                {
                    udfil = sfd.FileName;
                }

                ControllerKvmPris.Udskriv(DataGridKvmPriser, startDato, slutDato, udfil);
            }
        }

        private void ButtonÅbentHusUdskriv_Click(object sender, RoutedEventArgs e)
        {
            //ControllerÅbentHus.GenererListe(DataGridÅbentHusMægler, "");

            foreach (ModelÅbentHusEjendom ejendom in ControllerÅbentHus.ejendomme)
            {
                if (ejendom.IsChecked)
                {
                    Debug.WriteLine(ejendom.Sagsnr + " is true!");
                }
            }

        }

        private void ButtonOpretData_Click(object sender, RoutedEventArgs e)
        {
            ControllerTestData.OpretData();
        }

        void ComboBox_PrisBeregner_Postnummer_Open(object sender, EventArgs e)
        {
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Postnr FROM Område;", ControllerConnection.conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int sPostnr = reader.GetInt32(0);
                    bool hasItem = comboboxPrisBeregner_Postnr.Items.Contains(sPostnr);

                    if (hasItem)
                    {
                        Debug.WriteLine("Postnummer findes allerede!");
                    }
                    else
                    {
                        comboboxPrisBeregner_Postnr.Items.Add(sPostnr);
                    }
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine("Could not find database/table" + "\n");
            }
            finally
            {
                reader.Close();
            }
        }

        private void ComboBox_PrisBeregner_Postnummer_Close(object sender, EventArgs e)
        {
            comboboxPrisBeregner_Navn.Items.Clear();
            SqlCommand cmd = new SqlCommand("SELECT Navn FROM Område WHERE Postnr = " + comboboxPrisBeregner_Postnr.Text + ";", ControllerConnection.conn);
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    string sName = reader.GetString(reader.GetOrdinal("Navn"));
                    comboboxPrisBeregner_Navn.Items.Add(sName);

                }
                reader.Close();
            }
            catch (Exception x)
            {
                Debug.WriteLine("Could not find database/table - CLOSE");
            }
        }

        private void CheckBoxÅbentHusEjendom_Click(object sender, RoutedEventArgs e)
        {
            ModelÅbentHusEjendom ejendom = (ModelÅbentHusEjendom)DataGridÅbentHusEjendom.SelectedItem;

            if (ejendom.IsChecked == false)
            {
                ejendom.IsChecked = false;
            }
            else
            {
                ejendom.IsChecked = true;
            }
        }

        private void CheckBoxÅbentHusMægler_Click(object sender, RoutedEventArgs e)
        {
            ModelÅbentHusMægler mægler = (ModelÅbentHusMægler)DataGridÅbentHusMægler.SelectedItem;

            if (mægler.IsChecked == false)
            {
                mægler.IsChecked = false;
            }
            else
            {
                mægler.IsChecked = true;
            }
        }
    }
}
