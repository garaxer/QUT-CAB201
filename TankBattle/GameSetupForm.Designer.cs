namespace TankBattle
{
    partial class GameSetupForm
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
            this.nextButton = new System.Windows.Forms.Button();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameplayForm));
            this.playerLabel = new System.Windows.Forms.Label();
            this.human = new System.Windows.Forms.RadioButton();
            this.computer = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tank1 = new System.Windows.Forms.RadioButton();
            this.tank2 = new System.Windows.Forms.RadioButton();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(41, 147);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(547, 49);
            this.nextButton.TabIndex = 0;
            this.nextButton.Text = "Next player";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // playerLabel
            // 
            this.playerLabel.AutoSize = true;
            this.playerLabel.Location = new System.Drawing.Point(44, 27);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(93, 13);
            this.playerLabel.TabIndex = 6;
            this.playerLabel.Text = "Player #1\'s Name:";
            this.playerLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // human
            // 
            this.human.AutoSize = true;
            this.human.Checked = true;
            this.human.Location = new System.Drawing.Point(6, 30);
            this.human.Name = "human";
            this.human.Size = new System.Drawing.Size(59, 17);
            this.human.TabIndex = 7;
            this.human.TabStop = true;
            this.human.Text = "Human";
            this.human.UseVisualStyleBackColor = true;
            this.human.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // computer
            // 
            this.computer.AutoSize = true;
            this.computer.Location = new System.Drawing.Point(71, 30);
            this.computer.Name = "computer";
            this.computer.Size = new System.Drawing.Size(70, 17);
            this.computer.TabIndex = 8;
            this.computer.Text = "Computer";
            this.computer.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tank2);
            this.groupBox1.Controls.Add(this.tank1);
            this.groupBox1.Location = new System.Drawing.Point(205, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 62);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tank";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.computer);
            this.groupBox2.Controls.Add(this.human);
            this.groupBox2.Location = new System.Drawing.Point(41, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(158, 62);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controller";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // tank1
            // 
            this.tank1.AutoSize = true;
            this.tank1.Checked = true;
            this.tank1.Location = new System.Drawing.Point(15, 30);
            this.tank1.Name = "tank1";
            this.tank1.Size = new System.Drawing.Size(31, 17);
            this.tank1.TabIndex = 0;
            this.tank1.TabStop = true;
            this.tank1.Text = "1";
            this.tank1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tank1.UseVisualStyleBackColor = true;
            this.tank1.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // tank2
            // 
            this.tank2.AutoSize = true;
            this.tank2.Location = new System.Drawing.Point(166, 30);
            this.tank2.Name = "tank2";
            this.tank2.Size = new System.Drawing.Size(31, 17);
            this.tank2.TabIndex = 1;
            this.tank2.Text = "2";
            this.tank2.UseVisualStyleBackColor = true;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(205, 27);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(213, 20);
            this.nameBox.TabIndex = 11;
            this.nameBox.Text = "Player 1";
            // 
            // GameSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 208);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.playerLabel);
            this.Controls.Add(this.nextButton);
            this.Name = "GameSetupForm";
            this.Text = "GameSetupForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));

        }

        #endregion

        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.RadioButton human;
        private System.Windows.Forms.RadioButton computer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton tank1;
        private System.Windows.Forms.RadioButton tank2;
        private System.Windows.Forms.TextBox nameBox;
    }
}