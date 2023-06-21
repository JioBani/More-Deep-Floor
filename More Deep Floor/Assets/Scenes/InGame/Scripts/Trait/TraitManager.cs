using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    public class TraitManager : MonoBehaviour
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
            defenderManager.OnDefenderEnterBattleFieldAction += AddDefender;
            defenderManager.OnDefenderExitBattleFieldAction += RemoveDefender;
            
            traitDataTable.Init();
        }

        void AddDefender(Defender defender)
        {
            DefenderData data = defender.status.defenderData;
            currentTraits[data.job.Id].AddDefender(defender , TraitType.Job);
            currentTraits[data.character.Id].AddDefender(defender , TraitType.Character);
            SetSynergyLevel(defender);
            
            OnTraitChangeAction?.Invoke(currentTraits);
        }

        void RemoveDefender(Defender defender)
        {
            DefenderData data = defender.status.defenderData;
            currentTraits[data.job.Id].RemoveDefender(defender , TraitType.Job);
            currentTraits[data.character.Id].RemoveDefender(defender , TraitType.Character);
            SetSynergyLevel(defender);
            defender.GetComponent<TraitController>().OnBattleFieldExit();

            OnTraitChangeAction?.Invoke(currentTraits);
        }

        void SetSynergyLevel(Defender defender)
        {
            DefenderData data = defender.status.defenderData;
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
}

