namespace TankBattle
{
    partial class GameplayForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameplayForm));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.powerSelector = new System.Windows.Forms.TrackBar();
            this.angleSelector = new System.Windows.Forms.NumericUpDown();
            this.weaponSelector = new System.Windows.Forms.ComboBox();
            this.fireButton = new System.Windows.Forms.Button();
            this.powerLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Weapon = new System.Windows.Forms.Label();
            this.windLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.playerLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(0, 32);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(800, 600);
            this.displayPanel.TabIndex = 0;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.controlPanel.Controls.Add(this.powerSelector);
            this.controlPanel.Controls.Add(this.angleSelector);
            this.controlPanel.Controls.Add(this.weaponSelector);
            this.controlPanel.Controls.Add(this.fireButton);
            this.controlPanel.Controls.Add(this.powerLabel);
            this.controlPanel.Controls.Add(this.label3);
            this.controlPanel.Controls.Add(this.label2);
            this.controlPanel.Controls.Add(this.Weapon);
            this.controlPanel.Controls.Add(this.windLabel);
            this.controlPanel.Controls.Add(this.label1);
            this.controlPanel.Controls.Add(this.playerLabel);
            this.controlPanel.Enabled = false;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(800, 32);
            this.controlPanel.TabIndex = 1;
            // 
            // powerSelector
            // 
            this.powerSelector.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            this.powerSelector.LargeChange = 10;
            this.powerSelector.Location = new System.Drawing.Point(544, 8);
            this.powerSelector.Maximum = 100;
            this.powerSelector.Minimum = 5;
            this.powerSelector.Name = "powerSelector";
            this.powerSelector.Size = new System.Drawing.Size(104, 45);
            this.powerSelector.TabIndex = 10;
            this.powerSelector.Value = 5;
            this.powerSelector.Scroll += new System.EventHandler(this.powerSelector_Scroll);
            // 
            // angleSelector
            // 
            this.angleSelector.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.angleSelector.Location = new System.Drawing.Point(427, 6);
            this.angleSelector.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.angleSelector.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.angleSelector.Name = "angleSelector";
            this.angleSelector.Size = new System.Drawing.Size(57, 20);
            this.angleSelector.TabIndex = 9;
            this.angleSelector.ValueChanged += new System.EventHandler(this.angleSelector_ValueChanged);
            // 
            // weaponSelector
            // 
            this.weaponSelector.FormattingEnabled = true;
            this.weaponSelector.Location = new System.Drawing.Point(210, 5);
            this.weaponSelector.Name = "weaponSelector";
            this.weaponSelector.Size = new System.Drawing.Size(121, 21);
            this.weaponSelector.TabIndex = 8;
            // 
            // fireButton
            // 
            this.fireButton.Location = new System.Drawing.Point(700, 6);
            this.fireButton.Name = "fireButton";
            this.fireButton.Size = new System.Drawing.Size(75, 23);
            this.fireButton.TabIndex = 7;
            this.fireButton.Text = "Fire!";
            this.fireButton.UseVisualStyleBackColor = true;
            this.fireButton.Click += new System.EventHandler(this.fireButton_Click);
            // 
            // powerLabel
            // 
            this.powerLabel.AutoSize = true;
            this.powerLabel.Location = new System.Drawing.Point(654, 11);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(19, 13);
            this.powerLabel.TabIndex = 6;
            this.powerLabel.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(499, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Power:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Angle:";
            // 
            // Weapon
            // 
            this.Weapon.AutoSize = true;
            this.Weapon.Location = new System.Drawing.Point(144, 8);
            this.Weapon.Name = "Weapon";
            this.Weapon.Size = new System.Drawing.Size(51, 13);
            this.Weapon.TabIndex = 3;
            this.Weapon.Text = "Weapon:";
            // 
            // windLabel
            // 
            this.windLabel.AutoSize = true;
            this.windLabel.Location = new System.Drawing.Point(76, 16);
            this.windLabel.Name = "windLabel";
            this.windLabel.Size = new System.Drawing.Size(27, 13);
            this.windLabel.TabIndex = 2;
            this.windLabel.Text = "0 W";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "wind";
            // 
            // playerLabel
            // 
            this.playerLabel.AutoSize = true;
            this.playerLabel.Location = new System.Drawing.Point(13, 13);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(45, 13);
            this.playerLabel.TabIndex = 0;
            this.playerLabel.Text = "Player 1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GameplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 629);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "GameplayForm";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.GameplayForm_Resize);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleSelector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Weapon;
        private System.Windows.Forms.Label windLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar powerSelector;
        private System.Windows.Forms.NumericUpDown angleSelector;
        private System.Windows.Forms.ComboBox weaponSelector;
        private System.Windows.Forms.Button fireButton;
        private System.Windows.Forms.Label powerLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

