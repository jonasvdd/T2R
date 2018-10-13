namespace T2R.Forms
{
    partial class PlayersOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayersOptionsForm));
            this.cbxPossibilities = new System.Windows.Forms.ComboBox();
            this.validateButton = new System.Windows.Forms.Button();
            this.playerInfoTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbxPossibilities
            // 
            this.cbxPossibilities.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPossibilities.FormattingEnabled = true;
            this.cbxPossibilities.Location = new System.Drawing.Point(48, 268);
            this.cbxPossibilities.Name = "cbxPossibilities";
            this.cbxPossibilities.Size = new System.Drawing.Size(273, 39);
            this.cbxPossibilities.TabIndex = 0;
            // 
            // validateButton
            // 
            this.validateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validateButton.Location = new System.Drawing.Point(357, 268);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(174, 39);
            this.validateButton.TabIndex = 1;
            this.validateButton.Text = "validate";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // playerInfoTextbox
            // 
            this.playerInfoTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerInfoTextbox.Location = new System.Drawing.Point(48, 38);
            this.playerInfoTextbox.Multiline = true;
            this.playerInfoTextbox.Name = "playerInfoTextbox";
            this.playerInfoTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.playerInfoTextbox.Size = new System.Drawing.Size(483, 183);
            this.playerInfoTextbox.TabIndex = 2;
            // 
            // PlayersOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 368);
            this.Controls.Add(this.playerInfoTextbox);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.cbxPossibilities);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayersOptionsForm";
            this.Text = "PlayersOptionsForm";
            this.Load += new System.EventHandler(this.PlayersOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxPossibilities;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.TextBox playerInfoTextbox;
    }
}