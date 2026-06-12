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
        //========================================
        //フィールド
        //========================================
        internal Suit? Suit { get; }
        internal Rank? Rank { get; }
        internal bool IsJoker { get; }
        internal string DisplayName { get; }

        //========================================
        //コンストラクタ
        //========================================

        internal Card(Suit? suit, Rank? rank, bool isJoker, string displayName)
        {
            Suit = suit;
            Rank = rank;
            IsJoker = isJoker;
            DisplayName = displayName;
        }

        //========================================
        //メソッド
        //========================================

        /// <summary>
        /// 引数で渡されたカードと、自分が「ババ抜きのペア」になれるか判定
        /// </summary>
        internal bool IsPairWith(Card other)
        {
            // どちらかがnull、またはどちらかがジョーカーならペアにはならない
            if (this == null || other == null || this.IsJoker || other.IsJoker) return false;

            // 数字（Rank）が一致していればペア！
            return this.Rank == other.Rank;
        }

    }
}
