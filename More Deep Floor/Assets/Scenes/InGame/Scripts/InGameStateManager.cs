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

        public delegate void OnDataLoadEventHandler();

        public delegate void OnStageStartEventHandler();

        public delegate void OnStageEndEventHandler();

        public delegate void OnRoundStartEventHandler(int round);

        public delegate void OnRoundEndEventHandler(int round);

        public delegate void OnGameOverEventHandler();


        public OnSceneLoadEventHandler OnSceneLoadAction;
        public OnDataLoadEventHandler OnDataLoadAction;
        public OnStageStartEventHandler OnStageStartAction;
        public OnStageEndEventHandler OnStageEndAction;
        public OnRoundStartEventHandler OnRoundStartAction;
        public OnRoundEndEventHandler OnRoundEndAction;
        public OnGameOverEventHandler OnGameOverAction;


        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("[InGameStateManager] 씬 로드");
            OnSceneLoadAction?.Invoke();

            Debug.Log("[InGameStateManager] 데이터 로드");
            OnDataLoadAction?.Invoke();
        }

        public void SetStageStart()
        {
            Debug.Log("[InGameStateManager] 스테이지 시작");

            OnStageStartAction?.Invoke();
        }

        public void SetStageEnd()
        {
            Debug.Log("[InGameStateManager] 스테이지 끝");
            
            OnStageEndAction?.Invoke();
        }

        public void SetRoundStart(int round)
        {
            Debug.Log("[InGameStateManager] 라운드 시작");

            OnRoundStartAction?.Invoke(round);
        }

        public void SetRoundEnd(int round)
        {
            Debug.Log("[InGameStateManager] 라운드 끝");

            OnRoundEndAction?.Invoke(round);
        }

        public void SetGameOver()
        {
            OnGameOverAction?.Invoke();
            Debug.Log("[InGameStateManager.SetGameOver()] 게임 오버");
            SceneManager.LoadScene("MainMenu");
        }
    }
}


