using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LNK.MoreDeepFloor.InGame
{
    public class MonsterManager : MonoBehaviour
    {
        
        public ObjectPooler objectPooler;
        public GameObject monsterPool;
        private MarketManager marketManager;
        private TileManager tileManager;
        
        public GameObject monsterParent;
        public List<Monster> monsters = new List<Monster>();
        public int roundMonsterNums;
        public int roundOffMonsterNums;
        private RoundOriginalData _currentRoundOriginal;
        private List<List<Tile>> routes;

        
        private MonsterData currentMonsterData;
        [SerializeField] private MonsterOriginalData testMonsterOriginalData;

        public int monsterNumber { get; private set; }

        public delegate void OnMonsterNumberChangeEventHandler(int number);

        public OnMonsterNumberChangeEventHandler OnMonsterNumberChangeAction;

        private void Awake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            tileManager = ReferenceManager.instance.tileManager;

            ReferenceManager.instance.inGameStateManager.OnDataLoadAction += OnDataLoad;
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

        private void OnDataLoad()
        {
            monsterNumber = 0;
            OnMonsterNumberChangeAction?.Invoke(monsterNumber);
        }

        public void StartStage()
        {
            //SetRoute();
        }

        public void StartRound(RoundOriginalData roundOriginalData)
        {
            roundMonsterNums = roundOriginalData.MonsterNums;
            currentMonsterData = new MonsterData(roundOriginalData.MonsterOriginal);
            roundOffMonsterNums = 0;
            _currentRoundOriginal = roundOriginalData;
            StartCoroutine(GenerateMonsterLoop(roundOriginalData.MonsterNums));
        }

        public void StartInfinityTowerRound(InfinityTowerData infinityTowerData)
        {
            roundMonsterNums = infinityTowerData.monsterNums;
            currentMonsterData = infinityTowerData.currentMonsterData;
            roundOffMonsterNums = 0;
            StartCoroutine(GenerateMonsterLoop(roundMonsterNums));
        }


        IEnumerator GenerateMonsterLoop(int number)
        {
            yield return new WaitForSeconds(3.0f);
            while (number != 0)
            {
                GenerateMonster(Random.Range(0,5));
                yield return new WaitForSeconds(1.0f);
                number--;
            }
        }
        
        void GenerateMonster(int line)
        {
            
            Monster monster = objectPooler.Pool(monsterParent).GetComponent<Monster>();
            monster.Init(currentMonsterData, 0);
            monster.SetLine(line);
            monster.SetMove();
            monsters.Add(monster.GetComponent<Monster>());

            monsterNumber++;
            OnMonsterNumberChangeAction?.Invoke(monsterNumber);
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
    }

}
