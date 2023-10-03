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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchManiaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem valdSport = (ComboBoxItem)SportVal.SelectedItem;

            if (valdSport != null)
            {
                string textSport = valdSport.Content.ToString();
                Fotboll fotboll = new Fotboll();

                // Lagrar vald sport i applikationskoden för att påverka innehållet i
                // "Fotboll.xaml" som med detta skulle kunna döpas om till "Sport" istället.
                App.ValdSport = textSport;

                if (textSport == "Fotboll")
                {
                    this.Close();
                    fotboll.Show();
                }

                // Hämta texten från vald ComboBoxItem och visa det i en MessageBox
                else
                {
                    MessageBox.Show($"Vi har tyvärr inte lanserat sidorna för {textSport} ännu, \nhåll ögonen öppna efter kommande uppdatering.");
                }
            }
            else
            {
                MessageBox.Show("Vänligen välj en sport i listan.");
            }
        }
    }
}
