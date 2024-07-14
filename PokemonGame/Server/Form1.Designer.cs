namespace Server
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudGameTIme = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudMaxUserCnt = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameTIme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxUserCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudGameTIme);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudMaxUserCnt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtIPAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(43, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(439, 115);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "설정";
            // 
            // nudGameTIme
            // 
            this.nudGameTIme.Location = new System.Drawing.Point(334, 79);
            this.nudGameTIme.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudGameTIme.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudGameTIme.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudGameTIme.Name = "nudGameTIme";
            this.nudGameTIme.Size = new System.Drawing.Size(65, 25);
            this.nudGameTIme.TabIndex = 9;
            this.nudGameTIme.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudGameTIme.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "게임시간(초)";
            // 
            // nudMaxUserCnt
            // 
            this.nudMaxUserCnt.Location = new System.Drawing.Point(91, 79);
            this.nudMaxUserCnt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudMaxUserCnt.Name = "nudMaxUserCnt";
            this.nudMaxUserCnt.Size = new System.Drawing.Size(131, 25);
            this.nudMaxUserCnt.TabIndex = 7;
            this.nudMaxUserCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudMaxUserCnt.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "최대인원";
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(334, 35);
            this.nudPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudPort.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(65, 25);
            this.nudPort.TabIndex = 5;
            this.nudPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPort.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "포트";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(91, 34);
            this.txtIPAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(131, 25);
            this.txtIPAddress.TabIndex = 3;
            this.txtIPAddress.Text = "127.0.0.1";
            this.txtIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "아이피";
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(22, 130);
            this.btnServerStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(478, 56);
            this.btnServerStart.TabIndex = 6;
            this.btnServerStart.Text = "서버시작";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(22, 193);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(478, 438);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 656);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnServerStart);
            this.Name = "Form1";
            this.Text = "포켓몬게임서버";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameTIme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxUserCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudGameTIme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMaxUserCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

