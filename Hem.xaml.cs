using Newtonsoft.Json;
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
using System.IO;

namespace MatchManiaWPF
{
    /// <summary>
    /// Interaction logic for Hem.xaml
    /// </summary>
    public partial class Hem : Window
    {
        public List<Match> Matches { get; set; }
        public Hem()
        {
            InitializeComponent();
            LoadMatches();
            DataContext = this;
        }
        private void LoadMatches()
        {
            try
            {
                string path = "jsonTestResponse.json";
                string jsonText = File.ReadAllText(path);
                var json = JsonConvert.DeserializeObject<RootObject>(jsonText);
                Matches = json.response.Select(matchData => new Match
                    {
                        Lag1Name = matchData.teams.home.name,
                        Lag2Name = matchData.teams.away.name,
                        Lag1Score = matchData.goals.home,
                        Lag2Score = matchData.goals.away,
                        Lag1Logo = matchData.teams.home.logo,
                        Lag2Logo = matchData.teams.away.logo,
                        Datum = DateTime.Parse(matchData.fixture.date)
                    }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        public class Match
        {
            public string Lag1Name { get; set; }
            public string Lag2Name { get; set; }
            public int Lag1Score { get; set; }
            public int Lag2Score { get; set; }
            public string Lag1Logo { get; set; }
            public string Lag2Logo { get; set; }
            public DateTime Datum { get; set; }
        }
        public class NextMatchConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if(value is List<Match> matches)
                {
                    var sortedMatches = matches.OrderBy(match => match.Datum).ToList();
                    var nextMatch = sortedMatches.FirstOrDefault(match => match.Datum > DateTime.Now);
                }
            }
        }
    }
}
