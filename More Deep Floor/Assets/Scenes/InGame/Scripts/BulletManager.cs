using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.InGame.Entity;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class BulletManager : MonoBehaviour
    {
        public ObjectPooler objectPooler;

        public void Fire(GameObject firer , GameObject target)
        {
            //objectPooler.Pool();
            Bullet bullet = objectPooler.Pool().GetComponent<Bullet>();
            bullet.transform.position = firer.transform.position;
            bullet.Fire(firer.GetComponent<Defender>() , target);
        }
    }
}

