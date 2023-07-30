using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.InGame.Bullets;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
     public class Attacker : MonoBehaviour
    {
        [SerializeField]private EntitySearcher targetSearcher;
        private BulletManager bulletManager;
        private Entity firer;
        private AttackType attackType;
        
        public float attackTimer { get; private set; }
        public float timerPerAttack { get; private set; }
        
        public float attackSpeed;

        private Dictionary<string, int> attackDisableStateDic;
        private bool attackable = true;

        /*
        public delegate void BeforeAttackEventHandler(Entity target, float damage);
        public delegate void AfterAttackEventHandler(Entity target , float damage);
        public delegate void OnTargetHitEventHandler(Entity target , float damage);

        public BeforeAttackEventHandler BeforeAttackAction;
        public AfterAttackEventHandler AfterAttackAction;
        public OnTargetHitEventHandler OnTargetHitAction;
        */

        private IAttackInfoProvider attackInfoProvider;

        private void Awake()
        {
            bulletManager = ReferenceManager.instance.bulletManager;
            firer = GetComponent<Entity>();
        }

        private void Update()
        {
            if (targetSearcher.isTargetExist && attackable)
            {
                
                if (attackTimer < timerPerAttack)
                {
                    attackTimer += Time.deltaTime;
                }
                else
                {
                    Attack();
                }
            }
        }

        void Attack()
        {
            //float damage = attackInfoProvider.GetAttackDamage();
            float damage = 10;
            //BeforeAttackAction?.Invoke(targetSearcher.target , damage);
            
            bulletManager.Fire(firer , targetSearcher.target , damage , attackType);
            attackTimer = 0;
            
            //AfterAttackAction?.Invoke(targetSearcher.target , damage);
            
        }

        public void ChangeAttackSpeed(float attackSpeed)
        {
            this.attackSpeed = attackSpeed;
            timerPerAttack = 1 / attackSpeed;
        }

        public void Init(float attackSpeed , IAttackInfoProvider attackInfo , AttackType attackType)
        {
            attackInfoProvider = attackInfo;
            this.attackType = attackType;
            attackDisableStateDic = new Dictionary<string, int>();
            attackable = true;
            ChangeAttackSpeed(attackSpeed);
        }

        public void AddAttackDisableStack(string id , int value)
        {
            if (attackDisableStateDic.ContainsKey(id))
            {
                attackDisableStateDic[id] += value;
            }
            else
            {
                attackDisableStateDic.Add(id , value);
                attackable = false;
            }
        }

        public bool RemoveAttackDisableStack(string id , int value , bool removeAll = false)
        {
            if (attackDisableStateDic.ContainsKey(id))
            {
                if (removeAll)
                {
                    attackDisableStateDic.Remove(id);
                    attackable = true;
                }
                else
                {
                    int stack =  attackDisableStateDic[id];
                    stack -= value;
                
                    if (stack <= 0)
                    {
                        attackDisableStateDic.Remove(id);
                        if (attackDisableStateDic.Count == 0) attackable = true;
                    }
                    else
                    {
                        attackDisableStateDic[id] = stack;
                    }
                }
                return true;
            }
            else
            {
                CustomLogger.LogWarning($"[Attacker.RemoveAttackDisableStack()] 스택이 존재하지 않음 : ( id  : {id})");
                return false;
            }
        }
    }
}