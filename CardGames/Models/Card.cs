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
        private Suit? _suit { get; set; }
        private Rank? _rank { get; set; }
        private bool _isJoker{ get; set; }
        private string _displayName{ get; set; }

        //========================================
        //メソッド
        //========================================

        internal Card CreateCard(Suit suit,Rank rank,string displayName)
        {
            Card card = new Card
            {
                _suit = suit,
                _rank = rank,
                _isJoker = false,
                _displayName = displayName,
            };
            return card;
        }

        internal Card CreateJoker()
        {
            Card joker = new Card
            {
                _suit = null,
                _rank = null,
                _isJoker = true,
                _displayName = "Joker",
            };
            return joker;
        }



    }
}
