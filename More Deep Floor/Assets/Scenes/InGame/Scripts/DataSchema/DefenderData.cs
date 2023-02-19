using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    public class DefenderData
    {
        public DefenderId id;
        public string spawnId;
        public int cost;
        public string name;
        public int[] damages;
        public float[] attackSpeeds;
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
            attackSpeeds = (float[])defenderOriginalData.AttackSpeeds.Clone();
            sprite = defenderOriginalData.Sprite;
            maxMana = defenderOriginalData.MaxMana;
            skillData = defenderOriginalData.SkillData;
            job = defenderOriginalData.Job;
            character = defenderOriginalData.Character;
        }

        public DefenderData(DefenderOriginalData defenderOriginalData, DefenderStatusModifier statusModifier) : this(defenderOriginalData)
        {
            for (var i = 0; i < damages.Length; i++)
            {
                damages[i] += statusModifier.damage;
            }

            for (var i = 0; i < attackSpeeds.Length; i++)
            {
                attackSpeeds[i] += statusModifier.attackSpeed;
            }
        }
    }
}


