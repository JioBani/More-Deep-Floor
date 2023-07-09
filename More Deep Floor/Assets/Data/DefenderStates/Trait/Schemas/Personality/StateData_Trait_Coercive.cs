using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.ProbabilityChecks;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Trait_Coercive",
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Coercive",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
        order = int.MaxValue)]

    public class StateData_Trait_Coercive : TraitStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            Debug.LogWarning("[StateData_Trait_Coercive] 정상적이지 않은 방법으로 생성됨");
            return new TraitState_Coercive(this, null,  defender);
        }
    }

    public class TraitState_Coercive : TraitState
    {
        private RuntimeTrait_Coercive runtimeTraitData;
        private Trait_Coercive traitData;
        private float[] percents;
        private float[] time;
        
        
        public TraitState_Coercive(DefenderStateData _stateData, RuntimeTrait_Coercive _runtimeTraitData ,Defender _defender) : 
            base(_stateData,_runtimeTraitData, _defender)
        {
            runtimeTraitData = _runtimeTraitData;
            traitData = runtimeTraitData.traitData as Trait_Coercive;
            percents = runtimeTraitData.currentPercent;
            time = runtimeTraitData.currentTime;
        }

        /*public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            int level = traitController.character.synergyLevel;
            if (!ProbabilityCheck.Check((int)percents[level], 100))
                return;
            
            target.SetStun(true);
            
            TimerManager.instance.LateAction(time[level] , () =>
            {
                target.SetStun(false);
            });
        }*/
    }
}
