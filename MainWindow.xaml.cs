using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchManiaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SportVal.SelectedIndex = 0;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem valdSport = (ComboBoxItem)SportVal.SelectedItem;

            if (valdSport != null)
            {
                string textSport = valdSport.Content.ToString();
                Sport fotboll = new Sport();

                // Lagrar vald sport i applikationskoden för att påverka innehållet i
                // "Fotboll.xaml" som med detta skulle kunna döpas om till "Sport" istället.
                App.ValdSport = textSport;
 
                if (textSport == "Fotboll")
                {
                    this.Close();
                    // Läser in objekt "länder" somm innehåller en lista med tillgängliga Countries. Sök i listan genom t.ex.:
                    /*              string nameSearch = "Sverige";
                                    foreach (Country c in länder.response)
                                        if (c.name == nameQuery)
                                            DoStuff();
                     */
                    Land.Rootobject länder = Land.SearchCountries();
                    List<string> names = new(Land.CountryNames(länder));
                    fotboll.LandVal.ItemsSource = names;
                    fotboll.Show();
                }

                // Hämta texten från vald ComboBoxItem och visa det i en MessageBox
                else
                {
                    MessageBox.Show($"Vi har tyvärr inte lanserat sidorna för {textSport} ännu, \nhåll ögonen öppna efter kommande uppdatering.");
                }
            }
            else
            {
                MessageBox.Show("Vänligen välj en sport i listan.");
            }
        }

        private void SportVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
