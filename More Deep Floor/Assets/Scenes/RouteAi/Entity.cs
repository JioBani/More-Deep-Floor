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
        public HexTile currentTile;
        public GameObject currentTileObject;
        public HexTile currentDes;
        public HexTile finalDes;
        public bool isActive;
        [SerializeField] private TileSearcher tileSearcher;
        [SerializeField] private HexPathFinder pathFinder;
        private List<HexTile> routes = new List<HexTile>();  
        private float routeTimer = 0;
        [SerializeField] private EntityManager entityManager;
        public int teamNumber;
        [SerializeField] private Entity target;
        [SerializeField] private float range;
        [SerializeField] private Color gizmoColor;
        public int code;
        public int mode;
        [SerializeField] private List<Vector2Int> currentTileHistory = new List<Vector2Int>();

        public void Update()
        {
            if (isActive)
            {
                if (ReferenceEquals(currentDes, null))
                {
                    code = 4;
                }
                else if (Vector2.SqrMagnitude(transform.position - target.transform.position) <= range)
                {
                    code = 3;
                    currentDes.desNotNeeded = true;
                }
                else
                {
                    currentDes.desNotNeeded = false;
                    if(Vector2.SqrMagnitude(transform.position - currentDes.transform.position) > 0.001f)
                    {
                        code = 1;
                        transform.position = Vector2.MoveTowards(
                            transform.position, 
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
                            //currentTile = currentDes;
                            SetRoute();
                        }
                    }
                    routeTimer += Time.deltaTime;
                    if (routeTimer > 0.5f)
                    {
                        SetRoute();
                        routeTimer = 0;
                    }
                }
            }
        }

        public void SetCurrentTile(Collider2D collider2D)
        {
            currentTileObject = collider2D.gameObject;
            currentTile = currentTileObject.GetComponent<HexTile>();
            currentTileHistory.Add(currentTile.index);
            if (currentTileHistory.Count == 5)
            {
                if (currentTileHistory[0] == currentTile.index &&
                    currentTileHistory[2] == currentTile.index
                   )
                {
                    mode = 0;
                    SetRoute();
                    mode = 2;
                    Debug.Log("재시도");
                }
                currentTileHistory.RemoveAt(0);
            }
        }

        public void SetRoute()
        {
            List<Entity> entities = entityManager.SearchEnemies(teamNumber, this);

            for (int i = 0; i < 3 || i < entities.Count; i++)
            {
                target = entities[i];
                finalDes = target.currentTile;
            
                if (ReferenceEquals(finalDes, null))
                {
                    routes = new List<HexTile>();
                    return;
                }
            
                currentTile = currentTileObject.GetComponent<HexTile>();
                routes = pathFinder.GetRoute(this, currentTile, finalDes.index,mode);
            
                if (routes.Count > 1)
                {
                    currentDes = routes[1];
                    currentDes.SetDesOfEntity(this);
                    return;
                }
            }
            
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            foreach (var route in routes)
            {
                Gizmos.DrawSphere(route.transform.position , 0.2f);
            }
            
            if(code == 2)
                Gizmos.DrawIcon(transform.position, code+".png", true);
        }   
    }
}


