using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    public class RoundData
    {
        public string name;
        public MonsterOriginalData monsterData;
        public int monsterNums;
        public int increaseHpRate;

        public RoundData(RoundOriginalData roundOriginalData)
        {
            name = roundOriginalData.RoundName;
            monsterData = roundOriginalData.MonsterOriginal;
            monsterNums = roundOriginalData.MonsterNums;
        }

        public RoundData(string _name, MonsterOriginalData _monsterOriginalData, int _monsterNums)
        {
            name = _name;
            monsterData = _monsterOriginalData;
            monsterNums = _monsterNums;
        }

        public RoundData Clone()
        {
            return new RoundData(name,monsterData,monsterNums);
        }
    }
}
