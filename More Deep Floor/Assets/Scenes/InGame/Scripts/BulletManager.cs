using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class BulletManager : MonoBehaviour
    {
        public ObjectPooler objectPooler;
        public ObjectPoolingManager objectPoolingManager;

        void Awake()
        {
            objectPoolingManager = ReferenceManager.instance.objectPoolingManager;
        }

        public void Fire(Entity firer, Entity target, float damage ,AttackType attackType)
        {
            Bullet bullet = objectPoolingManager.PoolBullet();
            bullet.transform.position = firer.transform.position;
            bullet.SetInfo(firer , target , damage , attackType);
            bullet.Fire();
        }
    }
}

