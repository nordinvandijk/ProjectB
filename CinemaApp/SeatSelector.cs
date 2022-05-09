using System;
using static System.Console;

namespace CinemaApp
{
    class SeatSelector
    {
        private int currentX = 0;
        private int currentY = 0;
        private float totalPrice = 0;
        public Seat[][] seats;

        public SeatSelector() { // in plaats van tuples heb ik objects gebruik 
            seats = new Seat[4][];
            seats[0] = new Seat[3]; 
            seats[0][1] = new Seat("occupied", 1.00f,0,1); //  availability, price, row, seat
            seats[0][2] = new Seat("available", 1.00f,0,2);
            seats[0][0] = new Seat("available", 1.00f,0,0);
            seats[1] = new Seat[3];
            seats[1][1] = new Seat("occupied", 1.20f,1,1);
            seats[1][2] = new Seat("available", 1.00f,1,2);
            seats[1][0] = new Seat("available", 1.00f,1,0);
            seats[2] = new Seat[3];
            seats[2][1] = new Seat("available", 1.20f,2,1);
            seats[2][2] = new Seat("available", 1.00f,2,2);
            seats[2][0] = new Seat("available", 1.00f,2,0); 
            seats[3] = new Seat[3];
            seats[3][1] = new Seat("available", 1.20f,3,1);
            seats[3][2] = new Seat("occupied", 1.00f,3,2);
            seats[3][0] = new Seat("available", 1.00f,3,0);
        }
        public SeatSelector(Seat[][] seats) {
            this.seats = seats;
        }

        public void Display() {
            for (int y = 0; y < seats.Length; y++) {
                ResetColor();
                Write("Rij " + (y+1) + " ");
                for (int x = 0; x < seats[y].Length; x++) {
                    // Begin van huidig geselcteerde stoel kleur.
                    if (currentX == x && currentY == y) {
                        if (seats[y][x].Availability == "selected") {
                            Square_color("Yellow"); // print een gele vierkant
                        }
                        else if (seats[y][x].Availability == "occupied") {
                            Square_color("Red"); // print een rode vierkant
                        }
                        else {
                            Square_color("White"); // print een witte vierkant
                        }
                    }
                    // Einde van huidig geselcteerde stoel kleur.
                    // Begin van andere stoelen kleur.
                    else if (seats[y][x].Availability == "selected") {
                       Square_color("DarkYellow"); // het is beter om deze kleur te veranderen met iets anders aangezien je het verschil vrijwel niet ziet
                    }
                    else if (seats[y][x].Availability == "occupied") {
                       Square_color("DarkRed"); // het is beter om deze kleur te veranderen met iets anders aangezien je het verschil vrijwel niet ziet
                    }
                    else {
                        ResetColor();
                        Square_color("Green"); // ik heb hier groen gebruikt in plaats van zwart omdat het er beter uitzag
                    }
                    // Einde van andere stoelen kleur.
                }
                Write(Environment.NewLine + Environment.NewLine);
            }
            ResetColor();
            // Code om de stoel prijs te laten zien
            try {
                WriteLine("Stoel prijs: " + String.Format("{0:0.00}", seats[currentY][currentX].Price));
            }
            catch {
                WriteLine("Stoel prijs: 0.00");
            }
            // Code om de totaal prijs te laten zien.
            WriteLine("Totaal prijs: " + String.Format("{0:0.00}", totalPrice));
            // Code om te laten zien of je op de bevestig knop bent.
            if (currentY == seats.Length) {
                WriteLine("\nBevestigen", BackgroundColor = ConsoleColor.White);
            }
            else {
                ResetColor();
                WriteLine("\nBevestigen");
            }
            ResetColor();

            Write("\n╒═══════════════════╕\n");
            Write("│ Legenda stoelen   │\n");
            Write("│ Beschikbaar    ");
            Square_color("Green");
            Write(" │\n");
            Write("│                   │\n");
            Write("│ Bezet          ");
            Square_color("DarkRed");
            Write(" │\n");
            Write("│                   │\n");
            Write("│ Geselecteerd   ");
            Square_color("DarkYellow");
            Write(" │\n");
            Write("╘═══════════════════╛");

        }

        public void Run()
        {
            while (true) { // Deze while loop zorgt ervoor dat je meerdere stoelen kan selecteren en dus niet stopt nadat je 1 stoel hebt geselecteerd
                ConsoleKey keyPressed;
                do
                {
                    Clear();
                    Display();
                    
                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    keyPressed = keyInfo.Key;
                    
                    // Code om te navigeren met user input
                    if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W)
                    {
                        int tempY = currentY;
                        currentY--;
                        if (currentY < 0) {
                            currentY = 0;
                        }
                    }
                    if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
                    {
                        currentY++;
                        if (currentY > seats.Length) {
                            currentY = seats.Length;
                        }
                    }
                    if (keyPressed == ConsoleKey.RightArrow || keyPressed == ConsoleKey.D && currentY <= seats[0].Length) // currentY <= seats[0].Length zodat je niet out of bounds kan gaan als je op bevestigen knop zit
                    {
                        currentX++;
                        if (currentX >= seats[currentY].Length) {
                            currentX = seats[currentY].Length-1;
                        }
                    }
                    if (keyPressed == ConsoleKey.LeftArrow || keyPressed == ConsoleKey.A)
                    {
                        currentX--;
                        if (currentX < 0) {
                            currentX = 0;
                        }
                    }
                } while(keyPressed != ConsoleKey.Enter);

                // Als de huidge positie niet op bevistigen is, dan checkt ie de status van de stoel waar je op zit om die te veranderen.
                if (currentY != seats.Length) {
                    if (seats[currentY][currentX].Availability == "available") {
                        totalPrice += seats[currentY][currentX].Price;
                        seats[currentY][currentX].Availability = "selected";
                    }
                    else if (seats[currentY][currentX].Availability == "selected") {
                        totalPrice -= seats[currentY][currentX].Price;
                        seats[currentY][currentX].Availability = "available";
                    }
                    else if (seats[currentY][currentX].Availability == "occupied") {
                        WriteLine("Deze stoel is bezet");
                    }
                    totalPrice = Math.Abs(totalPrice); // Zodat totalPrice niet -0.00 wordt
                }
                // Anders sta je wel op bevestigen en gaat ie uit de while loop
                else {
                    break;
                }
            }
        }

        public void Square_color(string Color){ //dit is een functie dat een vierkant print met de kleur die je wilt
            if (Color == "Green") Console.BackgroundColor = ConsoleColor.Green;
            if (Color == "Blue") Console.BackgroundColor = ConsoleColor.Blue;
            if (Color == "White") Console.BackgroundColor = ConsoleColor.White;
            if (Color == "Red") Console.BackgroundColor = ConsoleColor.Red;
            if (Color == "Gray") Console.BackgroundColor = ConsoleColor.Gray;
            if (Color == "Yellow") Console.BackgroundColor = ConsoleColor.Yellow;
            if (Color == "Black") Console.BackgroundColor = ConsoleColor.Black;
            if (Color == "DarkYellow") Console.BackgroundColor = ConsoleColor.DarkYellow;
            if (Color == "DarkRed") Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("".PadRight(2));
            Console.ResetColor();
        }
    }
    
}