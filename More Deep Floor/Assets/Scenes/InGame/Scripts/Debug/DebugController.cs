using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DebugSystem
{
    public class DebugController : MonoBehaviour
    {
        //public bool showDefenderState ;

        private void Awake()
        {
            CustomLogger.SetLogger(true);
        }
    }
}


