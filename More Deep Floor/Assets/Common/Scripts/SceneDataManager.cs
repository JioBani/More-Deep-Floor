using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common
{
    public class SceneDataManager : MonoBehaviour
    {
        public static SceneDataManager instance;

        private GameDataSaver gameDataSaver;
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


