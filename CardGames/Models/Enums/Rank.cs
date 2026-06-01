using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Models.Enums
{
    // カードの数字・役を表す enum。
    // ババ抜きでは Rank の一致でペア判定を行う。
    // 将来、数字比較を行うカードゲームを追加する可能性があるため、
    // Ace=1 ～ King=13 の基本値を割り当てている。
    // ただし、ゲームごとの強さや点数は異なるため、
    // この値は共通の強さではなくカード本来の基本値として扱う。
    internal enum Rank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
}
