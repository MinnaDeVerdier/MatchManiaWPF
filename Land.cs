using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace MatchManiaWPF
{
    public class Land
    {
        public class Rootobject
        {
            public object[] errors { get; set; }
            public Country[] response { get; set; }
            public int results { get; set; }
        }
        public class Country
        {
            public string code { get; set; }
            public string flag { get; set; }
            public string name { get; set; }
        }
        public static Rootobject SearchCountries()
        {
            Rootobject länder = new();
            string dir = @"..\..\..\";
            string fileName = "CountriesResponseAll.json";
            string path = System.IO.Path.Combine(dir, fileName);
            JsonSerializerSettings nullIgnore = new() { NullValueHandling = NullValueHandling.Ignore };
            try
            {
                string jsonText = File.ReadAllText(path);
                länder = JsonConvert.DeserializeObject<Rootobject>(jsonText, nullIgnore);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (länder != null)
            {
                //MessageBox.Show("succe objekt länder");
                return länder;
            }
            else
            {
                MessageBox.Show("fel objekt länder");
                return länder;
            }
        }
        public static List<string> CountryNames(Rootobject länder)
        {
            List<string> länderNamn = new();
            foreach (Country c in länder.response)
                länderNamn.Add(c.name);
            return länderNamn;
        }
    }

}