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
        List<Card> deck = new List<Card>();
        private Random _random = new Random();

        internal int RemainingCount { get; }
        //残り枚数を返す

        internal List<Card> CreateDeck()
        {
            //一旦初期化
            deck.Clear();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    //表示用文字組立
                    StringBuilder sb = new StringBuilder();
                    sb.Append(GetSuitText(suit));
                    sb.Append("-");
                    sb.Append(GetRankText(rank));

                    Card card = new Card
                    {
                        Suit = suit,
                        Rank = rank,
                        IsJoker = false,
                        DisplayName = sb.ToString(),
                    };
                    deck.Add(card);
                }
                //Console.WriteLine(decks.Count);//デバッグ用52枚になるはず

            }
            Card joker = new Card
            {
                Suit = null,
                Rank = null,
                IsJoker = true,
                DisplayName = "Joker",
            };
            //    通常カード 52枚
            //    ジョーカー 1枚
            //    合計 53枚
            return deck;
        }


        internal List<Card> Shuffle(List<Card> deck)
        {
            //Fisher-Yates シャッフルを採用する。
            for (int i = deck.Count-1; i > 0; i--)
            {
                int move = _random.Next(i + 1);
                Card tmp = deck[i];
                deck[i] = deck[move];
                deck[move] = tmp;
            }
            return deck;
        }

        //internal Card DrawCard()
        //{
        //    //山札から1枚取り出す
        //}


        //==============================================
        //補助メソッド
        //==============================================

        //Rankを表示用文字記号に変換
        internal string GetRankText(Rank rank) 
        {
            switch (rank)
            {
                case Rank.Ace:
                    return "A";

                case Rank.Jack:
                    return"J";

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
