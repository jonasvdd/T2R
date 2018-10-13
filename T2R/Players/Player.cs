using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using T2R.Cards;
using T2R.Forms;
using T2R.Map;
using T2R.Rounds;

public enum GameElement { DestinationCard, Stations }
namespace T2R.Players
{
    public class Player : IComparable
    {
        /*instance Variables*/
        protected string name;
        protected int points = 0;
        protected System.Drawing.Color color;
        protected int amountOfTrains = 48;
        protected List<Station> stations = new List<Station>();
        protected List<City> stationCities = new List<City>();
        protected List<Railroad> network = new List<Railroad>();
        protected List<AbstrDestCard> DestinationCards = new List<AbstrDestCard>();
        protected Dictionary<TrainCard, int> traincards = new Dictionary<TrainCard, int>();
        private int tempval;


        /*constructor*/
        public Player(string name, System.Drawing.Color color)
        {
            this.name = name;
            this.color = color;
            addStations();
        }



        /******************************
       Methods
       *******************************/

        public void SortPlayerCards(GameElement type)
        {
            switch ((int)type)
            {
                case 0:     // destinationCard
                    DestinationCards.Sort();
                    break;
                case 1:     // stations
                    stations.Sort();
                    break;
            }
        }


        protected void addStations()
        {
            for (int i = 0; i < 3; i++)
            {
                Station station = new Station();
                stations.Add(station);
            }
        }


        internal void addPoints(Railroad road)
        {
            int lenghtOfTrack = road.getRailLength();
            switch (lenghtOfTrack)
            {
                case 1:
                    points += 1;
                    break;
                case 2:
                    points += 2;
                    break;
                case 3:
                    points += 4;
                    break;
                case 4:
                    points += 7;
                    break;
                case 6:
                    points += 15;
                    break;
                case 8:
                    points += 21;
                    break;
                default:
                    Program.logboek.WriteLine("you can't add a track of this lenght");
                    break;
            }
        }


        public virtual int ChoseOption(/*Dictionary<string, int> possibleActions,*/ Possibilities optiontype)
        {
            Dictionary<string, int> roundOptions = new Dictionary<string, int>();
            int val = 0;
            string text = null;
            switch ((int)optiontype)
            {
                case (int)Possibilities.General:
                    foreach (PlayerAction action in Enum.GetValues(typeof(PlayerAction)))
                        roundOptions.Add(action.ToString(), (int)action);
                    break;
                case (int)Possibilities.Traincard:
                    foreach (Cardoptions option in Enum.GetValues(typeof(Cardoptions)))
                        roundOptions.Add(option.ToString(), (int)option);
                    break;
                case (int)Possibilities.visibleTraincards:
                    foreach (TrainCard trCard in ActionsMethods.visibleTraincards)
                    {
                        text = trCard.ToString();
                        val = (int)(trCard.getColor());
                        List<int> vals = roundOptions.Values.ToList();
                        if (!vals.Contains(val)) roundOptions.Add(text, val);
                    }
                    break;
                case (int)Possibilities.stationCities:
                    Program.logboek.WriteLine("Building a station: the available cities to build a station are:");
                    ActionsMethods.citylist.Sort();
                    foreach (City stad in ActionsMethods.citylist)
                    {
                        /******************************
                        Showing the  available stationcities
                        *******************************/
                        if (stad.getStation() == null)
                        {
                            text += stad.getname() + '\t';
                            roundOptions.Add(stad.getname(), stad.getID());
                        }
                    }
                    Program.logboek.WriteLine(text);
                    break;
                case (int)Possibilities.Railroad:
                    City city1, city2 = null;
                    foreach (Railroad road in ActionsMethods.network)
                    {
                        if (!road.isOccupied())
                        {
                            List<int> IDs = roundOptions.Values.ToList();
                            city1 = road.getdestinations()[0];
                            city2 = road.getdestinations()[1];
                            if (!IDs.Contains(city1.getID())) { roundOptions.Add(city1.getname(), city1.getID()); }
                            if (!IDs.Contains(city2.getID())) { roundOptions.Add(city2.getname(), city2.getID()); }
                        }
                    }
                    break;
                case (int)Possibilities.Neighbours:
                    City city3 = ActionsMethods.getCity(tempval);
                    foreach (Railroad road in ActionsMethods.network)
                    {
                        if (road.getdestinations().Contains(city3) && !road.isOccupied())
                        {
                            if (road.getdestinations()[0].getID() == city3.getID())
                            {
                                if (!roundOptions.Keys.Contains(road.getdestinations()[1].getname()))
                                {
                                    roundOptions.Add(road.getdestinations()[1].getname(), road.getdestinations()[1].getID());
                                }
                            }
                            else
                            {
                                if (!roundOptions.Keys.Contains(road.getdestinations()[0].getname()))
                                {
                                    roundOptions.Add(road.getdestinations()[0].getname(), road.getdestinations()[0].getID());
                                }
                            }
                        }
                    }
                    break;

            }
            FormDataObject data = new FormDataObject();
            PlayersOptionsForm form = new PlayersOptionsForm(data, Program.form.gamepics, roundOptions, this);
            form.ShowDialog();
            while (data.getData() == null)
            {
                form.ShowDialog();
            }
            val = (int)data.getData();
            tempval = val;
            foreach (KeyValuePair<string, int> pair in roundOptions)
            {
                if (roundOptions[pair.Key] == val)
                {
                    Program.logboek.WriteLine(string.Format("the player chose {0}, {1}", pair.Value, pair.Key));
                }
            }
            return val;
        }


        public void addPoints(int points)
        {
            this.points += points;
        }


        public void addCard(object card)
        {
            if (card is LongDestCard || card is NormDestCard)
            {
                DestinationCards.Add((AbstrDestCard)card);
            }
            else if (card is TrainCard)
            {
                //traincards.Add((TrainCard)card);
                bool isInDeck = false;
                foreach (KeyValuePair<TrainCard, int> trCard in traincards)
                {
                    if (trCard.Key.getColor() == ((TrainCard)card).getColor())
                    {
                        isInDeck = true;
                        traincards[trCard.Key] += 1;
                        break;
                    }
                }
                if (!isInDeck)
                {
                    traincards.Add((TrainCard)card, 1);
                }
            }
        }


        public void addRailroad(Railroad track)
        {
            track.setOccupied(this);
            amountOfTrains -= track.getRailLength();
            network.Add(track);
        }


        protected string StationsToString()
        {
            string text = null;
            for (int i = 0; i < stations.ToArray().Length; i++)
            {
                text += "\n" + "\t" + stations[i].ToString();
            }
            return text;
        }



        /******************************
        Properties
        *******************************/

        public string GetName()
        {
            return name;
        }


        public List<AbstrDestCard> GetDestinationCards()
        {
            return DestinationCards;
        }


        public List<Railroad> GetNetwork()
        {
            return network;
        }


        public System.Drawing.Color GetColor()
        {
            return color;
        }


        public int GetAmountOfUnplacedStations()
        {
            int unplaced = 0;
            for (int i = 0; i < stations.ToArray().Length; i++)
                if (stations[i].getCity() == null) unplaced++;
            return unplaced;
        }


        public int GetAmountOfUnplacedTrains()
        {
            return amountOfTrains;
        }


        public Dictionary<TrainCard, int> GetTraincards()
        {
            return traincards;
        }


        public bool setStation(City city)
        {
            foreach (Station station in stations)
                if (station.getCity() == null)
                {
                    station.setCity(city, this);
                    city.setStation(station);
                    return true;
                }
            return false;
        }


        public int getPoints()
        {
            return points;
        }


        public override string ToString()
        {
            string text = "\n\t" + name + '\t' + '(' + (color) + ')' + '\n' +
                "punten\n\t" + points + "\nStationnen" + StationsToString() + '\n' +
                "Amount of trains:\n\t" + (amountOfTrains - 8) + '\n' +
                "DestinationCards:" + CardMethods<AbstrDestCard>.getCardValues(DestinationCards) + '\n' +
                "Traincards:" + '\n' + CardMethods<TrainCard>.getAllSortedTrainCards(traincards) + '\n';
            return text;
        }


        public int CompareTo(object obj)
        {
            if (obj is Player /*|| obj is IntelligentPlayer*/)
            {
                return (this.getPoints().CompareTo(((Player)obj).getPoints()));
            }
            else
            {
                MessageBox.Show("You tried to compare to antother type than a player" +
                    "\nYou can only compare players to eachother");
                throw new NotImplementedException();
            }
        }


        public void CheckforInvisbleRailroads()
        {
            ExtensionMethods.niceLayout("checking for some 'vitural' railroads to be made with stations");
            City city;
            foreach (Station station in stations) // max 3
            {
                if (station.getCity() != null)
                {
                    city = station.getCity();
                    foreach (City stad in city.getNeighBours()) // itereren over de buren (max 6)
                    {
                        foreach (Railroad road in network)      // iterren over de sporen fan de speler (max 30) 
                        {
                            if (road.getdestinations()[0].getID() == stad.getID() || road.getdestinations()[1].getID() == stad.getID())
                            {
                                // controlestructuur of er al een spoor toegevoegd is
                                Railroad spoor = new Railroad(stad, city);
                                network.Add(spoor);
                                Program.logboek.WriteLine(string.Format("virtueel spoor van {0} naar {1}", city.getname(), stad.getname()));
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
