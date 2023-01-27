using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public static class Extensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            int elements = list.Count;
            var random = new Random();
            while (elements > 1)
            {
                elements--;
                int swapIndex = random.Next(elements + 1);
                T value = list[swapIndex];
                list[swapIndex] = list[elements];
                list[elements] = value;
            }
        }
    }
}
