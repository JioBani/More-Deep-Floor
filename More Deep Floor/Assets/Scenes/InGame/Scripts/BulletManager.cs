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

        /*
        public void Fire(GameObject firer , GameObject target)
        {
            //objectPooler.Pool();
            Bullet bullet = objectPooler.Pool().GetComponent<Bullet>();
            bullet.transform.position = firer.transform.position;
            bullet.Fire(firer.GetComponent<Defender>() , target);
        }
        */

        public void Fire(Entity firer, Entity target, int damage ,AttackType attackType)
        {
            Bullet bullet = objectPooler.Pool().GetComponent<Bullet>();
            bullet.transform.position = firer.transform.position;
            bullet.SetInfo(firer , target , damage , attackType);
            bullet.Fire();
        }
    }
}

