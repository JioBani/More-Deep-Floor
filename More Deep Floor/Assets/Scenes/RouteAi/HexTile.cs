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
            neighbors = new List<HexTile>();
        }
        
        public void AddNeighbor(HexTile neighbor)
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


