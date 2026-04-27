using LibrarieModele;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InchirieriMasini.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Creăm un client
            Client c = new Client("Popescu", "Ana", "1234567890123");

            // Creăm o mașină conform structurii tale reale
            Masina m = new Masina("Dacia", "Logan", 2018, "SV-12-ABC");

            // Creăm o închiriere
            Inchiriere inch = new Inchiriere(c, m, DateTime.Now, DateTime.Now.AddDays(3));

            // Afișăm în UI
            lblClient.Content = $"Client: {c.Nume} {c.Prenume}";
            lblMasina.Content = $"Mașină: {m.Marca} {m.Model}, {m.AnFabricatie}, {m.NumarInmatriculare}";
            lblPerioada.Content = $"Perioadă: {inch.DataStart.ToShortDateString()} - {inch.DataFinal.ToShortDateString()}";
        }
    }
}