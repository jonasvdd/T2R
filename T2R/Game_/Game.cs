using System.Collections.Generic;
using System.IO;
using T2R.Cards;
using T2R.Forms;
using T2R.Initialize;
using T2R.Map;
using T2R.Players;

namespace T2R.Game_
{
    public class Game
    {
        // variables
        private static List<City> citylist = new List<City>(); // mss dictionary
        private static List<LongDestCard> LongDestDeck = new List<LongDestCard>();
        private static List<NormDestCard> NormDestDeck = new List<NormDestCard>();
        private static List<Railroad> tracks = new List<Railroad>();
        private static List<TrainCard> trainCardDeck = new List<TrainCard>();
        private static List<Player> players = new List<Player>();


        // constructor
        public Game(List<foto> pics)
        {
            initializeComponents();
            initializePlayers(pics);
        }


        /******************************
        Initializing
        *******************************/

        private static void initializeComponents()
        {
            string textFilePath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + @"\Data\DataEncr.txt";
            //http://stackoverflow.com/questions/14549766/how-to-get-my-project-path
            ExtensionMethods.niceLayout("INITIALIZING ELEMENTS");
            InitializeObjects.readDataFromTextFile(textFilePath, '\t', citylist, LongDestDeck, NormDestDeck, tracks);
            InitializeObjects.addTrainCards(trainCardDeck);
            foreach (City stad in citylist)
            {
                Program.logboek.WriteLine(stad.ToString());
                ExtensionMethods.niceLayout("");
            }

            ExtensionMethods.niceLayout("Start Game");
            Program.logboek.WriteLine("SHUFFLING CARDS");
            CardMethods<LongDestCard>.shuffle(LongDestDeck);
            Program.logboek.WriteLine("\tlong destination cards shuffled");
            CardMethods<NormDestCard>.shuffle(NormDestDeck);
            Program.logboek.WriteLine("\tnormal destination cards shuffled");
            CardMethods<TrainCard>.shuffle(trainCardDeck);
            Program.logboek.WriteLine("\ttraincards shuffled");
        }


        private void initializePlayers(List<foto> pics)
        {
            ExtensionMethods.niceLayout("initialziing players");
            Program.logboek.WriteLine("asking input from playerform");
            FormDataObject data = new FormDataObject();
            AmountofPlayersForm form = new AmountofPlayersForm(pics, data);

            /******************************
            Ask user here how many and if he wants to play with intelligent bots bl
            *******************************/

            form.ShowDialog();
            int realplayers = data.getReal();
            int coOps = data.getCoOps();
            Program.logboek.WriteLine("real players:\t" + realplayers);
            Program.logboek.WriteLine("Robots:\t" + coOps);

            ExtensionMethods.niceLayout("Adding Players and deviding the cards");
            InitializeObjects.addPlayers(players, pics, realplayers, coOps, citylist, tracks);
            CardMethods<AbstrDestCard>.devideCards(players, LongDestDeck, NormDestDeck, trainCardDeck);

            foreach (Player player in players){Program.logboek.WriteLine(player.ToString());}

            ExtensionMethods.niceLayout("remaining cards");
            Program.logboek.WriteLine(trainCardDeck.ToArray().Length + " traincards");
            Program.logboek.WriteLine(NormDestDeck.ToArray().Length + " normdestcards");
            Program.logboek.WriteLine(LongDestDeck.ToArray().Length + " longdestcards");
        }



        /******************************
        Properties
        *******************************/
        public List<City> getcitylist()
        {
            return citylist;
        }

        public List<NormDestCard> getNormDestcard()
        {
            return NormDestDeck;
        }

        public List<Railroad> getNetwork()
        {
            return tracks;
        }

        public List<TrainCard> getDeck()
        {
            return trainCardDeck;
        }

        public List<Player> getPlayers()
        {
            return players;
        }
    }
}
