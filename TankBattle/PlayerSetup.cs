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
    public partial class PlayerSetup : Form
    {
        public PlayerSetup()
        {
            InitializeComponent();
        }


        private void playerCountSelector_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Battle game = new Battle((int)playerCountSelector.Value, (int)roundSelector.Value);
            Hide();
            GameSetupForm gameForm = new GameSetupForm(game);
            gameForm.Show();
        }
    }
}
