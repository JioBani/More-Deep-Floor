using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class ListExtensions
    {
        public static List<T> Filter<T>(this List<T> list , Func<T , bool> check)
        {
            List<T> result = new List<T>();
            
            foreach (var e in list)
            {
                if(check(e)) result.Add(e);
            }

            return result;
        }
    }
}
