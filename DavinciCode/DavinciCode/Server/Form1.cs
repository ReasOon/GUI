using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        TcpListener server = null;
        TcpClient clientSocket = null;

        string date;
        private Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();
        private int PORT = 5000;

        public Form1()
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e){ }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitForm();
            this.Text = "Server";
            textBoxIP.Text = "";
            textBoxIP.Text = GetLocalIP();
            textBoxPORT.Text = PORT.ToString();

        }
        private void InitForm()
        {
            textBoxIP.Text = "";
            textBoxPORT.Text = "";
            richTextBox1.Text = "";
        }
        private string GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string localIP = string.Empty;

            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = host.AddressList[i].ToString();
                    break;
                }
            }
            return localIP;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(InitSocket);
            thread1.IsBackground = true;
            thread1.Start();
        }
        private void InitSocket()
        {
            server = new TcpListener(IPAddress.Parse(textBoxIP.Text), int.Parse(textBoxPORT.Text));
            clientSocket = default(TcpClient);
            server.Start();
            DisplayText(">>Server Start");

            while (true)
            {
                try
                {
                    clientSocket = server.AcceptTcpClient();

                    NetworkStream stream = clientSocket.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string userName = Encoding.Unicode.GetString(buffer, 0, bytes);
                    userName = userName.Substring(0, userName.IndexOf("$"));
                    DisplayText(">> [" + userName + "] 접속");

                    clientList.Add(clientSocket, userName);

                    HandleClient h_client = new HandleClient();
                    h_client.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                    h_client.OnDisconnected += new HandleClient.DisconnectedHandler(h_client_OnDisconnected);
                    h_client.startClient(clientSocket, clientList);
                }
                catch (SocketException ex) { break; }
                catch (Exception ex) { break; }
            }
            clientSocket.Close();
            server.Stop();
        }
        private void h_client_OnDisconnected(TcpClient clientSocket)
        {
            if (clientList.ContainsKey(clientSocket))
            {
                clientList.Remove(clientSocket);
            }
        }
        private void OnReceived(string message, string userName)
        {
            if (message.Equals("Leave Game"))
            {
                string displayMessage = "Leave user : " + userName;

                DisplayText(displayMessage);
                SendMessageAll("Leave Game", userName, true);
            } else
            {
                string displayMessage = userName + " : " + message;
                DisplayText(displayMessage);
                SendMessageAll(displayMessage, userName, true);
            }
        }
        public void SendMessageAll(string message, string userName, bool flag)
        {
            foreach (var pair in clientList)
            {
                date = DateTime.Now.ToString("yyyy.MM.dd. HH:mm:ss"); // 현재 날짜 받기

                TcpClient client = pair.Key as TcpClient;
                NetworkStream stream = client.GetStream();
                byte[] buffer = null;

                if (flag)
                {
                    if (message.Equals("Leave Game"))
                        buffer = Encoding.Unicode.GetBytes(userName + " 님이 게임을 나갔습니다.");
                    else
                        buffer = Encoding.Unicode.GetBytes("[ " + date + " ] " + userName + " : " + message);
                }
                else
                {
                    buffer = Encoding.Unicode.GetBytes(message);
                }
                stream.Write(buffer, 0, buffer.Length); // 버퍼 쓰기
                stream.Flush();
            }
        }
        private void DisplayText(string text)
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(text + "\r\n"); }); // 데이터를 수신창에 표시, 데이터 충돌을 피하기 위해 Invoke 사용
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.ScrollToCaret(); });  // 스크롤을 제일 아래로 설정
        }
    }
}
