using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.TroopTraits;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene
{
    [Serializable]
    public struct Amount
    {
        public string name;
        public float[] values;
    }
    
    [CreateAssetMenu(fileName = "TroopTraitData", menuName = "Scriptable Object/TroopTraitData", order = int.MaxValue)]

    public class TroopTraitData : ScriptableObject
    {
        [SerializeField] private TroopTraitId traitId;
        public TroopTraitId TraitId => traitId;
        
        [SerializeField] private string traitName;
        public string TraitName => traitName;
        
        [SerializeField] private int maxLevel;
        public int MaxLevel => maxLevel;
        
        [SerializeField] private int[] price;
        //public int[] Price => price;
        
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;
        
        [TextArea][SerializeField] private string description;
        public string Description => description;

        [SerializeField]private List<Amount> amounts;

        public int GetPrice(int _level)
        {
            if (price.Length <= _level)
            {
                return price[price.Length - 1];
            }
            else
            {
                return price[_level];
            }
        }

        public string GetDescription(int _level)
        {
            if (_level > maxLevel)
            {
                return "최대 레벨입니다.";
            }
            
            string result = description;
            foreach (var amount in amounts)
            {
                result = result.Replace("{"+ amount.name+"}", amount.values[_level].ToString());
            }

            return result;
        }
    }
}


