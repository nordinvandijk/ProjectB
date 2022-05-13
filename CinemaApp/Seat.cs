using System;

namespace CinemaApp
{
    class Seat
    {
        public string Availability { get; set; }
        public float Price { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }

        public Seat(){
            this.Availability = "available";
            this.Price = 10.00f;
        }

        public Seat(string availability,float price, int row, int seat){
            this.Availability = availability;
            this.Price = price;
            this.Row = row;
            this.SeatNumber = seat;
        }
        
    }  
}