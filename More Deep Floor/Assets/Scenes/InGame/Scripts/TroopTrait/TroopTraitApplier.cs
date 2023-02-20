using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using LNK.MoreDeepFloor.Data.TroopTraits;
using LNK.MoreDeepFloor.TroopTraitSelect;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.InGame.TroopTraitSystem
{
   
    
    public class TroopTraitApplier : MonoBehaviour
    {
        private InGameStateManager inGameStateManager;
        
        [SerializeField] private TroopTraitGenerator generator;
        
        public TroopTraitsSaveData saveData;
        private GameDataSaver gameDataSaver;
        private StageManager stageManager;
        public List<TroopTrait> traits;
        [SerializeField] private TroopTraitTable troopTraitTable;

        private Dictionary<TroopTraitType, List<TroopTrait>> traitSortByType;

        private void Awake()
        {
            inGameStateManager = ReferenceManager.instance.inGameStateManager;
            gameDataSaver = new GameDataSaver();
            stageManager = ReferenceManager.instance.stageManager;

            traitSortByType = new Dictionary<TroopTraitType, List<TroopTrait>>()
            {
                { TroopTraitType.None  , new List<TroopTrait>()},
                { TroopTraitType.OnDataLoad  , new List<TroopTrait>()},
            };
            
            
            inGameStateManager.OnSceneLoadAction += OnSceneLoaded;
            inGameStateManager.OnDataLoadAction += OnDataLoad;
            
        }
        
        void OnSceneLoaded()
        {
            
            if (!gameDataSaver.LoadTroopTraitsData(out saveData))
            {
                Debug.LogError("[TroopTraitAdapter.Start()] 데이터를 불러올 수 없습니다.");
            }
            else
            {
                traitSortByType = new Dictionary<TroopTraitType, List<TroopTrait>>()
                {
                    { TroopTraitType.None  , new List<TroopTrait>()},
                    { TroopTraitType.OnDataLoad  , new List<TroopTrait>()},
                };

                traits = new List<TroopTrait>();
                
                for (var i = 0; i < saveData.data.Count; i++)
                {
                    TroopTrait newTrait = generator.Get(saveData.data[i].id, saveData.data[i].level);
                    traits.Add(newTrait);
                    traitSortByType[newTrait.type].Add(newTrait);
                    Debug.Log($"[TroopTraitApplier.OnSceneLoaded()] 데이터 로드 : {newTrait.traitData.TraitId}");
                }
            }
        }
        
        void OnDataLoad()
        {
            foreach (var troopTrait in traitSortByType[TroopTraitType.OnDataLoad])
            {
                troopTrait.OnDataLoadAction();
            }
        }
    }
}


