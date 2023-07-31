using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.PathFinding
{
    public class RouteTileSearcher : MonoBehaviour
    {
        [SerializeField] private EntityBehavior entityBehavior;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            entityBehavior.SetCurrentTile(col);
        }
    }
}


