using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class DefaultButtonManager : MonoBehaviour
    {
        StageManager stageManager;

        private void Awake()
        {
            stageManager = ReferenceManager.instance.stageManager;
        }

        public void OnClickStartRound()
        {
            stageManager.OnClickStageStart();
        }
    }
}


