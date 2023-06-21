using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class TransformExtensions
    {
        public static void EachChild<T>(this Transform t , Action<Transform> check)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                check(t.GetChild(i));
            }
        }
    }
}
