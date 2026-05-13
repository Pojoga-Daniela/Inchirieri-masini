using LibrarieModele;
using System;
using System.Collections.ObjectModel;
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
        private const int MAX_LUNGIME = 15;

        // lista care se afișează în DataGrid
        private ObservableCollection<Client> listaClienti = new ObservableCollection<Client>();

        public MainWindow()
        {
            InitializeComponent();

            // conectăm DataGrid-ul la listă
            DataGridClienti.ItemsSource = listaClienti;
            CmbSelecteazaClient.ItemsSource = listaClienti;
            this.SizeChanged += MainWindow_SizeChanged;

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

            MessageBox.Show("Client adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

            OnResetClient(null, null);
        }

        private void OnResetClient(object sender, RoutedEventArgs e)
        {
            TxtNume.Text = "";
            TxtPrenume.Text = "";
            TxtCNP.Text = "";

            ErrNume.Visibility = Visibility.Collapsed;
            ErrPrenume.Visibility = Visibility.Collapsed;
            ErrCNP.Visibility = Visibility.Collapsed;

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

                // actualizare DataGrid
                DataGridClienti.Items.Refresh();
            }
        }




    }
}