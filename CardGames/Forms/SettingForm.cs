using CardGames.Models;
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
    public partial class SettingForm : Form
    {
        internal SettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            // =================================================================
            // 1. SelectBaba (ババ抜きのCheckBox) の制御
            // =================================================================
            // ON状態（チェックが入った状態）にする
            SelectBaba.Checked = true;

            // ユーザーがクリックして変更できないように無効化（グレーアウト）する
            // 今後ゲームが増えた場合は可変とする
            SelectBaba.Enabled = false;

            // =================================================================
            // 2. InputName (名前入力用のTextBox) の制御
            // =================================================================
            // 設定（保存データ）の中に「NameOfPlayer」というキーが存在し、かつ空っぽでないか確認

            if (Properties.Settings.Default.NameOfPlayer != null &&
                Properties.Settings.Default.NameOfPlayer != "")
            {
                // あれば、保存されているプレイヤー名を表示
                InputName.Text = Properties.Settings.Default.NameOfPlayer;

                // ★プレイヤー名の保存場所
                //　「ソリューションエクスプローラー/Properties」
                // 　　└「Settings.settings」

            }
            else
            {
                // 無い場合は、デフォルトの「Player1」を表示
                InputName.Text = "Player1";
            }

            // テキストボックスにフォーカス（カーソル）を当てる
            this.ActiveControl = InputName;

            // =================================================================
            // 3. labelCPU (CPU人数表示用ラベル) の制御
            // =================================================================
            // 初期値を「3人」に設定する
            labelCPU.Text = "3人";

        }

        /// <summary>
        /// 「ゲーム開始」ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGameStart_Click(object sender, EventArgs e)
        {
            // 次画面にプレイヤー名と人数を渡す
            string _NameOfPlayer = InputName.Text; // 入力されたプレイヤー名
            int _playerCount = 1;                // プレイヤー数 (固定: 1)
            int _cpuCount = 3;                   // CPU数 (固定: 3)
            GameSession gameSession = new GameSession();

            // 次画面を作成、引数(プレイヤー名と人数,プレイヤー勝敗数)を渡す
            GameForm gmForm = new GameForm(_NameOfPlayer, _playerCount, _cpuCount,gameSession);

            // ゲーム画面を表示
            gmForm.Show();

            // 現在の画面を閉じる
            this.Close();

        }

        /// <summary>
        /// 「戻る」ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            // スタート画面を再表示
            Application.OpenForms["StartForm"]?.Show();

            // 現在の画面を閉じる
            this.Close(); 
        }


        /// <summary>
        /// 「登録」ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegist_Click(object sender, EventArgs e)
        {
            // TextBox（InputName）より名前を取得
            string inputName = InputName.Text.Trim();

            // 文字列が入力されていなければエラー
            if (string.IsNullOrEmpty(inputName))
            {
                MessageBox.Show("プレイヤー名を入力してください。",
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NameOfPlayer に入力された名前をセット
            Properties.Settings.Default.NameOfPlayer = inputName;

            // 名前をプロパティで保存
            Properties.Settings.Default.Save();

            // ユーザーに完了を通知
            MessageBox.Show($"プレイヤー名を「{inputName}」で登録しました！",
                "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
