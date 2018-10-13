namespace T2R.Forms
{
    partial class ticketToRideForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ticketToRideForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.visibleCardbtn5 = new System.Windows.Forms.Button();
            this.visibleCardbtn4 = new System.Windows.Forms.Button();
            this.visibleCardbtn3 = new System.Windows.Forms.Button();
            this.visibleCardbtn2 = new System.Windows.Forms.Button();
            this.visibleCardbtn1 = new System.Windows.Forms.Button();
            this.VisibleCardsbox = new System.Windows.Forms.GroupBox();
            this.Deckbutton = new System.Windows.Forms.Button();
            this.destinationCardButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.VisibleCardsbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(628, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.newGameToolStripMenuItem.Text = "New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(6, 28);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(456, 378);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // visibleCardbtn5
            // 
            this.visibleCardbtn5.Location = new System.Drawing.Point(3, 237);
            this.visibleCardbtn5.Margin = new System.Windows.Forms.Padding(2);
            this.visibleCardbtn5.Name = "visibleCardbtn5";
            this.visibleCardbtn5.Size = new System.Drawing.Size(129, 52);
            this.visibleCardbtn5.TabIndex = 0;
            this.visibleCardbtn5.UseVisualStyleBackColor = true;
            // 
            // visibleCardbtn4
            // 
            this.visibleCardbtn4.Location = new System.Drawing.Point(3, 182);
            this.visibleCardbtn4.Margin = new System.Windows.Forms.Padding(2);
            this.visibleCardbtn4.Name = "visibleCardbtn4";
            this.visibleCardbtn4.Size = new System.Drawing.Size(129, 52);
            this.visibleCardbtn4.TabIndex = 1;
            this.visibleCardbtn4.UseVisualStyleBackColor = true;
            // 
            // visibleCardbtn3
            // 
            this.visibleCardbtn3.Location = new System.Drawing.Point(3, 127);
            this.visibleCardbtn3.Margin = new System.Windows.Forms.Padding(2);
            this.visibleCardbtn3.Name = "visibleCardbtn3";
            this.visibleCardbtn3.Size = new System.Drawing.Size(129, 52);
            this.visibleCardbtn3.TabIndex = 2;
            this.visibleCardbtn3.UseVisualStyleBackColor = true;
            // 
            // visibleCardbtn2
            // 
            this.visibleCardbtn2.Location = new System.Drawing.Point(3, 73);
            this.visibleCardbtn2.Margin = new System.Windows.Forms.Padding(2);
            this.visibleCardbtn2.Name = "visibleCardbtn2";
            this.visibleCardbtn2.Size = new System.Drawing.Size(129, 52);
            this.visibleCardbtn2.TabIndex = 3;
            this.visibleCardbtn2.UseVisualStyleBackColor = true;
            // 
            // visibleCardbtn1
            // 
            this.visibleCardbtn1.Location = new System.Drawing.Point(3, 18);
            this.visibleCardbtn1.Margin = new System.Windows.Forms.Padding(2);
            this.visibleCardbtn1.Name = "visibleCardbtn1";
            this.visibleCardbtn1.Size = new System.Drawing.Size(129, 52);
            this.visibleCardbtn1.TabIndex = 4;
            this.visibleCardbtn1.UseVisualStyleBackColor = true;
            // 
            // VisibleCardsbox
            // 
            this.VisibleCardsbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.VisibleCardsbox.AutoSize = true;
            this.VisibleCardsbox.BackColor = System.Drawing.Color.Transparent;
            this.VisibleCardsbox.Controls.Add(this.Deckbutton);
            this.VisibleCardsbox.Controls.Add(this.destinationCardButton);
            this.VisibleCardsbox.Controls.Add(this.visibleCardbtn5);
            this.VisibleCardsbox.Controls.Add(this.visibleCardbtn4);
            this.VisibleCardsbox.Controls.Add(this.visibleCardbtn3);
            this.VisibleCardsbox.Controls.Add(this.visibleCardbtn1);
            this.VisibleCardsbox.Controls.Add(this.visibleCardbtn2);
            this.VisibleCardsbox.Location = new System.Drawing.Point(466, 16);
            this.VisibleCardsbox.Margin = new System.Windows.Forms.Padding(2);
            this.VisibleCardsbox.Name = "VisibleCardsbox";
            this.VisibleCardsbox.Padding = new System.Windows.Forms.Padding(2);
            this.VisibleCardsbox.Size = new System.Drawing.Size(138, 414);
            this.VisibleCardsbox.TabIndex = 3;
            this.VisibleCardsbox.TabStop = false;
            this.VisibleCardsbox.Text = "cards";
            // 
            // Deckbutton
            // 
            this.Deckbutton.Location = new System.Drawing.Point(64, 292);
            this.Deckbutton.Margin = new System.Windows.Forms.Padding(2);
            this.Deckbutton.Name = "Deckbutton";
            this.Deckbutton.Size = new System.Drawing.Size(68, 86);
            this.Deckbutton.TabIndex = 6;
            this.Deckbutton.UseVisualStyleBackColor = true;
            // 
            // destinationCardButton
            // 
            this.destinationCardButton.Location = new System.Drawing.Point(3, 292);
            this.destinationCardButton.Margin = new System.Windows.Forms.Padding(2);
            this.destinationCardButton.Name = "destinationCardButton";
            this.destinationCardButton.Size = new System.Drawing.Size(64, 86);
            this.destinationCardButton.TabIndex = 5;
            this.destinationCardButton.UseVisualStyleBackColor = true;
            // 
            // ticketToRideForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 417);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.VisibleCardsbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ticketToRideForm";
            this.Text = "Ticket_To_Ride";
            this.Load += new System.EventHandler(this.ticketToRideForm_Load);
            this.Resize += new System.EventHandler(this.ticketToRideForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.VisibleCardsbox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button visibleCardbtn1;
        private System.Windows.Forms.Button visibleCardbtn2;
        private System.Windows.Forms.Button visibleCardbtn3;
        private System.Windows.Forms.Button visibleCardbtn4;
        private System.Windows.Forms.Button visibleCardbtn5;
        private System.Windows.Forms.GroupBox VisibleCardsbox;
        private System.Windows.Forms.Button Deckbutton;
        private System.Windows.Forms.Button destinationCardButton;
    }
}