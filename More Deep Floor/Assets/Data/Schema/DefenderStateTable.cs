using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Defender State Table", menuName = "Scriptable Object/Defender State Table", order = int.MaxValue)]

    public class DefenderStateTable : ScriptableObject
    {
        [SerializeField] private List<DefenderStateData> states;
        private Dictionary<DefenderStateId, DefenderStateData> dic = null;

        /*private void OnValidate()
        {
            dic = new Dictionary<DefenderStateId, DefenderStateData>();
            foreach (var stateData in states)
            {
                dic[stateData.Id] = stateData;
            }
        }*/

        public DefenderStateData Get(DefenderStateId id)
        {
            if (dic == null)
            {
                dic = new Dictionary<DefenderStateId, DefenderStateData>();
                foreach (var stateData in states)
                {
                    dic[stateData.Id] = stateData;
                }
            }
                
            if (dic.TryGetValue(id, out var state))
                return state;
            else
            {
                Debug.LogError($"[DefenderStateData.Get()] DefenderState를 찾을수 없습니다. id : {id}");
                return null;
            }
        }
    }
}


