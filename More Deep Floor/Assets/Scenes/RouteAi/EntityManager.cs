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
    }
}

