/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class PathUser : MonoBehaviour
    {
        private float speed;
        public Tile currentTile;
        public GameObject currentTileObject;
        public Tile nextTile;
        public Tile currentDes;
        public Tile finalDes;
        public bool isActive;
        [SerializeField] private TileSearcher tileSearcher;
        [SerializeField] private PathFinder pathFinder;
        private List<Tile> routes;
        private float routeTimer = 0;

        public void Update()
        {
            if (isActive)
            {
                if(Vector2.SqrMagnitude(transform.position - currentDes.transform.position) > 0.001f)
                {
                    transform.position = Vector2.MoveTowards(
                        transform.position, 
                        currentDes.transform.position, 
                        speed * Time.deltaTime);
                }
                else
                {
                    if (routes.Count == 0)
                    {
                        currentDes.RemoveDesOfEntity();
                        currentDes = null;
                    }
                    else
                    {
                        currentDes.RemoveDesOfEntity();
                        currentTile = currentDes;
                        SetRoute(finalDes);
                    }
                }
            }
            else
            {
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
        }
        
        public void SetCurrentTile(Tile tile)
        {
            currentTile = tile;
            SetRoute(tile);
        }

        public void SetCurrentTile(Collider2D collider2D)
        {
            currentTileObject = collider2D.gameObject;
        }

        public void SetRoute(Tile des)
        {
            currentTile = currentTileObject.GetComponent<Tile>();
            finalDes = des;
            routes = pathFinder.GetRoute(this, currentTile, des);
            
            if (routes.Count > 1)
            {
                currentDes = routes[1];
                currentDes.SetDesOfEntity(this);
            }
        }

        
    }
}
*/


