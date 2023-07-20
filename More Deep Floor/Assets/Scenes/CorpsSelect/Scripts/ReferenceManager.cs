using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.CorpsSelectScene.CorpsInfoViews;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class ReferenceManager : MonoBehaviour
    {
        public static ReferenceManager instance = null;
        
        public CorpsInfoView corpsInfoView;
        public EventManager eventManager;
        public CorpsFormationManager corpsFormationManager;
        public Canvas uiCanvas;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}


