﻿using System;
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

        //
        // CRUD
        //
        private void ButtonHusejer_Click(object sender, RoutedEventArgs e)
        {
            
            WrapPanelMægler.Visibility = Visibility.Hidden;
            DataGridMægler.Visibility = Visibility.Hidden;
            WrapPanelEjendom.Visibility = Visibility.Hidden;
            DataGridEjendom.Visibility = Visibility.Hidden;
            WrapPanelHusejer.Visibility = Visibility.Visible;
            DataGridHusejer.Visibility = Visibility.Visible;
            ControllerCrudHusejer.LæsHusejer(DataGridHusejer);
        }

        private void ButtonMægler_Click(object sender, RoutedEventArgs e)
        {
            
            WrapPanelEjendom.Visibility = Visibility.Hidden;
            DataGridEjendom.Visibility = Visibility.Hidden;
            WrapPanelHusejer.Visibility = Visibility.Hidden;
            DataGridHusejer.Visibility = Visibility.Hidden;
            WrapPanelMægler.Visibility = Visibility.Visible;
            DataGridMægler.Visibility = Visibility.Visible;
            ControllerCrudMægler.LæsMægler(DataGridMægler);
        }

        private void ButtonEjendom_Click(object sender, RoutedEventArgs e)
        {
            
            WrapPanelHusejer.Visibility = Visibility.Hidden;
            DataGridHusejer.Visibility = Visibility.Hidden;
            WrapPanelMægler.Visibility = Visibility.Hidden;
            DataGridMægler.Visibility = Visibility.Hidden;
            WrapPanelEjendom.Visibility = Visibility.Visible;
            DataGridEjendom.Visibility = Visibility.Visible;
        }

        private void ButtonOpret_Click(object sender, RoutedEventArgs e)
        {
            if (WrapPanelHusejer.IsVisible && TextBoxHusejerNavn.Text != "Navn" && TextBoxHusejerNavn.Text != "" && 
                TextBoxHusejerEmail.Text != "Email" && TextBoxHusejerEmail.Text != "" && TextBoxHusejerTelefon.Text != "Telefon" && 
                TextBoxHusejerTelefon.Text != "") 
            {
                ControllerCrudHusejer.OpretHusejer(TextBoxHusejerNavn.Text, TextBoxHusejerEmail.Text, TextBoxHusejerTelefon.Text);

                ControllerCrudHusejer.LæsHusejer(DataGridHusejer);

                TextBoxHusejerID.Text = "ID (Autogenereres)";
                TextBoxHusejerNavn.Text = "Navn";
                TextBoxHusejerEmail.Text = "Email";
                TextBoxHusejerTelefon.Text = "Telefon";
            }
            if (WrapPanelMægler.IsVisible && TextBoxMæglerNavn.Text != "Navn" && TextBoxMæglerNavn.Text != "" &&
                TextBoxMæglerTelefon.Text != "Telefon" && TextBoxMæglerTelefon.Text != "" && TextBoxMæglerEmail.Text != "Email" &&
                TextBoxMæglerEmail.Text != "")
            {
                ControllerCrudMægler.OpretMægler(TextBoxMæglerNavn.Text, TextBoxMæglerTelefon.Text, TextBoxMæglerEmail.Text);

                ControllerCrudMægler.LæsMægler(DataGridMægler);

                TextBoxMæglerID.Text = "ID (Autogenereres)";
                TextBoxMæglerNavn.Text = "Navn";
                TextBoxMæglerTelefon.Text = "Telefon";
                TextBoxMæglerEmail.Text = "Email";
                
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
            if(WrapPanelMægler.IsVisible && (TextBoxMæglerID.Text != "ID (Autogenereres)" && TextBoxMæglerID.Text != "") ||
                (TextBoxMæglerNavn.Text != "Navn" && TextBoxMæglerNavn.Text != "") || (TextBoxMæglerTelefon.Text != "Telefon" && TextBoxMæglerTelefon.Text != "") || 
                (TextBoxMæglerEmail.Text != "Email" && TextBoxMæglerEmail.Text != ""))
            {
                //ControllerCrudMægler.SøgMægler
            }
        }

        private void ButtonOpdater_Click(object sender, RoutedEventArgs e)
        {
            if (WrapPanelHusejer.IsVisible && TextBoxHusejerID.Text != "ID (Autogenereres)" && TextBoxHusejerID.Text != "")
            {
                ControllerCrudHusejer.OpdaterHusejer(Convert.ToInt32(TextBoxHusejerID.Text), TextBoxHusejerNavn.Text, TextBoxHusejerEmail.Text, TextBoxHusejerTelefon.Text);

                ControllerCrudHusejer.LæsHusejer(DataGridHusejer);

                TextBoxHusejerID.Text = "ID (Autogenereres)";
                TextBoxHusejerNavn.Text = "Navn";
                TextBoxHusejerEmail.Text = "Email";
                TextBoxHusejerTelefon.Text = "Telefon";
            }
            if (WrapPanelMægler.IsVisible && TextBoxMæglerID.Text != "ID (Autogenereres)" && TextBoxMæglerID.Text != "")
            {
                ControllerCrudMægler.OpdaterMægler(Convert.ToInt32(TextBoxMæglerID.Text), TextBoxMæglerNavn.Text, TextBoxMæglerTelefon.Text, TextBoxMæglerEmail.Text);

                ControllerCrudMægler.LæsMægler(DataGridMægler);

                TextBoxMæglerID.Text = "ID (Autogenereres)";
                TextBoxMæglerNavn.Text = "Navn";
                TextBoxMæglerTelefon.Text = "Telefon";
                TextBoxMæglerEmail.Text = "Email";
            }
        }

        private void ButtonSlet_Click(object sender, RoutedEventArgs e)
        {
            if(WrapPanelHusejer.IsVisible && TextBoxHusejerID.Text != "ID(Autogenereres)" && TextBoxHusejerID.Text != "")
            {
                ControllerCrudHusejer.SletHusEjer(Convert.ToInt32(TextBoxHusejerID.Text));

                ControllerCrudHusejer.LæsHusejer(DataGridHusejer);

                TextBoxHusejerID.Text = "ID (Autogenereres)";
                TextBoxHusejerNavn.Text = "Navn";
                TextBoxHusejerEmail.Text = "Email";
                TextBoxHusejerTelefon.Text = "Telefon";
            }
            if (WrapPanelMægler.IsVisible && TextBoxMæglerID.Text != "ID(Autogenereres)" && TextBoxMæglerID.Text != "")
            {
                ControllerCrudMægler.SletMægler(Convert.ToInt32(TextBoxHusejerID.Text));

                ControllerCrudMægler.LæsMægler(DataGridMægler);

                TextBoxMæglerID.Text = "ID (Autogenereres)";
                TextBoxMæglerNavn.Text = "Navn";
                TextBoxMæglerTelefon.Text = "Telefon";
                TextBoxMæglerEmail.Text = "Email";
            }
            
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

        //Hjælp til at kunne se den horisontale scroll bar i datagrid'et, ved at fylde objekter ind i kolonnerne
        //private void DataGridEjendom_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DataGridEjendom.Items.Add(new object());
        //}

        private void TextBoxEjendomSagsnr_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomSagsnr.Text == "")
            {
                TextBoxEjendomSagsnr.Text = "Sagsnr";
            }
        }

        private void TextBoxEjendomMæglerID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomMæglerID.Text == "")
            {
                TextBoxEjendomMæglerID.Text = "Mægler ID";
            }
        }

        private void TextBoxEjendomHusejerID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomHusejerID.Text == "")
            {
                TextBoxEjendomHusejerID.Text = "Husejer ID";
            }
        }

        private void TextBoxEjendomOmrådeNavn_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomOmrådeNavn.Text == "")
            {
                TextBoxEjendomOmrådeNavn.Text = "Områdenavn";
            }
        }

        private void TextBoxEjendomPostnr_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomPostnr.Text == "")
            {
                TextBoxEjendomPostnr.Text = "Postnr";
            }
        }

        private void TextBoxEjendomEnergiMærke_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomEnergiMærke.Text == "")
            {
                TextBoxEjendomEnergiMærke.Text = "Energimærke";
            }
        }

        private void TextBoxEjendomStartDato_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomStartDato.Text == "")
            {
                TextBoxEjendomStartDato.Text = "Startdato";
            }
        }

        private void TextBoxEjendomSalgsDato_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomSalgsDato.Text == "")
            {
                TextBoxEjendomSalgsDato.Text = "Salgsdato";
            }
        }

        private void TextBoxEjendomAdresse_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomAdresse.Text == "")
            {
                TextBoxEjendomAdresse.Text = "Adresse";
            }
        }

        private void TextBoxEjendomStartPris_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomStartPris.Text == "")
            {
                TextBoxEjendomStartPris.Text = "Startpris";
            }
        }

        private void TextBoxEjendomNuværendePris_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomNuværendePris.Text == "")
            {
                TextBoxEjendomNuværendePris.Text = "Nuværendepris";
            }
        }

        private void TextBoxEjendomGrundAreal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomGrundAreal.Text == "")
            {
                TextBoxEjendomGrundAreal.Text = "Grundareal";
            }
        }

        private void TextBoxEjendomKælderAreal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomKælderAreal.Text == "")
            {
                TextBoxEjendomKælderAreal.Text = "Kælderareal";
            }
        }

        private void TextBoxEjendomBoligAreal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomBoligAreal.Text == "")
            {
                TextBoxEjendomBoligAreal.Text = "Boligareal";
            }
        }

        private void TextBoxEjendomByggeår_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomByggeår.Text == "")
            {
                TextBoxEjendomByggeår.Text = "Byggeår";
            }
        }

        private void TextBoxEjendomGarageCarport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEjendomGarageCarport.Text == "")
            {
                TextBoxEjendomGarageCarport.Text = "Garage/carport";
            }
        }

        private void TextBoxEjendomSagsnr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomMæglerID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomHusejerID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomPostnr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomStartPris_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomNuværendePris_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomGrundAreal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomKælderAreal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomBoligAreal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBoxEjendomByggeår_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
