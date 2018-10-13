using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace T2R.Forms
{
    public partial class AmountofPlayersForm : Form
    {
        // instance variables
        private List<foto> piclist;
        private int realpl = 1;
        private int coops = 0;
        private FormDataObject formdata;

        // constructor
        public AmountofPlayersForm(List<foto> piclist, FormDataObject formdata)
        {
            InitializeComponent();
            this.piclist = piclist;
            this.formdata = formdata;
            nudRealPlayer.Minimum = 0;
        }


        /******************************
        Methods
        *******************************/
        private void players_Load(object sender, EventArgs e)
        {
            foreach (foto pic in piclist)
            {
                if (pic.getName().ToUpper() == "PLAYERS")
                {
                    BackgroundImageLayout = ImageLayout.Stretch;
                    BackgroundImage = Image.FromFile(pic.getDataPath());
                }
            }
        }

     
        private void Validationbtn_Click(object sender, EventArgs e)
        {
            realpl = (int)nudRealPlayer.Value;
            coops = (int)nudCoOps.Value;
            if (realpl + coops < 2)
            {
                MessageBox.Show("please make sure that here are at least 2 players");
            }
            else
            {
                formdata.setValues(realpl, coops);
                this.Close();
            }
        }


        /******************************
        Properties
       *******************************/
        public int getRealPl()
        {
            return realpl;
        }


        public int getCoOps()
        {
            return coops;
        }
    }

    public class FormDataObject
    {
        // instance variables
        private int real;
        private int coOps;
        private object data;

        /******************************
        Properties
        *******************************/
        public int getReal()
        {
            return real;
        }


        public int getCoOps()
        {
            return coOps;
        }


        public void setValues<T>(T data) where T : IComparable
        {
            this.data = data;
        }


        public object getData()
        {
            return data;
        }


        public void setValues(int real, int coOps)
        {
            this.real = real;
            this.coOps = coOps;
        }
    }
}
