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
using System.Diagnostics;
using Microsoft.VisualBasic;
using static System.Net.WebRequestMethods;

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

        // Menyknappar:
        private void KommandeKlick(object sender, RoutedEventArgs e)
        {
            CollapseAllContent();
            Start.Visibility = Visibility.Visible;
            MessageBox.Show($"Vi har tyvärr inte lanserat sidorna ännu, \nhåll ögonen öppna efter kommande uppdatering.");
        }
        private void ResultatKlick(object sender, RoutedEventArgs e)
        {
            CollapseAllContent();
            ResultatItemsControl.ItemsSource = FirstFiveMatches;
            Resultat.Visibility = Visibility.Visible;
        }
        private void StatistikKlick(object sender, RoutedEventArgs e)
        {
            CollapseAllContent();
            Statistik.Visibility = Visibility.Visible;
            try
            {
                string filväg = "LeaguesApi.json";
                LeagueStatistics statistik = LoadLeagueStatistics(filväg);

                if (statistik != null)
                {
                    var lastLeague = statistik.response.Last();
                    LeagueInfo leagueName = new LeagueInfo
                    {
                        Namn = $"Liga: {lastLeague.league.name}",
                        År = $"År: {lastLeague.seasons.Last().year}",
                        Start = $"Startade: {lastLeague.seasons.Last().start}",
                        Slut = $"Slutade: {lastLeague.seasons.Last().end}"
                    };

                    StatistikItemsControl.ItemsSource = new List<LeagueInfo> { leagueName };

                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KalenderKlick(object sender, RoutedEventArgs e)
        {
            CollapseAllContent();
            KalenderDatePicker.Visibility = Visibility.Visible;
        }
        private void NyheterKlick(object sender, RoutedEventArgs e)
        {
            CollapseAllContent();
            Nyheter.Visibility = Visibility.Visible;
        }
        private void TillbakaKlick(object sender, RoutedEventArgs e)
        {
            // Skapar ny startsida och stänger denna undermeny
            Sport fotboll = new Sport();
            this.Close();
            fotboll.Show();
        }

        // Hjälp-metoder, övrigt
        private void CollapseAllContent()
        {
            Start.Visibility = Visibility.Collapsed;
            Resultat.Visibility = Visibility.Collapsed;
            Nyheter.Visibility = Visibility.Collapsed;
            KalenderDatePicker.Visibility = Visibility.Collapsed;
            Statistik.Visibility = Visibility.Collapsed;
        }
        
        private void LoadMatches()
        {
            string dir = @"..\..\..\";
            string fileName = "League3Season2022Response10.json";
            string path = System.IO.Path.Combine(dir, fileName);
            //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                string jsonText = System.IO.File.ReadAllText(path);
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

                FirstFiveMatches = Matches.Take(10).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    items = items.OrderByDescending(item => item.PublishDate).ToList();
                    Nyheter.ItemsSource = items.Take(15);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NyhetWeb(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string link = button.Tag as string;
                if (!string.IsNullOrEmpty(link))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo { FileName = link, UseShellExecute = true });
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
        private void MatchKnappKlick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var matchData = (Match)button.DataContext; 
            Matchsida match = new Matchsida(matchData.Lag1Name, matchData.Lag2Name, matchData.Lag1Logo, matchData.Lag2Logo, matchData.Lag1Score, matchData.Lag2Score);
            match.Show();
        }
        private LeagueStatistics LoadLeagueStatistics(string fileName)
        {
            JsonSerializerSettings nullIgnore = new() { NullValueHandling = NullValueHandling.Ignore };
            string dir = @"..\..\..\";
            string filePath = "LeaguesApi.json";
            string path = System.IO.Path.Combine(dir, filePath);
            try
            {

                string jsonText = System.IO.File.ReadAllText(path);
                LeagueStatistics statistik = JsonConvert.DeserializeObject<LeagueStatistics>(jsonText, nullIgnore);
                if (statistik != null) 
                {
                    return statistik; 
                }
                else
                {
                    throw new NullReferenceException();
                }
                
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("fel");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            return null;
        }

        // Klasser för att hantera Liga-data från API-anrop
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