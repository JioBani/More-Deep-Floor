using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeDataTable", menuName = "Scriptable Object/Upgrades/UpgradeDataTable", order = int.MaxValue)]
    public class UpgradeDataTable : ScriptableObject
    {
        public UpgradeData[] data;

        public UpgradeData GetRandomUpgrade()
        {
            return data[Random.Range(0,data.Length)];
        }
    }
}


