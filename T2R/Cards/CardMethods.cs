using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using T2R.Players;

namespace T2R.Cards
{
    public static class CardMethods<T> where T : IComparable
    {

        /******************************
        Deviding The Cards
        *******************************/

        public static void devideCards(List<Player> players, List<LongDestCard> longdestcards, List<NormDestCard> normDestCards, List<TrainCard> traincards)
        {
            // we gaan ervan uit dat de kaarten goed geshuffled zijn
            foreach (Player player in players)
            {
                player.addCard(CardMethods<LongDestCard>.pickAndRemoveCardsFromDeck(longdestcards));       // each player gets a long distance card
                for (int i = 0; i < 2; i++)                                     // each player gets a normal distance card
                {
                    player.addCard(CardMethods<NormDestCard>.pickAndRemoveCardsFromDeck(normDestCards));
                }
                for (int j = 0; j < 6; j++)                                     // each player gets 6 traincards 
                {
                    player.addCard(CardMethods<TrainCard>.pickAndRemoveCardsFromDeck(traincards));
                }
                player.SortPlayerCards(GameElement.DestinationCard);
            }
        }



        /******************************
        ShuffleCards
        *******************************/

        public static void shuffle(List<T> data)
        {
            Random rand = new Random();
            int p, q;
            int length = data.Count;
            for (int i = 0; i < length * 4; i++)
            {
                p = rand.Next(0, length/* - 1*/);
                q = rand.Next(0, length/* - 1*/);
                swapData(data, p, q);
            }
        }


        private static void swapData(IList<T> data, int p, int q)
        {
            T temp1;
            temp1 = data[p];
            data[p] = data[q];
            data[q] = temp1;
        }



        /******************************
        Cardmethods while Playing the game
        *******************************/

        public static string getCardValues(List<T> cards)
        {
            if (cards[0] is TrainCard || cards[0] is TrainCard)
            {
                cards.Sort();
            }
            string text = null;
            for (int i = 0; i < cards.ToArray().Length; i++)
            {
                text += '\t' + cards[i].ToString();
            }
            return text;
        }


        public static T pickAndRemoveCardsFromDeck<T>(List<T> card, int index = -1) where T : IComparable
        {
            if (index < 0) /*default*/
            {
                index = card.ToArray().Length - 1;
            }

            T picked = card[index];
            card.RemoveAt(index);
            return picked;
        }


        public static void swapCardFromDeck<T>(List<T> FromDeckt, List<T> ToDeck)
        {
            if (FromDeckt.Count > 0)
            {
                T picked = FromDeckt[FromDeckt.ToArray().Length - 1];
                FromDeckt.RemoveAt(FromDeckt.ToArray().Length - 1);
                ToDeck.Add(picked);
            }
            else
            {
                MessageBox.Show(("het spel wordt beïndigd, de spelers hebben teveel kaarten bij zich"));
                Environment.Exit((0));
            }

        }

        public static string getAllSortedTrainCards(Dictionary<T, int> dict)
        {
            var list = dict.Keys.ToList();
            list.Sort();
            string text = null;
            foreach (KeyValuePair<T, int> data in dict)
            {
                for (int i = 0; i < data.Value; i++)
                {
                    text += "\t" + data.Key.ToString();
                }
            }
            return text;
        }


    }
}
