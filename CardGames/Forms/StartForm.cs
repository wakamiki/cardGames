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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 「スタート」ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            // 設定画面を作成
            SettingForm setForm = new SettingForm();
            // 設定画面を表示
            setForm.Show();
            // 現在の画面を非表示・タスクバーから消去
            this.Visible = false;
        }

        /// <summary>
        /// 「終了」ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            // 現在の画面を閉じる
            this.Close();
        }
    }
}
