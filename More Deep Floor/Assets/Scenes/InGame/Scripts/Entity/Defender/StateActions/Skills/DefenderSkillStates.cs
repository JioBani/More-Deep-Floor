using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.StateActions.Skills
{
    public class Bishop01_Buff : DefenderStateActionInfoBase
    {
        public Bishop01_Buff()
        {
            id = DefenderStateId.Skill_Bishop01;
            type = DefenderStateType.Immediately;
        }
        
        public override void OnAction(Defender caster, Monster target)
        {
            StatusBuff buff = caster.status.AddAttackSpeedBuff(caster.status.attackSpeed.currentValue * 2f , "BishopSkill");
            TimerManager.instance.LateAction(3f , () =>
            {
                caster.status.RemoveAttackSpeedBuff(buff);
                RemoveState();
            });
        }
    }

    public class Knight01_Buff : DefenderStateActionInfoBase
    {
        public Knight01_Buff()
        {
            id = DefenderStateId.Skill_Knight01;
            type = DefenderStateType.Immediately;
        }

        private float damageMul;
        private float attackSpeedMul;
        
        public override void OnGenerated()
        {
            stateData.GetParameter("damageMul" , out damageMul);
            stateData.GetParameter("attackSpeedMul" , out attackSpeedMul);
        }

        public override void OnAction(Defender caster, Monster target)
        {
            DefenderStatus status = caster.status;
            
            StatusBuff dmgBuff = status.damage.AddBuff(status.damage.originalValue * damageMul,"KnightSkill");
            StatusBuff asBuff = status.AddAttackSpeedBuff(status.attackSpeed.currentValue * attackSpeedMul,"KnightSkill");
            status.SetManaGain(false);
            TimerManager.instance.LateAction(3f , () =>
            {
                status.SetManaGain(true);
                status.damage.RemoveBuff(dmgBuff);
                status.RemoveAttackSpeedBuff(asBuff);
                RemoveState();
            });
        }
    }
}


