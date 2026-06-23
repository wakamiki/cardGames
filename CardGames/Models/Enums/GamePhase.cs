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
        PlayerSelecting,    // プレイヤーターン(カード選択前)
        PlayerConfirming,   // プレイヤーターン(カード選択後)
        CpuTurn,            // CPUターン中
        GameOver,            // ゲーム終了(敗北)
        GameWin             //ゲーム終了(勝利)

    }
}
