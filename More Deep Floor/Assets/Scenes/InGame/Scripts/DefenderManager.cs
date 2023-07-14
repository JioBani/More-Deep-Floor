using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{

    /*public class DefenderStatusModifier
    /*public class DefenderStatusModifier
    {
        public int damage = 0;
        public float attackSpeed = 0;
    }*/

    public class DefenderDataTable
    {
        List<DefenderData> defenderDatas;
        public Dictionary<DefenderId, DefenderData> defenderSortById { private set; get; }
        public List<DefenderData>[] defenderSortByCost { private set; get; }

        public DefenderDataTable(DefenderTableOriginalData defenderTableOriginalData)
        {
            defenderDatas = new List<DefenderData>();
            defenderSortById = new Dictionary<DefenderId, DefenderData>();
            defenderSortByCost = new List<DefenderData>[6];
            
            for (var i = 0; i < defenderSortByCost.Length; i++)
            {
                defenderSortByCost[i] = new List<DefenderData>();
            }
            
            for (var i = 0; i < defenderTableOriginalData.defenders.Count; i++)
            {
                DefenderData defenderData = new DefenderData(defenderTableOriginalData.defenders[i]);
                defenderDatas.Add(defenderData);
                defenderSortById.Add(defenderData.id, defenderData);
                defenderSortByCost[defenderData.cost].Add(defenderData);
            }
        }

        public DefenderData FindById(DefenderId id)
        {
            if (defenderSortById.TryGetValue(id, out var defender))
            {
                return defender;
            }
            else
            {
                Debug.LogWarning($"[DefenderDataTable.FindById()] Defender를 찾을수 없음 : {id}");
                return null;
            }
        }
        
        public void ModifyDefenderData(DefenderDataModifier modifier)
        {
            for (var i = 0; i < defenderDatas.Count; i++)
            {
                defenderDatas[i].ModifyData(modifier);
            }
        }
    }

    public class DefenderManager : MonoBehaviour
    {
        private MarketManager marketManager;
        [SerializeField] private ObjectPooler defenderPooler;
        [SerializeField] private DefenderTableOriginalData defenderTableOriginalData;
        
        private List<Defender> defenders = new List<Defender>();
        public List<Defender> battleDefenders = new List<Defender>();
        private int[] defenderIndex = {0,0,0,0,0,0};

        //public DefenderTableData defenderTableData;
        public DefenderDataTable defenderDataTable;
        public int battleDefender = 0;
        public int battleDefenderLimit = 0;

        public DefenderDataModifier defenderDataModifier = new DefenderDataModifier();
        
        public delegate void OnBattleLimitInitEventHandler(int limit);
        public delegate void OnBattleFieldDefenderChangeEventHandler(int limit , List<Defender> defenders , Defender add, Defender remove);
        public delegate void OnDefenderInOutEventHandler(List<Defender> defenders , Defender defender);
        public delegate void OnDefenderPlaceChangeEventHandler(Defender defender);
        public delegate void OnDefenderSpawnEventHandler(Defender defender);
        
        public OnBattleLimitInitEventHandler OnBattleLimitInitAction;
        public OnBattleFieldDefenderChangeEventHandler OnBattleFieldDefenderChangeAction;
        public OnDefenderInOutEventHandler OnDefenderEnterBattleFieldAction;
        public OnDefenderInOutEventHandler OnDefenderExitBattleFieldAction;
        public OnDefenderPlaceChangeEventHandler OnDefenderPlaceChangeAction;
        public OnDefenderSpawnEventHandler OnDefenderSpawnAction;
        
        //#. 이벤트 함수
        private void Awake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            //defenderTableData = new DefenderTableData(defenderTableOriginalData);
            defenderDataTable = new DefenderDataTable(defenderTableOriginalData);

            marketManager.onInitLevelAction += OnInitLevel;
            marketManager.OnLevelUpAction += OnLevelUp;
        }
        
        //#. 수호자 검색
        /*public List<Defender> FindDefendersByTrait(TraitId traitId)
        {
            return defenders.Filter((defender) => (defender.defenderData.character.Id == traitId) ||
                                                  (defender.defenderData.job.Id == traitId));
            
        }*/

        #region #. 커스텀 이벤트 
        void OnInitLevel(int level)
        {
            battleDefenderLimit = level;
            OnBattleLimitInitAction?.Invoke(battleDefenderLimit);
        }

        void OnLevelUp(int level)
        {
            battleDefenderLimit = level;
            OnBattleFieldDefenderChangeAction?.Invoke(battleDefenderLimit , battleDefenders , null , null);
        }
        
        public void OnDefenderEnterBattleField(Defender defender)
        {
            battleDefenders.Add(defender);
            
            OnDefenderEnterBattleFieldAction?.Invoke( defenders , defender);
            OnBattleFieldDefenderChangeAction?.Invoke(battleDefenderLimit, battleDefenders , add:defender , null);
            Debug.Log($"[DefenderManager.OnDefenderEnterBattleField()] 전투석 입장 : {defender.defenderData.spawnId}");
        }

        public void OnDefenderEnterWaitingRoom(Defender defender)
        {
           
        }

        public void OnDefenderExitBattleField(Defender defender)
        {
            RemoveDefenderAtBattleList(defender);
            
            OnDefenderExitBattleFieldAction?.Invoke(defenders, defender);
            OnBattleFieldDefenderChangeAction?.Invoke(battleDefenderLimit, battleDefenders , add:null , remove: defender);
            Debug.Log($"[DefenderManager.OnDefenderEnterBattleField()] 전투석 퇴장 : {defender.defenderData.spawnId}");
            
        }

        public void OnDefenderExitWaitingRoom(Defender defender)
        {
            
        }
        
        #endregion

        #region #. 수호자 소환
        
        public void SpawnDefenderById(DefenderId id , Tile tile)
        {
            Debug.Log($"[DefenderManager.SpawnDefenderById()] id : {id}");
            
            /*Defender defender = SpawnDefenderAtWaitingRoom(
                new DefenderData(defenderTableData.FindDefenderDataById(id) ,
                    defenderDataModifier), tile);*/

            Defender defender = SpawnDefenderAtWaitingRoom(defenderDataTable.FindById(id), tile);

            OnDefenderSpawnAction?.Invoke(defender);
        }

        Defender SpawnDefenderAtWaitingRoom(DefenderData defenderData , Tile tile)
        {
            Defender defender = defenderPooler.Pool().GetComponent<Defender>();
            //tile.SetDefender(defender);
            defender.GetComponent<Placer>().Init();
            defenders.Add(defender);
            defender.Init(defenderData , 1);
            defender.SpawnAtWaitingRoom(tile);
            SetDefenderSpawnId(defender);
            
            //defender.OnSpawn();
            return defender;
        }
        #endregion

        #region #. 수호자 합성

        public void CheckMerge(Defender defender)
        {
            Defender[] mergeDefenders = new Defender[3];
            DefenderId id = defender.defenderData.id;
            
            CustomLogger.LogWarning($"[Defender.CheckMerge()] {defender.status} ");
            CustomLogger.LogWarning($"[Defender.CheckMerge()] {defender.status.level} ");
            
            int level = defender.status.level;
            
            if (level >= 3) return;

            mergeDefenders[0] = null;
            mergeDefenders[1] = null;
            mergeDefenders[2] = defender;
            
            for (int i = 0; i < defenders.Count; i++)
            {
                if (defenders[i].defenderData.id == id && 
                    defenders[i].status.level == level && 
                    defenders[i] != defender
                   )
                {
                    if (mergeDefenders[0] == null)
                    {
                        mergeDefenders[0] = defenders[i];
                    }
                    else
                    {
                        Debug.Log($"[DefenderManager.CheckMerge()] 합성 주인 : {mergeDefenders[0].defenderData.spawnId}");
                        mergeDefenders[1] = defenders[i];
                        mergeDefenders[0].Merge();
                        Debug.Log($"[DefenderManager.CheckMerge()] 합성 재료 : " +
                                  $"{mergeDefenders[1].defenderData.spawnId} , " +
                                  $"{mergeDefenders[2].defenderData.spawnId}");
                        mergeDefenders[1].BeMerged(mergeDefenders[0]);
                        mergeDefenders[2].BeMerged(mergeDefenders[0]);
                        CheckMerge(mergeDefenders[0]);
                        return;
                    }
                }
            }
        }
        

        #endregion

        #region #. 수호자 목록
        void RemoveDefenderAtBattleList(Defender defender)
        {
            battleDefenders.Remove(defender);
        }

        public void OnDefenderOff(Defender defender)
        {
            RemoveDefender(defender);
        }

        void RemoveDefender(Defender defender)
        {
            RemoveDefenderAtBattleList(defender);
            defenders.Remove(defender);
            string str = "";
            
            foreach (var target in battleDefenders)
            {
                str += target.defenderData.spawnId + " , ";
            }
            
            Debug.Log($"[DefenderManager.RemoveDefender()] 수호자 삭제 : {defender.defenderData.spawnId}");
            Debug.Log($"[DefenderManager.RemoveDefender()] 현재 전투석 : {str}");
        }
        

        #endregion

        #region #. 기타

        //#. 수호자 id 생성
        public void SetDefenderSpawnId(Defender defender)
        {
            int cost = defender.defenderData.cost;
            
            defender.defenderData.spawnId = defender.defenderData.id + "_" + defenderIndex[cost];
            defenderIndex[cost]++;
        }

        //#. 수호자 제한 확인
        public bool CheckBattleDefenderLimit()
        {
            if (battleDefenderLimit > battleDefenders.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //#. 주변 수호자 가져오기
        public List<Defender> GetNearDefenders(Defender standard, int nums)
        {
            List<Defender> sortList = battleDefenders.OrderBy((defender) =>
            {
                return Vector2.Distance(standard.transform.position, defender.transform.position);

            }).ToList();

            sortList.Remove(standard);

            if (sortList.Count < nums)
            {
                nums = sortList.Count;
            }

            return sortList.GetRange(0, nums);
        }

        public void SetDefenderPlaceChange(Defender defender)
        {
            OnDefenderPlaceChangeAction?.Invoke(defender);
        }

        public void AddDamage(int _damage)
        {
            defenderDataModifier.damage += _damage;
        }
        
        public void AddAttackSpeed(int _attackSpeed)
        {
            defenderDataModifier.attackSpeed += _attackSpeed;
        }

        public void ModifyDefenderData(DefenderDataModifier modifier)
        {
            defenderDataTable.ModifyDefenderData(modifier);
            
            for (var i = 0; i < battleDefenders.Count; i++)
            {
                battleDefenders[i].status.RefreshStatus();
            }
        }

        public void ModifyDefenderDataTest()
        {
            DefenderDataModifier modifier = new DefenderDataModifier
            {
                damage = 10,
                attackSpeed = 0
            };

            ModifyDefenderData(modifier);
        }

        #endregion
        
    }
}


