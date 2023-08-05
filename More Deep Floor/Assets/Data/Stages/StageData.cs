using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Stages
{
    [CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stages/Stage Data", order = int.MaxValue)]
    public class StageData : ScriptableObject
    {
        [SerializeField] private int roundCount;
        public int RoundCount => roundCount;
        
        [SerializeField] private List<RoundData> rounds;
        public List<RoundData> Rounds => rounds;

    }

 

}


