using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    public class TraitManager : MonoBehaviour
    {
        public Dictionary<TraitId, BattleFieldTraitInfo> currentTraits = new Dictionary<TraitId, BattleFieldTraitInfo>();
        [SerializeField] private TraitDataTable traitDataTable;
        private DefenderManager defenderManager;

        public delegate void OnTraitChangeEventHandler(Dictionary<TraitId, BattleFieldTraitInfo> traits);

        public OnTraitChangeEventHandler OnTraitChangeAciton;
        
        private void Awake()
        {
            foreach (var traitData in traitDataTable.TraitDataList)
            {
                currentTraits.Add(traitData.Id , new BattleFieldTraitInfo(traitData));
            }
            
            defenderManager = ReferenceManager.instance.defenderManager;
            defenderManager.OnDefenderEnterBattleFieldAction += AddDefender;
            defenderManager.OnDefenderExitBattleFieldAction += RemoveDefender;
        }

        void AddDefender(Defender defender)
        {
            DefenderData data = defender.status.defenderData;
            currentTraits[data.job.Id].AddDefender(defender , TraitType.Job);
            currentTraits[data.character.Id].AddDefender(defender , TraitType.Character);
            SetSynergyLevel(defender);
            
            OnTraitChangeAciton?.Invoke(currentTraits);
        }

        void RemoveDefender(Defender defender)
        {
            DefenderData data = defender.status.defenderData;
            currentTraits[data.job.Id].RemoveDefender(defender , TraitType.Job);
            currentTraits[data.character.Id].RemoveDefender(defender , TraitType.Character);
            SetSynergyLevel(defender);
            defender.GetComponent<TraitController>().OnBattleFieldExit();

            OnTraitChangeAciton?.Invoke(currentTraits);
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

