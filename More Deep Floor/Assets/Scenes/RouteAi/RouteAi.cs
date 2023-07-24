using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class RouteAi : MonoBehaviour
    {
        private GameObject target;
        private Looker currentLooker = null;
        private List<Looker> lookers;

        public int FindWay(Vector2 targetVec)
        {
            Vector2 temp = targetVec - (Vector2)transform.position;
            
            if (temp.x > temp.y)
            {
                if (temp.x > -temp.y)
                {
                    return 1;
                }
                else
                {
                    return 4;
                }
            }
            else
            {
                if (temp.x > -temp.y)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!ReferenceEquals(target, null))
            {
                if (ReferenceEquals(target, null))
                {
                    
                }
                else
                {
                    
                }
            }
        }
    }

    public class Looker
    {
        public bool isColideExist = false;
    }
}


