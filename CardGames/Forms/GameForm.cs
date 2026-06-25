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

namespace CardGames
{
    public partial class GameForm : Form
    {

        //======================================
        //フィールド
        //======================================
        //初期
        private string _playerName;
        private int _playerCount;
        private int _cpuCount;

        //カードサイズ定数
        private const int CardWidth = 50;
        private const int CardHeight = 70;

        //専用ゲームマネージャー保存
        private BabanukiGameManager _gameManager;
        //勝敗記録用クラス保存
        private GameSession _gameSession;

        //カード画像保存
        private Dictionary<string, Image> _cardImages = new Dictionary<string, Image>();
        //裏面カード画像保存
        private Image _cardBackImage;

        //選択カード保存
        private PictureBox _selectedPictureBox;
        //ホバー中カード保存
        private PictureBox _hoveredPictureBox;

        // ゲームログを管理するリスト
        private List<string> _gameLogs = new List<string>();

        //操作ガイド演出制御用の変数
        private System.Threading.CancellationTokenSource _cts;
        private string _currentFullMessage = "";
        // 文字重なり防止用のバージョン管理
        // 操作ガイドの文字送りが古い処理かどうかを判定する世代番号
        private int _operationGuideVersion = 0;

        //プレイヤーとフローレイアウトパネルの紐づけ辞書
        private Dictionary<Player, FlowLayoutPanel> _playerHandPanels = new Dictionary<Player, FlowLayoutPanel>();
        // プレイヤーと対応する名前ラベルを結びつける辞書
        private Dictionary<Player, Label> _playerLabels = new Dictionary<Player, Label>();

        //  falseで×ボタンで「戻る」と同じ動きにする
        private bool _isBacking = false;

        //デバック画面起動コード
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
        //デバック画面起動コード判定用
        private int _debugCommandIndex = 0;

        //デバック画面ボタン連打防止
        private bool _isDebugActionRunning = false;


        //======================================
        //イベントメソッド
        //======================================
        internal GameForm(string playerName,int playerCount,int cpuCount,GameSession gameSession)
        {
            //ゲームフォーム作成
            InitializeComponent();

            //プレイヤーネーム設定
            _playerName = playerName;

            //参加人数内訳設定
            _playerCount = playerCount;
            _cpuCount = cpuCount;

            //勝敗記録用クラス設定
            _gameSession = gameSession;

            //ゲームマネージャー作成、設定
            _gameManager = new BabanukiGameManager();

            //ゲームマネージャーにプレイヤーネーム、参加人数内訳共有
            _gameManager.GameSettings(_playerName,_playerCount,_cpuCount);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            // フォーム全体の背景に背景画像をセット
            this.BackgroundImage = Image.FromFile(Path.Combine("images", "素材ズ", "04_wallpaper.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch; // 背景画像サイズの自動調整[Stretch]

            //各種初期化
            InitializeGameDisplay();

            //操作ガイド初期文を入力
            SetInitialOperationGuide();

            //ゲームログ初期文を入力
            SetLogs();

            //進行ボタンテキスト=かいし
            btnMainAction.Text = "かいし";

            //デッキ、プレイヤー、プレイヤーリスト準備
            _gameManager.InitializeGame();

            //フローレイアウトパネルとプレイヤーの紐づけ辞書・ラベルとプレイヤーの紐づけ辞書作成
            InitializePlayerViewMaps();

            //表示用カード画像準備
            LoadCardImages();

            // 勝敗表示　配列[0]が勝ち数、配列[1]が負け数
            lblResults.Text = $"{_playerName}さんの戦績：" +
                $"{_gameSession.PlayerResult[0]}勝 {_gameSession.PlayerResult[1]}敗";

            // 勝敗画像の設定
            this.Controls.Add(pictureBox_Result);
            pictureBox_Result.Parent = this;

            //表示場所設定
            pictureBox_Result.Location = new Point(0, 0);

            //サイズ設定
            pictureBox_Result.Size = this.ClientSize;
            pictureBox_Result.SizeMode = PictureBoxSizeMode.StretchImage;

            //通常時は隠す
            pictureBox_Result.Visible = false;

            // 重ね順を「手札が手前、黄色が後ろ」に設定
            flpPlayerHand.BringToFront();
            flpCpu1Hand.BringToFront();
            flpCpu2Hand.BringToFront();
            flpCpu3Hand.BringToFront();

            //勝敗画面を最前面に設定
            pictureBox_Result.BringToFront();

            //チュートリアル画像の設定
            this.Controls.Add(tutorialView);
            tutorialView.Parent = this;
            tutorialView.Location = new Point(0, 0);
            tutorialView.BringToFront();
        }

        //進行ボタンクリックイベント
        private void btnMainAction_Click(object sender, EventArgs e)
        {
            //ゲーム進行状態によって変化
            switch (_gameManager.CurrentPhase)
            {
                // 【ゲームスタート】
                case GamePhase.BeforeStart:

                    //ゲームログ更新(ゲームスタート)
                    UpdateGameLog();

                    // ゲーム開始処理
                    _gameManager.StartGame();

                    //画面更新
                    UpdateDisplay();

                    //カードを引く相手だけカード選択可能にする
                    EnableTargetPlayerCardSelection();
                    break;

                // 【プレイヤーターン:カード選択前】
                case GamePhase.PlayerSelecting:

                    // カード未選択なのでボタン選択できない
                    break;

                // 【プレイヤーターン:カード選択済み】
                case GamePhase.PlayerConfirming:

                    //引きたいカードの配列番号取得
                    int cardIndex = TakeSelectedCardIndex();

                    //カードドロー
                    Card drawCard = _gameManager.PlayerTurnCardDraw(cardIndex);

                    //ゲームログ更新(ドローカード表示)
                    AddCardDrawLog(drawCard);

                    //ターン進行&ゲームログ取得
                    List<string> logs = _gameManager.AdvanceTurn(drawCard);

                    //ゲームログ更新(捨てたペア表示、手札に加えたカード表示)
                    UpdateAdvanceTurnLogs(logs);

                    //ゲーム進行状態が勝敗状態に変化していなければCPUターンへ変更
                    if (_gameManager.CurrentPhase != GamePhase.GameWin&&
                        _gameManager.CurrentPhase != GamePhase.GameOver)
                    {
                        _gameManager.SetCpuTurn();
                    }

                    //画面更新
                    UpdateDisplay();
                    break;

                // 【CPUターンスタート】
                case GamePhase.CpuTurn:

                    // CPUがカードを引く
                    Card cpuDrawCard = _gameManager.CpuTurnCardDraw();

                    //ゲームログ更新(ドローカード表示)
                    AddCardDrawLog(cpuDrawCard);

                    //ターン進行&ゲームログ取得
                    List<string> cpuLogs = _gameManager.AdvanceTurn(cpuDrawCard);

                    //ゲームログ更新(捨てたペア表示、手札に加えたカード表示)
                    UpdateAdvanceTurnLogs(cpuLogs);

                    //次の手番がプレイヤー時の遷移処理
                    //【重要】ここから下breakまで順序変更等で不具合が起こりやすいゾーン変更するときは慎重に扱うこと
                    if (_gameManager.CurrentPhase != GamePhase.GameWin &&
                        _gameManager.CurrentPhase != GamePhase.GameOver)
                    {
                        if (_gameManager.IsPlayerTurn())
                        {
                            _gameManager.SetPlayerSelecting();
                        }
                    }

                    // 画面全体を更新
                    UpdateDisplay();

                    if (_gameManager.CurrentPhase == GamePhase.PlayerSelecting)
                    {
                        //相手カードを選択可能に変更
                        EnableTargetPlayerCardSelection();
                    }
                    break;

                // 【ゲームオーバー】
                case GamePhase.GameOver:

                    // 敗北数記録
                    _gameSession.AddPlayerLose();

                    // 再スタート
                    ReStart();
                    break;
                
                //　【ゲーム勝利】
                case GamePhase.GameWin:

                    // 勝利数記録
                    _gameSession.AddPlayerWin();

                    // 再スタート
                    ReStart();
                    break;
            }
        }

        // ×ボタンで「戻る」と同じ動きにする
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_isBackingがtrueなら何もしない
            if (_isBacking) return;

            // 画面を閉じる処理が行われたならば
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // ×ボタン本来の「アプリを消し去る動き」を一旦キャンセル
                e.Cancel = true;
                // 「戻るボタン」押下時と同じ挙動にする
                btnBack_Click(sender, e);
            }
        }

        //戻るボタン処理
        private void btnBack_Click(object sender, EventArgs e)
        {
            _isBacking = true;

            //新しい設定画面を作成・表示
            SettingForm frm3 = new SettingForm();
            frm3.Show();

            //現在のFormを閉じる
            this.Close();
        }


        //======================================
        //チュートリアル画面表示メソッド
        //======================================

        //チュートリアル画面表示
        private void btnTutorial_Click(object sender, EventArgs e)
        {
            tutorialView.Image = Properties.Resources._11_tutorial;
            tutorialView.Visible = true;
            tutorialView.BringToFront();
        }

        //チュートリアル画面を閉じる
        private void tutorialView_Click(object sender, EventArgs e)
        {
            tutorialView.Visible = false;
        }

        //======================================
        //ロードイベント内メソッド
        //======================================

        //カード画像全読み込み
        internal void LoadCardImages()
        {
            //カード表示エリア初期化
            ClearCardDisplayAreas();
            flpThrown.Controls.Clear();

            //カード画像を辞書に追加(key==DisplayName)
            foreach (Card card in _gameManager.Deck)
            {
                string key = card.DisplayName;

                //Path.Combineは記号間違いでパスが壊れるのを防ぐため採用
                string imagePaths = Path.Combine("images", "素材ズ", "07_トランプ表", key + ".png");

                //キーと画像を対応させて辞書に追加
                _cardImages.Add(key, Image.FromFile(imagePaths));
            }
            //裏面画像追加
            string imagePath = Path.Combine("images", "素材ズ", "05_Back.png");
            _cardBackImage = Image.FromFile(imagePath);
        }

        //初期化メソッド
        internal void InitializeGameDisplay()
        {
            //カード表示エリア初期化
            ClearCardDisplayAreas();
            flpThrown.Controls.Clear();

            //操作ガイド・ゲームログ初期化
            InitializeMessageAreas();

            //手札残数表示ラベル初期化
            InitializeHandCountLabels();
        }

        //フローレイアウトパネル(プレイヤー手札)を初期化
        internal void ClearCardDisplayAreas()
        {
            flpCpu1Hand.Controls.Clear();
            flpCpu2Hand.Controls.Clear();
            flpCpu3Hand.Controls.Clear();
            flpPlayerHand.Controls.Clear();
        }

        //操作ガイド・ゲームログテキストを初期化
        internal void InitializeMessageAreas()
        {
            Operation.Text = string.Empty;
            Logs.Text = string.Empty;
        }

        //手札枚数表示を0まい表示に初期設定
        internal void InitializeHandCountLabels()
        {
            DateOfCUP1.Text = "CPU１のてふだ のこり０まい";
            DateOfCUP2.Text = "CPU２のてふだ のこり０まい";
            DateOfCUP3.Text = "CPU３のてふだ のこり０まい";
            DateOfPlayer.Text = _playerName + "のてふだ のこり０まい";
        }

        //操作ガイド初期文設定
        internal void SetInitialOperationGuide()
        {
            Operation.Text = "▶「ゲーム開始」ボタンを押してください。";
        }

        //ゲームログ初期文設定(今は初期化だけだがいずれ初期文設定を行う時用にメソッドを残しておく)
        internal void SetLogs()
        {
            _gameLogs.Clear();
        }

        //フローレイアウトパネルとプレイヤーの紐づけ辞書・ラベルとプレイヤーの紐づけ辞書作成
        private void InitializePlayerViewMaps()
        {
            // パネルとプレイヤーの紐づけを登録
            _playerHandPanels.Add(_gameManager.Players[0], flpPlayerHand);
            _playerHandPanels.Add(_gameManager.Players[1],flpCpu1Hand);
            _playerHandPanels.Add(_gameManager.Players[2],flpCpu2Hand);
            _playerHandPanels.Add(_gameManager.Players[3],flpCpu3Hand);

            // プレイヤーとラベルの紐づけを登録
            _playerLabels.Add(_gameManager.Players[0], DateOfPlayer);
            _playerLabels.Add(_gameManager.Players[1], DateOfCUP1);
            _playerLabels.Add(_gameManager.Players[2], DateOfCUP2);
            _playerLabels.Add(_gameManager.Players[3], DateOfCUP3);
        }

        //=================================================
        //GamePhase.BeforeStart　進行状態【ゲームスタート】
        //=================================================

        //引く相手のカードを選択可能にする
        private void EnableTargetPlayerCardSelection()
        {
            //相手のフローレイアウトパネル内のカード一つ一つに設定を追加
            foreach (Control control in _playerHandPanels[_gameManager.TargetPlayer].Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    //ピクチャボックスのクリックイベントを初期化
                    pictureBox.Click -= SelectableCardPictureBox_Click;

                    //ピクチャボックスにクリックイベントを追加
                    pictureBox.Click += SelectableCardPictureBox_Click;

                    //カーソルがあった時にカーソルを手表示に変更
                    pictureBox.Cursor = Cursors.Hand;

                    //ホバーイベントを初期化
                    pictureBox.MouseEnter -= Card_MouseEnter;

                    //CPUエリアのカード選択時の演出のためホバーイベントを追加
                    pictureBox.MouseEnter += Card_MouseEnter;

                    //ホバー解除時イベントを初期化
                    pictureBox.MouseLeave -= Card_MouseLeave;

                    //CPUエリアのカード選択時の演出のためホバー解除時イベントを追加
                    pictureBox.MouseLeave += Card_MouseLeave;

                    //描画イベントを初期化
                    pictureBox.Paint -= Card_Paint;

                    //描画イベントを追加
                    pictureBox.Paint += Card_Paint;

                }
            }
        }

        //===========================================================================
        //GamePhase.PlayerSelecting　ゲーム進行状態【プレイヤーターン(カード選択前)】
        //===========================================================================
        //カードを選択するクリックイベント
        private void SelectableCardPictureBox_Click(object sender, EventArgs e)
        {
            //クリックされたピクチャボックスを取得
            PictureBox clickedPictureBox = (PictureBox)sender;

            //選択済みピクチャボックス内に何か入っていれば
            if (_selectedPictureBox != null)
            {
                // 前に選択していたカードの見た目を戻す
                _selectedPictureBox.BorderStyle = BorderStyle.None;
            }

            // 今回クリックしたカードを選択状態にする
            clickedPictureBox.BorderStyle = BorderStyle.Fixed3D;

            // 選択中のPictureBoxとして保存
            _selectedPictureBox = clickedPictureBox;

            // ゲーム進行状態をカード選択済みに進行する
            _gameManager.SetPlayerConfirming();

            // 画面全体は作り直さず、ボタンだけ更新
            // (画面を作り直すとフローレイアウトパネル設定が初期化してしまうため)
            UpdateButtons();
        }

        //=============================================================================
        //GamePhase.PlayerConfirming ゲーム進行状態【プレイヤーターン(カード選択済み)】
        //=============================================================================
        //ピクチャボックスからカード配列番号を返しカード選択状態を解除
        private int TakeSelectedCardIndex()
        {
            //配列番号が入っているはずの場所に数値以外が入っていたら例外を投げる
            if (!int.TryParse(_selectedPictureBox.Tag.ToString(), out int cardIndex))
            {
                throw new InvalidOperationException("カードが選択されていません。");
            }
            //_selectedPictureBoxをnullにする(カード選択状態を解除)
            _selectedPictureBox = null;

            //配列番号を返す
            return cardIndex;
        }

        //================================================================
        //GamePhase.GameWin GamePhase.GameOver ゲーム進行状態【勝敗】
        //================================================================

        //勝敗判定からゲーム演出への移行メソッド(秒数指定演出があるためasync)
        internal async void ShowGameResultIfNeeded()
        {
            if (_gameManager.CurrentPhase == GamePhase.GameOver)
            {
                //敗北演出メソッドへ遷移
                await ShowPlayerLoseResult();
            }
            else if (_gameManager.CurrentPhase == GamePhase.GameWin)
            {
                //勝利演出メソッドへ遷移
                await ShowPlayerWinResult();
            }
        }

        //リスタート処理
        private void ReStart()
        {
            //余計なフォームが開くのを抑制
            //(閉じるボタンイベントが発火しないよう設定)
            _isBacking = true;

            //新しいGameFormを作成・表示
            GameForm nextForm = new GameForm(_playerName,_playerCount,_cpuCount, _gameSession);
            nextForm.Show();

            //現在のGameFormを閉じる
            this.Close();
        }



        //======================================
        //全体更新メソッド
        //======================================
        private void UpdateDisplay()
        {

            //参加者の手札を描き直す
            UpdatePlayersHand();

            //捨て札エリアを描き直す
            UpdateFlpDiscardPile();

            //・残り枚数を更新する
            UpdateTurnGuide();

            //・操作ガイドを更新する 
            UpdateGameOperation();

            //プレーヤーのラベル表示の演出
            //全員のラベルの色をデフォルトの「白（Color.White）」にリセット
            foreach (var label in _playerLabels.Values)
            {
                label.ForeColor = Color.White;
            }

            // 現在の手番プレイヤー（ActivePlayer）の文字色だけをゴールド（Color.Gold)に設定
            if (_gameManager.ActivePlayer != null && _playerLabels.ContainsKey(_gameManager.ActivePlayer))
            {
                _playerLabels[_gameManager.ActivePlayer].ForeColor = Color.Gold;
            }

            //・ゲームログを更新する 
            UpdateGameLog();

            //・ボタンを更新する
            UpdateButtons();

            //・勝敗状態判定＆演出移行
            ShowGameResultIfNeeded();
        }

        //全体更新メソッド関連小メソッド

        //参加者の手札を書き直す
        private void  UpdatePlayersHand()
        {
            //フローレイアウトパネルを初期化
            ClearCardDisplayAreas();

            foreach (Player player in _gameManager.Players)
            {
                for (int i = 0; i < player.HandCount; i++)
                {
                    //CPUとプレイヤー共通のカード設定
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Width = CardWidth;
                    pictureBox.Height = CardHeight;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                    //CPUとプレイヤーの判定
                    if (!player.IsCpu)
                    {
                        //プレイヤーは表面カードを表示
                        pictureBox.Image = _cardImages[player.HandDeck[i].DisplayName];
                    }
                    else
                    {
                        //CPUは裏面カードを表示＆カードの配列番号をtagに保存
                        pictureBox.Image = _cardBackImage;
                        pictureBox.Tag = i;
                    }

                    //フローレイアウトパネルとプレイヤーの辞書を見て対象のプレイヤーのパネルにカードを追加
                    _playerHandPanels[player].Controls.Add(pictureBox);

                    //山札を右スクロール
                    if (flpPlayerHand.Controls.Count > 0)
                    {
                        flpPlayerHand.ScrollControlIntoView(flpPlayerHand.Controls[flpPlayerHand.Controls.Count - 1]);
                    }
                }
            }
        }

        //・残り枚数表示を更新する
        private void UpdateTurnGuide()
        {
            for (int i = 0; i < _gameManager.Players.Count; i++)
            {
                int num = _gameManager.Players[i].HandCount;
                switch (i)
                {
                    case 0:
                    DateOfPlayer.Text = _playerName + "のてふだ のこり" + num + "まい";
                    break;
                    case 1:
                    DateOfCUP1.Text = "CPU１のてふだ のこり"+num+"まい";
                    break;
                    case 2:
                    DateOfCUP2.Text = "CPU２のてふだ のこり" + num + "まい";
                    break;
                    case 3:
                    DateOfCUP3.Text = "CPU３のてふだ のこり" + num + "まい";
                    break;
                }
            }
        }

        // 操作ガイド更新
        private async void UpdateGameOperation()
        {

                // 進行中の文字送りがあれば、安全にキャンセルして破棄する
                if (_cts != null)
                {
                    _cts.Cancel();
                    _cts.Dispose();
                    _cts = null;
                }

                //テキスト初期化
                Operation.Text = string.Empty;

                //文字列用意
                string message = "";

            //ゲーム進行状態に応じてテキスト変化   
            switch (_gameManager.CurrentPhase)
                {
                    case GamePhase.PlayerSelecting:
                        // 途中で綺麗に改行されるように、明示的に Environment.NewLine を仕込みます
                        message = $"▶あなたのターンです。{Environment.NewLine}{_gameManager.TargetPlayer.Name}からカードを1枚選んで「けってい」ボタンを押してください。";
                        break;
                    case GamePhase.PlayerConfirming:
                        message = $"▶{_gameManager.ActivePlayer.Name}のターンです。{Environment.NewLine}「すすむ」ボタンを押してください。";
                        break;
                    case GamePhase.CpuTurn:
                        message = $"▶{_gameManager.ActivePlayer.Name}のターンです。{Environment.NewLine}「すすむ」ボタンを押してください。";
                        break;
                    case GamePhase.GameOver:
                        message = $"あなたの負けです。{Environment.NewLine}終了する場合は「もどる」ボタンを、{Environment.NewLine}もう一度遊ぶ場合は「リスタート」ボタンを押してください。";
                        break;
                    case GamePhase.GameWin:
                        message = $"あなたの勝ちです！{Environment.NewLine}終了する場合は「もどる」ボタンを、{Environment.NewLine}もう一度遊ぶ場合は「リスタート」ボタンを押してください。";
                        break;
                }

                //messageに文字列があれば
                if (!string.IsNullOrEmpty(message))
                {
                    //操作ガイド文字列を保存
                    _currentFullMessage = message;

                    // 今回の表示処理の世代番号を進めて、自分のバージョンを記憶
                    //(非同期処理のため進行ボタンを連打すると文字が混ざる問題を解消するための措置)
                    _operationGuideVersion++;
                    int currentVersion = _operationGuideVersion;

                    // 前回の文字送りがまだ動いていればキャンセルを要求
                    if (_cts != null)
                    {
                        _cts.Cancel();
                    }

                    //　今回専用の CancellationTokenSource をローカル変数として生成
                    System.Threading.CancellationTokenSource localCts = new System.Threading.CancellationTokenSource();
                    _cts = localCts;

                    try
                    {
                        // 引数に currentVersion を追加して文字送りを実行
                        await TypewriterEffectAsync(message, localCts.Token, currentVersion);
                    }
                    catch (OperationCanceledException)
                    {
                        // 自分が「最新の文字送り」の時だけ、クリックによるスキップとして全文を表示する
                        if (currentVersion == _operationGuideVersion)
                        {
                            Operation.Text = _currentFullMessage;
                        }
                    }
                    finally
                    {
                        // 古い文字送りの finally が、新しく始まっている次の _cts を誤って消さないようにガード
                        if (currentVersion == _operationGuideVersion)
                        {
                            localCts.Dispose();

                            if (_cts == localCts)
                            {
                                _cts = null;
                            }
                        }
                    }
                }
            }

        /// <summary>
        /// 操作ガイドを一文字ずつ「ポポポ」と表示する演出
        /// </summary>

        // キャンセル対応の演出
        private async Task TypewriterEffectAsync(string targetMessage, System.Threading.CancellationToken token, int version)
        {
            // メソッド開始時に自分がすでに古い世代になっていたら、何もせず即終了
            if (version != _operationGuideVersion)
            {
                return;
            }

            Operation.Text = string.Empty;

            foreach (char ch in targetMessage)
            {
                // キャンセル要求が来ていたら例外を投げる (末尾の全角スペースは削除)
                token.ThrowIfCancellationRequested();

                // ポポポ…の途中で新しい文字送りが始まっていたら、その場で処理を手放す
                if (version != _operationGuideVersion)
                {
                    return;
                }

                Operation.Text += ch.ToString();

                await Task.Delay(50, token);
            }
        }

        //操作ガイドクリック時のスキップ処理
        private void Operation_Click(object sender, EventArgs e)
        {
            // 文字送り実行中（_cts が存在している）であれば、キャンセルを発動する
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }

        //ゲームログ更新
        private void UpdateGameLog()
        {
            string message = "";

            //ゲーム進行状態に応じてテキスト変化
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

            //messageがnullや空白じゃなければ
            if (!string.IsNullOrEmpty(message))
            {

                // 新しいメッセージを「前側」に結合
                Logs.Text = message + Environment.NewLine + Logs.Text;

                //古い行を削る
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

                AddAndLimitLog(message);
                return;
            }
            else
            {
                message = $"{_gameManager.ActivePlayer.Name}が{drawCard.DisplayName}のカードを1枚引きました。";

                AddAndLimitLog(message);
            }
        }

        //ゲームログ更新(ターン進行後)
        private void UpdateAdvanceTurnLogs(List<string> logs)
        {
            // 順番が逆にならないよう、古い順（届いた順）にリストに入れる
            foreach (string log in logs)
            {
                AddAndLimitLog(log);
            }

        }

        //ゲームログ追加順番制御メソッド
        private void AddAndLimitLog(string newMessage)
        {
            //messageがなければリターン
            if (string.IsNullOrEmpty(newMessage)) return;

            // 1. リリストの【先頭（一番上）】に新しいログを割り込ませる
            _gameLogs.Insert(0, newMessage);

            // 2. もし4行を超えていたら、一番後ろ（一番古いログ）を削除する
            while (_gameLogs.Count > 4)
            {
                _gameLogs.RemoveAt(_gameLogs.Count - 1);
            }

            // 3. リストの中身を改行で合体させて、画面のテキストボックスに代入する
            Logs.Text = string.Join(Environment.NewLine, _gameLogs);
        }

        //・ボタンを更新する
        private void UpdateButtons()
        {
            //ゲーム進行状態に応じてテキスト・active変化
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

        //勝敗後ボタン表示更新
        private void ShowResultActionButtons()
        {
            btnMainAction.Enabled = true;
            btnMainAction.Text = "リスタート";
        }

        //======================================
        //演出メソッド
        //======================================

        // =====================================
        // カード選択イベント
        // =====================================

        //ピクチャボックス内にカーソルが侵入した時
        private void Card_MouseEnter(object sender, EventArgs e)
        {
            //ピクチャボックス以外での発火では反応しない
            if (sender is PictureBox pictureBox)
            {
                //ホバー中ピクチャボックスとして保存
                _hoveredPictureBox = pictureBox;

                //選択中のペイントイベント発火
                pictureBox.Invalidate();
            }
        }

        //ピクチャボックス内からカーソルが出た時
        private void Card_MouseLeave(object sender, EventArgs e)
        {
            //ピクチャボックス以外での発火では反応しない
            if (sender is PictureBox pictureBox)
            {
                if (_hoveredPictureBox == pictureBox)
                {
                    //ホバー中ピクチャボックスを解除
                    _hoveredPictureBox = null;
                }
                //選択解除のペイントイベント発火
                pictureBox.Invalidate();
            }
        }

        //ペイントイベント発火時
        private void Card_Paint(object sender, PaintEventArgs e)
        {
            //ピクチャボックス以外での発火では反応しない
            if (!(sender is PictureBox pictureBox))
            {
                return;
            }

            // ホバー中のPictureBoxでなければ何も描かない
            if (pictureBox != _hoveredPictureBox)
            {
                return;
            }

            //画像全体に白色(透明度60)のオーバーレイを作成する
            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.White)))
            {
                e.Graphics.FillRectangle(brush, pictureBox.ClientRectangle);
            }
        }

        //捨て札エリアを描き直す
        private void UpdateFlpDiscardPile()
        {
            //捨て札エリア初期化
            flpThrown.Controls.Clear();

            //捨て札エリア内カード再描画
            foreach (Card card in _gameManager.DiscardPile)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = _cardImages[card.DisplayName];
                pictureBox.Width = CardWidth;
                pictureBox.Height = CardHeight;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                flpThrown.Controls.Add(pictureBox);

            }

            //山札を右スクロール
            if (flpThrown.Controls.Count > 0)
            {
                flpThrown.ScrollControlIntoView(flpThrown.Controls[flpThrown.Controls.Count - 1]);
            }
        }

        // =================================================================
        //　勝利時演出メソッド
        // =================================================================
        internal async Task ShowPlayerWinResult()
        {
            // 1. 勝利画像をセットして、画面に表示する
            pictureBox_Result.Image = Properties.Resources._08_youWin;
            pictureBox_Result.Visible = true;
            pictureBox_Result.BringToFront(); // 一番手前に持ってくる

            // 4. 2秒間（2000ミリ秒）、画面をそのまま静止（余韻の演出）
            await Task.Delay(2000);

            // 5. 画像を消して、通常の画面に戻す
            pictureBox_Result.Visible = false;

            // 2. 画像が出た状態で、メッセージボックスを出す
            ShowMessegeBoxWin();
        }

        private void ShowMessegeBoxWin()
        {
            MessageBox.Show("あなたの勝ちです！", "勝利", MessageBoxButtons.OK, MessageBoxIcon.None);

            //ボタンを勝敗後の表示に更新
            ShowResultActionButtons();
        }

        // =================================================================
        // #48_GameForm敗北時画面演出
        // =================================================================
        internal async Task ShowPlayerLoseResult()
        {
            // 1. 敗北画像をセットして、画面に表示する
            pictureBox_Result.Image = Properties.Resources._08_youLose;
            pictureBox_Result.Visible = true;
            pictureBox_Result.BringToFront();

            // 2. 画像が出た状態で、敗北メッセージボックスを出す
            ShowMessegeBoxLose();

            // 3. 2秒間（2000ミリ秒）、画面をそのまま静止
            await Task.Delay(2000);

            // 4. 画像を消して、通常の画面に戻す
            pictureBox_Result.Visible = false;
        }

        private void ShowMessegeBoxLose()
        {
            MessageBox.Show("あなたの負けです。", "敗北", MessageBoxButtons.OK, MessageBoxIcon.None);

            //ボタンを勝敗後の表示に更新
            ShowResultActionButtons();
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

                    //コマンド入力が全てあっていればtrue
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

            if (!CanOpenDebugForm())
            {
                MessageBox.Show(
                    "デバッグ画面はゲーム開始前のみ開けます。",
                    "デバッグ画面",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            //デバック画面が複数開かないよう制御
            using (DebugForm debugForm = new DebugForm(
                ResetGameToBeforeStart,
                ShowDebugWinResult,
                ShowDebugLoseResult
            ))
            {
                //デバック画面表示中はGameForm操作不能
                debugForm.ShowDialog(this);
            }
        }

        //デバック画面表示タイミング制御
        private bool CanOpenDebugForm()
        {
            //ゲーム開始前だけ true
            //ゲーム進行中・勝敗後は false
            return _gameManager.CurrentPhase == GamePhase.BeforeStart;
        }

        //ゲームリセットメソッド
        private void ResetGameToBeforeStart()
        {
            //デバッグによる勝敗結果をGame画面に反映しないよう掃除 ゲームマネージャーのフェーズを「開始前」に修正
            _gameManager.SetBeforeStart();


            // 操作ガイドの文字送りが動いていたら止める
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            _operationGuideVersion++;
            _currentFullMessage = "";

            // GameManagerを初期状態に戻す
            _gameManager.InitializeGame();

            // GameManagerのPlayerが作り直されるので、辞書も作り直す
            _playerHandPanels.Clear();
            _playerLabels.Clear();
            InitializePlayerViewMaps();

            // 選択・ホバー状態を解除
            _hoveredPictureBox = null;
            _selectedPictureBox = null;
            pictureBox_Result.Visible = false;
            pictureBox_Result.Image = null;

            // ログを初期化
            _gameLogs.Clear();

            ClearCardDisplayAreas();
            flpThrown.Controls.Clear();
            InitializeMessageAreas();
            InitializeHandCountLabels();
            InitializeGameDisplay();
            SetInitialOperationGuide();

            // ボタンを初期状態に戻す
            btnMainAction.Enabled = true;
            btnMainAction.Text = "かいし";

            //デバッグによる勝敗結果をGame画面に反映しないよう掃除 戦績表示を本来の数に描き直す
            lblResults.Text = $"{_playerName}さんの戦績：{_gameSession.PlayerResult[0]}勝 {_gameSession.PlayerResult[1]}敗";

        }

        //デバック画面勝利時演出確認メソッド
        private async void ShowDebugWinResult()
        {
            //デバック画面ボタン連打防止
            if (_isDebugActionRunning)
            {
                return;
            }

            _isDebugActionRunning = true;

            try
            {

                //ゲームリセット
                ResetGameToBeforeStart();

                //通常ゲームスタート
                //ゲームログ更新
                UpdateGameLog();

                // ゲーム開始処理
                _gameManager.StartGame();
                _gameManager.PlayerWin();

                //画面再取得
                UpdatePlayersHand();
                UpdateFlpDiscardPile();
                UpdateTurnGuide();
                UpdateButtons();

                //即勝利判定へ
                await ShowPlayerWinResult();
                ShowResultActionButtons();

                //デバッグによる勝敗結果をGame画面に反映しないよう掃除 ボタンを「かいし」に強制リセット
                ResetGameToBeforeStart();

            }
            finally
            {
                _isDebugActionRunning = false;
            }

        }

        //デバック画面敗北時演出確認メソッド
        private async void ShowDebugLoseResult()
        {

            //デバック画面ボタン連打防止
            if (_isDebugActionRunning)
            {
                return;
            }

            _isDebugActionRunning = true;

            try
            {
                //ゲームリセット
                ResetGameToBeforeStart();

            //通常ゲームスタート
            //ゲームログ更新
            UpdateGameLog();

            // ゲーム開始処理
            _gameManager.StartGame();
            _gameManager.PlayerLose();

            //画面再取得
            UpdatePlayersHand();
            UpdateFlpDiscardPile();
            UpdateTurnGuide();
            UpdateButtons();

            //即敗北判定へ
            await ShowPlayerLoseResult();
            ShowResultActionButtons();

             //デバッグによる勝敗結果をGame画面に反映しないよう掃除 ボタンを「かいし」に強制リセット
             ResetGameToBeforeStart();

            }
            finally
            {
                _isDebugActionRunning = false;
            }
        }
    }
}
