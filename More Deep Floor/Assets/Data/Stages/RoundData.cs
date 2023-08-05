using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LNK.MoreDeepFloor.Data.Stages
{
    [CreateAssetMenu(fileName = "Round Data", menuName = "Scriptable Object/Stages/Round Data", order = int.MaxValue)]
    public class RoundData : ScriptableObject
    {
        [SerializeField] private int monsterCount;
        public int MonsterCount => monsterCount;
        
        [SerializeField] private List<MonsterProbability> probabilities;
        public List<MonsterProbability> Probabilities => probabilities;

        [SerializeField] private int totalRate;
        private List<int> monsterCounts;

        private void OnValidate()
        {
            totalRate = 0;
            monsterCounts = new List<int>();
            foreach (var pro in probabilities)
            {
                totalRate += pro.MonsterCount;
                monsterCounts.Add(totalRate);
            }
        }

        public MonsterOriginalData GetMonster()
        {
            int k = Random.Range(1,totalRate + 1);
            for (var i = 0; i < monsterCounts.Count; i++)
            {
                if (k <= monsterCounts[i])
                {
                    return Probabilities[i].MonsterOriginalData;
                }
            }
            
            CustomLogger.LogWarning("[RoundData.GetMonster()] totalRate가 초과됨");
            return null;
        }
    }   
    
    [Serializable]
    public struct MonsterProbability
    {
        [SerializeField] private int monsterCount;
        public int MonsterCount => monsterCount;
        
        [SerializeField] private MonsterOriginalData monsterOriginalData;
        public MonsterOriginalData MonsterOriginalData => monsterOriginalData;

        [SerializeField] private int rate;
        public int Rate => rate;
    }
}
