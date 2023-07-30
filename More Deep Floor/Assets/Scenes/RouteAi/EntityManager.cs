using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame;
using UnityEngine;


namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class EntityManager : MonoBehaviour
    {
        [SerializeField] private List<Mover> teamA;
        [SerializeField] private List<Mover> teamB;

        public Mover Search(int teamNums , Mover mover)
        {
            List<Mover> enemys;
            
            enemys = teamNums == 0 ? teamB : teamA;

            float dis = 99999;
            Mover minEnemy = null;
            
            foreach (var enemy in enemys)
            {
                float temp = Vector2.SqrMagnitude(enemy.transform.position - mover.transform.position);
                if (dis > temp)
                {
                    dis = temp;
                    minEnemy = enemy;
                }
            }

            return minEnemy;
        }

        public List<Mover> SearchEnemies(int teamNums , Mover mover)
        {
            List<Mover> enemys;

            enemys = teamNums == 0 ? teamB : teamA;

            float dis = 99999;
            Mover minEnemy = null;
            var position = mover.transform.position;

            foreach (var enemy in enemys)
            {
                float temp = Vector2.SqrMagnitude(enemy.transform.position - mover.transform.position);
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
        
        public void OnClickStart()
        {
            foreach (var mover in teamA)
            {
                mover.isActive = true;
                mover.SetRoute();
            }
            
            foreach (var mover in teamB)
            {
                mover.isActive = true;
                mover.SetRoute();
            }
        }

        void SetEntities()
        {
            
        }
    }
}

