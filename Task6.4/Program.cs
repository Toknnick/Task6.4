using System;
using System.Collections.Generic;

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
                        ShowCardsInfo();
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
            int choosenNumber = ChooseCardNumber();
            Suits choosenSuit = ChooseSuit();

            if (FindExistedCard(choosenNumber, choosenSuit))
            {
                GetMessage("Такая карта уже взята из колоды.");
            }
            else
            {
                _cards.Add(new Card(choosenSuit, choosenNumber));
                GetMessage("Карта успешно взята!");
            }
        }

        private bool FindExistedCard(int nummber, Suits suit)
        {
            foreach (Card card in _cards)
            {
                if (card.Number == nummber && card.Suit == suit)
                    return true;
            }

            return false;
        }

        private void TakeRandomCard()
        {
            _cards.Add(new Card());
            GetMessage("Карта успешно взята!");
        }

        private void ShowCardsInfo()
        {
            if (_cards.Count == 0)
            {
                Console.WriteLine("Карт нет!");
            }
            else
            {
                Console.WriteLine("Информация о картах:");

                for (int i = 0; i < _cards.Count; i++)
                {
                    _cards[i].ShowInfo();
                }
            }

            GetMessage();
        }

        private Suits ChooseSuit()
        {
            bool isRepeating = true;

            while (isRepeating)
            {
                ShowSuits();
                Console.Write("Выбирите масть: ");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= 0 && result <= Enum.GetValues(typeof(Suits)).Length)
                    {
                        return (Suits)result;
                    }
                    else
                    {
                        WriteError();
                    }
                }
            }

            return (Suits)(-1);
        }

        private void ShowSuits()
        {
            int suitCount = Enum.GetValues(typeof(Suits)).Length;

            for (int i = 0; i < suitCount; i++)
            {
                Console.WriteLine($"{i} - {(Suits)i}");
            }
        }

        private int ChooseCardNumber()
        {
            int maxNumber = 10;
            int minMubmber = 2;
            bool isRepeating = true;

            while (isRepeating)
            {
                Console.WriteLine($"Введите число от {minMubmber} до {maxNumber}");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= minMubmber && result < maxNumber)
                    {
                        return result;
                    }
                    else
                    {
                        WriteError();
                    }
                }
            }

            return -1;
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
            Console.ForegroundColor = ConsoleColor.Red;
            GetMessage("Введите корректные данные.");
            Console.ResetColor();
        }
    }

    public enum Suits
    {
        Diamonds,
        Hearts,
        Clubs,
        Spades
    }

    class Card
    {
        public int Number { get; private set; }
        public Suits Suit { get; private set; }

        public Card(Suits suit, int number)
        {
            Suit = suit;
            Number = number;
        }

        public Card()
        {
            Suit = GetRandomSuit();
            Number = GetRandomNumber();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Карта - {Number}. Масть - {Suit}.");
        }

        private Suits GetRandomSuit()
        {
            return (Suits)new Random().Next(Enum.GetValues(typeof(Suits)).Length);
        }

        private int GetRandomNumber()
        {
            int maxNumber = 10;
            int minMubmber = 2;

            return new Random().Next(minMubmber, maxNumber);
        }
    }
}
