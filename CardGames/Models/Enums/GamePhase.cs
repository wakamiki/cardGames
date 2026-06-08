using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Models.Enums
{
    internal enum GamePhase
    {
        BeforeStart,        // ゲーム開始前
        PlayerSelecting,    // プレイヤーが引くカードを選んでいる
        PlayerConfirming,   // プレイヤーが選択カードを決定する前
        CpuTurn,            // CPUターン中
        GameOver            // ゲーム終了
    }
}
