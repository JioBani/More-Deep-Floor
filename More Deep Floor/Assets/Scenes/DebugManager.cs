using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using UnityEngine;

namespace LNK.MoreDeepFloor
{
    public class DebugManager : MonoBehaviour
    {
        private void Awake()
        {
            CustomLogger.SetLogger(true);
        }
    }
}


