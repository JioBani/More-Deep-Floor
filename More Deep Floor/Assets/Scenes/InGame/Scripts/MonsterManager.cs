using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class MonsterManager : MonoBehaviour
    {
        
        public ObjectPooler objectPooler;
        public GameObject monsterPool;
        private MarketManager _marketManager;
        private StageManager stageManager;
        
        public GameObject monsterParent;
        public List<Monster> monsters = new List<Monster>();
        public int roundMonsterNums;
        public int roundOffMonsterNums;
        private RoundOriginalData _currentRoundOriginal;
        private List<Tile> route;
        
        private MonsterData currentMonsterData;

        private void Awake()
        {
            _marketManager = ReferenceManager.instance.marketManager;
            stageManager = ReferenceManager.instance.stageManager;
        }

        private void Start()
        {
            for (int i = 0; i < monsterPool.transform.childCount; i++)
            {
                Monster monster =  monsterPool.transform.GetChild(i).GetComponent<Monster>();
                monster.SetActions(
                    ()=>OnMonsterDie(monster),
                    ()=>OnMonsterPass(monster),
                    ()=>OnMonsterOff(monster)
                    );
            }
        }

        public void StartStage(List<Tile> _route)
        {
            SetRoute(_route);
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

        void SetRoute(List<Tile> _route)
        {
            route = _route;
        }

        IEnumerator GenerateMonsterLoop(int number)
        {
            yield return new WaitForSeconds(3.0f);
            while (number != 0)
            {
                GenerateMonster();
                yield return new WaitForSeconds(1.0f);
                number--;
            }
        }
        
        void GenerateMonster()
        {
            Monster monster = objectPooler.Pool(monsterParent).GetComponent<Monster>();
            monster.OnDieAction = () => OnMonsterDie(monster);
            monster.OnPassAction = () => OnMonsterPass(monster);
            monster.OnOffAction = () => OnMonsterOff(monster);
            monster.Init(currentMonsterData);
            monster.SetRoute(route);
            monster.SetMove();
            monsters.Add(monster.GetComponent<Monster>());
        }

        void OnMonsterDie(Monster monster)
        {
            _marketManager.GoldChange(monster.status.currentGold);
        }

        void OnMonsterPass(Monster monster)
        {
            
        }
        
        void OnMonsterOff(Monster monster)
        {
            roundOffMonsterNums++;
            monsters.Remove(monster);
            if (roundOffMonsterNums == roundMonsterNums)
            {
                stageManager.SetRoundEnd();
            }
        }
    }

}
