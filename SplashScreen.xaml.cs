using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading;

namespace MatchManiaWPF
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    /// 
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += SplashScreen_Loaded;
        }

        private async void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRandomQuote();
        }

        private async Task LoadRandomQuote()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://matchilling-chuck-norris-jokes-v1.p.rapidapi.com/jokes/random"),
                Headers =
            {
                { "accept", "application/json" },
                { "X-RapidAPI-Key", "b04b67f72amshebe35a6a5b3c6abp1612c1jsna12c7c8ebc95" },
                { "X-RapidAPI-Host", "matchilling-chuck-norris-jokes-v1.p.rapidapi.com" },
            },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var document = JObject.Parse(body);
                var value = document.GetValue("value").ToString();

                ChuckNorrisFact.Text = value;
            }
        }

        private void MatchManiaButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close(); // Stäng SplashScreen när knappen klickas
        }
    }

}