using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames.Models.Enums;
using CardGames.Models;

namespace CardGames.Models
{
//Deckは山札を管理するクラス。
//カード生成、シャッフル、カードの取り出し、残り枚数の確認を担当する。
//誰にどう配るかはゲームごとのルールなので、BabanukiGameManager側で行う。
    internal class Deck
    {
        private List<Card> _deck = new List<Card>();
        internal IReadOnlyList<Card> ReadDeck => _deck;
        private Random _random = new Random();

        internal int RemainingCount => _deck.Count;

        //残り枚数を返す

        internal void CreateDeck()
        {
            //一旦初期化
            _deck.Clear();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    //表示用文字組立
                    string displayName = GetSuitText(suit) + "-" + GetRankText(rank);

                    Card card = new Card(suit, rank,false,displayName);
                    _deck.Add(card);
                }
                //Console.WriteLine(decks.Count);//デバッグ用52枚になるはず

            }
            Card joker = new Card(null,null,true,"Joker");
            _deck.Add(joker);

            //    通常カード 52枚
            //    ジョーカー 1枚
            //    合計 53枚
        }


        internal void Shuffle()
        {
            //Fisher-Yates シャッフルを採用する。
            for (int i = _deck.Count-1; i > 0; i--)
            {
                int move = _random.Next(i + 1);
                Card tmp = _deck[i];
                _deck[i] = _deck[move];
                _deck[move] = tmp;
            }
        }

        //山札からカードを1枚渡す
        internal Card DrawCard()
        {
            if (RemainingCount == 0)
            {
                throw new InvalidOperationException("山札にカードがありません。");
            }
            //山札から1枚取り出す
            Card targetCard = _deck[0];
            _deck.RemoveAt(0);
            return targetCard;
        }


        //==============================================
        //補助メソッド
        //==============================================

        //Rankを表示用文字記号に変換
        internal string GetRankText(Rank rank) 
        {
            if (_deck.Count == 0)
            {
                throw new InvalidOperationException("山札が空です。");
            }
            switch (rank)
            {
                case Rank.Ace:
                    return "A";

                case Rank.Jack:
                    return "J";

                case Rank.Queen:
                    return "Q";

                case Rank.King:
                    return "K";

                default:
                    return ((int)rank).ToString();
            };
        }

        //Suitを表示用文字記号に変換
        internal string GetSuitText(Suit suit) 
        {
            switch (suit) 
            {
                case Suit.Spade:
                    return "S";

                case Suit.Diamond:
                    return "D";

                case Suit.Club:
                    return "C";

                case Suit.Heart:
                    return "H";
            }
            return "";
        }
    }
}
