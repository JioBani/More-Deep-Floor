using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using LNK.MoreDeepFloor.InGame.Upgrade;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Upgrades
{
    [CreateAssetMenu(fileName = "Gladiator_Furious", menuName = "Scriptable Object/Upgrades/Gladiator Furious", order = int.MaxValue)]

    
    //#. 공격시 대상 주변에 데미지
    public class Upgrade_Gladiator_Furious : UpgradeData
    {
        /*public override void OnAddAction(UpgradeManager upgradeManager)
        {
            List<Defender> defenders =
                ReferenceManager.instance.defenderManager.FindDefendersByTrait(TraitId.Gladiator);

            foreach (var defender in defenders)
            {
                defender.stateController.AddState(DefenderStateId.Upgrade_Gladiator_Furious);
            }
        }*/
    }
}


