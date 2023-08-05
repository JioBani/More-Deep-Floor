using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.SceneDataSystem
{
    public class SceneData
    {
        private int rewardGold;
    }

    public class InGameResult
    {
        //public StageOriginalData stageData;
        public int round;
        public int rewardGold;
        public bool isClear;

        /*public InGameResult(StageOriginalData _stageData , int _round, int remainGold, bool _isClear)
        {
            stageData = _stageData;
            round = _round;
            rewardGold = remainGold / 10 + _round;
            isClear = _isClear;
        }*/

    }
}


