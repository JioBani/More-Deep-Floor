using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    public class StageData
    {
        public string name;
        public bool isInfinity;
        public int rounds;
        public RoundOriginalData[] roundOriginalDatas;
        public InfiniteTowerOriginalData infiniteTowerOriginalData;
        public StageOriginalData stageOriginalData;
        
        public StageData(StageOriginalData stageOriginalData)
        {
            name = stageOriginalData.Name;
            isInfinity = stageOriginalData.isInfinity;
            rounds = stageOriginalData.Rounds;
            roundOriginalDatas = stageOriginalData.RoundsDatas;
            infiniteTowerOriginalData = stageOriginalData.InfiniteTowerOriginalData;
            this.stageOriginalData = stageOriginalData;
        }

        public StageData(
            string _name, 
            bool _isInfinity, 
            int _rounds, 
            RoundOriginalData[] _roundOriginalDatas, 
            InfiniteTowerOriginalData infiniteTowerOriginalData
            )
        {
            name = _name;
            isInfinity = _isInfinity;
            rounds = _rounds;
            roundOriginalDatas = _roundOriginalDatas;
            this.infiniteTowerOriginalData = infiniteTowerOriginalData;
        }
        
        public StageData Clone()
        {
            return new StageData(name, isInfinity, rounds, roundOriginalDatas, infiniteTowerOriginalData);
        }
    }
}


