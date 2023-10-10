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
            Main.Navigate(new SpeladeMatchsida(lag1, lag2, logo1, logo2, mål1, mål2));
        }

        /// Resultat = score, statistik osv.
        /// Kommande = Odds, arena, väder(?!) dommare osv.
    }
}
