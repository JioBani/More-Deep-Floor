using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public enum AttackType
    {
        None,
        CommonAttack,
        Skill,
        Etc
    }

    public delegate void AttackHitEventHandler(AttackInfo attackInfo , Monster target);
    
    public class AttackInfo
    {
        public Defender caster { get; private set; }
        public AttackType attackType { get; private set; }
        public int damage {get; private set;}

        public bool isFinalSkill = false;
        public bool isOriginalCommonAttack = false;

        public AttackHitEventHandler OnAttackHitAction;

        public AttackInfo(
            Defender _caster, 
            AttackType _attackType, 
            int _damage
            )
        {
            caster = _caster;
            attackType = _attackType;
            damage = _damage;
        }

        public static AttackInfo CommonAttack(Defender defender)
        {
            return new AttackInfo(defender , AttackType.CommonAttack, (int)defender.status.damage.currentValue).SetOriginalCommonAttack(true);
        }

        public static AttackInfo SkillAttack(Defender defender)
        {
            return new AttackInfo(defender , AttackType.Skill, 0);
        }

        public AttackInfo SetOriginalCommonAttack(bool value)
        {
            isOriginalCommonAttack = value;
            return this;
        }
        
        public AttackInfo SetFinalSkill(bool value)
        {
            isFinalSkill = value;
            return this;
        }

        public AttackInfo AddOnAttackHitAction(AttackHitEventHandler action)
        {
            OnAttackHitAction += action;
            return this;
        }

        public void ActiveAttackHitAction(Monster monster)
        {
            OnAttackHitAction?.Invoke(this , monster);
        }
    }
}


