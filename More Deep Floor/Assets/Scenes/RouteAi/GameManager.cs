using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ExtensionMethods;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace LNK.MoreDeepFloor.RouteAiScene
{

    public class GameManager : MonoBehaviour
    {
        public List<Entity> entities;

        public void OnClickStart()
        {
            foreach (var entity in entities)
            {
                //entity.isStarted = true;
                entity.isActive = true;
                entity.SetRoute();
                //entity.StartCoroutine(entity.SetRouteRoutine());
            }
        }
    }
}

