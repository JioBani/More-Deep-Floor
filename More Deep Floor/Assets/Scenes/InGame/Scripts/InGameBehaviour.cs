using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public abstract class InGameBehaviour : MonoBehaviour
    {
        private InGameStateManager inGameStateManager;
        
        private void Awake()
        {
            inGameStateManager = ReferenceManager.instance.inGameStateManager;
            inGameStateManager.OnSettingCompletedAction += OnSettingCompleted;
            inGameStateManager.OnRoundStartAction += OnRoundStart;
            inGameStateManager.OnRoundEndAction += OnRoundEnd;
            inGameStateManager.OnStageFinishedAction += OnStageFinished;
            //#. 이벤트 연결

            BehaviorAwake();
        }

        protected virtual void BehaviorAwake(){}

        protected virtual void OnSettingCompleted(){ }
        
        protected virtual void OnRoundStart(int round){ }
        
        protected virtual void OnRoundEnd(int round){ }
        
        protected virtual void OnStageFinished(){ }
    }
}


