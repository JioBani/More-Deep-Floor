using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.WaitForSecondsCache;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.TimerSystem
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public delegate void OnTimerEndEventHandler();

        public void LateAction(float time , OnTimerEndEventHandler OnTimerEndAction)
        {
            StartCoroutine(CheckTimerRoutine(time , OnTimerEndAction));
        }

        IEnumerator CheckTimerRoutine(float time , OnTimerEndEventHandler OnTimerEndAction)
        {
            yield return YieldInstructionCache.WaitForSeconds(time);
            OnTimerEndAction.Invoke();
        }
    }
}

