using System;
using static System.Console;
using static CinemaApp.ConsoleUtils;
using System.Linq;

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
            string title = "Voeg iets toe aan uw order:";
            string[] options = new string[App.addableItemsManager.addableItems.Count + 2];

            int i = 0;
            foreach(AddableItem item in App.addableItemsManager.addableItems)
            {
                // Displayt de naam van een item en hoevaak hij op dit moment in de current order zit
                options[i] = $"{item.Name} | Huidige hoeveelheid: {App.seatsOverviewScreen.currentOrder.AddableItems.Where(x => x == item.Name).Count()}";
                i++;
            }

            // Extra opties toevoegen aan menu
            options[options.Length - 1] = "Terug";
            options[options.Length - 2] = "Bevestigen";

            // Menu aanmaken en runnen
            Menu addToOrderMenu = new Menu(options, title, 0);
            int chosenOption = addToOrderMenu.Run();
            
            // Optie bevestigen
            if (chosenOption == options.Length - 2)
            {
                App.orderConfirmationScreen.run();
            }

            // Optie terug
            else if (chosenOption == options.Length - 1)
            {
                App.seatsOverviewScreen.run();
            }

            // Als er een addableItem geselecteerd is door de user
            else
            {
                string chosenItem = App.addableItemsManager.addableItems[chosenOption].Name;
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