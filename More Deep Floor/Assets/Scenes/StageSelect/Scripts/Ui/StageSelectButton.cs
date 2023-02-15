using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.StageSelect.Ui
{
    public class StageSelectButton : MonoBehaviour
    {
        public void OnClickButton(int stageNumber)
        {
            SceneDataManager.instance.SetStageSelectIndex(stageNumber);
            SceneManager.LoadScene("InGame");
        }
    }
}


