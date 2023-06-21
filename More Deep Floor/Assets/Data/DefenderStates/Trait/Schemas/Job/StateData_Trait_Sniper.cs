using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits
{
    [CreateAssetMenu(
        fileName = "Sniper",
        menuName = "Scriptable Object/Defender State Data/Trait/Job/Sniper",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
        order = int.MaxValue)]

    public class StateData_Trait_Sniper : TraitStateData
    {
        public float[] percents;
        
        public override DefenderState GetState(Defender defender)
        {
            Debug.LogWarning("[StateData_Trait_Sniper] 정상적이지 않은 방법으로 생성됨");
            return new TraitState_Sniper(this,null , defender);
        }
    }

    public class TraitState_Sniper : TraitState
    {
        private RuntimeTrait_Sniper runtimeTraitData;
        
        private float[] percents;
        
        public TraitState_Sniper(DefenderStateData _stateData, RuntimeTrait_Sniper _runtimeTraitData, Defender _defender) :
            base(_stateData, null,_defender)
        {
            runtimeTraitData = _runtimeTraitData;
            percents = runtimeTraitData.percent;
        }
        
        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            if (!target.gameObject.activeSelf) return;
            int level = traitController.job.synergyLevel;
            float maxDamagePercent = percents[level];
            float distance = Vector2.Distance(caster.transform.position, target.transform.position);
            float value = distance / 5f;
            float addDamage = maxDamagePercent * value * 0.01f * damage;
            target.SetHitFinal(addDamage * damage, caster);
        }
    }
}


