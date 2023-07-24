using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class RouteCol : MonoBehaviour
    {
        [SerializeField] private Entity entity;

        public void SetEntity(Entity _entity)
        {
            entity = _entity;
        }
        
        /*private void OnTriggerEnter2D(Collider2D other)
        {
            if(!ReferenceEquals(entity.gameObject , other.gameObject))
                entity.OnObstacleDetected();
        }*/
    }
}

