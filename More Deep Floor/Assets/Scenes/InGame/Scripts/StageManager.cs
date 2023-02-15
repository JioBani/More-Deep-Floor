using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class StageManager : MonoBehaviour
    {
        private MonsterManager monsterManager;
        private TileManager tileManager;
        
        public StageData stageData;
        private InfinityTowerData infinityTowerData;

        public bool isStageStarted = false;
        public List<Vector2Int> route;
        private List<Tile> routeTiles = new List<Tile>();
        public int round = 0;
        
        public delegate void RoundEvent(int round);

        
        public RoundEvent OnRoundStartAction;
        public RoundEvent OnRoundEndAction;

        void Awake()
        {
            monsterManager = ReferenceManager.instance.monsterManager;
            tileManager = ReferenceManager.instance.tileManager;
        }

        void Start()
        {
            //stageOriginalData = SceneDataManager.instance.stageOriginalData;
            stageData = new StageData(SceneDataManager.instance.GetStageData());
                
            for (int i = 0; i < route.Count; i++)
            {
                routeTiles.Add(tileManager.battleFieldTiles[route[i].y][route[i].x]);
            }
            tileManager.SetRoute(route);
        }

        public void OnClickStageStart()
        {
            if(isStageStarted) return;
            
            if (stageData.isInfinity)
            {
                infinityTowerData = new InfinityTowerData(stageData.infiniteTowerOriginalData);
            }
            StartStage();
        }

        public void StartStage()
        {
            monsterManager.StartStage(routeTiles);
            Debug.Log("[StageManager.StartStage()] 스테이지 시작");

            if(stageData.isInfinity)
                Invoke(nameof(StartInfinityTowerRound) , 3.0f);
            else
                Invoke(nameof(StartNormalRound) , 3.0f);
            isStageStarted = true;
        }
        
        public void SetRoundEnd()
        {
            Debug.Log("[StageManager.StartRound()] 라운드 종료");
            if (round == stageData.rounds)
            {
                Debug.Log("[StageManager.StartRound()] 스테이지 종료");
            }
            else
            {
                if (stageData.isInfinity)
                {
                    EndInfinityTowerRound();
                }
                else
                {
                    EndNormalRound();
                }
            }
            OnRoundEndAction?.Invoke(round);
        }

        void StartNormalRound()
        {
            monsterManager.StartRound(stageData.roundOriginalDatas[round]);
            round++;
            OnRoundStartAction?.Invoke(round);
            Debug.Log($"[StageManager.StartRound()] {round} 라운드 시작");
        }

        void EndNormalRound()
        {
            Debug.Log("[StageManager.StartRound()] 라운드 종료");
            if (round == stageData.rounds)
            {
                Debug.Log("[StageManager.StartRound()] 스테이지 종료");
            }
            else
            {
                Invoke(nameof(StartNormalRound) , 3.0f);
            }    
        }

        void StartInfinityTowerRound()
        {
            monsterManager.StartInfinityTowerRound(infinityTowerData);
            round++;
            OnRoundStartAction?.Invoke(round);
            Debug.Log($"[StageManager.StartRound()] {round} 라운드 시작");
        }

        void EndInfinityTowerRound()
        {
            infinityTowerData.SetNextRound();
            Invoke(nameof(StartInfinityTowerRound) , 3.0f);
        }

        
    }
}


