using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
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
            Debug.LogWarning("[StateData_Effect_Gladiator.GetState()] 정상적인 방법으로 생성되지 않았습니다 : GetState");
            return new Effect_Gladiator(this, defender,null);
        }
    }
    
    //[Serializable]
    public class Effect_Gladiator : DefenderState
    {
        private RuntimeTrait_Gladiator runtimeTraitGladiator;
        public float[] attackSpeed;

        public Effect_Gladiator(DefenderStateData _stateData, Defender _defender, RuntimeTrait_Gladiator runtimeTraitGladiator) : base(_stateData, _defender)
        {
            attackSpeed = runtimeTraitGladiator.currentAttackSpeedUp;
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

