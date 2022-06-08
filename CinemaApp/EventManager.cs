using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using static System.Console;
using System;

namespace CinemaApp
{

    class EventManager
    {
        public List<Event> events = new List<Event>();
        
        public EventManager() {
            LoadJson();// zorgt ervoor dat de list "events" direct na het runnen gevuld wordt met de events van de json
        }

        /// <summary>
        /// This method will first make a new Event object with all the required parameters you need to pass in. Then it adds the Event object to the List<Event> called "events"
        /// and at last it will update the json file called "movieList" by using UpdateJson()
        /// </summary>
        /// <param name="name">name of the event.</param>
        /// <param name="desc">Description of the event you want to add.</param>
        /// <param name="eventDate">date of the event you want to add.</param>
        /// <param name="minAge">Minimum age of the event you want to add.</param>
        /// <param name="duration">Duration of the event you want to add.</param>
        /// <param name="ticketprice">The ticketprice of the event you want to add.</param>
        public void AddEvent(string name, string desc, string minAge, string duration, string ticketprice) 
        {
            Event Event = new Event() {
                Name = name,
                Description = desc,
                MinimumAge = minAge,
                Duration = duration,
                TicketPrice = ticketprice
            };
            events.Add(Event);
            UpdateJson();
            WriteLine("Evenement toegevoegd!");
        }
        public void RemoveEvent(string Event) {
            foreach (var Evenement in events.ToList()) { // Mischien handig om hier een reverse for loop te gebruiken ipv de list tijdelijk te kopieeren met ToList().
                if (Evenement.Name == Event) {
                    events.Remove(Evenement);
                    UpdateJson();
                    WriteLine("Evenement bestaat in het systeem en is het verwijderd");
                    return;   
                }
            }
            WriteLine("Evenement staat niet in het systeem");
        } 

        public void LoadJson() 
        {
            using (StreamReader sre = new StreamReader("eventList.json"))
            {
                string json = sre.ReadToEnd();
                events = JsonConvert.DeserializeObject<List<Event>>(json);
            }
        }

        public void UpdateJson() 
        {
           using (StreamWriter swe = new StreamWriter("eventList.json"))
            {   
                string json = JsonConvert.SerializeObject(events,Formatting.Indented);
                swe.WriteLine(json);
            } 
        }

    }
}
