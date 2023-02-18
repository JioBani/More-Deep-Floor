using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.TroopTraits;
using LNK.MoreDeepFloor.TroopTraitSelect;
using UnityEngine;


namespace LNK.MoreDeepFloor.Common.DataSave.DataSchema
{
    public struct IdTraitPair
    {
        public TroopTraitId id;
        public TroopTrait trait;

        public IdTraitPair(TroopTraitId _id, TroopTrait _trait)
        {
            id = _id;
            trait = _trait;
        }
    }

    [Serializable]
    public struct TroopTraitSaveData
    {
        public TroopTraitId id;
        public int level;
        
        public TroopTraitSaveData(TroopTraitId _id, int _level)
        {
            id = _id;
            level = _level;
        }
        
        public TroopTraitSaveData(TroopTrait trait)
        {
            id = trait.traitData.TraitId;
            level = trait.level;
        }

        public static TroopTraitSaveData NoneData()
        {
            return new TroopTraitSaveData(TroopTraitId.None, 0);
        }
        
    }
    
    [Serializable]
    public class TroopTraitsSaveData
    {
        public List<TroopTraitSaveData> data;

        public TroopTraitsSaveData(List<TroopTraitSaveData> _data)
        {
            data = new List<TroopTraitSaveData>();
            foreach (var troopTrait in _data)
            {
                data.Add(new TroopTraitSaveData(troopTrait.id,troopTrait.level));
            }
        }

        public TroopTraitSaveData GetData(TroopTraitId id)
        {
            TroopTraitSaveData findData = Find(id);
            
            if (findData.id == TroopTraitId.None)
            {
                Debug.LogError($"[TroopTraitsSaveData.GetData()] 특성 데이터를 찾을 수 없습니다. : {id} ");
                return findData;
            }
            else
            {
                return findData;
            }
        }
        public TroopTraitSaveData Find(TroopTraitId id)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if(data[i].id == id) return data[i];
            }

            return TroopTraitSaveData.NoneData();
        }

        public Dictionary<TroopTraitId , TroopTraitSaveData> TransToDic()
        {
            Dictionary<TroopTraitId, TroopTraitSaveData> result = new Dictionary<TroopTraitId, TroopTraitSaveData>();

            foreach (var trait in data)
            {
                result[trait.id] = trait;
            }

            return result;
        }
    }
}


