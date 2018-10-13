using System;
using System.Collections.Generic;
using T2R.Cards;
using T2R.Map;
using T2R.Players;
using T2R.Forms;

namespace T2R.Initialize
{
    public static class InitializeObjects
    {
        /******************************
        Reading Data
        *******************************/
        public static void readDataFromTextFile(string path, char splitChar, List<City> cityList, List<LongDestCard> longCardList, List<NormDestCard> normDestCardList, List<Railroad> netWork)
        {
            List<string[]> splittedData;
            List<string[]>[] objectData;
            List<string> data = new List<string>();
            Decrypt decryption = new Decrypt(path, data);
            if (data.Count == 195)
            {
                Program.logboek.WriteLine("THE CORRECT DATA WAS ADDED");
            }
            else
            {
                Program.logboek.WriteLine("There was incorrect data added");
                throw new DataMisalignedException("There was incorrect data added");
            }
            string[] stopWords = new string[] { "Cards", "Tracks", "END" };
            splitData(out splittedData, splitChar, data);
            objectData = splitDataList(splittedData, stopWords);
            //ExtensionMethods.WaitForUser();
            addCities(cityList, objectData[0]);
            addDestCard(longCardList, normDestCardList, cityList, objectData[1]);
            addRailRoads(netWork, cityList, objectData[2]);
        }


        private static void splitData(out List<string[]> splitData, char splitChar, List<string> list)
        {       // this reads the data in form the texfile and splits it and puts it in a list of array
            splitData = new List<string[]>();
            string[] splittedText;

            foreach (string text in list)
            {
                splittedText = ExtensionMethods.SplitSTring(text, splitChar);
                splitData.Add(splittedText);
            }
        }

        private static List<string[]>[] splitDataList(List<string[]> data, string[] splitvalues)
        {
            List<string[]>[] splittedData = new List<string[]>[splitvalues.Length];
            int temp = 0;

            for (int i = 0; i < splitvalues.Length; i++)
            {
                for (int k = temp; k < data.Count; k++)
                {
                    //string[] text = data[k];
                    if (data[k][0] == splitvalues[i])
                    {
                        splittedData[i] = data.GetRange(temp, k - temp);
                        temp = (k + 1);
                        k = data.Count;
                    }
                }
            }
            return splittedData;
        }



        /******************************
        Adding The Objects
        *******************************/

        private static void addCities(List<City> list, List<string[]> data)
        {
            // eerst moeten alle steden aangemaakt worden vooraleer er buren kunnen aangemaakt worden
            ExtensionMethods.niceLayout("adding cities");
            City city;

            for (int i = 0; i < data.Count; i++)
            {
                string[] text = data[i];
                city = new City(text[0], i, Convert.ToDouble(text[1]), Convert.ToDouble(text[2]));
                list.Add(city);
            }

            for (int j = 0; j < list.Count; j++)
            {
                string[] neighbourNames = (string[])data[j];
                city = list[j];
                List<City> neighbourCities = new List<City>();
                for (int k = 1; k < neighbourNames.Length; k++)
                {
                    foreach (City city1 in list)
                    {
                        if (city1.getname() == neighbourNames[k])
                        {
                            neighbourCities.Add(city1);
                        }
                    }
                }
                city.Addneighbours(neighbourCities.ToArray());
            }
            foreach (City stad in list)
            {
                Program.logboek.WriteLine(stad.ToString());
            }
            Program.logboek.WriteLine(string.Format("{0} cities added", list.Count));
        }


        private static void addDestCard(List<LongDestCard> LongDestList, List<NormDestCard> normDestList, List<City> cities, List<string[]> Data)
        {
            ExtensionMethods.niceLayout("adding destinationcards");
            City city1 = null, city2 = null;
            AbstrDestCard card;

            foreach (string[] text in Data)
            {
                findCity(cities, ref city1, ref city2, text);
                if (Convert.ToInt32(text[2]) < 19)
                {
                    card = new NormDestCard(city1, city2, Convert.ToInt32(text[2]));
                    normDestList.Add((NormDestCard)card);
                }
                else
                {
                    card = new LongDestCard(city1, city2, Convert.ToInt32(text[2]));
                    LongDestList.Add((LongDestCard)card);
                }
            }
            ExtensionMethods.niceLayout(normDestList.Count + " Normal DestinationCards added");
            foreach (AbstrDestCard normcard in normDestList) { Program.logboek.WriteLine(normcard.ToString()); }
            Program.logboek.WriteLine("");
            Program.logboek.WriteLine("");
            ExtensionMethods.niceLayout(LongDestList.Count + " Long DestinationCards added");
            foreach (AbstrDestCard longCard in LongDestList) { Program.logboek.WriteLine(longCard.ToString()); }
        }


        private static void addRailRoads(List<Railroad> netWork, List<City> cities, List<string[]> data)
        {
            ExtensionMethods.niceLayout("adding railroads");
            City city1 = null, city2 = null;
            bool isTunnel;
            int length, amountOfLocomotives;
            Color kleur;
            Railroad railroad;

            foreach (string[] text in data)
            {
                findCity(cities, ref city1, ref city2, text);
                kleur = (Color)Enum.Parse(typeof(Color), text[2], true);     //http://stackoverflow.com/questions/16100/how-do-i-convert-a-string-to-an-enum-in-c
                length = Convert.ToInt32(text[3]);
                isTunnel = Convert.ToBoolean(Convert.ToInt32(text[4]));
                amountOfLocomotives = Convert.ToInt32(text[5]);
                railroad = new Railroad(city1, city2, length, kleur, isTunnel, amountOfLocomotives);
                netWork.Add(railroad);
            }
            Program.logboek.WriteLine(netWork.ToArray().Length + "\tTracks added");
        }

        public static void addTrainCards(List<TrainCard> cardlist)
        {
            ExtensionMethods.niceLayout("adding traincards");
            TrainCard card;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    card = new TrainCard((Color)i);
                    cardlist.Add(card);
                }
            }
            for (int i = 0; i < 14; i++)
            {
                card = new TrainCard((Color)8);
                cardlist.Add(card);
            }
            Program.logboek.WriteLine(cardlist.ToArray().Length + "\tTrainCards added");
        }



        /******************************
        Adding The players
        *******************************/

        public static void addPlayers(List<Player> playerList, List<foto> pics, int realPlayers, int robots, List<City> citylist, List<Railroad> network)
        {
            List<Player> botPlayer = new List<Player>();
            IntelligentPlayer Emiel = new IntelligentPlayer("Emile", System.Drawing.Color.Blue, citylist, network); botPlayer.Add(Emiel);
            IntelligentPlayer Dirk = new IntelligentPlayer("Dirkske", System.Drawing.Color.Red, citylist, network); botPlayer.Add(Dirk);
            IntelligentPlayer Luka = new IntelligentPlayer("Luka", System.Drawing.Color.Yellow, citylist, network); botPlayer.Add(Luka);
            IntelligentPlayer Jeff = new IntelligentPlayer("Jeff", System.Drawing.Color.Black, citylist, network); botPlayer.Add(Jeff);
            for (int i = 0; i < robots; i++)
            {
                CardMethods<Player>.swapCardFromDeck(botPlayer, playerList);
            }

            /******************************
            Ask user here player data
            *******************************/
            for (int i = 0; i < realPlayers; i++)
            {
                PlayerData data = new PlayerData();
                PlayerDialog form = new PlayerDialog(data, pics);
                while (data.getName() == null || data.getColor() == null)
                {
                    form.ShowDialog();
                }
                Player a = new Player(data.getName(), data.getColor());
                playerList.Add(a);
            }
            Program.logboek.WriteLine(string.Format("{0} players added", playerList.Count));
        }



        /******************************
         Methods
        *******************************/

        private static void findCity(List<City> cities, ref City city1, ref City city2, string[] text)
        {
            foreach (City city in cities)
            {
                if (city.getname() == text[0])
                {
                    city1 = city;
                }
                else if (city.getname() == text[1])
                {
                    city2 = city;
                }
            }
        }
    }
}
