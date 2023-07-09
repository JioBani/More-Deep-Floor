using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
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
            Debug.LogWarning("[StateData_Trait_Greedy] 올바르지 않는 방법으로 생성됨");
            return new TraitState_Greedy(this, null,defender);
        }
    }

    public class TraitState_Greedy : TraitState
    {
        private RuntimeTrait_Greedy runtimeTraitData;
        private MarketManager marketManager;
        private float[] percents;

        public TraitState_Greedy(DefenderStateData _stateData,RuntimeTrait_Greedy _runtimeTraitData ,Defender _defender) 
            : base(_stateData,_runtimeTraitData ,_defender)
        {
            runtimeTraitData = _runtimeTraitData;
            percents = runtimeTraitData.currentPercent;
        }

        public override void OnGenerated()
        {
            marketManager = ReferenceManager.instance.marketManager;
        }
        
        /*public override void ActiveAction(Defender caster, Monster target)
        {
            int level = traitController.character.synergyLevel;
            if (level < 0) return;
            
            if (Random.Range(1, 101) <= percents[level])
            {
                marketManager.GoldChange(1 , "탐욕적인");
            }
        }*/
    }
}