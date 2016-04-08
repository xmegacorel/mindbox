using System;
using System.Collections.Generic;
using System.Linq;

namespace TripCards
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Card> trips = new List<Card>() 
            //{ 
            //    new Card() { From = "Мельбурн", To = "Кельн" }, 
            //    new Card() { From = "Москва", To = "Париж" },
            //    new Card() { From = "Кельн", To = "Москва" },
            //};
            //SortCardAlgorithm sca = new SortCardAlgorithm(trips);

            //var result = sca.SortFirstMethod();

            //foreach(var card in result)
            //{
            //    Console.WriteLine("Город отправления {0} -> город назначения {1}", card.From, card.To);
            //}
            //Console.ReadKey();

            Tries<int> tr = new Tries<int>(new[]
            {
                new Tries<int>.Item() { Word = "Москва", Index = 0 }, 
                new Tries<int>.Item() { Word = "Спб" , Index = 1}, 
                new Tries<int>.Item() { Word = "Казань" ,  Index = 2}, 
                new Tries<int>.Item() { Word = "Пенза", Index = 3}
            });
            Console.WriteLine("Найдено слово {0}", tr.Get("пенза").Result == SearchResultTstEnum.Found);
            Console.WriteLine("Найдено слово {0}", tr.Get("пенза1").Result == SearchResultTstEnum.Found);

            Console.ReadKey();
        }

        

    }
}
