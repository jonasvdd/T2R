using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using T2R.Game_;
using T2R.Cards;
using T2R.Rounds;
using T2R.Players;
using T2R.Map;


namespace T2R.Forms
{
    public partial class ticketToRideForm : Form
    {
        // instance variables
        public readonly List<foto> gamepics = new List<foto>();
        //List<Button> visibleCardButtons = new List<Button>();
        private readonly Dictionary<Button, TrainCard> visiblecardsbtns = new Dictionary<Button, TrainCard>();
        private readonly Dictionary<TrainCard, foto> traincardpics = new Dictionary<TrainCard, foto>();
        private Game game;
        private Round rounds;
        public int val;
        private bool firsttime = true;
        public int actionval = 0;
        private List<City> cityList = new List<City>();
        private readonly List<Player> player = new List<Player>();
        private List<Railroad> network = new List<Railroad>();


        // constructor
        public ticketToRideForm()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        /******************************
        Methods
        *******************************/
        private void IsEnabled(bool val)
        {
            VisibleCardsbox.Enabled = val;
            pictureBox1.Enabled = val;
        }


        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameToolStripMenuItem.Enabled = false;
            game = new Game(gamepics);
            IsEnabled(true);
            rounds = new Round(game, gamepics);
            cityList = rounds.citylist;
            network = game.getNetwork();
            rounds.Startrounds();
        }

              private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ticketToRideForm_Load(object sender, EventArgs e)
        {
            visiblecardsbtns.Add(visibleCardbtn1, null);
            visiblecardsbtns.Add(visibleCardbtn2, null);
            visiblecardsbtns.Add(visibleCardbtn3, null);
            visiblecardsbtns.Add(visibleCardbtn4, null);
            visiblecardsbtns.Add(visibleCardbtn5, null);
            firsttime = false;
            foreach (KeyValuePair<Button, TrainCard> cardbutton in visiblecardsbtns)
            {
                cardbutton.Key.BackgroundImageLayout = ImageLayout.Stretch;
            }
            BackgroundImageLayout = ImageLayout.Stretch;
            destinationCardButton.BackgroundImageLayout = ImageLayout.Stretch;
            Deckbutton.BackgroundImageLayout = ImageLayout.Stretch;

            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())); 
            //http://stackoverflow.com/questions/14549766/how-to-get-my-project-path
            string[] files = Directory.GetFiles(wanted_path + @"\foto's\");
            IsEnabled(false);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fInfo = new FileInfo(files[i]);
                if (fInfo.Extension.ToUpper() == ".PNG" || fInfo.Extension.ToUpper() == ".JPG" || fInfo.Extension.ToUpper() == ".JPEG")
                {
                    foto foto = new foto(fInfo.Name.Split('.')[0], files[i]);
                    gamepics.Add(foto);
                }
            }
            ExtensionMethods.niceLayout("initializing pictures");
            Program.logboek.WriteLine("these pictures were found:");

            showpictures();
            initializeTrainCardPics(gamepics, traincardpics);
            // http://stackoverflow.com/questions/13383643/c-need-to-remove-last-folder-from-file-name-path
            // MessageBox.Show("the path is:\n" + wanted_path); 
            pictureBox1.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }


        private void ticketToRideForm_Resize(object sender, EventArgs e)
        {
            Deckbutton.Height = Convert.ToInt32(this.Height * 0.15);
            destinationCardButton.Height = Convert.ToInt32(this.Height * 0.15);

            pictureBox1.Height = Convert.ToInt32(this.Height * 0.90);
            pictureBox1.Width = Convert.ToInt32(this.Width * 0.80);
            VisibleCardsbox.Height = Convert.ToInt32(this.Height / 1.05);
            VisibleCardsbox.Width = Convert.ToInt32(this.Width * 0.18);
            pictureBox1.Location = new Point(this.Width / 80, Convert.ToInt32(this.Height - this.Height / 1.04));
            VisibleCardsbox.Location = new Point(Convert.ToInt32(this.Width - this.Width * 0.18), 30);

            Deckbutton.Width = Convert.ToInt32(VisibleCardsbox.Width * 0.43);
            destinationCardButton.Width = Convert.ToInt32(VisibleCardsbox.Width * 0.43);

            Deckbutton.Location = new Point(VisibleCardsbox.Width / 50, VisibleCardsbox.Height * 5 / 7 + 6 * this.Height / 100);
            destinationCardButton.Location = new Point(VisibleCardsbox.Width / 2, VisibleCardsbox.Height * 5 / 7 + 6 * this.Height / 100);

            int a = 1;
            foreach (KeyValuePair<Button, TrainCard> button in visiblecardsbtns)
            {
                button.Key.Width = Convert.ToInt32(VisibleCardsbox.Width * 0.95);
                button.Key.Height = Convert.ToInt32(this.Height * 0.14);
                button.Key.Location = new Point(VisibleCardsbox.Width / 50, (a - 1) * VisibleCardsbox.Height / 7 + a * this.Height / 100);
                a++;
            }


            foreach (City city in cityList)
            {
                if (city.getStation() != null)
                {
                    city.DrawCircle(city.getStation().getPlayer(), pictureBox1);
                }
            }

            foreach (Railroad road in network)
            {
                if (road.isOccupied())
                {
                    foreach (Player speler in player)
                    {
                        if (speler.GetNetwork().Contains(road))
                            road.drawLine(speler, pictureBox1);
                    }
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            /******************************
            Code Die gebruikt wordt om de coordinaten te berekenen
            *******************************/
            //MouseEventArgs me = (MouseEventArgs)e;
            //Point coordinates = me.Location;
            //write.WriteValuesToTxtFile(Convert.ToDouble(coordinates.X) / this.Width, Convert.ToDouble(coordinates.Y) / this.Height);
            //Console.WriteLine("x:\t" + this.Width + "y:\t" + me.Y);
            //if (cityList.Count > 0)
            //{
            //    City closestCity = null;
            //    double temp = double.MaxValue;
            //    foreach (City stad in cityList)
            //    {
            //        double var = stad.CalculateDistance(coordinates.X, coordinates.Y, this.Width, this.Height);
            //        if (var < temp)
            //        {
            //            closestCity = stad;
            //            temp = var;
            //        }
            //    }
            //    Console.WriteLine("\t" + closestCity.getname());
            //}
            ////else
            ////{
            ////    Console.WriteLine("X:\t{0}\nY:\t{1}", me.X, me.Y);
            ////}
        }



        /******************************
        picture methods
        *******************************/
        private void showpictures()
        {
            foreach (foto pic in gamepics)
            {
                Program.logboek.WriteLine("\t" + pic.getName() + "\n" + pic.getDataPath());
                if (pic.getName().ToUpper() == "WALLPAPER"){ BackgroundImage = Image.FromFile(pic.getDataPath()); }
                else if (pic.getName().ToUpper() == "MAP"){pictureBox1.Image = Image.FromFile(pic.getDataPath());}
                else if (pic.getName().ToUpper() == "DESTINATIONCARDS")
                { destinationCardButton.BackgroundImage = Image.FromFile(pic.getDataPath());}
                else if (pic.getName().ToUpper() == "DECK"){Deckbutton.BackgroundImage = Image.FromFile(pic.getDataPath());}
            }
        }


        private static void initializeTrainCardPics(List<foto> pics, Dictionary<TrainCard, foto> traincardpics)
        {
            for (int i = 0; i < 9; i++)
            {
                TrainCard card = new TrainCard((Color)i);
                traincardpics.Add(card, default(foto));
            }


            foreach (foto pic in pics)
            {
                foreach (KeyValuePair<TrainCard, foto> p in traincardpics)
                {
                    if (pic.getName().ToUpper() == p.Key.ToString().ToUpper())
                    {
                        traincardpics[p.Key] = pic;
                        break;
                    }
                }
            }

        }

        public void refreshVisibleTrainCards(List<TrainCard> visibleTraincard)
        {
            var buttons = new List<Button>(visiblecardsbtns.Keys);
            foreach (Button knop in buttons)
            {
                visiblecardsbtns[knop] = null;
            }
            ExtensionMethods.niceLayout("refreshing pictures");
            foreach (TrainCard card in visibleTraincard)
            {
                foreach (KeyValuePair<Button, TrainCard> button in visiblecardsbtns)
                {
                    if (button.Value == null)
                    {
                        visiblecardsbtns[button.Key] = card;
                        foreach (KeyValuePair<TrainCard, foto> bib in this.traincardpics)
                        {
                            if (bib.Key.ToString() == card.ToString())
                            {
                                button.Key.BackgroundImage = Image.FromFile(bib.Value.getDataPath());
                                //Program.logboek.WriteLine(
                                //    string.Format("De achtergrondfoto van {0} is: \n{1}"
                                //    , bib.Key.ToString(), bib.Value.getDataPath()));
                            }
                        }
                        break; // sorry
                    }
                }
            }
        }
    }

    public struct foto
    {
        private string datapath;
        private string name;

        public foto(string name, string datapath)
        {
            this.name = name;
            this.datapath = datapath;
        }

        public string getName()
        {
            return name;
        }

        public string getDataPath()
        {
            return datapath;
        }
    }


    public class WriteToTextFile
    {
        public void WriteValuesToTxtFile(double x, double y)
        {
           const string  path = @"C:\Users\Jonas\Desktop\Jonas Van Der Donckt\newinsertion\" + "coordinatenNewVersion"
                + ".txt";
            if (!File.Exists(path))     // indien het bestand niet bestaat
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    WriteData(x, y, sw);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    WriteData(x, y, sw);
                }
            }

            //List<SortTime_for_n> lijst = datalement.GetList(SortTime_for_n.SortType.BubbleSort);
            //Console.WriteLine("de lengte van de lijst is: " + lijst.ToArray().Length);
            //foreach (SortTime_for_n data in lijst)
            //{

            //    sw.WriteLine("{0}\t{1}", data.GetN(), data.GetTime());
            //}
        }


        private static void WriteData(double x, double y, StreamWriter sw)
        {
            sw.WriteLine("{0}\t{1}", x, y);
        }
    }
}

