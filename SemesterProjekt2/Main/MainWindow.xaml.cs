using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Input;
using System.Text.RegularExpressions;

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
            instance.Title = "SweetHome - Salgsstatistik";
        }

        private void ButtonKvmPris_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Visible;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            instance.Title = "SweetHome - Kvm. Pris";
        }

        private void ButtonPrisBeregner_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Visible;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            instance.Title = "SweetHome - Pris Beregner";
        }

        private void ButtonCRUD_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Visible;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Hidden;
            WrapPanelHusejer.Visibility = Visibility.Visible;
            DataGridHusejer.Visibility = Visibility.Visible;
            ControllerCrudHusejer.LæsHusejer(DataGridHusejer);
            instance.Title = "SweetHome - CRUD";
        }

        private void ButtonÅbentHus_Click(object sender, RoutedEventArgs e)
        {
            CanvasSalgstatistik.Visibility = System.Windows.Visibility.Hidden;
            CanvasKvmPris.Visibility = System.Windows.Visibility.Hidden;
            CanvasPrisBeregner.Visibility = System.Windows.Visibility.Hidden;
            CanvasCRUD.Visibility = System.Windows.Visibility.Hidden;
            CanvasÅbentHus.Visibility = System.Windows.Visibility.Visible;
            instance.Title = "SweetHome - Åbent Hus";
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

                if(isClosed == true)
                {
                    udfil = sfd.FileName;
                }

                ControllerSalgsstatistik.Udskriv(DataGridSalgsstatistik, startDato, slutDato, udfil);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox4.Text = Convert.ToString(ControllerPrisBeregner.BeregnPris(Convert.ToInt32(textbox1.Text), textbox2.Text, Convert.ToInt32(textbox3.Text)));
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

        private void ButtonHusejer_Click(object sender, RoutedEventArgs e)
        {
            ControllerCrudHusejer.LæsHusejer(DataGridHusejer);
        }

        private void ButtonMægler_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEjendom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonOpret_Click(object sender, RoutedEventArgs e)
        {
            if (WrapPanelHusejer.IsVisible && TextBoxHusejerNavn.Text != "Navn" && TextBoxHusejerEmail.Text != "Email" && TextBoxHusejerTelefon.Text != "Telefon" 
                && TextBoxHusejerNavn.Text != "" && TextBoxHusejerEmail.Text != "" && TextBoxHusejerTelefon.Text != "") 
            {
                ControllerCrudHusejer.OpretHusejer(TextBoxHusejerNavn.Text, TextBoxHusejerEmail.Text, TextBoxHusejerTelefon.Text);

                ControllerCrudHusejer.LæsHusejer(DataGridHusejer);

                TextBoxHusejerID.Text = "ID (Autogenereres)";
                TextBoxHusejerNavn.Text = "Navn";
                TextBoxHusejerEmail.Text = "Email";
                TextBoxHusejerTelefon.Text = "Telefon";
            }
        }

        private void ButtonSøg_Click(object sender, RoutedEventArgs e)
        {
            if (WrapPanelHusejer.IsVisible && (TextBoxHusejerID.Text != "ID (Autogenereres)" && TextBoxHusejerID.Text != "") || 
                (TextBoxHusejerNavn.Text != "Navn" && TextBoxHusejerNavn.Text != "") || (TextBoxHusejerEmail.Text != "Email" && TextBoxHusejerEmail.Text != "") || 
                (TextBoxHusejerTelefon.Text != "Telefon" && TextBoxHusejerTelefon.Text != ""))
            {
                ControllerCrudHusejer.SøgHusejer(TextBoxHusejerID.Text, TextBoxHusejerNavn.Text, TextBoxHusejerEmail.Text, TextBoxHusejerTelefon.Text, DataGridHusejer);
            }
        }

        private void ButtonOpdater_Click(object sender, RoutedEventArgs e)
        {
            if (WrapPanelHusejer.IsVisible && TextBoxHusejerID.Text != "ID (Autogenereres)" && TextBoxHusejerID.Text != "" && (TextBoxHusejerNavn.Text != "Navn" && TextBoxHusejerNavn.Text != "") || (TextBoxHusejerEmail.Text != "Email" && TextBoxHusejerEmail.Text != "") ||
                (TextBoxHusejerTelefon.Text != "Telefon" && TextBoxHusejerTelefon.Text != ""))
            {
                ControllerCrudHusejer.OpdaterHusejer(Convert.ToInt32(TextBoxHusejerID.Text), TextBoxHusejerNavn.Text, TextBoxHusejerEmail.Text, TextBoxHusejerTelefon.Text);

                ControllerCrudHusejer.LæsHusejer(DataGridHusejer);

                TextBoxHusejerID.Text = "ID (Autogenereres)";
                TextBoxHusejerNavn.Text = "Navn";
                TextBoxHusejerEmail.Text = "Email";
                TextBoxHusejerTelefon.Text = "Telefon";
            }
        }

        private void ButtonSlet_Click(object sender, RoutedEventArgs e)
        {
            ControllerCrudHusejer.SletHusEjer(Convert.ToInt32(TextBoxHusejerID.Text));

            ControllerCrudHusejer.LæsHusejer(DataGridHusejer);

            TextBoxHusejerID.Text = "ID (Autogenereres)";
            TextBoxHusejerNavn.Text = "Navn";
            TextBoxHusejerEmail.Text = "Email";
            TextBoxHusejerTelefon.Text = "Telefon";
        }

        private void TextBoxHusejerID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxHusejerID.Text == "")
            {
                TextBoxHusejerID.Text = "ID (Autogenereres)";
            }

        }

        private void TextBoxHusejerNavn_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxHusejerNavn.Text == "")
            {
                TextBoxHusejerNavn.Text = "Navn";
            }
        }

        private void TextBoxHusejerEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxHusejerEmail.Text == "")
            {
                TextBoxHusejerEmail.Text = "Email";
            }
        }

        private void TextBoxHusejerTelefon_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxHusejerTelefon.Text == "")
            {
                TextBoxHusejerTelefon.Text = "Telefon";
            }
        }

        private void TextBoxHusejerID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxHusejerTelefon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxMæglerID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxMæglerID.Text == "")
            {
                TextBoxMæglerID.Text = "ID (Autogenereres)";
            }
        }

        private void TextBoxMæglerNavn_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxMæglerNavn.Text == "")
            {
                TextBoxMæglerNavn.Text = "Navn";
            }
        }

        private void TextBoxMæglerTelefon_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxMæglerTelefon.Text == "")
            {
                TextBoxMæglerTelefon.Text = "Telefon";
            }
        }

        private void TextBoxMæglerEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxMæglerEmail.Text == "")
            {
                TextBoxMæglerEmail.Text = "Email";
            }
        }

        private void TextBoxMæglerID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxMæglerTelefon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void DataGridEjendom_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridEjendom.Items.Add(new object());
        }
    }
}
