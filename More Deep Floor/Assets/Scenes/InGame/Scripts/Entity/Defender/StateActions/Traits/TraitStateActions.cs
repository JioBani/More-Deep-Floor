using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.StateActions.Traits
{
    /*public class Name : DefenderStateActionInfoBase
    {
        public Name()
        {
            id = DefenderStateId.Trait_Gladiator;
            type = DefenderStateType.OnKill;
        }

        public override void ActiveAction(Defender caster, Monster target)
        {
            stateController.AddState(DefenderStateId.Effect_Gladiator);
        }
    }*/
    
    
    //#. None

    public class State_None : DefenderStateActionInfoBase {}

    #region #. 직업
    
    //#. 검투사
    public class Trait_Gladiator : DefenderStateActionInfoBase
    {
        public Trait_Gladiator()
        {
            id = DefenderStateId.Trait_Gladiator;
            type = DefenderStateType.OnKill;
        }

        public override void ActiveAction(Defender caster, Monster target)
        {
            stateController.AddState(DefenderStateId.Effect_Gladiator);
        }
    }
    
    
    //#. 직업효과_희열
    public class Effect_Gladiator : DefenderStateActionInfoBase
    {
        public Effect_Gladiator()
        {
            id = DefenderStateId.Effect_Gladiator;
            type = DefenderStateType.Immediately;
        }
        
        List<float> attackSpeedMul;

        public override void OnGenerated()
        {
            attackSpeedMul = new List<float>();
            stateData.GetParameter("AttackSpeedMul0", out float attackSpeedMul0);
            stateData.GetParameter("AttackSpeedMul1", out float attackSpeedMul1);
            stateData.GetParameter("AttackSpeedMul2", out float attackSpeedMul2);
            
            attackSpeedMul.Add(attackSpeedMul0);
            attackSpeedMul.Add(attackSpeedMul1);
            attackSpeedMul.Add(attackSpeedMul2);
        }
        
        public override void OnAction(Defender caster, Monster target)
        {
            int level = traitController.job.synergyLevel;
            float value = attackSpeedMul[level];

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
    
    
    //#. 연구가
    public class Trait_Researcher : DefenderStateActionInfoBase
    {
        private float[] percents;
        
        public Trait_Researcher()
        {
            id = DefenderStateId.Trait_Researcher;
            type = DefenderStateType.OnUseSkill;
        }

        public override void OnGenerated()
        {
            percents = new[] { 0f, 0f, 0f };
            stateData.GetParameter("percent0", out percents[0]);
            stateData.GetParameter("percent1", out percents[1]);
            stateData.GetParameter("percent2", out percents[2]);
        }

        public override void OnUseSkillAction(Defender caster, Monster target, bool isFinal)
        {
            if(isFinal) return;
            
            int level = traitController.job.synergyLevel;
            int trigger = Random.Range(1, 101);

            if (trigger <= percents[level])
            {
                caster.status.SetManaGain(false);
                TimerManager.instance.LateAction(0.75f , () =>
                {
                    caster.status.SetManaGain(true);
                    caster.UseSkillFinal();
                });
            }
        }
    }
    
    //#. 저격수
    public class Trait_Sniper : DefenderStateActionInfoBase
    {
        private float[] percents;
        
        public Trait_Sniper()
        {
            id = DefenderStateId.None;
            type = DefenderStateType.OnTargetHit;
        }

        public override void OnGenerated()
        {
            percents = new[] { 0f, 0f, 0f };
            stateData.GetParameter("Damage0", out percents[0]);
            stateData.GetParameter("Damage1", out percents[1]);
            stateData.GetParameter("Damage2", out percents[2]);
            
        }

        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            if (!target.gameObject.activeSelf) return;
            int level = traitController.job.synergyLevel;
            float maxDamagePercent = percents[level];
            float distance = Vector2.Distance(caster.transform.position, target.transform.position);
            float value = distance / 5f;
            float addDamage = maxDamagePercent * value * 0.01f * damage;
            target.SetHitFinal(addDamage * damage, caster);
            Debug.Log($"[Trait_Sniper] " +
                      $"distance = {distance} , " +
                      $"value = {value} , " +
                      $"addDamage = {addDamage}");
        }
    }

    public class Trait_Circus : DefenderStateActionInfoBase
    {
        private float[] targetNums;
        
        public Trait_Circus()
        {
            id = DefenderStateId.Trait_Circus;
            type = DefenderStateType.BeforeOriginalAttack;
        }

        public override void OnGenerated()
        {
            targetNums = new[] { 0f, 0f };
            stateData.GetParameter("num0", out targetNums[0]);
            stateData.GetParameter("num1", out targetNums[1]);
        }

        public override void OnBeforeOriginalAttackAction(Monster target, DefenderStateId stateId)
        {
            Debug.Log($"OnBeforeAttackAction1");
            int nums = (int)targetNums[traitController.job.synergyLevel];
            Debug.Log($"OnBeforeAttackAction2");
            List<Monster> monsters = defender.TrySearchTargetsExpectTarget(3);
            Debug.Log($"OnBeforeAttackAction3");
            if (stateId == DefenderStateId.Trait_Circus) return;
            Debug.Log($"monsters.Count = {monsters.Count} ,nums = {nums}");
            
            for (var i = 0; i < monsters.Count; i++)
            {
                if(i >= nums) break;
                Debug.Log($"Attack : {i}");
                defender.SetExtraAttack(monsters[i] , DefenderStateId.Trait_Circus);
            }
        }
    }
    
    
    #endregion

    #region #. 성격

    //.# 도전적인
    public class Trait_Challenging : DefenderStateActionInfoBase
    {
        private float[] maxHpPer;
        private TraitController traitController;
        
        public Trait_Challenging()
        {
            id = DefenderStateId.Trait_Challenging;
            type = DefenderStateType.OnTargetHit;
        }

        public override void OnGenerated()
        {
            maxHpPer = new[] { 0f, 0f, 0f};
            stateData.GetParameter("HpPer0", out maxHpPer[0]);
            stateData.GetParameter("HpPer1", out maxHpPer[1]);
            stateData.GetParameter("HpPer2", out maxHpPer[2]);
            
            for (var i = 0; i < maxHpPer.Length; i++)
            {
                maxHpPer[i] /= 100;
            }

            traitController = defender.GetComponent<TraitController>();
        }

        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            TimerManager.instance.LateAction(0.1f , () =>
            {
                if (target.gameObject.activeSelf)
                {
                    target.SetHitFinal(target.status.maxHp * 0.04f, caster);
                }
            });
            
        }
    }
    
    
    //#. 강압적인
    public class Trait_Coercive : DefenderStateActionInfoBase
    {
        private int[] percents;
        private float[] time;
        
        public Trait_Coercive()
        {
            id = DefenderStateId.Trait_Coercive;
            type = DefenderStateType.OnTargetHit;
        }

        public override void OnGenerated()
        {
            
            percents = new[] { 0, 0, 0 };
            time = new[] { 0f, 0f, 0f };

            stateData.GetParameter("percent0", out float value0);
            stateData.GetParameter("percent1", out float value1);
            stateData.GetParameter("percent2", out float value2);
            percents[0] = (int)value0;
            percents[1] = (int)value1;
            percents[2] = (int)value2;
            
            stateData.GetParameter("time0", out time[0]);
            stateData.GetParameter("time1", out time[1]);
            stateData.GetParameter("time2", out time[2]);
            
        }

        public override void ActiveAction(Defender caster, Monster target)
        {
            int level = traitController.character.synergyLevel;
            
            int trigger = Random.Range(1,101);
            if (trigger <= percents[level])
            {
                target.SetStun(true);
                TimerManager.instance.LateAction(time[level] , () =>
                {
                    target.SetStun(false);
                });
            }
           
        }
    }

    
    //#. 탐욕적인
    public class Trait_Greedy : DefenderStateActionInfoBase
    {
        private MarketManager marketManager;
        private float[] percents;
        public Trait_Greedy()
        {
            id = DefenderStateId.Trait_Greedy;
            type = DefenderStateType.OnKill;
        }

        public override void OnGenerated()
        {
            marketManager = ReferenceManager.instance.marketManager;
            percents = new[] { 0f, 0f };
            
            stateData.GetParameter("percent0", out percents[0]);
            stateData.GetParameter("percent1", out percents[1]);
        }

        public override void ActiveAction(Defender caster, Monster target)
        {
            int level = traitController.character.synergyLevel;
            if (level < 0) return;
            
            if (Random.Range(1, 101) <= percents[level])
            {
                marketManager.GoldChange(1);
            }
        }
    }
    
    //#. 고무적인
    public class Trait_Encouraging : DefenderStateActionInfoBase
    {
        private DefenderManager defenderManager;
        private List<Defender> defenders = null;
        
        public Trait_Encouraging()
        {
            id = DefenderStateId.Trait_Encouraging;
            type = DefenderStateType.OnDefenderPlaceChange;
        }

        public override void OnGenerated()
        {
            defenderManager = ReferenceManager.instance.defenderManager;
        }

        public override void OnDefenderPlaceChange(Defender target)
        {
            Debug.Log("Trait_Encouraging : OnDefenderPlaceChange");

            if (defenders != null)
            {
                for (var i = 0; i < defenders.Count; i++)
                {
                    defenders[i].stateController.RemoveState(DefenderStateId.Effect_Encouraging);   
                }
            }

            defenders = defenderManager.GetNearDefenders(defender , 2);
            
            for (var i = 0; i < defenders.Count; i++)
            {
                defenders[i].stateController.AddState(DefenderStateId.Effect_Encouraging);   
            }
        }

        public override void OffAction(Defender caster, Monster target)
        {
            for (var i = 0; i < defenders.Count; i++)
            {
                defenders[i].stateController.RemoveState(DefenderStateId.Effect_Encouraging);   
            }
        }
    }
    
    //#. 특성효과_고무됨
    public class Effect_Encouraging : DefenderStateActionInfoBase
    {
        private StatusBuff statsBuff;
        
        public Effect_Encouraging()
        {
            id = DefenderStateId.Effect_Encouraging;
            type = DefenderStateType.Immediately;
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

    
    

    #endregion
    
    
}


