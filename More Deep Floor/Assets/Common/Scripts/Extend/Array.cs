using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;
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
                Logger.LogException(e);
                Logger.LogWarning($"[Array.SaveGet()] 배열 참조 오류 : [{index}]의 값이 ${defaultValue}로 리턴됨");
                return defaultValue;
            }
        }
    }
}
