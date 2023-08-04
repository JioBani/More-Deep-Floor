using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Troops;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Corps
{
    [CreateAssetMenu(
        fileName = "Trait Database",
        menuName = "Scriptable Object/Trait/Trait Database",
        order = int.MaxValue)]
    
    public class TraitDataBase : ScriptableObject
    {
        [SerializeField] private List<TraitData> traitDatas;
        public List<TraitData> TraitDatas => traitDatas;
        
        public Dictionary<TraitId, TraitData> GetDic()
        {
            return traitDatas.ToDictionary(keySelector: data => data.TraitId, elementSelector: data => data);
        }
    }
}


