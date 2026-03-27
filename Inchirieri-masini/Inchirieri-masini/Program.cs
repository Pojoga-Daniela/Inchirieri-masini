using System;
using LibrarieStocareDate;
using LibrarieModele;


class Program
{
    static void Main()
    {
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
        }


        // Afisare masini

        Console.WriteLine("\nMasini disponibile: ");
        for (int i = 0; i < n; i++)
        {

            Console.WriteLine(masini[i].Info()); //apeleaza metoda Info() pentru fiecare masina din vector si afiseaza informatiile despre ea

        }


        //Cautare dupa marca

        Console.Write("\nIntrodu marca cautata: ");
        string marcaCautata = Console.ReadLine(); //citeste marca cautata de la tastatura

        Console.WriteLine("\nRezulate cautare: ");
        bool gasit = false; //variabila pentru a verifica daca s-a gasit macar o masina cu marca cautata

        for (int i = 0; i < n; i++)
        {
            if (masini[i].Marca.ToLower() == marcaCautata.ToLower()) //compara marca masinii curente cu marca cautata, ignorand majusculele
            {
                Console.WriteLine(masini[i].Info()); //daca se gaseste o masina cu marca cautata, se afiseaza informatiile despre ea
                gasit = true; //daca s-a gasit macar o masina cu marca cautata, se seteaza gasit pe true
            }


        }

        if (!gasit)
        {
            Console.WriteLine("Nu s-au gasit masini cu aceasta marca.");
        }

    }
}
