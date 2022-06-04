using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CinemaApp
{
    class OmzetManager
    {
        // Fields
        public List<Omzet> totaalOmzet;
        
        // Constructor
        public OmzetManager()
        {
            LoadJson();
        }
        
        // Methods
        public Omzet CreateOmzet(List<Seat> selectedSeats,List<AddableItem> AddableItems)
        {
            Omzet newOmzet = new Omzet()
            {
                Seats = new List<Seat>(selectedSeats),
                AddableItems = new List<AddableItem>(),
                CurrentDate = (DateTime.Now.Date).ToString()
            };
            return newOmzet;
        }

        public void LoadJson()
        {
            using (StreamReader sr = new StreamReader("Omzet.json"))
            {
                string json = sr.ReadToEnd();
                totaalOmzet = JsonConvert.DeserializeObject<List<Omzet>>(json);
            }
        }

        public void UpdateJson()
        {
            using (StreamWriter sw = new StreamWriter("Omzet.json"))
            {
                string json = JsonConvert.SerializeObject(totaalOmzet, Formatting.Indented);
                sw.WriteLine(json);
            }
        }
    }
}
