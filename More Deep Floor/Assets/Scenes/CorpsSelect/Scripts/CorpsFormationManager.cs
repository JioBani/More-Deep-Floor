using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Corps;
using LNK.MoreDeepFloor.Data.Traits.Corps;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class CorpsFormationManager : MonoBehaviour
    {
        private CorpsId[] selectedCorpsIds = {CorpsId.None , CorpsId.None , CorpsId.None , CorpsId.None};
        [SerializeField] private CorpsDataBase corpsDataBase;
        private Dictionary<CorpsId, CorpsData> corpsDatasDic;
        [SerializeField] private CorpsFormationView corpsFormationView;
        //[SerializeField] private 
        private GameDataSaver gameDataSaver;

        private void Awake()
        {
            ReferenceManager.instance.eventManager.AddOnAdaptAction(OnAdapt);
            gameDataSaver = new GameDataSaver();
            corpsDatasDic = new Dictionary<CorpsId, CorpsData>();
            
            corpsDatasDic = corpsDataBase.CorpsDatas.ToDictionary(
                keySelector: (corpsData) => corpsData.CorpsId,
                elementSelector: (corpsData) => corpsData
            );
            
            //LoadFormation();
        }
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadFormation();
        }

        void LoadFormation()
        {
            if (gameDataSaver.LoadFormationData(out var corpsIds))
            {
                for (var i = 0; i < corpsIds.Count; i++)
                {
                    corpsFormationView.SetCorps(i , corpsDatasDic[corpsIds[i]]);
                }
            }
            else
            {
                CustomLogger.LogWarning("[CorpsFormationManager.LoadFormation()] 편성 데이터 불러오기 실패");
            }
        }

        void OnAdapt(List<CorpsData> corpsDatas)
        {
            if (gameDataSaver.SaveCorpsFormationData(corpsDatas.Make((corp) => corp.CorpsId)))
            {
                for (var i = 0; i < corpsDatas.Count; i++)
                {
                    corpsFormationView.SetCorps(i , corpsDatas[i]);
                }
            }
            else
            {
                CustomLogger.LogWarning("[CorpsFormationManager.OnAdapt()] 편성을 저장하지 못함");
            }
            
            
        }
    }
}


