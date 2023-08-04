using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.SerializableDictionarys;
using LNK.MoreDeepFloor.Data.Traits.Corps;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Corps
{
    [CreateAssetMenu(
        fileName = "Corps Database",
        menuName = "Scriptable Object/Crops/Corps Database",
        order = int.MaxValue)]
    
    public class CorpsDataBase : ScriptableObject
    {
        [SerializeField] private List<CorpsData> corpsDatas;
        public List<CorpsData> CorpsDatas => corpsDatas;
        public int dicLength;

        public Dictionary<CorpsId, CorpsData> CorpsDic { get; private set; }

        /*private void OnValidate()
        {
            SetDic();   
        }*/

        public void SetDic()
        {
            CorpsDic = new Dictionary<CorpsId, CorpsData>();
            foreach (var corpsData in corpsDatas)
            {
                Debug.Log($"{corpsData.CorpsId} : {corpsData.name}");
                CorpsDic[corpsData.CorpsId] = corpsData;
            }
            Debug.Log("[CorpsDataBase.SetDic()] Dic 적용");
            dicLength = CorpsDic.Keys.Count;
        }
    }
}


