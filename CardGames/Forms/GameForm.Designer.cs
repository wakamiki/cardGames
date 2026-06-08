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
            this.btnMainAction = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.Operattion = new System.Windows.Forms.TextBox();
            this.Logs = new System.Windows.Forms.TextBox();
            this.flpCpu1Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCpu2Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.flpPlayerHand = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCpu3Hand = new System.Windows.Forms.FlowLayoutPanel();
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
            this.DateOfCUP3.Location = new System.Drawing.Point(663, 246);
            this.DateOfCUP3.Name = "DateOfCUP3";
            this.DateOfCUP3.Size = new System.Drawing.Size(241, 20);
            this.DateOfCUP3.TabIndex = 2;
            this.DateOfCUP3.Text = "CPU3のてふだ　のこり●まい　";
            // 
            // AreaOfThrownCards
            // 
            this.AreaOfThrownCards.AutoSize = true;
            this.AreaOfThrownCards.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AreaOfThrownCards.Location = new System.Drawing.Point(391, 60);
            this.AreaOfThrownCards.Name = "AreaOfThrownCards";
            this.AreaOfThrownCards.Size = new System.Drawing.Size(116, 20);
            this.AreaOfThrownCards.TabIndex = 3;
            this.AreaOfThrownCards.Text = "すてふだエリア";
            // 
            // DateOfPlayer
            // 
            this.DateOfPlayer.AutoSize = true;
            this.DateOfPlayer.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfPlayer.Location = new System.Drawing.Point(26, 246);
            this.DateOfPlayer.Name = "DateOfPlayer";
            this.DateOfPlayer.Size = new System.Drawing.Size(233, 20);
            this.DateOfPlayer.TabIndex = 4;
            this.DateOfPlayer.Text = "■■のてふだ　のこり●まい　";
            // 
            // OPGuide
            // 
            this.OPGuide.AutoSize = true;
            this.OPGuide.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OPGuide.Location = new System.Drawing.Point(93, 446);
            this.OPGuide.Name = "OPGuide";
            this.OPGuide.Size = new System.Drawing.Size(94, 20);
            this.OPGuide.TabIndex = 5;
            this.OPGuide.Text = "そうさガイド";
            // 
            // GameLog
            // 
            this.GameLog.AutoSize = true;
            this.GameLog.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GameLog.Location = new System.Drawing.Point(93, 579);
            this.GameLog.Name = "GameLog";
            this.GameLog.Size = new System.Drawing.Size(86, 20);
            this.GameLog.TabIndex = 6;
            this.GameLog.Text = "ゲームログ";
            // 
            // btnMainAction
            // 
            this.btnMainAction.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMainAction.Location = new System.Drawing.Point(695, 605);
            this.btnMainAction.Name = "btnMainAction";
            this.btnMainAction.Size = new System.Drawing.Size(82, 58);
            this.btnMainAction.TabIndex = 7;
            this.btnMainAction.Text = "かいし";
            this.btnMainAction.UseVisualStyleBackColor = true;
            this.btnMainAction.Click += new System.EventHandler(this.btnMainAction_Click);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnBack.Location = new System.Drawing.Point(792, 605);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(82, 58);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "もどる";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // Operattion
            // 
            this.Operattion.Location = new System.Drawing.Point(70, 446);
            this.Operattion.Multiline = true;
            this.Operattion.Name = "Operattion";
            this.Operattion.ReadOnly = true;
            this.Operattion.Size = new System.Drawing.Size(804, 115);
            this.Operattion.TabIndex = 9;
            // 
            // Logs
            // 
            this.Logs.Location = new System.Drawing.Point(70, 567);
            this.Logs.Multiline = true;
            this.Logs.Name = "Logs";
            this.Logs.ReadOnly = true;
            this.Logs.Size = new System.Drawing.Size(600, 118);
            this.Logs.TabIndex = 10;
            // 
            // flpCpu1Hand
            // 
            this.flpCpu1Hand.AutoScroll = true;
            this.flpCpu1Hand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpCpu1Hand.Location = new System.Drawing.Point(22, 56);
            this.flpCpu1Hand.Name = "flpCpu1Hand";
            this.flpCpu1Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu1Hand.TabIndex = 11;
            this.flpCpu1Hand.WrapContents = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(338, 94);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(237, 160);
            this.flowLayoutPanel2.TabIndex = 12;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // flpCpu2Hand
            // 
            this.flpCpu2Hand.AutoScroll = true;
            this.flpCpu2Hand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpCpu2Hand.Location = new System.Drawing.Point(667, 56);
            this.flpCpu2Hand.Name = "flpCpu2Hand";
            this.flpCpu2Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu2Hand.TabIndex = 13;
            this.flpCpu2Hand.WrapContents = false;
            // 
            // flpPlayerHand
            // 
            this.flpPlayerHand.AutoScroll = true;
            this.flpPlayerHand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpPlayerHand.Location = new System.Drawing.Point(28, 288);
            this.flpPlayerHand.Name = "flpPlayerHand";
            this.flpPlayerHand.Size = new System.Drawing.Size(237, 140);
            this.flpPlayerHand.TabIndex = 13;
            this.flpPlayerHand.WrapContents = false;
            // 
            // flpCpu3Hand
            // 
            this.flpCpu3Hand.AutoScroll = true;
            this.flpCpu3Hand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpCpu3Hand.Location = new System.Drawing.Point(667, 269);
            this.flpCpu3Hand.Name = "flpCpu3Hand";
            this.flpCpu3Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu3Hand.TabIndex = 14;
            this.flpCpu3Hand.WrapContents = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 705);
            this.Controls.Add(this.flpCpu3Hand);
            this.Controls.Add(this.flpPlayerHand);
            this.Controls.Add(this.OPGuide);
            this.Controls.Add(this.flpCpu2Hand);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.GameLog);
            this.Controls.Add(this.flpCpu1Hand);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnMainAction);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.Operattion);
            this.Controls.Add(this.DateOfPlayer);
            this.Controls.Add(this.AreaOfThrownCards);
            this.Controls.Add(this.DateOfCUP3);
            this.Controls.Add(this.DateOfCUP2);
            this.Controls.Add(this.DateOfCUP1);
            this.Name = "GameForm";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.GameForm_Load);
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
        private System.Windows.Forms.Button btnMainAction;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox Operattion;
        private System.Windows.Forms.TextBox Logs;
        private System.Windows.Forms.FlowLayoutPanel flpCpu1Hand;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flpCpu2Hand;
        private System.Windows.Forms.FlowLayoutPanel flpPlayerHand;
        private System.Windows.Forms.FlowLayoutPanel flpCpu3Hand;
    }
}