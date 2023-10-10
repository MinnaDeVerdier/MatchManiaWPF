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
        public static void SearchCountries(Rootobject länder)
        {
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
        }
    }

}