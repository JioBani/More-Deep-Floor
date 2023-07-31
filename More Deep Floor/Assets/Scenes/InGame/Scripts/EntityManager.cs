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
        //#. 참조
        //[SerializeField] private DefenderManager defenderManager;
        //[SerializeField] private MonsterManager monsterManager;

        //#. 변수
        [SerializeField] private List<Defender> battleDefenders;
        [SerializeField] private List<Monster> battleMonsters;


        private void Awake()
        {
            battleDefenders = ReferenceManager.instance.defenderManager.battleDefenders;
            battleMonsters = ReferenceManager.instance.monsterManager.monsters;
        }

        public List<Entity> SearchEnemies(Entity attacker)
        {
            List<Entity> enemies;
            float dis = 99999;
            var position = attacker.transform.position;
            
            if (attacker.data.entityType == EntityType.Defender)
            {
                enemies = battleMonsters.Cast<Entity>().ToList();
            }
            else if (attacker.data.entityType == EntityType.Monster)
            {
                enemies = battleDefenders.Cast<Entity>().ToList();
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

        public void OnClickStart()
        {
            foreach (var battleDefender in battleDefenders)
            {
                battleDefender.entityBehavior.isActive = true;
            }
            
            foreach (var battleMonster in battleMonsters)
            {
                battleMonster.entityBehavior.isActive = true;
            }
        }
    }
}


