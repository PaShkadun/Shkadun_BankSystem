using System.Collections.Generic;

namespace Shkadun_BankSystem
{
    public class Client
    {
        private string ClientName { get; set; }
        private int ClientAge { get; set; }
        public double Money { get; set; }
        public List<Card> ClientCards { get; private set; }
        public string ClientStatus { get; set; }

        public void GetMoney()  
        {
            Money += ConsoleWriteAndRead.GetRandomMoney(); //Получение рандомной суммы от 1 до 300
            Card.MonthlyService(this);  //Списание ежемесячного обслуживания и процентов по кредиту
            ConsoleWriteAndRead.YourBalance(this);  //Вывод сообщения о балансе
        }

        public void CheckClientCards() //Проверка баланса карточек("ежемесячно")
        {
            if(this.ClientCards.Count == 0)
            {
                return;
            }
            else
            {
                foreach(Card card in ClientCards)
                {
                    if(card.CardBalance < 0)
                    {
                        card.MessageAboutNegativeBalance(this); //Сообщение о негативном балансе
                    }
                    else
                    {
                        card.NormalBalance();
                    }
                }
            }
        }

        public void AddCardToList(Card card) //Добавление новой карты
        {
            ClientCards.Add(card);
            ConsoleWriteAndRead.SuccessAddCard(); //Вывод сообщения успешности
        }

        public void DeleteCard() //Удаление карточки 
        {
            int choise = ConsoleWriteAndRead.ChoiseCard(this);  //Вывод списка карт и выбор необходимой
            if (choise == 0)
            {
                return;
            }
            else if (ClientCards[choise - 1].CardBalance < 0)   //Если отрицательный баланс
            {
                ConsoleWriteAndRead.NegativeBalance(this);
            }
            else
            {
                ClientCards.RemoveAt(choise - 1);
            }
        }

        public Client(string name, int age)
        {
            ClientName = name;
            ClientAge = age;
            Money = 3000;
            ClientCards = new List<Card>();
            ClientStatus = "Активный";
        }
    }
}