using CardGames.Models;
using CardGames.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
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

        //フィールドセット
        internal void GameSettings(string playerName, int playerCount, int cpuCount)
        {
            _nameOfPlayer = playerName;
            _playerCount = playerCount;
            _cpuCount = cpuCount;
        }

        //===========================================
        //進行メソッド
        //===========================================

        internal void InitializeGame()
        {
            //プレイヤーを準備
            Player player1 = new Player(_nameOfPlayer, false);
            Player cpu1 = new Player("CPU1",true);
            Player cpu2 = new Player("CPU2",true);
            Player cpu3 = new Player("CPU3",true);

            //プレイヤーをリストに追加
            _players.Add(player1);
            _players.Add(cpu1);
            _players.Add(cpu2);
            _players.Add(cpu3);

            //山札を準備
            _deck.CreateDeck();
        }

        //ゲームスタート処理
        internal void StartGame()
        {
            //山札をシャッフル
            _deck.Shuffle();

            //山札からカードをそれぞれの手札に配りきる
            DealCardsToPlayers();

            //ペアのカードを捨て札エリアに捨てる
            foreach (Player player in _players)
            {
                RemoveAllPairs(player);
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

        //ターンを進める処理(返り値ゲームログ)
        internal List<string> AdvanceTurn(Card drawCard)
        {
            List<string> logs = new List<string>();
            //ペアを捨てる
            logs.Add(HandleDrawnCard(drawCard));
            //勝ち抜けがいないかチェック
            if (CheckAndHandleFinishedPlayer(_activePlayer))
            {
                logs.Add($"{_activePlayer.Name}の手札がなくなりました。");
            }
            if(CheckAndHandleFinishedPlayer(_targetPlayer))
            {
                logs.Add($"{_targetPlayer.Name}の手札がなくなりました。");
            }
                //【重要】必ずアクティブプレイヤー更新→ターゲットプレイヤー更新の順で処理すること
                //現在ターンを次の有効なプレイヤーへ進める
                _activePlayer = GetNextActivePlayer();
            //ターゲットプレイヤーも更新する
            _targetPlayer = GetDrawTarget();
            //ターゲットプレイヤーとアクティブプレイヤーが同じ時プレイヤー敗北
            if (_activePlayer==_targetPlayer)
            {
                PlayerLose();
            }
            return logs;
        }

        //CPUがカードを引く
        internal Card CpuTurnCardDraw()
        {
            //ここにカードを引く処理
            //CPUが引くカードを選択
            int CardIndex = _random.Next(_targetPlayer.HandCount);
            //カードを引く
            return _targetPlayer.RemoveCardAt(CardIndex);  
        }

        //プレイヤーがカードを引く
        internal Card PlayerTurnCardDraw(int CardIndex)
        {
            return _targetPlayer.RemoveCardAt(CardIndex);
        }

        //==========================
        //補助メソッド
        //==========================

        //山札から全員にカードを配り切る
        internal void DealCardsToPlayers()
        {
            while (_deck.RemainingCount > 0)
            {
                foreach (Player player in _players)
                {
                    if (_deck.RemainingCount == 0)
                    {
                        break;
                    }
                    player.AddCard(_deck.DrawCard());
                }
            }
        }

        //手札チェック処理+振り分け
        internal bool CheckAndHandleFinishedPlayer(Player player)
        {
            if (player.HandCount == 0)
            {
                if (player.IsCpu)
                {
                    MarkCpuAsFinished(player);
                    return true;
                }
                else
                {
                    PlayerWin(player);
                    return false;
                }
            }
            return false;
        }

        //ペア削除処理 全ペア確認
        internal void RemoveAllPairs(Player player)
        {
            //手札の手前からカード1枚とる。(配列最後のカードはとらない)
            for (int i = 0; i < player.HandCount-1; i++)
            {
                //残りのカードとランクが同じものが無いか探す
                for (int j = i+1; j < player.HandCount; j++)
                {
                    if (player.HandDeck[i].IsPairWith(player.HandDeck[j]))
                    {
                        //同じペアが見つかった場合の処理
                        //カードを捨て札エリアに追加
                        _discardPile.Add(player.HandDeck[j]);
                        _discardPile.Add(player.HandDeck[i]);
                        //jが指している配列番号が変わらないよう
                        //必ず配列の後ろから削除して行く。
                        player.RemoveCardAt(j);
                        player.RemoveCardAt(i);
                        i--;
                        break;
                    }
                }  
            }
        }

        //ペア削除処理とドローカードを手札に加える　引いたカードと手札確認　
        internal string HandleDrawnCard(Card drawCard)
        {
            string log;
            //手札の手前からカード1枚とる。(配列最後のカードはとらない)
            for (int i = 0; i < _activePlayer.HandCount-1; i++)
            {
                if (drawCard.IsPairWith(_activePlayer.HandDeck[i]))
                {
                    //同じペアが見つかった場合の処理
                    //カードを捨て札エリアに追加
                    _discardPile.Add(_activePlayer.HandDeck[i]);
                    _discardPile.Add(drawCard);
                    _activePlayer.RemoveCardAt(i);

                    return log = $"{drawCard.Rank}のペアを捨てました。";
                }
            }
            _activePlayer.AddCard(drawCard);
            if (_activePlayer.IsCpu)
            {
                return log = "カードを手札に加えました。";
            }
            return log = $"{drawCard.DisplayName}を手札に加えました。";
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
            for (int i = 0;i< _players.Count; i++)
            {
                if (_players[nextPlayerIndex].IsFinished) 
                {
                    nextPlayerIndex = (nextPlayerIndex + 1) % _players.Count;
                }
                else
                {
                    break;
                }
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
            int targetIndex = (playerIndex - 1 + _players.Count) % _players.Count;
            //引く相手が勝ち抜け状態のプレイヤーじゃなくなるまで繰り返す
            for (int i=0;i< _players.Count();i++)
            {
                if (_players[targetIndex].IsFinished)
                {
                    targetIndex = (targetIndex - 1 + _players.Count) % _players.Count;
                }
                else
                {
                    break;
                }
            }
            return _players[targetIndex];
        }
        //プレイヤーのindexナンバーを返す
        private int GetPlayerIndex(Player player)
        {
            int index = _players.IndexOf(player);
            return index;
        }

        //次の手番がプレイヤーかどうかチェック
        internal bool IsPlayerTurn()
        {
            //CPU==false
            if (_activePlayer.IsCpu)
            {
                return false;
            }
            //player==true
            return true;
        }

        //手札0枚時の勝ち抜け処理(CPU)
        internal void MarkCpuAsFinished(Player cpu)
        {
            //CPUのプレイヤー情報を書き替える
            cpu.MarkAsFinished();
        }


        //=====================================
        //ゲーム進行状態変更メソッド
        //=====================================
        //0枚時勝利状態へ遷移
        private void PlayerWin(Player player)
        {
            _currentPhase = GamePhase.GameWin;
        }
        private void PlayerLose()
        {
            _currentPhase = GamePhase.GameOver;
        }
        internal void SetPlayerSelecting()
        {
            _currentPhase = GamePhase.PlayerSelecting;
        }
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
