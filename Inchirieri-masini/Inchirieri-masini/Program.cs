using System;
using LibrarieStocareDate;
using LibrarieModele;


class Program
{
    static void Main()
    {

        AdministratorEntitateFisier adminMasini = new AdministratorEntitateFisier("masini.txt");
        AdministratorClientFisier adminClienti = new AdministratorClientFisier("clienti.txt");

        Console.Write("Numar masini: ");
        int n = int.Parse(Console.ReadLine()); //citeste un  numar de la tastatura si-l salveaza in n
        Masina[] masini = new Masina[n]; //vector de masini

        // Citire masini

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nMasina {i + 1}: "); //$ - pentru a afisa numarul curent al masinii (incepand de la 1)

            Console.Write("Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("An fabricatie: ");
            int anFabricatie = int.Parse(Console.ReadLine());

            Console.Write("Numar inmatriculare: ");
            string numarInmatriculare = Console.ReadLine();

            masini[i] = new Masina(marca, model, anFabricatie, numarInmatriculare); //creeaza o noua masina cu datele citite si o stocheaza in vector
            adminMasini.SalveazaMasina(masini[i]);
        }

        var masiniDinFisier = adminMasini.CitesteMasini(); ///apeleaza metoda CitesteMasini() a administratorului de masini pentru a citi toate masinile din fisier si le stocheaza in variabila masiniDinFisier

        Console.WriteLine("\nMasini din fisier:");
        foreach (var m in masiniDinFisier)
        {
            Console.WriteLine(m.Info());
        }

        // Afisare masini

        /*Console.WriteLine("\nMasini disponibile: ");
        for (int i = 0; i < n; i++)
        {

            Console.WriteLine(masini[i].Info()); //apeleaza metoda Info() pentru fiecare masina din vector si afiseaza informatiile despre ea

        }*/


        //Cautare dupa marca

        Console.Write("\nIntrodu marca cautata: ");
        string marcaCautata = Console.ReadLine();

        var masinaGasita = adminMasini.CautaDupaMarca(marcaCautata);

        if (masinaGasita != null)
        {
            Console.WriteLine(masinaGasita.Info());
        }
        else
        {
            Console.WriteLine("Nu s-au gasit masini.");
        }


        //Modificare masina

        Console.Write("\nNr inmatriculare de modificat: ");
        string nr = Console.ReadLine();

        Console.Write("Marca noua: ");
        string marcaNoua = Console.ReadLine();

        Console.Write("Model nou: ");
        string modelNou = Console.ReadLine();

        Console.Write("An nou: ");
        int anNou = int.Parse(Console.ReadLine());

        Masina masinaNoua = new Masina(marcaNoua, modelNou, anNou, nr);

        adminMasini.ModificaMasina(nr, masinaNoua);

        Console.WriteLine("Masina modificata!");

        //Adaugare client

        Console.WriteLine("\n--- CLIENT ---");

        Console.Write("Nume: ");
        string nume = Console.ReadLine();

        Console.Write("Prenume: ");
        string prenume = Console.ReadLine();

        Console.Write("CNP: ");
        string cnp = Console.ReadLine();

        Client client = new Client(nume, prenume, cnp);
        adminClienti.SalveazaClient(client);

        //Citire client

        var clienti = adminClienti.CitesteClienti();

        Console.WriteLine("\nClienti:");
        foreach (var c in clienti)
        {
            Console.WriteLine(c.Info());
        }
    }
}
