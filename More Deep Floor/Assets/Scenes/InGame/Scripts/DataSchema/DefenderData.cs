using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    public class DefenderDataModifier
    {
        public int damage;
        public float attackSpeed;
    }
    
    public class DefenderData
    {
        public DefenderId id;
        public string spawnId;
        public int cost;
        public string name;
        public int[] damages;
        public int[] currentDamages;
        public float[] attackSpeeds;
        public float[] currentAttackSpeeds;
        public Sprite sprite;
        public int maxMana;
        public SkillData skillData;
        public TraitData job;
        public TraitData character;

        public DefenderData(DefenderOriginalData defenderOriginalData)
        {
            id = defenderOriginalData.Id;
            cost = defenderOriginalData.Cost;
            name = defenderOriginalData.Name;
            damages = (int[])defenderOriginalData.Damages.Clone();
            currentDamages = (int[])defenderOriginalData.Damages.Clone();
            attackSpeeds = (float[])defenderOriginalData.AttackSpeeds.Clone();
            currentAttackSpeeds = (float[])defenderOriginalData.AttackSpeeds.Clone();
            sprite = defenderOriginalData.Sprite;
            maxMana = defenderOriginalData.MaxMana;
            skillData = defenderOriginalData.SkillData;
            job = defenderOriginalData.Job;
            character = defenderOriginalData.Character;
        }

        public DefenderData(DefenderOriginalData defenderOriginalData, DefenderDataModifier modifier) : this(defenderOriginalData)
        {
            for (var i = 0; i < currentDamages.Length; i++)
            {
                currentDamages[i] += modifier.damage;
            }

            for (var i = 0; i < currentAttackSpeeds.Length; i++)
            {
                currentAttackSpeeds[i] += modifier.attackSpeed;
            }
        }

        public void ModifyData(DefenderDataModifier modifier)
        {
            for (var i = 0; i < currentDamages.Length; i++)
            {
                currentDamages[i] += modifier.damage;
            }

            for (var i = 0; i < currentAttackSpeeds.Length; i++)
            {
                currentAttackSpeeds[i] += modifier.attackSpeed;
            }
        }
        
    }
}


