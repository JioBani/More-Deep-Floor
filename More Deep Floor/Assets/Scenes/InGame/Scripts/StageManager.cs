using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Stages;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class StageManager : InGameBehaviour
    {
        //#. 참조
        private MonsterManager monsterManager;
        private DefenderManager defenderManager;
        private InGameStateManager inGameStateManager;
        [SerializeField] private StageData stageData;

        //#. 변수
        public int round = 0;
        private WaitForSeconds roundTimer;

        //#. 프로퍼티
        public int monsterLimit;


        protected override void BehaviorAwake()
        {
            inGameStateManager = ReferenceManager.instance.inGameStateManager;
            monsterManager = ReferenceManager.instance.monsterManager;
            defenderManager = ReferenceManager.instance.defenderManager;

            monsterManager.OnMonsterNumberChangeAction += CheckMonsterLimit;
        }

        protected override void OnSettingCompleted()
        {
            round = 1;
            monsterManager.SetRoundMonster(stageData.Rounds[round]);
        }

        protected override void OnRoundEnd(int _round)
        {
            if (_round < stageData.RoundCount)
            {
                monsterManager.SetRoundMonster(stageData.Rounds[_round + 1]);
            }
        }
        
        public void OnClickRoundStart()
        {
            RoundStart();
        }
        
        void RoundStart()
        {
            CustomLogger.Log("[StageManager.RoundStart()]");
            //#. 활성화
            //monsterManager.SetBattleState();
            //defenderManager.SetBattleState();
            
            //#. 이벤트 알리기
            inGameStateManager.NotifyRoundStart(round);
        }


        void SetGameOver()
        {
            inGameStateManager.NotifyGameOver();
        }
        
        void CheckMonsterLimit(int number)
        {
            if (number > monsterLimit)
            {
                SetGameOver();
            }
        }


    }
}


