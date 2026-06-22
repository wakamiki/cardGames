using CardGames.Forms;
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
using System.Runtime;
using System.Runtime.Remoting.Messaging;
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
        private GameSession _gameSession;
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

        // 20260622 工藤 レビュー指摘対応 №8
        // ×ボタンで「戻る」と同じ動きにする
        private bool _isBacking = false;

        // 20260622 工藤 レビュー指摘対応 No.10 ゲームログを管理するリスト
        private List<string> _gameLogs = new List<string>();

        //デバック用
        private DebugForm _debugForm;

        private readonly Keys[] _debugCommand =
{
    Keys.Up,
    Keys.Up,
    Keys.Down,
    Keys.Down,
    Keys.Left,
    Keys.Right,
    Keys.Left,
    Keys.Right
};

        private int _debugCommandIndex = 0;


        // 20260619 工藤*UI改善* プレイヤーと対応する名前ラベルを結びつける辞書
        private Dictionary<Player, Label> _playerLabels = new Dictionary<Player, Label>();


        // =================================================================
        // #41_gameForm画面のボタンやログ表示の見た目を作りこむ // 20260615 工藤
        //  手番プレーヤーに背景色を付ける演出
        // =================================================================
        private Dictionary<Player, Panel> _activeIndicatorPanels = new Dictionary<Player, Panel>();

        //======================================
        //イベントメソッド
        //======================================
        internal GameForm(string playerName,int playerCount,int cpuCount,GameSession gameSession)
        {
            InitializeComponent();
            _playerName = playerName;
            _playerCount = playerCount;
            _cpuCount = cpuCount;
            _gameSession = gameSession;
            _gameManager = new BabanukiGameManager();
            _gameManager.GameSettings(_playerName,_playerCount,_cpuCount);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

            // =================================================================
            // #41_gameForm画面のボタンやログ表示の見た目を作りこむ // 20260615 工藤
            // フォーム全体の背景に壁紙画像をセットする
            // =================================================================
            this.BackgroundImage = Image.FromFile(Path.Combine("images", "素材ズ", "04_wallpaper.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch; // 背景画像サイズの自動調整[Stretch]

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

            // #60 リスタート実装  // 20260617 工藤 指摘対応
            // ●勝●敗の表示
            lblResults.Text = $"{_playerName}さんの戦績：" +
                $"{_gameSession.PlayerResult[0]}勝 {_gameSession.PlayerResult[1]}敗";


            // 20260616 工藤 不具合対応 ここから

            // 1. 勝敗画像の設定（最前面へ）
            this.Controls.Add(pictureBox_Result);
            pictureBox_Result.Parent = this;
            pictureBox_Result.Location = new Point(0, 0);
            pictureBox_Result.Size = this.ClientSize;
            // 20260617 工藤 指摘対応　Zoom ⇒StretchImage　
            // pictureBox_Result.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_Result.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Result.Visible = false;
            pictureBox_Result.BringToFront();

            /* 20260619 工藤*UI改善 不要部品を削除 START
                //// 2. 背景パネルを一度フォームのコントロールに追加
                //this.Controls.Add(pnl_Active_User);
                //this.Controls.Add(pnl_Active_CPU1);
                //this.Controls.Add(pnl_Active_CPU2);
                //this.Controls.Add(pnl_Active_CPU3);
                ////  3. 手札パネルの位置とサイズを背景パネルと完全に一致させる
                //pnl_Active_User.Location = flpPlayerHand.Location;
                //pnl_Active_User.Size = flpPlayerHand.Size;
                //pnl_Active_CPU1.Location = flpCpu1Hand.Location;
                //pnl_Active_CPU1.Size = flpCpu1Hand.Size;
                //pnl_Active_CPU2.Location = flpCpu2Hand.Location;
                //pnl_Active_CPU2.Size = flpCpu2Hand.Size;
                //pnl_Active_CPU3.Location = flpCpu3Hand.Location;
                //pnl_Active_CPU3.Size = flpCpu3Hand.Size;
             20260619 工藤*UI改善 不要部品を削除 END */


            //  🌟 4. 【修正！】重ね順を「手札が手前、黄色が後ろ」に完全固定！
            // BringToFrontを一度すべてやめて、手札を「手前に引き上げる」命令だけに絞ります
            flpPlayerHand.BringToFront();
            flpCpu1Hand.BringToFront();
            flpCpu2Hand.BringToFront();
            flpCpu3Hand.BringToFront();

            // 20260619 工藤*UI改善 不要部品を削除
            //削除// 🌟 5. 大理石の壁紙よりも手前に黄色パネルを置くため、壁紙（Form本体）を最背面へ
            //削除//this.SendToBack();

            // 6. 勝敗画像はさらにその手前にしたいので、最後に再度 BringToFront
            pictureBox_Result.BringToFront();

            // 20260616 工藤 不具合対応 ここまで

            //削除 // 20260622 工藤 レビュー指摘対応 №7/20260619 工藤*UI改善 ×ボタンで「戻る」と同じ動きにする 
            //削除 // 20260622 工藤 レビュー指摘対応 №7//this.Activate();

        }

        //ボタンイベント
        private void btnMainAction_Click(object sender, EventArgs e)
        {
            switch (_gameManager.CurrentPhase)
            {
                // ゲームスタート
                case GamePhase.BeforeStart:
                    //ゲームログ更新
                    UpdateGameLog();
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
                    //ゲームログ更新
                    AddCardDrawLog(drawCard);
                    //ターン進行
                    List<string> logs = _gameManager.AdvanceTurn(drawCard);
                    //ゲームログ更新
                    UpdateAdvanceTurnLogs(logs);
                    //ゲーム進行状態更新
                    _gameManager.SetCpuTurn();
                    //画面更新
                    UpdateDisplay();
                    break;

                // CPUターンスタート
                case GamePhase.CpuTurn:
                    // CPUがカードを引く
                    Card cpuDrawCard = _gameManager.CpuTurnCardDraw();
                    //ゲームログ更新
                    AddCardDrawLog(cpuDrawCard);
                    //ターン進行
                    List<string> cpuLogs = _gameManager.AdvanceTurn(cpuDrawCard);
                    //ゲームログ更新
                    UpdateAdvanceTurnLogs(cpuLogs);
                    //次の手番がプレイヤー時の遷移処理
                    if (_gameManager.IsPlayerTurn())
                    {
                        _gameManager.SetPlayerSelecting();
                        // UpdateButtons(); // 20260616 工藤　不具合対応
                        // EnableTargetPlayerCardSelection();　// 20260616 工藤　不具合対応
                    }
                    // 画面全体を更新
                    UpdateDisplay();
                    if (_gameManager.CurrentPhase == GamePhase.PlayerSelecting)　// 20260616 工藤　不具合対応
                    {
                        EnableTargetPlayerCardSelection();　// 20260616 工藤　不具合対応
                    }
                    break;

                case GamePhase.GameOver:
                    // 敗北時　敗北数記録　タイトルへ戻る、または再スタート
                    _gameSession.AddPlayerLose();
                    ReStart();
                    break;

                case GamePhase.GameWin:
                    //　勝利時　勝利数記録 タイトルへ戻る、または再スタート
                    _gameSession.AddPlayerWin();
                    ReStart();
                    break;
            }
        }

        //削除// 20260619 工藤*UI改善 ×ボタンで「戻る」と同じ動きにする
        //削除// private bool _isBacking = false;

        // 20260619 工藤*UI改善 ×ボタンで「戻る」と同じ動きにする　メソッド追加
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (_isBacking) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                // ×ボタン本来の「アプリを消し去る動き」を一旦キャンセル
                e.Cancel = true;
                // 「戻るボタン」押下時と同じ挙動にする
                btnBack_Click(sender, e);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _isBacking = true; // 20260619 工藤*UI改善 ×ボタンで「戻る」と同じ動きにする
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
            flpThrown.Controls.Clear();
        }
        //ログテキストをクリア
        internal void InitializeMessageAreas()
        {
            Operation.Text = string.Empty;
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
        internal void SetInitialOperationGuide()
        {
            Operation.Text = "▶「ゲーム開始」ボタンを押してください。";
        }
        internal void SetLogs()
        {

            // // 20260622 工藤 レビュー指摘対応 No.10 初期化時もリストと画面をきれいにクリアする
            //削除//Logs.Text = "";
            _gameLogs.Clear();
            Logs.Text = string.Empty;


        }
        //フローレイアウトパネルとプレイヤーの対応辞書用意
        private void InitializeCpuHandPanelMap()
        {
            // パネルとプレイヤーの紐づけ
            _playerHandPanels.Add(_gameManager.Players[0], flpPlayerHand);
            _playerHandPanels.Add(_gameManager.Players[1],flpCpu1Hand);
            _playerHandPanels.Add(_gameManager.Players[2],flpCpu2Hand);
            _playerHandPanels.Add(_gameManager.Players[3],flpCpu3Hand);

            // =================================================================
            // #41_gameForm画面のボタンやログ表示の見た目を作りこむ // 20260615 工藤
            //削除//20260619 工藤*UI改善*不要コメント 手番プレーヤーに背景色を付ける演出　を追加 
            //削除// 工藤*UI改善*不要コメントプレイヤーと手番インジケータ[pnl_Active]の紐づけ
            // =================================================================

                //削除// 20260619 工藤*UI改善 不要部品を削除
                //削除//_activeIndicatorPanels.Add(_gameManager.Players[0], pnl_Active_User); 
                //削除//_activeIndicatorPanels.Add(_gameManager.Players[1], pnl_Active_CPU1); 
                //削除//_activeIndicatorPanels.Add(_gameManager.Players[2], pnl_Active_CPU2); 
                //削除//_activeIndicatorPanels.Add(_gameManager.Players[3], pnl_Active_CPU3);

            // 20260619 工藤*UI改善* InitializeCpuHandPanelMap()　プレイヤーとラベルの紐づけを登録
            _playerLabels.Add(_gameManager.Players[0], DateOfPlayer);
            _playerLabels.Add(_gameManager.Players[1], DateOfCUP1);
            _playerLabels.Add(_gameManager.Players[2], DateOfCUP2);
            _playerLabels.Add(_gameManager.Players[3], DateOfCUP3);

                //削除// 20260619 工藤*UI改善 不要部品を削除
                //削除//// 背景パネル位置固定　
                //削除/// 20260615 工藤　#41不具合対応
                //削除////pnl_Active_User.Location = flpPlayerHand.Location;
                //削除////pnl_Active_User.Size = flpPlayerHand.Size;
                //削除// //pnl_Active_CPU1.Location = flpCpu1Hand.Location;
                //削除////pnl_Active_CPU1.Size = flpCpu1Hand.Size;
                //削除////pnl_Active_CPU2.Location = flpCpu2Hand.Location;
                //削除////pnl_Active_CPU2.Size = flpCpu2Hand.Size;
                //削除///pnl_Active_CPU3.Location = flpCpu3Hand.Location;
                //削除////pnl_Active_CPU3.Size = flpCpu3Hand.Size;

            // 初期化：最初は全員の手番背景を「透明」にしておく
            // 20260615 工藤　#41不具合対応
            foreach (var panel in _activeIndicatorPanels.Values)
            {
                panel.BackColor = Color.Transparent;
            }

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
                    pictureBox.Click -= SelectableCardPictureBox_Click;
                    pictureBox.Click += SelectableCardPictureBox_Click;
                    pictureBox.Cursor = Cursors.Hand;

                    //20260619 工藤*UI改善* CPUエリアのカード選択時の演出
                    pictureBox.MouseEnter -= Card_MouseEnter;
                    pictureBox.MouseEnter += Card_MouseEnter;

                    // 20260622 工藤 レビュー指摘対応 No.9 カーソル演出に伴いGeminiよりtypo指摘
                    //削除// pictureBox.MouseEnter -= Card_MouseLeave;
                    //削除// pictureBox.MouseEnter += Card_MouseLeave;
                    pictureBox.MouseLeave -= Card_MouseLeave; // MouseLeave に修正
                    pictureBox.MouseLeave += Card_MouseLeave; // MouseLeave に修正

                }
            }
        }

                //削除///* 20260619 工藤*UI改善 不要部品を削除 START
                //// =================================================================
                //// #41_gameForm画面のボタンやログ表示の見た目を作りこむ // 20260615 工藤
                //// 手番プレーヤーに背景色を付ける演出　を追加 
                //// プレイヤーと手番インジケータ[pnl_Active]の紐づけ
                //// =================================================================
                //private void UpdateActivePlayerIndicator()
                //{
                //  // 一度全員のインジケータを非表示にする
                //foreach (var panel in _activeIndicatorPanels.Values)
                //{
                //    panel.Visible = false;
                //}

                //// 現在の手番プレイヤーのインジケータを表示する
                //if (_activeIndicatorPanels.ContainsKey(_gameManager.ActivePlayer))
                //{
                //    _activeIndicatorPanels[_gameManager.ActivePlayer].Visible = true;
                //}

                //// #60 リスタート実装  // 20260615 工藤 による追加分
                //// ●勝●敗の表示
                //lblResults.Text = $"{_playerName}さんの戦績：" +
                //  $"{_gameSession.PlayerResult[0]}勝 {_gameSession.PlayerResult[1]}敗";

                //}
                //削除//    20260619 工藤*UI改善 不要部品を削除 END


        //======================================
        //GamePhase.PlayerSelecting
        //======================================
        //カードを選択
        private void SelectableCardPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;

            // =================================================================
            // #41_gameForm画面のボタンやログ表示の見た目を作りこむ // 20260615 工藤
            // 手番プレーヤーに背景色を付ける演出　を追加 
            // 手番インジケータを更新
            // =================================================================
            //UpdateActivePlayerIndicator();

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
                // ShowMessegeBoxWin();  // 20260616 工藤 移動 
                //画面変更メソッド
            }
        }

        //リスタート処理
        private void ReStart()
        {
            // 20260622 工藤 レビュー指摘対応 №6 余計なフォームが開くのを抑制
            _isBacking = true;

            GameForm nextForm = new GameForm(_playerName,_playerCount,_cpuCount, _gameSession);
            nextForm.Show();

            this.Close();
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
            UpdateGameOperation();  // #55：ゲームログ表示内容実装 // 20260616 工藤　

            /*
            // 20260616 工藤 #41対応漏れ　ここから
            // すべてのアクティブインジケータ（Panel）の色を、一旦全部「透明」にリセットする
            foreach (var panel in _activeIndicatorPanels.Values)
            {
                panel.BackColor = Color.Transparent; // 一度全員を透明に
            }   

            if (_gameManager.ActivePlayer != null && _activeIndicatorPanels.ContainsKey(_gameManager.ActivePlayer))
            {
                // 現在の手番の人だけを「黄色」に染める！
                _activeIndicatorPanels[_gameManager.ActivePlayer].BackColor = Color.Yellow;
            }
            // 20260616 工藤 #41対応漏れ　ここまで
            */

            // 20260619 工藤*UI改善* プレーヤーのラベル表示の演出
            //一度全員のラベルの色をデフォルトの「白（Color.White）」にリセット

            foreach (var label in _playerLabels.Values)
            {
                label.ForeColor = Color.White;
            }

            // 20260619 工藤*UI改善* プレーヤーのラベル表示の演出
            // 現在の手番プレイヤー（ActivePlayer）の文字色だけを「ゴールド（Color.Gold）」にする！
            if (_gameManager.ActivePlayer != null && _playerLabels.ContainsKey(_gameManager.ActivePlayer))
            {
                _playerLabels[_gameManager.ActivePlayer].ForeColor = Color.Gold; // または Color.Yellow
            }


            //・ゲームログを更新する 
            UpdateGameLog();
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
                // 20260617 工藤 指摘対応　マウス操作の後に移動しました
                // flpPlayerHand.Controls.Add(pictureBox);
                // =================================================================
                // #61_カードの選択状態が分かるような仕掛け実装(#49) // 20260612 工藤
                // =================================================================
                pictureBox.MouseEnter += Card_MouseEnter; // マウスが乗ったら浮く
                pictureBox.MouseLeave += Card_MouseLeave; // マウスが離れたら戻る
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

        // =================================================================
        // #61_カードの選択状態が分かるような仕掛け実装(#49) // 20260612 工藤
        // Card_MouseEnter(), Card_MouseLeave() を新規作成
        // =================================================================
        private void Card_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                // 20260622 工藤 レビュー指摘対応 No.9 演出の変更
                //削除// pictureBox.Top -= 15; // 15ピクセル上に浮かせる
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {

                // 20260622 工藤 レビュー指摘対応 No.9 演出の変更
                //削除// pictureBox.Top += 15; // 元の位置に戻す
                pictureBox.BorderStyle = BorderStyle.None;
            }
        }


        //捨て札エリアを描き直す
        private void UpdateFlpDiscardPile()
        {
            flpThrown.Controls.Clear();
            foreach (Card card in _gameManager.DiscardPile)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = _cardImages[card.DisplayName];
                pictureBox.Width = CardWidth;
                pictureBox.Height = CardHeight;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                flpThrown.Controls.Add(pictureBox);
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



        // 20260622 工藤 レビュー指摘対応 No.11 スキップ対応版の操作ガイド更新
            //削除// private void UpdateGameOperation()  // 20260619 工藤*UI改善* メソッドの頭に async を追加
        // =================================================================
        // #51：UpdateGameLog() ゲームログ・操作ログメソッド実装
        // #55：ゲームログ表示内容実装
        // #56：操作ガイド表示内容実装
        // 20260616 工藤
        // =================================================================

        private async void UpdateGameOperation()
        {
            // 進行中の文字送りがあれば、安全にキャンセルして破棄する
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            Operation.Text = string.Empty;

            GamePhase phase = _gameManager.CurrentPhase;
            string message = "";

            switch (phase)
            {
                case GamePhase.PlayerSelecting:
                    // 🌟 途中で綺麗に改行されるように、明示的に Environment.NewLine を仕込みます
                    message = $"▶あなたのターンです。{Environment.NewLine}{_gameManager.TargetPlayer.Name}からカードを1枚選んで「けってい」ボタンを押してください。";
                    break;
                case GamePhase.PlayerConfirming:
                    message = $"▶{_gameManager.ActivePlayer.Name}のターンです。{Environment.NewLine}「すすむ」ボタンを押してください。";
                    break;
                case GamePhase.CpuTurn:
                    message = $"▶{_gameManager.ActivePlayer.Name}のターンです。{Environment.NewLine}「すすむ」ボタンを押してください。";
                    break;
                case GamePhase.GameOver:
                    message = $"あなたの負けです。{Environment.NewLine}終了する場合は「もどる」ボタンを、もう一度遊ぶ場合は「リスタート」ボタンを押してください。";
                    break;
                case GamePhase.GameWin:
                    message = $"あなたの勝ちです！{Environment.NewLine}終了する場合は「もどる」ボタンを、もう一度遊ぶ場合は「リスタート」ボタンを押してください。";
                    break;
            }

            if (!string.IsNullOrEmpty(message))
            {
                _currentFullMessage = message; // 全文を記憶
                _cts = new System.Threading.CancellationTokenSource();

                try
                {
                    // キャンセルトークンを渡して文字送りを実行
                    await TypewriterEffectAsync(message, _cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // 🌟 スキップされたら一気に全文を表示！
                    Operation.Text = _currentFullMessage;
                }
                finally
                {
                    _cts?.Dispose();
                    _cts = null;
                }
            }
        }

        //20260619 工藤*UI改善* 
        /// <summary>
        /// 操作ガイドを一文字ずつ「ポポポ」と表示する演出
        /// </summary>

        // 20260622 工藤 レビュー指摘対応 No.11 キャンセル対応の演出
        private async Task TypewriterEffectAsync(string targetMessage, System.Threading.CancellationToken token)
            //削除//private async Task TypewriterEffectAsync(string targetMessage)
        {
            // 1. まずはテキストボックスを空っぽにする
            Operation.Text = string.Empty;

            // 2. 文字列を1文字ずつバラしてループで回す
            foreach (char ch in targetMessage)
            {
                // 20260622 工藤 レビュー指摘対応 No.11 キャンセル対応の演出
                // 毎文字の処理前に、キャンセル命令が飛んできていないかチェックする
                token.ThrowIfCancellationRequested();　

                // ラベルの末尾に1文字結合する
                Operation.Text += ch.ToString();

                // 3. 「ポッ」という効果音を鳴らす！
                // (※効果音ファイルをResourcesに登録している場合)
                // System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.pop_sound);
                // player.Play();

                // 4. 次の文字を表示するまでの「待ち時間」（ミリ秒）
                // 20260622 工藤 レビュー指摘対応 No.11 キャンセル対応の演出
                // 引数に token を渡すことで、待機中（Delay中）のキャンセルにも即座に対応できる
                await Task.Delay(50, token);
                // await Task.Delay(50);
            }
        }

        // =================================================================
        // #51：UpdateGameLog() ゲームログ・操作ログメソッド実装
        // #55：ゲームログ表示内容実装
        // 20260616 工藤 メソッド新規作成
        // =================================================================
        private void UpdateGameLog()
        {
            string message = "";

            switch (_gameManager.CurrentPhase)
            {
                case GamePhase.BeforeStart:
                    message = "ゲームが開始されました。";
                    break;
                case GamePhase.PlayerSelecting:
                    message = $"{_gameManager.ActivePlayer.Name}のターン";
                    break;
                case GamePhase.PlayerConfirming:
                    message = "カードを選択しました。"; 
                    break;
                case GamePhase.CpuTurn:
                    message = $"{_gameManager.ActivePlayer.Name}のターン";
                    break;
                case GamePhase.GameOver:
                    message = "Your Lose"; 
                    break;
                case GamePhase.GameWin:
                    message = "Game Win";
                    break;
            }
            if (!string.IsNullOrEmpty(message))
            {

                // 20260619 工藤*UI改善*　新しいメッセージを「前側」に結合
                Logs.Text = message + Environment.NewLine + Logs.Text;

                // 20260622 工藤 レビュー指摘対応 No.10 メソッド差し替え
                //削除// // 20260619 工藤*UI改善*　古い行を削る
                //削除// LimitLogLines();
                AddAndLimitLog(message);
            }
        }

        //ゲームログ更新(カードドロー時)
        private void AddCardDrawLog(Card drawCard)
        {
            string message = "";
            if (_gameManager.ActivePlayer.IsCpu)
            {
                message = $"{_gameManager.ActivePlayer.Name}がカードを1枚引きました。";

                // 20260622 工藤 レビュー指摘対応 No.10 メソッド差し替え
                //削除// // Logs.AppendText(message+Environment.NewLine); // 20260619 工藤*UI改善 
                //削除//Logs.Text = message + Environment.NewLine + Logs.Text; // 20260619 工藤*UI改善
                //削除//LimitLogLines(); // 20260619 工藤*UI改善
                AddAndLimitLog(message); // 20260622 工藤 レビュー指摘対応 No.10 新メソッド
                return;
            }
            else
            {
                message = $"{_gameManager.ActivePlayer.Name}が{drawCard.DisplayName}のカードを1枚引きました。";

                // 20260622 工藤 レビュー指摘対応 No.10 メソッド差し替え
                //削除// // Logs.AppendText(message + Environment.NewLine); // 20260619 工藤*UI改善
                //削除// Logs.Text = message + Environment.NewLine + Logs.Text; // 20260619 工藤*UI改善
                //削除// LimitLogLines(); // 20260619 工藤*UI改善
                AddAndLimitLog(message);  // 20260622 工藤 レビュー指摘対応 No.10 新メソッド
            }
        }

        //ゲームログ更新(ターン進行後)
        private void UpdateAdvanceTurnLogs(List<string> logs)
        {

            // 20260622 工藤 レビュー指摘対応 No.10 
                    /* 不要な処理削除　ここから
                            // 20260619 工藤*UI改善*　 1. 今回届いた複数のログを、あらかじめ「改行」で1つの塊に合体させます
                            string combinedLog = string.Join(Environment.NewLine, logs);

                            if (!string.IsNullOrEmpty(combinedLog))
                            {
                                // 20260619 工藤*UI改善*　 2. 塊ごと、現在のログの【一番前（上）】にガツンとドッキング！
                                Logs.Text = combinedLog + Environment.NewLine + Logs.Text;
                            }

                            // 20260619 工藤*UI改善*　 3. ループの外側で、最後に「1回だけ」スマートに行数を制限する
                            LimitLogLines();
                   不要な処理削除　ここまで */
            // 順番が逆にならないよう、古い順（届いた順）にリストに入れる
            foreach (string log in logs)
            {
                AddAndLimitLog(log);
            }

        }

        // 20260622 工藤 レビュー指摘対応 No.10 ログ更新と4行制限のロジック刷新 AddAndLimitLog() 新規追加
        /* 不要なメソッド削除　ここから
            // 20260619 工藤*UI改善* メソッド追加　
            /// <summary>
            /// ゲームログを最新の4行のみに制限する
            /// </summary>
            private void LimitLogLines()
            {
                //  1. 現在のテキストを「改行コード」で区切って、1行ずつの配列（名簿）に分解
                string[] lines = Logs.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                //  2. もし全体の行数が「4行」を超えていたら、古い行を切り捨て
                if (lines.Length > 4)
                {
                    // 最新の4行だけを残す
                    Logs.Text = string.Join(Environment.NewLine, lines.Take(4));
                }
            }
        不要なメソッド削除　ここまで */
        private void AddAndLimitLog(string newMessage)
        {
            if (string.IsNullOrEmpty(newMessage)) return;

            // 1. リリストの【先頭（一番上）】に新しいログを割り込ませる
            _gameLogs.Insert(0, newMessage);

            // 2. もし4行を超えていたら、一番後ろ（一番古いログ）を削除する
            while (_gameLogs.Count > 4)
            {
                _gameLogs.RemoveAt(_gameLogs.Count - 1);
            }

            // 3. リストの中身を改行で合体させて、画面のテキストボックスにドン！と代入する
            Logs.Text = string.Join(Environment.NewLine, _gameLogs);
        }

        // 20260622 工藤 レビュー指摘対応 No.11 操作ガイド演出制御用の変数
        private System.Threading.CancellationTokenSource _cts;
        private string _currentFullMessage = "";


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
            btnMainAction.Text = "リスタート";
        }

        //======================================
        //共通演出メソッド
        //======================================
        //工藤さん担当

        //(仮)削除変更OK!
        private void ShowMessegeBoxWin()
        {
            MessageBox.Show("あなたの勝ちです！","勝利",MessageBoxButtons.OK,MessageBoxIcon.None);
            ShowResultActionButtons();
        }
        private void ShowMessegeBoxLose()
        {
            MessageBox.Show("あなたの負けです。", "敗北", MessageBoxButtons.OK, MessageBoxIcon.None);
            ShowResultActionButtons();
        }


        //勝ち抜けCPUの画面表示(アクティブじゃないのが分かるように)

        //カードを引く際の演出

        //カードを捨てる際の演出

        // =================================================================
        // #47_GameForm勝利時演出メソッド // 20260611 工藤
        // =================================================================
        internal async Task ShowPlayerWinResult()
        {

            /*効果音を使う場合 ※win_sound の準備が必要
            SoundPlayer player = new SoundPlayer(Properties.Resources.win_sound);
            player.Play();
            */

            // 20260617 工藤 指摘対応　コメントの修正　「（操作をロック）」を削除
            // 1. 勝利画像をセットして、画面にバーンと表示する
            pictureBox_Result.Image = Properties.Resources._08_youWin;
            pictureBox_Result.Visible = true;
            pictureBox_Result.BringToFront(); // 一番手前に持ってくる

            // 4. 2秒間（2000ミリ秒）、画面をそのまま静止（余韻の演出）
            await Task.Delay(2000);

            // 5. 画像を消して、通常の画面に戻す
            pictureBox_Result.Visible = false;

            // 2. 画像が出た状態で、お祝いメッセージボックスを出す
            ShowMessegeBoxWin();　 // 20260616 工藤 移動 

        }

        // =================================================================
        // #48_GameForm敗北時画面演出 // 20260611 工藤 
        // =================================================================
        internal async Task ShowPlayerLoseResult()
        {

            /*効果音を使う場合 ※lose_sound の準備が必要
            SoundPlayer player = new SoundPlayer(Properties.Resources.lose_sound);
            player.Play();
            */

            // 1. 敗北画像をセットして、画面にバーンと表示する
            pictureBox_Result.Image = Properties.Resources._08_youLose;
            pictureBox_Result.Visible = true;
            pictureBox_Result.BringToFront();


            // 2. 画像が出た状態で、敗北メッセージボックスを出す
            ShowMessegeBoxLose();  // 20260616 工藤 移動 

            // 3. 2秒間（2000ミリ秒）、画面をそのまま静止
            await Task.Delay(2000);

            // 4. 画像を消して、通常の画面に戻す
            pictureBox_Result.Visible = false;
        }

        //=========================================================
        // デバック画面関係
        //=========================================================

        //キー入力イベント
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (IsDebugCommandCompleted(keyData))
            {
                ShowDebugForm();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        //デバックコマンド判定
        private bool IsDebugCommandCompleted(Keys keyCode)
        {
            if (keyCode == _debugCommand[_debugCommandIndex])
            {
                _debugCommandIndex++;

                if (_debugCommandIndex >= _debugCommand.Length)
                {
                    _debugCommandIndex = 0;
                    return true;
                }

                return false;
            }

            _debugCommandIndex = 0;
            return false;
        }

        //デバックフォーム表示
        private void ShowDebugForm()
        {
            //デバック画面が複数開かないよう判定
            if (_debugForm == null || _debugForm.IsDisposed)
            {
                _debugForm = new DebugForm(
                    ResetGameToBeforeStart,
                    ShowDebugWinResult,
                    ShowDebugLoseResult
                );

                _debugForm.Show(this);
            }
            else
            {
                _debugForm.Activate();
            }
        }

        //ゲームリセットメソッド
        private void ResetGameToBeforeStart()
        {
            _gameManager.SetBeforeStart();
            _gameManager.InitializeGame();

            _selectedPictureBox = null;
            pictureBox_Result.Visible = false;

            ClearCardDisplayAreas();
            InitializeGameDisplay();
            UpdateDisplay();
        }

        //勝利時演出確認メソッド
        private async void ShowDebugWinResult()
        {
            ResetGameToBeforeStart();
            //通常ゲームスタート
            //ゲームログ更新
            UpdateGameLog();
            // ゲーム開始処理
            _gameManager.StartGame();
            //画面再取得
            UpdateDisplay();

            //即勝利判定へ
            _gameManager.PlayerWin();
            await ShowPlayerWinResult();
            ShowResultActionButtons();

        }

        //敗北時演出確認メソッド
        private async void ShowDebugLoseResult()
        {
            ResetGameToBeforeStart();
            //通常ゲームスタート
            //ゲームログ更新
            UpdateGameLog();
            // ゲーム開始処理
            _gameManager.StartGame();
            //画面再取得
            UpdateDisplay();

            //即敗北判定へ
            _gameManager.PlayerLose();
            await ShowPlayerLoseResult();
            ShowResultActionButtons();
        }

        // 20260622 工藤 レビュー指摘対応 No.11 操作ガイドクリック時のスキップ処理
        private void Operation_Click(object sender, EventArgs e)
        {
            // 文字送り実行中（_ctsが存在する）であれば、キャンセルを発動する
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }
    }



}
