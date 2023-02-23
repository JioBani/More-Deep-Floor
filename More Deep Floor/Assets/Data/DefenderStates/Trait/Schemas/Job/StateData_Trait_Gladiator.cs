using System.Collections;
using System.Collections.Generic;
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
    
    public class StateData_Trait_Gladiator : DefenderStateData
    {
        public float[] attackSpeed;
        
        public override DefenderState GetState(Defender defender)
        {
            return new Trait_Gladiator(this, defender);
        }
    }
    
    public class Trait_Gladiator : DefenderState
    {
        public Trait_Gladiator(DefenderStateData _stateData,  Defender _defender) : base(_stateData, _defender)
        {
            
        }
        
        public override void ActiveAction(Defender caster, Monster target)
        {
            stateController.AddState(DefenderStateId.Effect_Gladiator);
        }
    }
}


