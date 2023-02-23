using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
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

    public class StateData_Trait_Researcher : DefenderStateData
    {
        public float[] percents;
        
        public override DefenderState GetState(Defender defender)
        {
            return new Trait_Researcher(this, defender);
        }
    }

    public class Trait_Researcher : DefenderState
    {
        private float[] percents;
        
        public Trait_Researcher(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
            percents = ((StateData_Trait_Researcher)_stateData).percents;
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

