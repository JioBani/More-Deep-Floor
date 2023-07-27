using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class RouteCol : MonoBehaviour
    {
        [SerializeField] private Mover mover;

        public void SetEntity(Mover mover)
        {
            this.mover = mover;
        }
        
        /*private void OnTriggerEnter2D(Collider2D other)
        {
            if(!ReferenceEquals(entity.gameObject , other.gameObject))
                entity.OnObstacleDetected();
        }*/
    }
}

