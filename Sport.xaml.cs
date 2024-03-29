﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace MatchManiaWPF
{
    /// <summary>
    /// TODO:   Implementera autoladding till dropdownmenyerna från .json-fil, 
    ///         experimentera med och implementera sökfunktionen med dynamisk uppdatering för varje nytt tecken.
    ///         Se till att OK-knapp lirar med alla funktioner och undermenyer.
    /// </summary>
    public partial class Sport : Window
    {

        public Sport()
        {
            InitializeComponent();
            LandLista();
        }

        private void LandLista()
        {
            // Läser in objekt "länder" somm innehåller en lista med tillgängliga Countries. Sök i listan genom t.ex.:
            /*              string nameSearch = "Sverige";
                            foreach (Country c in länder.response)
                                if (c.name == nameQuery)
                                    DoStuff();
             */
            Land.Rootobject länder = Land.SearchCountries();
            List<string> names = new(Land.CountryNames(länder));
            names.Insert(0, "Välj Land");
            LandVal.ItemsSource = names;
        }

        private void TillbakaKlick(object sender, RoutedEventArgs e)
        {
            //Skapar ny startsida och stänger denna undermeny
            MainWindow start = new MainWindow();
            this.Close();
            start.Show();
        }
        private void OkKlick(object sender, RoutedEventArgs e)
        {
            /// TODO:    Implementera if-loopar för alla alternativ som kommer laddas in från dropdown-menyer,
            ///          skapa anslutningar till för nu icke skapade menyer samt få sökfunktionen att lira med OK-knappen.
            ///          Glöm inte att logga gjorda val mot App.xaml.cs filen för att få rätt data i undermenyerna.
            ComboBoxItem valdLiga = (ComboBoxItem)Liga.SelectedItem;
            //ComboBoxItem valtLand = (ComboBoxItem)LandVal.SelectedItem;

            if (Liga.SelectedIndex != 0)
            {
                string textLiga = valdLiga.Content.ToString();
                Hem hem = new Hem();

                App.Liga = valdLiga.ToString();

                if (textLiga == "UEFA Europa League 2022") 
                {
                    this.Close();
                    hem.Show();
                }
                else if (textLiga != null)
                {
                    MessageBox.Show($"Välj Liga: Vi har tyvärr inte lanserat de valda sidorna ännu, \nhåll ögonen öppna efter kommande uppdatering.");
                }
            }
            if (LandVal.SelectedIndex != 0)
            {
                MessageBox.Show($"Välj Land: Vi har tyvärr inte lanserat de valda sidorna ännu, \nhåll ögonen öppna efter kommande uppdatering.");
            }
        }

        private void LandVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LandVal.SelectedIndex!=0)
                Liga.SelectedIndex = 0;
        }

        private void Liga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Liga.SelectedIndex!=0)
                LandVal.SelectedIndex = 0;
        }
    }
}
