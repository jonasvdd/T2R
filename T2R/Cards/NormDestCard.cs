using T2R.Map;

namespace T2R.Cards
{
    public class NormDestCard : AbstrDestCard
    {
        /*constructor*/
        // aangezien dit overerft van abstredestinationcard verkrijgt dit alle eigenschappen van abstrdestinationcard
        // en zal dit dus ook icomparable zijn en kan men dus ook deze kaarten sorteren op punten 
        public NormDestCard(City city1, City city2, int points)
        {
            this.city1 = city1;
            this.city2 = city2;
            this.points = points;
        }
    }
}
