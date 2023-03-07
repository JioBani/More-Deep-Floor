using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Upgrade;
using UnityEngine;


namespace LNK.MoreDeepFloor.Data.Upgrades
{
    [CreateAssetMenu(fileName = "PowerStatue", menuName = "Scriptable Object/Upgrades/PowerStatue", order = int.MaxValue)]
    public class PowerStatue : UpgradeData
    {
        public override void OnAddAction(UpgradeManager upgradeManager)
        {
            upgradeManager.defenderManager.ModifyDefenderData(new DefenderDataModifier()
            {
                damage = 10
            });
        }
    }
}

