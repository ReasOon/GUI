using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client2
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

        private int currentPlayerTurn = 2;

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
            List<PictureBox> allCards = ReceiveCardsFromServer();

            // 받은 카드 리스트를 16개로 제한
            List<PictureBox> selectedCards = allCards.Take(16).ToList();

            // 첫 8개 카드를 player1의 패널에 배치
            List<PictureBox> player1Cards = selectedCards.Take(8).ToList();
            List<PictureBox> player2Cards = selectedCards.Skip(8).Take(8).ToList();

            // 카드를 패널에 배열
            MoveCardToPlayerArea(player1Cards, playerArea1);
            MoveCardToPlayerArea(player2Cards, playerArea2);

            MessageBox.Show("카드가 패널에 추가되었습니다.");
        }

        private List<PictureBox> ReceiveCardsFromServer()
        {
            List<PictureBox> receivedCards = new List<PictureBox>();

            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = GlobalClient.Stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string cardData = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                    string[] cardIds = cardData.Split(',');

                    foreach (string cardId in cardIds)
                    {
                        PictureBox card = CreateCardPictureBox(cardId);
                        receivedCards.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("카드를 서버에서 받을 때 오류가 발생했습니다: " + ex.Message);
            }
            return receivedCards;
        }

        private PictureBox CreateCardPictureBox(string cardId)
        {
            PictureBox card = new PictureBox();
            card.Tag = cardId;

            // 카드 태그에 따라 이미지를 설정합니다.
            ShowCardFront(card);

            card.SizeMode = PictureBoxSizeMode.StretchImage;
            card.Width = 100;  // 적절한 너비로 설정
            card.Height = 150; // 적절한 높이로 설정

            return card;
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
        private void DisConnect()
        {
            GlobalClient.Client = null;
            byte[] buffer = Encoding.Unicode.GetBytes("Leave Game" + "$");
            GlobalClient.Stream.Write(buffer, 0, buffer.Length);
            GlobalClient.Stream.Flush();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
