namespace CardGames
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NameOfGame = new System.Windows.Forms.Label();
            this.NameOfPlayer = new System.Windows.Forms.Label();
            this.NumOfCPU = new System.Windows.Forms.Label();
            this.SelectBaba = new System.Windows.Forms.CheckBox();
            this.InputName = new System.Windows.Forms.TextBox();
            this.labelCPU = new System.Windows.Forms.Label();
            this.btnGameStart = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRegist = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameOfGame
            // 
            this.NameOfGame.AutoSize = true;
            this.NameOfGame.BackColor = System.Drawing.SystemColors.Info;
            this.NameOfGame.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NameOfGame.Location = new System.Drawing.Point(73, 52);
            this.NameOfGame.Name = "NameOfGame";
            this.NameOfGame.Size = new System.Drawing.Size(109, 28);
            this.NameOfGame.TabIndex = 0;
            this.NameOfGame.Text = "ゲーム名";
            // 
            // NameOfPlayer
            // 
            this.NameOfPlayer.AutoSize = true;
            this.NameOfPlayer.BackColor = System.Drawing.SystemColors.Info;
            this.NameOfPlayer.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NameOfPlayer.Location = new System.Drawing.Point(73, 123);
            this.NameOfPlayer.Name = "NameOfPlayer";
            this.NameOfPlayer.Size = new System.Drawing.Size(148, 28);
            this.NameOfPlayer.TabIndex = 1;
            this.NameOfPlayer.Text = "プレイヤー名";
            // 
            // NumOfCPU
            // 
            this.NumOfCPU.AutoSize = true;
            this.NumOfCPU.BackColor = System.Drawing.SystemColors.Info;
            this.NumOfCPU.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NumOfCPU.Location = new System.Drawing.Point(73, 195);
            this.NumOfCPU.Name = "NumOfCPU";
            this.NumOfCPU.Size = new System.Drawing.Size(122, 28);
            this.NumOfCPU.TabIndex = 2;
            this.NumOfCPU.Text = "CPU人数";
            // 
            // SelectBaba
            // 
            this.SelectBaba.AutoSize = true;
            this.SelectBaba.Checked = true;
            this.SelectBaba.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectBaba.Enabled = false;
            this.SelectBaba.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SelectBaba.Location = new System.Drawing.Point(238, 50);
            this.SelectBaba.Name = "SelectBaba";
            this.SelectBaba.Size = new System.Drawing.Size(131, 32);
            this.SelectBaba.TabIndex = 4;
            this.SelectBaba.Text = "ババ抜き";
            this.SelectBaba.UseVisualStyleBackColor = true;
            // 
            // InputName
            // 
            this.InputName.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InputName.Location = new System.Drawing.Point(243, 112);
            this.InputName.MaxLength = 50;
            this.InputName.Name = "InputName";
            this.InputName.Size = new System.Drawing.Size(176, 34);
            this.InputName.TabIndex = 5;
            // 
            // labelCPU
            // 
            this.labelCPU.AutoSize = true;
            this.labelCPU.BackColor = System.Drawing.SystemColors.Control;
            this.labelCPU.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCPU.Location = new System.Drawing.Point(247, 195);
            this.labelCPU.Name = "labelCPU";
            this.labelCPU.Size = new System.Drawing.Size(59, 28);
            this.labelCPU.TabIndex = 6;
            this.labelCPU.Text = "３人";
            // 
            // btnGameStart
            // 
            this.btnGameStart.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGameStart.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnGameStart.Location = new System.Drawing.Point(139, 307);
            this.btnGameStart.Name = "btnGameStart";
            this.btnGameStart.Size = new System.Drawing.Size(166, 76);
            this.btnGameStart.TabIndex = 7;
            this.btnGameStart.Text = "ゲーム開始";
            this.btnGameStart.UseVisualStyleBackColor = false;
            this.btnGameStart.Click += new System.EventHandler(this.btnGameStart_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBack.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnBack.Location = new System.Drawing.Point(358, 307);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(166, 76);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "戻る";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnGameEnd_Click);
            // 
            // btnRegist
            // 
            this.btnRegist.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRegist.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRegist.Location = new System.Drawing.Point(462, 112);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Size = new System.Drawing.Size(111, 39);
            this.btnRegist.TabIndex = 9;
            this.btnRegist.Text = "登録";
            this.btnRegist.UseVisualStyleBackColor = false;
            this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRegist);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnGameStart);
            this.Controls.Add(this.labelCPU);
            this.Controls.Add(this.InputName);
            this.Controls.Add(this.SelectBaba);
            this.Controls.Add(this.NumOfCPU);
            this.Controls.Add(this.NameOfPlayer);
            this.Controls.Add(this.NameOfGame);
            this.Name = "SettingForm";
            this.Text = "設定画面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameOfGame;
        private System.Windows.Forms.Label NameOfPlayer;
        private System.Windows.Forms.Label NumOfCPU;
        private System.Windows.Forms.CheckBox SelectBaba;
        private System.Windows.Forms.TextBox InputName;
        private System.Windows.Forms.Label labelCPU;
        private System.Windows.Forms.Button btnGameStart;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnRegist;
    }
}