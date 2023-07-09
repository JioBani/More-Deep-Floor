using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data", order = int.MaxValue)]
    
    public class StageOriginalData : ScriptableObject
    {
        [SerializeField] private string stageName;
        public string StageName { get { return stageName; } }

        public bool isInfinity;
        
        [SerializeField] private int rounds;
        public int Rounds { get { return rounds; } }
        [SerializeField] private RoundOriginalData[] roundsDatas;
        public RoundOriginalData[] RoundsDatas { get { return roundsDatas; } }
        
        [SerializeField] private InfiniteTowerOriginalData infiniteTowerOriginalData;
        public InfiniteTowerOriginalData InfiniteTowerOriginalData { get { return infiniteTowerOriginalData; } }
    }
}


