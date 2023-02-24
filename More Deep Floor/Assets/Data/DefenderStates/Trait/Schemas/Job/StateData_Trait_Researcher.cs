using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
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
            return new TraitState_Researcher(this, traitData,defender);
        }
    }

    public class TraitState_Researcher : DefenderState
    {
        private int[] percents;
        
        public TraitState_Researcher(DefenderStateData _stateData, TraitData traitData, Defender _defender) : base(_stateData, _defender)
        {
            percents = ((Trait_Researcher)traitData).Percent;
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

