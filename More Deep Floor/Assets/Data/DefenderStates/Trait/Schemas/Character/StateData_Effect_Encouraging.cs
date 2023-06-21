using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
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
        private StatusBuff statsBuff;
        private int percent;

        public Effect_Encouraging(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
           
        }
        
        public override void OnAction(Defender caster, Monster target)
        {
            //float attackSpeed = stateDataSpecific.percents[]
            statsBuff = defender.status.AddAttackSpeedBuff(defender.status.attackSpeed.originalValue * percent * 0.01f,"Effect_Encouraging" );
        }

        public override void OffAction(Defender caster, Monster target)
        {
            defender.status.RemoveAttackSpeedBuff(statsBuff);
        }


        public void SetPercent(int _percent)
        {
            percent = _percent;
        }
    }
}
