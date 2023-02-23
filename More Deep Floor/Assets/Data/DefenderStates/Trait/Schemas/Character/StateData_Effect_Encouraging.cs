using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Effect_Encouraging",
        //menuName = "Scriptable Object/Defender State Data/Trait/Job/"Effect_Encouraging,
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Effect_Encouraging", 
    order = int.MaxValue)]

    public class StateData_Effect_Encouraging : DefenderStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            return new Effect_Encouraging(this, defender);
        }
    }

    public class Effect_Encouraging : DefenderState
    {
        private StateData_Effect_Encouraging stateDataSpecific;
        private StatusBuff statsBuff;
        
        public Effect_Encouraging(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
            stateDataSpecific = ((StateData_Effect_Encouraging)_stateData);
        }
        
        public override void OnAction(Defender caster, Monster target)
        {
            statsBuff = defender.status.AddAttackSpeedBuff(defender.status.attackSpeed.originalValue ,"Effect_Encouraging" );
        }

        public override void OffAction(Defender caster, Monster target)
        {
            defender.status.RemoveAttackSpeedBuff(statsBuff);
        }
    }
}
