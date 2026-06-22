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
            this.btnMainAction = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.flpCpu1Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox_Result = new System.Windows.Forms.PictureBox();
            this.flpThrown = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCpu2Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.flpPlayerHand = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCpu3Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.lblResults = new System.Windows.Forms.Label();
            this.Operation = new System.Windows.Forms.Label();
            this.Logs = new System.Windows.Forms.Label();
            this.flpCpu1Hand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).BeginInit();
            this.SuspendLayout();
            // 
            // DateOfCUP1
            // 
            this.DateOfCUP1.AutoSize = true;
            this.DateOfCUP1.BackColor = System.Drawing.Color.Transparent;
            this.DateOfCUP1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfCUP1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DateOfCUP1.Location = new System.Drawing.Point(24, 22);
            this.DateOfCUP1.Name = "DateOfCUP1";
            this.DateOfCUP1.Size = new System.Drawing.Size(245, 20);
            this.DateOfCUP1.TabIndex = 0;
            this.DateOfCUP1.Text = "CPU１のてふだ　のこり●まい　";
            // 
            // DateOfCUP2
            // 
            this.DateOfCUP2.AutoSize = true;
            this.DateOfCUP2.BackColor = System.Drawing.Color.Transparent;
            this.DateOfCUP2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfCUP2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DateOfCUP2.Location = new System.Drawing.Point(663, 22);
            this.DateOfCUP2.Name = "DateOfCUP2";
            this.DateOfCUP2.Size = new System.Drawing.Size(241, 20);
            this.DateOfCUP2.TabIndex = 1;
            this.DateOfCUP2.Text = "CPU2のてふだ　のこり●まい　";
            // 
            // DateOfCUP3
            // 
            this.DateOfCUP3.AutoSize = true;
            this.DateOfCUP3.BackColor = System.Drawing.Color.Transparent;
            this.DateOfCUP3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfCUP3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DateOfCUP3.Location = new System.Drawing.Point(663, 227);
            this.DateOfCUP3.Name = "DateOfCUP3";
            this.DateOfCUP3.Size = new System.Drawing.Size(241, 20);
            this.DateOfCUP3.TabIndex = 2;
            this.DateOfCUP3.Text = "CPU3のてふだ　のこり●まい　";
            // 
            // AreaOfThrownCards
            // 
            this.AreaOfThrownCards.AutoSize = true;
            this.AreaOfThrownCards.BackColor = System.Drawing.Color.Transparent;
            this.AreaOfThrownCards.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AreaOfThrownCards.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AreaOfThrownCards.Location = new System.Drawing.Point(405, 123);
            this.AreaOfThrownCards.Name = "AreaOfThrownCards";
            this.AreaOfThrownCards.Size = new System.Drawing.Size(116, 20);
            this.AreaOfThrownCards.TabIndex = 3;
            this.AreaOfThrownCards.Text = "すてふだエリア";
            // 
            // DateOfPlayer
            // 
            this.DateOfPlayer.AutoSize = true;
            this.DateOfPlayer.BackColor = System.Drawing.Color.Transparent;
            this.DateOfPlayer.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DateOfPlayer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DateOfPlayer.Location = new System.Drawing.Point(32, 227);
            this.DateOfPlayer.Name = "DateOfPlayer";
            this.DateOfPlayer.Size = new System.Drawing.Size(233, 20);
            this.DateOfPlayer.TabIndex = 4;
            this.DateOfPlayer.Text = "■■のてふだ　のこり●まい　";
            // 
            // btnMainAction
            // 
            this.btnMainAction.BackgroundImage = global::CardGames.Properties.Resources.btn_default;
            this.btnMainAction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMainAction.Font = new System.Drawing.Font("Algerian", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainAction.Location = new System.Drawing.Point(344, 22);
            this.btnMainAction.Name = "btnMainAction";
            this.btnMainAction.Size = new System.Drawing.Size(110, 70);
            this.btnMainAction.TabIndex = 7;
            this.btnMainAction.Text = "かいし";
            this.btnMainAction.UseVisualStyleBackColor = true;
            this.btnMainAction.Click += new System.EventHandler(this.btnMainAction_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::CardGames.Properties.Resources.btn_default;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.Font = new System.Drawing.Font("Algerian", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(471, 22);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(110, 70);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "もどる";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // flpCpu1Hand
            // 
            this.flpCpu1Hand.AutoScroll = true;
            this.flpCpu1Hand.BackColor = System.Drawing.Color.Transparent;
            this.flpCpu1Hand.Controls.Add(this.pictureBox_Result);
            this.flpCpu1Hand.Location = new System.Drawing.Point(22, 56);
            this.flpCpu1Hand.Name = "flpCpu1Hand";
            this.flpCpu1Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu1Hand.TabIndex = 11;
            this.flpCpu1Hand.WrapContents = false;
            // 
            // pictureBox_Result
            // 
            this.pictureBox_Result.BackColor = System.Drawing.SystemColors.Highlight;
            this.pictureBox_Result.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_Result.Name = "pictureBox_Result";
            this.pictureBox_Result.Size = new System.Drawing.Size(852, 568);
            this.pictureBox_Result.TabIndex = 18;
            this.pictureBox_Result.TabStop = false;
            this.pictureBox_Result.Visible = false;
            // 
            // flpThrown
            // 
            this.flpThrown.AutoScroll = true;
            this.flpThrown.BackColor = System.Drawing.Color.Transparent;
            this.flpThrown.Location = new System.Drawing.Point(344, 160);
            this.flpThrown.Name = "flpThrown";
            this.flpThrown.Size = new System.Drawing.Size(237, 160);
            this.flpThrown.TabIndex = 12;
            this.flpThrown.WrapContents = false;
            // 
            // flpCpu2Hand
            // 
            this.flpCpu2Hand.AutoScroll = true;
            this.flpCpu2Hand.BackColor = System.Drawing.Color.Transparent;
            this.flpCpu2Hand.Location = new System.Drawing.Point(667, 56);
            this.flpCpu2Hand.Name = "flpCpu2Hand";
            this.flpCpu2Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu2Hand.TabIndex = 13;
            this.flpCpu2Hand.WrapContents = false;
            // 
            // flpPlayerHand
            // 
            this.flpPlayerHand.AutoScroll = true;
            this.flpPlayerHand.BackColor = System.Drawing.Color.Transparent;
            this.flpPlayerHand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flpPlayerHand.Location = new System.Drawing.Point(25, 259);
            this.flpPlayerHand.Name = "flpPlayerHand";
            this.flpPlayerHand.Size = new System.Drawing.Size(237, 140);
            this.flpPlayerHand.TabIndex = 13;
            this.flpPlayerHand.WrapContents = false;
            // 
            // flpCpu3Hand
            // 
            this.flpCpu3Hand.AutoScroll = true;
            this.flpCpu3Hand.BackColor = System.Drawing.Color.Transparent;
            this.flpCpu3Hand.Location = new System.Drawing.Point(667, 259);
            this.flpCpu3Hand.Name = "flpCpu3Hand";
            this.flpCpu3Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu3Hand.TabIndex = 14;
            this.flpCpu3Hand.WrapContents = false;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.BackColor = System.Drawing.Color.Transparent;
            this.lblResults.Enabled = false;
            this.lblResults.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblResults.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblResults.Location = new System.Drawing.Point(311, 407);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(116, 23);
            this.lblResults.TabIndex = 17;
            this.lblResults.Text = " しょう　はい";
            // 
            // Operation
            // 
            this.Operation.BackColor = System.Drawing.Color.Transparent;
            this.Operation.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Operation.ForeColor = System.Drawing.Color.Gold;
            this.Operation.Location = new System.Drawing.Point(59, 455);
            this.Operation.Name = "Operation";
            this.Operation.Size = new System.Drawing.Size(531, 78);
            this.Operation.TabIndex = 18;
            this.Operation.Text = "操作ガイド";
            this.Operation.Click += new System.EventHandler(this.Operation_Click);
            // 
            // Logs
            // 
            this.Logs.BackColor = System.Drawing.Color.Transparent;
            this.Logs.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Logs.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Logs.Location = new System.Drawing.Point(59, 546);
            this.Logs.Name = "Logs";
            this.Logs.Size = new System.Drawing.Size(876, 113);
            this.Logs.TabIndex = 19;
            this.Logs.Text = "操作ログ";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 696);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.Operation);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.flpCpu3Hand);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.flpPlayerHand);
            this.Controls.Add(this.btnMainAction);
            this.Controls.Add(this.flpCpu2Hand);
            this.Controls.Add(this.flpThrown);
            this.Controls.Add(this.flpCpu1Hand);
            this.Controls.Add(this.DateOfPlayer);
            this.Controls.Add(this.AreaOfThrownCards);
            this.Controls.Add(this.DateOfCUP3);
            this.Controls.Add(this.DateOfCUP2);
            this.Controls.Add(this.DateOfCUP1);
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.Text = "ゲーム画面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.Click += new System.EventHandler(this.Operation_Click);
            this.flpCpu1Hand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DateOfCUP1;
        private System.Windows.Forms.Label DateOfCUP2;
        private System.Windows.Forms.Label DateOfCUP3;
        private System.Windows.Forms.Label AreaOfThrownCards;
        private System.Windows.Forms.Label DateOfPlayer;
        private System.Windows.Forms.Button btnMainAction;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.FlowLayoutPanel flpCpu1Hand;
        private System.Windows.Forms.FlowLayoutPanel flpThrown;
        private System.Windows.Forms.FlowLayoutPanel flpCpu2Hand;
        private System.Windows.Forms.FlowLayoutPanel flpPlayerHand;
        private System.Windows.Forms.FlowLayoutPanel flpCpu3Hand;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.PictureBox pictureBox_Result;
        private System.Windows.Forms.Label Operation;
        private System.Windows.Forms.Label Logs;
    }
}