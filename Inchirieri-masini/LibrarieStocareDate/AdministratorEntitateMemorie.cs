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

        
        public Masina CautaDupaMarca(string marca) 
        {
            return masini
                .FirstOrDefault(m => m.Marca.ToLower() == marca.ToLower()); /// Caută prima mașină care are marca specificată, ignorând majusculele
        }

        // Exemplu LINQ
        public List<Masina> GetMasiniDisponibile()
        {
            return masini
                .Where(m => m.Disponibila)
                .ToList();
        }
    }
}
