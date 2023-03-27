using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Circus",
        menuName = "Scriptable Object/Defender State Data/Trait/Job/Circus",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
        order = int.MaxValue)]

    public class StateData_Trait_Circus : TraitStateData
    {
        public float[] targetNums;
        
        public override DefenderState GetState(Defender defender)
        {
            Debug.LogWarning("[StateData_Trait_Circus] 정상적이지 않은 방법으로 생성됨");
            return new TraitState_Circus(this,null ,defender);
        }
    }

    public class TraitState_Circus : TraitState
    {
        private RuntimeTrait_Circus runtimeTraitData;
        private DefenderStateId stateId;
        private int[] targetNums;
        
        public TraitState_Circus(DefenderStateData _stateData, RuntimeTrait_Circus _runtimeTraitData , Defender _defender) :
            base(_stateData, _runtimeTraitData,_defender)
        {
            runtimeTraitData = _runtimeTraitData;
            targetNums = runtimeTraitData.currentTargetNumber;
            stateId = runtimeTraitData.traitData.TraitStateData.Id;
        }

        public override void BeforeCommonAttackAction(Monster target, DefenderStateId from)
        {
            if (from == stateId) return;
            
            int nums = targetNums[traitController.job.synergyLevel];
            Debug.Log($"[TraitState_Circus] 발동 : {nums}");
            List<Monster> monsters = defender.TrySearchTargetsExpectTarget(3);
            if (from == DefenderStateId.Trait_Circus) return;
            
            for (var i = 0; i < monsters.Count; i++)
            {
                if(i >= nums) break;
                defender.CommonAttack(monsters[i] , stateId);
                //defender.SetExtraAttack(monsters[i] , DefenderStateId.Trait_Circus);
            }
        }

        /*public override void OnBeforeOriginalAttackAction(Monster target, DefenderStateId stateId)
        {
            int nums = targetNums[traitController.job.synergyLevel];
            Debug.Log($"[TraitState_Circus] 발동 : {nums}");
            List<Monster> monsters = defender.TrySearchTargetsExpectTarget(3);
            if (stateId == DefenderStateId.Trait_Circus) return;
            
            for (var i = 0; i < monsters.Count; i++)
            {
                if(i >= nums) break;
                defender.SetExtraAttack(monsters[i] , DefenderStateId.Trait_Circus);
            }
        }*/
    }
}
