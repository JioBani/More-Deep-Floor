using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class RouteTileSearcher : MonoBehaviour
    {
        [SerializeField] private Mover mover;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            mover.SetCurrentTile(col);
        }
    }
}


