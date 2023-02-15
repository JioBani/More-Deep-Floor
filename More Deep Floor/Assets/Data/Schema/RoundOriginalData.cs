using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Round Data", menuName = "Scriptable Object/Round Data", order = int.MaxValue)]
    
    public class RoundOriginalData : ScriptableObject
    {
        [SerializeField] private string name;
        public string Name { get { return name; }}

        [SerializeField] private MonsterOriginalData monsterOriginal;
        public MonsterOriginalData MonsterOriginal { get { return monsterOriginal; } }
        
        [SerializeField] private int monsterNums;
        public int MonsterNums { get { return monsterNums; } }
    }
}

