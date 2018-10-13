namespace T2R.Forms
{
    partial class AmountofPlayersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmountofPlayersForm));
            this.nudCoOps = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudRealPlayer = new System.Windows.Forms.NumericUpDown();
            this.Validationbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudCoOps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRealPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // nudCoOps
            // 
            this.nudCoOps.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCoOps.Location = new System.Drawing.Point(20, 69);
            this.nudCoOps.Margin = new System.Windows.Forms.Padding(2);
            this.nudCoOps.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudCoOps.Name = "nudCoOps";
            this.nudCoOps.Size = new System.Drawing.Size(60, 38);
            this.nudCoOps.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "how many intelligent CO-OPS?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "How many real players?";
            // 
            // nudRealPlayer
            // 
            this.nudRealPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRealPlayer.Location = new System.Drawing.Point(20, 176);
            this.nudRealPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.nudRealPlayer.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRealPlayer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRealPlayer.Name = "nudRealPlayer";
            this.nudRealPlayer.Size = new System.Drawing.Size(60, 38);
            this.nudRealPlayer.TabIndex = 3;
            this.nudRealPlayer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Validationbtn
            // 
            this.Validationbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Validationbtn.Location = new System.Drawing.Point(168, 215);
            this.Validationbtn.Margin = new System.Windows.Forms.Padding(2);
            this.Validationbtn.Name = "Validationbtn";
            this.Validationbtn.Size = new System.Drawing.Size(125, 44);
            this.Validationbtn.TabIndex = 4;
            this.Validationbtn.Text = "validate";
            this.Validationbtn.UseVisualStyleBackColor = true;
            this.Validationbtn.Click += new System.EventHandler(this.Validationbtn_Click);
            // 
            // AmountofPlayersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(463, 328);
            this.Controls.Add(this.Validationbtn);
            this.Controls.Add(this.nudRealPlayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudCoOps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AmountofPlayersForm";
            this.Text = "players";
            this.Load += new System.EventHandler(this.players_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCoOps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRealPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudCoOps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudRealPlayer;
        private System.Windows.Forms.Button Validationbtn;
    }
}