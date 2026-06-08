using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using CardGames.Models;

namespace CardGames.Models
{
    internal class Player
    {

        //インスタンスを作成
        Deck deck = new Deck();

        //・名前を持つ
        private string _name {  get; set; }

        //・手札を持つ
        private List<Card> _handDeck {  get; set; }

        //・プレイヤーかどうかを持つ
        private bool _isCpu {  get; set; }

        //・勝ち抜け済みかどうかを持つ
        private bool _isFinished {  get; set; }

        private int _handCount => _handDeck.Count;

        private bool _isHandEmpty {  get; set; }


        internal void AddCard()
        {
            _handDeck.Add(deck.DrawCard());
        }

        internal Card RemoveCardAt(int num)
        {
            if (num>=_handCount|num<0)
            {
                throw new Exception("不正な数値が指定されました。");
            }
            Card card = _handDeck[num];
            return card;
        }


        //========================================
        //補助メソッド
        //========================================

    }
}
