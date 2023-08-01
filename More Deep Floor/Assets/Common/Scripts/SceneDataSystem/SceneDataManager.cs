using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using LNK.MoreDeepFloor.Common.SceneDataSystem;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.Common
{
    public enum SceneType
    {
        None,
        MainMenu,
        InGame
    }
    
    public class SceneDataManager : MonoBehaviour
    {
        public static SceneDataManager instance;

        private GameDataSaver gameDataSaver;
        public StageOriginalData[] stageOriginalDatas;
        private int stageSelectIndex;

        private SceneType lastScene;
        private SceneType currentScene;
        
        private InGameResult inGameResult;

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

        public void MoveScene(SceneType sceneType)
        {
            string sceneName;
            
            switch (sceneType)
            {
                case SceneType.MainMenu :
                    sceneName = "MainMenu";
                    break;
                
                case SceneType.InGame : 
                    sceneName = "InGame";
                    break;
                
                default:
                    Debug.LogWarning($"[SceneDataManager.MoveScene()] 이동할수 없는 씬 : {sceneType}");
                    return;
            }

            lastScene = currentScene;
            currentScene = sceneType;
            
            SceneManager.LoadScene(sceneName);
        }

        public void InGameToMain(InGameResult _inGameResult)
        {
            inGameResult = _inGameResult;
            SceneLoadingManager.LoadScene("MainMenu");
            //SceneManager.LoadScene();
        }
        
        
    }
}


