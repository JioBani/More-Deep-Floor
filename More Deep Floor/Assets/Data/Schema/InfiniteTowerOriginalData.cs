using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Infinite Tower Data", menuName = "Scriptable Object/Infinite Tower Data", order = int.MaxValue)]
    public class InfiniteTowerOriginalData : ScriptableObject
    {
        [SerializeField] private string stageName;
        public string StageName { get { return stageName; } }

        [SerializeField] private MonsterOriginalData monsterOriginalData;
        public MonsterOriginalData MonsterOriginalData { get { return monsterOriginalData; } }
        
        [SerializeField] private int monsterNums;
        public int MonsterNums { get { return monsterNums; } }
        
        [SerializeField] private float hpIncreaseRate;
        public float HpIncreaseRate { get { return hpIncreaseRate; } }
        
    }
}

