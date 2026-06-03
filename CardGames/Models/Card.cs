using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames.Models.Enums;

namespace CardGames.Models
{
    // ジョーカーにはスートやランクが存在しないため、
    // Suit と Rank は nullable として定義する。
    // 通常カードでは Suit / Rank に値を持たせ、
    // ジョーカーでは Suit / Rank を null、IsJoker を true として扱う。
    internal class Card
    {
        internal Suit? Suit { get; set; }
        internal Rank? Rank { get; set; }
        internal bool IsJoker{ get; set; }
        internal string DisplayName{ get; set; }
        // TODO: 後でdeckクラス以外からは値を変更できなくする。
    }
}
