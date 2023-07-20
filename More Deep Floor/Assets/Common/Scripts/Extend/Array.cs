using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using LNK.MoreDeepFloor.Common.Loggers;
using Object = System.Object;

namespace ExtensionMethods
{
    public static class ArrayExtensions
    {
        public static T SaveGet<T>(this T[] array , int index , T defaultValue)
        {
            try
            {
                if (array.Length <= index)
                {
                    //Logger.LogWarning($"[Array.SaveGet()] 배열 참조 오류 : [{index}]의 값이 ${defaultValue}로 리턴됨");
                    throw new IndexOutOfRangeException();
                }
                else
                    return array[index];
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
                CustomLogger.LogWarning($"[Array.SaveGet()] 배열 참조 오류 : [{index}]의 값이 ${defaultValue}로 리턴됨");
                return defaultValue;
            }
        }
        
        public static bool HaveConditions <T>(this T[] array, Func<T , bool> check)
        {
            foreach (var e in array)
            {
                if (check(e)) return true;
            }
            return false;
        }
        
        public static List<TResult> MakeToList<TOriginal , TResult>(this TOriginal[] array, Func<TOriginal , TResult> make)
        {
            List<TResult> result = new List<TResult>();

            foreach (var e in array)
            {
                result.Add(make(e));
            }

            return result;
        }
        
        /*public static List<TResult> MakeToList<TResult , TOriginal>(this TOriginal[] array, Func<TOriginal , TResult> make)
        {
            List<TResult> result = new List<TResult>();

            foreach (var e in array)
            {
                result.Add(make(e));
            }

            return result;
        }*/


    }
}
