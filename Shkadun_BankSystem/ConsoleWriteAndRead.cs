using System;
using System.Collections.Generic;

namespace Shkadun_BankSystem
{
    public static class ConsoleWriteAndRead
    {
        public static void GameOver(Client client)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            client.ClientStatus = "ЗЭК";
            Console.WriteLine("Доигрался. Теперь Вы ЗЭК.");
        }

        public static void MessageAboutSpendMoney(int result)
        {
            if(result == 0)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Недостаточно средств.");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;;
                Console.WriteLine("Успешно потрачено.");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        public static int GetRandomMoney()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;;
            Random random = new Random();
            int profit = random.Next(1, 300);
            Console.WriteLine($"Вы получили {profit} за работу.");
            Console.BackgroundColor = ConsoleColor.Black;
            return profit;
        }

        public static int NegativeMonths(Card card)
        {
            if (card.CardBalance > card.PrevMonthBalance)
            {
                card.NegativeMonths = 0;
                return 0;
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            if (card.NegativeMonths > -3)
            {
                Console.WriteLine($"Напоминаем что по карте #{card.ID} имеется задолженность {card.CardBalance}.");
                return 0;
            }
            else if (card.NegativeMonths > -6)
            {
                Console.WriteLine($"Напоминаем что по карте #{card.ID} задолженность больше 3х месяцев. В случае неуплаты" +
                                    $"Вы будете привлечены к ответственности.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                return 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                return 1;
            }
        }

        public static void NegativeBalance(Client client)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Отрицательный баланс. Хотите пополнить?\n 0 - да, 1 - нет");
            if(WriteYourChoise(0, 1) == 1)
            {
                Card.AddCashOnCard(client);
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void YourBalance(Client client)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;;
            Console.WriteLine($"Баланс {client.Money}.");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static int ChoiseCard(Client client)
        {
            if(client.ClientCards.Count == 0)
            {
                Console.WriteLine("Отмена.");
                return 0;
            }
            Console.WriteLine("Список карт: ");
            int count = 0;
            foreach(Card card in client.ClientCards)
            {
                Console.WriteLine($"#{card.ID}, баланс {card.CardBalance}, тип {card.TypeOfCard}");
                count++;
            }

            Console.WriteLine($"Введите от 1 до {count} чтобы выбрать или 0 для отмены.");
            int choise = WriteYourChoise(0, count);
            
                if(choise == 0)
                {
                    return 0;
                }
                else
                {
                    return choise;
                }
        }

        public static void ListOfBanks(List<Bank> banks, Client client)
        {
            if(banks.Count == 0)
            {
                Console.WriteLine("Нет ни одного банка.");
                return;
            }
            Console.WriteLine("Список банков: ");
            int i = 0;
            foreach(Bank bank in banks)
            {
                Console.WriteLine(++i + " " + bank.BankName);
            }
            int choise = WriteYourChoise(1, i);
            banks[choise - 1].OpenNewCard(client);
        }

        public static void ListOfCards(Client client)
        {
            if(client.ClientCards.Count == 0)
            {
                Console.WriteLine("Карточек нет.");
                return;
            }
            foreach(Card card in client.ClientCards)
            {
                Console.Write(  $"Номер карты {card.ID}, тип карты {card.TypeOfCard}, баланс {card.CardBalance}" +
                                    $", банк, выдавший карту {card.BankOfCard.BankName}");
                if(card.TypeOfCard == "Кредитная")
                {
                    Console.Write($", лимит карты {card.LimitOfCard}");
                }
                Console.WriteLine();
            }
        } 

        public static int WhatDoNext()
        {
            Console.WriteLine(  "\nЧто дальше?\n1. Добавить карту\n2. Удалить карту" +
                                "\n3. Пополнить карту\n4. Вывести список карт" +
                                "\n5. Потратить деньги");
            return WriteYourChoise(1, 5);
        }

        public static void SuccessAddCard()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;;
            Console.WriteLine("Карта успешно добавлена.");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static string ChoiseTypeOfCard()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;;
            Console.WriteLine("Выберите тип карты: \n1.Дебетовая\n2.Кредитная");
            int choise = WriteYourChoise(1, 2);
            if(choise == 1)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                return "Дебетовая";
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                return "Кредитная";
            }
        }

        public static Bank CreateNewBank()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;;
            Console.WriteLine("Введите название банка: ");
            string BankName;
            while (true)
            {
                BankName = Console.ReadLine();
                if (BankName.Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Не может быть нулевым.");
                }
            }

            Console.WriteLine("Введите процентную ставку: ");
            int BankBet = WriteYourChoise(1, 25);

            Console.BackgroundColor = ConsoleColor.Black;
            return new Bank(BankName, BankBet);
        }

        public static string WriteYourName()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Введите Ваше имя: ");
            string name;
            while (true)
            {
                name = Console.ReadLine();
                if(name.Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Не может быть нулевым.");
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            return name;
        }

        public static int WriteYourAge()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;;
            Console.WriteLine("Введите Ваш возраст: ");
            Console.BackgroundColor = ConsoleColor.Black;
            return WriteYourChoise(18, 65);
        }

        public static int WriteYourChoise(int min, int max, string message = null)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;;
            if(message != null)
            {
                Console.WriteLine(message);
            }
            int choise;
            while(true)
            {
                int.TryParse(Console.ReadLine(), out choise);
                if(choise < min || choise > max)
                {
                    Console.WriteLine($"Некорректный ввод. Диапазон {min}-{max}.");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    return choise;
                }
            }
        }
    }
}