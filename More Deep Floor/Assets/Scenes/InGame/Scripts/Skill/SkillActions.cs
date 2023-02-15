using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defender;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.StateActions.Skills;
using UnityEngine;


namespace LNK.MoreDeepFloor.InGame.SkillSystem
{
    /*public class Name : SkillActionInfoBase
    {
        public Name(SkillData _skillData, Defender _caster, DefenderStateController _stateController)
            : base(_skillData, _caster, _stateController)
        {
            
        }

        public override void OnAction(List<Monster> targets = null)
        {
            
        }
    }*/
    
    public class None : SkillActionInfoBase
    {
        public override void Act(List<Monster> targets = null)
        {
            
        }
    }

    //#. 1코스트 나이트
    public class Knight01 : SkillActionInfoBase
    {
        public override void Act(List<Monster> targets = null)
        {
            stateController.AddState(DefenderStateId.Skill_Knight01);
        }
    }

    //#. 1코스트 룩
    public class Rock01 : SkillActionInfoBase
    {
        private float rangeDamage;
        private float slowTime;
        private ObjectPooler rangeSkillBulletPooler;

        public override void Set(SkillData _skillData, Defender _caster, DefenderStateController _stateController)
        {
            base.Set(_skillData , _caster,_stateController);
            if (skillData.GetParameter("RangeDamage", out float _rangeDamage))
                rangeDamage = _rangeDamage;
            else
                rangeDamage = 0;
            
            if (skillData.GetParameter("SlowTime", out float _slowTime))
                slowTime = _slowTime;
            else
                slowTime = 0;

            rangeSkillBulletPooler = ReferenceManager.instance.objectPoolingManager.rangeSkillBulletPooler;
        }

        public override void Act(List<Monster> targets)
        {
            Monster target = targets[0];
            if (ReferenceEquals(target , null))
            {
                return;
            }
           
            rangeSkillBulletPooler.Pool().GetComponent<RangeSkillBullet>().Show(target.transform.position);
            
            Collider2D[] cols = Physics2D.OverlapCircleAll(
                target.transform.position, 
                2f, 
                LayerMask.GetMask("MonsterCollideZone"));

            for (int i = 0; i < cols.Length; i++)
            {
                Monster monster = cols[i].transform.parent.GetComponent<Monster>();
                if(monster == null) continue;
                monster.SetHitWithBuff(new AttackInfo(
                    caster ,
                    (int)rangeDamage ,
                    (target) =>
                    {
                        MonsterStatus status = target.status;
                        int id = status.AddSpeedBuff(-target.status.currentSpeed / 2);
                        TimerManager.instance.LateAction(slowTime, () =>
                        {
                            status.RemoveSpeedBuff(id);
                        });
                    }
                ));
            }
        }
    }
    
    //#. 1코스트 비숍
    public class Bishop01 : SkillActionInfoBase
    {
        private DefenderManager defenderManager;
      
        public override void Set(SkillData _skillData, Defender _caster, DefenderStateController _stateController)
        {
            base.Set(_skillData, _caster, _stateController);
            defenderManager = ReferenceManager.instance.defenderManager;
        }

        public override void Act(List<Monster> targets = null)
        {
            List<Defender> defenders = defenderManager.GetNearDefenders(caster , 2);
            
            for (int i = 0; i < defenders.Count; i++)
            {
                Defender defender = defenders[i];
                DefenderStateController state = defender.GetComponent<DefenderStateController>();
                state.AddState(DefenderStateId.Skill_Bishop01 );
            }
        }
    }
}

