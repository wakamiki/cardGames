using CardGames.Models;
using CardGames.Models.Enums;
using CardGames.Properties;
using CardGames.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardGames
{
    public partial class GameForm : Form
    {
        private string _playerName;
        private int _playerCount;
        private int _cpuCount;
        private BabanukiGameManager _gameManager;
        //カード画像読み込み
        private Dictionary<string, Image> _cardImages = new Dictionary<string, Image>();
        //裏面カード画像
        private Image _cardBackImage;

        //カードサイズ
        private const int CardWidth = 50;
        private const int CardHeight = 70;

        //======================================
        //イベントメソッド
        //======================================
        public GameForm(string playerName,int playerCount,int cpuCount)
        {
            InitializeComponent();
            _playerName = playerName;
            _playerCount = playerCount;
            _cpuCount = cpuCount;
            _gameManager = new BabanukiGameManager();
            _gameManager.GameSettings(_playerName,_playerCount,_cpuCount);

        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //表示用カード画像準備
            LoadCardImages();
            //各種初期化
            InitializeGameDisplay();
            //操作ガイド初期文を入力
            SetInitialOperationGuide();
            //ゲームログ初期文を入力
            SetLogs();
            //決定ボタン=かいし
            btnMainAction.Text = "かいし";
            //デッキ、プレイヤー、プレイヤーリスト準備
            _gameManager.InitializeGame();
        }

        private void btnMainAction_Click(object sender, EventArgs e)
        {
            switch (_gameManager.CurrentPhase)
            {
                case GamePhase.BeforeStart:
                    // ゲーム開始処理
                    _gameManager.StartGame();
                    //画面再取得
                    UpdateDisplay();
                    break;

                case GamePhase.PlayerSelecting:
                    // カード未選択なので基本は何もしない
                    UpdateDisplay();
                    break;

                case GamePhase.PlayerConfirming:
                    // 選択済みカードを確定して引く
                    UpdateDisplay();
                    break;

                case GamePhase.CpuTurn:
                    // CPUターンを1回進める
                    UpdateDisplay();
                    break;

                case GamePhase.GameOver:
                    // タイトルへ戻る、または再スタート
                    break;
            }

            UpdateDisplay();
        }

        //======================================
        //共通メソッド
        //======================================
        //カード画像全読み込み
        internal void LoadCardImages()
        {
            ClearCardDisplayAreas();
            //カード画像を辞書に追加(key==DisplayName)
            foreach (Card card in _gameManager.Deck)
            {
                string key = card.DisplayName;
                _cardImages.Add(key, Image.FromFile("Images/Cards/"+key+".png"));
            }
            //裏面画像追加
            _cardBackImage = Image.FromFile("Images/Cards/Back.png");
        }

        //全体更新メソッド
        private void UpdateDisplay()
        {
            UpdatePlayerHand(_gameManager.Players[0]);
            //・プレイヤー手札を描き直す
            UpdateCpuHands();
            //・CPU手札を描き直す
            UpdateFlpDiscardPile();
            //捨て札エリアを描き直す
            UpdateTurnGuide();
            //・残り枚数を更新する
            UpdateGameLog();//未完成
            //・そうさガイドを更新する
            //・ゲームログを更新する
            UpdateButtons();
            //・ボタンの有効 / 無効を更新する
            ShowGameResultIfNeeded();
//・勝敗状態ならモーダルやMessageBoxを出す
        }
        //全体更新メソッド関連小メソッド
        //・プレイヤー手札を描き直す
        private void UpdatePlayerHand(Player player)
        {
            flpPlayerHand.Controls.Clear();
            
            foreach (Card card in player.HandDeck)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = _cardImages[card.DisplayName];
                pictureBox.Width = CardWidth;
                pictureBox.Height = CardHeight;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                flpPlayerHand.Controls.Add(pictureBox);
            }
            
        }
        private void UpdateCpuHands()
        {
            //・CPU手札を描き直す
            flpCpu1Hand.Controls.Clear();
            flpCpu2Hand.Controls.Clear();
            flpCpu3Hand.Controls.Clear();

            for (int i = 1; i < _gameManager.Players.Count; i++)
            {
                for (int j = 0; j < _gameManager.Players[i].HandCount; j++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = _cardBackImage;
                    pictureBox.Width = CardWidth;
                    pictureBox.Height = CardHeight;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    switch (i)
                    {
                        case 1:
                            flpCpu1Hand.Controls.Add(pictureBox);
                            break;
                        case 2:
                            flpCpu2Hand.Controls.Add(pictureBox);
                            break;
                        case 3:
                            flpCpu3Hand.Controls.Add(pictureBox);
                            break;
                    }     
                }
            }
        }
        //捨て札エリアを描き直す
        private void UpdateFlpDiscardPile()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (Card card in _gameManager.DiscardPile)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = _cardImages[card.DisplayName];
                pictureBox.Width = CardWidth;
                pictureBox.Height = CardHeight;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }
        //・残り枚数を更新する
        private void UpdateTurnGuide()
        {
            for (int i = 0; i < _gameManager.Players.Count; i++)
            {
                int num = _gameManager.Players[i].HandCount;
                switch (i)
                {
                    case 0:
                    DateOfPlayer.Text = _playerName + "のてふだ　のこり" + num + "まい";
                    break;
                    case 1:
                    DateOfCUP1.Text = "CPU１のてふだ　のこり"+num+"まい";
                    break;
                    case 2:
                    DateOfCUP2.Text = "CPU２のてふだ　のこり" + num + "まい";
                    break;
                    case 3:
                    DateOfCUP3.Text = "CPU３のてふだ　のこり" + num + "まい";
                    break;
                }
            }
        }
        //・そうさガイドを更新する
        //・ゲームログを更新する
        private void UpdateGameLog()
        {
            //未完成
        }
        //・ボタンの有効 / 無効を更新する
        private void UpdateButtons()
        {
            switch (_gameManager.CurrentPhase)
            {
                case GamePhase.BeforeStart:
                    btnMainAction.Enabled = true;
                    break;
                case GamePhase.PlayerSelecting:
                    btnMainAction.Enabled = false;
                    break;
                case GamePhase.PlayerConfirming:
                    btnMainAction.Enabled = true;
                    break;
                case GamePhase.CpuTurn:
                    btnMainAction.Enabled = false;
                    break;
                case GamePhase.GameOver:
                    btnMainAction.Enabled = false;
                    break;
            }
        }
        //・勝敗状態ならモーダルやMessageBoxを出す
        private void ShowGameResultIfNeeded()
        {
            
        }

        //初期化メソッド
        internal void InitializeGameDisplay()
        {
            ClearCardDisplayAreas();
            InitializeMessageAreas();
            InitializeHandCountLabels();


        }
        //flpを初期化
        internal void ClearCardDisplayAreas()
        {
            flpCpu1Hand.Controls.Clear();
            flpCpu2Hand.Controls.Clear();
            flpCpu3Hand.Controls.Clear();
            flpPlayerHand.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();
        }
        //ログテキストをクリア
        internal void InitializeMessageAreas()
        {
            Operattion.Text = string.Empty;
            Logs.Text = string.Empty;
        }
        //手札枚数表示を0に
        internal void InitializeHandCountLabels()
        {
            DateOfCUP1.Text = "CPU１のてふだ　のこり０まい";
            DateOfCUP2.Text = "CPU２のてふだ　のこり０まい";
            DateOfCUP3.Text = "CPU３のてふだ　のこり０まい";
            DateOfPlayer.Text = _playerName+"のてふだ　のこり０まい";
        }
        internal string SetInitialOperationGuide()
        {
            return "操作ガイド初期文";
        }
        internal string SetLogs()
        {
            return "操作ログ初期文";
        }
    }
}
