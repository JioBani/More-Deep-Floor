using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ExtensionMethods;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LNK.MoreDeepFloor.RouteAiScene
{

    public class GameManager : MonoBehaviour
    {
        public GameObject entities;

        private void Awake()
        {
            //Time.timeScale = 0.1f;
        }

        public void OnClickStart()
        {
            entities.transform.EachChild((c) =>
            {
                Mover e = c.GetComponent<Mover>();
                e.isActive = true;
                e.SetRoute();

            });
        }
    }
}

