using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public GameForm(string playerName,int playerCount,int cpuCount)
        {
            InitializeComponent();
            _playerName = playerName;
            _playerCount = playerCount;
            _cpuCount = cpuCount;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        // =================================================================
        // #47_GameForm勝利時演出メソッド
        // =================================================================
        internal async Task ShowPlayerWinResult()
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
        // #48_GameForm敗北時画面演出
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

            // 2. 2秒間（2000ミリ秒）、画面をそのまま静止
            await Task.Delay(2000);

            // 3. 画像を消して、通常の画面に戻す
            pictureBox_Result.Visible = false;
        }
    }








}
