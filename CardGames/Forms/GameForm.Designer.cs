namespace CardGames
{
    partial class GameForm
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
            this.DateOfCUP1 = new System.Windows.Forms.Label();
            this.DateOfCUP2 = new System.Windows.Forms.Label();
            this.DateOfCUP3 = new System.Windows.Forms.Label();
            this.AreaOfThrownCards = new System.Windows.Forms.Label();
            this.DateOfPlayer = new System.Windows.Forms.Label();
            this.OPGuide = new System.Windows.Forms.Label();
            this.GameLog = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DateOfCUP1
            // 
            this.DateOfCUP1.AutoSize = true;
            this.DateOfCUP1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfCUP1.Location = new System.Drawing.Point(24, 22);
            this.DateOfCUP1.Name = "DateOfCUP1";
            this.DateOfCUP1.Size = new System.Drawing.Size(245, 20);
            this.DateOfCUP1.TabIndex = 0;
            this.DateOfCUP1.Text = "CPU１のてふだ　のこり●まい　";
            // 
            // DateOfCUP2
            // 
            this.DateOfCUP2.AutoSize = true;
            this.DateOfCUP2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfCUP2.Location = new System.Drawing.Point(663, 22);
            this.DateOfCUP2.Name = "DateOfCUP2";
            this.DateOfCUP2.Size = new System.Drawing.Size(241, 20);
            this.DateOfCUP2.TabIndex = 1;
            this.DateOfCUP2.Text = "CPU2のてふだ　のこり●まい　";
            // 
            // DateOfCUP3
            // 
            this.DateOfCUP3.AutoSize = true;
            this.DateOfCUP3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfCUP3.Location = new System.Drawing.Point(640, 276);
            this.DateOfCUP3.Name = "DateOfCUP3";
            this.DateOfCUP3.Size = new System.Drawing.Size(241, 20);
            this.DateOfCUP3.TabIndex = 2;
            this.DateOfCUP3.Text = "CPU3のてふだ　のこり●まい　";
            // 
            // AreaOfThrownCards
            // 
            this.AreaOfThrownCards.AutoSize = true;
            this.AreaOfThrownCards.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AreaOfThrownCards.Location = new System.Drawing.Point(386, 82);
            this.AreaOfThrownCards.Name = "AreaOfThrownCards";
            this.AreaOfThrownCards.Size = new System.Drawing.Size(116, 20);
            this.AreaOfThrownCards.TabIndex = 3;
            this.AreaOfThrownCards.Text = "すてふだエリア";
            // 
            // DateOfPlayer
            // 
            this.DateOfPlayer.AutoSize = true;
            this.DateOfPlayer.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfPlayer.Location = new System.Drawing.Point(66, 276);
            this.DateOfPlayer.Name = "DateOfPlayer";
            this.DateOfPlayer.Size = new System.Drawing.Size(233, 20);
            this.DateOfPlayer.TabIndex = 4;
            this.DateOfPlayer.Text = "■■のてふだ　のこり●まい　";
            // 
            // OPGuide
            // 
            this.OPGuide.AutoSize = true;
            this.OPGuide.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OPGuide.Location = new System.Drawing.Point(85, 361);
            this.OPGuide.Name = "OPGuide";
            this.OPGuide.Size = new System.Drawing.Size(94, 20);
            this.OPGuide.TabIndex = 5;
            this.OPGuide.Text = "そうさガイド";
            // 
            // GameLog
            // 
            this.GameLog.AutoSize = true;
            this.GameLog.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GameLog.Location = new System.Drawing.Point(85, 466);
            this.GameLog.Name = "GameLog";
            this.GameLog.Size = new System.Drawing.Size(86, 20);
            this.GameLog.TabIndex = 6;
            this.GameLog.Text = "ゲームログ";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStart.Location = new System.Drawing.Point(667, 512);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(82, 58);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "かいし";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnBack.Location = new System.Drawing.Point(765, 512);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(82, 58);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "もどる";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 592);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.GameLog);
            this.Controls.Add(this.OPGuide);
            this.Controls.Add(this.DateOfPlayer);
            this.Controls.Add(this.AreaOfThrownCards);
            this.Controls.Add(this.DateOfCUP3);
            this.Controls.Add(this.DateOfCUP2);
            this.Controls.Add(this.DateOfCUP1);
            this.Name = "GameForm";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DateOfCUP1;
        private System.Windows.Forms.Label DateOfCUP2;
        private System.Windows.Forms.Label DateOfCUP3;
        private System.Windows.Forms.Label AreaOfThrownCards;
        private System.Windows.Forms.Label DateOfPlayer;
        private System.Windows.Forms.Label OPGuide;
        private System.Windows.Forms.Label GameLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnBack;
    }
}