using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.InGame.Bullets;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public ObjectPooler monsterPooler;
    public ObjectPooler bulletPooler;
    public ObjectPooler defenderPooler;
    public ObjectPooler textPooler;
    public ObjectPooler rangeSkillBulletPooler;


    private Dictionary<GameObject , Bullet> bullets;

    private void Awake()
    {
        bullets = new Dictionary<GameObject , Bullet>();
        
        bulletPooler.transform.EachChild((child) =>
        {
            bullets.Add(child.gameObject , child.GetComponent<Bullet>());
        });
        
    }

    public Bullet PoolBullet()
    {
        GameObject g = bulletPooler.Pool();

        if (!bullets.TryGetValue(g, out Bullet bullet))
        {
            Bullet newBullet = g.GetComponent<Bullet>();
            bullets.Add(g, newBullet);
            return newBullet;
        }

        return bullet;
    }
}
