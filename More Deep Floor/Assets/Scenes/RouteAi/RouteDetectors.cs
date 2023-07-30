using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.InGame;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class RouteDetectors : MonoBehaviour
    {
        public List<RouteCol> routeCols;
        
        private void Awake()
        {
            routeCols = new List<RouteCol>();
            transform.EachChild((child) =>
            {
                routeCols.Add(child.gameObject.GetComponent<RouteCol>());
            });
        }

        public void SetEntity(Mover mover)
        {
            foreach (var routeCol in routeCols)
            {
                routeCol.SetEntity(mover);
            }
        }

        public void SetRoute(List<Tile> route)
        {
            for (var i = 0; i < routeCols.Count; i++)
            {
                if (i < route.Count)
                {
                    routeCols[i].gameObject.SetActive(true);
                    routeCols[i].transform.position = route[i].transform.position;
                }
                else
                {
                    routeCols[i].gameObject.SetActive(false);
                }
            }
        }

    }
}


