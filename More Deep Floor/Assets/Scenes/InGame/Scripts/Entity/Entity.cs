using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using LNK.MoreDeepFloor.InGame.SkillSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public enum EntityLifeState
    {
        None = 0,
        Wait = 1,
        Battle = 2,
        Die = 3,
    }

    public abstract class Entity : MonoBehaviour , IAttackInfoProvider
    {
        //#. 컴포넌트
        [SerializeField] protected EntitySearcher entitySearcher;
        [SerializeField] protected HpBar hpBar;
        [SerializeField] private EntityTarget entityTarget;
        [SerializeField] public Mover mover;

        //#. 컨트롤러
        public StateController stateController { get; private set; }
        protected SkillController skillController { get; private set; }
        
        public EntityStatus status { protected set; get; }
        public EntityData data { private set; get; }

        public EntityLifeState entityLifeState { get; private set; }
        
        //#. 변수 
        public Entity target { protected set; get; }

        //#. Init -> Spawn
        #region #. 이벤트 핸들러

        //#. OnSpawn
        public delegate void OnSpawnEventHandler(Entity self);

        public OnSpawnEventHandler OnSpawnAction;


        //#. Off
        public delegate void OnOffEventHandler(Entity self , Entity killer);

        public OnOffEventHandler OnOffAction;

        //#. OnSpawn
        public delegate void OnUseSkillEventHandler(List<Entity> targets);

        public OnUseSkillEventHandler OnUseSkillAction;


        //#. BeforeAttack
        public delegate void BeforeAttackEventHandler(Entity firer, Entity target, float damage);

        public BeforeAttackEventHandler BeforeAttackAction;


        //#. AfterAttack
        public delegate void AfterAttackEventHandler(Entity firer, Entity target, float damage);

        public AfterAttackEventHandler AfterAttackAction;


        //#. TargetHit
        public delegate void OnTargetHitEventHandler(Entity firer, Entity target, float damage);

        public OnTargetHitEventHandler OnTargetHitAction;


        //#. OnBeforeAttack
        public delegate void OnBeforeAttackAction(Entity firer, Entity target, float damage);

        public OnBeforeAttackAction OnBeforeAttackEvent;


        //#. OnAfterAttack
        public delegate void OnAfterAttackAction(Entity firer, Entity target, float damage);

        public OnAfterAttackAction OnAfterAttackEvent;


        //#. Kill
        public delegate void OnKillEventHandler(Entity killer, Entity target);

        public OnKillEventHandler OnKillAction;


        //#. Hit
        public delegate void OnHitEventHandler(Entity self, Entity firer, float damage);

        public OnHitEventHandler OnHitAction;


        //#. Shield
        //public delegate void OnShieldBreakEventHandler(Entity self , float maxAmount);
        //public OnShieldBreakEventHandler OnShieldBreakAction;

        //public delegate void OnShieldTimeOutEventHandler(Entity self , float maxAmount, float leftAmount);
        //public OnShieldTimeOutEventHandler OnShieldTimeOutAction;


        //#.Die
        public delegate void OnDieEventHandler(Entity self, Entity killer);

        public OnDieEventHandler OnDieAction;

        #endregion


        private void Awake()
        {
            status = new EntityStatus();
            stateController = GetComponent<StateController>();
            skillController = GetComponent<SkillController>();
            //attacker = GetComponent<Attacker>();
            
            entityTarget.AddOnHitListener(BaseOnHit);
            
            EntityAwake();
        }

        protected virtual void EntityAwake()
        {
            
        }

        public void Init(EntityData entityData, int level)
        {
            data = entityData;
            //Logger.Log($"[Entity.Init()] : {status}");
            status.SetStatus(entityData, level);

            AddStateControllerListener();
            skillController.SetSkillData(this, entityData.skillData, stateController);
            //#. TODO 마나바 리프레쉬
            entitySearcher.Init();
            stateController.Reset();

            /*attacker.BeforeAttackAction += BaseOnBeforeAttack;
            attacker.AfterAttackAction += BaseOnAfterAttack;
            attacker.OnTargetHitAction += BaseOnTargetHit;*/
            //status.shieldController.AddListener(OnSh);

            status.OnHpChangedAction += OnHpChanged;

            hpBar.RefreshBar(status.maxHp.currentValue, status.currentHp, status.shieldController.amount);
            
            mover.Init(this);
            
            SetLifeState(EntityLifeState.None);

            OnInit(entityData, level);
            BaseOnSpawn();
        }

        protected virtual void OnInit(EntityData entityData, int level) {}

        void AddStateControllerListener()
        {
            //OnSpawnAction += stateController.EntityStateList(ActionType.OnOn).OnOnAction;
            OnOffAction += stateController.EntityStateList(ActionType.OnOff).OnOffAction;
            OnUseSkillAction += stateController.EntityStateList(ActionType.OnUseSkill).OnUseSkillAction;
            OnTargetHitAction += stateController.EntityStateList(ActionType.OnTargetHit).OnTargetHitAction;
            OnBeforeAttackEvent += stateController.EntityStateList(ActionType.OnBeforeAttack).OnBeforeAttackAction;
            OnAfterAttackEvent += stateController.EntityStateList(ActionType.OnAfterAttack).OnAfterAttackAction;
            OnKillAction += stateController.EntityStateList(ActionType.OnKill).OnKillAction;
            //OnShieldBreakAction += stateController.EntityStateList(ActionType.OnShieldBreak).OnShieldBreakAction;
            //OnShieldTimeOutAction += stateController.EntityStateList(ActionType.OnShieldTimeOut).OnShieldTimeOutAction;
        }


        //#. OnSpawn
        void BaseOnSpawn()
        {
            OnSpawn();
            OnSpawnAction?.Invoke(this);
        }

        protected virtual void OnSpawn()
        {

        }



        //#. Off

        public virtual void SetOff(Entity killer , string msg = null)
        {
            BaseOnOff(killer);
            OnOff(killer);
        }
        void BaseOnOff(Entity killer)
        {
            OnOff(killer);
            OnOffAction?.Invoke(this , killer);
        }

        protected virtual void OnOff(Entity killer)
        {

        }


        //#. OnBeforeAttack
        void BaseOnBeforeAttack(Entity target, float damage)
        {
            OnBeforeAttack(target, damage);
            BeforeAttackAction?.Invoke(this, target, damage);
        }

        protected virtual void OnBeforeAttack(Entity target, float damage)
        {
        }


        //#. OnAfterAttack
        void BaseOnAfterAttack(Entity target, float damage)
        {
            OnAfterAttack(target, damage);
            AfterAttackAction?.Invoke(this, target, damage);
        }

        protected virtual void OnAfterAttack(Entity target, float damage)
        {
        }


        //#. OnTargetHit
        void BaseOnTargetHit(Entity target, float damage)
        {
            OnTargetHit(target, damage);
            OnTargetHitAction?.Invoke(this, target, damage);
        }

        protected virtual void OnTargetHit(Entity entity, float damage)
        {
        }

        //#. OnKill
        public virtual void OnKill(Entity target)
        {
        }


        //#. Hit
        public void SetHit(Entity firer, float damage)
        {
            float leftDamage = status.shieldController.SetDamage(damage);
            status.ChangeHp(-leftDamage, firer);
            hpBar.RefreshBar(status.maxHp.currentValue, status.currentHp, status.shieldController.amount);
            BaseOnHit(firer, damage);

            if (status.currentHp <= 0)
            {
                SetDie(firer);
                OnDie(firer);
                OnDieAction?.Invoke(this, firer);

                //TODO firer.OnKill(this);
                //TODO firer.OnKillAction?.Invoke(firer , this);
            }
        }

        void BaseOnHit(Entity firer, float damage)
        {
            OnHit(firer, damage);
            OnHitAction?.Invoke(this, firer, damage);
        }

        protected virtual void OnHit(Entity firer, float damage)
        {
            
        }


        //#. Attack info
        public abstract float GetAttackDamage();


        //#. Stun
        /*public void SetStun()
        {
            attacker.AddAttackDisableStack("stun", 1);
        }

        public void RemoveStun()
        {
            attacker.RemoveAttackDisableStack("stun", 1);
        }*/

        //#. Die
        protected abstract void SetDie(Entity killer);

        protected virtual void OnDie(Entity killer)
        {
            SetLifeState(EntityLifeState.Die);
        }


        //#. 쉴드
        void BaseOnShieldBreak()
        {

        }

        void AddShield(string id , float maxAmount , EntityState state ,  float lateTime, bool isHasTimeoutAction = false)
        {
            status.shieldController.AddShield(
                id ,
                maxAmount ,
                lateTime,
                state,
                isHasTimeoutAction
            );
            
            hpBar.RefreshBar((int)status.maxHp.currentValue , status.currentHp ,status.shieldController.amount);
        }


        void OnHpChanged(float maxHp , float currentHp, Entity firer)
        {
            hpBar.RefreshBar((int)maxHp , (int)currentHp,status.shieldController.amount);
        }

        public void SetLifeState(EntityLifeState _entityLifeState)
        {
            entityLifeState = _entityLifeState;
        }

        public void SetTarget(Entity _target)
        {
            target = _target;
        }
    }

    public interface IAttackInfoProvider
    {
        float GetAttackDamage();
    }
    
    
}
