using System;

namespace Shkadun_BankSystem
{
    public class Card
    {
        public static int countCards = 0;
        public string TypeOfCard { get; private set; }
        public double CardBalance { get; set; }
        public string StatusOfCard { get; set; }
        public Bank BankOfCard { get; private set; }
        public int LimitOfCard { get; private set; }
        public int ID { get; private set; }
        public int NegativeMonths { get; set; }
        public double PrevMonthBalance { get; set; }

        public void NormalBalance()
        {
            NegativeMonths = 0; //Количество последовательныъ месяцев негативного баланса
                                //Если долго не платить, то начнут списывать с других счетов
        }

        public void MessageAboutNegativeBalance(Client client)
        {
            NegativeMonths--;
            if(ConsoleWriteAndRead.NegativeMonths(this) != 0)   //Если количество негавтивных месяцев слишком велико
            {
                BankOfCard.Collectors(client, this);        //Статус ЗЭК и конец игры.
            }
        }

        public static void AddCashOnCard(Client client)
        {
            int choise = ConsoleWriteAndRead.ChoiseCard(client);    //Выбор карты
            if(choise == 0)
            {
                return;
            }
            ConsoleWriteAndRead.YourBalance(client);    //Вывод баланса
            int howMoney = ConsoleWriteAndRead.WriteYourChoise(1, (int)client.Money, message: "Введите сумму: "); //Ввод суммы от 1 до balance
            client.ClientCards[choise - 1].CardBalance += howMoney;
            client.Money -= howMoney;
        }

        public static void SpendMoney(Client client)
        {
            int choise = ConsoleWriteAndRead.ChoiseCard(client);
            if(choise == 0)
            {
                return;
            }
            int howMoney = ConsoleWriteAndRead.WriteYourChoise(1, 1000, message: "Введите сумму: ");
            ConsoleWriteAndRead.MessageAboutSpendMoney(client.ClientCards[choise - 1].BankOfCard.PossibilityToUse(client.ClientCards[choise - 1], howMoney));
        }

        public static void MonthlyService(Client client)    //Ежемесячное обслуживание
        {
            foreach (Card card in client.ClientCards)
            {
                if (card.TypeOfCard == "Дебетовая")
                {
                    card.CardBalance -= 1;
                }
                else    //Если кредитная, то и списание процентов.
                {
                    if (card.CardBalance < 0)
                    {
                        card.CardBalance = -(Math.Round(Math.Abs(card.CardBalance + (card.CardBalance * card.BankOfCard.CreditBet / 1200)), 2));
                    }
                }
            }
        }

        public Card(string type, string status, Bank bank)
        {
            TypeOfCard = type;
            CardBalance = 0;
            StatusOfCard = status;
            BankOfCard = bank;
            if (type == "Дебетовая")
            {
                LimitOfCard = 0;
            }
            else
            {
                LimitOfCard = 1000;
            }
            ID = ++countCards;
            NegativeMonths = 0;
        }
    }
}