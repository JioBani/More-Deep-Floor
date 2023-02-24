using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas //.
{
    [CreateAssetMenu(
        fileName = "Trait_Greedy",
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Greedy",
    //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
    order = int.MaxValue)]

    public class StateData_Trait_Greedy : TraitStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            return new TraitState_Greedy(this,traitData ,defender);
        }
    }

    public class TraitState_Greedy : DefenderState
    {
        private Trait_Greedy traitData;
        private MarketManager marketManager;
        private float[] percents;

        public TraitState_Greedy(DefenderStateData _stateData,TraitData _traitData ,Defender _defender) : base(_stateData, _defender)
        {
            traitData = _traitData as Trait_Greedy;
            percents = traitData.Percent;
        }

        public override void OnGenerated()
        {
            marketManager = ReferenceManager.instance.marketManager;
        }
        
        public override void ActiveAction(Defender caster, Monster target)
        {
            int level = traitController.character.synergyLevel;
            if (level < 0) return;
            
            if (Random.Range(1, 101) <= percents[level])
            {
                marketManager.GoldChange(1 , "탐욕적인");
            }
        }
    }
}