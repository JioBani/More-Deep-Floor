using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using LNK.MoreDeepFloor.Common.GoodsSystyem;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
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
        
        
        private void Awake()
        {
            gameDataSaver = new GameDataSaver();
            goodsManager = ReferenceManager.instance.goodsManager;

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
            
            for (var i = 0; i < troopTraitsSaveData.data.Count; i++)
            {
                TroopTraitData traitData = traitsData.Find(data => data.TraitId == troopTraitsSaveData.data[i].id);
                    
                if (traitData == null)
                {
                    traitData = noneTraitData;
                }

                TroopTrait trait = new TroopTrait(traitData,troopTraitsSaveData.data[i].level);
                Debug.Log($"[TroopTraitManager.SetTraitButtonData()] {trait.traitData.TraitId} : Lv {trait.level}");
                traits.Add(trait);
                    
                TraitSelectButton button = traitsSelectButtonParent.transform.GetChild(i).GetComponent<TraitSelectButton>();
                    
                button.SetData(trait);
            }
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


