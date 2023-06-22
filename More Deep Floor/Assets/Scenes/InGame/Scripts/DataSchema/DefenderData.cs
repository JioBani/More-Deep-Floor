using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    public class DefenderDataModifier
    {
        public int damage;
        public float attackSpeed;
    }
    
    public class DefenderData : EntityData
    {
        public DefenderId id;
        public string spawnId;
        public int cost;
      
        public SkillData skillData;
        public TraitData job;
        public TraitData character;

        public DefenderData(DefenderOriginalData defenderOriginalData) 
            : base(defenderOriginalData)
        {
            
            id = defenderOriginalData.Id;
            cost = defenderOriginalData.Cost;
            skillData = defenderOriginalData.SkillData;
            job = defenderOriginalData.Job;
            character = defenderOriginalData.Character;
        }

        /*public DefenderData(DefenderOriginalData defenderOriginalData, DefenderDataModifier modifier) : this(defenderOriginalData)
        {
            for (var i = 0; i < currentDamages.Length; i++)
            {
                currentDamages[i] += modifier.damage;
            }

            for (var i = 0; i < currentAttackSpeeds.Length; i++)
            {
                currentAttackSpeeds[i] += modifier.attackSpeed;
            }
        }*/

        public void ModifyData(DefenderDataModifier modifier)
        {
            for (var i = 0; i < damages.currentValues.Length; i++)
            {
                damages.currentValues[i] += modifier.damage;
            }

            for (var i = 0; i < attackSpeeds.currentValues.Length; i++)
            {
                attackSpeeds.currentValues[i] += modifier.attackSpeed;
            }
        }
        
    }
}


