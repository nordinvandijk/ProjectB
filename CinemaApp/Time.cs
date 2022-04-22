using System;
using static System.Console;

namespace CinemaApp
{
    class Time
    {
        public int hours;
        public int minutes;
        public Application App;

        public Time(Application app){
            App = app;
        }

        public bool CheckTimeOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2){
            if (start1 >= start2 && start1 <= end2){
                return true;
            }
            else if (end1 >= start2 && end1 <= end2){
                return true;
            }
            else if (start2 >= start1 && start2 <= end1){
                return true;
            }
            else if (end2 >= start1 && end2 <= end1){
                return true;
            }
            else {
                return false;
            }
        }
    }
}