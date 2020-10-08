using System;

namespace Shkadun_BankSystem
{
    public class Bank
    {
        public string BankName { get; private set; }
        public double CreditBet { get; private set; }

        public int PossibilityToUse(Card card, int money)   //Возможность использования карты
        {
            if(card.TypeOfCard == "Дебетовая")
            {
                if ((card.CardBalance - money) < 0)
                {
                    return 0;
                }
                else
                {
                    card.CardBalance -= money;
                    return 1;
                }
            }
            else
            {
                if((card.CardBalance - money) > -card.LimitOfCard)
                {
                    card.CardBalance -= money;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Collectors(Client client, Card card)   
        {
            if(client.Money > Math.Abs(card.CardBalance)) //Если есть деньги, то забирают их.
            {
                client.Money += card.CardBalance;   
                card.CardBalance = 0;
            }
            else    //если не хватает
            {
                card.CardBalance += client.Money;
                client.Money = 0;
                foreach(Card cards in client.ClientCards)   //То списывают с других карточек
                {
                    if(cards.BankOfCard == this)            //Но этого же банка
                    {
                        if(cards.CardBalance > 0)           
                        {
                            if(cards.CardBalance > Math.Abs(card.CardBalance))
                            {
                                cards.CardBalance += card.CardBalance;
                                card.CardBalance = 0;
                            }
                            else
                            {
                                card.CardBalance += cards.CardBalance;
                                cards.CardBalance = 0;
                            }
                        }
                    }
                    if(card.CardBalance >= 0)
                    {
                        break;
                    }
                }
                if(card.CardBalance < 0)    //Если баланс все равно отрицателен
                {
                    ConsoleWriteAndRead.GameOver(client);   //Статус ЗЭК и конец игры
                }
            }
        }

        public void OpenNewCard(Client client)
        {
            Card card = new Card(ConsoleWriteAndRead.ChoiseTypeOfCard(), "Активная", this);
            client.AddCardToList(card);
        }

        public Bank(string name, int bet) {
            this.BankName = name;
            this.CreditBet = bet;
        }
    }
}