using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Bullets
{
    public enum AttackType
    {
        None,
        DefenderToMonster,
        MonsterToDefender
    }
    
    public class Bullet : MonoBehaviour
    {
        public Entity target;
        public Entity firer;
        private Poolable poolable;
        public float speed;
        private AttackType attackType;

        private int damage;
        private bool isBulletActive;

        void Awake()
        {
            poolable = GetComponent<Poolable>();
        }

        void FixedUpdate()
        {
            Vector3 bulletPos = transform.position;
            Vector3 targetPos = target.transform.position;

            if (target.gameObject.activeSelf == false)
            {
                SetOff();
            }
            else if (isBulletActive)
            {
                if (Vector2.Distance(bulletPos, targetPos) < 0.05)
                {
                    if (attackType == AttackType.DefenderToMonster)
                    {
                        target.GetComponent<Monster>().SetHit(damage , firer as Defender);
                    }
                    else if(attackType == AttackType.MonsterToDefender)
                    {
                        target.GetComponent<Defender>().SetHit(damage , firer as Monster);
                    }
                    SetOff();
                }
                else
                {
                    transform.position = Vector2.MoveTowards(bulletPos, targetPos, speed);
                    transform.rotation = LookDegree.Get(bulletPos, targetPos);
                }
            }
            
        }

        public void SetInfo(Entity _firer , Entity _target , int _damage, AttackType _attackType)
        {
            firer = _firer;
            target = _target;
            damage = _damage;
            attackType = _attackType;
        }

        public void Fire()
        {
            isBulletActive = true;
        }

        void SetOff()
        {
            isBulletActive = false;
            poolable.SetOff();
        }
    }
}


