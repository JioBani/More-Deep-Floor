using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Tiles
{
    public class Tile : MonoBehaviour
    {
        public Vector2Int index;
        public TileType type;
        public Placer placer;
        public bool isFull;
        private SpriteRenderer debugSpriteRenderer;

        void Awake()
        {
            debugSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public Tile(Vector2Int index, TileType type)
        {
            this.index = index;
            this.type = type;
        }

        public void SetEntity(Placer _placer)
        {
            placer = _placer;
            isFull = true;
        }

        public void LeftEntity()
        {
            try
            {
                Debug.Log($"[TileManager.LeftEntity()] {gameObject.name} : {placer.GetComponent<Defender>().defenderData.spawnId}");
                placer = null;
                isFull = false;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TileManager.LeftEntity()] {gameObject.name} : 배치된 Placer 없음");
                Debug.LogError("[TileManager.LeftEntity()]" + e);
                throw;
            }
        }

        public void SetDebugColor(Color color)
        {
            debugSpriteRenderer.color = color;
        }
    }
}


