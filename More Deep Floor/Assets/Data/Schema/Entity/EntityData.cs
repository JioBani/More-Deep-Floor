using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Entity
{
    public abstract class EntityData 
    {
        public int cost;
        public string name;
        
        public int[] damages;
        public int[] currentDamages;
        
        public float[] attackSpeeds;
        public float[] currentAttackSpeeds;
        
        public float[] ranges;
        public float[] currentRanges;

        public float[] magicalPowers;
        public float[] currentMagicalPowers;

        public float[] criticalRates;
        public float[] currentCriticalRates;

        public int[] heathPoints;
        public int[] currentHeathPoints;

        public float[] physicalDefenses;
        public float[] currentPhysicalDefense;

        public float[] magicalDefenses;
        public float[] currentMagicalDefenses;

        public float[] moveSpeeds;
        public float[] currentMoveSpeeds;
        
        public int[] maxManas;
        public int[] currentMaxManas;
        
        public Sprite sprite;

        public EntityData(EntityOriginalData entityOriginalData)
        {
            //#. 공격
            damages = (int[])entityOriginalData.Damages.Clone();
            currentDamages = (int[])entityOriginalData.Damages.Clone();
            
            attackSpeeds = (float[])entityOriginalData.AttackSpeeds.Clone();
            currentAttackSpeeds = (float[])entityOriginalData.AttackSpeeds.Clone();
            
            ranges = (float[])entityOriginalData.Ranges.Clone();
            currentRanges = (float[])entityOriginalData.Ranges.Clone();

            magicalPowers = (float[])entityOriginalData.MagicalPowers.Clone();
            currentMagicalPowers = (float[])entityOriginalData.MagicalPowers.Clone();
            
            criticalRates = (float[])entityOriginalData.CriticalRates.Clone();
            currentCriticalRates = (float[])entityOriginalData.CriticalRates.Clone();
            
            //#. 방어
            heathPoints = (int[])entityOriginalData.HeathPoints.Clone();
            currentHeathPoints = (int[])entityOriginalData.HeathPoints.Clone();
            
            physicalDefenses = (float[])entityOriginalData.PhysicalDefense.Clone();
            currentPhysicalDefense = (float[])entityOriginalData.PhysicalDefense.Clone();

            magicalDefenses = (float[])entityOriginalData.MagicalPowers.Clone();
            currentMagicalDefenses = (float[])entityOriginalData.MagicalDefenses.Clone();

            //#. 기타
            moveSpeeds = (float[])entityOriginalData.MoveSpeeds.Clone();
            currentMoveSpeeds = (float[])entityOriginalData.MoveSpeeds.Clone();

            maxManas = (int[])entityOriginalData.MaxManas.Clone();
            currentMaxManas = (int[])entityOriginalData.MaxManas.Clone();

            sprite = entityOriginalData.Sprite;

        }
    }
}


