using System;

public enum Color { Blauw, Rood, Oranje, Geel, Zwart, Wit, Groen, Roze, Locomotief, Grijs }
namespace T2R.Cards
{
    public class TrainCard : IComparable
    {
        /*instance Variables*/
        private readonly Color Col;      // the color of the train on the card


        /*constructor*/
        public TrainCard(Color Col)
        {
            this.Col = Col;
        }



        /******************************
        Methods
        *******************************/
        /* we use this to sort the traincards to give the player a more clear view of how much cards he has of each type,
        later heb ik besloten om met een dictionary te werken die als value het # kaarten bevat de kleur (key), dus was deze 
        icomparable niet echt nodig, maar het is wel handig om de kaartenop de form alfabetisch te sortern*/
        public int CompareTo(object obj)       
        {
            if (obj is TrainCard)
            {
                return (Convert.ToString(Col).ToUpper().CompareTo(((TrainCard)obj).ToString().ToUpper()));
            }
            else if (obj is string)
            {
                return (Convert.ToString(Col).ToUpper().CompareTo(((string)obj).ToUpper()));
            }
            else
            {
                ExtensionMethods.ShowError("you can't compare a traincard card to any other type than a string or another traincard!");
            }
            return default(int);
        }

        
        public override string ToString()
        {
            return (Convert.ToString(Col));
        }



        /******************************
        Properties
        *******************************/

        public Color getColor()
        {
            return Col;
        }
    }
}
