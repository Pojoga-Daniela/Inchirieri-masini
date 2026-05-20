using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibrarieModele
{
    [Flags]
    public enum OptiuniMasina
    {
        None = 0,
        AerConditionat = 1,
        Navigatie = 2,
        CutieAutomata = 4

    }

    public enum CuloareMasina
    {
        Rosu,
        Alb,
        Negru,
        Albastru,
        Gri

    }
    public class Masina : INotifyPropertyChanged
    {
        private string marca;
        private string model;
        private int anFabricatie;
        private string numarInmatriculare;
        private bool disponibila;

        public string Marca
        {
            get => marca;
            set { marca = value; OnPropertyChanged(); }
        }

        public string Model
        {
            get => model;
            set { model = value; OnPropertyChanged(); }
        }

        public int AnFabricatie
        {
            get => anFabricatie;
            set { anFabricatie = value; OnPropertyChanged(); }
        }

        public string NumarInmatriculare
        {
            get => numarInmatriculare;
            set { numarInmatriculare = value; OnPropertyChanged(); }
        }

        public bool Disponibila
        {
            get => disponibila;
            set { disponibila = value; OnPropertyChanged(); }
        }

        public Masina() { }

        public Masina(string marca, string model, int anFabricatie, string numarInmatriculare)
        {
            Marca = marca;
            Model = model;
            AnFabricatie = anFabricatie;
            NumarInmatriculare = numarInmatriculare;
            Disponibila = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string Info()
        {
            return $"{Marca} {Model}, An: {AnFabricatie}, Nr: {NumarInmatriculare}, Disponibila: {(Disponibila ? "Da" : "Nu")}";
        }

    }
}