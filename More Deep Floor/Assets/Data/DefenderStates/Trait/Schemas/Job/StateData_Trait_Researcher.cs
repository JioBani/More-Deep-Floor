using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits
{
    [CreateAssetMenu(
        fileName = "Researcher",
        menuName = "Scriptable Object/Defender State Data/Trait/Job/Researcher",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
        order = int.MaxValue)]

    public class StateData_Trait_Researcher : TraitStateData
    {
        public float[] percents;
        
        public override DefenderState GetState(Defender defender)
        {
            Debug.LogWarning("[StateData_Trait_Researcher] 정상적이지 않은 방법으로 생성됨");
            return new TraitState_Researcher(this, null,defender);
        }
    }

    public class TraitState_Researcher : TraitState
    {
        public RuntimeTrait_Researcher runtimeTraitData;
        private int[] percents;
        
        public TraitState_Researcher(DefenderStateData _stateData, RuntimeTrait_Researcher _runtimeTraitData, Defender _defender) 
            : base(_stateData, _runtimeTraitData,_defender)
        {
            runtimeTraitData = _runtimeTraitData;
            percents = runtimeTraitData.currentPercent;
        }
        
        public override void OnUseSkillAction(Defender caster, Monster target, bool isFinal)
        {
            if(isFinal) return;
            
            int level = traitController.job.synergyLevel;
            int trigger = Random.Range(1, 101);

            if (trigger <= percents[level])
            {
                caster.status.SetManaGain(false);
                TimerManager.instance.LateAction(0.75f , () =>
                {
                    caster.status.SetManaGain(true);
                    caster.UseSkillFinal();
                });
            }
        }
        
    }
}

