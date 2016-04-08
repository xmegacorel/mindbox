using System;
using System.Collections.Generic;

namespace TripCards
{
    public class ShuffleCard
    {
        static readonly Random _random = new Random();

        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = list[r];
                list[r] = list[i];
                list[i] = t;
            }
        }
    }
}