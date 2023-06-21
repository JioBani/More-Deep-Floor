using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.Loggers
{
    public class Logger
    {
        public static bool isLoggerOn { private set; get; }
        

        public static void SetLogger(bool value)
        {
            isLoggerOn = value;
        }

        public static void Log(object msg)
        {
            if(isLoggerOn)
                Debug.Log(msg);
        }

        public static void LogWarning(object msg)
        {
            if(isLoggerOn)
                Debug.LogWarning(msg);
        }
        
        public static void LogException(Exception e)
        {
            if(isLoggerOn)
                Debug.LogException(e);
        }
    }
}


