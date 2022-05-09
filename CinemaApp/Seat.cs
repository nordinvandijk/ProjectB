using System;

namespace CinemaApp
{
    class Seat
    {
        public string Availability;
        public double Price;

        public Tuple<int,int> CurrentPosition;

        public Seat(string availability,double price, int row, int seat){
            this.Availability = availability;
            this.Price = price;
            this.CurrentPosition = Tuple.Create(row,seat);
        }
    }  
}