using System;
using System.Collections;
using System.Collections.Generic;
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

        public Dictionary<CorpsId, CorpsData> CorpsDic { get; private set; }

        private void OnValidate()
        {
            SetDic();
        }

        void SetDic()
        {
            CorpsDic = new Dictionary<CorpsId, CorpsData>();
            foreach (var corpsData in corpsDatas)
            {
                CorpsDic[corpsData.CorpsId] = corpsData;
            }
            Debug.Log("[CorpsDataBase.SetDic()] Dic 적용");

        }
    }
}


