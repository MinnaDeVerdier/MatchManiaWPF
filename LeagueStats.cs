using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatchManiaWPF
{
    public class LeagueStatClass
    {
        // Arrays med Rank 1-4 inom varje grupp
        public static GroupStanding[] groupA;
        public static GroupStanding[] groupB;
        public static GroupStanding[] groupC;
        public static GroupStanding[] groupD;
        public static GroupStanding[] groupE;
        public static GroupStanding[] groupF;
        public static GroupStanding[] groupG;
        public static GroupStanding[] groupH;
        public static void LoadStats()
        {
            JsonSerializerSettings nullIgnore = new() { NullValueHandling = NullValueHandling.Ignore };
            string dir = @"..\..\..\";
            string filePath = "dataLeague3Season2022Statistics.json";
            string path = System.IO.Path.Combine(dir, filePath);
            try
            {
                string jsonText = System.IO.File.ReadAllText(path);
                RootobjectStandings statistik = JsonConvert.DeserializeObject<RootobjectStandings>(jsonText, nullIgnore);
                if (statistik == null)
                {
                    throw new NullReferenceException();
                }
                League leagueInfo = statistik.response[0].league;
                groupA = leagueInfo.standings[0];
                groupB = leagueInfo.standings[1];
                groupC = leagueInfo.standings[2];
                groupD = leagueInfo.standings[3];
                groupE = leagueInfo.standings[4];
                groupF = leagueInfo.standings[5];
                groupG = leagueInfo.standings[6];
                groupH = leagueInfo.standings[7];

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("json-objekt null");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
    public class LeagueInfo  // Obsolet?
    {
        public string Namn { get; set; }
        public string År { get; set; }
        public string Start { get; set; }
        public string Slut { get; set; }
        public string Aktuell { get; set; }
    }
    public class LeagueSeasonStatistics  // Obsolet?
    {
        public object[] errors { get; set; }
        public int results { get; set; }
        public Response[] response { get; set; }
    }
    public class Response  // Obsolet?
    {
        public League league { get; set; }
        public Country country { get; set; }
        public Season[] seasons { get; set; }
    }
    public class Country  // Obsolet?
    {
        public string name { get; set; }
        public string code { get; set; }
        public string flag { get; set; }
    }
    public class Season  // Obsolet?
    {
        public int year { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool current { get; set; }
        public Coverage coverage { get; set; }
    }
    public class Coverage // Obsolet?
    {
        public Fixtures fixtures { get; set; }
        public bool standings { get; set; }
        public bool players { get; set; }
        public bool top_scorers { get; set; }
        public bool top_assists { get; set; }
        public bool top_cards { get; set; }
        public bool injuries { get; set; }
        public bool predictions { get; set; }
        public bool odds { get; set; }
    }
    public class Fixtures  // Obsolet?
    {
        public bool events { get; set; }
        public bool lineups { get; set; }
        public bool statistics_fixtures { get; set; }
        public bool statistics_players { get; set; }
    }

    // Statistik för Liga under en säsong
    public class RootobjectStandings
    {
        public object[]? errors { get; set; }
        public int results { get; set; }
        // Får bara svar med en league: response[0]
        public LeagueStandings[] response { get; set; }
    }
    // Använd LeagueStandings.league direkt för att nå statistik för liga 3
    public class LeagueStandings
    {
        public League league { get; set; }
    }

    public class League
    {
        public string? type { get; set; } //endast i LeagueStatistics.response
        public int? id { get; set; }
        public string? name { get; set; }
        public string? country { get; set; } //använd t.ex. foreach(x in LeagueStatistics.response) if this == x.country.name, för att hämta country.flag
        public string? logo { get; set; }
        public string? flag { get; set; }
        public int? season { get; set; } //använd t.ex. foreach(x in LeagueStatistics.response[i].seasons) if this == x.year, för att hämta start+slutdatum+current
        
        // GroupStanding[0-7][0-3]: Array 0-7 motsvarar Grupprank A-H, nästa array 0-3 motsvarar Rank 1-4 inom varje grupp
        public GroupStanding[][]? standings { get; set; }
    }
    public class GroupStanding
    {
        // rank 1-4
        public int rank { get; set; }
        public Team team { get; set; }
        public int points { get; set; }
        public int goalsDiff { get; set; }
        // grupp A-H
        public string group { get; set; }
        public string form { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        // stats för lagets spelade matcher
        public All all { get; set; }
        public Home home { get; set; }
        public Away away { get; set; }
        public DateTime update { get; set; }
    }

    public class Team
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }
    // stats för alla lagets matcher i ligaspelet
    public class All
    {
        public int played { get; set; }
        public int win { get; set; }
        public int draw { get; set; }
        public int lose { get; set; }
        public Goals goals { get; set; }
    }

    // Stats för hemmamatcher
    public class Home
    {
        public int played { get; set; }
        public int win { get; set; }
        public int draw { get; set; }
        public int lose { get; set; }
        public Goals goals { get; set; }
    }
    // Stats för bortamatcher
    public class Away
    {
        public int played { get; set; }
        public int win { get; set; }
        public int draw { get; set; }
        public int lose { get; set; }
        public Goals goals { get; set; }
    }
    public class Goals
    {
        // Gjorda mål
        public int _for { get; set; }
        // Insläppta mål
        public int against { get; set; }
    }
}