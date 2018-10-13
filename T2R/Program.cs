using System;
using T2R.MenuStructure;
using System.IO;
using T2R.Forms;

namespace T2R
{
    public class Program
    {
        public static ticketToRideForm form;

        public static Logger logboek;
        static void Main(string[] args)
        {
            // https://msdn.microsoft.com/en-us/library/ms228388.aspx
            string dateString = Convert.ToString(DateTime.Now.TimeOfDay);
            char[] splitchars = { ' ', ',', '.', ':' };
            string[] bla = dateString.Split(splitchars);
            string time = string.Format("{0}H{1}M{2}S", bla[0], bla[1], bla[2]);
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
                + @"\Logboek\" + time +/* "_" + DateTime.Now +*/  ".txt";
            logboek = new Logger(path);
            path = @"C:\Users\Jonas\Desktop\DataEncr.txt";
            Console.SetWindowSize(85, 40);
            ExtensionMethods.author();

            form = new ticketToRideForm();
            form.ShowDialog();


            //    Node root = new Node();

            //    RunNewGame runNewgame = new RunNewGame("play a new game");
            //    RunExistingGame runExistingGame = new RunExistingGame("play an existing game!");

            //    Node nodeNewGame = new Node(root, runNewgame);
            //    nodeNewGame = new Node(root, runExistingGame);
            //    AccesMenu(root);
            //}

            //private static void AccesMenu(Node knoop)
            //{
            //    bool blijvenUitvoeren = true;
            //    while (blijvenUitvoeren == true)
            //    {

            //        WriteMenuType(knoop, "----HoofdMenu----", "----Submenu----");
            //        // er wordt weergegeven in welk deel van de menu structuur we zitten
            //        Menunode menu = (Menunode)knoop.getData();
            //        //int i = menu.printmenu();
            //        int i = 1;              // de variabele i wordt gebruikt als prefix voor de mogelijkheden van het mneu
            //        foreach (Node node in knoop.getChildNodes())
            //        {                                   // alle elementen in het hoofdmenu worden weergegeven 
            //            logboek.WriteLine(i + ".\t" + node.getData().getName());
            //            i += 1;
            //        }
            //        if (IsSuperRoot(knoop) == true)             // er wordt gecontroleerd of we in het hoofmenu zitten of niet 
            //            logboek.WriteLine(i + ".\tStoppen");
            //        else
            //            logboek.WriteLine(i + ".\tTerugkeren");

            //        int userVal = ExtensionMethods.tryGetInt(1, i);
            //        if (1 <= userVal && userVal < i)
            //        {
            //            if (knoop.getChildNodes()[userVal - 1].getData() is Menunode)                            // recursieve aanroep van de methode SubMenu
            //                AccesMenu(knoop.getChildNodes()[userVal - 1]);
            //            else                              // indien het een commando is zal het corresponderende project worden uitgevoerd
            //                knoop.getChildNodes()[userVal - 1].getData().RunCommand();
            //        }
            //        else if (userVal == i)
            //            blijvenUitvoeren = false;
            //    }
            //}



            //private static void WriteMenuType(Node knoop, string a, string b)
            //{
            //    if (IsSuperRoot(knoop) == true)
            //        logboek.WriteLine(a);
            //    else
            //        logboek.WriteLine(b);
            //}


            //private static bool IsSuperRoot(Node knoop)
            //{
            //    bool superRoot;
            //    if (knoop.getParent() == null)
            //        superRoot = true;
            //    else
            //        superRoot = false;
            //    return superRoot;
        }
    }

    public struct Logger
    {
        private string datapath;

        public Logger(string datapath) { this.datapath = datapath; }

        public void WriteLine(string text)
        {
            string[] splittedText = text.Split('\n');
            if (!File.Exists(datapath))
            {
                using (StreamWriter sw = File.CreateText(datapath))
                {
                    foreach (string splittedEl in splittedText) { sw.WriteLine(splittedEl); Console.WriteLine(splittedEl); }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(datapath, true))
                {
                    foreach (string splittedEl in splittedText) { sw.WriteLine(splittedEl); Console.WriteLine(splittedEl); }
                    
                }
            }
        }
    }
}
