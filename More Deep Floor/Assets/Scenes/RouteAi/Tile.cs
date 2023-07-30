using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.InGame;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    [Serializable]
    public class Tile : MonoBehaviour
    {
        public Vector2Int index;
        public bool isWall = false;
        private SpriteRenderer spriteRenderer;
        public int wallStack = 0;

        private List<Mover> usingEntities = new List<Mover>();
        public Mover desOf;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetIndex(Vector2Int index)
        {
            this.index = index;
        }

        public void SetRoute()
        {
            spriteRenderer.color = Color.green;
        }

        public void SetWall()
        {
            isWall = true;
            spriteRenderer.color = Color.grey;
        }

        public void Init()
        {
            spriteRenderer.color = Color.white;
        }

        public void AddEntity(Mover mover)
        {
            usingEntities.Add(mover);
        }

        public void RemoveEntity(Mover mover)
        {
            usingEntities.Remove(mover);
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
        }

        public void RemoveDesOfEntity()
        {
            desOf = null;
        }
    }
}


