using CardGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Services
{
    internal class BabanukiGameManager
    {


        internal void StartGame()
        {
            Player player1 = new Player();
            Player cpu1 = new Player();
            Player cpu2 = new Player();
            Player cpu3 = new Player();

            List<Player> players = new List<Player>() 
                                    { player1, cpu1, cpu2, cpu3 };

            Deck deck = new Deck();

            deck.CreateDeck();
            deck.Shuffle();

            DealCardsToPlayers(deck,players);
            RemovePairs();



        }
        //メソッド名：
        //StartGame

        //役割：
        //ババ抜きのゲーム開始準備を行う。

        //処理内容：

        //・各参加者の初期ペアを削除する
        //・最初のターンを設定する

        //呼び出し元：
        //SettingForm または GameForm

        //注意：
        //画面遷移や表示更新は行わない。


        //メソッド名：
        //DrawCardFromTarget

        //役割：
        //人間プレイヤーが、次の相手の手札からカードを1枚選んで引く。

        //処理内容：
        //・現在のターンが人間プレイヤーか確認する
        //・引く相手を取得する
        //・指定された位置のカードを相手の手札から取り出す
        //・プレイヤーの手札に追加する
        //・ペアができた場合は削除する
        //・勝ち抜け判定を行う
        //・次のターンへ進める

        //入力：
        //・cardIndex

        //出力：
        //・必要ならゲーム状態、または結果情報

        //注意：
        //どのカードボタンがクリックされたかはGameForm側で判断する。
        //GameManagerはcardIndexを受け取って処理する。



        //メソッド名：
        //ProceedCpuTurn

        //役割：
        //CPUのターンを1回進める。

        //処理内容：
        //・現在のターンがCPUか確認する
        //・次の相手を取得する
        //・相手の手札からランダムに1枚引く
        //・CPUの手札に追加する
        //・ペアができた場合は削除する
        //・勝ち抜け判定を行う
        //・次のターンへ進める

        //入力：
        //なし

        //出力：
        //・必要ならゲーム状態、または結果情報

        //注意：
        //CPUの行動は初期版ではランダム選択とする。




        //メソッド名：
        //RemovePairs

        //役割：
        //指定した参加者の手札から、同じRankのカード2枚をペアとして削除する。

        //処理内容：
        //・ジョーカーは対象外にする
        //・同じRankのカードを2枚見つける
        //・見つかったペアを手札から削除する
        //・複数ペアがある場合はすべて削除する

        //入力：
        //・対象のPlayer

        //出力：
        //・削除したペア数、またはなし

        //注意：
        //画面から直接呼ぶ処理ではなく、ゲーム開始時やカードを引いた後にGameManager内部で呼ぶ。



        //メソッド名：
        //CheckGameResult

        //役割：
        //現在のゲーム状態から、プレイヤーの勝利・敗北・継続中を判定する。

        //処理内容：
        //・手札がなくなった参加者を勝ち抜け扱いにする
        //・人間プレイヤーが勝ち抜けした場合は勝利
        //・人間プレイヤーが最後まで残った場合は敗北
        //・それ以外はゲーム継続

        //入力：
        //なし

        //出力：
        //・ゲーム結果

        //注意：
        //MessageBoxの表示はGameManagerでは行わない。
        //GameManagerは結果だけを返し、表示はGameForm側で行う。



        //メソッド名：
        //GetPlayers

        //役割：
        //参加者一覧を取得する。

        //用途：
        //GameFormがプレイヤーとCPUの手札枚数・名前を表示するために使う。



        //プロパティ名：
        //CurrentPlayer

        //役割：
        //現在のターンの参加者を取得する。

        //用途：
        //GameFormが「現在のターン」を表示するために使う。



        //プロパティ名：
        //TargetPlayer

        //役割：
        //現在のターンの参加者がカードを引く相手を取得する。

        //用途：
        //プレイヤーがどのCPUのカードを引くか、画面に表示するために使う。

        //==========================
        //補助メソッド
        //==========================

        //手札0枚時の勝ち抜け処理(CPU)
        internal void MarkCpuAsFinishedIfHandEmpty(Player cpu)
        {
            //CPUのプレイヤー情報を書き替える
            cpu.MarkAsFinished();
        }

        //手札0枚時の勝ち演出遷移(プレイヤー)

        //山札から全員にカードを配り切る
        internal void DealCardsToPlayers(Deck deck, List<Player> players)
        {
            while (deck.RemainingCount > 0) 
            {

                foreach (Player player in players)
                {
                    player.AddCard(deck);
                    if (deck.RemainingCount == 0)
                    {
                        break;
                    }
                }
            }
        }

        //初期とカードを引いた後のペア削除処理
        internal void RemovePairs(Player player)
        {
            
        }
    }
}
