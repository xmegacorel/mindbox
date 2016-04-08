using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace TripCards
{
    /// <summary>
    /// в тестах разрешено использовать только русские буквы, гибридное дерево оптимизировано на русские буквы
    /// если использовать иностранные буквы, то размер буфера 26 символов и первая буква - "a" латинского алфавита в низнем регистре
    /// </summary>
    public class UnitTest
    {
        private readonly ITestOutputHelper _output;
        public UnitTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void PassSmall()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Кельн" }, 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Кельн", To = "Москва" },
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            var result = sca.SortSecondMethod();
            int count = 0;
            for(int i = 0; i < result.Count - 1; i++)
            {
                if (result[i].To == result[i + 1].From)
                    count++;
            }
            Assert.Equal(count + 1, result.Count);
        }

        [Fact]
        public void PassMiddle()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Кельн", To = "Париж" },
                new Card() { From = "Париж", To = "Москва" },
                new Card() { From = "Москва", To = "Пенза" },
                new Card() { From = "Пенза", To = "Рязань" },
                new Card() { From = "Рязань", To = "Новгород" },
                new Card() { From = "Новгород", To = "Сочи" },
                new Card() { From = "Сочи", To = "Ростов" },
                new Card() { From = "Ростов", To = "Краков" },
                new Card() { From = "Краков", To = "Лондон" },
                new Card() { From = "Лондон", To = "Милан" }
            };

            // шаффл по методу Кнута
            ShuffleCard.Shuffle<Card>(trips);

            trips.Insert(0, new Card() { From = "Мельбурн", To = "Кельн" });

            foreach (var trip in trips)
            {
                _output.WriteLine("Из " + trip.From + " в " + trip.To);
            }

            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            var result = sca.SortSecondMethod();
            int count = 0;
            for (int i = 0; i < result.Count - 1; i++)
            {
                if (result[i].To == result[i + 1].From)
                    count++;
            }
            Assert.Equal(count + 1, result.Count);
        }

       

        [Fact]
        public void TestArgumentException1()
        {
            Assert.Throws(typeof(ArgumentException), () => { new SortCardAlgorithm(null); });
        }

        [Fact]
        public void TestArgumentException2()
        {
            Assert.Throws(typeof(ArgumentException), () => { new SortCardAlgorithm(new List<Card>()); });
        }

        [Fact]
        public void TestRecursiveOneElement()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Мельбурн" } 
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(RecursivePathException), () => { sca.SortSecondMethod(); });
        }

        [Fact]
        public void TestRecursive1()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Кельн" }, 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Кельн", To = "Москва" },
                new Card() { From = "Москва", To = "Кельн" },
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(RecursivePathException), () => { sca.SortSecondMethod(); });
        }

        [Fact]
        public void TestRecursive2()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Кельн" }, 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Кельн", To = "Москва" },
                new Card() { From = "Кельн", To = "Кельн" },
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(RecursivePathException), () => { sca.SortSecondMethod(); });
        }
        [Fact]
        public void TestRecursive3()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Кельн" }, 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Кельн", To = "Москва" },
                new Card() { From = "Кельн", To = "Токио" },
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(RecursivePathException), () => { sca.SortSecondMethod(); });
        }
        [Fact]
        public void TestRecursive4()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Кельн" }, 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Кельн", To = "Москва" },
                new Card() { From = "Кельн", To = "Москва" },
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(RecursivePathException), () => { sca.SortSecondMethod(); });
        }
        
        [Fact]
        public void TestInvalidPath1()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Кельн" }, 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Кельн", To = "Москва" },
                new Card() { From = "Мельбурн", To = "Москва" },
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(InvalidPathException), () => { sca.SortSecondMethod(); });
        }

        [Fact]
        public void TestInvalidPath2()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Москва", To = "Париж" },
            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(InvalidPathException), () => { sca.SortSecondMethod(); });
        }


        [Fact]
        public void TestInvalidpath3()
        {
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "Кельн" }, 
                new Card() { From = "Москва", To = "Париж" },
                new Card() { From = "Кельн", To = "Пенза" },

            };
            SortCardAlgorithm sca = new SortCardAlgorithm(trips);
            Assert.Throws(typeof(InvalidPathException), () => { sca.SortSecondMethod(); });
        }

        [Fact]
        public void TestEmptyTown()
        {
            
            List<Card> trips = new List<Card>() 
            { 
                new Card() { From = "Мельбурн", To = "" }, 
                new Card() { From = "", To = "Париж" },
                new Card() { From = "Кельн", To = "Пенза" },

            };

            Assert.Throws(typeof(ArgumentException), () => { new SortCardAlgorithm(trips); });
        }
    }
}
