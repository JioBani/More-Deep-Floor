using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class EntityManager : MonoBehaviour
    {
        [SerializeField] private List<Entity> teamA;
        [SerializeField] private List<Entity> teamB;

        public Entity Search(int teamNums , Entity entity)
        {
            List<Entity> enemys;
            
            enemys = teamNums == 0 ? teamB : teamA;

            float dis = 99999;
            Entity minEnemy = null;
            
            foreach (var enemy in enemys)
            {
                float temp = Vector2.SqrMagnitude(enemy.transform.position - entity.transform.position);
                if (dis > temp)
                {
                    dis = temp;
                    minEnemy = enemy;
                }
            }

            return minEnemy;
        }

        public List<Entity> SearchEnemies(int teamNums , Entity entity)
        {
            List<Entity> enemys;

            enemys = teamNums == 0 ? teamB : teamA;

            float dis = 99999;
            Entity minEnemy = null;
            var position = entity.transform.position;

            foreach (var enemy in enemys)
            {
                float temp = Vector2.SqrMagnitude(enemy.transform.position - entity.transform.position);
                if (dis > temp)
                {
                    dis = temp;
                    minEnemy = enemy;
                }
            }

            enemys.Sort((a, b) =>
            {
                float aL = Vector2.SqrMagnitude(a.transform.position - position);
                float bL = Vector2.SqrMagnitude(b.transform.position - position);
                if (aL > bL) return 1;
                if (aL < bL) return -1;
                else return 0;
            });
            
            return enemys;
        }
    }
}

