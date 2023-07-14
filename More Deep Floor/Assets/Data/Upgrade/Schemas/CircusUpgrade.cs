using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Upgrade;
using UnityEngine;


namespace LNK.MoreDeepFloor.Data.Upgrades
{
    [CreateAssetMenu(fileName = "CircusUpgrade", menuName = "Scriptable Object/Upgrades/CircusUpgrade", order = int.MaxValue)]
    public class CircusUpgrade : UpgradeData
    {
        public override void OnAddAction(UpgradeManager upgradeManager)
        {
            /*Debug.Log("[CircusUpgrade.OnAddAction()] 발동");
            RuntimeTrait_Circus runtimeTraitData = 
                ReferenceManager.instance.traitManager.traitDataTable.FindRuntimeTrait(TraitId.Circus) as RuntimeTrait_Circus;

            for (var i = 0; i < runtimeTraitData.currentTargetNumber.Length; i++)
            {
                runtimeTraitData.currentTargetNumber[i] += 2;
            }*/
        }
    }
}
