using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Stages;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LNK.MoreDeepFloor.InGame
{
    public class MonsterManager : InGameBehaviour
    {
        
        public ObjectPooler objectPooler;
        public GameObject monsterPool;
        private MarketManager marketManager;
        private TileManager tileManager;
        
        public GameObject monsterParent;
        public List<Monster> monsters = new List<Monster>();
        public int roundMonsterNums;
        public int roundOffMonsterNums;
        //private RoundOriginalData _currentRoundOriginal;
        private List<List<Tile>> routes;

        
        private MonsterData currentMonsterData;
        [SerializeField] private MonsterOriginalData testMonsterOriginalData;

        public int monsterNumber { get; private set; }

        public delegate void OnMonsterNumberChangeEventHandler(int number);

        public OnMonsterNumberChangeEventHandler OnMonsterNumberChangeAction;

        #region #. 이벤트함수

        protected override void BehaviorAwake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            tileManager = ReferenceManager.instance.tileManager;
        }

        private void Start()
        {
            for (int i = 0; i < monsterPool.transform.childCount; i++)
            {
                Monster monster =  monsterPool.transform.GetChild(i).GetComponent<Monster>();
                monster.OnDieAction += OnMonsterDie;
                monster.OnPassAction += OnMonsterPass;
                monster.OnOffAction += OnMonsterOff;
            }


            routes = new List<List<Tile>>();
            
            for (int i = 0; i < 5; i++)
            {
                List<Tile> route = new List<Tile>();
                route.Add(tileManager.battleFieldTiles[i][14]);
                route.Add(tileManager.battleFieldTiles[i][0]);
                routes.Add(route);
            }
        }

        protected override void OnRoundStart(int round)
        {
            SetBattleState();
        }

        public void Setting(){
            monsterNumber = 0;
            OnMonsterNumberChangeAction?.Invoke(monsterNumber);
        }

        #endregion
        
        

        public void SetRoundMonster(RoundData roundData)
        {
            monsterNumber = 0;
            monsters = new List<Monster>();
            for (int i = 0; i < roundData.MonsterCount; i++)
            {
                MonsterOriginalData monsterOriginalData = roundData.GetMonster();
                Monster monster = GenerateMonster(new MonsterData(monsterOriginalData));
                monster.transform.position = tileManager.battleFieldTiles[Random.Range(0, 5)][Random.Range(0, 5)].transform.position;
                monsters.Add(monster);
            }
        }

        Monster GenerateMonster(MonsterData monsterData)
        {
            Monster monster = objectPooler.Pool(monsterParent).GetComponent<Monster>();
            monster.Init(monsterData, 0);
            monsters.Add(monster.GetComponent<Monster>());

            monsterNumber++;
            OnMonsterNumberChangeAction?.Invoke(monsterNumber);
            return monster;
        }

        void OnMonsterDie(Entity monster , Entity killer)
        {
            marketManager.GoldChange(((Monster)monster).monsterStatus.currentGold , "몬스터 처치");
            monsterNumber--;
            monsters.Remove((Monster)monster);
            OnMonsterNumberChangeAction?.Invoke(monsterNumber);
        }

        void OnMonsterPass(Entity monster)
        {
            
        }
        
        void OnMonsterOff(Entity monster , Entity killer)
        {
            roundOffMonsterNums++;
            monsters.Remove(monster as Monster);
        }

        public void GenerateTestMonster()
        {
            Monster monster = objectPooler.Pool(monsterParent).GetComponent<Monster>();
            monsters.Add(monster);
            monster.Init(new MonsterData(testMonsterOriginalData), 0);

            monsterNumber++;
            OnMonsterNumberChangeAction?.Invoke(monsterNumber);
        }

        public void SetBattleState()
        {
            for (var i = 0; i < monsters.Count; i++)
            {
                monsters[i].entityBehavior.SetBehaviorState(EntityBehaviorState.Battle);
            }
        }
    }

}
