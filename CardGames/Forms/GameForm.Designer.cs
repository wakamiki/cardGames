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
            this.Operattion = new System.Windows.Forms.TextBox();
            this.Logs = new System.Windows.Forms.TextBox();
            this.flpCpu1Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox_Result = new System.Windows.Forms.PictureBox();
            this.pnl_Active_CPU1 = new System.Windows.Forms.Panel();
            this.flpThrown = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCpu2Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_Active_CPU2 = new System.Windows.Forms.Panel();
            this.flpPlayerHand = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_Active_User = new System.Windows.Forms.Panel();
            this.flpCpu3Hand = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_Active_CPU3 = new System.Windows.Forms.Panel();
            this.pnl_Shadow = new System.Windows.Forms.Panel();
            this.lblResults = new System.Windows.Forms.Label();
            this.flpCpu1Hand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).BeginInit();
            this.flpCpu2Hand.SuspendLayout();
            this.flpPlayerHand.SuspendLayout();
            this.flpCpu3Hand.SuspendLayout();
            this.pnl_Shadow.SuspendLayout();
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
            this.DateOfCUP3.Location = new System.Drawing.Point(663, 227);
            this.DateOfCUP3.Name = "DateOfCUP3";
            this.DateOfCUP3.Size = new System.Drawing.Size(241, 20);
            this.DateOfCUP3.TabIndex = 2;
            this.DateOfCUP3.Text = "CPU3のてふだ　のこり●まい　";
            // 
            // AreaOfThrownCards
            // 
            this.AreaOfThrownCards.AutoSize = true;
            this.AreaOfThrownCards.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AreaOfThrownCards.Location = new System.Drawing.Point(385, 126);
            this.AreaOfThrownCards.Name = "AreaOfThrownCards";
            this.AreaOfThrownCards.Size = new System.Drawing.Size(116, 20);
            this.AreaOfThrownCards.TabIndex = 3;
            this.AreaOfThrownCards.Text = "すてふだエリア";
            // 
            // DateOfPlayer
            // 
            this.DateOfPlayer.AutoSize = true;
            this.DateOfPlayer.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            this.btnMainAction.Location = new System.Drawing.Point(360, 22);
            this.btnMainAction.Name = "btnMainAction";
            this.btnMainAction.Size = new System.Drawing.Size(100, 70);
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
            this.btnBack.Location = new System.Drawing.Point(481, 22);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 70);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "もどる";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Operattion
            // 
            this.Operattion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Operattion.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Operattion.Location = new System.Drawing.Point(4, 4);
            this.Operattion.Multiline = true;
            this.Operattion.Name = "Operattion";
            this.Operattion.ReadOnly = true;
            this.Operattion.Size = new System.Drawing.Size(875, 115);
            this.Operattion.TabIndex = 9;
            // 
            // Logs
            // 
            this.Logs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Logs.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Logs.Location = new System.Drawing.Point(4, 125);
            this.Logs.Multiline = true;
            this.Logs.Name = "Logs";
            this.Logs.ReadOnly = true;
            this.Logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Logs.Size = new System.Drawing.Size(875, 118);
            this.Logs.TabIndex = 10;
            // 
            // flpCpu1Hand
            // 
            this.flpCpu1Hand.AutoScroll = true;
            this.flpCpu1Hand.BackColor = System.Drawing.Color.Transparent;
            this.flpCpu1Hand.Controls.Add(this.pictureBox_Result);
            this.flpCpu1Hand.Controls.Add(this.pnl_Active_CPU1);
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
            // pnl_Active_CPU1
            // 
            this.pnl_Active_CPU1.BackColor = System.Drawing.Color.Gold;
            this.pnl_Active_CPU1.Location = new System.Drawing.Point(861, 3);
            this.pnl_Active_CPU1.Name = "pnl_Active_CPU1";
            this.pnl_Active_CPU1.Size = new System.Drawing.Size(234, 137);
            this.pnl_Active_CPU1.TabIndex = 16;
            this.pnl_Active_CPU1.Visible = false;
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
            this.flpCpu2Hand.Controls.Add(this.pnl_Active_CPU2);
            this.flpCpu2Hand.Location = new System.Drawing.Point(667, 56);
            this.flpCpu2Hand.Name = "flpCpu2Hand";
            this.flpCpu2Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu2Hand.TabIndex = 13;
            this.flpCpu2Hand.WrapContents = false;
            // 
            // pnl_Active_CPU2
            // 
            this.pnl_Active_CPU2.BackColor = System.Drawing.Color.Gold;
            this.pnl_Active_CPU2.Location = new System.Drawing.Point(3, 3);
            this.pnl_Active_CPU2.Name = "pnl_Active_CPU2";
            this.pnl_Active_CPU2.Size = new System.Drawing.Size(234, 137);
            this.pnl_Active_CPU2.TabIndex = 17;
            this.pnl_Active_CPU2.Visible = false;
            // 
            // flpPlayerHand
            // 
            this.flpPlayerHand.AutoScroll = true;
            this.flpPlayerHand.BackColor = System.Drawing.Color.Transparent;
            this.flpPlayerHand.Controls.Add(this.pnl_Active_User);
            this.flpPlayerHand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flpPlayerHand.Location = new System.Drawing.Point(25, 259);
            this.flpPlayerHand.Name = "flpPlayerHand";
            this.flpPlayerHand.Size = new System.Drawing.Size(237, 140);
            this.flpPlayerHand.TabIndex = 13;
            this.flpPlayerHand.WrapContents = false;
            // 
            // pnl_Active_User
            // 
            this.pnl_Active_User.BackColor = System.Drawing.Color.Gold;
            this.pnl_Active_User.Location = new System.Drawing.Point(3, 3);
            this.pnl_Active_User.Name = "pnl_Active_User";
            this.pnl_Active_User.Size = new System.Drawing.Size(241, 137);
            this.pnl_Active_User.TabIndex = 19;
            this.pnl_Active_User.Visible = false;
            // 
            // flpCpu3Hand
            // 
            this.flpCpu3Hand.AutoScroll = true;
            this.flpCpu3Hand.BackColor = System.Drawing.Color.Transparent;
            this.flpCpu3Hand.Controls.Add(this.pnl_Active_CPU3);
            this.flpCpu3Hand.Location = new System.Drawing.Point(667, 259);
            this.flpCpu3Hand.Name = "flpCpu3Hand";
            this.flpCpu3Hand.Size = new System.Drawing.Size(237, 140);
            this.flpCpu3Hand.TabIndex = 14;
            this.flpCpu3Hand.WrapContents = false;
            // 
            // pnl_Active_CPU3
            // 
            this.pnl_Active_CPU3.BackColor = System.Drawing.Color.Gold;
            this.pnl_Active_CPU3.Location = new System.Drawing.Point(3, 3);
            this.pnl_Active_CPU3.Name = "pnl_Active_CPU3";
            this.pnl_Active_CPU3.Size = new System.Drawing.Size(234, 137);
            this.pnl_Active_CPU3.TabIndex = 18;
            this.pnl_Active_CPU3.Visible = false;
            // 
            // pnl_Shadow
            // 
            this.pnl_Shadow.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnl_Shadow.Controls.Add(this.Operattion);
            this.pnl_Shadow.Controls.Add(this.Logs);
            this.pnl_Shadow.Location = new System.Drawing.Point(57, 433);
            this.pnl_Shadow.Name = "pnl_Shadow";
            this.pnl_Shadow.Size = new System.Drawing.Size(882, 248);
            this.pnl_Shadow.TabIndex = 16;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblResults.Location = new System.Drawing.Point(385, 376);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(116, 23);
            this.lblResults.TabIndex = 17;
            this.lblResults.Text = " しょう　はい";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 696);
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
            this.Controls.Add(this.pnl_Shadow);
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.Text = "ゲーム画面";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.flpCpu1Hand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).EndInit();
            this.flpCpu2Hand.ResumeLayout(false);
            this.flpPlayerHand.ResumeLayout(false);
            this.flpCpu3Hand.ResumeLayout(false);
            this.pnl_Shadow.ResumeLayout(false);
            this.pnl_Shadow.PerformLayout();
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
        private System.Windows.Forms.TextBox Operattion;
        private System.Windows.Forms.TextBox Logs;
        private System.Windows.Forms.FlowLayoutPanel flpCpu1Hand;
        private System.Windows.Forms.FlowLayoutPanel flpThrown;
        private System.Windows.Forms.FlowLayoutPanel flpCpu2Hand;
        private System.Windows.Forms.FlowLayoutPanel flpPlayerHand;
        private System.Windows.Forms.FlowLayoutPanel flpCpu3Hand;
        private System.Windows.Forms.Panel pnl_Shadow;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.PictureBox pictureBox_Result;
        private System.Windows.Forms.Panel pnl_Active_CPU1;
        private System.Windows.Forms.Panel pnl_Active_CPU2;
        private System.Windows.Forms.Panel pnl_Active_User;
        private System.Windows.Forms.Panel pnl_Active_CPU3;
    }
}