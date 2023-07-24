using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    [Serializable]
    public class Tile : MonoBehaviour
    {
        public Vector2Int index;
        public Entity entity = null;
        public bool isSomeoneExist = false;
        public PathFinder pathFinder;
        public bool isWall = false;
        private SpriteRenderer spriteRenderer;
        [SerializeField] private bool isDesti = false;
        public int wallStack = 0;
        public Entity owner;

        private List<Entity> usingEntities = new List<Entity>();
        public Entity desOf;
        
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

        public void AddEntity(Entity _entity)
        {
            usingEntities.Add(_entity);
        }

        public void RemoveEntity(Entity _entity)
        {
            usingEntities.Remove(_entity);
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

        public void SetDesOfEntity(Entity _entity)
        {
            desOf = _entity;
        }

        public void RemoveDesOfEntity()
        {
            desOf = null;
        }
    }
}


