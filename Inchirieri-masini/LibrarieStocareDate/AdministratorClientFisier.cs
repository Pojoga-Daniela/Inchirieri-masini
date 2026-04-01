using LibrarieModele;

public class AdministratorClientFisier
{
    private string caleFisier;

    public AdministratorClientFisier(string caleFisier)
    {
        this.caleFisier = caleFisier;
    }

    //Facilități pentru a doua entitate
    public void SalveazaClient(Client c)
    {
        File.AppendAllText(caleFisier, $"{c.Nume};{c.Prenume};{c.CNP}\n");
    }

    //Nivelul StocareDate cu fișier text (tema 5)
    public List<Client> CitesteClienti()
    {
        List<Client> clienti = new List<Client>();
        if (!File.Exists(caleFisier))
            return clienti;

        foreach (var linie in File.ReadAllLines(caleFisier))
        {
            var campuri = linie.Split(';');
            clienti.Add(new Client(campuri[0], campuri[1], campuri[2]));
        }
        return clienti;
    }

    public Client CautaDupaCNP(string cnp)
    {
        return CitesteClienti().FirstOrDefault(c => c.CNP == cnp);
    }

    public void ModificaClient(string cnp, Client clientNou)
    {
        var clienti = CitesteClienti();
        for (int i = 0; i < clienti.Count; i++)
        {
            if (clienti[i].CNP == cnp)
            {
                clienti[i] = clientNou;
                break;
            }
        }
        File.WriteAllLines(caleFisier, clienti.Select(c => $"{c.Nume};{c.Prenume};{c.CNP}"));
    }
}