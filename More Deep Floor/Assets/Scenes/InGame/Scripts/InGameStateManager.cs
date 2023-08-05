using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.SceneDataSystem;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.InGame
{
    public enum GameState
    {
        StageSetting = 1,
        RoundWaiting = 2,
        RoundPlaying = 3,
        StageFinished = 4,
    }
    
    public class InGameStateManager : MonoBehaviour
    {
        //#. 참조
        [SerializeField] private ResultWindow resultWindow;

        //#. 변수
        public GameState gameState { private set; get; }

        //#. 프로퍼티

        #region #. 이벤트

        public delegate void OnSettingCompletedEventHandler();
        public OnSettingCompletedEventHandler OnSettingCompletedAction;
        
        public delegate void OnRoundStartEventHandler(int round);
        public OnRoundStartEventHandler OnRoundStartAction;
        
        public delegate void OnRoundEndEventHandler(int round);

        public OnRoundEndEventHandler OnRoundEndAction;

        public delegate void OnStageFinishedEventHandler();
        public OnStageFinishedEventHandler OnStageFinishedAction;

        #endregion

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            gameState = GameState.StageSetting;
            
            LoadData();
            StartSetting();
            gameState = GameState.RoundWaiting;
            OnSettingCompletedAction?.Invoke();
        }
        
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

       
        void LoadData()
        {
            ReferenceManager.instance.defenderManager.SetData();
        }
        
        //#. 매니저들 세팅하기
        void StartSetting()
        {
            ReferenceManager.instance.marketManager.Setting();
            ReferenceManager.instance.monsterManager.Setting();
        }

        public void NotifyRoundStart(int round)
        {
            Debug.Log("[InGameStateManager] 라운드 시작");
            gameState = GameState.RoundPlaying;
            OnRoundStartAction?.Invoke(round);
        }

        public void SetRoundEnd(int round)
        {
            Debug.Log("[InGameStateManager] 라운드 끝");
            OnRoundEndAction?.Invoke(round);
        }

        public void NotifyGameOver()
        {
            //TODO 게임오버 로직
        }
    }
}


