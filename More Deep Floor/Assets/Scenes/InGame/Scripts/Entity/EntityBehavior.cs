using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.PathFinding;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public enum MoverState
    {
        None = 0,
        목적지_없음 = 1,
        이동중 = 2,
        도착 = 3,
        타겟없음 = 4,
        타겟죽음 = 5,
        공격중 = 6,
        
    }
    public class EntityBehavior : MonoBehaviour
    {
        //#. 참조
        [SerializeField] private EntityManager entityManager;
        [SerializeField] private BulletManager bulletManager;
        private HexPathFinder pathFinder;
        [SerializeField] private Entity entity;
        
        //#. 프로퍼티
        [SerializeField] private StatusValue damage;
        [SerializeField] private StatusValue moveSpeed;
        [SerializeField] private AttackSpeedValue attackSpeed;
        [SerializeField] private RangeValue range;
        public bool isActive;
        public int mode;
        [SerializeField] private Color gizmoColor;
        [SerializeField] private float routeFindTimer = 0.5f;
        [SerializeField] private bool show;

        //#. 변수
        private List<RouteTile> routes = new List<RouteTile>();
        public RouteTile currentTile;
        public RouteTile currentDes;
        public RouteTile finalDes;
        private float routeTimer = 0;
        [SerializeField] private List<Vector2Int> currentTileHistory = new List<Vector2Int>();
        private readonly Stopwatch stopwatch = new Stopwatch();
        private float routeFindTime = 0;
        private int routeFindCount = 0;
        private float routeFindStd = 0;
        private float similaritySum = 0;
        private float similarityStd = 0;
        private MoverState moverState;
        private float attackTimer;
        
        //#. 이벤트 
        public delegate void BeforeAttackEventHandler(Entity target, float damage);
        public delegate void AfterAttackEventHandler(Entity target , float damage);
        public delegate void OnTargetHitEventHandler(Entity target , float damage);

        public BeforeAttackEventHandler BeforeAttackAction;
        public AfterAttackEventHandler AfterAttackAction;
        public OnTargetHitEventHandler OnTargetHitAction;


        public void Awake()
        {
            entityManager = ReferenceManager.instance.entityManager;
            pathFinder = ReferenceManager.instance.hexPathFinder;
            bulletManager = ReferenceManager.instance.bulletManager;
        }

        public void Init(Entity _entity)
        {
            entity = _entity;
            isActive = false;
            routes = new List<RouteTile>();
            currentTile = null;
            currentDes = null;
            finalDes = null;
            routeTimer = 0;
            currentTileHistory = new List<Vector2Int>();
            routeFindCount = 0;
            routeFindTime = 0;
            routeFindStd = 0;
            moveSpeed = entity.status.moveSpeed;
            attackSpeed = entity.status.attackSpeed;
            range = entity.status.range;
            damage = entity.status.damage;

        }

        void Update()
        {
            if (isActive)
            {
                if (!ReferenceEquals(entity.target, null))
                {
                    if (entity.target.gameObject.activeSelf)
                    {
                        if (Vector2.SqrMagnitude(entity.transform.position - entity.target.transform.position) <= range.square)
                        {
                            moverState = MoverState.공격중;
                            currentDes.desNotNeeded = true;
                            if (attackTimer > attackSpeed.timerPerAttack)
                            {
                                Attack();
                                
                            }
                            else
                            {
                                attackTimer += Time.deltaTime;
                            }
                        }
                        else
                        {
                            if (!ReferenceEquals(currentDes, null))
                            {
                                currentDes.desNotNeeded = false;
                                if(Vector2.SqrMagnitude(entity.transform.position - currentDes.transform.position) > 0.001f)
                                {
                                    moverState = MoverState.이동중;
                                    entity.transform.position = Vector2.MoveTowards(
                                        entity.transform.position, 
                                        currentDes.transform.position, 
                                        moveSpeed.currentValue * Time.deltaTime);
                                }
                                else
                                {
                                    moverState = MoverState.도착;
                                    if (routes.Count == 0)
                                    {
                                        currentDes.RemoveDesOfEntity();
                                        currentDes = null;
                                    }
                                    else
                                    {
                                        currentDes.RemoveDesOfEntity();
                                        SetRoute();
                                    }
                                }
                            }
                            else
                            {
                                moverState = MoverState.목적지_없음;
                                routeTimer += Time.deltaTime;
                                if (routeTimer > routeFindTimer)
                                {
                                    SetRoute();
                                    routeTimer = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        entity.SetTarget(null);
                    }
                }
                else
                {
                    moverState = MoverState.타겟없음;
                    routeTimer += Time.deltaTime;
                    if (routeTimer > routeFindTimer)
                    {
                        SetRoute();
                        routeTimer = 0;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (show)
            {
                Gizmos.color = gizmoColor;
                foreach (var routeTile in routes)
                {
                    Gizmos.DrawSphere(routeTile.transform.position , 0.1f);
                }
            }
        }

        #region #. 길찾기

          public void SetCurrentTile(Collider2D collider2D)
        {
            currentTile = collider2D.GetComponent<RouteTile>();
            currentTileHistory.Add(currentTile.index);
            if (currentTileHistory.Count == 5)
            {
                if (currentTileHistory[0] == currentTile.index &&
                    currentTileHistory[2] == currentTile.index
                   )
                {
                    int tempMode = mode;
                    SetRoute();
                    mode = tempMode;
                }
                currentTileHistory.RemoveAt(0);
            }
        }

        public void SetRoute()
        {
            routeFindCount++;
            
            stopwatch.Reset();
            stopwatch.Start();
            
            //List<Mover> entities = entityManager.SearchEnemies(teamNumber, this);
            List<Entity> entities = entityManager.SearchEnemies(entity);
            for (int i = 0; (i < 3 && i < entities.Count); i++)
            {
                entity.SetTarget(entities[i]);
                finalDes = entity.target.entityBehavior.currentTile;
            
                if (ReferenceEquals(finalDes, null))
                {
                    routes = new List<RouteTile>();
                    break;
                }
                else
                {
                    routes = pathFinder.PathFindingWithPerformanceTest(this, finalDes.index,mode);
            
                    if (routes.Count > 2)
                    {
                        currentDes = routes[1];
                        currentDes.SetDesOfEntity(this);
                        break;
                    }
                }
            }
            stopwatch.Stop();
            routeFindTime += ((float)stopwatch.ElapsedTicks / (float)Stopwatch.Frequency) * 1000;
            routeFindStd = routeFindTime / routeFindCount;
        }

        public void SetRouteWithSimilarityAnalytics()
        {
            routeFindCount++;
            
            stopwatch.Reset();
            stopwatch.Start();
            
            List<Entity> entities = entityManager.SearchEnemies(entity);
            RouteTile lastDes = currentDes;

            for (int i = 0; i < 3 || i < entities.Count; i++)
            {
                entity.SetTarget(entities[i]);
                finalDes = entity.target.entityBehavior.currentTile;
            
                if (ReferenceEquals(finalDes, null))
                {
                    routes = new List<RouteTile>();
                    break;
                }
                else
                {
                    routes = pathFinder.PathFindingWithPerformanceTest(this, finalDes.index,mode);
            
                    if (routes.Count > 1)
                    {
                        currentDes = routes[1];
                        currentDes.SetDesOfEntity(this);
                        break;
                    }
                }
            }
            stopwatch.Stop();
        
            routeFindTime += ((float)stopwatch.ElapsedTicks / (float)Stopwatch.Frequency) * 1000;
            routeFindStd = routeFindTime / routeFindCount;
            
            if (ReferenceEquals(lastDes ,currentDes))
            {
                similaritySum++;
                similarityStd = similaritySum / routeFindCount;
            }
            
        }

        #endregion

        #region #. 행동함수

        void Attack()
        {
            BeforeAttackAction?.Invoke(entity.target , damage.currentValue);
            attackTimer = 0;
            bulletManager.Fire(entity , entity.target, damage.currentValue , entity.data.attackType);
            AfterAttackAction?.Invoke(entity.target , damage.currentValue);
        }

        #endregion
      
    }
}


