using System;
using static System.Console;
using System.Collections.Generic;

namespace CinemaApp
{
    class SeatSelector
    {
        private int currentX = 0;
        private int currentY = 0;
        public float totalPrice = 0;
        public List<Seat> selectedSeats = new List<Seat>();
        public Seat[][] seats;

        public SeatSelector() { // in plaats van tuples heb ik objects gebruik 
            seats = new Seat[5][];
            seats[0] = new Seat[5]; 
            seats[0][0] = new Seat("occupied", 1.00f,1,1); //  availability, price, row, seat
            seats[0][1] = new Seat("available", 1.00f,1,2);
            seats[0][2] = new Seat("available", 1.00f,1,3);
            seats[0][3] = new Seat("available", 1.00f,1,4);
            seats[0][4] = new Seat("available", 1.00f,1,5);
            seats[1] = new Seat[5];
            seats[1][0] = new Seat("occupied", 1.20f,2,1);
            seats[1][1] = new Seat("available", 1.00f,2,2);
            seats[1][2] = new Seat("available", 1.00f,2,3);
            seats[1][3] = new Seat("available", 1.00f,2,4);
            seats[1][4] = new Seat("available", 1.00f,2,5);
            seats[2] = new Seat[5];
            seats[2][0] = new Seat("available", 1.20f,3,1);
            seats[2][1] = new Seat("available", 1.00f,3,2);
            seats[2][2] = new Seat("available", 1.00f,3,3); 
            seats[2][3] = new Seat("available", 1.00f,3,4);
            seats[2][4] = new Seat("available", 1.00f,3,5);
            seats[3] = new Seat[5];
            seats[3][0] = new Seat("available", 1.20f,4,1);
            seats[3][1] = new Seat("occupied", 1.00f,4,2);
            seats[3][2] = new Seat("available", 1.00f,4,3);
            seats[3][3] = new Seat("available", 1.00f,4,4);
            seats[3][4] = new Seat("available", 1.00f,4,5);
            seats[4] = new Seat[5];
            seats[4][0] = new Seat("available", 1.20f,5,1);
            seats[4][1] = new Seat("occupied", 1.00f,5,2);
            seats[4][2] = new Seat("available", 1.00f,5,3);
            seats[4][3] = new Seat("available", 1.00f,5,4);
            seats[4][4] = new Seat("available", 1.00f,5,5);
        }
        public SeatSelector(Seat[][] seats) {
            this.seats = seats;
        }

        public void Display() {
            CursorVisible = false;
            WriteLine("╒════════════════════════════════════════════════════════════╕"); //uitleg
            WriteLine("│ Gebruik de pijltes toetsen of 'WASD' om te navigeren.      │");
            WriteLine("│ Toets ENTER om te selecteren en deselecteren.              │");
            WriteLine("│ Navigeer helemaal naar beneden om naar bevestigen te gaan. │");
            WriteLine("╘════════════════════════════════════════════════════════════╛");
            WriteLine("");
    
            WriteLine("       " + new string(' ', ((seats[0].Length*3)+(seats[0].Length)-1)/2-4) + "Filmdoek.");
            WriteLine("");        
            for (int y = 0; y < seats.Length; y++) {
                ResetColor();
                Write("Rij {0:00} ", y+1);
                for (int x = 0; x < seats[y].Length; x++) {
                    // Begin van huidig geselcteerde stoel kleur.
                    if (currentX == x && currentY == y) {
                        if (seats[y][x].Availability == "selected") {
                            Square_color("Yellow", "X"); // print een gele vierkant
                        }
                        else if (seats[y][x].Availability == "occupied") {
                            Square_color("Red", "X"); // print een rode vierkant
                        }
                        else {
                            Square_color("Green", "X"); // print een witte vierkant
                        }
                    }
                    // Einde van huidig geselcteerde stoel kleur.
                    // Begin van andere stoelen kleur.
                    else if (seats[y][x].Availability == "selected") {
                       Square_color("DarkMagenta","+"); // het is beter om deze kleur te veranderen met iets anders aangezien je het verschil vrijwel niet ziet
                    }
                    else if (seats[y][x].Availability == "occupied") {
                       Square_color("DarkRed", "O"); // het is beter om deze kleur te veranderen met iets anders aangezien je het verschil vrijwel niet ziet
                    }
                    else {
                        ResetColor();
                        Square_color("DarkGreen"); // ik heb hier groen gebruikt in plaats van zwart omdat het er beter uitzag
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
            // Code om te laten zien of je op de bevestig of terug knop bent.
            if (currentY == seats.Length) {
                WriteLine("\nBevestigen", BackgroundColor = ConsoleColor.White);
                ResetColor();
                WriteLine("Terug");
            }
            else if (currentY > seats.Length) {
                WriteLine("\nBevestigen");
                WriteLine("Terug", BackgroundColor = ConsoleColor.White);
                ResetColor();
            }
            else {
                ResetColor();
                WriteLine("\nBevestigen");
                WriteLine("Terug");
            }
            ResetColor();

            Write("\n╒══════════════════════════════╕\n"); // legenda
            Write("│ Legenda                      │\n");
            Write("│                              │\n");
            Write("│ Huidige positie           X  │\n");
            Write("│                              │\n");
            Write("│ Beschikbaar              ");
            Square_color("DarkGreen");
            Write("│\n");
            Write("│                              │\n");
            Write("│ Bezet                    ");
            Square_color("DarkRed","O");
            Write("│\n");
            Write("│                              │\n");
            Write("│ Geselecteerd             ");
            Square_color("DarkMagenta","+");
            Write("│\n");
            Write("╘══════════════════════════════╛");

        }

        // Bool = (true) als er op bevestigen is geklikt, (false) als er op terug is geklikt.
        public bool Run()
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
                        currentY--;
                        if (currentY < 0) {
                            currentY = 0;
                        }
                    }
                    if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
                    {
                        currentY++;
                        if (currentY > seats.Length + 1) {
                            currentY = seats.Length + 1;
                        }
                    }
                    if (keyPressed == ConsoleKey.RightArrow || keyPressed == ConsoleKey.D && currentY < seats[0].Length) // currentY < seats[0].Length zodat je niet out of bounds kan gaan als je op bevestigen knop zit
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
                if (currentY < seats.Length) {
                    if (seats[currentY][currentX].Availability == "available") {
                        totalPrice += seats[currentY][currentX].Price;
                        selectedSeats.Add(seats[currentY][currentX]);
                        seats[currentY][currentX].Availability = "selected";
                    }
                    else if (seats[currentY][currentX].Availability == "selected") {
                        totalPrice -= seats[currentY][currentX].Price;
                        selectedSeats.Remove(seats[currentY][currentX]);
                        seats[currentY][currentX].Availability = "available";
                    }
                    else if (seats[currentY][currentX].Availability == "occupied") {
                        WriteLine("\nDeze stoel is bezet");
                        ConsoleUtils.WaitForKeyPress();
                    }
                    totalPrice = Math.Abs(totalPrice); // Zodat totalPrice niet -0.00 wordt
                }
                // Anders sta je wel op bevestigen of terug
                else if (currentY == seats.Length){
                    return true; // Op bevestigen geklikt
                }    
                else {
                    return false; // Op terug geklikt
                }
            }
             
        }

        public void Square_color(string Color, string selected = ""){ //dit is een functie dat een vierkant print met de kleur die je wilt 
            if (Color == "DarkGreen") Console.BackgroundColor = ConsoleColor.DarkGreen;
            if (Color == "Blue") Console.BackgroundColor = ConsoleColor.Blue;
            if (Color == "Green") Console.BackgroundColor = ConsoleColor.Green;
            if (Color == "Red") Console.BackgroundColor = ConsoleColor.Red;
            if (Color == "Gray") Console.BackgroundColor = ConsoleColor.Gray;
            if (Color == "Yellow") Console.BackgroundColor = ConsoleColor.Magenta;
            if (Color == "Black") Console.BackgroundColor = ConsoleColor.Black;
            if (Color == "DarkMagenta") Console.BackgroundColor = ConsoleColor.DarkMagenta;
            if (Color == "DarkRed") Console.BackgroundColor = ConsoleColor.DarkRed;
            if (selected == "X") { 
                Console.Write("".PadLeft(1) + "X".PadRight(2)); 
            } // ik heb ervoor verzorgt dat je een X, O, + en - kan toevoegen aan de vierkanten zodat het duidelijker is voor iemand die kleurenblind is
            else if(selected == "O"){
                Console.Write("".PadLeft(1) + "O".PadRight(2));
            }
            else if(selected == "+"){
                Console.Write("".PadLeft(1) + "+".PadRight(2));
            }
            else {
                Console.Write(" -".PadRight(3));
            }
            Console.ResetColor();
            Write(" ");
        }

        public void filter(){ // zorgt ervoor dat selected veranderd naar available want je wilt geen selected in je json  
            foreach(Seat[] rows in seats){
                foreach(Seat chair in rows){
                    if (chair.Availability == "selected"){
                        chair.Availability = "available";
                    }
                }
            }
        }
    }
    
}