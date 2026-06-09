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
        private string _name {  get; set; }

        //・手札を持つ
        private List<Card> _handDeck = new List<Card>();
        internal IReadOnlyList<Card> HandDeck => _handDeck;

        //・プレイヤーかどうかを持つ
        private bool _isCpu {  get; set; }
        internal bool IsCpu => _isCpu;

        //・勝ち抜け済みかどうかを持つ
        private bool _isFinished {  get; set; }
        internal bool IsFinished => _isFinished;

        internal int HandCount => _handDeck.Count;


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
            if (num>=HandCount|num<0)
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
            _isFinished = true;
        }
    }
}
