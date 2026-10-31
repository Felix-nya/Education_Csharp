using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Classes
{
    internal static class CacheCalculator<T> where T : notnull
    {
        private static Dictionary<T, T> cache = [];
        internal static T Calculate(T input, Func<T, T> operation)
        {
            if (cache.TryGetValue(input, out T? result))
            {
                Console.WriteLine($"Возвращены данные из кэша: для {input}");
                return result;
            }
            result = operation(input);
            cache.Add(input, result);
            return result;
        }
    }
}
