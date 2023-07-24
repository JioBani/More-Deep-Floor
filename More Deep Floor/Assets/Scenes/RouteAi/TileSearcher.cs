using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class TileSearcher : MonoBehaviour
    {
        [SerializeField] private Entity entity;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            entity.SetCurrentTile(col);
        }
    }
}


