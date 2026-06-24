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

        //現在のプレイヤー名
        private string _nameOfPlayer;
        //参加者人数
        private int _playerCount;
        //参加CCPU数
        private int _cpuCount;

        //CPU処理用ランダム用意
        private Random _random = new Random();

        //ゲーム用デッキを用意
        private Deck _deck = new Deck();
        //デッキ取得getter
        internal IReadOnlyList<Card> Deck => _deck.ReadDeck;

        //捨て札エリア用意
        private List<Card> _discardPile = new List<Card>();
        //捨て札エリアgetter
        internal IReadOnlyList<Card> DiscardPile => _discardPile;

        //参加者リスト
        private List<Player> _players = new List<Player>();
        //参加者リストgetter
        internal IReadOnlyList<Player> Players => _players;

        //誰の手番か
        private Player _activePlayer;
        //ActivePlayer getter
        internal Player ActivePlayer => _activePlayer;
        
        //カードを引く相手
        private Player _targetPlayer;
        //TargetPlayer getter
        internal Player TargetPlayer => _targetPlayer;

        //現在のゲーム進行状態
        private GamePhase _currentPhase = GamePhase.BeforeStart;
        //ゲーム進行状態 getter
        internal GamePhase CurrentPhase => _currentPhase;

        //フィールドコンストラクタ
        internal void GameSettings(string playerName, int playerCount, int cpuCount)
        {
            _nameOfPlayer = playerName;
            _playerCount = playerCount;
            _cpuCount = cpuCount;
        }

        //===========================================
        //初期化処理
        //===========================================

        //ロードイベント時準備
        internal void InitializeGame()
        {
            //状態を初期化
            _players.Clear();
            _discardPile.Clear();
            _activePlayer = null;
            _targetPlayer = null;
            _currentPhase = GamePhase.BeforeStart;

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

        //===========================================
        //ゲーム開始処理
        //===========================================

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
                MarkPlayerAsFinishedIfNoCards(player);
            }

            //最初のプレイヤーを指定
            _activePlayer = _players[0];

            //引く相手を指定
            _targetPlayer = GetDrawTarget();

            //ゲーム進行状態を変更
            SetPlayerSelecting();
        }

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

        // ==============================
        // ターン管理
        // ==============================

        //ターンを進める処理(返り値ゲームログ)
        internal List<string> AdvanceTurn(Card drawCard)
        {
            //ゲームログ記録用リストを用意
            List<string> logs = new List<string>();
            string tmp;

            //ペアを捨てる
            logs.Add(HandleDrawnCard(drawCard));

            //CPU勝ち抜けがいないかチェック
            tmp = MarkPlayerAsFinishedIfNoCards(_activePlayer);
            if (!string.IsNullOrEmpty(tmp))
            {
                logs.Add(tmp);
                tmp = null;
            }
            tmp = MarkPlayerAsFinishedIfNoCards(_targetPlayer);
            if (!string.IsNullOrEmpty(tmp))
            {
                logs.Add(tmp);
                tmp = null;
            }

            // ゲーム自体の勝敗判定
            EveluateGameResult();
            // 勝敗が決まったら、これ以上ターンを進めない
            if (IsGameEnded())
            {
                return　logs;
            }

            //【重要】必ずアクティブプレイヤー更新→ターゲットプレイヤー更新の順で処理すること
            //現在ターンを次の有効なプレイヤーへ進める
            _activePlayer = GetNextActivePlayer();

            //ターゲットプレイヤーも更新する
            _targetPlayer = GetDrawTarget();

            //ゲームログを返す
            return logs;
        }

        // ==============================
        // カードを引く処理
        // ==============================
        
        //CPUがカードを引く
        internal Card CpuTurnCardDraw()
        {
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

        // ==============================
        // ペア判定・捨て札処理
        // ==============================

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
            //ゲームログ用意
            string log;

            //引数のカードと手札を比べていく
            for (int i = 0; i < _activePlayer.HandCount; i++)
            {
                //もしも引いたカードと対象のカードが同じスートなら
                if (drawCard.IsPairWith(_activePlayer.HandDeck[i]))
                {
                    //同じペアが見つかった場合の処理
                    //カードを捨て札エリアに追加
                    _discardPile.Add(_activePlayer.HandDeck[i]);
                    _discardPile.Add(drawCard);

                    //手札から捨てたカードを削除
                    _activePlayer.RemoveCardAt(i);

                    //ゲームログを返す
                    return log = $"{drawCard.Rank}のペアを捨てました。";
                }
            }

            //ペアのカードが無ければ手札に加える
            _activePlayer.AddCard(drawCard);

            //CPUであればカードの詳細は伏せる
            if (_activePlayer.IsCpu)
            {
                return log = "カードを手札に加えました。";
            }

            //ゲームログを返す
            return log = $"{drawCard.DisplayName}を手札に加えました。";
        }

        // ==============================
        // プレイヤー状態判定
        // ==============================

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

        //プレイヤーのindexナンバーを返す(手番判定に使用)
        private int GetPlayerIndex(Player player)
        {
            int index = _players.IndexOf(player);
            return index;
        }

        //CPU勝ち抜け時処理(返り値ゲームログ)
        internal string MarkPlayerAsFinishedIfNoCards(Player player)
        {
            string log;
            //trueでCPU勝ち抜け状態　falseでそれ以外
            if (player.HandCount == 0)
            {
                if (player.IsCpu)
                {
                    player.MarkAsFinished();
                    log = ($"{player.Name}の手札がなくなりました。");

                }
            }
            return null;
        }

        // ==============================
        // ゲーム終了判定
        // ==============================



        //ゲームの勝敗判定
        internal void EveluateGameResult()
        {
            //すでにゲームの勝敗が決まっていたら何もしない
            if (IsGameEnded())
            {
                return;
            }

            //①人間の参加者設定
            Player humanPlayer = null;

            foreach (Player player in _players)
            {
                //CPU判定を使って人間の参加者を指定
                if (!player.IsCpu)
                {
                    humanPlayer = player;
                    break;
                }
            }

            //CPUのみなら判定しない
            if (humanPlayer == null)
            {
                return;
            }

            //②プレイヤー本人の手札が０枚なら勝利状態へ進行
            if (humanPlayer.HandCount == 0 || humanPlayer.IsFinished)
            {
                PlayerWin();
                return;
            }

            //③現在勝ち抜けていない参加者人数
            int remainingPlayerCount = 0;

            //④最後に残ったプレイヤーを設定
            Player lastRemainingPlayer = null;

            foreach(Player player in _players)
            {
                //プレイヤーが勝ち抜け状態じゃない場合
                if (!player.IsFinished && player.HandCount > 0)
                {
                    //勝ち抜けでない参加者カウンターを増やす
                    remainingPlayerCount++;

                    //foreachで指定されている参加者を代入
                    lastRemainingPlayer = player;
                }
            }

            //手札を持って残っているのが人間の参加者一人なら敗北
            if (remainingPlayerCount == 1 && lastRemainingPlayer == humanPlayer)
            {
                PlayerLose();
            }
        }

        //ゲームの決着がついているか判定
        internal bool IsGameEnded()
        {
            return _currentPhase == GamePhase.GameOver
                || _currentPhase == GamePhase.GameWin;
        }

        //=====================================
        //ゲーム進行状態変更処理
        //=====================================

        internal void PlayerWin()
        {
            _currentPhase = GamePhase.GameWin;
        }

        internal void PlayerLose()
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

        internal void SetBeforeStart()
        {
            _currentPhase = GamePhase.BeforeStart;
        }
    }
}
