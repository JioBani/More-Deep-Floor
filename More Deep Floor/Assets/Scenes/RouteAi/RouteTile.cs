using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class RouteTile : MonoBehaviour
    {
        public bool show;
        public List<RouteTile> neighbors { private set; get; } = new List<RouteTile>();
        public Vector2Int index;
        public bool isWall;
        public int wallStack = 0;
        public Mover desOf;

        public bool desNotNeeded = false;

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
        
        public void Init()
        {
            isWall = false;
            desOf = null;
            neighbors = new List<RouteTile>();
        }
        
        public void AddNeighbor(RouteTile neighbor)
        {
            neighbors.Add(neighbor);
        }
        
        public void SetIndex(Vector2Int index)
        {
            this.index = index;
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


