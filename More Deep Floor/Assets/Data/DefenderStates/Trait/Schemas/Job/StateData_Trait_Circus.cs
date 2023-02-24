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
            return new TraitState_Circus(this,traitData ,defender);
        }
    }

    public class TraitState_Circus : DefenderState
    {
        private StateData_Trait_Circus stateDataSpecific;
        private int[] targetNums;
        
        public TraitState_Circus(DefenderStateData _stateData, TraitData _traitData , Defender _defender) : base(_stateData, _defender)
        {
            targetNums = ((Trait_Circus)_traitData).TargetNumber;
        }
        
        public override void OnBeforeOriginalAttackAction(Monster target, DefenderStateId stateId)
        {
            int nums = targetNums[traitController.job.synergyLevel];
            List<Monster> monsters = defender.TrySearchTargetsExpectTarget(3);
            if (stateId == DefenderStateId.Trait_Circus) return;
            
            for (var i = 0; i < monsters.Count; i++)
            {
                if(i >= nums) break;
                defender.SetExtraAttack(monsters[i] , DefenderStateId.Trait_Circus);
            }
        }
    }
}
