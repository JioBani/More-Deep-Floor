using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.ProbabilityChecks;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.StateActions.TroopTrait
{
    public class TroopTraitState_GoldAttack : DefenderStateActionInfoBase
    {
        private MarketManager marketManager;
        
        public TroopTraitState_GoldAttack()
        {
            id = DefenderStateId.TroopTrait_GoldAttack;
            type = DefenderStateType.OnTargetHit;
        }

        public override void OnGenerated()
        {
            marketManager = ReferenceManager.instance.marketManager;
        }

        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            if (ProbabilityCheck.Check(1,1))
            {
                marketManager.GoldChange(1 , "TroopTraitState_GoldAttack");
            }
        }
        
    }
}


