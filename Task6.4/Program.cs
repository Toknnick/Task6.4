using System;
using System.Collections.Generic;
using System.Linq;

namespace Task6._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.Work();
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("1.Взять случайную карту.\n2.Взять выбранную карту.\n3.Просмотреть информацию о взятых картах.\n4.Выход.\nВыберите вариант:");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        TakeRandomCard();
                        break;
                    case "2":
                        TakeCard();
                        break;
                    case "3":
                        ShowDetails();
                        break;
                    case "4":
                        isWork = false;
                        break;
                    default:
                        WriteError();
                        break;
                }
            }
        }

        private void TakeCard()
        {
            Console.Clear();
            char result = ' ';
            Console.WriteLine("Введите номер карты:");

            if (CheckState(ref result))
            {
                Console.WriteLine("Выберите масть: \nПики ♠ \nКрести ♣ \nБуби ♦ \nЧерви ♥");
                string suit = Console.ReadLine();

                for (int i = 0; i < _cards.Count; i++)
                {
                    if (_cards.ElementAt(i).NumberOfCard == result && _cards.ElementAt(i).Suit == suit)
                    {
                        GetMessage("Такая карта уже взята из колоды.");
                    }
                    else
                    {
                        _cards.Add(new Card(suit, result));
                        GetMessage("карта успешно взята!");
                        return;
                    }
                }
            }
            else
            {
                WriteError();
            }
        }

        private void TakeRandomCard()
        {
            Random random = new Random();
            string[] suits = new string[4] { "Черви ♥", "Буби ♦", "Крести ♣", "Пики ♠" };
            int minValue = 2;
            int maxValueForChar = 13;
            int maxValue = 10;
            int randomNumber = random.Next(minValue, maxValueForChar);
            string randomSuit = suits[random.Next(0, suits.Length)];
            Console.WriteLine(randomNumber);
            Console.WriteLine(TryGetChar(randomNumber, maxValue));
            Console.ReadKey();
            Console.Clear();
            char randomChar = TryGetChar(randomNumber, maxValue);
            _cards.Add(new Card(randomSuit, randomChar));
            GetMessage("карта успешно взята!");
        }

        private char TryGetChar(int randomNumber, int maxValue)
        {
            char randomChar;
            char[] chars = new char[4] { 'J', 'Q', 'K', 'A' };

            //maxValue = 10
            if (randomNumber >= maxValue)
            {
                randomNumber -= maxValue;
                randomChar = chars[randomNumber];
            }
            else
            {
                randomChar = (char)randomNumber;
            }

            return randomChar;
        }

        private void ShowDetails()
        {
            Console.Clear();
            Console.WriteLine("Информация о картах:");

            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].ShowInfo();
            }

            GetMessage();
        }

        private bool CheckState(ref char result)
        {
            string userInput = Console.ReadLine();
            bool state = char.TryParse(userInput, out result);
            return state;
        }

        private void GetMessage(string text = "")
        {
            Console.WriteLine(text);
            Console.WriteLine("Нажмите любую клавишу для продолжения:");
            Console.ReadKey();
            Console.Clear();
        }

        private void WriteError()
        {
            GetMessage("Введите корректные данные.");
        }
    }

    class Card
    {
        private string _suit;
        private char _number;
        public char NumberOfCard { get; private set; }
        public string Suit { get; private set; }

        public Card(string suit, char number)
        {
            _suit = suit;
            _number = number;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Карта - {_number} .Масть - {_suit}.");
        }
    }
}
