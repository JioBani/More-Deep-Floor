using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common
{
    public class SceneDataManager : MonoBehaviour
    {
        public static SceneDataManager instance;
        public StageOriginalData[] stageOriginalDatas;
        private int stageSelectIndex;
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public StageOriginalData GetStageData()
        {
            return stageOriginalDatas[stageSelectIndex];
        }

        public void SetStageSelectIndex(int index)
        {
            stageSelectIndex = index;
        }
    }
}


