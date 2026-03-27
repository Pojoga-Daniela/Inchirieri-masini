using LibrarieModele;
using System.Collections.Generic;
using System.Linq;

namespace LibrarieStocareDate


{
    public class AdministratorEntitateMemorie
    {
        private List<Masina> masini = new List<Masina>();

        public void AdaugaMasina(Masina m)
        {
            masini.Add(m);
        }

        public List<Masina> GetToateMasinile()
        {
            return masini;
        }

        // 🔥 Metoda cerută în temă — folosește LINQ
        public Masina CautaDupaMarca(string marca)
        {
            return masini
                .FirstOrDefault(m => m.Marca.ToLower() == marca.ToLower());
        }

        // Exemplu suplimentar cu LINQ
        public List<Masina> GetMasiniDisponibile()
        {
            return masini
                .Where(m => m.Disponibila)
                .ToList();
        }
    }
}
