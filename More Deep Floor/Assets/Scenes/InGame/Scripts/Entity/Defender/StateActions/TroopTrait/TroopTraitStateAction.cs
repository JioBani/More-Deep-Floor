using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.ProbabilityChecks;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.StateActions.TroopTrait
{
    public class TroopTraitState_GoldAttack : DefenderState
    {
        private MarketManager marketManager;

        public float[] percentList;
        
        public TroopTraitState_GoldAttack(
            DefenderStateId _id, 
            DefenderStateData _stateData, 
            Defender _defender) 
            : base(_id, _stateData, _defender)
        {
            
        }

        public override void OnGenerated()
        {
            id = DefenderStateId.TroopTrait_GoldAttack;
            type = DefenderStateType.OnTargetHit;
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


