using System;
using System.Collections.Generic;

namespace TripCards
{
    internal class SortCardAlgorithm
    {
        private List<Card> _cards;

        public SortCardAlgorithm(List<Card> cards)
        {
            if (cards == null)
                throw new ArgumentException("Некорректные данные на входе алгоритма");
            if (cards.Count == 0)
                throw new ArgumentException("Передан пустой массив карточек");
            // По заветам Седжвика, нужно произвести полное копирование входных данных. 
            // Полное, это значит не только скопировать ссылки на элементы, но и значения элементов
            
            _cards = new List<Card>(cards.Count);
            for (int i = 0; i < cards.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(cards[i].From) || string.IsNullOrWhiteSpace(cards[i].To))
                    throw new ArgumentException("В карточке пустой город");

                _cards.Add(new Card() { From = cards[i].From.Clone() as string, To = cards[i].To.Clone() as string });
            }
                
        }

        /// <summary>
        /// сортировка с использование словаря, 
        /// сложность L(N-1) + L(N -1) + 2L(N -1) = 4L(N-1) ~ cN, 
        /// потребление памяти 4..16N
        /// </summary>
        /// <returns></returns>
        public List<Card> SortFirstMethod()
        {
            var result = InitResult();
            
            if (_cards.Count == 1)
                return result;
                
            var from = new Dictionary<string, int>(_cards.Count);
            for (int i = 1; i < _cards.Count; i++)
            {
                var town = _cards[i].From;
                if (from.ContainsKey(town))
                    throw new RecursivePathException("В массиве карточек присутствую циклы");
                from.Add(town, i);
            }
                
            var currentTown = _cards[0].To;
            for (int i = 1; i < _cards.Count; i++)
            {
                if (!from.ContainsKey(currentTown))
                    throw new InvalidPathException("В массиве карточек присутствуют разрывы");

                int index = from[currentTown];
                result.Add(new Card() { From = _cards[index].From.Clone() as string, To = _cards[index].To.Clone() as string});
                currentTown = _cards[index].To;
            }
            
            return result;
        }

        /// <summary>
        /// сортировка с использование гибридного суффиксного дерева,  TST with R^2
        /// сложность 2 * Sum(L + ln(n)) + Sum(L + ln(n)) ~ NlnN, потребление памяти 4N
        /// быстрее, чем хэширование
        /// </summary>
        /// <returns></returns>
        public List<Card> SortSecondMethod()
        {
            var result = InitResult();

            if (_cards.Count == 1)
                return result;

            Tries<int> tr = new Tries<int>();
            for (int i = 1; i < _cards.Count; i++)
            {
                var town = _cards[i].From;
                if (tr.Get(town).Result == SearchResultTstEnum.Found)
                    throw new RecursivePathException("В массиве карточек присутствую циклы");
                tr.Put(new Tries<int>.Item() { Word = _cards[i].From, Index = i});
            }

            var currentTown = _cards[0].To;
            for (int i = 1; i < _cards.Count; i++)
            {
                var elem = tr.Get(currentTown);
                if (elem.Result == SearchResultTstEnum.NotFound)
                    throw new InvalidPathException("В массиве карточек присутствуют разрывы");

                result.Add(new Card() { From = _cards[elem.Value].From.Clone() as string, To = _cards[elem.Value].To.Clone() as string });
                currentTown = _cards[elem.Value].To;
            }

            return result;

        }

        private List<Card> InitResult()
        {
            if (string.Compare(_cards[0].From, _cards[0].To, true) == 0)
                throw new RecursivePathException("В массиве карточек присутствую циклы");

            var result = new List<Card>(_cards.Count) { new Card() { From = _cards[0].From.Clone() as string, To = _cards[0].To.Clone() as string } };
            return result;
        }

    }
}
