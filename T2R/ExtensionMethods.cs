using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace T2R
{

    public static class ExtensionMethods
    {
        public static string[] SplitSTring(string line, char splitValue)            // via deze methode wordt de input opgesplitst in een reel en imaginair deel
        {
            string[] splittedNumbers = line.Split(new char[] { splitValue }, StringSplitOptions.RemoveEmptyEntries);
            return splittedNumbers;
        }


        public static void WaitForUser()
        {
            Program.logboek.WriteLine("Press any key to Continue  ...");
            Console.ReadKey();
            Program.logboek.WriteLine("");
            Program.logboek.WriteLine("-------------------------------------");
        }
        


        public static int tryGetInt(int minval = 0, int maxval = int.MaxValue)
        {
            int value;
            bool validInput = false;
            while (validInput == false)
            {
                string sval = Console.ReadLine();
                validInput = int.TryParse(sval, out value);
                if (validInput == false)
                {
                    Console.WriteLine("gelieve een geldige waarde van type int te voeren");
                }
                else
                {
                    if (minval <= value && maxval >= value)
                    {
                        return value;
                    }
                    else
                    {
                        validInput = false;
                        Console.WriteLine("gelieve een waarde tussen het correcte interval in te geven");
                    }
                }
            }
            return int.MaxValue;
        }


        public static int GetMaxOfArray(int[] amountofCards)
        {
            int max = 0;
            for (int i = 0; i < amountofCards.Length; i++)
            {
                if (amountofCards[i] > max) max = amountofCards[i];
            }
            return max;
        }


        public static bool getObject<T>(out T obj, IEnumerable<T> objlist) where T : IComparable
        {
            Console.WriteLine("please give in the value");
            string text = Console.ReadLine();
            obj = default(T);      // null bij referentie, false bij boolean en 0 bij integers
            bool objFound = false;
            foreach (T ob in objlist)
            {
                if (ob.CompareTo(text) == 0)
                {
                    obj = ob;
                    objFound = true;
                    Console.WriteLine("The value was found");
                }
            }
            return objFound;
        }


        public static bool getObject<T>(out T obj, Dictionary<T, int> objlist) where T : IComparable    // we wekren met een dictionary
        {
            Console.WriteLine("please give in the value");
            string text = Console.ReadLine();
            obj = default(T);      // null bij referentie, false bij boolean en 0 bij integers
            bool objFound = false;
            foreach (KeyValuePair<T, int> ob in objlist)
            {
                if (ob.Key.CompareTo(text) == 0)
                {
                    obj = ob.Key;
                    objFound = true;
                    Console.WriteLine("The value was found");
                }
            }
            return objFound;
        }


        public static void ProgrammaUitvoeren(Action programma)
        {
            bool blijvenUitvoeren = true;
            while (blijvenUitvoeren == true)
            {
                programma();
                Console.WriteLine("wilt u het programma opnieuw uitvoeren (ja/nee)?");
                string answer = Console.ReadLine();
                if (answer == "nee" || answer == "Nee")
                    blijvenUitvoeren = false;
            }
        }


        public static void author()
        {
            Program.logboek.WriteLine("This program was made by Jonas Van Der Donckt");
        }


        public static void niceLayout(string text)
        {
            Program.logboek.WriteLine("");
            Program.logboek.WriteLine("-------------------" + text.ToUpper() + "------------------");
        }

        // http://stackoverflow.com/questions/6454950/passing-an-enum-type-as-an-argument


        public static IDictionary<string, Int32> ConvertEnumToDictionary<K>()
        {
            // converts an enem to an dictionarye so we can pass enums as an argument
            return Enum.GetValues(typeof(K)).Cast<Int32>().ToDictionary
                (currentItem => Enum.GetName(typeof(K), currentItem));
        }

        public static void ShowError(string text)
        {
            Program.logboek.WriteLine(text);
            MessageBox.Show(text);
            throw new NotImplementedException(text);
        }
    }
}



