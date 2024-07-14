using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client2
{
    public partial class StartScreen : Form
    {
        string message = string.Empty;
        private int PORT = 5000; // 포트 정보
        private string USER_NAME = string.Empty;
        private static string CONNECT_STATUS = "DISCONNECT"; // 연결상태

        public StartScreen()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CONNECT_STATUS.Equals("DISCONNECT"))
                {
                    Connect();
                    USER_NAME = "Client1";
                    CONNECT_STATUS = "CONNECT";
                }
                else if (CONNECT_STATUS.Equals("CONNECT"))
                {
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버가 실행중이 아닙니다.", "연결 실패!");
            }

            byte[] buffer = Encoding.Unicode.GetBytes(USER_NAME + "$");

            GlobalClient.Stream.Write(buffer, 0, buffer.Length);
            GlobalClient.Stream.Flush();

            Thread t_handler = new Thread(GetMessage);
            t_handler.IsBackground = true;
            t_handler.Start();

            GameScreen gameScreen = new GameScreen();
            gameScreen.Show();
            this.Hide();
        }


        private void InitForm()
        {
            textBoxIP.Text = "";
            textBoxPORT.Text = "";
        }

        private void Connect()
        {
            GlobalClient.Client.Connect(textBoxIP.Text.ToString(), PORT); // 접속 IP 및 포트
            GlobalClient.Stream = GlobalClient.Client.GetStream();
        }

        private void GetMessage() // 메세지 받기
        {
            while (true)
            {
                GlobalClient.Stream = GlobalClient.Client.GetStream();
                int BUFFERSIZE = GlobalClient.Client.ReceiveBufferSize;
                byte[] buffer = new byte[BUFFERSIZE];
                int bytes = GlobalClient.Stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.Unicode.GetString(buffer, 0, bytes);
            }
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {
            InitForm();
            this.Text = "Client";
            textBoxIP.Text = "TCP Server의 IP 입력";
            textBoxPORT.Text = PORT.ToString();
        }
    }
}
