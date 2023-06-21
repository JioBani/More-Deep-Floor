using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Entity
{
    public abstract class EntityOriginalData : ScriptableObject
    {
        [SerializeField]
        private string name;
        public string Name { get { return name; } }

            
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;
        
        #region #. 스탯

        //#. 공격력
        [SerializeField] private int[] damages;
        public int[] Damages => damages;
        
        
        //#. 공격속도
        [SerializeField] private float[] attackSpeeds;
        public float[] AttackSpeeds => attackSpeeds;
        
        //#. 사거리
        [SerializeField] private float[] ranges;
        public float[] Ranges => ranges;
        
        //#. 마법력
        [SerializeField] private float[] magicalPowers;
        public float[] MagicalPowers => magicalPowers;

        //#. 치명타률
        [SerializeField] private float[] criticalRates;
        public float[] CriticalRates => criticalRates;
        
        
        //#. 체력
        [SerializeField] private float[] heathPoints;
        public float[] HeathPoints => heathPoints;
        
        //#. 물리방어력
        [SerializeField] private float[] physicalDefenses;
        public float[] PhysicalDefense => physicalDefenses;
        
        //#. 마법방어력
        [SerializeField] private float[] magicalDefenses;
        public float[] MagicalDefenses => magicalDefenses;
        
        //#. 이동속도
        [SerializeField] private float[] moveSpeeds;
        public float[] MoveSpeeds => moveSpeeds;

        //#. 최대마나
        [SerializeField] private int[] maxManas;
        public int[] MaxManas => maxManas;

        #endregion
    }
}
