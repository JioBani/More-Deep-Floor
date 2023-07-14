/*
using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using LNK.MoreDeepFloor.Common.GoodsSystyem;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using LNK.MoreDeepFloor.Data.TroopTraits;
using UnityEngine;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class TroopTraitManager : MonoBehaviour
    {
        private GoodsManager goodsManager;
        private GameDataSaver gameDataSaver;
        private TroopTraitsSaveData troopTraitsSaveData;
        private List<TroopTrait> traits;
        [SerializeField] private GameObject traitsSelectButtonParent;
        
        [SerializeField] private List<TroopTraitData> traitsData;
        [SerializeField] private TroopTraitData noneTraitData;
        private List<TraitSelectButton> troopTraitButtons = null;
        
        private void Awake()
        {
            gameDataSaver = new GameDataSaver();
            goodsManager = ReferenceManager.instance.goodsManager;

            troopTraitButtons = new List<TraitSelectButton>();
            
            for (int i = 0; i < traitsSelectButtonParent.transform.childCount; i++)
            {
                troopTraitButtons.Add(traitsSelectButtonParent.transform.GetChild(i).GetComponent<TraitSelectButton>());
            }
            
            
            if (!gameDataSaver.IsTroopTraitSavaDataExist())
            {
                traits = new List<TroopTrait>();
                for (var i = 0; i < traitsData.Count; i++)
                {
                    traits.Add(new TroopTrait(traitsData[i] , 0));
                }
                gameDataSaver.SaveTroopTraitsData(traits);
            }

            if (gameDataSaver.LoadTroopTraitsData(out troopTraitsSaveData))
            {
                SetTraitButtonData();
            }
            else
            {
                Debug.LogError("[TroopTraitManager.Awake()] 데이터를 불러오지 못했습니다.");
            }
        }

        void SetTraitButtonData()
        {
            traits = new List<TroopTrait>();

            /*if (troopTraitButtons == null)
            {
                troopTraitButtons = new Dictionary<TroopTraitId, TraitSelectButton>();
                
                for (int i = 0; i < traitsSelectButtonParent.transform.childCount; i++)
                {
                    TraitSelectButton button = traitsSelectButtonParent.transform.GetChild(i).GetComponent<TraitSelectButton>();
                    if(button.traitData.TraitId != TroopTraitId.None)
                        troopTraitButtons.Add(button.traitData.TraitId , button);
                }
            }#1#
            
            
            for (var i = 0; i < troopTraitButtons.Count; i++)
            {
                var traitData = troopTraitButtons[i].traitData;
                
                if (traitData.TraitId == TroopTraitId.None)
                {
                    troopTraitButtons[i].SetData(new TroopTrait(traitData , 0));
                }
                else
                {
                    var saveData = troopTraitsSaveData.GetData(traitData.TraitId);
                    int level = 0;
                    
                    if (saveData.id != TroopTraitId.None)
                    {
                        level = saveData.level;
                    }
                    
                    TroopTrait trait = new TroopTrait(traitData, level);
                    traits.Add(trait);
                    troopTraitButtons[i].SetData(trait);

                }
                
                
                
                
            }
            
            
            
            /*for (int i = 0; i < traitsSelectButtonParent.transform.childCount; i++)
            {
               
                if(troopTraitsSaveData.GetData(troopTraitButtons))
            }
            
            

            for (var i = 0; i < troopTraitsSaveData.data.Count; i++)
            {
                var saveTraitData = troopTraitsSaveData.data[i];
                var button = troopTraitButtons[saveTraitData.id];
                TroopTraitData traitData = button.traitData;
                TroopTrait trait = new TroopTrait(traitData,saveTraitData.level);
                traits.Add(trait);

                button.SetData(trait);
                Debug.Log($"[TroopTraitManager.SetTraitButtonData()] {trait.traitData.TraitId} : Lv {trait.level}");

                /#2#/TroopTraitData traitData = traitsData.Find(data => data.TraitId == troopTraitsSaveData.data[i].id);
                    
                if (traitData == null)
                {
                    traitData = noneTraitData;
                }

                    
                //TraitSelectButton button = traitsSelectButtonParent.transform.GetChild(i).GetComponent<TraitSelectButton>();#2#
                    
            }#1#
        }

        public bool TryUpgrade(TroopTrait data)
        {
            if (goodsManager.TryBuy(data.traitData.GetPrice(data.level + 1)))
            {
                data.level++;
                gameDataSaver.SaveTroopTraitsData(traits);
                return true;
            }
            else
            {
                return false;
            }
        } 
        
    }
}
*/


