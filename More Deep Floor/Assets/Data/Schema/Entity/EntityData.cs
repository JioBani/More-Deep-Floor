using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Entity
{
 

    public struct EntityStatusArray
    {
        public string name;
        public float[] values;
        public float[] currentValues;

        public EntityStatusArray(string _name, float[] _values)
        {
            name = _name;
            values = _values.Clone() as float[];
            currentValues = _values.Clone() as float[];
        }
    }
    
    [Serializable]
    public class EntityData 
    {
        //public int cost;
        public string name;
        public  EntityType entityType { get; private set; }

        public EntityStatusArray damages { get; private set; }
        public EntityStatusArray attackSpeeds { get; private set; }
        public EntityStatusArray ranges { get; private set; }
        public EntityStatusArray magicalPowers { get; private set; }
        public EntityStatusArray criticalRates { get; private set; }
        
        public EntityStatusArray heathPoints { get; private set; }
        public EntityStatusArray physicalDefenses { get; private set; }
        public EntityStatusArray magicalDefenses { get; private set; }
        public EntityStatusArray moveSpeeds { get; private set; }
        public EntityStatusArray maxManas { get; private set; }
        
        public SkillData skillData;


        /*public int[] damages;
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
        public int[] currentMaxManas;*/
        
        public Sprite sprite;
        public Dictionary<string, EntityStatusArray> statusDic { private set; get; }

        public EntityData(EntityOriginalData entityOriginalData)
        {
            name = entityOriginalData.EntityName;
            skillData = entityOriginalData.SkillData;
            entityType = entityOriginalData.EntityType;

            //#. 공격
            damages = new EntityStatusArray("데미지" ,entityOriginalData.Damages);
            magicalPowers = new EntityStatusArray("마법력" ,entityOriginalData.MagicalPowers);
            attackSpeeds = new EntityStatusArray("공격속도" ,entityOriginalData.AttackSpeeds);
            ranges = new EntityStatusArray("사거리" ,entityOriginalData.Ranges);
            criticalRates = new EntityStatusArray("치명타율" ,entityOriginalData.CriticalRates);
            
            heathPoints = new EntityStatusArray("체력" ,entityOriginalData.HeathPoints);
            physicalDefenses = new EntityStatusArray("물리방어력" ,entityOriginalData.PhysicalDefense);
            magicalDefenses = new EntityStatusArray("마법방어력" ,entityOriginalData.MagicalDefenses);
            
            moveSpeeds = new EntityStatusArray("이동속도" ,entityOriginalData.MoveSpeeds);
            maxManas = new EntityStatusArray("최대마나" ,entityOriginalData.MaxManas);

            statusDic = new Dictionary<string, EntityStatusArray>();
            
            statusDic.Add(damages.name , damages);
            statusDic.Add(magicalPowers.name , magicalPowers);
            statusDic.Add(attackSpeeds.name , attackSpeeds);
            statusDic.Add(ranges.name , ranges);
            statusDic.Add(criticalRates.name , criticalRates);
            statusDic.Add(heathPoints.name , heathPoints);
            statusDic.Add(physicalDefenses.name , physicalDefenses);
            statusDic.Add(magicalDefenses.name , magicalDefenses);
            statusDic.Add(moveSpeeds.name , moveSpeeds);
            statusDic.Add(maxManas.name , maxManas);
            
            /*damages = (int[])entityOriginalData.Damages.Clone();
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
            currentMaxManas = (int[])entityOriginalData.MaxManas.Clone();*/

            sprite = entityOriginalData.Sprite;
            entityType = entityOriginalData.EntityType;

        }
    }
}


