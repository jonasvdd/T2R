using T2R.Map;

namespace T2R.Cards
{
    public class LongDestCard : AbstrDestCard
    {
        /*constructor*/
        public LongDestCard(City city1, City city2, int points)
        {
            this.city1 = city1;
            this.city2 = city2;
            this.points = points;
        }
    }
}
