namespace Client2
{
    partial class GameScreen
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
            this.textBoxIndex = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.textBoxplayer2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxIndex
            // 
            this.textBoxIndex.Location = new System.Drawing.Point(26, 371);
            this.textBoxIndex.Name = "textBoxIndex";
            this.textBoxIndex.Size = new System.Drawing.Size(113, 25);
            this.textBoxIndex.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 512);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 44);
            this.button2.TabIndex = 11;
            this.button2.Text = "탈주";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "확인하기";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(26, 404);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(113, 25);
            this.textBoxValue.TabIndex = 9;
            // 
            // textBoxplayer2
            // 
            this.textBoxplayer2.Location = new System.Drawing.Point(1188, 32);
            this.textBoxplayer2.Name = "textBoxplayer2";
            this.textBoxplayer2.Size = new System.Drawing.Size(148, 25);
            this.textBoxplayer2.TabIndex = 13;
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 570);
            this.Controls.Add(this.textBoxplayer2);
            this.Controls.Add(this.textBoxIndex);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxValue);
            this.Name = "GameScreen";
            this.Text = "GameScreen";
            this.Load += new System.EventHandler(this.GameScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIndex;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.TextBox textBoxplayer2;
    }
}