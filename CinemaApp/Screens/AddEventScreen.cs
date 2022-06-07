using System;
using static System.Console;
using System.Globalization;

namespace CinemaApp.Screens
{
    class AddEventScreen : Screen
    {
        //Fields
        public string name = "<Leeg>";
        public string description = "<Leeg>";

        public string minAge = "<Leeg>";
        public string duration = "<Leeg>";
        public string ticketprice = "<Leeg>";

        //Constructor
                public AddEventScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
            
        }

        //Methods
        public override void run()
        {
           
           //wordt gebruikt voor de Event datum
           DateTimeStyles styles = DateTimeStyles.None;
           var cultureInfo = new CultureInfo("nl-NL");
           
           //keuzes voor keuzemenu
           string titel = @"Events toevoegen";
           string[] options = {$"Naam event : {name}", $"Beschrijving : {description}", $"Minimale leeftijd : {minAge}", $"Duur evenement : {duration}", $"Ticket prijs: {ticketprice}", "Bevestig", "Terug"}; 

            Menu AddEventMenu = new Menu(options, titel, 0);
            int ChosenOption = AddEventMenu.Run();
            switch(ChosenOption)
            {
                case 0: //Name
                    Clear();
                    WriteLine("Wat is de naam van het evenement?");
                    CursorVisible = true;
                    name = ReadLine();
                    CursorVisible = false;
                    run();
                    break;
                
                case 1:
                    //Description
                    Clear();
                    WriteLine("Wat is de beschrijving van het evenement?");
                    CursorVisible = true;
                    description = ReadLine();
                    CursorVisible = false;
                    run();
                    break;
                
                case 2: //MinAge
                    Clear();
                    WriteLine("Wat is de minimale leeftijd voor het evenement?");
                    CursorVisible = true;
                    minAge = ReadLine();
                    int intMinAge = -1;
                    while(!Int32.TryParse(minAge,out intMinAge) || intMinAge<=0){
                        Clear();
                        WriteLine("Dit is geen geldige leeftijd, voer een getal van 0 of hoger in: ");
                        minAge = ReadLine();
                    }
                    CursorVisible = false;
                    run();
                    break;
                
                case 3: //Duration
                    bool durationInputCorrect = false;
                    while (!durationInputCorrect) {
                    Clear();
                    WriteLine("Hoe lang duurt het evenement? Voer een tijd in met de format 00:00:00");
                    CursorVisible = true;
                    string durationInputString = ReadLine();
                    CursorVisible = false;

                    TimeSpan durationTimeSpan;
                    durationInputCorrect = TimeSpan.TryParse(durationInputString, cultureInfo, out durationTimeSpan);

                    if (!durationInputCorrect)
                        {
                            Clear();
                            WriteLine("Dit is geen juist format voor het invoeren van de evenement duur");
                            ConsoleUtils.WaitForKeyPress();
                        }
                        else
                        {
                            duration = durationTimeSpan.ToString();
                        }
                    }
                    run();
                    break;

                case 4: //TicketPrice
                    Clear();
                    WriteLine("Wat is de prijs voor een ticket?");
                    CursorVisible = true;
                    float minPrice = -1;
                    ticketprice = ReadLine();
                    while(!float.TryParse(ticketprice,out minPrice) || minPrice<=0)
                    {
                        Clear();
                        WriteLine("Dit is geen geldige prijs, voer een getal van 0 of hoger in: ");
                        ticketprice = ReadLine();
                    }
                    CursorVisible = false;
                    run();
                    break;
                
                case 5:
                    Clear();
                    WriteLine("Bevestig");
                    if (name != "<Leeg>" && description != "<Leeg>" && minAge != "<Leeg>" && duration != "<Leeg>" && ticketprice != "<Leeg>")
                    {
                      try {
                      //zet in eventList, kan alleen bevestigen als er geen error is
                      App.eventManager.AddEvent(name, description, minAge, duration, ticketprice);
                      name = "<leeg>";
                      description = "<leeg>";
                      minAge = "<leeg>";
                      duration = "<leeg>";
                      ticketprice = "<leeg>";
                      ConsoleUtils.WaitForKeyPress();
                      run();
                      App.adminPanelScreen.run();
                      }
                      catch {
                        WriteLine("De inputs waren verkeerd probeer het nog een keer.");
                        ConsoleUtils.WaitForKeyPress();
                        run();
                      }
                    }
                    else
                    {
                        WriteLine("Sommige vakken zijn nog niet ingevuld");
                        ConsoleUtils.WaitForKeyPress();
                        run();
                    }
                    break;
                
                case 6: // terug naar Admin panel
                    App.adminPanelScreen.run();
                    break;

        
                }
        }
    }
}