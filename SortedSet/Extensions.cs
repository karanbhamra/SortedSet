using System;
using System.Collections.Generic;
using System.Text;

namespace Set
{
    public static class Extensions
    {
        public static void PrintEnumerable<T>(this IEnumerable<T> items, char sep=',')
        {
            Console.WriteLine(string.Join(sep, items));
        }
    }
}
