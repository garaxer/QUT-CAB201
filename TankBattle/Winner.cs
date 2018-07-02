using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class Winner : Form
    {
        public Winner(Battle game)
        {

            InitializeComponent();
            int highscore = 0;
            String winner = "";

            //for every player in the battle find out their score
            GenericPlayer[] genericPlayers = new GenericPlayer[game.NumPlayers()];

            for (int i = 0; i < genericPlayers.Length; i++)
            {
                GenericPlayer gp = game.GetPlayerNumber(i+1);
                if (gp.GetScore() > highscore)
                {
                    highscore = gp.GetScore();
                    winner = String.Format("{0} won!", gp.PlayerName());
                } else if (gp.GetScore() == highscore)
                {
                    winner = "Tie!";
                }
                playerWon.Text = winner;
                listBox.Items.Add(String.Format("{0} ({1} Wins)",gp.PlayerName(), gp.GetScore()));
            }     
        }
        

        private void nameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
