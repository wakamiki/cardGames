using CardGames.Models;
using CardGames.Models.Enums;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Services
{
    internal class BabanukiGameManager
    {
        //===========================================
        //フィールド
        //===========================================

        private Deck _deck = new Deck();
        internal IReadOnlyList<Card> Deck => _deck.ReadDeck;
        private List<Card> _discardPile = new List<Card>();
        internal int DiscardCount => _discardPile.Count;
        internal IReadOnlyList<Card> DiscardPile => _discardPile;

        private List<Player> _players = new List<Player>();
        internal IReadOnlyList<Player> Players => _players;
        //現在のターン数
        private int _turnIndex=0;
        //誰の手番か
        private Player _activePlayer;
        internal Player ActivePlayer => _activePlayer;
        
        //カードを引く相手
        private Player _targetPlayer;
        internal Player TargetPlayer => _targetPlayer;

        //ゲーム進行状態
        private GamePhase _currentPhase = GamePhase.BeforeStart;
        internal GamePhase CurrentPhase => _currentPhase;
        private string _nameOfPlayer;
        private int _playerCount;
        private int _cpuCount;
        //CPU処理用ランダム用意
        private Random _random = new Random();


        //===========================================
        //メソッド
        //===========================================

        internal void GameSettings(string playerName, int playerCount, int cpuCount)
        {
            _nameOfPlayer = playerName;
            _playerCount = playerCount;
            _cpuCount = cpuCount;
        }

        internal void InitializeGame()
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
        }

        //呼び出し元：
        //GameForm
        internal void StartGame()
        {
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
            foreach (var player in _players)
            {
                CheckAndHandleFinishedPlayer(player);
            }

            //最初のプレイヤーを指定
            _activePlayer = _players[0];

            //引く相手を指定
            _targetPlayer = GetDrawTarget();

            //最初のターンを設定する
            _turnIndex = 1;
            //ゲーム進行状態を変更
            _currentPhase = GamePhase.PlayerSelecting;

        }

        //ターンを進める処理
        internal void AdvanceTurn()
        {
            //ペアを捨てる
            RemovePairs(_activePlayer);
            //勝ち抜けがいないかチェック
            CheckAndHandleFinishedPlayer(_activePlayer);
            CheckAndHandleFinishedPlayer(_targetPlayer);

            //【重要】必ずアクティブプレイヤー更新→ターゲットプレイヤー更新の順で処理すること
            //現在ターンを次の有効なプレイヤーへ進める
            GetNextActivePlayer();
            //ターゲットプレイヤーも更新する
            GetDrawTarget();

            //次の手番がプレイヤー時の処理
            if (!_activePlayer.IsCpu)
            {
                _currentPhase = GamePhase.PlayerSelecting;
            }
            //次の手番がCPU
            else
            {
                return;
            }
        }

        //次の手番のプレイヤーを返す
        internal Player GetNextActivePlayer()
        {
            //アクティブプレイヤーのIndexOf % 全プレイヤー人数
            //IndexOf = 0+1 → 余り1
            //IndexOf = 1+1 → 余り2
            //IndexOf = 2+1 → 余り3
            //IndexOf = 3+1 → 余り0
            int playerIndex = GetPlayerIndex(_activePlayer);
            int nextPlayerIndex = (playerIndex + 1) % _players.Count;
            //次のプレイヤーが勝ち抜け状態のプレイヤーじゃなくなるまで繰り返す
            while (_players[nextPlayerIndex].IsFinished)
            {
                nextPlayerIndex = (nextPlayerIndex + 1) % _players.Count();
            }
            return _players[nextPlayerIndex];
        }

        //プレイヤーが、カードを引く相手を返す
        internal Player GetDrawTarget()
        {
            //((activeIndex+全プレイヤー人数)-1) % 全プレイヤー人数
            //マイナスの数字にならないようにプレイヤー人数を足す
            //activeIndex = 0 → targetIndex = 3
            //activeIndex = 1 → targetIndex = 0
            //activeIndex = 2 → targetIndex = 1
            //activeIndex = 3 → targetIndex = 2
            int playerIndex = GetPlayerIndex(_activePlayer);
            int targetIndex = (playerIndex - 1 + _players.Count) % _players.Count();
            //引く相手が勝ち抜け状態のプレイヤーじゃなくなるまで繰り返す
            while (_players[targetIndex].IsFinished)
            {
                targetIndex = (playerIndex - 1 + _players.Count) % _players.Count();
            }
            return _players[targetIndex];
        }

        //CPUターンを進める
        internal void ProcessCpuTurn()
        {
            //ここにカードを引く処理
            AdvanceTurn();
        }

        //ペア削除処理
        internal void RemovePairs(Player player)
        {
            bool pairFound = true;
            while (pairFound)
            {

                pairFound = false;

                //手札の手前からカード1枚とる。
                for (int i = 0; i < player.HandDeck.Count; i++)
                {
                    //残りのカードとランクが同じものが無いか探す
                    for (int j = i + 1; j < player.HandDeck.Count; j++)
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

        //手札チェック処理+振り分け
        internal void CheckAndHandleFinishedPlayer(Player player)
        {

            if (player.HandCount==0)
            {
                if (player.IsCpu)
                {
                    MarkCpuAsFinished(player);
                }
                else
                {
                    PlayerWin(player);
                }
            }
            return;
        }


        //山札から全員にカードを配り切る
        internal void DealCardsToPlayers()
        {
            while (_deck.RemainingCount > 0)
            {

                foreach (Player player in _players)
                {
                    player.AddCard(_deck.DrawCard());
                    if (_deck.RemainingCount == 0)
                    {
                        break;
                    }
                }
            }
        }


        //==========================
        //補助メソッド
        //==========================

        //プレイヤーのindexナンバーを返す
        private int GetPlayerIndex(Player player)
        {
            int index = _players.IndexOf(player);
            return index;
        }

        //手札0枚時の勝ち抜け処理(CPU)
        internal void MarkCpuAsFinished(Player cpu)
        {
            //CPUのプレイヤー情報を書き替える
            cpu.MarkAsFinished();
        }

        //0枚時勝利演出遷移
        private void PlayerWin(Player player)
        {
            _currentPhase = GamePhase.GameWin;
            //勝利演出メソッドへ
        }

        //ゲーム進行状態を変更
        internal void SetPlayerConfirming()
        {
            _currentPhase = GamePhase.PlayerConfirming;
        }
        internal void SetCpuTurn()
        {
            _currentPhase = GamePhase.CpuTurn;
        }
    }
}
