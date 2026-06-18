using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardGames.Forms
{
    public partial class DebugForm : Form
    {
        private readonly Action _resetGame;
        private readonly Action _showWinResult;
        private readonly Action _showLoseResult;

        public DebugForm(Action resetGame, Action showWinResult, Action showLoseResult)
        {
            InitializeComponent();

            _resetGame = resetGame;
            _showWinResult = showWinResult;
            _showLoseResult = showLoseResult;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _resetGame();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart1_Click(object sender, EventArgs e)
        {
            _showWinResult();
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            _showLoseResult();
        }
    }
}
