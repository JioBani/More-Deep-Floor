using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    //[Serializable]
    public class HexTile : MonoBehaviour
    {
        public bool show;
        public List<HexTile> neighbors { private set; get; } = new List<HexTile>();
        public Vector2Int index;
        public bool isWall;
        public bool isRoute = false;
        public int wallStack = 0;
        public Mover desOf;

        public bool desNotNeeded = false;
        //public bool isEntityExist;
        
        public void AddNeighbor(HexTile neighbor)
        {
            neighbors.Add(neighbor);
        }

        private void OnDrawGizmos()
        {
            

            if (show)
            {
                Gizmos.color = Color.cyan;

                foreach (var hexTile in neighbors)
                {
                    Gizmos.DrawSphere(hexTile.transform.position , 0.2f);
                }
            }
            
            if (isRoute)
            {
                Gizmos.color = Color.cyan;

                Gizmos.DrawSphere(transform.position , 0.2f);
            }

            if (isWall)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawSphere(transform.position , 0.2f);
            }

        }

        public void SetIndex(Vector2Int index)
        {
            this.index = index;
        }
        
        public void SetWall()
        {
            isWall = true;
            //spriteRenderer.color = Color.grey;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            wallStack++;
            if (wallStack > 0) isWall = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            wallStack--;
            if (wallStack < 1) isWall = false;
        }

        public void AddWallStack()
        {
            
        }

        public void RemoveWallStack()
        {
            
        }

        public void SetDesOfEntity(Mover mover)
        {
            desOf = mover;
            desNotNeeded = false;
        }

        public void RemoveDesOfEntity()
        {
            desOf = null;
        }
    }
}


