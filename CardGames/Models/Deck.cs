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
        List<Card> decks = new List<Card>();

        internal int RemainingCount { get; }
         //残り枚数を返す

        //internal List<Card> CreateDeck()
        //{
        //    //・4種類のスートを回す
        //    //・各スートごとにAce〜Kingまで作る
        //    //・最後にJokerを1枚追加する
        //    //    通常カード 52枚
        //    //    ジョーカー 1枚
        //    //    合計 53枚
        //}

        //internal List<Card> ShuffleDeck()
        //{
        //    //シャッフルする
        //}

        //internal Card DrawCard()
        //{
        //    //山札から1枚取り出す
        //}
    }
}
