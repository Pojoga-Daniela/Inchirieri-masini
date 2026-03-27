using System;

namespace LibrarieModele
{
    public class Inchiriere
    {
        public Client Client { get; set; }
        public Masina Masina { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataFinal { get; set; }

        public Inchiriere(Client client, Masina masina, DateTime dataStart, DateTime dataFinal)
        {
            Client = client;
            Masina = masina;
            DataStart = dataStart;
            DataFinal = dataFinal;

            masina.Disponibila = false;
        }

        public string Info()
        {
            return Client.Nume + " " + Client.Prenume +
                   " a inchiriat masina " + Masina.Marca + " " + Masina.Model +
                   " din " + DataStart.ToShortDateString() +
                   " pana in " + DataFinal.ToShortDateString();
        }
    }
}