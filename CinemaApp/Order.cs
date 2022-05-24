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
    }
}
