using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.SerializableDictionarys;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [Serializable]
    public struct Amounts
    {
        public string name;
        public float[] values;
    }
    
    [CreateAssetMenu(fileName = "Trait Data", menuName = "Scriptable Object/Trait Data", order = int.MaxValue)]
    
    public class TraitData : ScriptableObject
    {
        [SerializeField] private TraitId id;
        public TraitId Id => id;
        
        [SerializeField] private string traitName;
        public string TraitName => traitName;

        [SerializeField] private Sprite image;
        public Sprite Image => image;
        
        [SerializeField] private DefenderStateData traitStateData;
        public DefenderStateData TraitStateData => traitStateData;
        
        [SerializeField] private TraitType traitType;
        public TraitType TraitType => traitType;
        
        [SerializeField] private int[] synergyTrigger;
        public int[] SynergyTrigger => synergyTrigger;
        
        [Space()]
        [Space()]
        [TextArea][SerializeField] protected string description;
        public string Description => description;

        public void SetTraitId(TraitId _traitId)
        {
            id = _traitId;
        }
        
        public void SetTraitName(string _name)
        {
            traitName = _name;
        }

        public virtual DefenderState GetTraitState(Defender defender)
        {
            return traitStateData.GetState(defender);
        }
    }
}