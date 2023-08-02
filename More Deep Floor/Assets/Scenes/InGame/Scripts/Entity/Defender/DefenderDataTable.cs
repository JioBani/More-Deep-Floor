using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders
{
    public class DefenderDataTable
    {
        List<DefenderData> defenderDatas;
        public Dictionary<DefenderId, DefenderData> defenderSortById { private set; get; }
        public List<DefenderData>[] defenderSortByCost { private set; get; }

        public DefenderDataTable(List<DefenderOriginalData> members)
        {
            defenderDatas = new List<DefenderData>();
            defenderSortById = new Dictionary<DefenderId, DefenderData>();
            defenderSortByCost = new List<DefenderData>[6];
            
            for (var i = 0; i < defenderSortByCost.Length; i++)
            {
                defenderSortByCost[i] = new List<DefenderData>();
            }
            
            for (var i = 0; i < members.Count; i++)
            {
                DefenderData defenderData = new DefenderData(members[i]);
                defenderDatas.Add(defenderData);
                defenderSortById.Add(defenderData.id, defenderData);
                defenderSortByCost[defenderData.cost].Add(defenderData);
            }
        }

        public DefenderData FindById(DefenderId id)
        {
            if (defenderSortById.TryGetValue(id, out var defender))
            {
                return defender;
            }
            else
            {
                Debug.LogWarning($"[DefenderDataTable.FindById()] Defender를 찾을수 없음 : {id}");
                return null;
            }
        }
        
        public void ModifyDefenderData(DefenderDataModifier modifier)
        {
            for (var i = 0; i < defenderDatas.Count; i++)
            {
                defenderDatas[i].ModifyData(modifier);
            }
        }
    }
}