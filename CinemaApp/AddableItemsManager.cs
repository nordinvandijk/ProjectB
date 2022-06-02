using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CinemaApp
{
    internal class AddableItemsManager
    {
        public List<AddableItem> addableItems = new List<AddableItem>();
        public AddableItemsManager()
        {
            LoadJson();
        }
        public void LoadJson()
        {
            using (StreamReader sr = new StreamReader("AddableItemsList.json"))
            {
                string json = sr.ReadToEnd();
                addableItems = JsonConvert.DeserializeObject<List<AddableItem>>(json);
            }
        }

        public void UpdateJson()
        {
            using (StreamWriter sw = new StreamWriter("AddableItemsList.json"))
            {
                string json = JsonConvert.SerializeObject(addableItems, Formatting.Indented);
                sw.WriteLine(json);
            }
        }
    }
}
