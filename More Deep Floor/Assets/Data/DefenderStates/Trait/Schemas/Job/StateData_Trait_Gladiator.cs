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
            return new TraitState_Gladiator(this, traitData, defender);
        }
    }
    
    public class TraitState_Gladiator : DefenderState
    {
        private Trait_Gladiator traitData;
        
        public TraitState_Gladiator(DefenderStateData _stateData, TraitData _traitData, Defender _defender) : base(_stateData, _defender)
        {
            traitData = ((Trait_Gladiator)_traitData);
        }
        
        public override void ActiveAction(Defender caster, Monster target)
        {

            Effect_Gladiator state = new Effect_Gladiator(traitData.Effect , caster,traitData.AttackSpeedUp);
            stateController.AddState(state);
        }
    }
}


