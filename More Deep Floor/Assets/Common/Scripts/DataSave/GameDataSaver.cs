using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.TroopTraitSelect;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.DataSave
{
    
    public class GameDataSaver
    {
        private static string savePath = Application.dataPath + "/" + "GameSaveData";
        private static string goodsDataFileName = Application.dataPath + "/GameSaveData/GoodsData.dat";
        private static string troopTraitsDataFileName = Application.dataPath + "/GameSaveData/TroopTraitsData.dat";

        /*
        string MakeFilePath(string fileName)
        {
            return Application.dataPath + "/" + fileName;
        }*/

        bool SaveData<T>(T data, string fileName)
        {
            return DataSaver.SaveData<T>(data,fileName);
        }

        bool LoadData<T>(string fileName, out T data) where T : class
        {
            return DataSaver.LoadData<T>(fileName, out data);
        }

        public bool SaveGoodsData(GoodsData goodsData)
        {
            return SaveData<GoodsData>(goodsData, goodsDataFileName);
        }

        public bool LoadGoodsData(out GoodsData goodsData)
        {
            if (DataSaver.CheckData(goodsDataFileName))
            {
                bool flag = LoadData<GoodsData>(goodsDataFileName, out goodsData);
                Debug.Log($"[GameDataSaver.LoadGoodsData()] 데이터 불러오기 성공 : {goodsData.gold}");
                return flag;
            }
            else
            {
                Debug.Log("[GameDataSaver.LoadGoodsData()] 데이터가 존재하지 않습니다. 새로 생성합니다.");
                GoodsData newGoodsData = new GoodsData();
                if (!SaveGoodsData(newGoodsData))
                {
                    goodsData = null;
                    return false;
                }
                else
                {
                    goodsData = newGoodsData;
                    return true;
                }
            }
        }

        public bool SaveTroopTraitsData(List<TroopTrait> saveTraits)
        {
            if (DataSaver.CheckData(goodsDataFileName))
            {
                if (!LoadTroopTraitsData(out var lastData))
                {
                    Debug.Log("[GameDataSaver.SaveTroopTraitsData()] 이전에 저장된 정보를 불러오지 못했습니다.");
                    return false;
                }
                
                List<TroopTraitSaveData> newSaveList = lastData.data.ToList();
                
                foreach (var troopTrait in saveTraits)
                {
                    int index = newSaveList.FindIndex(trait => trait.id == troopTrait.traitData.TraitId);
                        
                    if (index != -1)
                    {
                        newSaveList[index] = new TroopTraitSaveData(troopTrait);
                    }
                    else
                    {
                        newSaveList.Add(new TroopTraitSaveData(troopTrait));
                    }
                }

                TroopTraitsSaveData newSaveData = new TroopTraitsSaveData(newSaveList);
                return SaveData<TroopTraitsSaveData>(newSaveData, troopTraitsDataFileName);
            }
            else
            {
                return SaveData<TroopTraitsSaveData>(new TroopTraitsSaveData(new List<TroopTraitSaveData>()), troopTraitsDataFileName);
            }
        }

        public bool LoadTroopTraitsData(out TroopTraitsSaveData traitsData)
        {
            if (DataSaver.CheckData(goodsDataFileName))
            {
                if (LoadData<TroopTraitsSaveData>(troopTraitsDataFileName, out var data))
                {
                    Debug.Log("[GameDataSaver.LoadTroopTraitsData()] 데이터 불러오기 성공");
                    traitsData = data;
                    return true;
                }
                else
                {
                    Debug.Log("[GameDataSaver.LoadTroopTraitsData()] 데이터 불러오기 실패");
                    traitsData = null;
                    return false;
                }
            }
            else
            {
                Debug.Log("[GameDataSaver.LoadTroopTraitsData()] 데이터가 존재하지 않습니다. 새로 생성합니다.");
                if (!SaveTroopTraitsData(new List<TroopTrait>()))
                {
                    traitsData = null;
                    return false;
                }
                else
                {
                    bool flag = LoadTroopTraitsData(out var newData);
                    traitsData = newData;
                    return flag;
                }
            }
            
           
        }
        
    }
}


