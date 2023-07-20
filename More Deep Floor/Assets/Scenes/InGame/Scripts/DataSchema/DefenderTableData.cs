using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    /*public class DefenderTableData
    {
        private List<DefenderOriginalData>[] defenderSortByCost;
        private Dictionary<DefenderId, DefenderOriginalData> defendersSortById;

        public DefenderTableData(List<DefenderOriginalData> defenders)
        {
            defendersSortById = new Dictionary<DefenderId, DefenderOriginalData>();
            
            foreach (var originalDataDefender in defenders)
            {
                defendersSortById.Add(originalDataDefender.Id , originalDataDefender);
            }
            defenderSortByCost = new List<DefenderOriginalData>[6];
            for (int i = 0; i < defenderSortByCost.Length; i++)
            {
                defenderSortByCost[i] = new List<DefenderOriginalData>();
            }
            
            foreach (var defender in defenders)
            {
                defenderSortByCost[defender.Cost].Add(defender);
            }
        }

        public DefenderOriginalData FindDefenderDataById(DefenderId id)
        {
            if (!defendersSortById.ContainsKey(id))
            {
                Debug.Log($"[DefenderTableData.FindDefenderDataById()] 찾는 id 가 없습니다 : {id}");
                return null;
            }
            else
            {
                return defendersSortById[id];
            }
        }

        public List<DefenderOriginalData> GetDefendersByCost(int cost)
        {
            return defenderSortByCost[cost];
        }
    }*/
}


