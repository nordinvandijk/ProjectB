using System;
using static System.Console;
using static CinemaApp.ConsoleUtils;
using System.Linq;
using System.Collections.Generic;

namespace CinemaApp.Screens
{
    class AddToOrderScreen : Screen
    {
        //Fields

        //Constructor
        public AddToOrderScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            Clear();

            // String maken voor het weergeven huidige items order
            string currentItems = "";
            List<string> alreadyFountItems = new List<string>();
            
            foreach(string item in App.seatsOverviewScreen.currentOrder.AddableItems)
            {
                if (!alreadyFountItems.Contains(item))
                {
                    currentItems += $"{item} | Huidige hoeveelheid: {App.seatsOverviewScreen.currentOrder.AddableItems.Where(x => x == item).Count()}\n";
                    alreadyFountItems.Add(item);
                }
            }

            // Alle categorien vinden
            List<string> categories = new List<string>();

            foreach(AddableItem item in App.addableItemsManager.addableItems)
            {
                if (!categories.Contains(item.Category))
                {
                    categories.Add(item.Category);
                }
            }

            // Categorien menu
            categories.Add("\nBevestigen");
            categories.Add("Terug");
            string[] categoryArr= categories.ToArray();
            Menu categoryMenu = new Menu(categoryArr, $"Snacks in uw order:\n--------------------\n{currentItems}--------------------\n\nKies een categorie waarvan u snacks wilt toevoegen:\n(Als u een snack wilt verwijderen voert u de hoeveelheid 0 in)", 0);
            string chosenCategorie = categories[categoryMenu.Run()];

            if (chosenCategorie == "\nBevestigen")
            {
                App.orderConfirmationScreen.run();
            }

            else if (chosenCategorie == "Terug")
            {
                App.seatsOverviewScreen.run();
            }

            else
            {
                // Alle snacks vinden in categorie
                List<string> snacksChosenCategorie = new List<string>();
                foreach (AddableItem item in App.addableItemsManager.addableItems)
                {
                    if (item.Category == chosenCategorie)
                    {
                        snacksChosenCategorie.Add($"{item.Name}");
                    }
                }

                // Snacks menu
                snacksChosenCategorie.Add("\nTerug");
                string[] snackArr = snacksChosenCategorie.ToArray();
                Menu snackMenu = new Menu(snackArr, "Voeg iets toe aan uw order:", 0);
                int chosenSnackIndex = snackMenu.Run();

                // Optie terug
                if (chosenSnackIndex == snackArr.Length - 1)
                {
                    run();
                }

                // Als er een addableItem geselecteerd is door de user
                else
                {
                    string chosenItem = snacksChosenCategorie[chosenSnackIndex];
                    Clear();
                    Console.WriteLine($"Hoe vaak wilt u '{chosenItem}' toevoegen aan uw bestelling?");

                    // TryParse om te controleren of de gebruiker een integer invult
                    string amountChosenItemString = ReadLine();
                    int amountChosenItem = 0;
                    while (!(int.TryParse(amountChosenItemString, out amountChosenItem)))
                    {
                        Clear();
                        Console.WriteLine("Voer een getal in");
                        amountChosenItemString = ReadLine();
                    }

                    // Verwijdert de huidige heoeveelheid van het gekozen item
                    App.seatsOverviewScreen.currentOrder.AddableItems.RemoveAll(x => x == chosenItem);

                    // Voegt nieuwe hoeveelheid toe van het gekozen item
                    for (int j = 0; j < amountChosenItem; j++)
                    {
                        App.seatsOverviewScreen.currentOrder.AddableItems.Add(chosenItem);
                    }
                    Clear();
                    Console.WriteLine($"{chosenItem} is {amountChosenItem}x aan uw bestelling toegevoegd");
                    WaitForKeyPress();
                    run();
                }
            }
        }
    }   
}