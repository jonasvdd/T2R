using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using T2R.Players;

namespace T2R.Map
{

    public class City : IComparable
    {
        /*instance variables*/
        private List<City> neighbours = new List<City>();
        private int cityID;
        private Station station = null;
        private string name;
        private double x;
        private double y;



        /*constructor*/
        public City(string name, int cityID, double x, double y)
        {
            this.name = name;
            this.cityID = cityID;
            this.x = x;
            this.y = y;
        }



        /******************************
        Methods
        *******************************/

        public void Addneighbours(City[] stad)   // nodig voor initialisatie, uit (geencrypteerdeà textfiles
        {
            foreach (City city in stad)
            {
                neighbours.Add(city);
            }
        }


        public double CalculateDistance(int xPos, int yPos, int width, int height)
        // was oorspronkelijk van plan om dit te gebruiken om de afstand van de 
        //coördinaten van  het klik even tot een stad weer te geven
        {
            return Math.Sqrt(Math.Pow((this.x * width - xPos), 2) + Math.Pow((this.y * height - yPos), 2));
        }


        public void DrawCircle(Player player, PictureBox box)
        {
            System.Drawing.Color color = player.GetColor();
            Graphics g = box.CreateGraphics();
            Rectangle rectangle = new Rectangle();
            PaintEventArgs e = new PaintEventArgs(g, rectangle);
            Pen pen = new Pen(color, box.Width / 200);
            e.Graphics.DrawEllipse(pen, Convert.ToInt32(this.x * Program.form.Width - box.Width / 100),
                Convert.ToInt32(this.y * Program.form.Height - box.Width / 100), box.Width / 50, box.Width / 50);
        }


        public override string ToString()
        {
            string text = "Stad:\t" + name + '\t' + "ID:\t" + cityID + '\n' + "Buren:\t";
            foreach (City city in neighbours)
            {
                text += city.getname() + '\t';
            }
            return text;
        }

        // de steden kunnen zo alfabetisch gesorteerd worden
        public int CompareTo(object obj)
        {
            if (obj is City)
            {
                return (name.ToUpper().CompareTo(((City)obj).getname().ToUpper()));
            }
            else if (obj is string)
            {
                return (name.ToUpper().CompareTo(((string)obj).ToUpper()));
            }
            return -1;
        }



        /******************************
        Properties
        *******************************/

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }


        public List<City> getNeighBours()
        {
            return neighbours;
        }


        public void setStation(Station station)
        {
            this.station = station;
        }


        public Station getStation()
        {
            return station;
        }


        public string getname()
        {
            return name;
        }


        public int getID()
        {
            return cityID;
        }
    }
}
