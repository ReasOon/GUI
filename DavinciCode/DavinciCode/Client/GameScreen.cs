using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class GameScreen : Form
    {
        private static string CONNECT_STATUS = "CONNECT"; // 연결상태

        private int player1ClickCount = 0;
        private int player2ClickCount = 0;
        private const int MaxCardClicks = 8;

        private bool isChuriMode = false;
        private PictureBox selectedCard = null;
        private Size originalCardSize;
        private bool isMyCardShownToOpponent = false;
        private PictureBox selectedPlayerCard = null;
        private const int CardZoomFactor = 10;

        private Dictionary<PictureBox, int> cardClickCount = new Dictionary<PictureBox, int>();
        private List<PictureBox> cardList = new List<PictureBox>();

        private List<PictureBox> player1Cards = new List<PictureBox>();
        private List<PictureBox> player2Cards = new List<PictureBox>();
        private Panel playerArea1;
        private Panel playerArea2;

        private int currentPlayerTurn = 1;

        public GameScreen()
        {
            InitializeComponent();
            InitializePlayerArea();
            InitializeGame();
        }

        private void InitializeGame()
        {
            this.ClientSize = new Size(800, 600);

            List<string> cardTags = new List<string>();

            for (int i = 0; i <= 10; i++)
            {
                cardTags.Add($"black{i}");
                cardTags.Add($"white{i}");
            }

            Shuffle(cardTags);

            int x = 10;
            int y = 10;
            int cardWidth = 60;
            int cardHeight = 90;
            int spacing = 10;
            int cardsPerRow = 11;
            int cardCount = 0;

            originalCardSize = new Size(cardWidth, cardHeight);

            foreach (string cardTag in cardTags)
            {
                PictureBox card = new PictureBox();
                card.Size = originalCardSize;
                card.Size = new Size(cardWidth, cardHeight);
                card.SizeMode = PictureBoxSizeMode.Zoom;
                card.Location = new Point(x, y);
                card.Tag = cardTag;
                card.MouseEnter += Card_MouseEnter;
                card.MouseLeave += Card_MouseLeave;

                card.MouseClick += Card_Click;

                switch (cardTag)
                {
                    case string tag when tag.StartsWith("black"):
                        card.Image = Properties.Resources.blackback;
                        break;
                    case string tag when tag.StartsWith("white"):
                        card.Image = Properties.Resources.whiteback;
                        break;
                }

                cardList.Add(card);
                this.Controls.Add(card);

                cardCount++;

                if (cardCount % cardsPerRow == 0)
                {
                    x = 10;
                    y += cardHeight + spacing;
                }
                else
                {
                    x += cardWidth + spacing;
                }
            }

            InitializePlayerAreaWithSortedRandomCards();
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void InitializePlayerArea()
        {
            playerArea1 = new Panel();
            playerArea1.Size = new Size(600, 150);
            playerArea1.Location = new Point((this.ClientSize.Width - playerArea1.Width) / 2, 400);
            playerArea1.BorderStyle = BorderStyle.FixedSingle;
            playerArea1.BackColor = Color.LightBlue;
            this.Controls.Add(playerArea1);

            playerArea2 = new Panel();
            playerArea2.Size = new Size(600, 150);
            playerArea2.Location = new Point((this.ClientSize.Width - playerArea2.Width) / 2, 200);
            playerArea2.BorderStyle = BorderStyle.FixedSingle;
            playerArea2.BackColor = Color.LightYellow;
            this.Controls.Add(playerArea2);

            playerArea2.Click += OpponentCard_Click;
            playerArea1.Click += OpponentCard_Click;
        }


        private void InitializePlayerAreaWithSortedRandomCards()
        {
            Random random = new Random();
            List<PictureBox> allCards = cardList.ToList();

            List<PictureBox> selectedCards = allCards.OrderBy(c => random.Next()).Take(16).ToList();

            // white0~9 및 black0~9 카드를 작은 숫자가 왼쪽으로 오게 하고 동일한 숫자일 경우 black 카드가 왼쪽에 오게 정렬
            var orderedNumberCards = selectedCards
                .Where(c => !c.Tag.ToString().EndsWith("10"))
                .OrderBy(c => int.Parse(c.Tag.ToString().Substring(5)))
                .ThenBy(c => c.Tag.ToString().StartsWith("white"))
                .ToList();

            // black10 및 white10 카드를 무작위로 배열
            var specialCards = selectedCards
                .Where(c => c.Tag.ToString().EndsWith("10"))
                .OrderBy(c => random.Next())
                .ToList();

            // 모든 카드를 합침
            List<PictureBox> combinedCards = orderedNumberCards.Concat(specialCards).ToList();


            // 플레이어 1에게 8장씩 카드 분배
            player1Cards = combinedCards.Take(8).ToList();

            foreach (var card in player1Cards)
            {
                ShowCardFront(card);
            }


            // 플레이어 2에게 나머지 카드 분배
            player2Cards = combinedCards.Skip(8).ToList();

            // 카드를 패널에 배열
            MoveCardToPlayerArea(player1Cards, playerArea1);
            MoveCardToPlayerArea(player2Cards, playerArea2);

            string player1CardsString = string.Join(",", player2Cards.Select(card => card.Name));
            textBoxplayer1.Text = player1CardsString;
            string player2CardsString = string.Join(",", player2Cards.Select(card => card.Name));
            textBoxplayer2.Text = player2CardsString;

            // 서버로 전송
            SendMessageToServer(player1CardsString);
            SendMessageToServer(player2CardsString);

            MoveCardToPlayerArea(player1Cards, playerArea1);
            MoveCardToPlayerArea(player2Cards, playerArea2);

            MessageBox.Show("카드가 패널에 추가되었습니다.");

        }

        private void SendMessageToServer(string message)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(message);
            GlobalClient.Stream.Write(buffer, 0, buffer.Length);
            GlobalClient.Stream.Flush();
        }

        private void MoveCardToPlayerArea(List<PictureBox> cards, Panel playerArea)
        {
            playerArea.Controls.Clear();
            foreach (var card in cards)
            {
                playerArea.Controls.Add(card);
            }

            int cardWidth = cards[0].Width;
            int spacing = 10;
            int totalWidth = cards.Count * cardWidth + (cards.Count - 1) * spacing;
            int x = (playerArea.Width - totalWidth) / 2;
            foreach (var card in cards)
            {
                card.Location = new Point(x, (playerArea.Height - card.Height) / 2);
                x += cardWidth + spacing;
            }
        }

        private async void OpponentCard_Click(object sender, EventArgs e)
        {
            // 상대 플레이어의 카드를 보이지 않게 설정
            if (currentPlayerTurn == 1)
            {
                foreach (var card in player2Cards)
                {
                    switch (card.Tag.ToString())
                    {
                        case string tag when tag.StartsWith("black"):
                            card.Image = Properties.Resources.blackback;
                            break;
                        case string tag when tag.StartsWith("white"):
                            card.Image = Properties.Resources.whiteback;
                            break;
                    }
                }
            }
            else if (currentPlayerTurn == 2)
            {
                foreach (var card in player1Cards)
                {
                    switch (card.Tag.ToString())
                    {
                        case string tag when tag.StartsWith("black"):
                            card.Image = Properties.Resources.blackback;
                            break;
                        case string tag when tag.StartsWith("white"):
                            card.Image = Properties.Resources.whiteback;
                            break;
                    }
                }

                if (isChuriMode)
                {
                    MessageBox.Show("추리 모드가 아닙니다. '추리하기' 버튼을 먼저 클릭하세요.");
                }
            }
        }
        private void Card_MouseEnter(object sender, EventArgs e)
        {
            PictureBox card = sender as PictureBox;
            card.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            PictureBox card = sender as PictureBox;
            card.BorderStyle = BorderStyle.None;
        }

        private void ChuriButton_Click(object sender, EventArgs e)
        {
            isChuriMode = !isChuriMode;
            if (isChuriMode)
            {
                MessageBox.Show("추리 모드가 활성화되었습니다.");
            }
        }

        private async void Card_Click(object sender, EventArgs e)
        {
            PictureBox clickedCard = sender as PictureBox;

            if (clickedCard == null)
                return;

            bool isCurrentPlayerCard = (currentPlayerTurn == 1 && player1Cards.Contains(clickedCard)) ||
                                       (currentPlayerTurn == 2 && player2Cards.Contains(clickedCard));

            if (isCurrentPlayerCard)
            {
                if (currentPlayerTurn == 1)
                    player1ClickCount++;
                else if (currentPlayerTurn == 2)
                    player2ClickCount++;
            }

            if (isCurrentPlayerCard)
            {
                if (!cardClickCount.ContainsKey(clickedCard))
                {
                    cardClickCount.Add(clickedCard, 0);
                }

                if (cardClickCount[clickedCard] % 2 == 0)
                {
                    ShowCardFront(clickedCard);
                    UpdateOpponentView(clickedCard);
                    ZoomCard(clickedCard, CardZoomFactor);
                }
                else
                {
                    ShowCardBack(clickedCard);
                    ZoomCard(clickedCard, -CardZoomFactor);
                }

                cardClickCount[clickedCard]++;

                if (player1ClickCount == MaxCardClicks || player2ClickCount == MaxCardClicks)
                {
                    GameOver();
                }
            }
        }

        private void ShowCardFront(PictureBox card)
        {
            string cardTag = card.Tag.ToString();

            switch (cardTag)
            {
                case "black0": card.Image = Properties.Resources.black0; break;
                case "black1": card.Image = Properties.Resources.black1; break;
                case "black2": card.Image = Properties.Resources.black2; break;
                case "black3": card.Image = Properties.Resources.black3; break;
                case "black4": card.Image = Properties.Resources.black4; break;
                case "black5": card.Image = Properties.Resources.black5; break;
                case "black6": card.Image = Properties.Resources.black6; break;
                case "black7": card.Image = Properties.Resources.black7; break;
                case "black8": card.Image = Properties.Resources.black8; break;
                case "black9": card.Image = Properties.Resources.black9; break;
                case "black10": card.Image = Properties.Resources.black10; break;
                case "white0": card.Image = Properties.Resources.white0; break;
                case "white1": card.Image = Properties.Resources.white1; break;
                case "white2": card.Image = Properties.Resources.white2; break;
                case "white3": card.Image = Properties.Resources.white3; break;
                case "white4": card.Image = Properties.Resources.white4; break;
                case "white5": card.Image = Properties.Resources.white5; break;
                case "white6": card.Image = Properties.Resources.white6; break;
                case "white7": card.Image = Properties.Resources.white7; break;
                case "white8": card.Image = Properties.Resources.white8; break;
                case "white9": card.Image = Properties.Resources.white9; break;
                case "white10": card.Image = Properties.Resources.white10; break;
            }
        }

        private void ShowCardBack(PictureBox card)
        {
            string cardTag = card.Tag.ToString();

            switch (cardTag)
            {
                case string tag when tag.StartsWith("black"):
                    card.Image = Properties.Resources.blackback;
                    break;
                case string tag when tag.StartsWith("white"):
                    card.Image = Properties.Resources.whiteback;
                    break;
            }
        }

        private void GameOver()
        {
            MessageBox.Show("게임 종료 - 모든 카드를 공개됐습니다.");
        }

        private void ZoomCard(PictureBox card, int factor)
        {
            card.Size = new Size(card.Width + factor, card.Height + factor);
        }

        private void UpdateOpponentView(PictureBox clickedCard)
        {
            if (currentPlayerTurn == 1)
            {
                playerArea2.BackgroundImage = clickedCard.Image;
            }
            else if (currentPlayerTurn == 2)
            {
                playerArea1.BackgroundImage = clickedCard.Image;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            // 사용자 입력값 검증
            if (!int.TryParse(textBoxIndex.Text, out int index))
            {
                MessageBox.Show("순서는 정수로 입력해야 합니다.");
                return;
            }

            // 입력받은 값 문자열로 처리
            string value = textBoxValue.Text;

            // 인덱스 조정 (사용자는 1부터 카운팅 시작할 수 있으므로)
            index -= 1;

            // 인덱스 유효성 검사
            if (index < 0 || index >= player2Cards.Count)
            {
                MessageBox.Show("입력한 순서가 범위를 벗어났습니다.");
                return;
            }

            // 카드 배열의 해당 순서의 카드 태그와 문자열 비교
            if (String.Equals(player2Cards[index].Tag.ToString(), value, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("정답입니다! 순서와 값이 일치합니다.");
            }
            else
            {
                MessageBox.Show("틀렸습니다. 순서와 값이 일치하지 않습니다.");
            }


            textBoxIndex.Focus();
            textBoxValue.Focus();
           
            byte[] buffer = Encoding.Unicode.GetBytes(textBoxIndex.Text + "번째가 " + textBoxValue.Text + "이라고 추측" + "$");
            GlobalClient.Stream.Write(buffer, 0, buffer.Length);
            GlobalClient.Stream.Flush();

            textBoxValue.Text = "";
            textBoxIndex.Text = "";
        }

        private void DisConnect()
        {
            GlobalClient.Client = null;
            byte[] buffer = Encoding.Unicode.GetBytes("Leave Game" + "$");
            GlobalClient.Stream.Write(buffer, 0, buffer.Length);
            GlobalClient.Stream.Flush();
        }

        private void GameScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisConnect();

            Application.ExitThread();
            Environment.Exit(0);
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (CONNECT_STATUS.Equals("DISCONNECT"))
            {
                MessageBox.Show("연결된 상태가 아닙니다.");
                return;
            }
            else if (CONNECT_STATUS.Equals("CONNECT"))
            {
                DisConnect();
                return;
            }
            else
            { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxIndex_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // 엔터키 눌렀을 때
                button1_Click(this, e);
        }

        private void textBoxValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // 엔터키 눌렀을 때
                button1_Click(this, e);
        }

        private void textBoxIndex_TextChanged(object sender, EventArgs e)
        {

        }
    }
}





