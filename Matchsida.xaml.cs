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
    /// Interaction logic for Matchsida.xaml
    /// </summary>
    public partial class Matchsida : Window
    {
        public Matchsida(string lag1, string lag2, string logo1, string logo2, int mål1, int mål2)
        {
            InitializeComponent();

            Lag1Namn.Text = lag1;
            Lag2Namn.Text = lag2;
            BitmapImage logoHemma = new BitmapImage(new Uri(logo1, UriKind.RelativeOrAbsolute));
            Lag1Logo.Source = logoHemma;
            BitmapImage logoBorta = new BitmapImage(new Uri(logo2, UriKind.RelativeOrAbsolute));
            Lag2Logo.Source = logoBorta;
            Lag1Score.Text = mål1.ToString();
            Lag2Score.Text = mål2.ToString();
            
        }

    /// Kod som tar tidigare vald match och visar mer information om matchen,
    /// kärndata från det förra fönstret måste hit och visas på ett snyggt sätt i
    /// ett eget fönster.
    /// Resultat = statistik osv.
    /// Kommande = Odds, arena, väder(?!) dommare osv.
    }
}
