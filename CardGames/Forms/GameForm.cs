using CardGames.Models;
using CardGames.Models.Enums;
using CardGames.Properties;
using CardGames.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Media; //効果音を使う場合

namespace CardGames
{
    public partial class GameForm : Form
    {
        private string _playerName;
        private int _playerCount;
        private int _cpuCount;
        //プレイヤーとフローレイアウトパネルの紐づけ辞書
        private Dictionary<Player, FlowLayoutPanel> _playerHandPanels = new Dictionary<Player, FlowLayoutPanel>();
        private BabanukiGameManager _gameManager;
        //カード画像読み込み
        private Dictionary<string, Image> _cardImages = new Dictionary<string, Image>();
        //裏面カード画像
        private Image _cardBackImage;
        //選択カード
        private PictureBox _selectedPictureBox;

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
            //フローレイアウトパネルとプレイヤーの対応辞書用意
            InitializeCpuHandPanelMap();
            //表示用カード画像準備
            LoadCardImages();
        }

        //ボタンイベント
        private void btnMainAction_Click(object sender, EventArgs e)
        {
            switch (_gameManager.CurrentPhase)
            {
                // ゲームスタート
                case GamePhase.BeforeStart:
                    // ゲーム開始処理
                    _gameManager.StartGame();
                    //画面再取得
                    UpdateDisplay();
                    //カードを引く相手だけカード選択可能にする
                    EnableTargetPlayerCardSelection();
                    break;

                // プレイヤーターンスタート
                case GamePhase.PlayerSelecting:
                    // カード未選択なので基本は何もしない
                    break;

                // プレイヤーターン:カードを引く
                case GamePhase.PlayerConfirming:
                    //カード配列番号取得
                    int cardIndex = TakeSelectedCardIndex();
                    //カードドロー
                    Card drawCard = _gameManager.PlayerTurnCardDraw(cardIndex);
                    //ターン進行
                    _gameManager.AdvanceTurn(drawCard);
                    //ゲーム進行状態更新
                    _gameManager.SetCpuTurn();
                    UpdateDisplay();
                    break;

                // CPUターンスタート
                case GamePhase.CpuTurn:
                    // CPUがカードを引く
                    Card cpuDrawCard = _gameManager.CpuTurnCardDraw();
                    //ターン進行
                    _gameManager.AdvanceTurn(cpuDrawCard);
                    UpdateDisplay();
                    //次の手番がプレイヤー時の遷移処理
                    if (_gameManager.IsPlayerTurn())
                    {
                        _gameManager.SetPlayerSelecting();
                        UpdateButtons();
                        EnableTargetPlayerCardSelection();
                    }
                    break;

                case GamePhase.GameOver:
                    // 敗北時演出　タイトルへ戻る、または再スタート
                    _gameManager.ReStart();
                    break;

                case GamePhase.GameWin:
                    //　勝利時演出 タイトルへ戻る、または再スタート
                    _gameManager.ReStart();
                    break;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SettingForm frm3 = new SettingForm();
            frm3.Show();
            this.Close();
        }

        //======================================
        //ロードイベント
        //======================================
        //カード画像全読み込み
        internal void LoadCardImages()
        {
            ClearCardDisplayAreas();
            //カード画像を辞書に追加(key==DisplayName)
            foreach (Card card in _gameManager.Deck)
            {
                string key = card.DisplayName;
                //Path.Combineは記号間違いでパスが壊れるのを防ぐため採用
                string imagePaths = Path.Combine("images", "素材ズ", "07_トランプ表", key + ".png");
                _cardImages.Add(key, Image.FromFile(imagePaths));
            }
            //裏面画像追加
            string imagePath = Path.Combine("images", "素材ズ", "05_Back.png");
            _cardBackImage = Image.FromFile(imagePath);
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
            DateOfPlayer.Text = _playerName + "のてふだ　のこり０まい";
        }
        internal string SetInitialOperationGuide()
        {
            return "操作ガイド初期文";
        }
        internal string SetLogs()
        {
            return "操作ログ初期文";
        }
        //フローレイアウトパネルとプレイヤーの対応辞書用意
        private void InitializeCpuHandPanelMap()
        {
            _playerHandPanels.Add(_gameManager.Players[0], flpPlayerHand);
            _playerHandPanels.Add(_gameManager.Players[1],flpCpu1Hand);
            _playerHandPanels.Add(_gameManager.Players[2],flpCpu2Hand);
            _playerHandPanels.Add(_gameManager.Players[3],flpCpu3Hand);
        }

        //======================================
        //GamePhase.BeforeStart
        //======================================
        //引く相手のカードを選択可能にする
        private void EnableTargetPlayerCardSelection()
        {
            foreach (Control control in _playerHandPanels[_gameManager.TargetPlayer].Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.Click += SelectableCardPictureBox_Click;
                    pictureBox.Cursor = Cursors.Hand;
                }
            }
        }

        //======================================
        //GamePhase.PlayerSelecting
        //======================================
        //カードを選択
        private void SelectableCardPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;

            //押されたカードの見た目変更
            // 前に選択していたカードの見た目を戻す
            if (_selectedPictureBox != null)
            {
                _selectedPictureBox.BorderStyle = BorderStyle.None;
            }

            // 今回クリックしたカードだけ選択状態にする
            clickedPictureBox.BorderStyle = BorderStyle.Fixed3D;

            // 選択中のPictureBoxとして保存
            _selectedPictureBox = clickedPictureBox;

            // ゲーム状態を「選択済み・決定待ち」にする
            _gameManager.SetPlayerConfirming();

            // 画面全体は作り直さず、ボタンだけ更新
            UpdateButtons();
        }

        //======================================
        //GamePhase.PlayerConfirming
        //======================================

        //ピクチャボックスからカード配列番号を返しカード選択状態を解除
        private int TakeSelectedCardIndex()
        {
            if (!int.TryParse(_selectedPictureBox.Tag.ToString(), out int cardIndex))
            {
                throw new InvalidOperationException("カードが選択されていません。");
            }
            //_selectedPictureBoxをnullにする(選択状態を解除)
            _selectedPictureBox = null;
            return cardIndex;
        }


        //======================================
        //GamePhase.CpuTurn
        //======================================

        //======================================
        //GamePhase.GameOver
        //======================================

        //======================================
        //GamePhase.GameWin
        //======================================

        //勝敗判定などなど
        internal async void ShowGameResultIfNeeded() // 20260611 工藤 async を追記
        {
            if (_gameManager.CurrentPhase == GamePhase.GameOver)
            {
                await ShowPlayerLoseResult();　 // 20260611 工藤 await を追記
                ShowMessegeBoxLose();
                //画面変更メソッド
            }
            else if (_gameManager.CurrentPhase == GamePhase.GameWin)
            {
                await ShowPlayerWinResult(); // 20260611 工藤 await を追記
                ShowMessegeBoxWin();
                //画面変更メソッド
            }
        }


        //======================================
        //共通メソッド
        //======================================
        //全体更新メソッド
        private void UpdateDisplay()
        {
            //・プレイヤー手札を描き直す
            UpdatePlayerHand(_gameManager.Players[0]);
            //・CPU手札を描き直す
            UpdateCpuHands();
            //捨て札エリアを描き直す
            UpdateFlpDiscardPile();
            //・残り枚数を更新する
            UpdateTurnGuide();
            //・そうさガイドを更新する
            //・ゲームログを更新する
            UpdateGameLog();//未完成
            //・ボタンを更新する
            UpdateButtons();
            //・勝敗状態ならモーダルやMessageBoxを出す
            ShowGameResultIfNeeded();
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
        //・CPU手札を描き直す
        private void UpdateCpuHands()
        { 
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
                    pictureBox.Tag = j;
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
            switch (_gameManager.CurrentPhase)
            {
                case GamePhase.BeforeStart:

                    break;
                case GamePhase.PlayerSelecting:

                    break;
                case GamePhase.PlayerConfirming:

                    break;
                case GamePhase.CpuTurn:

                    break;
                case GamePhase.GameOver:

                    break;
                case GamePhase.GameWin:

                    break;
            }
        }
        //・ボタンを更新する
        private void UpdateButtons()
        {
            switch (_gameManager.CurrentPhase)
            {
                case GamePhase.BeforeStart:
                    btnMainAction.Enabled = true;
                    break;
                case GamePhase.PlayerSelecting:
                    btnMainAction.Enabled = false;
                    btnMainAction.Text = "けってい";
                    break;
                case GamePhase.PlayerConfirming:
                    btnMainAction.Enabled = true;
                    break;
                case GamePhase.CpuTurn:
                    btnMainAction.Enabled = true;
                    btnMainAction.Text = "すすむ";
                    break;
                case GamePhase.GameOver:
                    btnMainAction.Enabled = false;
                    break;
                case GamePhase.GameWin:
                    btnMainAction.Enabled = false;
                    break;
            }
        }

        //勝敗後画面表示
        private void ShowResultActionButtons()
        {
            btnMainAction.Enabled = true;
            btnMainAction.Text = "もう一度あそぶ";
        }

        //======================================
        //共通演出メソッド
        //======================================
        //工藤さん担当

        //(仮)削除変更OK!
        private void ShowMessegeBoxWin()
        {
            MessageBox.Show("あなたの勝ちです！","勝利",MessageBoxButtons.OK,MessageBoxIcon.None);
        }
        private void ShowMessegeBoxLose()
        {
            MessageBox.Show("あなたの負けです。", "敗北", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        //勝ち抜けCPUの画面表示(アクティブじゃないのが分かるように)

        //カードを引く際の演出

        //カードを捨てる際の演出

        // =================================================================
        // #47_GameForm勝利時演出メソッド // 20260611 工藤
        // =================================================================
        internal async Task ShowPlayerWinResult()
        //internal void ShowPlayerWinResult()
        {

            /*効果音を使う場合 ※win_sound の準備が必要
            SoundPlayer player = new SoundPlayer(Properties.Resources.win_sound);
            player.Play();
            */

            // 1. 勝利画像をセットして、画面にバーンと表示する（操作をロック）
            pictureBox_Result.Image = Properties.Resources._08_youWin;
            pictureBox_Result.Visible = true;
            pictureBox_Result.BringToFront(); // 一番手前に持ってくる

            // 2. 2秒間（2000ミリ秒）、画面をそのまま静止（余韻の演出）
            await Task.Delay(2000);

            // 3. 画像を消して、通常の画面に戻す
            pictureBox_Result.Visible = false;
        }

        // =================================================================
        // #48_GameForm敗北時画面演出 // 20260611 工藤 
        // =================================================================
        internal async Task ShowPlayerLoseResult()
        //internal void ShowPlayerLoseResult()
        {

            /*効果音を使う場合 ※lose_sound の準備が必要
            SoundPlayer player = new SoundPlayer(Properties.Resources.lose_sound);
            player.Play();
            */

            // 1. 敗北画像をセットして、画面にバーンと表示する
            pictureBox_Result.Image = Properties.Resources._08_youLose; 
            pictureBox_Result.Visible = true; 
            pictureBox_Result.BringToFront(); 

            // 2. 2秒間（2000ミリ秒）、画面をそのまま静止
            await Task.Delay(2000);

            // 3. 画像を消して、通常の画面に戻す
            pictureBox_Result.Visible = false;
        }
    }








}
