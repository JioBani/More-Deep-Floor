using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame.DebugSystem
{
    public class DebugController : MonoBehaviour
    {
        public bool showDefenderState ;

        private void Awake()
        {
            Logger.SetLogger(true);
        }
    }
}


