using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace T2R.Forms
{
    public partial class PlayerDialog : Form
    {
        // instance variables
        private System.Drawing.Color col;
        private readonly PlayerData data;
        private readonly List<foto> pics;

        public PlayerDialog(PlayerData data, List<foto> pics)
        {
            this.data = data;
            InitializeComponent();
            this.pics = pics;
        }

        //private void button1_Click(object sender, EventArgs e)  // button voor de kleur
        //{
        //   DialogResult restult = colorDialog1.ShowDialog();
        //    if (restult == DialogResult.OK)
        //    {
        //        col = colorDialog1.Color;
        //        button1.BackColor = col;
        //    }
        //}


        /******************************
        Methods
         *******************************/
        private void btnColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();

            while (result != DialogResult.OK && colorDialog1.Color == null)
            {
                MessageBox.Show("please choose a color");
                result = colorDialog1.ShowDialog();
            }
            col = colorDialog1.Color;
            btnColor.BackColor = col;           
        }


        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1 || col == System.Drawing.Color.Empty)
            {
                MessageBox.Show("Controleer of alle waarde goed ingevuld zijn");
            }
            else
            {
                data.setValues(textBox1.Text, col);
                this.Close();
            }                   
        }


        private void PlayerDialog_Load(object sender, EventArgs e)
        {
            foreach (foto pic in pics)
            {
                if (pic.getName().ToUpper() == "WALLPAPER2")
                {
                    BackgroundImageLayout = ImageLayout.Stretch;
                    BackgroundImage = Image.FromFile(pic.getDataPath());
                }
            }
        }
    }


    public class PlayerData
    {
        // instance Variables
        private System.Drawing.Color col;
        private string name;


        /******************************
        properties
        *******************************/

        public System.Drawing.Color getColor()
        {
            return col;
        }

        public string getName()
        {
            return name;
        }

        public void setValues(string name, System.Drawing.Color col)
        {
            this.name = name;
            this.col = col;
        }
    }
}
