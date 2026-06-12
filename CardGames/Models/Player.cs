using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace CardGames.Models
{
    internal class Player
    {

        //========================================
        //フィールド
        //========================================

        //・名前を持つ
        internal string Name { get; }

        //・手札を持つ
        private List<Card> _handDeck = new List<Card>();
        internal IReadOnlyList<Card> HandDeck => _handDeck;
        internal int HandCount => _handDeck.Count;

        //・プレイヤーかどうかを持つ
        internal bool IsCpu { get; }

        //・勝ち抜け済みかどうかを持つ
        internal bool IsFinished { get; set; }


        //========================================
        //コンストラクタ
        //========================================

        internal Player(string name, bool isCpu)
        {
            Name = name;
            IsCpu = isCpu;
            IsFinished = false;
        }

        //========================================
        //基本メソッド
        //========================================

            //カード追加
        internal void AddCard(Card card)
        {
            _handDeck.Add(card);
        }

        //カードの配列を指定して削除&渡す
        internal Card RemoveCardAt(int num)
        {
            if (num>=HandCount||num<0)
            {
                throw new Exception("不正な数値が指定されました。");
            }
            Card card = _handDeck[num];
            _handDeck.RemoveAt(num);
            return card;
        }


        //========================================
        //専用メソッド
        //========================================

        //勝ち抜けCPUの状態変更
        internal void MarkAsFinished()
        {
            IsFinished = true;
        }
    }
}
