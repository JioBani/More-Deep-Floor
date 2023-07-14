using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Traits.Personalities
{
    
    [CreateAssetMenu(
        fileName = "Personality Data",
        menuName = "Scriptable Object/Traits/Personality/Personality Data",
        order = int.MaxValue)]
    
    public class PersonalityData : TraitData
    {
        [SerializeField] private PersonalityId personalityId;
        public PersonalityId PersonalityId => personalityId;
        
        [SerializeField] private string personalityName;
        public string PersonalityName => personalityName;
        
        
        /*public override RuntimeTraitData GetRuntimeData()
        {
            throw new System.NotImplementedException();
        }*/
    }
}


