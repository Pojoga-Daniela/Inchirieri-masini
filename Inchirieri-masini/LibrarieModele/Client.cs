using System;

namespace LibrarieModele
{

    public class Client
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }

        public string Gen { get; set; }
        public string Abonat { get; set; }
        public DateTime DataNasterii { get; set; }

        public Client(string nume, string prenume, string cnp)
        {
            Nume = nume;
            Prenume = prenume;
            CNP = cnp;
            DataNasterii = DateTime.Today; // valoare implicită
        }


        public Client(string nume, string prenume, string cnp, DateTime dataNasterii)
        {
            Nume = nume;
            Prenume = prenume;
            CNP = cnp;
            DataNasterii = dataNasterii;

        }

        public string Info()
        {
            return Nume + " " + Prenume + " - CNP: " + CNP;
        }
    }
}