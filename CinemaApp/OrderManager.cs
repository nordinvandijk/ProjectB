using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CinemaApp
{
    class OrderManager
    {
        // Fields
        public Application App;
        public List<Order> orders;
        
        // Constructor
        public OrderManager(Application app)
        {
            App = app;
            LoadJson();
        }
        
        // Methods
        public Order CreateOrder(string username, List<Seat> selectedSeats, MovieItem chosenMovieItem)
        {
            int findOrderId = 0;
            bool orderIdFound = false;

            while (!orderIdFound)
            {
                // Used to generate a random ordernumber
                Random random = new Random();
                findOrderId = random.Next(100000, 999999);

                bool orderIdAlreadyExist = false;
                foreach (Order order in orders)
                {
                    if (findOrderId == order.OrderID)
                    {
                        orderIdAlreadyExist = true;
                    }
                }
                if (!orderIdAlreadyExist)
                {
                    orderIdFound = true;
                }
            }
            
            Order newOrder = new Order()
            {
                OrderID = findOrderId,
                Username = username,
                Seats = new List<Seat>(selectedSeats),
                AddableItems = new List<string>(),
                StartTimeString = chosenMovieItem.StartTimeString,
                EndTimeString = chosenMovieItem.EndTimeString,
                FilmTitle = chosenMovieItem.Title,
                Format = chosenMovieItem.Format,
                LocationName = chosenMovieItem.LocationName
            };

            return newOrder;
        }

        public Order CreateOrder(List<Seat> selectedSeats, MovieItem chosenMovieItem)
        {
            int findOrderId = 0;
            bool orderIdFound = false;

            while (!orderIdFound)
            {
                // Used to generate a random ordernumber
                Random random = new Random();
                findOrderId = random.Next(100000, 999999);

                bool orderIdAlreadyExist = false;
                foreach (Order order in orders)
                {
                    if (findOrderId == order.OrderID)
                    {
                        orderIdAlreadyExist = true;
                    }
                }
                if (!orderIdAlreadyExist)
                {
                    orderIdFound = true;
                }
            }

            Order newOrder = new Order()
            {
                OrderID = findOrderId,
                Username = null,
                Seats = new List<Seat>(selectedSeats),
                AddableItems = new List<string>(),
                StartTimeString = chosenMovieItem.StartTimeString,
                EndTimeString = chosenMovieItem.EndTimeString,
                FilmTitle = chosenMovieItem.Title,
                Format = chosenMovieItem.Format,
                LocationName = chosenMovieItem.LocationName
            };

            return newOrder;
        }
        public void LoadJson()
        {
            using (StreamReader sr = new StreamReader("Order.json"))
            {
                string json = sr.ReadToEnd();
                orders = JsonConvert.DeserializeObject<List<Order>>(json);
            }
        }

        public void UpdateJson()
        {
            using (StreamWriter sw = new StreamWriter("Order.json"))
            {
                string json = JsonConvert.SerializeObject(orders, Formatting.Indented);
                sw.WriteLine(json);
            }
        }
    }
}
