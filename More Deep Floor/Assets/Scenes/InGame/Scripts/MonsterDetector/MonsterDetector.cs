using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entity;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.MonsterDetectors
{
    public class MonsterDetector
    {
        public static List<Monster> Circle(Vector2 pos ,int radius,int limit = -1)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(pos , radius ,LayerMask.GetMask("MonsterCollideZone"));

            int n;

            if (limit == -1) n = targets.Length;
            else n = limit;

            List<Monster> result = new List<Monster>();

            for (int i = 0; i < n; i++)
            {
                result.Add(targets[i].GetComponent<Monster>());
            }

            return result;
        }
    }
}


