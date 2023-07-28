using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ExtensionMethods;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class Mover : MonoBehaviour
    {
        //#. 참조
        [SerializeField] private EntityManager entityManager;
        [SerializeField] private HexPathFinder pathFinder;
        [SerializeField] private Entity entity;
        
        //#. 프로퍼티
        [SerializeField] private float speed;
        [SerializeField] private float range;
        public bool isActive;
        public int teamNumber;
        public int code;
        public int mode;
        [SerializeField] private Color gizmoColor;
        [SerializeField] private float routeFindTimer = 0.5f;
        [SerializeField] private bool show;
        [SerializeField] private EntityType entityType;

        //#. 변수
        private List<RouteTile> routes = new List<RouteTile>();
        public RouteTile currentTile;
        public RouteTile currentDes;
        public RouteTile finalDes;
        private float routeTimer = 0;
        [SerializeField] private Mover target;
        [SerializeField] private List<Vector2Int> currentTileHistory = new List<Vector2Int>();
        private readonly Stopwatch stopwatch = new Stopwatch();
        private float routeFindTime = 0;
        private int routeFindCount = 0;
        private float routeFindStd = 0;
        private float similaritySum = 0;
        private float similarityStd = 0;
        
        public void Init()
        {
            routes = new List<RouteTile>();
            currentTile = null;
            currentDes = null;
            finalDes = null;
            routeTimer = 0;
            currentTileHistory = new List<Vector2Int>();
            routeFindCount = 0;
            routeFindTime = 0;
            routeFindStd = 0;
        }

        void Update()
        {
            if (isActive)
            {
                if (ReferenceEquals(currentDes, null))
                {
                    code = 4;
                }
                else if (
                    !ReferenceEquals(target , null) && 
                    Vector2.SqrMagnitude(entity.transform.position - target.transform.position) <= range)
                {
                    code = 3;
                    currentDes.desNotNeeded = true;
                }
                else
                {
                    currentDes.desNotNeeded = false;
                    if(Vector2.SqrMagnitude(entity.transform.position - currentDes.transform.position) > 0.001f)
                    {
                        code = 1;
                        entity.transform.position = Vector2.MoveTowards(
                            entity.transform.position, 
                            currentDes.transform.position, 
                            speed * Time.deltaTime);
                    }
                    else
                    {
                        code = 2;
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
            
            List<Mover> entities = entityManager.SearchEnemies(teamNumber, this);

            for (int i = 0; i < 3 || i < entities.Count; i++)
            {
                target = entities[i];
                finalDes = target.currentTile;
            
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
        }

        public void SetRouteWithSimilarityAnalytics()
        {
            routeFindCount++;
            
            stopwatch.Reset();
            stopwatch.Start();
            
            List<Mover> entities = entityManager.SearchEnemies(teamNumber, this);
            RouteTile lastDes = currentDes;

            for (int i = 0; i < 3 || i < entities.Count; i++)
            {
                target = entities[i];
                finalDes = target.currentTile;
            
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
    }
}


