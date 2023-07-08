using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppCards
{
    public partial class Form1 : Form
    {
        private List<string> deck;

        public Form1()
        {
            InitializeComponent();
            InitializeDeck();// Метод , который создает колоду карт
                             // и добавляет ее в список
        }
        private void InitializeDeck()// Создаем колоду
        {
            deck = new List<string>();//список

            string[] suits = { "Червовая масть","Бубновая масть", "Крестовая масть", "Пиковая масть" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    deck.Add(rank + "  " + suit);
                }
            }
        }

        private void ShuffleDeck()// Метод перемешивания колоды
        {
            Random random = new Random();

            int n = deck.Count; // получаем число карт в  колоде
            while (n > 1)
            {
                //алгоритм сортировки
                n--;
                int k = random.Next(n + 1);
                string value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        private List<string> DealCards(int numCards)//метод раздачи карт из колоды
        {
            List<string> hand = new List<string>();

            for (int i = 0; i < numCards; i++)
            {
                if (deck.Count > 0)
                {
                    hand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
                else
                {
                    MessageBox.Show("Колода пуста!");
                    break;
                }
            }


            return hand;
        }
        private void SortHand(List<string> hand)//метод сортировки выданных на руки карт
        {
            hand.Sort((card1, card2) =>
            {
                // Сравнение по мастям
                string suit1 = GetCardSuit(card1);
                string suit2 = GetCardSuit(card2);
                int suitComparison = suit1.CompareTo(suit2);
                if (suitComparison != 0)
                    return suitComparison;

                // Сравнение по возрастанию
                int rankComparison = GetCardRankValue(card1).CompareTo(GetCardRankValue(card2));
                return rankComparison;
            });
        }

        private string GetCardSuit(string card)// метод для извлечения масти карты из строки с названием
        {
            return card.Split()[2];
        }

        private int GetCardRankValue(string card)//метод для преобразования достоинства карты
                                                 //в числовое значение для дальнейшего сравнения
        {
            string rank = card.Split()[0];
            switch (rank)
            {
                case "2": return 2;
                case "3": return 3;
                case "4": return 4;
                case "5": return 5;
                case "6": return 6;
                case "7": return 7;
                case "8": return 8;
                case "9": return 9;
                case "10": return 10;
                case "Валет": return 11;
                case "Дама": return 12;
                case "Король": return 13;
                case "Туз": return 14;
                default:
                return 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            ShuffleDeck();
            MessageBox.Show("Колода перемешана!");
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            List<string> hand = DealCards(5);
            SortHand(hand);
            string handString = string.Join("\n", hand);
            MessageBox.Show("Случайные карты :\n" + handString);
        }
    }
}
