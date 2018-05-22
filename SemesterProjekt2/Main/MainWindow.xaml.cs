﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;

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
    }
}
