using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class ListExtensions
    {
        public static List<T> Filter<T>(this List<T> list, Func<T, bool> check)
        {
            List<T> result = new List<T>();

            foreach (var e in list)
            {
                if (check(e)) result.Add(e);
            }

            return result;
        }

        public static List<TResult> Make<TResult , TOriginal>(this List<TOriginal> list, Func<TOriginal , TResult> make)
        {
            List<TResult> result = new List<TResult>();

            foreach (var e in list)
            {
                result.Add(make(e));
            }

            return result;
        }
        
        public static bool HaveConditions <T>(this List<T> list, Func<T , bool> check)
        {
            foreach (var e in list)
            {
                if (check(e)) return true;
            }
            return false;
        }

        //public static Dictionary<K , V> ToDictionary
    }
}
