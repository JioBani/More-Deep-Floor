using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LNK.MoreDeepFloor.Common.Loggers;
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
            try
            {
                if (time <= 0)
                {
                    throw new Exception($"[TimerManager.LateAction()] 유효하지않은 time : {time}");
                }
                StartCoroutine(CheckTimerRoutine(time , OnTimerEndAction));
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
                throw;
            }
        }

        IEnumerator CheckTimerRoutine(float time , OnTimerEndEventHandler OnTimerEndAction)
        {
            yield return YieldInstructionCache.WaitForSeconds(time);
            OnTimerEndAction.Invoke();
        }
    }
}

