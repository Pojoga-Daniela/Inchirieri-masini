using LibrarieModele;
using LibrarieStocareDate;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        
        // LOGIN
        //Citirea utilizatorului de la tastatura

        string userCorect = "Daniela";
        string parolaCorecta = "234";

        Console.Write("User: ");
        string user = Console.ReadLine();

        Console.Write("Parola: ");
        string parola = Console.ReadLine();

        if (user != userCorect || parola != parolaCorecta)
        {
            Console.WriteLine("Autentificare esuata!");
            return;
        }

        Console.WriteLine("Autentificare reusita!");

        
        // ADMINISTRATORI
        
        var adminMasini = new AdministratorEntitateFisier("masini.txt");
        var adminClienti = new AdministratorClientFisier("clienti.txt");

        Console.WriteLine("\n===== MENIU =====");
        Console.WriteLine("1. Afiseaza toate masinile");
        Console.WriteLine("2. Afiseaza masini disponibile");
        Console.WriteLine("3. Cauta masina dupa marca");
        Console.WriteLine("4. Adauga client");
        Console.WriteLine("5. Afiseaza clienti");
        Console.WriteLine("6. Iesire");

        bool ruleaza = true;

        while (ruleaza)
        {
            

            Console.Write("Optiune: ");
            if (!int.TryParse(Console.ReadLine(), out int opt))
            {
                Console.WriteLine("Introdu un numar valid!");
                continue;
            }

            switch (opt)
            {
                
                // Afisare toate masinile
                
                case 1:
                    var masini = adminMasini.CitesteMasini(); // Salvarea datelor într-o colecție de obiecte (TEMA 3)
                    foreach (var m in masini)                 //Afișarea datelor dintr-o colecție de obiecte (Tema 3)
                        Console.WriteLine(m.Info());
                    break;

                
                // Afisare masini disponibile
                
                case 2:
                    var masiniDisp = adminMasini.CitesteMasini();
                    bool gasitDisp = false;
                    foreach (var m in masiniDisp)
                    {
                        if (m.Disponibila)
                        {
                            Console.WriteLine(m.Info());
                            gasitDisp = true;
                        }
                    }
                    if (!gasitDisp)
                        Console.WriteLine("Nu sunt masini disponibile.");
                    break;

                
                // Cauta masina dupa marca
                
                case 3:
                    Console.Write("Marca cautata: ");
                    string marca = Console.ReadLine();

                    var lista = adminMasini.CitesteMasini(); 
                    bool gasit = false;

                    foreach (var m in lista)
                    {
                        if (m.Marca.ToLower() == marca.ToLower())
                        {
                            Console.WriteLine(m.Info());
                            gasit = true;
                        }
                    }

                    if (!gasit)
                        Console.WriteLine("Nu s-au gasit masini cu marca introdusa.");
                    break;


                // Adauga client
                //Datele sunt citite de la tastatură folosind Console.ReadLine() (TEMA 3)


                case 4:
                    Console.Write("Nume: ");
                    string nume = Console.ReadLine();

                    Console.Write("Prenume: ");
                    string prenume = Console.ReadLine();

                    Console.Write("CNP: ");
                    string cnp = Console.ReadLine();

                    Client client = new Client(nume, prenume, cnp);
                    adminClienti.SalveazaClient(client);

                    Console.WriteLine("Client adaugat!");
                    break;

                
                // Afiseaza clienti
                

                case 5:
                    var clienti = adminClienti.CitesteClienti();  //Salvarea datelor într-o colecție de obiecte (TEMA 3)
                    if (clienti.Count == 0)
                        Console.WriteLine("Nu exista clienti in fisier.");
                    else
                        foreach (var c in clienti)      //Afișarea datelor dintr-o colecție de obiecte
                            Console.WriteLine(c.Info());
                    break;

                
                // Iesire
                
                case 6:
                    ruleaza = false;
                    Console.WriteLine("La revedere!");
                    break;

                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }
        }
    }
}