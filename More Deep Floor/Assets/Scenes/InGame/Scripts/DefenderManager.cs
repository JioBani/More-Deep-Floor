using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Data.Defender;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{

    public class DefenderStatusModifier
    {
        public int damage = 0;
        public float attackSpeed = 0;
    }

    
    public class DefenderManager : MonoBehaviour
    {
        private MarketManager marketManager;
        [SerializeField] private ObjectPooler defenderPooler;
        [SerializeField] private DefenderTableOriginalData defenderTableOriginalData;
        
        private List<Defender> defenders = new List<Defender>();
        public List<Defender> battleDefenders = new List<Defender>();
        private int[] defenderIndex = {0,0,0,0,0,0};

        public DefenderTableData defenderTableData;
        public int battleDefender = 0;
        public int battleDefenderLimit = 0;

        public DefenderStatusModifier defenderStatusModifier = new DefenderStatusModifier();
        
        public delegate void OnBattleLimitInitEventHandler(int limit);
        public delegate void OnBattleFieldDefenderChangeEventHandler(int limit , List<Defender> defenders);
        public delegate void OnDefenderInOutEventHandler(Defender defender);
        public delegate void OnDefenderPlaceChangeEventHandler(Defender defender);
        
        public OnBattleLimitInitEventHandler OnBattleLimitInitAction;
        public OnBattleFieldDefenderChangeEventHandler OnBattleFieldDefenderChangeAction;
        public OnDefenderInOutEventHandler OnDefenderEnterBattleFieldAction;
        public OnDefenderInOutEventHandler OnDefenderExitBattleFieldAction;
        public OnDefenderPlaceChangeEventHandler OnDefenderPlaceChangeAction;
        
        //#. 이벤트 함수
        private void Awake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            defenderTableData = new DefenderTableData(defenderTableOriginalData);

            marketManager.onInitLevelAction += OnInitLevel;
            marketManager.OnLevelUpAction += OnLevelUp;
        }

        #region #. 커스텀 이벤트 
        void OnInitLevel(int level)
        {
            battleDefenderLimit = level;
            OnBattleLimitInitAction?.Invoke(battleDefenderLimit);
        }

        void OnLevelUp(int level)
        {
            battleDefenderLimit = level;
            OnBattleFieldDefenderChangeAction?.Invoke(battleDefenderLimit , battleDefenders);
        }
        
        public void OnDefenderEnterBattleField(Defender defender)
        {
            battleDefenders.Add(defender);
            
            OnDefenderEnterBattleFieldAction?.Invoke(defender);
            OnBattleFieldDefenderChangeAction?.Invoke(battleDefenderLimit, battleDefenders);
            Debug.Log($"[DefenderManager.OnDefenderEnterBattleField()] 전투석 입장 : {defender.status.defenderData.spawnId}");
        }

        public void OnDefenderEnterWaitingRoom(Defender defender)
        {
           
        }

        public void OnDefenderExitBattleField(Defender defender)
        {
            RemoveDefenderAtBattleList(defender);
            
            OnDefenderExitBattleFieldAction?.Invoke(defender);
            OnBattleFieldDefenderChangeAction?.Invoke(battleDefenderLimit, battleDefenders);
            Debug.Log($"[DefenderManager.OnDefenderEnterBattleField()] 전투석 퇴장 : {defender.status.defenderData.spawnId}");
            
        }

        public void OnDefenderExitWaitingRoom(Defender defender)
        {
            
        }
        
        #endregion

        #region #. 수호자 소환
        
        public void SpawnDefenderById(DefenderId id , Tile tile)
        {
            Debug.Log($"[DefenderManager.SpawnDefenderById()] id : {id}");
            SpawnDefenderAtWaitingRoom(new DefenderData(defenderTableData.FindDefenderDataById(id) , defenderStatusModifier), tile);
        }

        Defender SpawnDefenderAtWaitingRoom(DefenderData defenderData , Tile tile)
        {
            Defender defender = defenderPooler.Pool().GetComponent<Defender>();
            //tile.SetDefender(defender);
            defender.GetComponent<Placer>().Init();
            defenders.Add(defender);
            defender.SpawnAtWaitingRoom(tile);
            defender.Init(defenderData);
            SetDefenderSpawnId(defender);
            
            defender.OnSpawn();
            return defender;
        }
        #endregion

        #region #. 수호자 합성

        public void CheckMerge(Defender defender)
        {
            Defender[] mergeDefenders = new Defender[3];
            DefenderId id = defender.status.defenderData.id;
            int level = defender.status.level;
            
            if (level >= 3) return;

            mergeDefenders[0] = null;
            mergeDefenders[1] = null;
            mergeDefenders[2] = defender;
            
            for (int i = 0; i < defenders.Count; i++)
            {
                if (defenders[i].status.defenderData.id == id && 
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
                        Debug.Log($"[DefenderManager.CheckMerge()] 합성 주인 : {mergeDefenders[0].status.defenderData.spawnId}");
                        mergeDefenders[1] = defenders[i];
                        mergeDefenders[0].Merge();
                        Debug.Log($"[DefenderManager.CheckMerge()] 합성 재료 : " +
                                  $"{mergeDefenders[1].status.defenderData.spawnId} , " +
                                  $"{mergeDefenders[2].status.defenderData.spawnId}");
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
                str += target.status.defenderData.spawnId + " , ";
            }
            
            Debug.Log($"[DefenderManager.RemoveDefender()] 수호자 삭제 : {defender.status.defenderData.spawnId}");
            Debug.Log($"[DefenderManager.RemoveDefender()] 현재 전투석 : {str}");
        }
        

        #endregion

        #region #. 기타

        //#. 수호자 id 생성
        public void SetDefenderSpawnId(Defender defender)
        {
            int cost = defender.status.defenderData.cost;
            
            defender.status.defenderData.spawnId = defender.status.defenderData.id + "_" + defenderIndex[cost];
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
            Debug.Log("SetDefenderPlaceChange");
        }

        public void AddDamage(int _damage)
        {
            defenderStatusModifier.damage += _damage;
        }
        
        public void AddAttackSpeed(int _attackSpeed)
        {
            defenderStatusModifier.attackSpeed += _attackSpeed;
            Debug.Log("[DefenderManager.AddAttackSpeed()] AddAttackSpeed");
        }

        #endregion
        
    }
}


