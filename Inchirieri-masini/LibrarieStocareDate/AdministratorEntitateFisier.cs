using LibrarieModele;
using System.IO;
using System.Collections.Generic;

namespace LibrarieStocareDate
{
    public class AdministratorEntitateFisier
    {
        private string caleFisier;

        public AdministratorEntitateFisier(string caleFisier)
        {
            this.caleFisier = caleFisier;
        }

        public void SalveazaMasina(Masina m)
        {
            // salvăm fiecare mașină ca linie text, de ex: Marca;Model;An;Nr
            File.AppendAllText(caleFisier, $"{m.Marca};{m.Model};{m.AnFabricatie};{m.NumarInmatriculare};{m.Disponibila}\n");
        }

        public List<Masina> CitesteMasini()
        {
            List<Masina> masini = new List<Masina>();
            if (!File.Exists(caleFisier))
                return masini;

            foreach (var linie in File.ReadAllLines(caleFisier))
            {
                var campuri = linie.Split(';');
                var masina = new Masina(
                    campuri[0],
                    campuri[1],
                    int.Parse(campuri[2]),
                    campuri[3]
                )
                { Disponibila = bool.Parse(campuri[4]) };

                masini.Add(masina);
            }
            return masini;
        }

        public Masina CautaDupaMarca(string marca)
        {
            var masini = CitesteMasini();
            return masini.FirstOrDefault(m => m.Marca.ToLower() == marca.ToLower());
        }

        public void ModificaMasina(string nrInmatriculare, Masina masinaNoua)
        {
            var masini = CitesteMasini();
            for (int i = 0; i < masini.Count; i++)
            {
                if (masini[i].NumarInmatriculare == nrInmatriculare)
                {
                    masini[i] = masinaNoua;
                    break;
                }
            }
            // rescriem fișierul complet
            File.WriteAllLines(caleFisier, masini.Select(m => $"{m.Marca};{m.Model};{m.AnFabricatie};{m.NumarInmatriculare};{m.Disponibila}"));
        }
    }
}