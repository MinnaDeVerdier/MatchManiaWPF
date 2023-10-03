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

namespace MatchManiaWPF
{
    /// <summary>
    /// TODO:   Implementera autoladding till dropdownmenyerna från .json-fil, 
    ///         experimentera med och implementera sökfunktionen med dynamisk uppdatering för varje nytt tecken.
    ///         Se till att OK-knapp lirar med alla funktioner och undermenyer.
    /// </summary>
    public partial class Fotboll : Window
    {
        public Fotboll()
        {
            InitializeComponent();
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
            App.Liga = valdLiga.ToString();

            Hem hem = new Hem();
            this.Close();
            hem.Show();
        }
    }
}
