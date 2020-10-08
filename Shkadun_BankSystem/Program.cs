using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shkadun_BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(ConsoleWriteAndRead.WriteYourName(), ConsoleWriteAndRead.WriteYourAge()); //Создание клиента
            List<Bank> banks = new List<Bank>();    //Списка банков
            banks.Add(ConsoleWriteAndRead.CreateNewBank());
            Task task = Task.Factory.StartNew(() =>
            {
                while (client.ClientStatus != "ЗЭК") //Статус ЗЭК можно получить имея длительную задолженность
                {
                    Thread.Sleep(20000);
                    client.GetMoney();  //Начисление денег за работу
                    client.CheckClientCards(); //Проверка карточек пользователя на задолженность
                }
            });
            while (client.ClientStatus != "ЗЭК")
            {
                switch(ConsoleWriteAndRead.WhatDoNext())
                {
                    case 1: ConsoleWriteAndRead.ListOfBanks(banks, client); break; //Вывод списка банков для добавления карты в дальнейшем
                    case 2: client.DeleteCard(); break; //Удаление карты
                    case 3: Card.AddCashOnCard(client); break;  //Пополнить карту
                    case 4: ConsoleWriteAndRead.ListOfCards(client); break; //Список карт
                    case 5: Card.SpendMoney(client); break; //потратить деньги
                }
            }
        }
    }
}
