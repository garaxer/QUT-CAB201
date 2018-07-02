namespace TankBattle
{
    partial class Winner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameplayForm));
            this.playerWon = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // playerWon
            // 
            this.playerWon.AutoSize = true;
            this.playerWon.Location = new System.Drawing.Point(109, 9);
            this.playerWon.Name = "playerWon";
            this.playerWon.Size = new System.Drawing.Size(59, 13);
            this.playerWon.TabIndex = 1;
            this.playerWon.Text = "Player won";
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(70, 204);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(132, 45);
            this.close.TabIndex = 0;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(13, 40);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(235, 134);
            this.listBox.TabIndex = 2;
            // 
            // Winner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.close);
            this.Controls.Add(this.playerWon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Winner";
            this.Text = "Winner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playerWon;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.ListBox listBox;
    }
}