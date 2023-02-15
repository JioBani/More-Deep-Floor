using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.InGame.Entity;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Bullets
{
    public class Bullet : MonoBehaviour
    {
        public GameObject target;
        public Defender firer;
        private Poolable poolable;
        public float speed;

        void Awake()
        {
            poolable = GetComponent<Poolable>();
        }

        private void Update()
        {
            
        }

        void FixedUpdate()
        {
            Vector3 bulletPos = transform.position;
            Vector3 targetPos = target.transform.position;

            if (target.activeSelf == false)
            {
                SetOff();
            }
            else if (Vector2.Distance(bulletPos, targetPos) < 0.05)
            {
                target.GetComponent<Monster>().SetHit(firer.status.damage.currentValue , firer);
                SetOff();
            }
            else
            {
                transform.position = Vector2.MoveTowards(bulletPos, targetPos, speed);
                transform.rotation = LookDegree.Get(bulletPos, targetPos);
            }
        }

        public void Fire(Defender _firer, GameObject _target)
        {
            firer = _firer;
            target = _target;
        }

        void SetOff()
        {
            poolable.SetOff();
        }
    }
}


