using System.Collections.Generic;
using T2R.Map;
using T2R.Cards;
using T2R.Forms;
using T2R.Rounds;

namespace T2R.Players
{
    public class IntelligentPlayer : Player
    {
        private List<City> citylist;
        private List<Railroad> AllTracks;
        //private Dictionary<TrainCard, int> neededCards;
        private List<Railroad> goalRoute = new List<Railroad>();
        public Railroad Maderoad { private set; get; }
        private Railroad goalRoad;
        private City goalStation;
        public bool firstloop = true;

        public IntelligentPlayer(string name, System.Drawing.Color color, List<City> citylist, List<Railroad> network) : base(name, color)
        {
            this.name = name;
            this.color = color;
            this.citylist = citylist;
            this.AllTracks = network;
            //addStations();
        }

        public override int ChoseOption(Possibilities optionType)
        {
            int val = 0;
            switch ((int)optionType)
            {
                case (int)Possibilities.General:
                    val = IntelligentGeneralChoice();
                    break;
                case (int)Possibilities.Neighbours:
                    val = goalRoad.getdestinations()[1].getID();
                    break;
                case (int)Possibilities.Railroad:
                    val = goalRoad.getdestinations()[0].getID();
                    break;
                case (int)Possibilities.stationCards:
                    val = goalStation.getID();
                    break;
                case (int)Possibilities.stationCities:
                    val = goalStation.getID();
                    return val;
                case (int)Possibilities.leastImportantTraincard:
                    foreach (KeyValuePair<TrainCard, int> card in traincards){val = (int)card.Key.getColor();}
                    return val;
                case (int)Possibilities.visibleTraincards:
                    if (firstloop)
                    {
                        foreach (TrainCard card in ActionsMethods.visibleTraincards)
                        {
                            if (goalRoad != null)
                            {
                                if (goalRoad.getColor() != Color.Grijs && card.getColor() == goalRoad.getColor())
                                {
                                    val = (int)goalRoad.getColor();
                                    return val;
                                }
                            }
                            else if (card.getColor() == Color.Locomotief)
                            {
                                return (int)Color.Locomotief;
                            }
                        }
                    }
                    else
                    {
                        foreach (TrainCard card in ActionsMethods.visibleTraincards)
                        {
                            val = (int)card.getColor();
                            return val;
                        }
                    }
                    firstloop = false;
                    break;
            }


            return val;
        }


        private int IntelligentGeneralChoice()
        {
            int i = 0;
            if (this.DestinationCards.Count < 3)
            {
                return (int)PlayerAction.DestinationCard;
            }
            if (firstloop)
            {
                ExtensionMethods.niceLayout("Dijkstra");
                double mini = 0;
                firstloop = false;
                List<List<Railroad>> shortestpaths = GraphAlgorithms.Dijkstra(AllTracks, citylist, this);
                List<City> destinations = new List<City>();
                ExtensionMethods.niceLayout("Vertaling naar railRoads");
                foreach (List<Railroad> path in shortestpaths)
                {
                    Program.logboek.WriteLine(this.GetDestinationCards()[i].ToString());
                    int totalamountOfCardsneeded = 0, amountOfStations = 0;
                    foreach (Railroad road in path)
                    {
                        destinations = road.getdestinations();
                        Program.logboek.WriteLine("\t" + destinations[0].getname() + " - " + destinations[1].getname());
                        if (!road.isOccupied())
                        {
                            totalamountOfCardsneeded += road.getAmountOfLocomotives() * 2 + road.getRailLength()
                                + ((road.IsTunnel()) ? 2 : 0);
                        }
                        else { amountOfStations++; }
                    }
                    double trackvalue = (double)(GetDestinationCards()[i].getPoints()
                        + 4 - amountOfStations) / (double)(totalamountOfCardsneeded);
                    // empyrisch, hoe groter hoe beter 
                    if (trackvalue > mini) { goalRoute = path; mini = trackvalue; }
                    i++;

                }
                i = 1;
                foreach (Railroad road in goalRoute)
                {
                    if (road.isOccupied() && road.getPlayer().CompareTo(this) != 0)
                    {
                        if (i / goalRoute.Count > 0.5)
                        {
                            if (road.getdestinations()[0].getStation() == null)
                            {
                                goalStation = road.getdestinations()[0];
                                return (int)PlayerAction.Station;
                            }
                            else if((road.getdestinations()[1].getStation() == null))
                            {
                                goalStation = road.getdestinations()[1];
                                return (int)PlayerAction.Station;
                            }
                        }
                    }

                    else if (road.isOccupied() == false)
                    {
                        Program.logboek.WriteLine(this.name + " is trying to build a road between: " + road.ToString());
                        if (ActionsMethods.Pickcards(this, road))
                        {
                            //goalRoad = road;
                            Maderoad = road;
                            return (int)PlayerAction.Railroad;
                        }
                    }
                    i++;
                }
                foreach (Railroad road in goalRoute)
                {
                    //goalRoad = road;
                    if (!road.isOccupied()){goalRoad = road;}
                }
            }
            else 
            {
                return (int)PlayerAction.Traincards;
            }
            return (int) PlayerAction.Traincards;
        }


        // we gaan eerst controleren of ze gemeenschappelijke kortste paden bevatten
        private static void CheckifCommon(List<Railroad> a, List<Railroad> b, Player player, List<Railroad> commonList)
        {
            foreach (Railroad road in a)
            {
                // indien de road 
                if (b.Contains(road) && !road.isOccupied())
                {
                    commonList.Add(road);
                }
            }
        }
    }
}
