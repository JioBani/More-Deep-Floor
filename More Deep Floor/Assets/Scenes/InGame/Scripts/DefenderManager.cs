using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Corps;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Traits.Corps;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class DefenderManager : InGameBehaviour
    {
        //#. 참조
        private MarketManager marketManager;
        [SerializeField] private ObjectPooler defenderPooler;
        [SerializeField] private CorpsDataBase corpsDataBase;
        [SerializeField] private TextMeshProUGUI corpsText;
        public DefenderDataTable defenderDataTable { private set; get; }

        //#. 프로퍼티
        
        //#. 변수
        private GameDataSaver gameDataSaver;
        private List<Defender> defenders = new List<Defender>();
        public List<Defender> battleDefenders = new List<Defender>();
        private int[] defenderIndex = {0,0,0,0,0,0};
        public int battleDefender = 0;
        public int battleDefenderLimit = 0;
        public DefenderDataModifier defenderDataModifier = new DefenderDataModifier();

        private Dictionary<CorpsId, CorpsData> corpsDic;

        //#. 이벤트
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
        
        #region #. 라이프 사이클

        protected override void BehaviorAwake()
        {
            marketManager = ReferenceManager.instance.marketManager;

            gameDataSaver = new GameDataSaver();

            marketManager.onInitLevelAction += OnInitLevel;
            marketManager.OnLevelUpAction += OnLevelUp;
            
            corpsDic = new Dictionary<CorpsId, CorpsData>();
            
            for (var i = 0; i < corpsDataBase.CorpsDatas.Count; i++)
            {
                corpsDic[corpsDataBase.CorpsDatas[i].CorpsId] = corpsDataBase.CorpsDatas[i];
            }
        }
        
        public void SetData()
        {
            var members = new List<DefenderOriginalData>();
            List<CorpsData> corpsDatas = new List<CorpsData>();

            if (!gameDataSaver.LoadFormationData(out var corps))
            {
                CustomLogger.LogWarning("[MarketManager.OnSceneLoad()] 편성 데이터를 불러올 수 없음");
            }
           
            foreach (var corpsId in corps)
            {
                CustomLogger.Log($"{corpsId}");
                corpsDatas.Add(corpsDic[corpsId]);
                members.AddRange(corpsDic[corpsId].Members);
            }

            defenderDataTable = new DefenderDataTable(members);
            
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var corpsData in corpsDatas)
            {
                stringBuilder.Append(corpsData.CommanderName);
            }

            corpsText.text = stringBuilder.ToString();
        }

        protected override void OnRoundStart(int round)
        {
            SetBattleState();
        }

        #endregion

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

        public void SetBattleState()
        {
            for (var i = 0; i < battleDefenders.Count; i++)
            {
                battleDefenders[i].entityBehavior.SetBehaviorState(EntityBehaviorState.Battle);
            }
        }

        public void SetRoundWaitState()
        {
            for (var i = 0; i < battleDefenders.Count; i++)
            {
                battleDefenders[i].entityBehavior.SetBehaviorState(EntityBehaviorState.RoundWait);
            }
        }

        /// <summary>
        /// 전투석에 있는 Defender들의 위치를 원래 배치 위치로 되돌리기
        /// </summary>
        public void RevertDefenderOriginalPlacement()
        {
            for (var i = 0; i < battleDefenders.Count; i++)
            {
                battleDefenders[i].MoveToPlacerTile();
            }
        }

        #endregion
        
    }
}


