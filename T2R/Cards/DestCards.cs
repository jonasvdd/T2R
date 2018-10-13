using System;
using T2R.Map;

namespace T2R.Cards
{
    public abstract class AbstrDestCard :   /*abstract destination card*/  IComparable
    {
        /*instance Variables*/
        /*er zijn 2 types destinationcards, elke type heeft deze variabelen gemeen*/
        // voor een mooier overzicht in de logfile hebben we ze ook icomparable gemaakt zodat we ze kunnen sorteren op de punten.
        protected int points;
        protected City city1;
        protected City city2;



        /******************************
        Methods
        *******************************/

        public void getCities(out City a, out City b)
        {
            a = city1;
            b = city2;
        }


        public override string ToString()
        {
            return (string.Format("\n\t{0} - {1}: {2} points", city1.getname(), city2.getname(), points));
        }


        public int getPoints()
        {
            return points;
        }


        public int CompareTo(object obj)
        {
            if (obj is AbstrDestCard)
            {
                return (((AbstrDestCard)obj).getPoints().CompareTo(points));
            }
            else {
                ExtensionMethods.ShowError("you can't compare a destination card to any other type than a destination card!");
            }
            return default(int);
        }
    }
}

