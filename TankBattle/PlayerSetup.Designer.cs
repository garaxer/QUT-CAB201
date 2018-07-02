namespace TankBattle
{
    partial class PlayerSetup
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.playerCountSelector = new System.Windows.Forms.NumericUpDown();
            this.roundSelector = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.playerCountSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "How many players? (2-8)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "How many gameplay rounds?";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(369, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "Setup Players";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // playerCountSelector
            // 
            this.playerCountSelector.Location = new System.Drawing.Point(258, 21);
            this.playerCountSelector.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.playerCountSelector.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.playerCountSelector.Name = "playerCountSelector";
            this.playerCountSelector.Size = new System.Drawing.Size(120, 20);
            this.playerCountSelector.TabIndex = 0;
            this.playerCountSelector.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.playerCountSelector.ValueChanged += new System.EventHandler(this.playerCountSelector_ValueChanged);
            // 
            // roundSelector
            // 
            this.roundSelector.Location = new System.Drawing.Point(258, 50);
            this.roundSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.roundSelector.Name = "roundSelector";
            this.roundSelector.Size = new System.Drawing.Size(120, 20);
            this.roundSelector.TabIndex = 1;
            this.roundSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PlayerSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 136);
            this.Controls.Add(this.roundSelector);
            this.Controls.Add(this.playerCountSelector);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayerSetup";
            this.Text = "PlayerSetup";
            ((System.ComponentModel.ISupportInitialize)(this.playerCountSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown playerCountSelector;
        private System.Windows.Forms.NumericUpDown roundSelector;
    }
}