using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common
{
    public class LookDegree
    {
        public static Quaternion Get(Vector3 from , Vector3 to)
        {
            Vector2 direction = to - from;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            return angleAxis;
        }
    }
}

