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

        private int _handCount { get; set; }



        internal void AddCard()
        {
            _handDeck.Add(deck.DrawCard());
        }


        //メソッド名：
        //RemoveCardAt

        //役割：
        //指定した位置のカードを手札から取り出して返す。

        //入力：
        //cardIndex

        //出力：
        //取り出したCard

        //用途：
        //プレイヤーやCPUが相手の手札からカードを引くときに使う。

        //注意：
        //指定された位置が不正な場合の扱いを決める必要がある。




        //プロパティ名：
        //IsHandEmpty

        //役割：
        //手札が0枚かどうかを返す。

        //用途：
        //勝ち抜け判定に使う。

    }
}
