using System;
using System.Collections.Generic;
using T2R.Cards;
using System.Windows.Forms;
using System.Drawing;
using T2R.Players;


namespace T2R.Map
{
    public class Railroad : IComparable
    {
        /*instance Variables*/
        private int length = 0;
        private Color color;
        private int amountOfLocomotives;
        private bool isTunnel;
        private bool Occupied = false;
        private Player player;
        List<City> destinations = new List<City>();


        /******************************
        Constructors
        *******************************/

        public Railroad(City city1, City city2, int length, Color color, bool isTunnel, int amountOfLocomotives = 0)
        {
            this.length = length;
            destinations.Add(city1);
            destinations.Add(city2);
            this.color = color;
            this.isTunnel = isTunnel;
            this.amountOfLocomotives = amountOfLocomotives;
        }


        public Railroad(City city1, City city2)         // voor de virtuele railroad (omdat we mest stations werken
        {
            destinations.Add(city1);
            destinations.Add(city2);
        }



        /******************************
        Methods
        *******************************/

        public void drawLine(Player player, PictureBox box)
        {
            // http://stackoverflow.com/questions/2729751/how-do-i-draw-a-circle-and-line-in-the-picturebox
            System.Drawing.Color color = player.GetColor();
            int width = Program.form.Width;
            int height = Program.form.Height;
            Graphics g = box.CreateGraphics();
            Pen pen = new Pen(player.GetColor(), width / 150);
            Rectangle rectangle = new Rectangle();
            PaintEventArgs e = new PaintEventArgs(g, rectangle);
            e.Graphics.DrawLine(pen, Convert.ToInt32(destinations[0].getX() * width),
                Convert.ToInt32(destinations[0].getY() * height), Convert.ToInt32(destinations[1].getX() * width),
                Convert.ToInt32(destinations[1].getY() * height));
        }

        public int CompareTo(object obj) // de railroads kunnen nu op lengte gesorteerd worden
        {
            if (obj is Railroad)
            {
                return (length.CompareTo(((Railroad)obj).getRailLength()));
            }
            else
            {
                ExtensionMethods.ShowError("you can only compare a railroad to another railroad");
            }
            return default(int);
        }


        public override string ToString()
        {
            string text = "\n\tfrom: " + destinations[0].getname() + " to: " + destinations[1].getname() + "\n\tlengte: " + length +
                "\n\tkleur: " + Convert.ToString(color) + "\n\ttunnel: " + ((isTunnel) ? "ja" : "nee") + "\n\tminimum aantal locomotieven?: " +
                amountOfLocomotives;
            return text;
        }



        /******************************
        Properties
        *******************************/

        public List<City> getdestinations()
        {
            return destinations;
        }


        public int getRailLength()
        {
            return length;
        }


        public bool IsTunnel()
        {
            return isTunnel;
        }


        public Player getPlayer()
        {
            return player;
        }

        public Color getColor()
        {
            return color;
        }


        public int getAmountOfLocomotives()
        {
            return amountOfLocomotives;
        }


        public void setOccupied(Player player)
        {
            Occupied = true;
            this.player = player;
        }


        public bool isOccupied()
        {
            return Occupied;
        }
    }
}
