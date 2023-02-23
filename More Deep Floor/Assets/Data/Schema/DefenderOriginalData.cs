using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using UnityEngine;



namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Defender Data", menuName = "Scriptable Object/Defender Data", order = int.MaxValue)]
    
    public class DefenderOriginalData : ScriptableObject
    {
        [SerializeField]
        private DefenderId id;
        public DefenderId Id { get { return id; } }

        [SerializeField]
        private string name;
        public string Name { get { return name; } }
        

        [SerializeField] private int[] damages;
        public int[] Damages => damages;
        
        
        [SerializeField] private float[] attackSpeeds;
        public float[] AttackSpeeds => attackSpeeds;
        

        [SerializeField] private int maxMana;
        public int MaxMana { get { return maxMana; } }
        
        [SerializeField] private int cost;
        public int Cost => cost;
        
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;

        [SerializeField] private SkillData skillData;
        public SkillData SkillData => skillData;

        //#. 직업과 특성 
        [SerializeField] private TraitData job;
        public TraitData Job =>job;
        
        [SerializeField] private TraitData character ;
        public TraitData Character  => character ;
        
    }
}

