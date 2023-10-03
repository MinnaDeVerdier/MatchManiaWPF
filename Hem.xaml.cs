using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for Hem.xaml
    /// </summary>
    public partial class Hem : Window
    {
        public Hem()
        {
            InitializeComponent();
        }
        private void KommandeClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Vi har tyvärr inte lanserat sidorna ännu, \nhåll ögonen öppna efter kommande uppdatering.");
        }
        private void MatchKnappKlick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Vi har tyvärr inte lanserat sidorna ännu, \nhåll ögonen öppna efter kommande uppdatering.");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /// Skapa en string-lista från json-filen och eventuellt med en <ItemsControl> skapar vi en UI för att visa matchinformationen. 
            /// Logotyper hämtas troligtvis via http från någon databas och dessa visas som <Image Source="lagx" Height="" Width=""/> tillsammans med <TextBlock/>
            /// Detta kan räcka för att få till en dräglig lösning för att visa kommande matcher.
        }
        private void TillbakaKlick(object sender, RoutedEventArgs e)
        {
            //Skapar ny startsida och stänger denna undermeny
            Fotboll fotboll = new Fotboll();
            this.Close();
            fotboll.Show();
        }
    }
}
