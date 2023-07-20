using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.Data.Traits.Corps;
using LNK.MoreDeepFloor.Data.Traits.Personalities;
using LNK.MoreDeepFloor.Data.Troops;
using UnityEngine;



namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Defender Data", menuName = "Scriptable Object/Defender Data", order = int.MaxValue)]
    
    public class DefenderOriginalData : EntityOriginalData
    {
        [SerializeField]
        private DefenderId id;
        public DefenderId Id { get { return id; } }

        /*[SerializeField]
        private string name;
        public string Name { get { return name; } }*/
        

        [SerializeField] private int cost;
        public int Cost => cost;
        

        //#. 직업과 특성 
        /*[SerializeField] private TraitData job;
        public TraitData Job =>job;
        
        [SerializeField] private TraitData character ;
        public TraitData Character  => character ;*/

        [SerializeField] private CorpsTraitData corpsTraitData;
        public CorpsTraitData CorpsTraitData => corpsTraitData;

        [SerializeField] private CorpsId corpsId; 
        public CorpsId CorpsId => corpsId;
        
        [SerializeField] private PersonalityData personalityData;
        public PersonalityData PersonalityData => personalityData;

    }
}

