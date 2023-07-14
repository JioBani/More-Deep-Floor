using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.EntityStates.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Traits;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    [Serializable]
    public class TraitAdapter : MonoBehaviour
    {
        private TraitManager traitManager;
        private TraitData[] traitData;
        private TraitState[] traitState;
        private StateController stateController;
        private EntityStateGenerator entityStateGenerator;
        private Defender defender;
        //public ActiveTraitInfo activeTraitInfo { get; private set; }

        public ActiveTraitInfo[] activeTraitInfos { get; private set; }

        public bool[] isSynergyActive { get; private set; } = { false, false };


        /*public delegate void OnSynergyChangedEventHandler();
        public OnSynergyChangedEventHandler OnSynergyChangedAction;*/

        private void Awake()
        {
            traitManager = ReferenceManager.instance.traitManager;
            entityStateGenerator = ReferenceManager.instance.entityStateGenerator;
            stateController = GetComponent<StateController>();
            defender = GetComponent<Defender>();
            defender.OnSpawnAction += OnDefenderSpawn;
        }

        public void ActiveSynergy(ActiveTraitInfo activeTraitInfo , TraitType traitType)
        {
            int index = (int)traitType;
            activeTraitInfos[index] = activeTraitInfo;
            //this.activeTraitInfo = activeTraitInfo;
            traitState[index] =  
                stateController.AddState(entityStateGenerator.GenerateAsTraitState(traitData[(int)traitType] , defender)) as TraitState;
            isSynergyActive[index] = true;
        }

        public void DeactiveSynergy(TraitType traitType)
        {
            int index = (int)traitType;
            stateController.RemoveState(traitState[index]);
            traitState[index] = null;
            isSynergyActive[index] = false;
        }

        public void OnDefenderSpawn(Entity self)
        {
            defender = self as Defender;
            traitData = new TraitData[2];
            traitData[(int)TraitType.Corps] = defender.defenderData.originalData.CorpsData;
            traitData[(int)TraitType.Personality] = defender.defenderData.originalData.PersonalityData;
            activeTraitInfos = new ActiveTraitInfo[2];
            traitState = new TraitState[2];
            isSynergyActive[0] = false;
            isSynergyActive[1] = false;
        }

        public void OnSynergyLevelChange(int synergyLevel, TraitType traitType)
        {
            //TODO state.Modify(synergyLevel)
            
            traitState[(int)traitType].OnSynergyLevelChange(synergyLevel);
        }
    }
}


