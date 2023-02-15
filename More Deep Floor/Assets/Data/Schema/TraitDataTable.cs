using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Trait Data Table", menuName = "Scriptable Object/Trait Data Table", order = int.MaxValue)]

    public class TraitDataTable : ScriptableObject
    {
        [SerializeField] private List<TraitData> traitDataList;
        public List<TraitData> TraitDataList => traitDataList;

    }
}


