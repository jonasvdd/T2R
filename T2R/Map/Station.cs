using System;
using T2R.Players;

namespace T2R.Map
{
    public class Station    : IComparable
    {
        /*Instance Variables*/
        private City city;
        private Player player;
        


        /******************************
       Methods
       *******************************/

        public override string ToString()
        {
            if (city == null)
            {
                return "not added to a city";
            }
            else
            {
                return city.getname();
            }
        }


        public int CompareTo(object obj)
        {
            if (obj is City)
            {
                return city.CompareTo((City)obj);
            }
            else if (obj is string)
            {
                return (city.getname().CompareTo((string)obj));
            }
            return 1;
        }



        /******************************
        Properties
        *******************************/

        public void setCity(City city, Player player)
        {
            this.city = city;
            this.player = player;
        }


        public City getCity()
        {
            return city;    // als city == null --> nog niet geplaatst
        }


        public Player getPlayer()
        {
            return player;
        } 
    }
}
