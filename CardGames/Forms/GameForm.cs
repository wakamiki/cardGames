using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardGames
{
    public partial class GameForm : Form
    {
        private string _playerName;
        private int _playerCount;
        private int _cpuCount;

        public GameForm(string playerName,int playerCount,int cpuCount)
        {
            InitializeComponent();
            _playerName = playerName;
            _playerCount = playerCount;
            _cpuCount = cpuCount;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }
    }
}
