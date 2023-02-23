using System.Collections;
using System.Collections.Generic;
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

    public class StateData_Trait_Circus : DefenderStateData
    {
        public float[] targetNums;
        
        public override DefenderState GetState(Defender defender)
        {
            return new Trait_Circus(this, defender);
        }
    }

    public class Trait_Circus : DefenderState
    {
        private StateData_Trait_Circus stateDataSpecific;
        private float[] targetNums;
        
        public Trait_Circus(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
            stateDataSpecific = ((StateData_Trait_Circus)_stateData);
            targetNums = stateDataSpecific.targetNums;
        }
        
        public override void OnBeforeOriginalAttackAction(Monster target, DefenderStateId stateId)
        {
            //int nums = (int)targetNums[traitController.job.synergyLevel];
            int nums = 3;
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
