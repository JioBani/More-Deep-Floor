using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float speed;
        public Tile currentTile;
        public GameObject currentTileObject;
        public Tile nextTile;
        public Tile currentDes;
        public Tile finalDes;
        public bool isActive;
        [SerializeField] private TileSearcher tileSearcher;
        [SerializeField] private PathFinder pathFinder;
        private List<Tile> routes = new List<Tile>();  
        private float routeTimer = 0;
        [SerializeField] private EntityManager entityManager;
        public int teamNumber;
        [SerializeField] private Entity target;
        [SerializeField] private float range;
        [SerializeField] private Color gizmoColor;
        public int code;

        public void Update()
        {
            if (isActive)
            {
                if (Vector2.SqrMagnitude(transform.position - target.transform.position) <= range)
                {
                    code = 2;
                }
                else if (ReferenceEquals(currentDes, null))
                {
                    code = 1;
                    if (routeTimer > 0.5f)
                    {
                        SetRoute();
                        routeTimer = 0;
                    }
                    else
                    {
                        routeTimer += Time.deltaTime;
                    }
                }
                else if(Vector2.SqrMagnitude(transform.position - currentDes.transform.position) > 0.001f)
                {
                    code = 3;
                    transform.position = Vector2.MoveTowards(
                        transform.position, 
                        currentDes.transform.position, 
                        speed * Time.deltaTime);
                }
                else
                {
                    code = 4;
                    if (routes.Count == 0)
                    {
                        currentDes.RemoveDesOfEntity();
                        currentDes = null;
                    }
                    else
                    {
                        currentDes.RemoveDesOfEntity();
                        //currentTile = currentDes;
                        SetRoute();
                    }
                }
            }
        }

        public void SetCurrentTile(Collider2D collider2D)
        {
            currentTileObject = collider2D.gameObject;
            currentTile = currentTileObject.GetComponent<Tile>();
        }

        public void SetRoute()
        {
            target = entityManager.Search(teamNumber, this);
            finalDes = target.currentTile;
            
            if (ReferenceEquals(finalDes, null))
            {
                routes = new List<Tile>();
                return;
            }
            
            currentTile = currentTileObject.GetComponent<Tile>();
            routes = pathFinder.GetRoute(this, currentTile, finalDes.index);
            
            if (routes.Count > 1)
            {
                currentDes = routes[1];
                currentDes.SetDesOfEntity(this);
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            foreach (var route in routes)
            {
                Gizmos.DrawSphere(route.transform.position , 0.2f);
            }
        }
    }
}


