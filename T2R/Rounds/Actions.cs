using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2R.Map;
using T2R.Cards;
using T2R.Players;
using T2R.Forms;
using System.IO;
using System.Windows.Forms;


namespace T2R.Rounds
{
    public static class ActionsMethods
    {
        public static List<TrainCard> visibleTraincards;
        public static List<City> citylist;
        private static Random rand = new Random();
        internal static List<Railroad> network;
        internal static List<TrainCard> usedCardsDeck = new List<TrainCard>();

        public static bool PickNormDestinationCard(List<NormDestCard> destinationcard, Player player)
        {
            NormDestCard card = CardMethods<AbstrDestCard>.pickAndRemoveCardsFromDeck(destinationcard);
            player.addCard(card);
            Program.logboek.WriteLine(player.GetName() + " picked:\t" + card.ToString());
            if (!(player is IntelligentPlayer)) { MessageBox.Show(string.Format("You picked:\n\t" + card.ToString())); }
            return true;
        }   // ez done

        public static bool BuildStation(Player player, List<City> cityList)
        {
            if (player.GetAmountOfUnplacedStations() > 0)                                // indien er nog ongeplaatste stationnen zijn
            {
                /******************************
                checking if the player has enough cards of the same type
                *******************************/
                citylist = cityList;
                Dictionary<string, int> possibilities = new Dictionary<string, int>();
                TrainCard tCard = null;
                int amountOfsameCardsNeeded = 4 - player.GetAmountOfUnplacedStations();     // # kaarten van het zelfde type dat nodig is
                Program.logboek.WriteLine(string.Format("you need {0} card{1} of the same type", amountOfsameCardsNeeded,
                       ((4 - player.GetAmountOfUnplacedStations()) > 1) ? "s" : ""));
                Dictionary<TrainCard, int> viableCards = goodCards(player, amountOfsameCardsNeeded);

                if (viableCards.Count > 0)                                  // indien de speler genoeg kaarten heeft
                {
                    Program.logboek.WriteLine("These are The Posibilities");
                    string a = null;
                    foreach (KeyValuePair<TrainCard, int> de in viableCards)
                    {
                        a += "\t" + de.Key.ToString();
                        possibilities.Add(de.Key.ToString(), (int)(de.Key.getColor()));
                    }
                    Program.logboek.WriteLine(a);                               // de mogelijke kaarten worden uitgeprint

                    /******************************
                    player input
                    *******************************/
                    ExtensionMethods.niceLayout("asking input");
                    if (player is IntelligentPlayer)
                    {
                        tCard = new TrainCard((Color)((IntelligentPlayer)player).ChoseOption(Possibilities.leastImportantTraincard));
                    }
                    else
                    {
                        int cardval = GetInput(possibilities, player);
                        tCard = new TrainCard((Color)cardval);
                    }

                    /******************************
                    removing the cards from the players 
                    *******************************/
                    foreach (KeyValuePair<TrainCard, int> card in player.GetTraincards())
                    {
                        if (card.Key.getColor() == tCard.getColor())
                        {
                            // gaan we ervan uitgaan dat hij alle kaarten gebruikt indien hij er genoeg heeft
                            if (card.Value >= amountOfsameCardsNeeded)
                            {
                                TrainCard trCard = card.Key;
                                for (int i = 0; i < card.Value; i++) { usedCardsDeck.Add(trCard); }
                                player.GetTraincards()[card.Key] -= amountOfsameCardsNeeded;
                                break;
                            }

                            else if (card.Value < amountOfsameCardsNeeded)
                            {
                                foreach (KeyValuePair<TrainCard, int> trcard in player.GetTraincards())
                                {
                                    if (trcard.Key.getColor() == Color.Locomotief)
                                    {
                                        amountOfsameCardsNeeded -= player.GetTraincards()[card.Key];
                                        player.GetTraincards()[card.Key] = 0;
                                        TrainCard kaart = trcard.Key;
                                        player.GetTraincards()[trcard.Key] -= amountOfsameCardsNeeded;
                                        for (int i = 0; i < amountOfsameCardsNeeded; i++)
                                        {
                                            usedCardsDeck.Add((kaart));
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }


                    /******************************
                    Player input for city name
                    *******************************/
                    int val = 0;
                    val = player.ChoseOption(Possibilities.stationCities);
                    City stad = getCity(val);
                    if (stad != null)
                    {
                        player.setStation(stad);
                        stad.DrawCircle(player, Program.form.pictureBox1);
                        return true;
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    Program.logboek.WriteLine("You don't have enough cards of the same cards");
                    if (!(player is IntelligentPlayer))
                    {
                        MessageBox.Show(string.Format("You don't have enough cards of the same type," +
                 "\n\t you need atleast {0} cards of the same type", amountOfsameCardsNeeded));
                    }
                }
            }
            else
            {
                Program.logboek.WriteLine("this is impossible to do, you don't have any stations left!");
                if (!(player is IntelligentPlayer)) { MessageBox.Show("this is impossible to do, since you don't have any stations left!"); }
            }
            return false;
        }


        public static bool PickTrainCards(Player player, List<TrainCard> deck, List<TrainCard> visibleDeck,
            int roundNumber, List<foto> gamePics, List<TrainCard> favourableCards = null)
        {
            // visibleTraincards = visibleDeck;
            /******************************
            playerInput
            *******************************/
            int i = 0, val = 0;
            TrainCard card = null;
            bool Locomotive = false;
            while (Locomotive == false && i < 2)
            // de spelers mogen 2 kaarten nemen (tenzij ze een locomotief nemen in de eerste ronde
            {
                Program.logboek.WriteLine("");
                Program.logboek.WriteLine("1.\tPick an invisible card");
                Program.logboek.WriteLine("2.\tPick a visible card");
                val = player.ChoseOption(Possibilities.Traincard);    // geïmplementeerd bij intelligente speler

                switch (val)
                {
                    case (int)Cardoptions.Invisible:
                        /******************************
                         Onzichtbare kaart
                        *******************************/
                        card = CardMethods<TrainCard>.pickAndRemoveCardsFromDeck(deck);
                        player.addCard(card);
                        Program.logboek.WriteLine(string.Format(
                            "{0} picked an invisible card, that was a {1} traincard", player.GetName(), card.ToString()));
                        if (!(player is IntelligentPlayer))
                        {
                            MessageBox.Show(string.Format("{0} picked an invisible card, that was a {1} traincard",
                              player.GetName(), card.ToString()));
                        }
                        break;

                    case (int)Cardoptions.Visible:
                        /******************************
                        Zichtbare kaart
                        *******************************/
                        Program.logboek.WriteLine("Please type the name of the card you want to pick");
                        bool cardFound = false;
                        while (cardFound == false)
                        {
                            val = player.ChoseOption(Possibilities.visibleTraincards); // override bij intelligente spelers
                            cardFound = true;

                            if (i == 1 && cardFound)
                            // indien geen geneste if krijg ik een fout omdat het object card --> null (nulreferenceException)
                            {
                                if ((Color)val == Color.Locomotief)
                                // indien het de 2de (sub) beurt is en de gevonden kaart geen locomotief is
                                {
                                    Program.logboek.WriteLine("you can't pick an locomotive as a visible card");
                                    if (!(player is IntelligentPlayer))
                                    {
                                        MessageBox.Show("you can't pick an locomotive as a visible card, in you second turn");
                                        cardFound = false;
                                    }

                                }
                            }

                            if (cardFound)
                            {
                                cardFound = false;
                                foreach (TrainCard tCard in visibleDeck)
                                {
                                    /******************************
                                    swapping card form dack
                                    *******************************/
                                    if (tCard.getColor() == (Color)val)
                                    {
                                        card = CardMethods<TrainCard>.pickAndRemoveCardsFromDeck(visibleDeck, visibleDeck.IndexOf(tCard));
                                        CardMethods<TrainCard>.swapCardFromDeck(deck, visibleDeck);
                                        player.addCard(card);
                                        Program.logboek.WriteLine("valid");
                                        cardFound = true;
                                        Program.form.refreshVisibleTrainCards(visibleDeck);
                                        //Round.Header(visibleDeck, roundNumber, player);
                                        if (tCard.ToString() == Convert.ToString(Color.Locomotief)) Locomotive = true;
                                        break; /*sorrynotsorry*/
                                    }
                                }
                                if (!cardFound)
                                {
                                    Program.logboek.WriteLine("the card you selected isn't available on the visible deck");
                                    if (!(player is IntelligentPlayer)) { MessageBox.Show("the card you selected isn't available on the visible deck"); }
                                }
                            }
                        }
                        break;
                }
                i++;
            }
            return true;
        }       // done


        public static bool BuildRailRoad(Player player, List<City> cities, List<Railroad> totalNetwork)
        {
            citylist = cities;
            City city1, city2;
            int val = 0;
            network = totalNetwork;
            string text = null;
            if (!(player is IntelligentPlayer))
            {
                foreach (City city in cities)
                {
                    text += (city.getname() + "\t");
                }
            }

            Console.WriteLine("Choose between which 2 Cities you want to add an track (give in 1 city at a time) +\n" + text);

            //getting input from console
            if (!(player is IntelligentPlayer))
            {
                val = player.ChoseOption(Possibilities.Railroad);
                city1 = getCity(val);
                val = player.ChoseOption(Possibilities.Neighbours);
                city2 = getCity(val);
                if (city1 != city2)
                {
                    if (city1.getNeighBours().Contains(city2))
                    {
                        List<Railroad> possibleRoads = new List<Railroad>();
                        // alle mogelijke tracks tussen de 2 steden
                        Railroad track = null;

                        foreach (Railroad road in totalNetwork)
                        {
                            if (road.getdestinations().Contains(city1) && road.getdestinations().Contains(city2))
                            {
                                if (!possibleRoads.Contains(road))  // heb er per ongeluk sommige steden dubbel toegevoegd, heb dit ontdekt bij het debuggen van de code
                                                                    // een testgeoriënteerd ontwerp (met testklassen) zou hier zeker van pas komen
                                {
                                    track = road;
                                    possibleRoads.Add(road);
                                }
                            }
                        }

                        if (possibleRoads.Count > 1)    // bij parallelle steden 
                        {
                            Dictionary<string, int> roundOptions = new Dictionary<string, int>();
                            Program.logboek.WriteLine("er is meer dan 1 mogelijkheid");
                            //roundOptions.Clear();
                            foreach (Railroad road in possibleRoads)
                            {
                                roundOptions.Add(Convert.ToString(road.getColor()), (int)road.getColor());
                            }

                            val = GetInput(roundOptions, player);
                            foreach (Railroad road in totalNetwork)
                            {
                                if (road.getdestinations().Contains(city1) && road.getdestinations().Contains(city2))
                                    if (road.getColor() == (Color)val)
                                    {
                                        track = road;
                                    }
                                    else
                                    {
                                        totalNetwork.Remove(road);
                                        break;
                                    }
                            }
                        }

                        Program.logboek.WriteLine("there is a track between the two cities");
                        Program.logboek.WriteLine(track.ToString());
                        bool succesful = Pickcards(player, track);
                        if (succesful)
                        {
                            player.addRailroad(track);
                            player.addPoints(track);
                            track.drawLine(player, Program.form.pictureBox1);
                        }
                        return succesful;
                    }
                    else
                    {
                        Console.WriteLine("The cities you picked aren't neighbours");
                        if (!(player is IntelligentPlayer)) { MessageBox.Show("the cities you picked aren't neighbours"); }
                    }
                }
                else
                {
                    Console.WriteLine("You picked twice the same city");
                    if (!(player is IntelligentPlayer)) { MessageBox.Show("you picked twice the same city"); }
                }
                return false;
            }
            else
            {
                player.addRailroad(((IntelligentPlayer)player).Maderoad);
                player.addPoints(((IntelligentPlayer)player).Maderoad);
                ((IntelligentPlayer)player).Maderoad.drawLine(player, Program.form.pictureBox1);
                return true;
            }
        }


        public static bool Pickcards(Player player, Railroad road)
        {

            Dictionary<TrainCard, int> traincards = player.GetTraincards();
            {
                int amountOfLocomotives = road.getAmountOfLocomotives();
                int railLength = road.getRailLength() - amountOfLocomotives;
                Color kleur = road.getColor();
                bool hasCard = false;
                int /*temp = 0,*/ maxamount = 0;
                Console.WriteLine("this track needs {0} locomotives", amountOfLocomotives);
                if (amountOfLocomotives > 0)
                {
                    foreach (KeyValuePair<TrainCard, int> playercard in traincards)        // met index werken?
                    {
                        if (playercard.Key.getColor() == Color.Locomotief)
                        {
                            hasCard = true;
                            if (playercard.Value >= amountOfLocomotives)
                            {
                                Console.WriteLine("you have enough locomotives");
                                player.GetTraincards()[playercard.Key] -= amountOfLocomotives;
                                TrainCard card = playercard.Key;
                                for (int i = 0; i < amountOfLocomotives; i++) { usedCardsDeck.Add(card); }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("you don't have enough locomotives to make the track");
                                if (!(player is IntelligentPlayer)) { MessageBox.Show("you don't have enough locomotives to make the track"); }
                                return false;
                            }
                        }
                    }
                    if (!hasCard)
                    {
                        Console.WriteLine("you dont have any locomotives");
                        if (!(player is IntelligentPlayer)) { MessageBox.Show("you don't have enough locomotives to make the track"); }
                        return false;
                    }
                }


                // de maximumwaarde wordt gezocht 
                int amountLoco = 0;
                foreach (KeyValuePair<TrainCard, int> card in traincards)
                {
                    if (card.Value > maxamount && card.Key.getColor() != Color.Locomotief)
                    {
                        maxamount = card.Value;
                    }
                    else if (card.Key.getColor() == Color.Locomotief)
                    {
                        amountLoco = card.Value;
                    }
                }
                maxamount += amountLoco;

                if (railLength > 0)
                {
                    if (railLength <= maxamount)
                    {
                        bool colorfound = false;
                        if (kleur == Color.Grijs)
                        {
                            TrainCard traincard = null;
                            Console.WriteLine("Please chose a color");
                            //bool firsttime = true;
                            string a = null;
                            Dictionary<TrainCard, int> possibleCard = goodCards(player, railLength);
                            /*misschien beter een dictionary van object int maken*/
                            Dictionary<string, int> possiblities = new Dictionary<string, int>();
                            foreach (KeyValuePair<TrainCard, int> de in possibleCard)
                            {
                                a += "\t" + de.Key.ToString();
                                possiblities.Add(de.Key.ToString(), (int)de.Key.getColor());
                            }
                            Console.WriteLine(a);
                            if (!(player is IntelligentPlayer))
                            {
                                int val = GetInput(possiblities, player);
                                traincard = new TrainCard((Color)val);
                            }
                            else
                            {
                                traincard = new TrainCard((Color)possiblities.Values.ToList()[0]);
                                // sorry, heb hiervoor geen intelligentie meer geïmplementeerd
                            }
                            foreach (KeyValuePair<TrainCard, int> card in traincards)
                            {
                                if (card.Key.getColor() == traincard.getColor())
                                {
                                    if (railLength > card.Value)
                                    {
                                        int temp = card.Value;
                                        railLength -= temp;
                                        TrainCard tCard = card.Key;
                                        for (int i = 0; i < temp; i++) { usedCardsDeck.Add(tCard); }
                                        traincards[card.Key] = 0;
                                        foreach (KeyValuePair<TrainCard, int> trCard in traincards)
                                        {
                                            if (trCard.Key.getColor() == Color.Locomotief)
                                            {
                                                traincards[trCard.Key] -= railLength;
                                                tCard = trCard.Key;
                                                for (int i = 0; i < railLength; i++) { usedCardsDeck.Add(tCard); }
                                                return true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        traincards[card.Key] -= railLength;
                                        traincard = card.Key;
                                        for (int i = 0; i < railLength; i++) { usedCardsDeck.Add(traincard); }
                                        return true;
                                    }
                                }
                            }
                        }

                        else
                        {
                            // indien de kleur van het spoort vasligt
                            foreach (KeyValuePair<TrainCard, int> card in traincards)
                            {
                                if (card.Key.getColor() == kleur)
                                {
                                    colorfound = true;
                                    // indien je voldoende kaarten hebt van die kleur (zonder locomotieven bij te leggen)
                                    if (card.Value >= railLength)
                                    {
                                        traincards[card.Key] -= railLength;
                                        player.addRailroad(road);
                                        TrainCard trCard = card.Key;
                                        for (int i = 0; i < railLength; i++) { usedCardsDeck.Add(trCard); }
                                        return true;
                                    }
                                    else
                                    {
                                        foreach (KeyValuePair<TrainCard, int> pair in traincards)
                                        {
                                            if (pair.Key.getColor() == Color.Locomotief)
                                            {
                                                int val = traincards[card.Key];
                                                traincards[card.Key] = 0;
                                                traincards[pair.Key] -= (railLength - val);
                                                TrainCard trCard = card.Key;
                                                for (int i = 0; i < railLength - val; i++) { usedCardsDeck.Add(trCard); }
                                                return true;
                                            }
                                        }
                                    }
                                }
                                else if (amountLoco >= railLength)
                                {
                                    foreach (KeyValuePair<TrainCard, int> pair in traincards)
                                    {
                                        if (pair.Key.getColor() == Color.Locomotief)
                                        {
                                            traincards[pair.Key] -= (railLength);
                                            TrainCard trCard = pair.Key;
                                            for (int i = 0; i < railLength; i++) { usedCardsDeck.Add(trCard); }
                                            return true;
                                        }
                                    }
                                }

                            }
                            return false;
                            if (!colorfound)
                            {
                                if (amountLoco >= railLength)
                                {
                                    foreach (KeyValuePair<TrainCard, int> card in traincards)
                                    {
                                        traincards[card.Key] -= amountLoco;
                                        TrainCard trCard = card.Key;
                                        for (int i = 0; i < railLength; i++) { usedCardsDeck.Add(trCard); }
                                        return true;
                                    }
                                }
                                Console.WriteLine("U heeft geen kaarten van dit type kleur");
                                if (!(player is IntelligentPlayer)) { MessageBox.Show("you dont have any cards of this type color"); }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("you dont have enough cards of the same type to build the track");
                        if (!(player is IntelligentPlayer)) { MessageBox.Show("you dont have enough cards of the same type to build the track"); }
                    }
                }
                else
                {
                    player.addRailroad(road);
                    Console.WriteLine("The railroad {0} was successfully added", road.ToString());
                    ExtensionMethods.WaitForUser();
                    return true;
                }
                //ExtensionMethods.WaitForUser();
                returnLocomotives(traincards, amountOfLocomotives);
                return false;
            }
        }



        /******************************
        additional methods
        *******************************/

        public static City getCity(int ID)
        {
            foreach (City city in citylist)       // is er niet ergense een methode getcity
            {
                if (city.getID() == ID) return city;
            }
            return null;
        }


        public static int GetInput(Dictionary<string, int> options, Player player)
        {
            FormDataObject data = new FormDataObject();
            PlayersOptionsForm form = new PlayersOptionsForm(data, Program.form.gamepics, options, player);
            while (data.getData() == null)
            {
                form.ShowDialog();
            }
            int val = (int)data.getData();
            return val;
        }


        private static void returnLocomotives(Dictionary<TrainCard, int> traincards, int amountOfLocomotives)
        {
            if (amountOfLocomotives > 0)
            {
                // de locomotieven worden teruggegeven
                foreach (KeyValuePair<TrainCard, int> card in traincards)
                {
                    if (card.Key.getColor() == Color.Locomotief)
                    {
                        traincards[card.Key] += amountOfLocomotives;
                        break;
                    }
                }

                while (amountOfLocomotives > 0)
                {
                    foreach (TrainCard card in usedCardsDeck)
                    {
                        if (card.getColor() == Color.Locomotief)
                        {
                            amountOfLocomotives--;
                            usedCardsDeck.Remove(card);
                            break;
                        }
                    }
                }

            }
        }




        private static Dictionary<TrainCard, int> goodCards(Player player, int amount)
        {
            int temp = amount;
            int[] cards = new int[9]; // there are 8 different kinds of cards
            foreach (KeyValuePair<TrainCard, int> card in player.GetTraincards())
            {
                if (card.Key.getColor() == Color.Locomotief) { temp -= card.Value; }
            }

            if (temp <= 0)     // aangezien amount > 0 zijn er niet genoeg locomotiefkaarten. 
                temp = 1;

            Dictionary<TrainCard, int> goodcards = new Dictionary<TrainCard, int>();
            foreach (KeyValuePair<TrainCard, int> card in player.GetTraincards())
            {
                if (card.Key.getColor() != Color.Locomotief)
                {
                    if (card.Value >= temp) { goodcards.Add(card.Key, card.Value); }
                }
            }
            return goodcards;
        }


        internal static void CheckSizeOfMainDeck(List<TrainCard> Deck)
        {
            if (Deck.Count < 5)                                      // we add the used cards to the deck and shuffle the deck if the deck gets bigger 
            {
                while (usedCardsDeck.Count > 5)
                {
                    CardMethods<TrainCard>.swapCardFromDeck(usedCardsDeck, Deck);
                }
                CardMethods<TrainCard>.shuffle(Deck);
            }
        }

    }
}

