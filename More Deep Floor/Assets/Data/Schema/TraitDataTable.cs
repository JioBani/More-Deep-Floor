using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Trait Data Table", menuName = "Scriptable Object/Trait Data Table", order = int.MaxValue)]

    public class TraitDataTable : ScriptableObject
    {
        [SerializeField] private List<TraitData> traitDataList;
        public List<TraitData> TraitDataList => traitDataList;

        private Dictionary<TraitId , RuntimeTraitData> runtimeTraitDataList;

        public void Init()
        {
            runtimeTraitDataList = new Dictionary<TraitId , RuntimeTraitData>();
            
            for (var i = 0; i < traitDataList.Count; i++)
            {
                runtimeTraitDataList.Add(traitDataList[i].Id , traitDataList[i].GetRuntimeData());
            }
        }

        public RuntimeTraitData FindRuntimeTrait(TraitId id)
        {
            if (runtimeTraitDataList.TryGetValue(id, out var runtimeData))
            {
                return runtimeData;
            }
            else
            {
                Debug.LogWarning($"[TraitDataTable.GetRuntimeTrait()] RuntimeTraitData를 찾을 수 없음 : {id}");
                return null;
            }
        }
    }
}


