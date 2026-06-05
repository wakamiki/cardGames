using CardGames.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Services
{
    internal class BabanukiGameManager
    {
        //===========================================
        //フィールド
        //===========================================

        private Deck _deck;
        private List<Card> _discardPile = new List<Card>();
        internal int DiscardCount => _discardPile.Count;
        internal IReadOnlyList<Card> DiscardPile => _discardPile;

        private List<Player> _players = new List<Player>();
        internal IReadOnlyList<Player> Players => _players;
        private int _currentTurnIndex;

        //===========================================
        //メソッド
        //===========================================

        //呼び出し元：
        //SettingForm または GameForm
        internal void StartGame()
        {
            //プレイヤーを準備
            Player player1 = new Player();
            Player cpu1 = new Player();
            Player cpu2 = new Player();
            Player cpu3 = new Player();

            //プレイヤーをリストに追加
            _players.Add(player1);
            _players.Add(cpu1);
            _players.Add(cpu2);
            _players.Add(cpu3);
            
            //山札を準備
            _deck.CreateDeck();

            //山札をシャッフル
            _deck.Shuffle();

            //山札からカードをそれぞれの手札に配りきる
            DealCardsToPlayers();

            //ペアのカードを捨て札エリアに捨てる
            foreach (Player player in _players)
            {
                RemovePairs(player);
            }
            //勝ち抜け参加者がいないかチェック


            //最初のターンを設定する
            _currentTurnIndex = 0;

        }

        //人間プレイヤーが、次の相手の手札からカードを1枚選んで引く。
        internal Player GetDrawTarget(Player player)
        {
            for (int i = Players.Count-1 ; i > 0; i--)
            {
                if (Players[i].IsFinished)
                {
                    continue;
                }
                return Players[i];
            }
            throw new Exception("カードを引けるプレイヤーがいません");
        }
        //処理内容：
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
        internal void DealCardsToPlayers()
        {
            while (_deck.RemainingCount > 0) 
            {

                foreach (Player player in _players)
                {
                    player.AddCard(_deck);
                    if (_deck.RemainingCount == 0)
                    {
                        break;
                    }
                }
            }
        }

        //初期とカードを引いた後のペア削除処理
        internal void RemovePairs(Player player)
        {
            bool pairFound = true;
            while (pairFound) {

                pairFound = false;

                //手札の手前からカード1枚とる。
                for (int i = 0; i < player.HandDeck.Count; i++)
                {
                    //残りのカードとランクが同じものが無いか探す
                    for(int j = i + 1; j < player.HandDeck.Count; j++)
                    {
                        //ジョーカーは除外
                        if (player.HandDeck[i].IsJoker || player.HandDeck[j].IsJoker)
                        {
                            continue;
                        }
                        //同じペアが見つかった場合の処理
                        if (player.HandDeck[i].Rank == player.HandDeck[j].Rank)
                        {
                            //カードを捨て札エリアに追加
                            _discardPile.Add(player.HandDeck[j]);
                            _discardPile.Add(player.HandDeck[i]);
                            //jが指している配列番号が変わらないよう
                            //必ず配列の後ろから削除して行く。
                            player.RemoveCardAt(j);
                            player.RemoveCardAt(i);
                            //whileからやり直す
                            pairFound = true;
                            break;
                        }
                    }
                    // ペアを削除した場合、外側のforも抜けてwhileの先頭に戻る
                    if (pairFound)
                    {
                        break;
                    }
                }
            }
        }

        //手札が0枚かチェック
        private bool IsHandEmpty(Player player)
        {
            if (player.HandCount == 0)
            {
                return true;
            }
            return false;
        }

        //CPU手札チェック+0枚時状態遷移
        private void CheckAndHandleCpuFinish(Player cpu)
        {
            if (!IsHandEmpty(cpu))
            {
                return;
            }
            MarkCpuAsFinishedIfHandEmpty(cpu);

        }

        //プレイヤー手札チェック+0枚時勝利演出遷移
        private void HandlePlayerWinIfHandEmpty(Player player)
        {
            if (!IsHandEmpty(player))
            {
                return;
            }


        }
    }
}
