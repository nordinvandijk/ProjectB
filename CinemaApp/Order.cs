using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp
{
    class Order
    {
        public int orderID { get; set; }
        public string username { get; set; }
        public List<Seat> seats { get; set; }
        public List<string> snacks { get; set; }
        public List<string> accessoires { get; set; }
    }
}
