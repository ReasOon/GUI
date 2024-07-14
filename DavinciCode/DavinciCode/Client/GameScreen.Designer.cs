namespace Client
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxIndex = new System.Windows.Forms.TextBox();
            this.textBoxplayer2 = new System.Windows.Forms.TextBox();
            this.textBoxplayer1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(26, 366);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(113, 25);
            this.textBoxValue.TabIndex = 5;
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBoxValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "확인하기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 44);
            this.button2.TabIndex = 7;
            this.button2.Text = "탈주";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxIndex
            // 
            this.textBoxIndex.Location = new System.Drawing.Point(26, 333);
            this.textBoxIndex.Name = "textBoxIndex";
            this.textBoxIndex.Size = new System.Drawing.Size(113, 25);
            this.textBoxIndex.TabIndex = 8;
            this.textBoxIndex.TextChanged += new System.EventHandler(this.textBoxIndex_TextChanged);
            this.textBoxIndex.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxIndex_KeyUp);
            // 
            // textBoxplayer2
            // 
            this.textBoxplayer2.Location = new System.Drawing.Point(1179, 32);
            this.textBoxplayer2.Name = "textBoxplayer2";
            this.textBoxplayer2.Size = new System.Drawing.Size(148, 25);
            this.textBoxplayer2.TabIndex = 14;
            // 
            // textBoxplayer1
            // 
            this.textBoxplayer1.Location = new System.Drawing.Point(1179, 1);
            this.textBoxplayer1.Name = "textBoxplayer1";
            this.textBoxplayer1.Size = new System.Drawing.Size(126, 25);
            this.textBoxplayer1.TabIndex = 15;
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 633);
            this.Controls.Add(this.textBoxplayer1);
            this.Controls.Add(this.textBoxplayer2);
            this.Controls.Add(this.textBoxIndex);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxValue);
            this.Name = "GameScreen";
            this.Text = "GameScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameScreen_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxIndex;
        private System.Windows.Forms.TextBox textBoxplayer2;
        private System.Windows.Forms.TextBox textBoxplayer1;
    }
}