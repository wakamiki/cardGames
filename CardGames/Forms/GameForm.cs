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

        private BabanukiGameManager _gameManager;
        public GameForm()
        {
            InitializeComponent();
            _gameManager = new BabanukiGameManager();

        }
        private void GameForm_Load(object sender, EventArgs e)
        {
            //各種初期化
            //操作ガイド初期文を入力
            //ゲームログ初期文を入力
            //ターン数=0
            //プレイヤー名画面反映
            //手札数表示=0
            //決定ボタン=かいし
            //フローレイアウトパネル空表示
        }
        private void btnMainAction_Click(object sender, EventArgs e)
        {
            switch (_gameManager.CurrentPhase)
            {
                case GamePhase.BeforeStart:
                    // ゲーム開始処理
                    break;

                case GamePhase.PlayerSelecting:
                    // カード未選択なので基本は何もしない
                    break;

                case GamePhase.PlayerConfirming:
                    // 選択済みカードを確定して引く
                    break;

                case GamePhase.CpuTurn:
                    // CPUターンを1回進める
                    break;

                case GamePhase.GameOver:
                    // タイトルへ戻る、または再スタート
                    break;
            }

            UpdateDisplay();
        }

        //全体更新メソッド
        private void UpdateDisplay()
        {
            UpdatePlayerHand();
            //・プレイヤー手札を描き直す
            UpdateCpuHands();
            //・CPU手札を描き直す
            UpdateTurnGuide();
            //・残り枚数を更新する
            UpdateGameLog();
            //・そうさガイドを更新する
            //・ゲームログを更新する
            UpdateButtons();
            //・ボタンの有効 / 無効を更新する
            ShowGameResultIfNeeded();
//・勝敗状態ならモーダルやMessageBoxを出す
        }
        //全体更新メソッド関連小メソッド
        private void UpdatePlayerHand()
        {
            //・プレイヤー手札を描き直す
        }
        private void UpdateCpuHands()
        {
            //・CPU手札を描き直す
        }
        private void UpdateTurnGuide()
        {

        }
        private void UpdateGameLog()
        {
            //・そうさガイドを更新する
            //・ゲームログを更新する
        }
        private void UpdateButtons()
        {
            //・ボタンの有効 / 無効を更新する
        }
        private void ShowGameResultIfNeeded()
        {
            //・勝敗状態ならモーダルやMessageBoxを出す
        }


    }
}
