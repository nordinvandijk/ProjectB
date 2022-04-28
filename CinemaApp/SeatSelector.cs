using System;
using static System.Console;

namespace CinemaApp
{
    class SeatSelector
    {
        public int currentX;
        public int currentY;
        public Tuple<string,double>[][] seats = new Tuple<string,double>[2][];
        int SelectedOption;


        public SeatSelector() {
            currentX = 0;
            currentY = 0;
            seats[0] = new Tuple<string,double>[3];
            seats[0][1] = new Tuple<string,double>("test", 1.00);
            seats[0][2] = new Tuple<string,double>("test", 1.00);
            seats[0][0] = new Tuple<string,double>("test", 1.00);
            seats[1] = new Tuple<string,double>[3];
            seats[1][1] = new Tuple<string,double>("test", 1.00);
            seats[1][2] = new Tuple<string,double>("test", 1.00);
            seats[1][0] = new Tuple<string,double>("test", 1.00);
        }

        public void Display() {
            for (int y = 0; y < seats.Length; y++) {
                ResetColor();
                Write("Rij " + y + " ");
                for (int x = 0; x < seats[y].Length; x++) {
                    if (currentX == x && currentY == y) {
                        Write(seats[y][x].ToString(), BackgroundColor = ConsoleColor.Red);
                    }
                    else {
                        ResetColor();
                        Write(seats[y][x].ToString());
                    }
                }
                Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                Display();
                
                ConsoleKeyInfo keyInfo = ReadKey(true); //ReadKey geeft ConsoleKeyInfo, onder andere welke key ingedrukt is
                keyPressed = keyInfo.Key; //keyInfo.Key pakt uit de keyInfo welke key er ingedrukt is
                
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W) //checkt of up arrow geklikt wordt
                {
                    currentY--;
                    if (currentY < 0) {
                        currentY = 0;
                    }
                }
                if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
                {
                    currentY++;
                }
                if (keyPressed == ConsoleKey.RightArrow || keyPressed == ConsoleKey.D)
                {
                    currentX++;
                }
                if (keyPressed == ConsoleKey.LeftArrow || keyPressed == ConsoleKey.A)
                {
                    currentX--;
                    if (currentX < 0) {
                        currentX = 0;
                    }
                }
            } while(keyPressed != ConsoleKey.Enter);
            
            return SelectedOption; //returnt de geselecteerde optie als enter wordt gekilkt
        }

    }  
}