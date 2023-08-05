using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.EntityStates;
using LNK.MoreDeepFloor.Data.Traits;
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
    
    public abstract class TraitData : ScriptableObject
    {
        [SerializeField] private TraitType traitType;
        public TraitType TraitType => traitType;
        
        [SerializeField] private TraitId traitId;
        public TraitId TraitId => traitId;
        
        
        [SerializeField] private string traitName;
        public string TraitName => traitName;

        [SerializeField] private Sprite image;
        public Sprite Image => image;
        
        

        [SerializeField] private EntityStateData traitStateData;
        public EntityStateData TraitStateData => traitStateData;


        [SerializeField] private List<DefenderOriginalData> tempMembers;
        public List<DefenderOriginalData> TempMembers => tempMembers;

        [SerializeField] private int[] synergyTrigger;
        public int[] SynergyTrigger => synergyTrigger;
        
        [Space()] [Space()]
        [TextArea][SerializeField] protected string description;
        public string Description => description;
        
        [Space()] [Space()] [SerializeField] private string[] effects;
        
        [SerializeField] private List<Property> properties;
        
        private Dictionary<string, List<float>> propertiesDictionary;
        public Dictionary<string, List<float>> PropertiesDictionary => propertiesDictionary;
        private void OnValidate()
        {
            propertiesDictionary = GetData();
            traitStateData.SetProperties(propertiesDictionary);
        }

        Dictionary<string, List<float>> GetData()
        {
            return properties.ToDictionary(keySelector:p => p.key , elementSelector: p=>p.values);
        }

        

        
        /*public string[] Effects => effects;

        public void SetTraitId(TraitId _traitId)
        {
            id = _traitId;
        }
        
        public void SetTraitName(string _name)
        {
            traitName = _name;
        }

        public string GetEffect(int level)
        {
            if (synergyTrigger.Length <= level && effects.Length <= level)
            {
                return "";
            }
            else
            {
                return synergyTrigger[level] + ") " + effects[level];
            }
        }

        public DefenderState GetTraitState(Defender defender)
        {
            return traitStateData.GetState(defender);
        }*/

        //public abstract RuntimeTraitData GetRuntimeData();
    }
    
    [Serializable]
    public struct Property
    {
        public string key;
        public List<float> values;
    }
}