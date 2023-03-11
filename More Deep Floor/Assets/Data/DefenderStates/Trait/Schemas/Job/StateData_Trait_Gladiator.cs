using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas
{
    [CreateAssetMenu(
        fileName = "Gladiator", 
        menuName = "Scriptable Object/Defender State Data/Trait/Job/Gladiator", 
        order = int.MaxValue)]
    
    public class StateData_Trait_Gladiator : TraitStateData
    {
        public StateData_Effect_Gladiator effectStateData;
        public float[] attackSpeed;
        
        public override DefenderState GetState(Defender defender)
        {
            Debug.LogWarning("[StateData_Trait_Gladiator] 정상적이지 않은 방법으로 생성됨");
            return new TraitState_Gladiator(this, null, defender);
        }
    }
    
    public class TraitState_Gladiator : TraitState
    {
        private RuntimeTrait_Gladiator runtimeTraitData;
        private Trait_Gladiator traitGladiator;
        
        public TraitState_Gladiator(DefenderStateData _stateData, RuntimeTrait_Gladiator _runTimeTraitData, Defender _defender) 
            : base(_stateData,_runTimeTraitData ,  _defender)
        {
            runtimeTraitData = _runTimeTraitData;
            traitGladiator = _runTimeTraitData.traitData as Trait_Gladiator;
        }
        
        public override void ActiveAction(Defender caster, Monster target)
        {
            Effect_Gladiator state = new Effect_Gladiator(traitGladiator.Effect , caster, runtimeTraitData);
            stateController.AddState(state);
        }
    }
}


