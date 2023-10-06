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
using System.Globalization;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

namespace MatchManiaWPF
{
    /// <summary>
    /// Interaction logic for Hem.xaml
    /// </summary>
    public partial class Hem : Window
    {
        public List<Match> Matches { get; set; }
        public List<Match> FirstFiveMatches { get; set; }

        public Hem()
        {
            InitializeComponent();
            LoadMatches();
            DataContext = this;
            LoadRssFeed();
        }
        private async void LoadRssFeed()
        {
            string feedUrl = "https://www.eyefootball.com/football_news.xml";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string rssContent = await httpClient.GetStringAsync(feedUrl);
                    XDocument rssFeed = XDocument.Parse(rssContent);
                    List<RssItem> items = rssFeed.Descendants("item").Select(item => 
                        new RssItem
                        {
                            Title = item.Element("title")?.Value,
                            Link = item.Element("link")?.Value,
                            Description = item.Element("description")?.Value,
                            PublishDate = DateTime.TryParse(item.Element("pubDate")?.Value, out DateTime date) ? date : DateTime.MinValue
                        }).ToList();
                    NewsListBox.ItemsSource = items.Take(15);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadMatches()
        {
            string dir = @"..\..\..\";
            string fileName = "jsonTestResponse.json";
            string path = System.IO.Path.Combine(dir, fileName);
            //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
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
                }).ToList();

                // Hämta de fem första matcherna
                FirstFiveMatches = Matches.Take(10).ToList();
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

            NewsListBox.Visibility = Visibility.Collapsed;
        }
        private void NyheterKlick(object sender, RoutedEventArgs e)
        {
            NewsListBox.Visibility = Visibility.Visible;
        }
        private void ResultatKlick(object sender, RoutedEventArgs e)
        {
            NewsListBox.Visibility = Visibility.Collapsed;
            ResultatItemsControl.ItemsSource = FirstFiveMatches;
        }
        private void TillbakaKlick(object sender, RoutedEventArgs e)
        {
            //Skapar ny startsida och stänger denna undermeny
            Sport fotboll = new Sport();
            this.Close();
            fotboll.Show();
        }
        private void KalenderKlick(object sender, RoutedEventArgs e)
        {
            if (kalender.Visibility == Visibility.Collapsed)
            {
                kalender.Visibility = Visibility.Visible;
            }
            else
            {
                kalender.Visibility = Visibility.Collapsed;
            }

            NewsListBox.Visibility = Visibility.Collapsed;
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
                if (value is List<Match> matches)
                {
                    var sortedMatches = matches.OrderBy(match => match.Datum).ToList();
                    var nextMatch = sortedMatches.FirstOrDefault(match => match.Datum > DateTime.Now);
                    return nextMatch;
                }
                return null;
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        public class RootObject
        {
            public string get { get; set; }
            public Parameters parameters { get; set; }
            public List<object> errors { get; set; }
            public int results { get; set; }
            public List<ResponseItem> response { get; set; }
        }

        public class Parameters
        {
            public string league { get; set; }
            public string last { get; set; }
            public string season { get; set; }
        }

        public class ResponseItem
        {
            public Fixture fixture { get; set; }
            public League league { get; set; }
            public Teams teams { get; set; }
            public Goals goals { get; set; }
        }

        public class Fixture
        {
            public int id { get; set; }
            public string referee { get; set; }
            public string timezone { get; set; }
            public DateTime date { get; set; }
            public int timestamp { get; set; }
            public Venue venue { get; set; }
        }

        public class Venue
        {
            public int id { get; set; }
            public string name { get; set; }
            public string city { get; set; }
        }

        public class League
        {
            public int id { get; set; }
            public string name { get; set; }
            public string country { get; set; }
            public string logo { get; set; }
            public string round { get; set; }
        }

        public class Teams
        {
            public Team home { get; set; }
            public Team away { get; set; }
        }

        public class Team
        {
            public int id { get; set; }
            public string name { get; set; }
            public string logo { get; set; }
            public bool winner { get; set; }
        }

        public class Goals
        {
            public int home { get; set; }
            public int away { get; set; }
        }

        public class RssItem
        {
            public string Title { get; set; }
            public string Link { get; set; }
            public string Description { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}