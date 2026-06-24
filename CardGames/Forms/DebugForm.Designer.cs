namespace CardGames.Forms
{
    partial class DebugForm
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
            this.btnStart1 = new System.Windows.Forms.Button();
            this.winLabel = new System.Windows.Forms.Label();
            this.winGroupBox = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.loseGroupBox = new System.Windows.Forms.GroupBox();
            this.btnStart2 = new System.Windows.Forms.Button();
            this.loseLabel = new System.Windows.Forms.Label();
            this.resetGroupBox = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.resetLabel = new System.Windows.Forms.Label();
            this.Caution = new System.Windows.Forms.Label();
            this.winGroupBox.SuspendLayout();
            this.loseGroupBox.SuspendLayout();
            this.resetGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart1
            // 
            this.btnStart1.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStart1.Location = new System.Drawing.Point(53, 71);
            this.btnStart1.Name = "btnStart1";
            this.btnStart1.Size = new System.Drawing.Size(103, 46);
            this.btnStart1.TabIndex = 0;
            this.btnStart1.Text = "スタート";
            this.btnStart1.UseVisualStyleBackColor = true;
            this.btnStart1.Click += new System.EventHandler(this.btnStart1_Click);
            // 
            // winLabel
            // 
            this.winLabel.AutoSize = true;
            this.winLabel.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.winLabel.Location = new System.Drawing.Point(22, 33);
            this.winLabel.Name = "winLabel";
            this.winLabel.Size = new System.Drawing.Size(171, 23);
            this.winLabel.TabIndex = 1;
            this.winLabel.Text = "勝利時演出確認";
            // 
            // winGroupBox
            // 
            this.winGroupBox.Controls.Add(this.btnStart1);
            this.winGroupBox.Controls.Add(this.winLabel);
            this.winGroupBox.Location = new System.Drawing.Point(43, 256);
            this.winGroupBox.Name = "winGroupBox";
            this.winGroupBox.Size = new System.Drawing.Size(215, 133);
            this.winGroupBox.TabIndex = 2;
            this.winGroupBox.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(525, 343);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 46);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // loseGroupBox
            // 
            this.loseGroupBox.Controls.Add(this.btnStart2);
            this.loseGroupBox.Controls.Add(this.loseLabel);
            this.loseGroupBox.Location = new System.Drawing.Point(290, 256);
            this.loseGroupBox.Name = "loseGroupBox";
            this.loseGroupBox.Size = new System.Drawing.Size(215, 133);
            this.loseGroupBox.TabIndex = 3;
            this.loseGroupBox.TabStop = false;
            // 
            // btnStart2
            // 
            this.btnStart2.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStart2.Location = new System.Drawing.Point(53, 71);
            this.btnStart2.Name = "btnStart2";
            this.btnStart2.Size = new System.Drawing.Size(103, 46);
            this.btnStart2.TabIndex = 0;
            this.btnStart2.Text = "スタート";
            this.btnStart2.UseVisualStyleBackColor = true;
            this.btnStart2.Click += new System.EventHandler(this.btnStart2_Click);
            // 
            // loseLabel
            // 
            this.loseLabel.AutoSize = true;
            this.loseLabel.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.loseLabel.Location = new System.Drawing.Point(22, 33);
            this.loseLabel.Name = "loseLabel";
            this.loseLabel.Size = new System.Drawing.Size(171, 23);
            this.loseLabel.TabIndex = 1;
            this.loseLabel.Text = "敗北時演出確認";
            // 
            // resetGroupBox
            // 
            this.resetGroupBox.Controls.Add(this.btnReset);
            this.resetGroupBox.Controls.Add(this.resetLabel);
            this.resetGroupBox.Location = new System.Drawing.Point(43, 78);
            this.resetGroupBox.Name = "resetGroupBox";
            this.resetGroupBox.Size = new System.Drawing.Size(215, 133);
            this.resetGroupBox.TabIndex = 2;
            this.resetGroupBox.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnReset.Location = new System.Drawing.Point(53, 71);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(103, 46);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "リセット";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // resetLabel
            // 
            this.resetLabel.AutoSize = true;
            this.resetLabel.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.resetLabel.Location = new System.Drawing.Point(37, 31);
            this.resetLabel.Name = "resetLabel";
            this.resetLabel.Size = new System.Drawing.Size(128, 23);
            this.resetLabel.TabIndex = 1;
            this.resetLabel.Text = "ゲームリセット";
            // 
            // Caution
            // 
            this.Caution.AutoSize = true;
            this.Caution.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Caution.Location = new System.Drawing.Point(39, 24);
            this.Caution.Name = "Caution";
            this.Caution.Size = new System.Drawing.Size(492, 23);
            this.Caution.TabIndex = 2;
            this.Caution.Text = "デバッグ画面表示中はGameFomrは操作できません。";
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(650, 421);
            this.Controls.Add(this.Caution);
            this.Controls.Add(this.loseGroupBox);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.resetGroupBox);
            this.Controls.Add(this.winGroupBox);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.winGroupBox.ResumeLayout(false);
            this.winGroupBox.PerformLayout();
            this.loseGroupBox.ResumeLayout(false);
            this.loseGroupBox.PerformLayout();
            this.resetGroupBox.ResumeLayout(false);
            this.resetGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart1;
        private System.Windows.Forms.Label winLabel;
        private System.Windows.Forms.GroupBox winGroupBox;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox loseGroupBox;
        private System.Windows.Forms.Button btnStart2;
        private System.Windows.Forms.Label loseLabel;
        private System.Windows.Forms.GroupBox resetGroupBox;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label resetLabel;
        private System.Windows.Forms.Label Caution;
    }
}