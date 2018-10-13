using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using T2R.Players;

namespace T2R.Forms
{
    public enum PlayerAction { Traincards, Railroad, DestinationCard, Station };
    public enum Cardoptions { Visible, Invisible };
    public partial class PlayersOptionsForm : Form
    {
        // instance variables
        private readonly List<foto> gamepics;
        private readonly FormDataObject data;
        private readonly Dictionary<string, int> enums;
        private readonly Player player;

        // constructor
        public PlayersOptionsForm(FormDataObject data, List<foto> gamepics, Dictionary<string, int> enums, Player player)
        {
            this.enums = enums;
            this.data = data;
            this.gamepics = gamepics;
            this.player = player;
            InitializeComponent();
            List<string> val = enums.Keys.ToList();
            foreach (string text in val)
            {
                cbxPossibilities.Items.Add(text);
            }
        }


        /******************************
        Methods
        *******************************/
        private void button1_Click(object sender, EventArgs e)
        {
            string text = (string)cbxPossibilities.SelectedItem;
            foreach (KeyValuePair<string, int> option in enums)
            {
                if (option.Key == text) { data.setValues(option.Value); }
            }

            this.Close();
        }

        private void PlayersOptionsForm_Load(object sender, EventArgs e)
        {
            foreach (foto pic in gamepics)
            {
                if (pic.getName().ToUpper() == "PLAYEROPTIONS")
                {
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    this.BackgroundImage = Image.FromFile(pic.getDataPath());
                }
            }

            string text = player.ToString();
            string[] splittedText = ExtensionMethods.SplitSTring(text, '\n');
            playerInfoTextbox.Clear();
            for (int i = 0; i < splittedText.Length; i++)
            {
                playerInfoTextbox.AppendText(splittedText[i]);
                playerInfoTextbox.AppendText(Environment.NewLine);
            }
            Update();
        }
    }
}
