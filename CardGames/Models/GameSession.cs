using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Models
{
    internal class GameSession
    {
        //配列[0]が勝ち数、配列[1]が負け数
        internal int[] PlayerResult { get; private set; } = {0,0};

        internal void AddPlayerWin()
        {
            PlayerResult[0]++;
        }

        internal void AddPlayerLose()
        {
            PlayerResult[1]++;
        }
    }
}
