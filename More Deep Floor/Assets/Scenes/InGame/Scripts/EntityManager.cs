using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class EntityManager : MonoBehaviour
    {
        //#. 변수
        private DefenderManager defenderManager;
        private MonsterManager monsterManager;


        private void Awake()
        {
            defenderManager = ReferenceManager.instance.defenderManager;
            monsterManager = ReferenceManager.instance.monsterManager;
        }

        public List<Entity> SearchEnemies(Entity attacker)
        {
            List<Entity> enemies;
            var position = attacker.transform.position;

            if (attacker.data.entityType == EntityType.Defender)
            {
                enemies = monsterManager.monsters.Cast<Entity>().ToList();
            }
            else if (attacker.data.entityType == EntityType.Monster)
            {
                enemies = defenderManager.battleDefenders.Cast<Entity>().ToList();
            }
            else
            {
                return new List<Entity>();
            }
            
            enemies.Sort((a, b) =>
            {
                float aL = Vector2.SqrMagnitude(a.transform.position - position);
                float bL = Vector2.SqrMagnitude(b.transform.position - position);
                if (aL > bL) return 1;
                if (aL < bL) return -1;
                else return 0;
            });
            
            return enemies;
        }
    }
}


