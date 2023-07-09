using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using UnityEngine;

public class InfinityTowerData
{
    public string stageName;
    public int monsterNums;
    public MonsterOriginalData monsterOriginalData;
    public MonsterData currentMonsterData;
    public float hpIncreaseRate;

    public InfinityTowerData(InfiniteTowerOriginalData infiniteTowerOriginalData)
    {
        stageName = infiniteTowerOriginalData.StageName;
        monsterNums = infiniteTowerOriginalData.MonsterNums;
        monsterOriginalData = infiniteTowerOriginalData.MonsterOriginalData;
        currentMonsterData = new MonsterData(infiniteTowerOriginalData.MonsterOriginalData);
        hpIncreaseRate = infiniteTowerOriginalData.HpIncreaseRate;
    }

    public void SetNextRound()
    {
        currentMonsterData.hp = (int)(currentMonsterData.hp * hpIncreaseRate);
    }
}
