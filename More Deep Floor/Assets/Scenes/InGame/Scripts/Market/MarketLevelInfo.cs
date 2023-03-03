using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.MarketSystem
{
    public class MarketLevelInfo
    {
        public int level;
        public int currentExp;
        public int maxExp;

        public int[] maxExpList = new int[]
        {
            2, 2, 2, 4, 8 , 12, 16, 20, 24 , 28, 32, 36 , 40  
        };

        public void Init()
        {
            level = 3;
            currentExp = 0;
            maxExp = maxExpList[level];
        }

        //#. Out Index 문제 있음
        public bool ExpUp(int value)
        {
            currentExp += value;
            if (currentExp >= maxExp)
            {
                currentExp = maxExp - currentExp;
                level++;
                maxExp = maxExpList[level];
                return true;
            }
            return false;
        }
    }
}

