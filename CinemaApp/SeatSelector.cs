using System;
using static System.Console;

namespace CinemaApp
{
    class SeatSelector
    {
        private int currentX;
        private int currentY;
        private double totalPrice = 0;
        public Tuple<string,double>[][] seats;


        public SeatSelector() {
            currentX = 0;
            currentY = 0;
            seats = new Tuple<string,double>[4][];
            seats[0] = new Tuple<string,double>[3];
            seats[0][1] = new Tuple<string,double>("occupied", 1.00);
            seats[0][2] = new Tuple<string,double>("available", 1.00);
            seats[0][0] = new Tuple<string,double>("available", 1.00);
            seats[1] = new Tuple<string,double>[3];
            seats[1][1] = new Tuple<string,double>("occupied", 1.20);
            seats[1][2] = new Tuple<string,double>("available", 1.00);
            seats[1][0] = new Tuple<string,double>("available", 1.00);
            seats[2] = new Tuple<string,double>[3];
            seats[2][1] = new Tuple<string,double>("available", 1.20);
            seats[2][2] = new Tuple<string,double>("available", 1.00);
            seats[2][0] = new Tuple<string,double>("available", 1.00);
            seats[3] = new Tuple<string,double>[3];
            seats[3][1] = new Tuple<string,double>("available", 1.20);
            seats[3][2] = new Tuple<string,double>("occupied", 1.00);
            seats[3][0] = new Tuple<string,double>("available", 1.00);
        }

        public void Display() {
            for (int y = 0; y < seats.Length; y++) {
                ResetColor();
                Write("Rij " + y + " ");
                for (int x = 0; x < seats[y].Length; x++) {
                    if (currentX == x && currentY == y) {
                        ConsoleColor cursorColor;
                        if (seats[y][x].Item1 == "selected") {
                            BackgroundColor = ConsoleColor.Yellow;
                        }
                        else if (seats[y][x].Item1 == "occupied") {
                            BackgroundColor = ConsoleColor.Red;
                        }
                        else {
                            BackgroundColor = ConsoleColor.White;
                        }
                        Write(seats[y][x].ToString());
                    }
                    else if (seats[y][x].Item1 == "selected") {
                        Write(seats[y][x].ToString(), BackgroundColor = ConsoleColor.DarkYellow);
                    }
                    else if (seats[y][x].Item1 == "occupied") {
                        Write(seats[y][x].ToString(), BackgroundColor = ConsoleColor.DarkRed);
                    }
                    else {
                        ResetColor();
                        Write(seats[y][x].ToString());
                    }
                }
                Write(Environment.NewLine + Environment.NewLine);
            }
            ResetColor();
            try {
                WriteLine("Stoel prijs: " + String.Format("{0:0.00}", seats[currentY][currentX].Item2));
            }
            catch {
                WriteLine("Stoel prijs: 0.00");
            }
            WriteLine("Totaal prijs: " + String.Format("{0:0.00}", totalPrice));
            if (currentY == seats.Length) {
                WriteLine("\nBevestigen", BackgroundColor = ConsoleColor.White);
            }
            else {
                ResetColor();
                WriteLine("\nBevestigen");
            }
            ResetColor();
        }

        // Return value van deze method moet waarschijlijnk worden aangepast
        public Tuple<int,int> Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                Display();
                
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                
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
                if (keyPressed == ConsoleKey.RightArrow || keyPressed == ConsoleKey.D)
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
                if (seats[currentY][currentX].Item1 == "available") {
                    totalPrice += seats[currentY][currentX].Item2;
                    seats[currentY][currentX] = new Tuple<string,double>("selected", seats[currentY][currentX].Item2);
                }
                else if (seats[currentY][currentX].Item1 == "selected") {
                    totalPrice -= seats[currentY][currentX].Item2;
                    seats[currentY][currentX] = new Tuple<string,double>("available", seats[currentY][currentX].Item2);
                }
                else if (seats[currentY][currentX].Item1 == "occupied") {
                    WriteLine("Deze stoel is bezet");
                }

                Run(); // Waarschijnlijk handig om dit te veranderen, want nu opent ie denk ik een nieuwe run en sluit hem niet dus voor ram geheugen niet zo fijn
            }

            return new Tuple<int, int>(currentX,currentY);
        }
    }
}