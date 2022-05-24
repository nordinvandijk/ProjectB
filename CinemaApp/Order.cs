using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp
{
    class Order
    {
        public int OrderID { get; set; }
        public string Username { get; set; }
        public List<Seat> Seats { get; set; }
        public List<string> AddableItems { get; set; }
        public string FilmTitle { get; set; }
        public string Format { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public string LocationName { get; set; }
    }
}
