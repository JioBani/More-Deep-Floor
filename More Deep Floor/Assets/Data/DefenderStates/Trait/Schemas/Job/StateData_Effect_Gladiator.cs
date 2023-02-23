using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas
{
    [CreateAssetMenu(
        fileName = "GladiatorEffect", 
        menuName = "Scriptable Object/Defender State Data/Trait/Job/GladiatorEffect", 
        order = int.MaxValue)]
    
    public class StateData_Effect_Gladiator : DefenderStateData
    {
        public float[] attackSpeed;
        
        public override DefenderState GetState(Defender defender)
        {
            return new Effect_Gladiator(this, defender,attackSpeed);
        }
    }
    
    public class Effect_Gladiator : DefenderState
    {
        public float[] attackSpeed;
        
        public Effect_Gladiator(DefenderStateData _stateData, Defender _defender, float[] _attackSpeed) : base(_stateData, _defender)
        {
            attackSpeed = ((StateData_Effect_Gladiator)_stateData).attackSpeed;
        }

        public override void OnAction(Defender caster, Monster target)
        {
            int level = traitController.job.synergyLevel;
            if(level < 0) return;
            
            float value = attackSpeed[level];

            StatusBuff buff = caster.status.ModifyAttackSpeedBuff(
                caster.status.attackSpeed.originalValue * value, 
                "GladiatorEffect");
            
            TimerManager.instance.LateAction(3f, () =>
            {
                caster.status.RemoveAttackSpeedBuff(buff);
                RemoveState();
            });
        }
    }
}

