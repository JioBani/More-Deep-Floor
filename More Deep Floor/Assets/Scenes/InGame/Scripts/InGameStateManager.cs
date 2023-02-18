using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.InGame
{
    public class InGameStateManager : MonoBehaviour
    {
        public delegate void OnSceneLoadEventHandler();

        public delegate void OnStageStartEventHandler();

        public delegate void OnStageEndEventHandler();

        public delegate void OnRoundStartEventHandler(int round);

        public delegate void OnRoundEndEventHandler(int round);


        public OnSceneLoadEventHandler OnSceneLoadAction;
        public OnStageStartEventHandler OnStageStartAction;
        public OnStageEndEventHandler OnStageEndAction;
        public OnRoundStartEventHandler OnRoundStartAction;
        public OnRoundEndEventHandler OnRoundEndAction;


        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnSceneLoadAction?.Invoke();
        }

        public void SetStageStart()
        {

        }

        public void SetStageEnd()
        {

        }

        public void SetRoundStart(int round)
        {

        }

        public void SetRoundEnd(int round)
        {
            
        }
    }
}


