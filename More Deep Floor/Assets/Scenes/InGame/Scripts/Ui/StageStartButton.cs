using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class StageStartButton : MonoBehaviour
    {
        public void OnClickStartStage()
        {
            ReferenceManager.instance.stageManager.OnClickStageStart();
            gameObject.SetActive(false);
        }
    }
}


