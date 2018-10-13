using System;
using System.Collections.Generic;
using System.Linq;
using T2R.Cards;
using T2R.Players;

namespace T2R.Map
{
    public static class GraphAlgorithms
    {
        // properties
        private static List<City> cities = new List<City>();
        private static List<Railroad> subNetwork = new List<Railroad>();



        /******************************
        Breadth first search
        *******************************/

        public static bool BFS(int totalCites, Player player1, City city1, City city2)
        {
            subNetwork = player1.GetNetwork();
            int cityID1 = city1.getID();
            int cityID2 = city2.getID();
            Queue<int> queue = new Queue<int>();

            int pos = city1.getID();
            queue.Enqueue(city1.getID());
            HashSet<int> visited = new HashSet<int>();
            visited.Add(pos);
            while (queue.Count > 0)
            {
                int visitedCity = queue.Dequeue();
                foreach (Railroad roadNet in subNetwork)
                {
                    if (roadNet.getdestinations()[0].getID() == visitedCity & !visited.Contains(roadNet.getdestinations()[1].getID())
                        || roadNet.getdestinations()[1].getID() == visitedCity & !visited.Contains(roadNet.getdestinations()[0].getID()))
                    {
                        if (roadNet.getdestinations()[0].getID() != visitedCity)
                        {
                            queue.Enqueue(roadNet.getdestinations()[0].getID());
                            visited.Add(roadNet.getdestinations()[0].getID());
                        }
                        else
                        {
                            queue.Enqueue(roadNet.getdestinations()[1].getID());
                            visited.Add(roadNet.getdestinations()[1].getID());
                        }
                    }
                }
            }
            if (visited.Contains(cityID2))
            {
                return true;
            }
            return false;
        }


        /******************************
        Methods
        *******************************/

        // step 1: remove all the loops (not necesary here)
        // remove alle bezette edges
        // bereken korste pad
        // remove de parallele edges waarvan je de minste soorten kaarten hebt (enz)

        // creathe shortest path table
        // graaf --> evenveel kolommen als steden
        // evenveel rijen al
        // alle cities hebben een City id
        // jammer genoeg hebben mijn networks dit niet 

        /*rij 1*/
        // we schrijven nul in city 1 en we gaan opzoek naar de kleinste ongemaarkeerde waarde in  eerste rij --> pointer erna toe (in een lijst steken)
        // nu een nieuwe lijn totdat 

        /*rij 2*/
        // minimum value formula 
        // min (DestValue, MarkedValue + Edgeweight)
        // zoek de edge die direct a en b verbind
        // weight city a = 0;
        // destvalue = int.max*2;
        // weight edge a to  city b = 5;
        // min(int.max, markedValue + Edgeweight) = 5;

        // dit doen voor elke edge verbonden aan a;
        // (aan de andere edges kom je niet)
        // nu kleinste waarde uit de 2de rij (buiten B)

        /*rij 3 en verdere*/
        // voor de gemarkeerde comlom gaan we de edges zoeken die direct verbonden zijn met gemarkeerde knoop in de vorige rij;
        // we moeten vertext a niet meer controleren (want is al gemarkeerd)


        // dit wordt doorlopen tot onze final vertex gemarkeerd is;



        /******************************
        Dijkstra
        *******************************/
        public static List<List<Railroad>> Dijkstra(List<Railroad> network, List<City> cities, Player player)
        {
            List<List<Railroad>> shortestTrainTracks = new List<List<Railroad>>();
            // Initialisatie
            foreach (AbstrDestCard card in player.GetDestinationCards())
            {
                /*INITIALIZATIE*/
                City toCity, fromCity, aTemp, btemp;
                card.getCities(out toCity, out fromCity); // Je krijgt de steden van op de kaart terug (dus from en to)
                btemp = fromCity;
                //int indexA = a.GetID();
                //int indexB = b.GetID();

                // een methode vinden voor de edges te vinden;


                int pointer = fromCity.getID();
                List<int[]> shortestPathToA = new List<int[]>();
                List<int> markedvals = new List<int>();       // this will contain the indexes of the marked vals
                markedvals.Add(pointer);


                // de eerste keer wordt de mark van de eerste stad op nul gezet
                // all de rest op oneindig
                int[] bla = new int[cities.Count];
                for (int i = 0; i < bla.Length; i++)
                {
                    if (i == pointer)
                    {
                        bla[i] = 0;
                    }
                    else
                    {
                        bla[i] = int.MaxValue;
                    }
                }

                shortestPathToA.Add(bla);
                int j = 1;
                while (pointer != toCity.getID())
                {
                    // zoek alle buren van de pointer en neem  de minimumwaarde van de pointer en 
                    int[] newiteration = new int[shortestPathToA[j - 1].Length];
                    Array.Copy(shortestPathToA[j - 1], newiteration, shortestPathToA[j - 1].Length);
                    aTemp = getCity(pointer, cities);
                    List<City> neighbours = aTemp.getNeighBours();
                    foreach (City neighbour in neighbours)
                    {
                        Railroad road = getRailRoad(neighbour, aTemp, network);
                        int weight = getWeight(road, player);
                        newiteration[neighbour.getID()] = Math.Min(newiteration[neighbour.getID()], newiteration[pointer] + weight);
                    }
                    shortestPathToA.Add(newiteration);
                    pointer = 0;
                    int tempint = int.MaxValue;
                    for (int k = 0; k < newiteration.Length; k++)
                    // de pointer wordt geplaatst op de volgende minimum waarde
                    // die nog niet gemarkeeerd is
                    {
                        if (!markedvals.Contains(k))
                        {
                            if (newiteration[k] < tempint)
                            {
                                tempint = newiteration[k];
                                pointer = k;
                            }
                        }
                    }
                    markedvals.Add(pointer);
                    j++;
                }
                // nu moet de lijst van de stationnen opgebouwd worden die gebruikt worden om de railroad te bouwen
                shortestTrainTracks.Add(getRailroads(shortestPathToA, markedvals, network, cities, fromCity.getID()));
            }
            return shortestTrainTracks;
        }


        private static List<Railroad> getRailroads(List<int[]> DijkstraTable, List<int> markedVals,
            List<Railroad> railroads, List<City> cities, int targetCityID)
        {
            int j = DijkstraTable.Count - 1;
            int pointer = markedVals[j];
            List<City> travelCities = new List<City>();
            while (pointer != targetCityID)
            {
                if (DijkstraTable[j][pointer] != DijkstraTable[j - 1][pointer])      // de index is dus veranderd
                {
                    travelCities.Add(getCity(pointer, cities));
                    pointer = markedVals[j - 1];
                }
                j--;

            }
            travelCities.Add(getCity(targetCityID, cities));
            Program.logboek.WriteLine(string.Format("U zal {0} steden moeten doorkruisen, voor het kortste pad", travelCities.Count));
            foreach (City stad in travelCities)
            {
                Program.logboek.WriteLine("\t" + stad.getname());
            }
            List<Railroad> bestTracks = new List<Railroad>();
            for (int i = 0; i < travelCities.Count - 1; i++)
            {
                bestTracks.Add(getRailRoad(travelCities[i], travelCities[i + 1], railroads));
            }
            return bestTracks;
        }


        private static Railroad getRailRoad(City a, City b, List<Railroad> network)
        {
            foreach (Railroad road in network)
            {
                if (road.getdestinations().Contains(a) && road.getdestinations().Contains(b))
                {
                    return road;
                }
            }
            return null;
        }


        private static City getCity(int index, List<City> steden)
        {
            foreach (City stad in steden)
            {
                if (stad.getID() == index)
                {
                    return stad;
                }
            }
            return null;
        }


        private static int getWeight(Railroad road, Player player)
        // dynamisch gewicht (het gewicht wordt aangepast aan de hand van welke kaarten de speler in de hand heeft)
        {

            int weight = road.getRailLength();
            int loco = 0;
            Dictionary<TrainCard, int> playerCaards = player.GetTraincards();

            if (road.getColor() != Color.Grijs)
            {
                // we gaan de dictionary sorteren bij key value
                var list = playerCaards.Values.ToList();
                list.Sort();

                List<KeyValuePair<TrainCard, int>> myList = playerCaards.ToList();      // http://stackoverflow.com/questions/289/how-do-you-sort-a-dictionary-by-value

                myList.Sort(delegate (KeyValuePair<TrainCard, int> pair1, KeyValuePair<TrainCard, int> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                });



            }
            foreach (KeyValuePair<TrainCard, int> trainCard in playerCaards)
            {
                if (trainCard.Key.getColor() == road.getColor())
                {
                    weight -= trainCard.Value; // kan onder de nul van
                }
                if (trainCard.Key.getColor() == road.getColor())
                {
                    loco += trainCard.Value;
                }

            }
            if (road.getAmountOfLocomotives() > 0)
            {
                weight += road.getAmountOfLocomotives() - loco;
            }
            if (road.IsTunnel())
            {
                weight += 2;
            }
            if (weight < 0)
            {
                weight = 0;
            }
            return weight;
        }
        // hiermee kan geëxperimenteerd worden om de intelegentie te verhogen/ verlagen
    }
}

