using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Trait_Challenging",
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Challenging",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Challenging", 
        order = int.MaxValue)]

    public class StateData_Trait_Challenging : TraitStateData
    {
        public float[] maxHpPer;
        
        public override DefenderState GetState(Defender defender)
        {
            Debug.LogWarning("[StateData_Trait_Challenging] 정상적이지 않은 방법으로 생성됨");
            return new TraitState_Challenging(this, null,  defender);
        }
    }

    public class TraitState_Challenging : TraitState
    {
        private RuntimeTrait_Challenging runtimeTraitData;
        private Trait_Challenging traitData;
        private float[] maxHpPer;
        
        public TraitState_Challenging(DefenderStateData _stateData, RuntimeTrait_Challenging _runtimeTraitData , Defender _defender) : 
            base(_stateData,_runtimeTraitData ,_defender)
        {
            runtimeTraitData = _runtimeTraitData;
            traitData = runtimeTraitData.traitData as Trait_Challenging;
            maxHpPer = runtimeTraitData.currentMaxHpPer;
        }
        
        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            int level = traitController.GetTraitInfo(traitData.TraitType).synergyLevel;
            
            TimerManager.instance.LateAction(0.1f , () =>
            {
                if (target.gameObject.activeSelf)
                {
                    target.SetHitFinal(target.status.maxHp.currentValue * maxHpPer[level] * 0.01f, caster);
                }
            });
        }
    }
}
