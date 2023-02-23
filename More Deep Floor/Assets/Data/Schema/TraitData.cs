using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Trait Data", menuName = "Scriptable Object/Trait Data", order = int.MaxValue)]
    
    public class TraitData : ScriptableObject
    {
        [SerializeField] private TraitId id;
        public TraitId Id => id;
        
        [SerializeField] private string traitName;
        public string TraitName => traitName;

        [SerializeField] private DefenderStateData traitStateData;
        public DefenderStateData TraitStateData => traitStateData;
        
        [SerializeField] private TraitType traitType;
        public TraitType TraitType => traitType;
        
        /*[SerializeField] private DefenderStateId traitStateId;
        public DefenderStateId TraitStateId => traitStateId;*/


        [SerializeField] private int[] synergyTrigger;
        public int[] SynergyTrigger => synergyTrigger;


        public void SetTraitId(TraitId _traitId)
        {
            id = _traitId;
        }
        
        public void SetTraitName(string _name)
        {
            traitName = _name;
        }
    }
}