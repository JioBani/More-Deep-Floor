using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class ListExtensions
    {
        /// <summary>
        /// 조건식에 참인 요소를 리스트로 반환합니다
        /// </summary>
        /// <param name="check">조건식</param>
        /// <returns>참인 요소 리스트</returns>
        public static List<T> Filter<T>(this List<T> list, Func<T, bool> check)
        {
            List<T> result = new List<T>();

            foreach (var e in list)
            {
                if (check(e)) result.Add(e);
            }

            return result;
        }

        /// <summary>
        /// 조건식이 반환하는 객체들의 리스트를 반환합니다.
        /// </summary>
        /// <param name="make">조건식</param>
        /// <typeparam name="TResult">객체 타입</typeparam>
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
