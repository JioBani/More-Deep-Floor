using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Corps;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Traits;
using LNK.MoreDeepFloor.Data.Troops;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    /*public class TraitManager : MonoBehaviour
    {
        public Dictionary<TraitId, BattleFieldTraitInfo> currentTraits = new Dictionary<TraitId, BattleFieldTraitInfo>();
        public Dictionary<TraitId, List<DefenderOriginalData>> defenderSortByTrait;

        public TraitDataTable traitDataTable;
        [SerializeField] private DefenderTableOriginalData defenderTableData;
        
        private DefenderManager defenderManager;

        public delegate void OnTraitChangeEventHandler(Dictionary<TraitId, BattleFieldTraitInfo> traits);

        public OnTraitChangeEventHandler OnTraitChangeAction;
        
        private void Awake()
        {
            defenderSortByTrait = new Dictionary<TraitId, List<DefenderOriginalData>>();
            
            foreach (var traitData in traitDataTable.TraitDataList)
            {
                currentTraits.Add(traitData.Id , new BattleFieldTraitInfo(traitData));
            }
            
            for (var i = 0; i < defenderTableData.Defenders.Count; i++)
            {
                if (defenderSortByTrait.TryGetValue(defenderTableData.Defenders[i].Job.Id, out var defenderList))
                {
                    defenderList.Add(defenderTableData.Defenders[i]);
                }
                else
                {
                    List<DefenderOriginalData> list = new List<DefenderOriginalData>();
                    list.Add(defenderTableData.Defenders[i]);
                    defenderSortByTrait.Add(defenderTableData.Defenders[i].Job.Id , list);
                }
            }
            
            for (var i = 0; i < defenderTableData.Defenders.Count; i++)
            {
                if (defenderSortByTrait.TryGetValue(defenderTableData.Defenders[i].Character.Id, out var defenderList))
                {
                    defenderList.Add(defenderTableData.Defenders[i]);
                }
                else
                {
                    List<DefenderOriginalData> list = new List<DefenderOriginalData>();
                    list.Add(defenderTableData.Defenders[i]);
                    defenderSortByTrait.Add(defenderTableData.Defenders[i].Character.Id , list);
                }
            }
            
            defenderManager = ReferenceManager.instance.defenderManager;
            defenderManager.OnDefenderEnterBattleFieldAction += OnAddDefender;
            defenderManager.OnDefenderExitBattleFieldAction += OnRemoveDefender;
            
            traitDataTable.SetRunTimeTraitData();
            
        }


        /// <summary>
        /// 수호자가 전투석에 추가될때 호출
        /// </summary>
        /// <param name="defender">수호자</param>
        void OnAddDefender(Defender defender)
        {
            DefenderData data = defender.defenderData;
            try
            {
                currentTraits[data.job.Id].AddDefender(defender , TraitType.Job);
                currentTraits[data.character.Id].AddDefender(defender , TraitType.Character);
                SetSynergyLevel(defender);
                OnTraitChangeAction?.Invoke(currentTraits);
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
                throw;
            }
        }

        /// <summary>
        /// 수호자가 전투석에서 제거될때 호출
        /// </summary>
        /// <param name="defender">수호자</param>
        void OnRemoveDefender(Defender defender)
        {
            try
            {
                DefenderData data = defender.defenderData;
                currentTraits[data.job.Id].RemoveDefender(defender , TraitType.Job);
                currentTraits[data.character.Id].RemoveDefender(defender , TraitType.Character);
                SetSynergyLevel(defender);
                defender.GetComponent<TraitController>().OnBattleFieldExit();

                OnTraitChangeAction?.Invoke(currentTraits);
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
                throw;
            }
            
      
        }

        void SetSynergyLevel(Defender defender)
        {
            DefenderData data = defender.defenderData;
            for (int i = 0; i < currentTraits[data.job.Id].defenders.Count; i++)
            {
                var traitInfo = currentTraits[data.job.Id];
                traitInfo.defenders[i].GetComponent<TraitController>().SetSynergyLevel(TraitType.Job , traitInfo.synergyLevel);
            }
            
            for (int i = 0; i < currentTraits[data.character.Id].defenders.Count; i++)
            {
                var traitInfo = currentTraits[data.character.Id];
                traitInfo.defenders[i].GetComponent<TraitController>().SetSynergyLevel(TraitType.Character , traitInfo.synergyLevel);
            }
        }
         
        
    }
    */

    public class TraitManager : MonoBehaviour
    {
        private DefenderManager defenderManager;
        [SerializeField] private TraitDataBase traitDataBase;
        //private Stage

        public Dictionary<TraitId, ActiveTraitInfo> activeTraitInfoPool  { get; private set; }
        public Dictionary<TraitId, ActiveTraitInfo> battleFieldTraits { get; private set; }

        public delegate void OnTraitChangeEventHandler(Dictionary<TraitId, ActiveTraitInfo> battleFieldCrops);

        public OnTraitChangeEventHandler OnTraitChangeAction;

        private void Awake()
        {
            defenderManager = ReferenceManager.instance.defenderManager;

            activeTraitInfoPool = new Dictionary<TraitId, ActiveTraitInfo>();
            battleFieldTraits = new Dictionary<TraitId, ActiveTraitInfo>();
            
            foreach (var traitData in traitDataBase.TraitDatas)
            {
                var activeTraitInfo = new ActiveTraitInfo(traitData);
                activeTraitInfo.OnTraitChangeAction += OnTraitChange;
                activeTraitInfoPool[traitData.TraitId] = activeTraitInfo;
            }
            
            //defenderManager.OnBattleFieldDefenderChangeAction += OnBattleFieldDefenderChange;
            
            defenderManager.OnDefenderEnterBattleFieldAction += OnDefenderEnterBattleField;
            defenderManager.OnDefenderExitBattleFieldAction += OnDefenderExitBattleField;
        }

        private void OnDefenderEnterBattleField(List<Defender> defenders,  Defender defender)
        {
            CustomLogger.Log($"[TraitManager.OnDefenderEnterBattleField()] {defender.defenderData.id} 입장");
            AddDefender(defender.defenderData.originalData.CorpsTraitData, defender);
            AddDefender(defender.defenderData.originalData.PersonalityData,  defender);
        }

        
        //#. TraitType이 지휘관인지 성격인지에 따라서 나눔
        private void AddDefender(TraitData traitData , Defender defender)
        {
            CustomLogger.Log($"[TraitManager.AddDefender()] {defender.defenderData.id}");
            CustomLogger.Log($"[TraitManager.AddDefender()] {defender.defenderData.id} : {traitData.TraitId} 추가");
            
            if(battleFieldTraits.TryGetValue(traitData.TraitId , out var result))
            {
                result.AddDefender(defender);
            }
            else
            {
                battleFieldTraits.Add(traitData.TraitId , activeTraitInfoPool[traitData.TraitId]);
                battleFieldTraits[traitData.TraitId].AddDefender(defender);
            }
        }
        
        private void OnDefenderExitBattleField(List<Defender> defenders, Defender defender)
        {

            if (battleFieldTraits[defender.defenderData.originalData.CorpsTraitData.TraitId].Remove(defender))
            {
                battleFieldTraits.Remove(defender.defenderData.originalData.CorpsTraitData.TraitId);
            }

            if (battleFieldTraits[defender.defenderData.originalData.PersonalityData.TraitId].Remove(defender))
            {
                battleFieldTraits.Remove(defender.defenderData.originalData.PersonalityData.TraitId);
            }
            
            OnTraitChangeAction?.Invoke(battleFieldTraits);
        }

        void OnTraitChange(ActiveTraitInfo activeTraitInfo)
        {
            OnTraitChangeAction?.Invoke(battleFieldTraits);
        }


        private void OnBattleFieldDefenderChange(int limit , List<Defender> defenders , Defender add, Defender remove)
        {
            void OnAdd(TraitId traitId)
            {
                if(battleFieldTraits.TryGetValue(traitId , out var result))
                {
                    result.AddDefender(add);
                }
                else
                {
                    battleFieldTraits[traitId] = new ActiveTraitInfo(add.defenderData.corpsData);
                }
            }

            void OnRemove(TraitId traitId)
            {
                battleFieldTraits[traitId].Remove(remove);
            }
            
            
            //TODO 성격도 적용되게
            if (!ReferenceEquals(add, null))
            {
                OnAdd(add.defenderData.originalData.CorpsTraitData.TraitId);
            }
            else
            {
                OnRemove(remove.defenderData.originalData.CorpsTraitData.TraitId);
            }
        }
    }

    
}

