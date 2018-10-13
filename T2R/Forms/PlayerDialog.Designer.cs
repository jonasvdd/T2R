namespace T2R.Forms
{
    partial class PlayerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerDialog));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.namelbl = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // namelbl
            // 
            this.namelbl.AutoSize = true;
            this.namelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.Location = new System.Drawing.Point(24, 26);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(105, 32);
            this.namelbl.TabIndex = 0;
            this.namelbl.Text = "Name: ";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(131, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 38);
            this.textBox1.TabIndex = 1;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(131, 78);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(160, 45);
            this.btnColor.TabIndex = 2;
            this.btnColor.Text = "choose Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(27, 142);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(264, 45);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // PlayerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 237);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.namelbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PlayerDialog";
            this.Text = "PlayerDialog";
            this.Load += new System.EventHandler(this.PlayerDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label namelbl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnValidate;
    }
}