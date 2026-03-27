using System;

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
    public class Masina
    {
        public string Marca { get; }
        public string Model { get; }
        public int AnFabricatie { get; }
        public string NumarInmatriculare { get; }
        public bool Disponibila { get; set; }
        public Masina(string marca, string model, int anFabricatie, string numarInmatriculare)
        {
            Marca = marca;
            Model = model;
            AnFabricatie = anFabricatie;
            NumarInmatriculare = numarInmatriculare;
        }

        public string Info()
        {
            return $"Masina: {Marca} {Model}, An: {AnFabricatie}, Nr: {NumarInmatriculare}";
        }
    }
}