using System;
using System.Collections.Generic;
using System.Windows.Forms;
using T2R.Cards;
using T2R.Game_;
using T2R.Map;
using T2R.Forms;
using T2R.Players;

public enum Possibilities { General, Traincard, stationCities, stationCards, Railroad, visibleTraincards, Neighbours, leastImportantTraincard }
namespace T2R.Rounds
{
    public class Round
    {
        private readonly List<TrainCard> visibleTraincards = new List<TrainCard>();
        private readonly List<NormDestCard> destinationcard;
        private readonly List<foto> gamePics;
        private readonly List<TrainCard> deck;
        public readonly List<City> citylist;
        private readonly List<Player> playerlist;
        private readonly List<Railroad> network;
        private int _maxPoints, _roundNumber;


        /********************
        wou met events handlers werken en wachten op input wan de console 
        (om zoveel mogelijk de data/ onderliggende objecten af te schermen)
        maar had hiervoor een asynchrone methode voor nodig en had een beetje tijdtekort,
        heb me vooral gefocust op intelligentie en een werkend programma
        *********************/

        public Round(Game game, List<foto> gamePics)
        {
            this.gamePics = gamePics;
            citylist = game.getcitylist();
            destinationcard = game.getNormDestcard();
            deck = game.getDeck();
            network = game.getNetwork();
            playerlist = game.getPlayers();

            for (int i = 0; i < 5; i++)
            {
                CardMethods<TrainCard>.swapCardFromDeck(deck, visibleTraincards);     // we pick the first 5 cards from the deck and put them in the visible pack
            }
            visibleTraincards.Sort();
            Program.form.refreshVisibleTrainCards(visibleTraincards);
        }

        public void Startrounds()
        {
            int amountOfTrains = 48;
            while (_maxPoints < 50 && amountOfTrains > 8)
            {
                bool succesfulMove = false;
                foreach (Player player in playerlist)
                {
                    Header(visibleTraincards, _roundNumber, player);
                    while (succesfulMove == false)      // until a player makes a succesful move
                    {
                        ActionsMethods.visibleTraincards = visibleTraincards;
                        int val = 0;
                       // if (player is IntelligentPlayer)
                       // {
                            //val = IntelligentChoice(player);
                           
                            /*indien ik hier meer tijd had zou ik met dijkstra en dynamische gewichten a.d.h.v. de toestand van het spel en de kaarten van de speler zelf makne,
                            gebaseerd op de destinationcards die hij in het bezit heeft. Dan zou ik deze speler zich vooral focuseen op de tracks die de kaarten het meest 
                            gemeenschappelijk hebben bij hun kortste pad. maar aangezien de omvang van het initiële programma ben ik wat in tijdsnood gekomen*/
                            /*daarom zal ook de statische klasse actions een beetje wanordelijk uitzien door de vele controlestructuren van het spel*/
                        // }
                        val = player.ChoseOption(Possibilities.General);  // intelligente spelers overriden deze methode
                        switch (val)
                        {
                            case (int)PlayerAction.Traincards:
                                ExtensionMethods.niceLayout("Traincards");
                                succesfulMove = ActionsMethods.PickTrainCards(player, deck, visibleTraincards,  _roundNumber, gamePics);
                                break;
                            case (int)PlayerAction.Railroad:
                                ExtensionMethods.niceLayout("building a track");
                                succesfulMove = ActionsMethods.BuildRailRoad(player, citylist, network);
                                player.CheckforInvisbleRailroads();
                                break;
                            case (int)PlayerAction.DestinationCard:
                                ExtensionMethods.niceLayout("Destination Card");
                                succesfulMove = ActionsMethods.PickNormDestinationCard(destinationcard, player);
                                break;
                            case (int)PlayerAction.Station:
                                ExtensionMethods.niceLayout("building a station");
                                succesfulMove = ActionsMethods.BuildStation(player, citylist);
                                player.CheckforInvisbleRailroads();
                                break;
                        }
                    }
                    Program.logboek.WriteLine("the visible traincards are");
                    foreach (TrainCard card in visibleTraincards)
                    {
                        Program.logboek.WriteLine(card.ToString());
                    }
                    ActionsMethods.CheckSizeOfMainDeck(deck);
                    CheckDistanceCards(citylist, player);
                    if (player is IntelligentPlayer) { ((IntelligentPlayer)player).firstloop = true; }
                    if (player.getPoints() > _maxPoints) _maxPoints = player.getPoints();
                    if (player.GetAmountOfUnplacedTrains() < amountOfTrains){amountOfTrains = player.GetAmountOfUnplacedTrains();}
                    succesfulMove = false;
                }
                _roundNumber++;
            }
            string text = null;
            foreach (Player player in playerlist){text += player.ToString();}
            MessageBox.Show("END GAME\n" + text);
            Environment.Exit((0));
        }



        private static void CheckDistanceCards(List<City> cityList, Player player)
        {
            foreach (AbstrDestCard card in player.GetDestinationCards())        // check if the player made a new network
            {
                City a, b;
                card.getCities(out a, out b);
                if (GraphAlgorithms.BFS(cityList.Count, player, a, b))
                {
                    player.addPoints(card.getPoints());
                    player.GetDestinationCards().Remove(card);
                    break;
                }
            }
            Console.WriteLine(player.ToString());
        }


        private static void Header(List<TrainCard> visibleTraincards, int roundNumber, Player player)
        {
            Program.logboek.WriteLine("");
            Program.logboek.WriteLine("");
            //ExtensionMethods.WaitForUser();
            // Console.Clear();
            Program.logboek.WriteLine("Round " + roundNumber);
            Program.logboek.WriteLine("De zichtbare kaarten zijn");
            Program.logboek.WriteLine(CardMethods<TrainCard>.getCardValues(visibleTraincards));
            Program.logboek.WriteLine(player.ToString());
            Program.logboek.WriteLine("-----------------------------------");
            Program.logboek.WriteLine("Turn:\t" + player.GetName());
            Program.logboek.WriteLine("\t1.\tPick some TrainCards\n\t" + "2.\tBuild a track between 2 cities\n\t" +
               "3.\tPick a destination Card\n\t" + "4.\tBuild a station");
        }
    }
}
