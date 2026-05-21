using LibrarieModele;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Text.RegularExpressions;


namespace InchirieriMasini.WPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public ObservableCollection<Masina> Masini { get; set; }

        private Masina masinaCurenta;
        public Masina MasinaCurenta
        {
            get => masinaCurenta;
            set
            {
                masinaCurenta = value;
                OnPropertyChanged(nameof(MasinaCurenta));
            }
        }


        private const int MAX_LUNGIME = 15;

        // lista care se afișează în DataGrid
        private ObservableCollection<Client> listaClienti = new ObservableCollection<Client>();

        public MainWindow()
        {
            InitializeComponent();
            IncarcaClientiDinFisier();


            // conectăm DataGrid-ul la listă
            DataGridClienti.ItemsSource = listaClienti;
            CmbSelecteazaClient.ItemsSource = listaClienti;

            

            this.SizeChanged += MainWindow_SizeChanged;
            Masini = new ObservableCollection<Masina>();
            MasinaCurenta = new Masina();
            IncarcaMasiniDinFisier();
            DataContext = this;




        }

        private void IncarcaClientiDinFisier()
        {
            string path = "clienti.txt";

            if (!File.Exists(path))
                return;

            var linii = File.ReadAllLines(path);

            foreach (var linie in linii)
            {
                var parts = linie.Split(';');
                if (parts.Length == 6)
                {
                    var c = new Client(
                        parts[0],               // Nume
                        parts[1],               // Prenume
                        parts[2],               // CNP
                        DateTime.Parse(parts[3])// Data nașterii
                    );

                    c.Gen = parts[4];
                    c.Abonat = parts[5];

                    listaClienti.Add(c);
                }
            }
        }

        private void SalveazaClientiInFisier()
        {
            string path = "clienti.txt";

            List<string> linii = new List<string>();

            foreach (var c in listaClienti)
            {
                linii.Add($"{c.Nume};{c.Prenume};{c.CNP};{c.DataNasterii:yyyy-MM-dd};{c.Gen};{c.Abonat}");
            }

            File.WriteAllLines(path, linii);
        }




        // ============================
        // VALIDARE + ADAUGARE CLIENT
        // ============================

        private bool ValideazaDateClient()
        {
            bool ok = true;

            // reset erori
            ErrNume.Visibility = Visibility.Collapsed;
            ErrPrenume.Visibility = Visibility.Collapsed;
            ErrCNP.Visibility = Visibility.Collapsed;

            LblNume.Foreground = new SolidColorBrush(Color.FromRgb(27, 79, 114));
            LblPrenume.Foreground = new SolidColorBrush(Color.FromRgb(27, 79, 114));
            LblCNP.Foreground = new SolidColorBrush(Color.FromRgb(27, 79, 114));

            // NUME
            if (string.IsNullOrWhiteSpace(TxtNume.Text))
            {
                ErrNume.Text = "Numele este obligatoriu.";
                ErrNume.Visibility = Visibility.Visible;
                LblNume.Foreground = Brushes.Red;
                ok = false;
            }
            else if (TxtNume.Text.Length > MAX_LUNGIME)
            {
                ErrNume.Text = "Maxim 15 caractere.";
                ErrNume.Visibility = Visibility.Visible;
                LblNume.Foreground = Brushes.Red;
                ok = false;
            }

            // PRENUME
            if (string.IsNullOrWhiteSpace(TxtPrenume.Text))
            {
                ErrPrenume.Text = "Prenumele este obligatoriu.";
                ErrPrenume.Visibility = Visibility.Visible;
                LblPrenume.Foreground = Brushes.Red;
                ok = false;
            }
            else if (TxtPrenume.Text.Length > MAX_LUNGIME)
            {
                ErrPrenume.Text = "Maxim 15 caractere.";
                ErrPrenume.Visibility = Visibility.Visible;
                LblPrenume.Foreground = Brushes.Red;
                ok = false;
            }

            // CNP
            if (string.IsNullOrWhiteSpace(TxtCNP.Text))
            {
                ErrCNP.Text = "CNP obligatoriu.";
                ErrCNP.Visibility = Visibility.Visible;
                LblCNP.Foreground = Brushes.Red;
                ok = false;
            }
            else if (TxtCNP.Text.Length != 13)
            {
                ErrCNP.Text = "CNP trebuie să aibă 13 cifre.";
                ErrCNP.Visibility = Visibility.Visible;
                LblCNP.Foreground = Brushes.Red;
                ok = false;
            }

            return ok;
        }

        private void OnAddClient(object sender, RoutedEventArgs e)
        {
            if (!ValideazaDateClient())
                return;

            string gen = RbFeminin.IsChecked == true ? "Feminin" : "Masculin";
            string abonat = CbNewsletter.IsChecked == true ? "Da" : "Nu";

            DateTime dataNasterii = DtpDataNasterii.SelectedDate ?? DateTime.Today;

            Client c = new Client(
                TxtNume.Text,
                TxtPrenume.Text,
                TxtCNP.Text,
                dataNasterii





            );
            c.Gen = gen;
            c.Abonat = abonat;
            

            // adăugăm în tabel
            listaClienti.Add(c);
            SalveazaClientiInFisier();

            MessageBox.Show("Client adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

            OnResetClient(null, null);
        }

        private void OnResetClient(object sender, RoutedEventArgs e)
        {
            // Reset text
            TxtNume.Text = "";
            TxtPrenume.Text = "";
            TxtCNP.Text = "";

            // Reset DatePicker
            DtpDataNasterii.SelectedDate = null;

            // Reset gen
            RbFeminin.IsChecked = false;
            RbMasculin.IsChecked = false;

            // Reset newsletter
            CbNewsletter.IsChecked = false;

            // Reset erori
            ErrNume.Visibility = Visibility.Collapsed;
            ErrPrenume.Visibility = Visibility.Collapsed;
            ErrCNP.Visibility = Visibility.Collapsed;

            // Reset culori label
            LblNume.Foreground = new SolidColorBrush(Color.FromRgb(27, 79, 114));
            LblPrenume.Foreground = new SolidColorBrush(Color.FromRgb(27, 79, 114));
            LblCNP.Foreground = new SolidColorBrush(Color.FromRgb(27, 79, 114));
        }



        private void OnExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void FocusAdd(object sender, RoutedEventArgs e)
        {
            TxtNume.Focus();
        }

        private void OnCautaClient(object sender, RoutedEventArgs e)
        {
            string text = TxtCauta.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(text))
            {
                DataGridClienti.ItemsSource = listaClienti;
                return;
            }

            var filtrat = listaClienti
                .Where(c =>
                    c.Nume.ToLower().Contains(text) ||
                    c.Prenume.ToLower().Contains(text) ||
                    c.CNP.ToLower().Contains(text) ||
                    c.Gen.ToLower().Contains(text) ||
                    c.Abonat.ToLower().Contains(text)
                )
                .ToList();

            DataGridClienti.ItemsSource = filtrat;
        }


        private void OnResetCautare(object sender, RoutedEventArgs e)
        {
            TxtCauta.Text = "";
            DataGridClienti.ItemsSource = listaClienti;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth < 900)
            {
                // Fereastră mică → formularul și tabelul se pun unul sub altul
                Grid.SetColumn(ScrollViewerFormular, 0);
                Grid.SetRow(ScrollViewerFormular, 0);

                Grid.SetColumn(StackPanelTabel, 0);
                Grid.SetRow(StackPanelTabel, 1);
            }
            else
            {
                // Fereastră mare → formularul și tabelul sunt unul lângă altul
                Grid.SetColumn(ScrollViewerFormular, 0);
                Grid.SetRow(ScrollViewerFormular, 0);

                Grid.SetColumn(StackPanelTabel, 1);
                Grid.SetRow(StackPanelTabel, 0);
            }
        }


        private void OnMenuAddClient(object sender, RoutedEventArgs e)
        {
            TxtNume.Focus();
        }

        private void OnMenuSearchClient(object sender, RoutedEventArgs e)
        {
            TxtCauta.Focus();
        }

        private void OnAbout(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Aplicație realizată de Pojoga Dumitrita-Daniela\nProiect PIU – Inchirieri AUTO\n2026",
                "Despre aplicație",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
        /// lab 9- selectare client din ComboBox și afișare detalii în formular

        private void CmbSelecteazaClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbSelecteazaClient.SelectedItem is Client c)
            {
                // Umple formularul cu datele clientului selectat
                TxtNume.Text = c.Nume;
                TxtPrenume.Text = c.Prenume;
                TxtCNP.Text = c.CNP;
                DtpDataNasterii.SelectedDate = c.DataNasterii;

                if (c.Gen == "Feminin") RbFeminin.IsChecked = true;
                else RbMasculin.IsChecked = true;

                CbNewsletter.IsChecked = c.Abonat == "Da";
            }
        }

        private void LstFiltruGen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstFiltruGen.SelectedItem is ListBoxItem item)
            {
                string genSelectat = item.Content.ToString();

                if (genSelectat == "Toți")
                {
                    DataGridClienti.ItemsSource = listaClienti;
                }
                else
                {
                    DataGridClienti.ItemsSource = listaClienti
                        .Where(c => c.Gen == genSelectat)
                        .ToList();
                }
            }
        }

        private void OnModificaClient(object sender, RoutedEventArgs e)
        {
            if (DataGridClienti.SelectedItem is Client c)
            {
                TxtNume.Text = c.Nume;
                TxtPrenume.Text = c.Prenume;
                TxtCNP.Text = c.CNP;
                DtpDataNasterii.SelectedDate = c.DataNasterii;

                // gen
                if (c.Gen == "Feminin") RbFeminin.IsChecked = true;
                else RbMasculin.IsChecked = true;

                // abonare
                CbNewsletter.IsChecked = c.Abonat=="Da";
            }
        }
        private void OnActualizeazaClient(object sender, RoutedEventArgs e)
        {
            if (DataGridClienti.SelectedItem is Client c)
            {
                c.Nume = TxtNume.Text;
                c.Prenume = TxtPrenume.Text;
                c.CNP = TxtCNP.Text;
                c.DataNasterii = DtpDataNasterii.SelectedDate ?? DateTime.Today;
                c.Gen = RbFeminin.IsChecked == true ? "Feminin" : "Masculin";
                c.Abonat = CbNewsletter.IsChecked == true ? "Da" : "Nu";

                SalveazaClientiInFisier();

                // actualizare DataGrid
                DataGridClienti.Items.Refresh();
            }
        }

        private void BtnAdaugaMasina_Click(object sender, RoutedEventArgs e)
        {
            if (!ValideazaDateMasina())
                return;

            Masini.Add(new Masina
            {
                Marca = MasinaCurenta.Marca,
                Model = MasinaCurenta.Model,
                AnFabricatie = MasinaCurenta.AnFabricatie,
                NumarInmatriculare = MasinaCurenta.NumarInmatriculare,
                Disponibila = MasinaCurenta.Disponibila
            });

           
            MessageBox.Show("Mașină adăugată cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

            MasinaCurenta = new Masina();
        }

        private void BtnModificaMasina_Click(object sender, RoutedEventArgs e)
        {
            // Binding TwoWay actualizează automat obiectul selectat
            if (DataGridMasini.SelectedItem is Masina m)
                MasinaCurenta = m;
        }

        private void BtnActualizeazaMasina_Click(object sender, RoutedEventArgs e)
        {
            if (!ValideazaDateMasina())
                return;

            // Binding TwoWay a modificat deja obiectul selectat
            DataGridMasini.Items.Refresh();

            // Resetăm formularul după actualizare
            MasinaCurenta = new Masina();

            MessageBox.Show("Mașină actualizată cu succes!", "Succes",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }



        private void BtnStergeMasina_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridMasini.SelectedItem is Masina m)
                Masini.Remove(m);
        }


        private void DataGridMasini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridMasini.SelectedItem is Masina m)
                MasinaCurenta = m;
        }

        private void IncarcaMasiniDinFisier()
        {
            string path = "masini.txt";

            if (!File.Exists(path))
                return;

            var linii = File.ReadAllLines(path);

            foreach (var linie in linii)
            {
                var parts = linie.Split(';');
                if (parts.Length == 5)
                {
                    Masini.Add(new Masina
                    {
                        Marca = parts[0],
                        Model = parts[1],
                        AnFabricatie = int.Parse(parts[2]),
                        NumarInmatriculare = parts[3],
                        Disponibila = bool.Parse(parts[4])
                    });
                }
            }
        }

        private void SalveazaMasiniInFisier()
        {
            string path = "masini.txt";

            List<string> linii = new List<string>();

            foreach (var m in Masini)
            {
                linii.Add($"{m.Marca};{m.Model};{m.AnFabricatie};{m.NumarInmatriculare};{m.Disponibila}");
            }

            File.WriteAllLines(path, linii);
        }

        private bool ValideazaDateMasina()
        {
            bool ok = true;

            // VALIDARE MARCĂ
            if (string.IsNullOrWhiteSpace(MasinaCurenta.Marca))
            {
                MessageBox.Show("Marca este obligatorie.");
                ok = false;
            }

            // VALIDARE MODEL
            if (string.IsNullOrWhiteSpace(MasinaCurenta.Model))
            {
                MessageBox.Show("Modelul este obligatoriu.");
                ok = false;
            }

            
            // VALIDARE AN FABRICAȚIE – realist (1900–2026)
            if (MasinaCurenta.AnFabricatie < 1900 || MasinaCurenta.AnFabricatie > 2026)
            {
                MessageBox.Show("Anul de fabricație trebuie să fie între 1900 și 2026.");
                ok = false;
            }


            // VALIDARE NUMĂR ÎNMATRICULARE – format SV12ABC
            if (string.IsNullOrWhiteSpace(MasinaCurenta.NumarInmatriculare) ||
                !EsteNumarInmatriculareValid(MasinaCurenta.NumarInmatriculare.ToUpper()))
            {
                MessageBox.Show("Numărul de înmatriculare trebuie să fie în formatul SV12ABC.");
                ok = false;
            }

            // VALIDARE UNICITATE NUMĂR ÎNMATRICULARE
            if (Masini.Any(x => x != MasinaCurenta &&
                                x.NumarInmatriculare == MasinaCurenta.NumarInmatriculare))
            {
                MessageBox.Show("Există deja o mașină cu acest număr de înmatriculare!");
                ok = false;
            }





            return ok;
        }

        private bool EsteNumarInmatriculareValid(string nr)
        {
            return Regex.IsMatch(nr, @"^[A-Z]{2}[0-9]{2}[A-Z]{3}$");
        }













    }
}