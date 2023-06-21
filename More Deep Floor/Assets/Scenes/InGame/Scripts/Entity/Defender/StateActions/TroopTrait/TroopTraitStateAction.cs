using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.ProbabilityChecks;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;

/*
namespace LNK.MoreDeepFloor.InGame.StateActions.TroopTrait
{
    public class TroopTraitState_GoldAttack : DefenderState
    {
        private MarketManager marketManager;

        public float[] percentList;
        
        public TroopTraitState_GoldAttack(DefenderStateData _stateData, Defender _defender) 
            : base(_stateData, _defender)
        {
            
        }

        public override void OnGenerated()
        {
            actionType = DefenderStateType.OnTargetHit;
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
*/


