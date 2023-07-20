using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Traits.Corps;
//using LNK.MoreDeepFloor.Data.TroopTraits;
using LNK.MoreDeepFloor.TroopTraitSelect;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.DataSave
{
    
    public class GameDataSaver
    {
        private string savePath;
        private string goodsDataFileName;
        private string troopTraitsDataFileName;
        private string corpsFormationFileName;

        public GameDataSaver()
        {
            savePath = Application.persistentDataPath    + "/" + "GameSaveData";
            goodsDataFileName = Application.persistentDataPath    + "/GameSaveData/GoodsData.dat";
            troopTraitsDataFileName = Application.persistentDataPath    + "/GameSaveData/TroopTraitsData.dat";
            corpsFormationFileName = Application.persistentDataPath + "/GameSaveData/CorpsFormation.dat";
        }
        
        /*
        string MakeFilePath(string fileName)
        {
            return Application.dataPath + "/" + fileName;
        }*/

        bool SaveData<T>(T data, string fileName)
        {
            if (!DataSaver.CheckDirectory(savePath))
            {
                Debug.Log("[GameDataSaver.SaveData()] 데이터 저장 폴더가 없습니다. 새로 생성합니다.");
                Debug.Log($"[GameDataSaver.SaveData()] {savePath + '/'}");
                DataSaver.CreateDirectory(savePath + '/');
            }
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
        
        public bool SaveCorpsFormationData(List<CorpsId> corps)
        {
            return SaveData(corps, corpsFormationFileName);
        }

        public bool LoadFormationData(out List<CorpsId> corps)
        {
            if (DataSaver.CheckData(corpsFormationFileName))
            {
                bool flag = LoadData(corpsFormationFileName, out corps);
                Debug.Log($"[GameDataSaver.LoadGoodsData()] 데이터 불러오기 성공 : {corps.Count}");
                return flag;
            }
            else
            {
                Debug.Log("[GameDataSaver.LoadGoodsData()] 데이터가 존재하지 않습니다. 새로 생성합니다.");
                List<CorpsId> newCorps = new List<CorpsId>() { CorpsId.None  , CorpsId.None,CorpsId.None,CorpsId.None};
                if (!SaveCorpsFormationData(newCorps))
                {
                    corps = null;
                    return false;
                }
                else
                {
                    corps = newCorps;
                    return true;
                }
            }
        }

        /*public bool SaveTroopTraitsData(List<TroopTrait> saveTraits)
        {
            if (DataSaver.CheckData(troopTraitsDataFileName))
            {
                if (!LoadTroopTraitsData(out var lastData))
                {
                    Debug.Log("[GameDataSaver.SaveTroopTraitsData()] 이전에 저장된 정보를 불러오지 못했습니다.");
                    return false;
                }

                Dictionary<TroopTraitId, TroopTraitSaveData> lastDataDic = lastData.TransToDic();
                
                foreach (var troopTrait in saveTraits)
                {
                    lastDataDic[troopTrait.traitData.TraitId] = 
                        new TroopTraitSaveData(troopTrait.traitData.TraitId, troopTrait.level);
                }
                
                List<TroopTraitSaveData> newTroopList = new List<TroopTraitSaveData>();

                foreach (var troopData in lastDataDic)
                {
                    newTroopList.Add(new TroopTraitSaveData(troopData.Key, troopData.Value.level));
                }

                TroopTraitsSaveData newData = new TroopTraitsSaveData(newTroopList);

                return SaveData<TroopTraitsSaveData>(newData,troopTraitsDataFileName);
            }
            else
            {
                List<TroopTraitSaveData> newData = new List<TroopTraitSaveData>();
                
                foreach (var troopTrait in saveTraits)
                {
                    newData.Add(new TroopTraitSaveData(troopTrait));
                }

                return SaveData<TroopTraitsSaveData>(new TroopTraitsSaveData(newData), troopTraitsDataFileName);
            }
        }*/
        
        /*public bool LoadTroopTraitsData(out TroopTraitsSaveData traitsData)
        {
            if (DataSaver.CheckData(troopTraitsDataFileName))
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
            
           
        }*/

        public bool IsTroopTraitSavaDataExist()
        {
            return DataSaver.CheckData(troopTraitsDataFileName);
        }
        
    }
}


