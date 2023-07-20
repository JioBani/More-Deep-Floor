using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Corps
{
    [CreateAssetMenu(
        fileName = "Corps Database",
        menuName = "Scriptable Object/Crops/Corps Database",
        order = int.MaxValue)]
    
    public class CorpsDataBase : ScriptableObject
    {
        [SerializeField ]private List<CorpsData> corpsDatas;
        public List<CorpsData> CorpsDatas => corpsDatas;
    }
}


